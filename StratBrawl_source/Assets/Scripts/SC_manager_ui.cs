using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SC_manager_ui : MonoBehaviour {

	[SerializeField]
	private GameObject _GO_button_back_slots;
	[SerializeField]
	private GameObject _GO_button_back_types;

	[SerializeField]
	private GameObject _GO_panel_actions_slots;
	[SerializeField]
	private GameObject _GO_panel_actions_types;

	public void Awake()
	{
		SetActiveButtonBackSlots(false);
		SetActiveButtonBackTypes(false);
		SetActivePanelActionsSlots(false);
		SetActivePanelActionsTypes(false);
	}

	public void SetActiveButtonBackSlots(bool b_active)
	{
		_GO_button_back_slots.SetActive(b_active);
	}

	public void SetActiveButtonBackTypes(bool b_active)
	{
		_GO_button_back_types.SetActive(b_active);
	}

	public void SetActivePanelActionsSlots(bool b_active)
	{
		_GO_panel_actions_slots.SetActive(b_active);
	}

	public void SetActivePanelActionsTypes(bool b_active)
	{
		_GO_panel_actions_types.SetActive(b_active);
	}

	public void SetActiveGameFieldCells(bool b_active){
//		SC_cell[] cells = _GO_gameField.GetComponentsInChildren (SC_cell);
//		foreach ( SC_cell cell in cells ){
//			Button cell_button = cell.GetComponentInChildren(Button);
//			GameObject cell_button_canvas = (GameObject) cell.GetComponentInChildren(Canvas);
//			cell_button_canvas.SetActive(b_active);
//		}
	}
}
