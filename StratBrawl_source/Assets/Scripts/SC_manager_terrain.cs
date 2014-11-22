using UnityEngine;
using System.Collections;

public struct GridPosition
{
	public int _i_x, _i_y;

	public GridPosition(int i_x, int i_y)
	{
		_i_x = i_x;
		_i_y = i_y;
	}
}

public class SC_manager_terrain : MonoBehaviour {

	static public SC_manager_terrain _instance { get; private set; }

	public int _i_width { get; private set; }
	public int _i_height { get; private set; }

	private float _f_grid_offset = 1.1f;

	[SerializeField]
	private GameObject _GO_prefab_cell;

	private SC_cell[,] _cells;

	
	void Awake()
	{
		_instance = this;
	}
	
	public void GenerateTerrain(int i_width, int i_height)
	{
		Transform T_root = transform;
		_i_width = i_width;
		_i_height = i_height;
		_cells = new SC_cell[i_width, i_height];

		for(int i = 0; i < i_width; i++)
		{
			for(int j = 0; j < i_height; j++)
			{
				GameObject GO_tmp = (GameObject) Instantiate(_GO_prefab_cell, new Vector3(_f_grid_offset * i, _f_grid_offset * j, 0f), Quaternion.identity);
				GO_tmp.transform.parent = T_root;
				_cells[i,j] = GO_tmp.GetComponent<SC_cell>();
				_cells[i,j].Init(new GridPosition(i, j));
			}
		}
	}

	static public Vector3 GridPositionToWorldPosition(GridPosition grid_position)
	{
		if (_instance == null || grid_position._i_x >= _instance._cells.GetLength(0) || grid_position._i_y >= _instance._cells.GetLength(1))
			return Vector3.zero;

		return _instance._cells[grid_position._i_x, grid_position._i_y]._V3_world_position;
	}
}
