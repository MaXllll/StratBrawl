using UnityEngine;
using System.Collections;

public class SC_simulator : MonoBehaviour {

	private BallData _ball_data;
	private BrawlersData _brawlers_data;
	private TerrainData _terrain_data;


	private class TerrainData
	{
		public int _i_terrain_width;
		public int _i_terrain_height;
		public int[,] _i_position_brawlers_current;
		public int[,] _i_position_brawlers_prevision;
		
		public TerrainData(int i_terrain_width, int i_terrain_height)
		{
			_i_terrain_width = i_terrain_width;
			_i_terrain_height = i_terrain_height;
			_i_position_brawlers_current = new int[i_terrain_width, i_terrain_height];
			_i_position_brawlers_prevision = new int[i_terrain_width, i_terrain_height];
			for (int i = 0; i < i_terrain_width; i++)
			{
				for (int j = 0; j < i_terrain_height; j++)
				{
					_i_position_brawlers_current[i, j] = -1;
					_i_position_brawlers_prevision[i, j] = -1;
				}
			}
		}
		
		public void SetBrawlersPositionCurrentInTerrain(BrawlersData brawlers_data)
		{
			for (int i = 0; i < brawlers_data._i_nb_brawlers; i++)
			{
				_i_position_brawlers_current[brawlers_data._brawlers[i]._position_current._i_x, brawlers_data._brawlers[i]._position_current._i_y] = i;
			}
		}
		
		public void SetBrawlePositionPrevisionInTerrain(BrawlerData brawler_data)
		{
			_i_position_brawlers_prevision[brawler_data._position_prevision._i_x, brawler_data._position_prevision._i_y] = brawler_data._i_index;
		}
		
		public void CancelPrevisionAtPosition(GridPosition position)
		{
			_i_position_brawlers_prevision[position._i_x, position._i_y] = -1;
		}
		
		public bool IsInsideTheTerrain(GridPosition position)
		{
			if (position._i_x < 0
			    || position._i_x >= _i_terrain_width
			    || position._i_y < 0
			    || position._i_y >= _i_terrain_height)
				return true;
			else
				return false;
		}
		
		public int GetPrevisionBrawlerIndexAtPosition(GridPosition position)
		{
			if (IsInsideTheTerrain(position))
				return _i_position_brawlers_prevision[position._i_x, position._i_y];
			else
				return -1;
		}

		public int GetCurrentBrawlerIndexAtPosition(GridPosition position)
		{
			if (IsInsideTheTerrain(position))
				return _i_position_brawlers_prevision[position._i_x, position._i_y];
			else
				return -1;
		}

		public int GetFirstBrawlerIndexOnTrajectory(GridPosition position_start, GridPosition position_end)
		{

			// TODO : Calaculate brawlers on trajectory


			return -1;
		}
		
		public void EndOfIteration()
		{
			for (int i = 0; i < _i_terrain_width; i++)
			{
				for (int j = 0; j < _i_terrain_height; j++)
				{
					_i_position_brawlers_prevision[i, j] = -1;
				}
			}
			_i_position_brawlers_current = _i_position_brawlers_prevision;
			for (int i = 0; i < _i_terrain_width; i++)
			{
				for (int j = 0; j < _i_terrain_height; j++)
				{
					_i_position_brawlers_prevision[i, j] = -1;
				}
			}
		}
	}
	
	
	private class BrawlersData
	{
		public int _i_nb_brawlers;
		public BrawlerData[] _brawlers;
		
		public BrawlersData(SC_brawler[] brawlers)
		{
			_i_nb_brawlers = brawlers.Length;
			_brawlers = new BrawlerData[_i_nb_brawlers];
			for (int i = 0; i < _i_nb_brawlers; i++)
			{
				_brawlers[i] = new BrawlerData(brawlers[i]);
			}
		}
		
		public void EndOfIteration()
		{
			for (int i = 0; i < _i_nb_brawlers; i++)
			{
				_brawlers[i].EndOfIteration();
			}
		}
	}
	
	
	private class BrawlerData
	{
		public int _i_index;
		public Action[] _actions;
		public GridPosition _position_current;
		public GridPosition _position_prevision;
		public bool _b_have_the_ball_current;
		public bool _b_have_the_ball_prevision;
		public bool _b_is_KO_current;
		public bool _b_is_KO_prevision;
		public int _i_KO_round_remaining;
		
		public BrawlerData(SC_brawler brawler)
		{
			_i_index = brawler._i_index;
			_actions = brawler._actions;
			_position_current = brawler._position;
			_b_have_the_ball_current = brawler._b_have_the_ball;
			_b_is_KO_current = brawler._b_is_KO;
			_i_KO_round_remaining = brawler._i_KO_round_remaining;
		}
		
		public void EndOfIteration()
		{
			_position_current = _position_prevision;
			_position_prevision = new GridPosition(-1, -1);
			_b_have_the_ball_current = _b_have_the_ball_prevision;
			if (_b_is_KO_prevision)
			{
				_b_is_KO_current = _b_is_KO_prevision;
				_b_is_KO_prevision = false;
				_i_KO_round_remaining = 3;
			}
			else if (_i_KO_round_remaining > 0)
				_i_KO_round_remaining--;
		}
	}
	
	
	private class BallData
	{
		public BallStatus _ball_status_current;
		public BallStatus _ball_status_prevision;
		public int _i_brawler_with_the_ball_current;
		public int _i_brawler_with_the_ball_prevision;
		public GridPosition _position_on_ground_current;
		public GridPosition _position_on_ground_prevision;
		public GridPosition _position_ball_target;

		
		public BallData(SC_ball ball)
		{
			_ball_status_current = ball._ball_status;
			_ball_status_prevision = ball._ball_status;

			switch (_ball_status_current)
			{
			case BallStatus.OnBrawler:
				_i_brawler_with_the_ball_current = ball._brawler_with_the_ball._i_index;
				_i_brawler_with_the_ball_prevision = ball._brawler_with_the_ball._i_index;
				_position_on_ground_current = new GridPosition(-1, -1);
				_position_on_ground_prevision = new GridPosition(-1, -1);
				break;
			case BallStatus.OnGround:
				_i_brawler_with_the_ball_current = -1;
				_i_brawler_with_the_ball_prevision = -1;
				_position_on_ground_current = ball._cell_with_the_ball._position;
				_position_on_ground_prevision = ball._cell_with_the_ball._position;
				break;
			}
			
			_position_ball_target = new GridPosition(-1, -1);
		}

		public void SetOnBrawlerPrevision(int i_brawler_index)
		{
			_ball_status_prevision = BallStatus.OnBrawler;
			_i_brawler_with_the_ball_prevision = i_brawler_index;
			_position_on_ground_prevision = new GridPosition(-1, -1);
		}

		public void SetOnGroundPrevision(GridPosition position)
		{
			_ball_status_prevision = BallStatus.OnGround;
			_i_brawler_with_the_ball_prevision = -1;
			_position_on_ground_prevision = position;
		}
		
		public void EndOfIteration()
		{
			_i_brawler_with_the_ball_current = _i_brawler_with_the_ball_prevision;
			_i_brawler_with_the_ball_prevision = -1;
			_position_on_ground_current = _position_on_ground_prevision;
		}
	}
	
	
	
	/// SUMMARY : Simulate brawlers action.
	/// PARAMETERS : None.
	/// RETURN : Return the results.
	public SimulationResult[] StartSimulation(SC_brawler[] brawlers, SC_ball ball, int i_terrain_width, int i_terrain_height)
	{
		_ball_data = new BallData(ball);
		_brawlers_data = new BrawlersData(brawlers);
		_terrain_data = new TerrainData(i_terrain_width, i_terrain_height);
		_terrain_data.SetBrawlersPositionCurrentInTerrain(_brawlers_data);
		
		int i_nb_iteration = 4;
		SimulationResult[] simulation_result = new SimulationResult[i_nb_iteration];
		
		for (int i = 0; i < i_nb_iteration; i++)
		{
			bool b_ball_is_launch = false;
			GridPosition position_ball_target = new GridPosition(0, 0);
			bool b_ball_is_received_by_a_brawler = false;
			int i_brawler_receiving_the_ball = -1;
			
			for (int j = 0; j < _brawlers_data._i_nb_brawlers; j++)
			{
				if (_brawlers_data._brawlers[j]._b_is_KO_current)
				{
					switch (_brawlers_data._brawlers[j]._actions[i]._action_type)
					{
					case ActionType.Move:
						GridPosition direction = GridPosition.DirectionToGridPosition(_brawlers_data._brawlers[j]._actions[i]._direction_move);
						GridPosition position_to_test = _brawlers_data._brawlers[j]._position_current + direction;
						SetPrevisionPosition(_brawlers_data._brawlers[j], position_to_test);
						break;

					default:
						SetPrevisionPosition(_brawlers_data._brawlers[j], _brawlers_data._brawlers[j]._position_current);
						break;
					}
				}
				else
					SetPrevisionPosition(_brawlers_data._brawlers[j], _brawlers_data._brawlers[j]._position_current);
			}

			for (int j = 0; j < _brawlers_data._i_nb_brawlers; j++)
			{
				if (_brawlers_data._brawlers[j]._b_have_the_ball_current)
				{
					if (_brawlers_data._brawlers[j]._b_is_KO_current
					    && _brawlers_data._brawlers[j]._actions[i]._action_type == ActionType.Pass)
					{
						_brawlers_data._brawlers[j]._b_have_the_ball_prevision = false;

						int i_brawler_on_trajectory = _terrain_data.GetFirstBrawlerIndexOnTrajectory(_brawlers_data._brawlers[j]._position_current, _brawlers_data._brawlers[j]._actions[i]._position);
						if (i_brawler_on_trajectory != -1)
						{
							_brawlers_data._brawlers[i_brawler_on_trajectory]._b_have_the_ball_prevision = true;
							_ball_data.SetOnBrawlerPrevision(i_brawler_on_trajectory);
						}
						else
						{
							_ball_data.SetOnGroundPrevision(_brawlers_data._brawlers[j]._actions[i]._position);
						}
					}
					else
					{
						_brawlers_data._brawlers[j]._b_have_the_ball_prevision = true;
						_ball_data._i_brawler_with_the_ball_prevision = j;
					}
				}
			}

			if (_ball_data._ball_status_prevision == BallStatus.OnGround)
			{
				_terrain_data.GetPrevisionBrawlerIndexAtPosition(_ball_data._position_on_ground_prevision);
			}

			for (int j = 0; j < _brawlers_data._i_nb_brawlers; j++)
			{
				if (_brawlers_data._brawlers[j]._b_is_KO_current
				    && _brawlers_data._brawlers[j]._actions[i]._action_type == ActionType.Tackle)
				{
					GridPosition direction = GridPosition.DirectionToGridPosition(_brawlers_data._brawlers[j]._actions[i]._direction_move);
					GridPosition position_to_tackle = _brawlers_data._brawlers[j]._position_current + direction;
					TackleBrawler(j, _terrain_data.GetPrevisionBrawlerIndexAtPosition(position_to_tackle));
					TackleBrawler(j, _terrain_data.GetCurrentBrawlerIndexAtPosition(position_to_tackle));
				}
			}
			
			_terrain_data.EndOfIteration();
			_brawlers_data.EndOfIteration();
			_ball_data.EndOfIteration();
			
			BrawlerSimulationResult[] brawlers_simulation_result = new BrawlerSimulationResult[_brawlers_data._i_nb_brawlers];
			BallSimulationResult _ball_simulation_result = new BallSimulationResult(b_ball_is_launch,
			                                                                        position_ball_target,
			                                                                        b_ball_is_received_by_a_brawler,
			                                                                        i_brawler_receiving_the_ball);
			
			simulation_result[i] = new SimulationResult(_ball_simulation_result, brawlers_simulation_result);
		}
		
		return simulation_result;
	}


	private void SetPrevisionPosition(BrawlerData brawler, GridPosition position)
	{
		if (!_terrain_data.IsInsideTheTerrain(position))
		{
			brawler._position_prevision = brawler._position_current;
			_terrain_data.SetBrawlePositionPrevisionInTerrain(brawler);
			return;
		}
		int i_brawler_at_prevision_position = _terrain_data.GetPrevisionBrawlerIndexAtPosition(position);
		if (i_brawler_at_prevision_position != -1)
		{
			_terrain_data.CancelPrevisionAtPosition(_brawlers_data._brawlers[i_brawler_at_prevision_position]._position_prevision);
			SetPrevisionPosition(_brawlers_data._brawlers[i_brawler_at_prevision_position], _brawlers_data._brawlers[i_brawler_at_prevision_position]._position_current);

			brawler._position_prevision = brawler._position_current;
			_terrain_data.SetBrawlePositionPrevisionInTerrain(brawler);
			return;
		}
		brawler._position_prevision = position;
		_terrain_data.SetBrawlePositionPrevisionInTerrain(brawler);
	}

	private void TackleBrawler(int i_brawler_from, int i_brawler_to_tackle)
	{
		if (i_brawler_to_tackle != -1)
		{
			_brawlers_data._brawlers[i_brawler_to_tackle]._b_is_KO_prevision = true;
			if (_brawlers_data._brawlers[i_brawler_to_tackle]._b_have_the_ball_prevision)
			{
				_brawlers_data._brawlers[i_brawler_to_tackle]._b_have_the_ball_prevision = false;
				_brawlers_data._brawlers[i_brawler_from]._b_have_the_ball_prevision = true;
				_ball_data._i_brawler_with_the_ball_prevision = i_brawler_from;
			}
		}
	}
}
