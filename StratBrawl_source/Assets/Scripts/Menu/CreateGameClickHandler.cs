using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateGameClickHandler : MonoBehaviour {

	public GameObject currentPanel;
	public GameObject nextPanel;
	public Text title;

	public void ClickCreateButton(InputField gameName){
		currentPanel.SetActive (false);
		nextPanel.SetActive (true);
	}

}
