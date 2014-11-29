using UnityEngine;
using System.Collections;

public class SC_ball : MonoBehaviour {

	private Transform _T_ball;
	private SC_brawler _brawler_with_the_ball;
	private SC_cell _cell_with_the_ball;
//	public GridPosition? _position
//	{
//		get 
//		{
//			if (_brawler_with_the_ball != null)
//				return _brawler_with_the_ball._position;
//			else if (_cell_with_the_ball != null)
//				return _cell_with_the_ball._position;
//			else
//				return null;
//		}
//	}


	/// SUMMARY : Initialize the ball.
	/// PARAMETERS : None.
	/// RETURN : Void.
	public void Init()
	{
		_T_ball = transform;
	}

	/// SUMMARY : Set the brawler who have the ball.
	/// PARAMETERS : The brawler who have the ball.
	/// RETURN : Void.
	public void SetBrawlerWithTheBall(SC_brawler _brawler)
	{
		ResetOwner();

		_brawler_with_the_ball = _brawler;
		_brawler_with_the_ball._b_have_the_ball = true;
		_T_ball.parent = _brawler._T_brawler;
		_T_ball.localPosition = new Vector3(0f, 0f, -1f);
	}

	/// SUMMARY : Set the cell where the ball is.
	/// PARAMETERS : The cell.
	/// RETURN : Void.
	public void SetBallOnTheCell(SC_cell _cell)
	{
		ResetOwner();

		if (_cell._brawler_on_the_cell != null)
			SetBrawlerWithTheBall(_cell._brawler_on_the_cell);
		else
		{
			_cell_with_the_ball = _cell;
			_cell_with_the_ball._b_is_ball_on_this_cell = true;
			_T_ball.parent = _cell._T_cell;
			_T_ball.localPosition = new Vector3(0f, 0f, -2f);
		}
	}

	/// SUMMARY : Set the brawler who have the ball or the cell where is the ball to null.
	/// PARAMETERS : None.
	/// RETURN : Void.
	private void ResetOwner()
	{
		if (_brawler_with_the_ball != null)
		{
			_brawler_with_the_ball._b_have_the_ball = false;
			_brawler_with_the_ball = null;
		}
		if (_cell_with_the_ball != null)
		{
			_cell_with_the_ball._b_is_ball_on_this_cell = false;
			_cell_with_the_ball = null;
		}
	}
}
