using UnityEngine;
using System.Collections;

public class SC_brawler : MonoBehaviour {

	public GameObject _GO_button_open_menu_actions;

	[HideInInspector]
	public int _i_index { get; private set; }
	public GridPosition _position { get; private set; }

	public Transform _T_brawler { get; private set; }

	private bool _b_team = false;
	public bool _b_have_the_ball = false;

	public SC_class_action[] _actions;
	

	public void Init(int i_index, bool b_team)
	{
		_i_index = i_index;
		_b_team = b_team;
		_T_brawler = transform;
		InitActions(4);
	}

	private void InitActions(int i_nb_actions)
	{
		_actions = new SC_class_action[i_nb_actions];
		for(int i = 0; i < i_nb_actions; i++)
		{
			_actions[i] = new SC_class_action();
		}
	}

	public void SetPosition(GridPosition position)
	{
		_position = position;
		Vector3 V3_world_position = SC_manager_terrain.GridPositionToWorldPosition(_position);
		V3_world_position.z = -1;
		_T_brawler.position = V3_world_position;
	}

	public void OpenMenuActions()
	{
		SC_manager_main.OpenMenuActions(this);
	}
}
