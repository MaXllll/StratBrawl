using UnityEngine;
using System.Collections;

public enum ActionType {Wait, Move, Tackle, Pass}
public enum Direction {Up, Down, Right, Left}

public class SC_class_action
{
	public ActionType _action_type {get; private set;}

	public Direction _direction_move {get; private set;}
	public Vector3 _V3_position_pass {get; private set;}

	
	public void SetWait()
	{
		_action_type = ActionType.Wait;
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

	public void SetPass(Vector3 V3_position_pass)
	{
		_action_type = ActionType.Pass;
		_V3_position_pass = V3_position_pass;
	}
}
