using UnityEngine;
using System.Collections;

public class SC_actions_simulator : MonoBehaviour {

	public SC_brawler[] _brawlers;


	public void Simulate()
	{
		int i_nb_iteration = 0;
		for(int i = 0; i < _brawlers.Length; i++)
		{
			if (_brawlers[i]._actions.Length > i_nb_iteration)
			{
				i_nb_iteration = _brawlers[i]._actions.Length;
			}
		}

		for(int i = 0; i < i_nb_iteration; i++)
		{
			for(int j = 0; j < _brawlers.Length; j++)
			{
				if (i < _brawlers[i]._actions.Length)
				{

				}
			}
		}


	}
}
