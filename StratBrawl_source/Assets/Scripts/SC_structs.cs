using UnityEngine;
using System.Collections;
using System;


[Serializable]
public struct GridPosition
{
	public int _i_x, _i_y;

	public GridPosition(int i_x, int i_y)
	{
		_i_x = i_x;
		_i_y = i_y;
	}

	public Vector3 GetWorldPosition()
	{
		return new Vector3(_i_x, _i_y, 0);
	}
}

[Serializable]
public struct Action
{
	public ActionType _action_type {get; private set;}
	
	public Direction _direction_move {get; private set;}
	public GridPosition _position_pass {get; private set;}


	public void SetNone()
	{
		_action_type = ActionType.None;
	}
	
	public void SetMove(Direction direction_move)
	{
		_action_type = ActionType.Move;
		_direction_move = direction_move;
	}
	
	public void SetTackle(Direction direction_move)
	{
		_action_type = ActionType.Tackle;
		_direction_move = direction_move;
	}
	
	public void SetPass(GridPosition position_pass)
	{
		_action_type = ActionType.Pass;
		_position_pass = position_pass;
	}

	public void SetType(string action_name){
		switch (action_name) {
			case "move":
				_action_type = ActionType.Move;
				break;
			case "pass":
				_action_type = ActionType.Pass;
				break;
			case "tackle":
				_action_type = ActionType.Tackle;
				break;
			case "defense":
				_action_type = ActionType.Defense;
				break;
			default:
				_action_type = ActionType.None;
				break;
		}
	}

	public string ToString(){
		switch (this._action_type) {

			case ActionType.Move:
				return "move";
			case ActionType.Pass:
				return "pass";
			case ActionType.Tackle:
				return "tackle";
			case ActionType.Defense:
				return "defense";	
			case ActionType.None:
				return "none";
			default:
				return "none";
		}
	}
}

[Serializable]
public struct SimulationResult
{
	//TODO
}
