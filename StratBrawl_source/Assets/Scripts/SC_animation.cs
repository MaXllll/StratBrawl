using UnityEngine;
using System.Collections;

public class SC_animation : MonoBehaviour {

	private Transform _T_object;


	void Awake()
	{
		_T_object = transform;
	}

	public IEnumerator PlayAnimation(ActionType action_type, GridPosition position_target, float f_duration)
	{
		switch (action_type)
		{
		case ActionType.Move:
			yield return StartCoroutine(Interpolation(position_target.GetWorldPosition(), f_duration));
			break;
		case ActionType.Tackle:
			Vector3 V3_position_start = _T_object.position;
			yield return StartCoroutine(Interpolation(position_target.GetWorldPosition(), f_duration * 0.5f));
			yield return StartCoroutine(Interpolation(V3_position_start, f_duration * 0.5f));
			break;
		}
	}

	public IEnumerator Interpolation(Vector3 V3_position_target, float f_duration)
	{
		Vector3 V3_position_start = _T_object.position;
		for (float f_time = 0; f_time < f_duration; f_time += Time.deltaTime)
		{
			yield return null;
			_T_object.position = Vector3.Lerp(V3_position_start, V3_position_target, f_time / f_duration);
		}
		_T_object.position = V3_position_target;
	}
}
