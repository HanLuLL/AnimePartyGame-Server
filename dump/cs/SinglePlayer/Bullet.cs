using System;
using UnityEngine;

namespace SinglePlayer;

public class Bullet : MonoBehaviour
{
	public float initialSpeed = 100f;

	public float acceleration = 10f;

	public float initialTurnSpeed;

	public float turnAcceleration = 1f;

	public float hitDistance = 1f;

	public float maxSpeed = 999f;

	private float _currentSpeed;

	private float _currentTurnSpeed;

	private Vector3 _targetPosition;

	private Action<Vector3> _onHitAction;

	public void Init(Vector3 originPosition, Vector3 originDirection, Vector3 targetPosition, float distance, Action<Vector3> onHitAction)
	{
		_currentSpeed = initialSpeed;
		_currentTurnSpeed = initialTurnSpeed;
		base.transform.position = originPosition;
		base.transform.forward = originDirection;
		_targetPosition = targetPosition;
		hitDistance = distance;
		_onHitAction = onHitAction;
	}

	private void Update()
	{
		_currentSpeed += acceleration * Time.deltaTime;
		if (_currentSpeed >= maxSpeed)
		{
			_currentSpeed = maxSpeed;
		}
		if (_currentSpeed < 0f)
		{
			_currentSpeed = 0f;
		}
		base.transform.position += base.transform.forward * (_currentSpeed * Time.deltaTime);
		_currentTurnSpeed += turnAcceleration * Time.deltaTime;
		Quaternion to = Quaternion.LookRotation(_targetPosition - base.transform.position);
		base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, to, _currentTurnSpeed * Time.deltaTime);
		if (Vector3.Distance(base.transform.position, _targetPosition) <= hitDistance)
		{
			_onHitAction?.Invoke(base.transform.position);
		}
	}
}
