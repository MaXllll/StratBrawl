using UnityEngine;
using System.Collections;

public class SC_lobby : MonoBehaviour {

	private int play_count;

	void OnPlayerConnected(NetworkPlayer player){
		Application.LoadLevel ("game_test");
	}

	void OnConnectedToServer()	
	{
		Application.LoadLevel("game_test");
	}


}
