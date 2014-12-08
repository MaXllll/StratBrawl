using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SC_play_menu_click_handler : MonoBehaviour
{
		private List<Button> gamesButton = new List<Button> ();
		[SerializeField]
		private GameObject
				currentPanel;
		[SerializeField]
		private GameObject
				serversContainer;
		[SerializeField]
		private Button
				buttonServerPrefab;


		/*void Start ()
		{
				StartCoroutine (RefreshServersList ());	
		}*/

		public void ClickGameButton (GameObject panelToShow)
		{
				currentPanel.SetActive (false);
				panelToShow.SetActive (true);
		}

		public void ClickCreateGameButton (GameObject panelToShow)
		{
				currentPanel.SetActive (false);
				panelToShow.SetActive (true);
		}

		IEnumerator RefreshServersList ()
		{
				while (true) {
						//foreach (Button btn in gamesButton) {
						//		Destroy (btn);
						//}
						//gamesButton.Clear ();
						if (MasterServer.PollHostList ().Length != 0) {
								HostData[] hostData = MasterServer.PollHostList ();
								//Debug.LogError ("\n" + hostData.Length + "\n");
								int i = 0;
								while (i < hostData.Length) {
										//Debug.LogError (hostData [i].gameName + " " + hostData [i].comment);
										Button button = Instantiate (buttonServerPrefab) as Button;
										button.GetComponentInChildren<Text> ().text = hostData [i].gameName;
										button.transform.SetParent (serversContainer.transform, false);

										gamesButton.Add (button);
										//button.transform.Translate (new Vector3 (0, 2, 0));
										

										i++;
								}
								MasterServer.ClearHostList ();
						}
						yield return new WaitForSeconds (5f);
				}
		}

		void Awake ()
		{
				MasterServer.ClearHostList ();
				MasterServer.RequestHostList ("1V1");
				StartCoroutine (RefreshServersList ());	
		}

		/*void Update ()
		{
				if (MasterServer.PollHostList ().Length != 0) {
						HostData[] hostData = MasterServer.PollHostList ();
						int i = 0;
						while (i < hostData.Length) {
								Button button = Instantiate (buttonServerPrefab) as Button;
								button.transform.SetParent (serversContainer.transform, false);
								i++;
						}
						MasterServer.ClearHostList ();
				}
		}*/
}
