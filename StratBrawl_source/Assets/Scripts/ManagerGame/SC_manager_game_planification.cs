using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public partial class SC_manager_game : MonoBehaviour {

	public Sprite buttonSprite;

	public SC_brawler _selected_brawler;
	public Action _selected_action;
	public Action[] _actions_chosen;
	private int _number_of_chosen_actions;
	private int _selected_slot;

	//Planification

	/// SUMMARY : Store the current brawler in the attribute, then open the panel containing the slots of action for this turn 
	/// aswell as the back button.Last thingswitch off the brawler buttons.
	/// PARAMETERS : The brawler that will execute the selected actions in the slots or null if it is called by CloseMenuActionsTypes.
	/// RETURN : Void.
	public void OpenMenuActionsSlots(SC_brawler _brawler=null)
	{
		_manager_ui.SetActivePanelActionsSlots(true);
		_manager_ui.SetActiveButtonBackSlots(true);
		// Test to check if it is the first time the method is called or not this turn, since only CloseMenuActionsTypes calls it
		// without _brawler parameter
		if (_brawler != null) {			
			_selected_brawler = _brawler;
			SC_manager_game._instance.SetActiveButtonsBrawlers (false, _brawler._b_team);
		}
	}

	/// SUMMARY : Opens the panel containing the different types slots of action possible
	/// PARAMETERS : 
	/// RETURN : Void.
	public void OpenMenuActionsTypes()
	{
		_manager_ui.SetActivePanelActionsSlots(false);		
		_manager_ui.SetActivePanelActionsTypes(true);		
		_manager_ui.SetActiveButtonBackSlots(false);	
		_manager_ui.SetActiveButtonBackTypes(true);		
	}
	
	public void CloseMenuActionsSlots()
	{
		_selected_brawler = null;
		SC_manager_game._instance.SetActiveButtonsBrawlers(true,_b_player_team);
		_manager_ui.SetActiveButtonBackSlots(false);
		_manager_ui.SetActivePanelActionsSlots(false);
	}

	public void CloseMenuActionsTypes()
	{
		_manager_ui.SetActiveButtonBackTypes(false);
		_manager_ui.SetActivePanelActionsTypes(false);
		OpenMenuActionsSlots ();
	}

	
	public void SetActiveCellsNextToSelectedCell(bool active)
	{
		GridPosition cell_selected_position = _selected_brawler._position;
		SC_cell cell_right = _cells_gameField[(int)cell_selected_position.GetWorldPosition().x+1, 
		                                      (int)cell_selected_position.GetWorldPosition().y];
		cell_right._GO_button_canvas.SetActive (active);
		
		SC_cell cell_left = _cells_gameField[(int)cell_selected_position.GetWorldPosition().x-1, 
		                                     (int)cell_selected_position.GetWorldPosition().y];
		cell_left._GO_button_canvas.SetActive (active);
		
		SC_cell cell_top = _cells_gameField[(int)cell_selected_position.GetWorldPosition().x, 
		                                    (int)cell_selected_position.GetWorldPosition().y+1];
		cell_top._GO_button_canvas.SetActive(active);
		
		SC_cell cell_bottom = _cells_gameField[(int)cell_selected_position.GetWorldPosition().x, 
		                                       (int)cell_selected_position.GetWorldPosition().y-1];
		cell_bottom._GO_button_canvas.SetActive (active);
	}

	/// SUMMARY : Register the type of action chose by the player to display the right elements ( arrow if it is a move, nothing if it
	/// is a defend, etc) but DOESN'T add it yet to the array of chosen actions
	/// PARAMETERS : The name of the action type.
	/// RETURN : Void.
	public void RegisterAction(string action_name){
		Debug.Log (action_name);
		_selected_action = new Action ();
		_selected_action.SetType (action_name);
		Debug.Log(_selected_action.ToString());
	}

	public void RegisterSelectedSlot(int selected_slot){
		_selected_slot = selected_slot;
	}

	public void AddActionToArray(){
		_actions_chosen [_selected_slot] = _selected_action;
		_number_of_chosen_actions++;
	}
}
