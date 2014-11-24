using UnityEngine;
using System.Collections;

public class SC_manager_main : MonoBehaviour {

	static public SC_manager_main _instance { get; private set; }

	[SerializeField]
	private SC_manager_ui _manager_ui;
	[SerializeField]
	private SC_manager_terrain _manager_terrain;
	[SerializeField]
	private SC_manager_brawlers _manager_brawlers;
	[SerializeField]
	private SC_ball _ball;

	private SC_brawler _selected_brawler;
	public int _i_selected_action_slot;



	// Init

	void Awake()
	{
		_instance = this;
	}

	void Start()
	{
		_manager_terrain.GenerateTerrain(8, 12);
		_manager_brawlers.GenerateBrawlers(5);
		_ball.Init();

		_ball.SetBrawlerWithTheBall(_manager_brawlers._brawlers[0]);
	}



	// Planification

	static public void OpenMenuActions(SC_brawler _brawler)
	{
		_instance._selected_brawler = _brawler;
		_instance._manager_brawlers.SetActiveButtonsActions(false);
		_instance._manager_ui.SetActiveButtonBack(true);
		_instance._manager_ui.SetActivePanelActionsSlot(true);
	}

	public void CloseMenuActions()
	{
		_selected_brawler = null;
		_manager_brawlers.SetActiveButtonsActions(true);
		_manager_ui.SetActiveButtonBack(false);
		_manager_ui.SetActivePanelActionsSlot(false);
	}
}
