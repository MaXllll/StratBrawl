using UnityEngine;
using System.Collections;

public class SO_game_settings : ScriptableObject {

	[SerializeField]
	private int i_terrain_width = 7;
	public int _i_terrain_width { get{ return i_terrain_width; } }

	[SerializeField]
	private int i_terrain_height = 12;
	public int _i_terrain_height { get{ return i_terrain_height; } }

	[SerializeField]
	private int i_nb_turn_max = 10;
	public int _i_nb_turn_max { get{ return i_nb_turn_max; } }

	[SerializeField]
	private int i_planification_time = 60;
	public int _i_planification_time { get{ return i_planification_time; } }

	[SerializeField]
	private int i_nb_brawlers_per_team = 5;
	public int _i_nb_brawlers_per_team { get{ return i_nb_brawlers_per_team; } }

	[SerializeField]
	private GridPosition[] positions_brawlers_attack_formation = new GridPosition[] {
																		new GridPosition(3,5),
																		new GridPosition(1,4),
																		new GridPosition(5,4),
																		new GridPosition(2,2),
																		new GridPosition(4,2)};
	public GridPosition[] _positions_brawlers_attack_formation { get{ return positions_brawlers_attack_formation; } }

	[SerializeField]
	private GridPosition[] positions_brawlers_defense_formation = new GridPosition[] {
																		new GridPosition(3,1),
																		new GridPosition(1,2),
																		new GridPosition(5,2),
																		new GridPosition(2,4),
																		new GridPosition(4,4)};
	public GridPosition[] _positions_brawlers_defense_formation { get{ return positions_brawlers_defense_formation; } }

	[SerializeField]
	private float f_orthographic_size = 5;
	public float _f_orthographic_size { get{ return f_orthographic_size; } }
}
