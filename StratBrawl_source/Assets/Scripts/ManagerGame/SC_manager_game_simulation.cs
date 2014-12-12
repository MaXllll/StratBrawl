using UnityEngine;
using System.Collections;

public partial class SC_manager_game : MonoBehaviour {


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

		public int GetBrawlerPrevisionIndexAtPosition(GridPosition position)
		{
			if (IsInsideTheTerrain(position))
				return _i_position_brawlers_prevision[position._i_x, position._i_y];
			else
				return -1;
		}

		public void SetPrevisionToCurrent()
		{
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

		public void SetPrevisionToCurrent()
		{
			for (int i = 0; i < _i_nb_brawlers; i++)
			{
				_brawlers[i].SetPrevisionToCurrent();
			}
		}
	}


	private class BrawlerData
	{
		public int _i_index;
		public Action[] _actions;
		public GridPosition _position_current;
		public GridPosition _position_prevision;
		public bool _b_have_the_ball;
		public bool _b_is_KO_current;
		public bool _b_is_KO_prevision;
		public int _i_KO_round_remaining;

		public BrawlerData(SC_brawler brawler)
		{
			_i_index = brawler._i_index;
			_actions = brawler._actions;
			_position_current = brawler._position;
			_b_have_the_ball = brawler._b_have_the_ball;
			_b_is_KO_current = brawler._b_is_KO;
			_i_KO_round_remaining = brawler._i_KO_round_remaining;
		}

		public void CancelPrevision()
		{
			_position_prevision = _position_current;
		}

		public void SetPrevisionToCurrent()
		{
			_position_current = _position_prevision;
			_position_prevision = new GridPosition(-1, -1);
			_b_is_KO_current = _b_is_KO_prevision;
		}
	}

	
	private class BallData
	{
		int _i_brawler_with_the_ball_current;
		int _i_brawler_with_the_ball_prevision;
		GridPosition _position_on_ground_current;
		GridPosition _position_on_ground_prevision;
		bool _b_ball_is_launch;
		GridPosition _position_ball_target;
		bool _b_ball_is_received_by_a_brawler;
		int _i_brawler_receiving_the_ball;

		public BallData(SC_ball ball)
		{
			if (ball._brawler_with_the_ball != null)
				_i_brawler_with_the_ball_current = ball._brawler_with_the_ball._i_index;
			else
				_i_brawler_with_the_ball_current = -1;

			if (ball._cell_with_the_ball != null)
				_position_on_ground_current = ball._cell_with_the_ball._position;
			else
				_position_on_ground_current = new GridPosition(-1, -1);

			_b_ball_is_launch = false;
			_position_ball_target = new GridPosition(-1, -1);
			_b_ball_is_received_by_a_brawler = false;
			_i_brawler_receiving_the_ball = -1;
			_i_brawler_with_the_ball_prevision = -1;
			_position_on_ground_prevision = new GridPosition(-1, -1);
		}

		public void SetPrevisionToCurrent()
		{
			_i_brawler_with_the_ball_current = _i_brawler_with_the_ball_prevision;
			_i_brawler_with_the_ball_prevision = -1;
			_position_on_ground_current = _position_on_ground_prevision;
			_position_on_ground_prevision = new GridPosition(-1, -1);
		}
	}



	/// SUMMARY : Simulate brawlers action.
	/// PARAMETERS : None.
	/// RETURN : Return data of the result.
	private SimulationResult[] Simulate()
	{
		BallData ball_data = new BallData(_ball);
		BrawlersData brawlers_data = new BrawlersData(_brawlers);
		TerrainData terrain_data = new TerrainData(_i_terrain_width, _i_terrain_height);
		terrain_data.SetBrawlersPositionCurrentInTerrain(brawlers_data);

		int i_nb_iteration = 4;
		SimulationResult[] simulation_results = new SimulationResult[i_nb_iteration];

		for (int i = 0; i < i_nb_iteration; i++)
		{
			bool b_ball_is_launch = false;
			GridPosition position_ball_target = new GridPosition(0, 0);
			bool b_ball_is_received_by_a_brawler = false;
			int i_brawler_receiving_the_ball = -1;

			for (int j = 0; j < brawlers_data._i_nb_brawlers; j++)
			{
				if (brawlers_data._brawlers[j]._b_is_KO_current)
				{
					switch (brawlers_data._brawlers[j]._actions[i]._action_type)
					{
					case ActionType.None:
						brawlers_data._brawlers[j]._position_prevision = brawlers_data._brawlers[i]._position_current;
						terrain_data.SetBrawlePositionPrevisionInTerrain(brawlers_data._brawlers[j]);
						break;

					case ActionType.Move:
						GridPosition position_to_test = brawlers_data._brawlers[j]._position_current + brawlers_data._brawlers[j]._actions[i]._direction_move;
						if (!terrain_data.IsInsideTheTerrain(position_to_test))
						{
							brawlers_data._brawlers[j]._position_prevision = brawlers_data._brawlers[j]._position_current;
							terrain_data.SetBrawlePositionPrevisionInTerrain(brawlers_data._brawlers[j]);
							break;
						}
						int i_brawler_at_prevision_position = terrain_data.GetBrawlerPrevisionIndexAtPosition(position_to_test);
						if (i_brawler_at_prevision_position != -1)
						{
							terrain_data.CancelPrevisionAtPosition(brawlers_data._brawlers[i_brawler_at_prevision_position]._position_prevision);
							brawlers_data._brawlers[i_brawler_at_prevision_position].CancelPrevision();
							terrain_data.SetBrawlePositionPrevisionInTerrain(brawlers_data._brawlers[i_brawler_at_prevision_position]);

							brawlers_data._brawlers[j]._position_prevision = brawlers_data._brawlers[i]._position_current;
							terrain_data.SetBrawlePositionPrevisionInTerrain(brawlers_data._brawlers[j]);
							break;
						}
						brawlers_data._brawlers[j]._position_prevision = position_to_test;
						terrain_data.SetBrawlePositionPrevisionInTerrain(brawlers_data._brawlers[j]);
						break;

					case ActionType.Tackle:

						break;

					case ActionType.Pass:

						break;
					}
				}
				else
				{
					brawlers_data._brawlers[j]._position_prevision = brawlers_data._brawlers[j]._position_current;
					terrain_data.SetBrawlePositionPrevisionInTerrain(brawlers_data._brawlers[j]);
				}
			}

			terrain_data.SetPrevisionToCurrent();
			brawlers_data.SetPrevisionToCurrent();
			ball_data.SetPrevisionToCurrent();

			BrawlerSimulationResult[] brawlers_simulation_results = new BrawlerSimulationResult[_brawlers.Length];
			BallSimulationResult _ball_simulation_result = new BallSimulationResult(b_ball_is_launch,
			                                                                        position_ball_target,
			                                                                        b_ball_is_received_by_a_brawler,
			                                                                        i_brawler_receiving_the_ball);

			simulation_results[i] = new SimulationResult(_ball_simulation_result,
			                                            brawlers_simulation_results);
		}

		return simulation_results;
	}
}
