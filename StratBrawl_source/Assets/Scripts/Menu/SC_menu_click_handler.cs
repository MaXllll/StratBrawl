using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SC_menu_click_handler: MonoBehaviour
{
		[SerializeField]
		private GameObject
				currentPanel;

		public void ClickPlayButton (GameObject panelToShow)
		{
				currentPanel.SetActive (false);
				//RegisterTestServer ();
				RetrieveHostList ();
				
				panelToShow.SetActive (true);
		}

		public void ClickOptionsButton (GameObject panelToShow)
		{
				currentPanel.SetActive (false);
				panelToShow.SetActive (false);
		}

		public void ClickQuitButton ()
		{
				Application.Quit ();
		}

		/// SUMMARY : Retrieve from MasterServer, servers waiting for players
		/// PARAMETERS : None.
		/// RETURN : Return the servers waiting for players
		public void RetrieveHostList ()
		{
				MasterServer.ipAddress = "127.0.0.1";
				MasterServer.port = 23466;
				MasterServer.RequestHostList ("1V1");
		}



		/*public void RegisterTestServer ()
		{
				Network.InitializeServer (32, 25002, !Network.HavePublicAddress ());
				MasterServer.RegisterHost ("1V1", "Test1", "Test Comment");
				MasterServer.RegisterHost ("1V1", "Test2", "Test2 Comment");
				MasterServer.RegisterHost ("1V1", "Test3", "Test3 Comment");
				Debug.Log ("BLAAA");
		}*/
}
