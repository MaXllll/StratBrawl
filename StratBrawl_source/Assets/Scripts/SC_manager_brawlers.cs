using UnityEngine;
using System.Collections;

public class SC_manager_brawlers : MonoBehaviour {

	[SerializeField]
	private GameObject _GO_prefab_brawler;

	public SC_brawler[] _brawlers { get; private set;}


	public void GenerateBrawlers(int i_nb_brawlers)
	{
		Transform T_root = transform;
		_brawlers = new SC_brawler[i_nb_brawlers];
		
		for (int i = 0; i < i_nb_brawlers; i++)
		{

			GameObject GO_tmp = (GameObject) Instantiate(_GO_prefab_brawler);
			GO_tmp.transform.parent = T_root;
			_brawlers[i] = GO_tmp.GetComponent<SC_brawler>();
			_brawlers[i].Init(i, false);
			_brawlers[i].SetPosition(new GridPosition(Random.Range(0, SC_manager_terrain._instance._i_width), Random.Range(0, SC_manager_terrain._instance._i_height)));
		}
	}

	public void SetActiveButtonsActions(bool b_active)
	{
		for (int i = 0; i < _brawlers.Length; i++)
		{
			_brawlers[i]._GO_button_open_menu_actions.SetActive(b_active);
		}
	}
}
