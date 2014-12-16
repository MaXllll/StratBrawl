using UnityEngine;
using System.Collections;

public partial class SC_manager_game : MonoBehaviour {

	[SerializeField]
	private float _f_duration_animation = 0.5f;


	/// SUMMARY : This makes it easy to create, name and place unique new ScriptableObject asset files.
	/// PARAMETERS : Data of the result of the simulation.
	/// RETURN : Void.
	private IEnumerator Animate(SimulationResult[] simulation_results)
	{
		for (int i = 0; i < simulation_results.Length; i++)
		{
			for (int j = 0; j < simulation_results[i]._brawlers_simulation_result.Length; j++)
			{
				if (!simulation_results[i]._brawlers_simulation_result[j]._b_is_KO)
					StartCoroutine(_brawlers[j]._animation.PlayAnimation(simulation_results[i]._brawlers_simulation_result[j]._action_type,
					                                                     simulation_results[i]._brawlers_simulation_result[j]._position_target,
					                                                     _f_duration_animation));
			}

			switch (simulation_results[i]._ball_simulation_result._ball_status)
			{
			case BallStatus.OnBrawler:
				if (_ball._brawler_with_the_ball == null || simulation_results[i]._ball_simulation_result._i_brawler_with_the_ball != _ball._brawler_with_the_ball._i_index)
				{
					StartCoroutine(_ball._animation.InterpolationOnMovingTagret(_brawlers[simulation_results[i]._ball_simulation_result._i_brawler_with_the_ball]._T_brawler, _f_duration_animation));
				}
				break;
			case BallStatus.OnGround:
				if (_ball._cell_with_the_ball == null || simulation_results[i]._ball_simulation_result._position_on_ground != _ball._cell_with_the_ball._position)
				{
					StartCoroutine(_ball._animation.Interpolation(simulation_results[i]._ball_simulation_result._position_on_ground.GetWorldPosition(), _f_duration_animation));
				}
				break;
			}

			yield return new WaitForSeconds(_f_duration_animation);

			for (int j = 0; j < simulation_results[i]._brawlers_simulation_result.Length; j++)
			{
				if (simulation_results[i]._brawlers_simulation_result[j]._action_type == ActionType.Move)
					_brawlers[j].SetPosition(simulation_results[i]._brawlers_simulation_result[j]._position_target);
			}

			switch (simulation_results[i]._ball_simulation_result._ball_status)
			{
			case BallStatus.OnBrawler:
				_ball.SetBrawlerWithTheBall(_brawlers[simulation_results[i]._ball_simulation_result._i_brawler_with_the_ball]);
				break;
			case BallStatus.OnGround:
				_ball.SetBallOnTheCell(_cells_gameField[simulation_results[i]._ball_simulation_result._position_on_ground._i_x,simulation_results[i]._ball_simulation_result._position_on_ground._i_y]);
				break;
			}

			yield return new WaitForSeconds(_f_duration_animation * 0.5f);

			if (simulation_results[i]._b_is_goal)
			{
				IncrementScore(simulation_results[i]._b_team_who_scores);
				SetEngagePosition(!simulation_results[i]._b_team_who_scores);
				break;
			}
		}
	}
}
