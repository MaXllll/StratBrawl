using UnityEngine;
using System.Collections;

public class SC_manager_ui : MonoBehaviour {
	
	[SerializeField]
	private GameObject _GO_button_back;
	[SerializeField]
	private GameObject _GO_panel_actions_slots;
	[SerializeField]
	private GameObject _GO_panel_actions;
	
	
	public void Awake()
	{
		SetActiveButtonBack(false);
		SetActivePanelActionsSlot(false);
		SetActivePanelActionsTypes(false);
	}
	
	public void SetActiveButtonBack(bool b_active)
	{
		_GO_button_back.SetActive(b_active);
	}

	public void SetActivePanelActionsSlot(bool b_active)
	{
		_GO_panel_actions_slots.SetActive(b_active);
	}

	public void SetActivePanelActionsTypes(bool b_active)
	{
		_GO_panel_actions.SetActive(b_active);
	}
}
