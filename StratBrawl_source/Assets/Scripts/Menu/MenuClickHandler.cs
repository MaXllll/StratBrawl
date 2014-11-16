using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuClickHandler: MonoBehaviour {

	public GameObject currentPanel;


	public void ClickPlayButton(GameObject panelToShow){
		currentPanel.SetActive (false);
		panelToShow.SetActive (true);
	}

	public void ClickOptionsButton(GameObject panelToShow){
		currentPanel.SetActive (false);
		panelToShow.SetActive (false);
	}

	public void ClickQuitButton(){
		Application.Quit ();
	}

}
