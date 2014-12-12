using UnityEngine;
using System.Collections;

public partial class SC_manager_game : MonoBehaviour {

	[SerializeField]
	private float _f_duration_animation = 1f;


	/// SUMMARY : This makes it easy to create, name and place unique new ScriptableObject asset files.
	/// PARAMETERS : Data of the result of the simulation.
	/// RETURN : Void.
	private IEnumerator Animate(SimulationResult[] _simulation_results)
	{
		for (int i = 0; i < _simulation_results.Length; i++)
		{
			//TODO

			for (int j = 0; j < _simulation_results[i]._brawlers_simulation_result.Length; i++)
			{
				if (!_simulation_results[i]._brawlers_simulation_result[j]._b_is_KO)
					StartCoroutine(_brawlers[j]._animation.PlayAnimation(_simulation_results[i]._brawlers_simulation_result[j]._action_type,
					                                                     _simulation_results[i]._brawlers_simulation_result[j]._position_target,
					                                                     _f_duration_animation));
			}

			if (_simulation_results[i]._ball_simulation_result._b_is_launch)
			{
				_ball.ResetOwner();
				StartCoroutine(_ball._animation.Interpolation(_simulation_results[i]._ball_simulation_result._position_target.GetWorldPosition(), _f_duration_animation));
				if (_simulation_results[i]._ball_simulation_result._b_is_received_by_a_brawler)
					_ball.SetBrawlerWithTheBall(_brawlers[_simulation_results[i]._ball_simulation_result._i_brawler]);
				else
					_ball.SetBallOnTheCell(_cells_terrain[_simulation_results[i]._ball_simulation_result._position_target._i_x,_simulation_results[i]._ball_simulation_result._position_target._i_y]);
			}

			yield return new WaitForSeconds(_f_duration_animation + 0.5f);
		}
	}
}
