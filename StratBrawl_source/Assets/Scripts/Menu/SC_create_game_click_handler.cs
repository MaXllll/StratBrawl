using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SC_create_game_click_handler : MonoBehaviour
{
		[SerializeField]
		public GameObject
				currentPanel;
		[SerializeField]
		public GameObject
				nextPanel;
		[SerializeField]	
		public Text
				title;

		/// SUMMARY : 
		/// PARAMETERS : None.
		/// RETURN : Void.
		public void ClickCreateButton (InputField gameName)
		{
				currentPanel.SetActive (false);
				RegisterAGame (gameName.text);
				nextPanel.SetActive (true);
		}
		
		public void RegisterAGame (string gameName)
		{
	
				Network.InitializeServer (32, 25002, !Network.HavePublicAddress ());
				MasterServer.ipAddress = "127.0.0.1";
				MasterServer.port = 23466;
				MasterServer.RegisterHost ("1V1", gameName, "Test Comment");
				Debug.LogError ("Je créer la partie " + gameName + " sur le port " + MasterServer.port);
		}
}
