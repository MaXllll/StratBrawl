using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SC_game_manager_server : MonoBehaviour {

	[SerializeField]
	private SO_game_settings _game_settings;

	private SC_game_simulation _game_simulation;

	private NetworkView _network_view;

	private bool _b_server_is_ready_planification = false;
	private bool _b_client_is_ready_planification = false;
	private bool _b_server_is_ready_animation = false;
	private bool _b_client_is_ready_animation = false;

	public static SC_game_manager_server _instance;


	void Start()
	{
		if (Network.isServer)
		{
			_network_view = networkView;
			_instance = this;
			_game_simulation = new SC_game_simulation();
			_game_simulation.Init(_game_settings);

			BinaryFormatter _BF = new BinaryFormatter();
			MemoryStream _MS = new MemoryStream();
			_BF.Serialize(_MS, _game_settings._settings);
			byte[] _data_game_settings = _MS.ToArray();
			_MS.Close();
			_network_view.RPC("InitGame", RPCMode.All, _data_game_settings);
		}
	}

	/// SUMMARY : 
	/// PARAMETERS : None.
	/// RETURN : Void.
	[RPC]
	private void ClientIsReadyToStart()
	{
		StartPlanification_Server();
	}
	
	/// SUMMARY : Set the server to start planification phase, and send the Start to client.
	/// PARAMETERS : None.
	/// RETURN : Void.
	private void StartPlanification_Server()
	{
		Debug.Log("a");
		_b_server_is_ready_planification = false;
		_b_client_is_ready_planification = false;
		
		StartCoroutine("EndPlanificationTimer");
		
		_network_view.RPC("StartPlanification_Client", RPCMode.All);
	}
	
	/// SUMMARY : Timer of the planification phase, when the timer is over it's stop the planification phase.
	/// PARAMETERS : None.
	/// RETURN : Void.
	private IEnumerator EndPlanificationTimer()
	{
		yield return new WaitForSeconds(_game_settings._settings._i_planification_time);
		EndPlanification_Server();
	}
	
	/// SUMMARY :  The server player can says when he have finish his planification. If the connected player have already finish, it's stop the planification phase.
	/// PARAMETERS : None.
	/// RETURN : Void.
	public void ServerIsReadyPlanification()
	{
		_b_server_is_ready_planification = true;
		if (_b_client_is_ready_planification)
			EndPlanification_Server();
	}
	
	/// SUMMARY : The connected player can says when he have finish his planification. If the server player have already finish, it's stop the planification phase.
	/// PARAMETERS : None.
	/// RETURN : Void.
	[RPC]
	private void ClientIsReadyPlanification()
	{
		_b_client_is_ready_planification = true;
		if (_b_server_is_ready_planification)
			EndPlanification_Server();
	}
	
	/// SUMMARY : Send Stop planification to client.
	/// PARAMETERS : None.
	/// RETURN : Void.
	private void EndPlanification_Server()
	{
		StopCoroutine("EndPlanificationTimer");
		
		_network_view.RPC("EndPlanification_Client", RPCMode.All);
	}
	
	/// SUMMARY : Get the serialized data, deserialize it, and ask to start the simulation. 
	/// PARAMETERS : Serialized data of the brawlers's actions of the connected player.
	/// RETURN : Void.
	[RPC]
	private void SendActions(byte[] _data_actions)
	{
		BinaryFormatter _BF = new BinaryFormatter();
		MemoryStream _MS = new MemoryStream();
		_MS.Write(_data_actions,0,_data_actions.Length); 
		_MS.Seek(0, SeekOrigin.Begin); 
		Action[,] _actions = (Action[,])_BF.Deserialize(_MS);

		for (int i = 0; i < SC_game_manager_client._instance._brawlers_team_false.Length; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				SC_game_manager_client._instance._brawlers_team_false[i]._actions[j] = _actions[i, j];
			}
		}
		
		Simulation();
	}
	
	/// SUMMARY : Launch the simulation, serialize the result data, and send it to client.
	/// PARAMETERS : None.
	/// RETURN : Void.
	private void Simulation()
	{
		SimulationResult[] _simulation_result = _game_simulation.StartSimulation();
		
		_b_server_is_ready_animation = false;
		_b_client_is_ready_animation = false;
		
		BinaryFormatter _BF = new BinaryFormatter();
		MemoryStream _MS = new MemoryStream();
		_BF.Serialize(_MS, _simulation_result);
		byte[] _data_simulation_result = _MS.ToArray();
		_MS.Close();
		
		_network_view.RPC ("SendResultOfSimulation", RPCMode.All, _data_simulation_result);
	}
	
	/// SUMMARY : The server player can says when he have finish his animation. If the connected player have already finish, it's launch the planification phase of the next turn.
	/// PARAMETERS : None.
	/// RETURN : Void.
	public void ServerIsReadyAnimation()
	{
		_b_server_is_ready_animation = true;
		if (_b_client_is_ready_animation)
			StartPlanification_Server();
	}
	
	/// SUMMARY : The connected player can says when he have finish his animation. If the server player have already finish, it's launch the planification phase of the next turn.
	/// PARAMETERS : None.
	/// RETURN : Void.
	[RPC]
	private void ClientIsReadyAnimation()
	{
		_b_client_is_ready_animation = true;
		if (_b_server_is_ready_animation)
			StartPlanification_Server();
	}
}
