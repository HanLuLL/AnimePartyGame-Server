using Cinemachine;
using Core.Unit;
using FairyGUI;
using UnityEngine;

namespace Core.Camera;

public class GMFreeCamera : Core.Unit.Unit
{
	[SerializeField]
	private float cameraSpeed = 60f;

	[SerializeField]
	private float cameraSensitivity = 0.08f;

	[SerializeField]
	private float cameraOffsetYSpeed = 60f;

	[SerializeField]
	private Vector2 cameraOffsetYRange = new Vector2(20f, 180f);

	[SerializeField]
	private float cameraRotateSpeed = 45f;

	private CinemachineVirtualCamera gmCamera;

	private CinemachineTransposer gmTransposer;

	private Transform followTarget;

	private Vector2 startPos;

	private Vector3 targetPos;

	private float cameraPitch;

	private float cameraYaw;

	public void Initialize(CinemachineVirtualCamera sourceCamera)
	{
		gmCamera = GetComponent<CinemachineVirtualCamera>();
		if (!(gmCamera == null) && !(sourceCamera == null))
		{
			Transform follow = sourceCamera.m_Follow;
			cameraPitch = NormalizeEulerAngle(base.transform.eulerAngles.x);
			cameraYaw = NormalizeEulerAngle(base.transform.eulerAngles.y);
			GameObject gameObject = new GameObject("GMFreeCameraFollowTarget");
			gameObject.transform.position = ((follow != null) ? follow.position : base.transform.position);
			gameObject.transform.rotation = Quaternion.Euler(0f, cameraYaw, 0f);
			followTarget = gameObject.transform;
			gmCamera.m_Follow = followTarget;
			gmCamera.m_LookAt = null;
			gmCamera.m_Priority = 1000;
			gmCamera.transform.rotation = Quaternion.Euler(cameraPitch, cameraYaw, 0f);
			gmTransposer = gmCamera.GetCinemachineComponent<CinemachineTransposer>();
		}
	}

	protected override void OnDestroy()
	{
		if (followTarget != null)
		{
			Object.Destroy(followTarget.gameObject);
		}
	}

	private void Update()
	{
		if (!(gmCamera == null) && !(followTarget == null))
		{
			HandleMoveCamera();
			HandleDragCamera();
			HandleOffsetY();
			HandleRotateCamera();
		}
	}

	private void HandleMoveCamera()
	{
		float axis = Input.GetAxis("Horizontal");
		float axis2 = Input.GetAxis("Vertical");
		if (axis != 0f || axis2 != 0f)
		{
			Vector3 vector = followTarget.forward * axis2 + followTarget.right * axis;
			targetPos += vector * (cameraSpeed * Time.deltaTime);
		}
	}

	private void HandleDragCamera()
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		if ((Stage.isTouchOnUI && GRoot.inst.touchTarget.touchable) || (Input.touchCount <= 0 && !Input.GetMouseButton(0)))
		{
			startPos = Stage.inst.touchPosition;
			MoveTarget();
			return;
		}
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			if ((int)((Touch)(ref touch)).phase == 0)
			{
				startPos = Stage.inst.touchPosition;
			}
		}
		else if (Input.GetMouseButtonDown(0))
		{
			startPos = Stage.inst.touchPosition;
		}
		Vector2 vector = Stage.inst.touchPosition - startPos;
		Vector3 vector2 = followTarget.forward * vector.y - followTarget.right * vector.x;
		targetPos += vector2 * cameraSensitivity;
		startPos = Stage.inst.touchPosition;
		MoveTarget();
	}

	private void MoveTarget()
	{
		if (!(targetPos == Vector3.zero))
		{
			targetPos.y = 0f;
			followTarget.position += targetPos;
			targetPos = Vector3.zero;
		}
	}

	private void HandleOffsetY()
	{
		float num = 0f - Input.mouseScrollDelta.y;
		if (!(Mathf.Abs(num) < 0.01f) && !(gmTransposer == null))
		{
			Vector3 followOffset = gmTransposer.m_FollowOffset;
			followOffset.y = Mathf.Clamp(followOffset.y + num * cameraOffsetYSpeed * Time.deltaTime, cameraOffsetYRange.x, cameraOffsetYRange.y);
			gmTransposer.m_FollowOffset = followOffset;
		}
	}

	private void HandleRotateCamera()
	{
		bool flag = false;
		if (Input.GetKey(KeyCode.Q))
		{
			cameraPitch += cameraRotateSpeed * Time.deltaTime;
			flag = true;
		}
		if (Input.GetKey(KeyCode.E))
		{
			cameraPitch -= cameraRotateSpeed * Time.deltaTime;
			flag = true;
		}
		if (Input.GetKey(KeyCode.Z))
		{
			cameraYaw -= cameraRotateSpeed * Time.deltaTime;
			followTarget.rotation = Quaternion.Euler(0f, cameraYaw, 0f);
			flag = true;
		}
		if (Input.GetKey(KeyCode.C))
		{
			cameraYaw += cameraRotateSpeed * Time.deltaTime;
			followTarget.rotation = Quaternion.Euler(0f, cameraYaw, 0f);
			flag = true;
		}
		if (flag)
		{
			gmCamera.transform.rotation = Quaternion.Euler(cameraPitch, cameraYaw, 0f);
		}
	}

	private float NormalizeEulerAngle(float angle)
	{
		if (!(angle > 180f))
		{
			return angle;
		}
		return angle - 360f;
	}
}
