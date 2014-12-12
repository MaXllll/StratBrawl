﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public partial class SC_manager_game : MonoBehaviour {

	public SC_brawler _selected_brawler;
	public Action _selected_action;
	//public Action[] _actions_chosen;
	private int _number_of_chosen_actions;
	private int _selected_slot;
	private SC_cell _selected_cell;

	[SerializeField]
	private Sprite _Spr_ui_arrow;

	//Planification

	/// SUMMARY : Store the current brawler in the attribute, then open the panel containing the slots of action for this turn 
	/// aswell as the back button.Last thingswitch off the brawler buttons.
	/// PARAMETERS : The brawler that will execute the selected actions in the slots or null if it is called by CloseMenuActionsTypes.
	/// RETURN : Void.
	public void OpenMenuActionsSlots(SC_brawler _brawler=null)
	{
		_manager_ui.SetActivePanelActionsSlotsBrawler(true);
		_manager_ui.SetActiveButtonBackSlotsBrawler(true);
		// Test to check if it is the first time the method is called or not this turn, since only CloseMenuActionsTypes calls it
		// without _brawler parameter
		if (_brawler != null) {			
			_selected_brawler = _brawler;
			_manager_ui.UpdateActionsSlotForBrawler(_selected_brawler);
			SC_manager_game._instance.SetActiveButtonsBrawlers (false, _brawler._b_team);
		}
	}

	/// SUMMARY : Opens the panel containing the different types slots of action possible
	/// PARAMETERS : 
	/// RETURN : Void.
	public void OpenMenuActionsTypes()
	{
		_manager_ui.SetActivePanelActionsSlotsBrawler(false);		
		_manager_ui.SetActivePanelActionsTypes(true);		
		_manager_ui.SetActiveButtonBackSlotsBrawler(false);	
		_manager_ui.SetActiveButtonBackTypes(true);		
	}
	
	public void CloseMenuActionsSlots()
	{
		_selected_brawler = null;
		SC_manager_game._instance.SetActiveButtonsBrawlers(true,_b_player_team);
		_manager_ui.SetActiveButtonBackSlotsBrawler(false);
		_manager_ui.SetActivePanelActionsSlotsBrawler(false);

	}

	public void CloseMenuActionsTypes()
	{
		_manager_ui.SetActiveButtonBackTypes(false);
		_manager_ui.SetActivePanelActionsTypes(false);
		SetActiveCellsForMove (false);
		SetActiveCellsForPass (false);
		OpenMenuActionsSlots ();
	}

	
	public void SetActiveCellsForMove(bool active)
	{
		GridPosition cell_selected_position = CalculateCurrentPositionWithFutureActions();
		int x = (int)cell_selected_position.GetWorldPosition ().x;
		int y = (int)cell_selected_position.GetWorldPosition ().y;

		SetCellActive (x + 1, y, active);
		SetCellActive (x - 1, y, active);
		SetCellActive (x, y+1, active);
		SetCellActive (x, y-1, active);
	}

		public void SetActiveCellsForPass(bool active)
	{
		GridPosition cell_selected_position = CalculateCurrentPositionWithFutureActions();
		int nb_cells = _game_settings._i_pass_nb_cells;
		Debug.Log(nb_cells);
		int x = (int)cell_selected_position.GetWorldPosition ().x;
		int y = (int)cell_selected_position.GetWorldPosition ().y;
		for (int i=1; i< nb_cells+1; i++) {
			SetCellActive(x + i, y, active);
			SetCellActive(x - i, y, active);
			SetCellActive(x, y + i, active);
			SetCellActive(x, y - i, active);
			SetCellActive(x-i+1, y-i+1, active);
			SetCellActive(x-i+1, y+i-1, active);
			SetCellActive(x+i-1, y-i+1, active);
			SetCellActive(x+i-1, y+i-1, active);
		}
	}

	/// SUMMARY : Set Active state of the cell corresponding to the coordinates to true ou false depending on the 'active' parameters 
	/// Try catch the line that fetch the cell in the array cuz it will oftenly be a wrong coordinate if the brawler is close to a wall
	/// PARAMETERS : int pos_x : the x coordinate of the cell, int pos_y : the y coordinate of the cell, bool active : the state of Activit
	/// to be applied to the cell
	/// RETURN : Void.
	private void SetCellActive(int pos_x, int pos_y,bool active){
		try{
			SC_cell cell = _cells_gameField [pos_x, pos_y];
			cell._GO_button.SetActive (active);
		}catch (IndexOutOfRangeException exception){
			// Do nothing because we don't care, it will happens in a lot of cases (mostly when the brawler is close to a wall)
		}
	}

	private GridPosition CalculateCurrentPositionWithFutureActions(){
		if (_selected_slot == 0) {
			return _selected_brawler._position;
		}
		else if (_selected_slot == 1) {
			if(_selected_brawler._actions[0]._action_type != ActionType.Move){
				return _selected_brawler._position;
			}else{
				return CalculateNextPositionWithDirection(_selected_brawler._position, _selected_brawler._actions[0]._direction_move);
			}
		}
		else{
			if(_selected_brawler._actions[0]._action_type != ActionType.Move && _selected_brawler._actions[1]._action_type != ActionType.Move){
				return _selected_brawler._position;
			}
			else if(_selected_brawler._actions[0]._action_type != ActionType.Move){				
				return CalculateNextPositionWithDirection(_selected_brawler._position, _selected_brawler._actions[1]._direction_move);
			}
			else if(_selected_brawler._actions[1]._action_type != ActionType.Move){				
				return CalculateNextPositionWithDirection(_selected_brawler._position, _selected_brawler._actions[0]._direction_move);
			}else{
				GridPosition tempNewPosition = CalculateNextPositionWithDirection(_selected_brawler._position, _selected_brawler._actions[0]._direction_move);
				return CalculateNextPositionWithDirection(tempNewPosition, _selected_brawler._actions[1]._direction_move);
			}
		}
	}

	private GridPosition CalculateNextPositionWithDirection(GridPosition position, Direction direction){
		GridPosition new_position = position;
		switch (direction) {
			case Direction.Right:
				new_position._i_x += 1;
				return new_position;
			case Direction.Left:
				new_position._i_x -= 1;
				return new_position;
			case Direction.Up:
				new_position._i_y +=1;
				return new_position;
			case Direction.Down:
				new_position._i_y -=1;
				return new_position;
			default:
				return new_position;
		}	
	}

	private Direction DetermineMoveDirection(GridPosition selected_cell_position){
		GridPosition brawlerPositionWithNextMoves = CalculateCurrentPositionWithFutureActions ();
		// TODO remplacer brawlerPositionWithNextMoves par _selected_brawler._position si on ne dit pas que le joueur choisit ses actions 
		// dans l'ordre
		if (brawlerPositionWithNextMoves._i_x < selected_cell_position._i_x) 
		{
			return Direction.Right;
		} 
		else if (brawlerPositionWithNextMoves._i_x > selected_cell_position._i_x) 
		{
			return Direction.Left;
		} 
		else if (brawlerPositionWithNextMoves._i_y < selected_cell_position._i_y) 
		{
			return Direction.Up;
		}
		else{
			return Direction.Down;
		}
	}

	public void RegisterSelectedSlot(int selected_slot){
		_selected_slot = selected_slot;
	}
	/*
	private void AddActionToArray(){
		Debug.Log (_selected_slot);
		_actions_chosen [_selected_slot] = _selected_action;
		_number_of_chosen_actions++;
	}
	*/
	private void AddActionToBrawlerArray(){
		_selected_brawler._actions[_selected_slot] = _selected_action;
		_number_of_chosen_actions++;
	}

	public void RegisterSelectedCellPositionForAction(SC_cell selected_cell){
		_selected_cell = selected_cell;
		// TODO si il y a un bug sur le tackle, voir ici car le tackle ne passera pas par cette méthode donc la 
		// la selected_cell ne sera pas a jour

		_selected_action._position = selected_cell._position;
		_selected_action._direction_move = DetermineMoveDirection (selected_cell._position);
		// TODO optimisation en mettant juste tout les cells à false?
		SetActiveCellsForMove (false);
		SetActiveCellsForPass (false);
	}

	public void UpdateActionSlotText(){
		//_manager_ui.SetActionSlotText (_selected_action.ToString (), _selected_slot);		
		_manager_ui.SetActionSlotText (_selected_action.ToString (), _selected_slot);
		//AddActionToArray ();
		AddActionToBrawlerArray ();		
		CloseMenuActionsTypes ();		
		DisplayChosenActionsOnField ();
	}

	/// SUMMARY : Register the type of action chose by the player to display the right elements ( arrow if it is a move, nothing if it
	/// is a defend, etc) but DOESN'T add it yet to the array of chosen actions
	/// PARAMETERS : The name of the action type.
	/// RETURN : Void.
	public void RegisterAction(string action_name){
		_selected_action = new Action ();
		_selected_action.SetType (action_name);
		//DisplayChosenActionsOnField ();
	}

	public void DisplayChosenActionsOnField(){
		Debug.Log ("TIMMY");
		if(_selected_action._action_type == ActionType.Move){			
			Debug.Log ("TIMMY2");
			//_selected_cell._GO_button_canvas.SetActive (true); 
			//_selected_cell._IMG_action_display.SetActive(true);
			_selected_cell._IMG_action_display.sprite = _Spr_ui_arrow;
			_selected_cell._IMG_action_display.color = new Color(255,2555,255,255);
			//_selected_cell._UIButton_button.image.rectTransform.Rotate(new Vector3(0,0,90));
		}
	}
}