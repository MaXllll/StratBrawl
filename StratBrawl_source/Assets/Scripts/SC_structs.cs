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
}

[Serializable]
public struct SimulationResult
{
	//TODO
}
