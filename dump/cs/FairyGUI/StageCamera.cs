using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace FairyGUI;

[ExecuteInEditMode]
[AddComponentMenu("FairyGUI/UI Camera")]
public class StageCamera : MonoBehaviour
{
	public bool constantSize = true;

	[NonSerialized]
	public float unitsPerPixel = 0.02f;

	[NonSerialized]
	public Transform cachedTransform;

	[NonSerialized]
	public Camera cachedCamera;

	[NonSerialized]
	private int screenWidth;

	[NonSerialized]
	private int screenHeight;

	[NonSerialized]
	private bool isMain;

	[NonSerialized]
	private Display _display;

	[NonSerialized]
	public static Camera main;

	[NonSerialized]
	public static int screenSizeVer = 1;

	private const string _Tag = "UICamera";

	private const string _Name = "UICamera";

	public const string LayerName = "UI";

	public static float DefaultCameraSize = 5f;

	public static float DefaultUnitsPerPixel = 0.02f;

	private void OnEnable()
	{
		cachedTransform = base.transform;
		cachedCamera = GetComponent<Camera>();
		cachedCamera.backgroundColor = Color.black;
		cachedCamera.allowHDR = true;
		cachedCamera.allowMSAA = true;
		if (base.gameObject.CompareTag("UICamera"))
		{
			main = cachedCamera;
			isMain = true;
		}
		if (Display.displays.Length > 1 && cachedCamera.targetDisplay != 0 && cachedCamera.targetDisplay < Display.displays.Length)
		{
			_display = Display.displays[cachedCamera.targetDisplay];
		}
		if (_display == null)
		{
			OnScreenSizeChanged(Screen.width, Screen.height);
		}
		else
		{
			OnScreenSizeChanged(_display.renderingWidth, _display.renderingHeight);
		}
	}

	private void Update()
	{
		if (_display == null)
		{
			if (screenWidth != Screen.width || screenHeight != Screen.height)
			{
				OnScreenSizeChanged(Screen.width, Screen.height);
			}
		}
		else if (screenWidth != _display.renderingWidth || screenHeight != _display.renderingHeight)
		{
			OnScreenSizeChanged(_display.renderingWidth, _display.renderingHeight);
		}
	}

	private void OnScreenSizeChanged(int newWidth, int newHeight)
	{
		if (newWidth == 0 || newHeight == 0)
		{
			return;
		}
		screenWidth = newWidth;
		screenHeight = newHeight;
		if (constantSize)
		{
			cachedCamera.orthographicSize = DefaultCameraSize;
			unitsPerPixel = cachedCamera.orthographicSize * 2f / (float)screenHeight;
		}
		else
		{
			unitsPerPixel = DefaultUnitsPerPixel;
			cachedCamera.orthographicSize = (float)screenHeight * 0.5f * unitsPerPixel;
		}
		cachedTransform.localPosition = new Vector3(cachedCamera.orthographicSize * (float)screenWidth / (float)screenHeight, 0f - cachedCamera.orthographicSize);
		if (!isMain)
		{
			return;
		}
		screenSizeVer++;
		if (Application.isPlaying)
		{
			Stage.inst.HandleScreenSizeChanged(screenWidth, screenHeight, unitsPerPixel);
			return;
		}
		UIContentScaler uIContentScaler = UnityEngine.Object.FindObjectOfType<UIContentScaler>();
		if (uIContentScaler != null)
		{
			uIContentScaler.ApplyChange();
		}
		else
		{
			UIContentScaler.scaleFactor = 1f;
		}
	}

	private void OnRenderObject()
	{
		if (isMain && !Application.isPlaying)
		{
			EMRenderSupport.Update();
		}
	}

	public void ApplyModifiedProperties()
	{
		screenWidth = 0;
	}

	public static void CheckMainCamera()
	{
		GameObject gameObject = GameObject.FindWithTag("UICamera");
		if (gameObject == null)
		{
			CreateCamera();
		}
		else
		{
			AttackToMainCamera(gameObject.GetComponent<Camera>());
		}
		HitTestContext.cachedMainCamera = Camera.main;
		UniversalAdditionalCameraData universalAdditionalCameraData = CameraExtensions.GetUniversalAdditionalCameraData(Camera.main);
		universalAdditionalCameraData.SetRenderer(1);
		if (Application.isMobilePlatform)
		{
			universalAdditionalCameraData.antialiasing = (AntialiasingMode)0;
		}
		foreach (Camera item in universalAdditionalCameraData.cameraStack)
		{
			int renderer = ((item.tag == "UICamera") ? 3 : 2);
			CameraExtensions.GetUniversalAdditionalCameraData(item).SetRenderer(renderer);
		}
	}

	public static void CheckCaptureCamera()
	{
		GameObject gameObject = GameObject.FindWithTag("UICamera");
		if (gameObject == null)
		{
			CreateCamera();
		}
		else
		{
			AttackToMainCamera(gameObject.GetComponent<Camera>());
		}
	}

	public static Camera CreateCamera()
	{
		GameObject gameObject = new GameObject("UICamera")
		{
			tag = "UICamera"
		};
		Camera camera = gameObject.AddComponent<Camera>();
		camera.depth = 1f;
		camera.cullingMask = 1 << LayerMask.NameToLayer("UI");
		camera.clearFlags = CameraClearFlags.Depth;
		camera.orthographic = true;
		camera.orthographicSize = DefaultCameraSize;
		camera.nearClipPlane = -5f;
		camera.farClipPlane = 5f;
		camera.stereoTargetEye = StereoTargetEyeMask.None;
		camera.allowHDR = false;
		camera.allowMSAA = false;
		gameObject.AddComponent<StageCamera>();
		SetCameraData(camera);
		AttackToMainCamera(camera);
		return camera;
	}

	private static void SetCameraData(Camera camera)
	{
		UniversalAdditionalCameraData universalAdditionalCameraData = CameraExtensions.GetUniversalAdditionalCameraData(camera);
		universalAdditionalCameraData.renderType = (CameraRenderType)1;
		universalAdditionalCameraData.renderShadows = false;
		universalAdditionalCameraData.renderPostProcessing = false;
		universalAdditionalCameraData.antialiasing = (AntialiasingMode)0;
		universalAdditionalCameraData.volumeLayerMask = 0;
		universalAdditionalCameraData.allowXRRendering = false;
		universalAdditionalCameraData.dithering = true;
		universalAdditionalCameraData.requiresDepthTexture = false;
		universalAdditionalCameraData.requiresColorTexture = false;
	}

	private static void AttackToMainCamera(Camera camera)
	{
		List<Camera> cameraStack = CameraExtensions.GetUniversalAdditionalCameraData(GameObject.Find("Main Camera").GetComponent<Camera>()).cameraStack;
		if (!cameraStack.Exists((Camera x) => x == camera))
		{
			cameraStack.Add(camera);
		}
	}
}
