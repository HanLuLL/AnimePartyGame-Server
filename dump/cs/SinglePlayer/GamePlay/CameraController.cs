using System;
using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using FairyGUI;
using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay;

public class CameraController : MonoBehaviour, IController, IInitialize
{
	[Serializable]
	private struct FovRange
	{
		public float minRange;

		public float maxRange;
	}

	private CharacterController _characterController;

	[SerializeField]
	private float CameraSpeed = 60f;

	[SerializeField]
	private float CameraSensitivity = 0.08f;

	[SerializeField]
	private float _mobileFovSensitivity = 0.3f;

	[SerializeField]
	private float _pcFovSensitivity = 100f;

	[SerializeField]
	private FovRange _fovRange = new FovRange
	{
		minRange = 15f,
		maxRange = 25f
	};

	[SerializeField]
	private float _defaultFov = 25f;

	[SerializeField]
	private CinemachineVirtualCamera _freeCamera;

	[SerializeField]
	private Transform _freeCameraTarget;

	[SerializeField]
	private CinemachineVirtualCamera _startCamera;

	private Vector3 _initialPosition;

	private Vector2 startPos;

	private Vector3 targetPos;

	private float _currentFov;

	private bool _enable;

	private void Start()
	{
		_initialPosition = _freeCameraTarget.transform.position;
		_characterController = _freeCameraTarget.GetComponent<CharacterController>();
		_enable = false;
	}

	private void Update()
	{
		if (_enable)
		{
			HandleControlCamera();
			HandleMoveCamera();
			HandleCameraFovPC();
			startPos = Stage.inst.touchPosition;
		}
	}

	public async UniTask Initialize()
	{
		await UniTask.CompletedTask;
		_currentFov = _defaultFov;
		_currentFov = Mathf.Clamp(_currentFov, _fovRange.minRange, _fovRange.maxRange);
		_freeCamera.m_Lens.FieldOfView = _currentFov;
	}

	public void ActiveCamera(Vector3 position)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		Vector3 vector = position - base.transform.position;
		vector.y = 0f;
		_characterController.Move(vector);
		_freeCamera.m_Priority = 5;
		_freeCamera.gameObject.SetActiveEx(active: true);
	}

	private void HandleControlCamera()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Invalid comparison between Unknown and I4
		if (Input.touchCount == 2)
		{
			Touch touch = Input.GetTouch(0);
			Touch touch2 = Input.GetTouch(1);
			Vector2 a = ((Touch)(ref touch)).position - ((Touch)(ref touch)).deltaPosition;
			Vector2 b = ((Touch)(ref touch2)).position - ((Touch)(ref touch2)).deltaPosition;
			float num = Vector2.Distance(a, b);
			float num2 = Vector2.Distance(((Touch)(ref touch)).position, ((Touch)(ref touch2)).position) - num;
			_currentFov -= num2 * _mobileFovSensitivity * Time.deltaTime;
			_currentFov = Mathf.Clamp(_currentFov, _fovRange.minRange, _fovRange.maxRange);
			_freeCamera.m_Lens.FieldOfView = _currentFov;
		}
		else if ((!Stage.isTouchOnUI || !GRoot.inst.touchTarget.touchable) && (Input.touchCount == 1 || Input.GetMouseButton(0)) && Input.touchCount == 1)
		{
			Touch touch3 = Input.GetTouch(0);
			if ((int)((Touch)(ref touch3)).phase == 1)
			{
				Vector3 vector = _freeCameraTarget.forward * (0f - ((Touch)(ref touch3)).deltaPosition.y) - _freeCameraTarget.right * ((Touch)(ref touch3)).deltaPosition.x;
				targetPos = vector * CameraSensitivity;
			}
		}
	}

	private void HandleMoveCamera()
	{
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		float axis = Input.GetAxis("Horizontal");
		float axis2 = Input.GetAxis("Vertical");
		if (axis != 0f || axis2 != 0f)
		{
			Vector3 vector = _freeCameraTarget.forward * axis2 + _freeCameraTarget.right * axis;
			targetPos = vector * (CameraSpeed * Time.deltaTime);
		}
		if (targetPos.magnitude > 0f)
		{
			targetPos.y = 0f;
			_characterController.Move(targetPos);
		}
		targetPos = Vector3.zero;
	}

	private void HandleCameraFovPC()
	{
		if (!(Input.mouseScrollDelta.sqrMagnitude < 0.01f))
		{
			_currentFov += (0f - Input.mouseScrollDelta.y) * _pcFovSensitivity * Time.deltaTime;
			_currentFov = Mathf.Clamp(_currentFov, _fovRange.minRange, _fovRange.maxRange);
			_freeCamera.m_Lens.FieldOfView = _currentFov;
		}
	}

	public async UniTask ResetFreeCameraPosition()
	{
		TweenAwaiter val = DOTweenAsyncExtensions.GetAwaiter((Tween)_freeCameraTarget.transform.DOMove(_initialPosition, 0.3f));
		if (!((TweenAwaiter)(ref val)).IsCompleted)
		{
			await val;
			TweenAwaiter val2 = default(TweenAwaiter);
			val = val2;
		}
		((TweenAwaiter)(ref val)).GetResult();
	}

	public void SetEnable(bool enable)
	{
		_enable = enable;
	}

	public void CloseStartCamera()
	{
		_startCamera.gameObject.SetActive(value: false);
		SetEnable(enable: true);
	}
}
