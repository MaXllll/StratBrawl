using UnityEngine;
using System.Collections;

public partial class SC_manager_game : MonoBehaviour {

	[SerializeField]
	private SO_game_settings _game_settings;

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


	// Planification
	//
	//	static public void OpenMenuActions(SC_brawler _brawler)
	//	{
	//		_instance._selected_brawler = _brawler;
	//		_instance._manager_brawlers.SetActiveButtonsActions(false);
	//		_instance._manager_ui.SetActiveButtonBack(true);
	//		_instance._manager_ui.SetActivePanelActionsSlot(true);
	//	}
	//
	//	public void CloseMenuActions()
	//	{
	//		_selected_brawler = null;
	//		_manager_brawlers.SetActiveButtonsActions(true);
	//		_manager_ui.SetActiveButtonBack(false);
	//		_manager_ui.SetActivePanelActionsSlot(false);
	//	}
}
