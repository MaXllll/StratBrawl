using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayMenuClickHandler : MonoBehaviour {

	public GameObject currentPanel;

	public void ClickGameButton(GameObject panelToShow){
		currentPanel.SetActive (false);
		panelToShow.SetActive (true);
	}

	public void ClickCreateGameButton(GameObject panelToShow){
		currentPanel.SetActive (false);
		panelToShow.SetActive (true);
	}

}
