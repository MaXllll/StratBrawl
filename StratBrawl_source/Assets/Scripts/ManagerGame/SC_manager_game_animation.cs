using UnityEngine;
using System.Collections;

public partial class SC_manager_game : MonoBehaviour {

	[SerializeField]
	private float _f_duration_animation = 0.5f;


	/// SUMMARY : This makes it easy to create, name and place unique new ScriptableObject asset files.
	/// PARAMETERS : Data of the result of the simulation.
	/// RETURN : Void.
	private IEnumerator Animate(SimulationResult[] _simulation_results)
	{
		for (int i = 0; i < _simulation_results.Length; i++)
		{
			for (int j = 0; j < _simulation_results[i]._brawlers_simulation_result.Length; j++)
			{
				if (!_simulation_results[i]._brawlers_simulation_result[j]._b_is_KO)
					StartCoroutine(_brawlers[j]._animation.PlayAnimation(_simulation_results[i]._brawlers_simulation_result[j]._action_type,
					                                                     _simulation_results[i]._brawlers_simulation_result[j]._position_target,
					                                                     _f_duration_animation));
			}

			switch (_simulation_results[i]._ball_simulation_result._ball_status)
			{
			case BallStatus.OnBrawler:
				if (_ball._brawler_with_the_ball == null || _simulation_results[i]._ball_simulation_result._i_brawler_with_the_ball != _ball._brawler_with_the_ball._i_index)
				{
					Debug.Log(_simulation_results[i]._ball_simulation_result._i_brawler_with_the_ball);
					StartCoroutine(_ball._animation.InterpolationOnMovingTagret(_brawlers[_simulation_results[i]._ball_simulation_result._i_brawler_with_the_ball]._T_brawler, _f_duration_animation));
				}
				break;
			case BallStatus.OnGround:
				if (_ball._cell_with_the_ball == null || _simulation_results[i]._ball_simulation_result._position_on_ground != _ball._cell_with_the_ball._position)
				{
					StartCoroutine(_ball._animation.Interpolation(_simulation_results[i]._ball_simulation_result._position_on_ground.GetWorldPosition(), _f_duration_animation));
				}
				break;
			}

			yield return new WaitForSeconds(_f_duration_animation);

			for (int j = 0; j < _simulation_results[i]._brawlers_simulation_result.Length; j++)
			{
				if (_simulation_results[i]._brawlers_simulation_result[j]._action_type == ActionType.Move)
					_brawlers[j].SetPosition(_simulation_results[i]._brawlers_simulation_result[j]._position_target);
			}

			switch (_simulation_results[i]._ball_simulation_result._ball_status)
			{
			case BallStatus.OnBrawler:
				_ball.SetBrawlerWithTheBall(_brawlers[_simulation_results[i]._ball_simulation_result._i_brawler_with_the_ball]);
				break;
			case BallStatus.OnGround:
				_ball.SetBallOnTheCell(_cells_gameField[_simulation_results[i]._ball_simulation_result._position_on_ground._i_x,_simulation_results[i]._ball_simulation_result._position_on_ground._i_y]);
				break;
			}

			yield return new WaitForSeconds(_f_duration_animation * 0.5f);
		}
	}
}
