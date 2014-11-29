using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public partial class SC_manager_game : MonoBehaviour {

	private bool _b_server_is_ready_planification = false;
	private bool _b_client_is_ready_planification = false;
	private bool _b_server_is_ready_animation = false;
	private bool _b_client_is_ready_animation = false;


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
		Debug.Log("Start planification");

		_b_server_is_ready_planification = false;
		_b_client_is_ready_planification = false;

		StartCoroutine(EndPlanificationTimer());

		_network_view.RPC("StartPlanification_Client", RPCMode.All);
	}

	/// SUMMARY : Timer of the planification phase, when the timer is over it's stop the planification phase.
	/// PARAMETERS : None.
	/// RETURN : Void.
	private IEnumerator EndPlanificationTimer()
	{
		yield return new WaitForSeconds(_game_settings._i_planification_time);
		EndPlanification_Server();
	}

	/// SUMMARY :  The server player can says when he have finish his planification. If the connected player have already finish, it's stop the planification phase.
	/// PARAMETERS : None.
	/// RETURN : Void.
	private void ServerIsReadyPlanification()
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
		Debug.Log("End planification");

		StopCoroutine(EndPlanificationTimer());

		_network_view.RPC("EndPlanification_Client", RPCMode.All);
	}

	/// SUMMARY : Get the serialized data, deserialize it, and ask to start the simulation. 
	/// PARAMETERS : Serialized data of the brawlers's actions of the connected player.
	/// RETURN : Void.
	[RPC]
	private void SendActions(byte[] _data_actions)
	{
		Debug.Log("Client actions arrived");

		BinaryFormatter _BF = new BinaryFormatter();
		MemoryStream _MS = new MemoryStream();
		_MS.Write(_data_actions,0,_data_actions.Length); 
		_MS.Seek(0, SeekOrigin.Begin); 
		Action[,] _actions = (Action[,])_BF.Deserialize(_MS);

		for (int i = 0; i < _brawlers_team_false.Length; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				_brawlers_team_false[i]._actions[j] = _actions[i, j];
			}
		}
		
		Simulation();
	}

	/// SUMMARY : Launch the simulation, serialize the result data, and send it to client.
	/// PARAMETERS : None.
	/// RETURN : Void.
	private void Simulation()
	{
		Debug.Log("Simulation");

		SimulationResult _simulation_result = Simulate();

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
	private void ServerIsReadyAnimation()
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
