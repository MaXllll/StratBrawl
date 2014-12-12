using UnityEngine;
using System.Collections;

public partial class SC_manager_game : MonoBehaviour {

	[SerializeField]
	private SO_game_settings game_settings;
	public SO_game_settings _game_settings{ get {return game_settings;} }

	[SerializeField]
	private Camera _camera;
	[SerializeField]
	private Transform _T_camera;

	[SerializeField]
	private NetworkView _network_view;
	[SerializeField]
	private SC_manager_ui _manager_ui;
	[SerializeField]
	private SC_ball _ball;
	
	public static SC_manager_game _instance;

}
