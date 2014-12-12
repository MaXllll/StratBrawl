using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public partial class SC_manager_game : MonoBehaviour {

	private bool _b_player_team;


	/// SUMMARY : Initialize th game manager.
	/// PARAMETERS : None.
	/// RETURN : Void.
	void Start()
	{
		_b_player_team = Network.isServer;
		
		GenerateGameField(_game_settings._i_gameField_width, _game_settings._i_gameField_height);
		GenerateBrawlers(_game_settings._i_nb_brawlers_per_team);
		_ball.Init();
		
		SetEngagePosition(true);
		_instance = this;

		_network_view = networkView;
		Network.isMessageQueueRunning = true;
		if (Network.isClient)
			_network_view.RPC("ClientIsReadyToStart", RPCMode.Server);
		StartPlanification_Client ();
		Debug.Log ("tamer");
	}

	/// SUMMARY : Start the planification phase on client side.
	/// PARAMETERS : None.
	/// RETURN : Void.
	[RPC]
	private void StartPlanification_Client()
	{
		Debug.Log ("tamermaggle");
		ResetActionsOfAllBrawlers();
		SetActiveButtonsBrawlers(true, _b_player_team);
	}

	/// SUMMARY : End the planification phase on client side.
	/// PARAMETERS : None.
	/// RETURN : Void.
	[RPC]
	private void EndPlanification_Client()
	{
		SetActiveButtonsBrawlers(false, _b_player_team);
		if (Network.isClient)
		{
			byte[] _actions = GenerateActionsDataToSend();
			_network_view.RPC("SendActions", RPCMode.Server, _actions);
		}
	}

	/// SUMMARY : Generate, serialize and return data of the brawler's actions.
	/// PARAMETERS : None.
	/// RETURN : Return serialize data.
	private byte[] GenerateActionsDataToSend()
	{
		Action[,] _actions = new Action[_brawlers_team_false.Length, 4];
		for (int i = 0; i < _brawlers_team_false.Length; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				_actions[i, j] = _brawlers_team_false[i]._actions[j];
			}
		}

		BinaryFormatter _BF = new BinaryFormatter();
		MemoryStream _MS = new MemoryStream();
		_BF.Serialize(_MS, _actions);
		byte[] _data_actions = _MS.ToArray();
		_MS.Close();

		return _data_actions;
	}

	/// SUMMARY : Send the serialized data of the simulation result and deserialize it on client side.
	/// PARAMETERS : Serialed data of the simulation result.
	/// RETURN : Void.
	[RPC]
	private void SendResultOfSimulation(byte[] _data_simulation_result)
	{
		BinaryFormatter _BF = new BinaryFormatter();
		MemoryStream _MS = new MemoryStream();
		_MS.Write(_data_simulation_result,0,_data_simulation_result.Length); 
		_MS.Seek(0, SeekOrigin.Begin); 
		SimulationResult[] _simulation_result = (SimulationResult[])_BF.Deserialize(_MS);

		StartCoroutine(ResultAnimation(_simulation_result));
	}

	/// SUMMARY : Launch the result animation of the simulation and send Ready to the server when animation is done.
	/// PARAMETERS : Result of the simulation.
	/// RETURN : Void.
	private IEnumerator ResultAnimation(SimulationResult[] _simulation_result)
	{
		yield return Animate(_simulation_result);

		if (Network.isClient)
		{
			_network_view.RPC("ClientIsReadyAnimation", RPCMode.Server);
		}
		else if (Network.isServer)
		{
			ServerIsReadyAnimation();
		}
	}

	/// SUMMARY : Initialize brawlers and ball for engage.
	/// PARAMETERS : The team who have the ball.
	/// RETURN : Void.
	private void SetEngagePosition(bool b_team_with_ball)
	{
		SetBrawlersEngagePositions(_game_settings._positions_brawlers_attack_formation, _game_settings._positions_brawlers_defense_formation, b_team_with_ball);
		_ball.SetBrawlerWithTheBall(GetTeamCaptain(b_team_with_ball));
	}
}
