using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SC_manager_ui : MonoBehaviour {

	//[SerializeField]
	//private GameObject _GO_button_back_slots;
	[SerializeField]
	private GameObject _GO_button_back_slots_brawler;
	[SerializeField]
	private GameObject _GO_button_back_types;

	//[SerializeField]
	//private GameObject _GO_panel_actions_slots;
	[SerializeField]
	private GameObject _GO_panel_actions_slots_brawler;
	[SerializeField]
	private GameObject _GO_panel_actions_types;

	[SerializeField]
	private Text _t_button_slot_1;
	[SerializeField]
	private Text _t_button_slot_2;
	[SerializeField]
	private Text _t_button_slot_3;

	public void Awake()
	{
		SetActiveButtonBackSlotsBrawler(false);
		SetActiveButtonBackTypes(false);
		SetActivePanelActionsSlotsBrawler(false);
		SetActivePanelActionsTypes(false);
	}

	public void SetActiveButtonBackSlotsBrawler(bool b_active)
	{
		_GO_panel_actions_slots_brawler.SetActive(b_active);
	}
	/*
	public void SetActiveButtonBackSlots(bool b_active)
	{
		_GO_button_back_slots.SetActive(b_active);
	}
	*/
	public void SetActiveButtonBackTypes(bool b_active)
	{
		_GO_button_back_types.SetActive(b_active);
	}

	public void SetActivePanelActionsSlotsBrawler(bool b_active)
	{
		_GO_panel_actions_slots_brawler.SetActive(b_active);
	}
	/*
	public void SetActivePanelActionsSlots(bool b_active)
	{
		_GO_panel_actions_slots.SetActive(b_active);
	}
	*/
	public void SetActivePanelActionsTypes(bool b_active)
	{
		_GO_panel_actions_types.SetActive(b_active);
	}

	public void SetActionSlotText(string text, int slotNumber){	
		switch (slotNumber+1) {
			case 1:
				_t_button_slot_1.text = text;
				break;
			case 2:
				_t_button_slot_2.text = text;
				break;
			case 3:
				_t_button_slot_3.text = text;
				break;
			default:
				break;
		}
	}

	public void UpdateActionsSlotForBrawler(SC_brawler brawler){
		Debug.Log ("Tamayr");
		/** ne marche pas il faudrait stocker les buttons dans un tableau pour les parcourir en meme temps que les actions
		int nb_actions = SC_manager_game._instance._game_settings._number_actions_per_turn;
		for (int i = 0; i < nb_actions-1; i++) {
			_t_button_slot_1.text = brawler._actions [i].ToString ();
		}
		**/
		_t_button_slot_1.text = brawler._actions [0].ToString ();
		_t_button_slot_2.text = brawler._actions [1].ToString ();
		_t_button_slot_3.text = brawler._actions [2].ToString ();

	}	
}
