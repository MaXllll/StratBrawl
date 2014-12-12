using UnityEngine;
using System.Collections;
using System;


[Serializable]
public struct GridPosition
{
	public int _i_x, _i_y;

	public static GridPosition zero { get { return new GridPosition(0, 0); } }
	public static GridPosition right { get { return new GridPosition(1, 0); } }
	public static GridPosition left { get { return new GridPosition(-1, 0); } }
	public static GridPosition up { get { return new GridPosition(0, 1); } }
	public static GridPosition down { get { return new GridPosition(0, 0 -1); } }

	public GridPosition(int i_x, int i_y)
	{
		_i_x = i_x;
		_i_y = i_y;
	}

	public Vector3 GetWorldPosition()
	{
		return new Vector3(_i_x, _i_y, 0);
	}

	public static GridPosition DirectionToGridPosition(Direction direction)
	{
		switch(direction)
		{
		case Direction.Right:
			return right;
		case Direction.Left:
			return left;
		case Direction.Up:
			return up;
		case Direction.Down:
			return down;
		default :
			return zero;
		}
	}

	public static GridPosition operator+(GridPosition position_a, GridPosition position_b)
	{
		return new GridPosition(position_a._i_x + position_b._i_x, position_a._i_y + position_b._i_y);
	}
}


[Serializable]
public struct Action
{
	public ActionType _action_type { get; private set; }

//	private Direction _direction;
//	public GridPosition _direction_move
//	{
//		get
//		{ 
//			switch (_direction)
//			{
//			case Direction.Right:
//				return GridPosition.right;
//			case Direction.Left:
//				return GridPosition.left;
//			case Direction.Up:
//				return GridPosition.up;
//			case Direction.Down:
//				return GridPosition.down;
//			default:
//				return GridPosition.zero;
//			}
//		}
//	}
//
//	public GridPosition _position_pass { get; private set; }

	public Direction _direction_move {get; set;}
	public GridPosition _position {get; set;}

	public void SetPosition(GridPosition position)
	{
		_position = position;
	}
	public void SetNone()
	{
		_action_type = ActionType.None;
	}
	/*
	public GridPosition _position_pass {get; private set;}
	
	public void SetMove(Direction direction_move)
	{
		_action_type = ActionType.Move;
		_direction = direction_move;
	}
	
	public void SetTackle(Direction direction_move)
	{
		_action_type = ActionType.Tackle;
		_direction = direction_move;
	}
	
	public void SetPass(GridPosition position_pass)
	{
		_action_type = ActionType.Pass;
		_position_pass = position_pass;
	}

	*/
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
				return "Move : " + _direction_move.ToString();
			case ActionType.Pass:
				return "Pass";
			case ActionType.Tackle:
				return "Tackle";
			case ActionType.Defense:
				return "Defense";	
			case ActionType.None:
				return "None";
			default:
				return "None";
		}
	}
}


[Serializable]
public struct SimulationResult
{
	public BallSimulationResult _ball_simulation_result { get; private set; }
	public BrawlerSimulationResult[] _brawlers_simulation_result { get; private set; }

	public SimulationResult(BallSimulationResult ball_simulation_result, BrawlerSimulationResult[] brawler_simulation_result)
	{
		_ball_simulation_result = ball_simulation_result;
		_brawlers_simulation_result = brawler_simulation_result;
	}
}


[Serializable]
public struct BrawlerSimulationResult
{
	public ActionType _action_type { get; private set; }
	public GridPosition _position_target { get; private set; }
	public bool _b_is_KO { get; private set; }

	public BrawlerSimulationResult(ActionType action_type, GridPosition position_target, bool b_is_KO)
	{
		_action_type = action_type;
		_position_target = position_target;
		_b_is_KO = b_is_KO;
	}
}


[Serializable]
public struct BallSimulationResult
{
	public bool _b_is_launch { get; private set; }
	public GridPosition _position_target { get; private set; }
	public bool _b_is_received_by_a_brawler { get; private set; }
	public int _i_brawler { get; private set; }

	public BallSimulationResult(bool b_is_launch, GridPosition position_target, bool b_is_received_by_a_brawler, int i_brawler)
	{
		_b_is_launch = b_is_launch;
		_position_target = position_target;
		_b_is_received_by_a_brawler = b_is_received_by_a_brawler;
		_i_brawler = i_brawler;
	}
}
