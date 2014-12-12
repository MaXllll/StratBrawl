using UnityEngine;
using System.Collections;

public partial class SC_manager_game : MonoBehaviour {
	
	private int _i_gameField_width ;
	private int _i_gameField_height;

	[SerializeField]
	private Transform _T_root_cells_gameField;
	[SerializeField]
	private GameObject _GO_prefab_cell_gameField;

	private SC_cell[,] _cells_gameField;
	

	/// SUMMARY : Generate the gameField.
	/// PARAMETERS : Size of the gameField.
	/// RETURN : Void.
	public void GenerateGameField(int i_width, int i_height)
	{
		_i_gameField_width = i_width;
		_i_gameField_height = i_height;
		_cells_gameField = new SC_cell[i_width, i_height];

		for(int i = 0; i < i_width; i++)
		{
			for(int j = 0; j < i_height; j++)
			{
				GameObject GO_tmp = (GameObject) Instantiate(_GO_prefab_cell_gameField, new Vector3(i, j, 0f), Quaternion.identity);
				GO_tmp.transform.parent = _T_root_cells_gameField;
				_cells_gameField[i,j] = GO_tmp.GetComponent<SC_cell>();
				_cells_gameField[i,j].Init(new GridPosition(i, j));
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
		return new GridPosition(_i_gameField_width - grid_position._i_x -1 , grid_position._i_y);
		//return new GridPosition( grid_position._i_x , _i_gameField_height-grid_position._i_y-1);
	}
}
