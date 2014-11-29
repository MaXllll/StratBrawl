using UnityEngine;
using System.Collections;

public partial class SC_manager_game : MonoBehaviour {
	
	private int _i_terrain_width ;
	private int _i_terrain_height;

	[SerializeField]
	private Transform _T_root_cells_terrain;
	[SerializeField]
	private GameObject _GO_prefab_cell_terrain;

	private SC_cell[,] _cells_terrain;
	

	/// SUMMARY : Generate the terrain.
	/// PARAMETERS : Size of the terrain.
	/// RETURN : Void.
	public void GenerateTerrain(int i_width, int i_height)
	{
		_i_terrain_width = i_width;
		_i_terrain_height = i_height;
		_cells_terrain = new SC_cell[i_width, i_height];

		for(int i = 0; i < i_width; i++)
		{
			for(int j = 0; j < i_height; j++)
			{
				GameObject GO_tmp = (GameObject) Instantiate(_GO_prefab_cell_terrain, new Vector3(i, j, 0f), Quaternion.identity);
				GO_tmp.transform.parent = _T_root_cells_terrain;
				_cells_terrain[i,j] = GO_tmp.GetComponent<SC_cell>();
				_cells_terrain[i,j].Init(new GridPosition(i, j));
			}
		}

		_T_camera.position = new Vector3((i_width - 1) * 0.5f, (i_height - 1) * 0.5f, -10);
		_camera.orthographicSize = _game_settings._f_orthographic_size;
	}

	/// SUMMARY : 
	/// PARAMETERS : 
	/// RETURN : 
	private GridPosition GetSymmetricPosition(GridPosition grid_position)
	{
		return new GridPosition(grid_position._i_x, _i_terrain_height - grid_position._i_y - 1);
	}
}
