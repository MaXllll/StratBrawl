using UnityEngine;
using System.Collections;

public class SC_cell : MonoBehaviour {

	public GameObject _GO_button_open_menu_actions;

	public GridPosition _position;
	public Transform _T_cell { get; private set;}
	public Vector3 _V3_world_position { get; private set;}
	[HideInInspector]
	public SC_brawler _brawler_on_the_cell;
	[HideInInspector]
	public bool _b_is_ball_on_this_cell = false;


	public void Init(GridPosition position)
	{
		_position = position;
		_V3_world_position = transform.position;
		_T_cell = transform;
	}

}
