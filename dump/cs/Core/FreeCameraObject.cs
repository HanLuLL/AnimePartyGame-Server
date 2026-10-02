using System;
using Cinemachine;
using Core.Camera;
using Core.Scene;
using Core.Unit;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;
using UnityTimer;

namespace Core;

public class FreeCameraObject : Core.Unit.Unit
{
	private CinemachineVirtualCamera freeCamera;

	[SerializeField]
	private float CameraSpeed = 60f;

	[SerializeField]
	private float CameraSensitivity = 0.08f;

	private Vector2 startPos;

	private Vector3 targetPos;

	public FreeCameraStatus status;

	private CharacterController _characterController;

	[SerializeField]
	private float freeDuration = 15f;

	private Timer freeTimer;

	private float Y => (SimpleSingletonProvider<GameLogicManager>.inst?.battle)?.mapCharacterOffsetHeight ?? 0f;

	private void Start()
	{
		freeCamera = BattleSceneController.inst.freeCamera;
		_characterController = GetComponent<CharacterController>();
	}

	public void InitPosition(Transform firstBornLand)
	{
		base.transform.position = new Vector3(firstBornLand.position.x, Y, firstBornLand.position.z);
		freeCamera = BattleSceneController.inst.freeCamera;
		freeCamera.m_Priority = 5;
		freeCamera.gameObject.SetActiveEx(active: true);
	}

	public bool CloseFreeCamera(CharacterCamera _camera)
	{
		if (_camera != null && !IsOutOfControlStatus())
		{
			freeCamera.m_Priority = 0;
			freeCamera.gameObject.SetActiveEx(active: false);
			Vector3 position = _camera.owner.standLand.transform.position;
			base.transform.position = new Vector3(position.x, Y, position.z);
			return true;
		}
		return false;
	}

	public void ActiveCamera(CharacterCamera _camera)
	{
		if (_camera != null && !IsOutOfControlStatus())
		{
			Vector3 position = _camera.owner.standLand.transform.position;
			ActiveCamera(position);
		}
	}

	public void ActiveCamera(Vector3 pos)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		Vector3 vector = pos - base.transform.position;
		vector.y = 0f;
		_characterController.Move(vector);
		freeCamera.m_Priority = 5;
		freeCamera.gameObject.SetActiveEx(active: true);
	}

	protected void Update()
	{
		HandleControlCamera();
		HandleMoveCamera();
		HandleCameraClick();
	}

	private void HandleMoveCamera()
	{
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		FreeCameraStatus freeCameraStatus = status;
		if (freeCameraStatus == FreeCameraStatus.System || freeCameraStatus == FreeCameraStatus.MapSignal || (BattleSceneController.inst == null && BattleSceneController.inst.cinemachineBrain == null) || BattleSceneController.inst.cinemachineBrain.IsBlending)
		{
			return;
		}
		if (GameSettings.KeyControl)
		{
			float axis = Input.GetAxis("Horizontal");
			float axis2 = Input.GetAxis("Vertical");
			if (axis != 0f || axis2 != 0f)
			{
				Vector3 vector = base.transform.forward * axis2 + base.transform.right * axis;
				targetPos = vector * (CameraSpeed * Time.deltaTime);
				UpdateFreeDuration();
			}
		}
		if (freeCamera.m_Priority == 0 && targetPos != Vector3.zero)
		{
			ActiveCameraForCurPlayer();
		}
		if (targetPos.magnitude > 0f && (GameSettings.KeyControl || GameSettings.MouseControl))
		{
			targetPos.y = 0f;
			_characterController.Move(targetPos);
		}
		targetPos = Vector3.zero;
	}

	private void HandleCameraClick()
	{
		UnitLand unitLand = null;
		if (Input.GetMouseButtonDown(0))
		{
			if ((object)unitLand == null)
			{
				unitLand = RaycastLand();
			}
			SimpleSingletonProvider<GameLogicManager>.inst.land.signal.chooseLand.Dispatch(unitLand);
		}
	}

	private UnitLand RaycastLand()
	{
		if (Stage.isTouchOnUI && !TouchOnAttrUI())
		{
			return null;
		}
		Ray ray = BattleSceneController.inst.mainCamera.ScreenPointToRay(Input.mousePosition);
		int num = 1 << LayerMask.NameToLayer("MapLand");
		RaycastHit val = default(RaycastHit);
		Physics.Raycast(ray, ref val, 999f, num);
		if ((UnityEngine.Object)(object)((RaycastHit)(ref val)).collider != null)
		{
			Transform parent = ((Component)(object)((RaycastHit)(ref val)).collider).transform.parent;
			if (parent != null)
			{
				UnitLand component = parent.GetComponent<UnitLand>();
				if (component != null && !component.Disable)
				{
					return component;
				}
			}
		}
		return null;
	}

	private bool TouchOnAttrUI()
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.battle?.battleInfo.AttrContainerIsAncestorOf(Stage.inst.touchTarget) ?? false;
	}

	private void HandleControlCamera()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		if (!GameSettings.MouseControl)
		{
			return;
		}
		if ((!Stage.isTouchOnUI || !GRoot.inst.touchTarget.touchable) && status != FreeCameraStatus.System && (Input.touchCount > 0 || Input.GetMouseButton(0)))
		{
			if (Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);
				if ((int)((Touch)(ref touch)).phase == 0)
				{
					startPos = Stage.inst.touchPosition;
				}
			}
			Vector2 vector = Stage.inst.touchPosition - startPos;
			Vector3 vector2 = base.transform.forward * vector.y - base.transform.right * vector.x;
			targetPos = vector2 * CameraSensitivity;
			if (targetPos.magnitude > 0.1f)
			{
				UpdateFreeDuration();
			}
		}
		startPos = Stage.inst.touchPosition;
	}

	private void ActiveCameraForCurPlayer()
	{
		CharacterCamera curPlayerCamera = SimpleSingletonProvider<CameraManager>.inst.GetCurPlayerCamera();
		SimpleSingletonProvider<CameraManager>.inst.ResetCameraPriority();
		if (curPlayerCamera != null)
		{
			ActiveCamera(curPlayerCamera.owner.transform.position);
		}
	}

	public void SetHostCamera(bool state)
	{
		if (!IsOutOfControlStatus())
		{
			UpdateStatus(state ? FreeCameraStatus.System : FreeCameraStatus.None);
		}
	}

	public bool GetHostStatus()
	{
		FreeCameraStatus freeCameraStatus = status;
		return freeCameraStatus == FreeCameraStatus.System || freeCameraStatus == FreeCameraStatus.MapSignal;
	}

	private bool IsOutOfControlStatus()
	{
		FreeCameraStatus freeCameraStatus = status;
		return freeCameraStatus == FreeCameraStatus.Player || freeCameraStatus == FreeCameraStatus.MapSignal;
	}

	public void UpdateStatus(FreeCameraStatus _status = FreeCameraStatus.None)
	{
		if (_status == FreeCameraStatus.MapSignal)
		{
			Timer obj = freeTimer;
			if (obj != null)
			{
				obj.Cancel();
			}
			ActiveCameraForCurPlayer();
		}
		if (_status != status && status == FreeCameraStatus.MapSignal)
		{
			status = _status;
			UpdateFreeDuration();
		}
		else
		{
			status = _status;
		}
	}

	private void UpdateFreeDuration()
	{
		if (GetHostStatus() || (!GameSettings.MouseControl && !GameSettings.KeyControl))
		{
			return;
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(SimpleSingletonProvider<GameLogicManager>.inst.battle.curPlayerId))
		{
			CancelFreeStatus();
			return;
		}
		FreeCameraStatus freeCameraStatus = status;
		if (freeCameraStatus == FreeCameraStatus.System || freeCameraStatus == FreeCameraStatus.MapSignal)
		{
			return;
		}
		UpdateStatus(FreeCameraStatus.Player);
		if (GameSettings.freeCamera)
		{
			return;
		}
		if (freeTimer == null || freeTimer.isDone)
		{
			freeTimer = Timer.Register(0f, freeDuration, (Action)delegate
			{
				status = FreeCameraStatus.None;
			}, (Action)null, (Action)delegate
			{
				status = FreeCameraStatus.None;
			}, (Action)null, (Action)null, (Action<float>)null, (Action)null, false, -1f, false, (GameObject)null);
		}
		else
		{
			freeTimer.ReStart(true);
		}
	}

	public void CancelFreeStatus()
	{
		Timer obj = freeTimer;
		if (obj != null)
		{
			obj.Cancel();
		}
		status = FreeCameraStatus.None;
	}
}
