using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SC_create_game_click_handler : MonoBehaviour
{
		[SerializeField]
		private GameObject
				_GO_current_panel;
		[SerializeField]
		private GameObject
				_GO_next_panel;
		[SerializeField]
		private Text
				_TE_lobby_title;

		/// SUMMARY : 
		/// PARAMETERS : None.
		/// RETURN : Void.
		public void ClickCreateButton (InputField gameName)
		{
				_GO_current_panel.SetActive (false);
				_GO_next_panel.SetActive (true);
				_TE_lobby_title.text = gameName.text;
				RegisterAGame (gameName.text);
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
