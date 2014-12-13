using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SC_play_menu_click_handler : MonoBehaviour
{
		private List<GameObject> _games_button = new List<GameObject> ();
		private float _time = 4;
		private const int _REFRESH_RATE = 5;
		[SerializeField]
		private GameObject
				_GO_current_panel;
		[SerializeField]
		private GameObject
				_GO_servers_container;
		[SerializeField]
		private GameObject
				_GO_button_server;
		[SerializeField]
		private Text
				_TE_lobby_title;
		[SerializeField]
		private GameObject
				_GO_lobby_panel;
		private int bla = 0;

		public void ClickCreateGameButton (GameObject panel_to_show)
		{
				_GO_current_panel.SetActive (false);
				panel_to_show.SetActive (true);
		}


		/// SUMMARY : Retrieve master server hosts list
		/// PARAMETERS : None.
		/// RETURN : Void.
		void RefreshServersList ()
		{	
				foreach (GameObject btn_obj in _games_button) {
						Destroy (btn_obj);
				}
				_games_button.Clear ();
				if (MasterServer.PollHostList ().Length != 0) {
						HostData[] hostData = MasterServer.PollHostList ();
						int i = 0;
						while (i < hostData.Length) {
								HostData server = hostData [i];
								string server_name = server.gameName;

								GameObject button_obj = Instantiate (_GO_button_server) as GameObject;
								Button button = button_obj.GetComponentInChildren<Button> ();

								button.GetComponentInChildren<Text> ().text = server_name + bla;
								button.transform.SetParent (_GO_servers_container.transform, false);

								button.onClick.AddListener (delegate {
										NextPanel (server);
								});

								_games_button.Add (button_obj);
								//button.transform.Translate (new Vector3 (0, 2, 0));

				
								i++;
								bla++;
						}
						MasterServer.ClearHostList ();
				}
		}

		void Awake ()
		{
				MasterServer.ClearHostList ();
				MasterServer.RequestHostList ("1V1");

		}

		void Update ()
		{
				_time += Time.deltaTime;
				if (_time > _REFRESH_RATE) {
				_time = 0;
				RefreshServersList ();
				}
		}

		/// SUMMARY : The user want to join a server
		/// PARAMETERS : None.
		/// RETURN : Void.
		void NextPanel (HostData server)
		{
				_GO_current_panel.SetActive (false);
				_GO_lobby_panel.SetActive (true);
				_TE_lobby_title.text = server.gameName;
				Network.Connect (server);
		}

		void OnPlayerConnected (NetworkPlayer player)
		{
				Debug.Log ("Player connected from " + player.ipAddress + ":" + player.port);
		}

}
