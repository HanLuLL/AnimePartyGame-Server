using System;
using UnityEngine;

namespace FairyGUI;

public class CaptureCamera : MonoBehaviour
{
	[NonSerialized]
	public Transform cachedTransform;

	[NonSerialized]
	public Camera cachedCamera;

	[NonSerialized]
	private static CaptureCamera _main;

	[NonSerialized]
	private static int _layer = -1;

	private static int _hiddenLayer = -1;

	public const string Name = "Capture Camera";

	public const string LayerName = "VUI";

	public const string HiddenLayerName = "Hidden VUI";

	public static int layer
	{
		get
		{
			if (_layer == -1)
			{
				_layer = LayerMask.NameToLayer("VUI");
				if (_layer == -1)
				{
					_layer = 30;
					Debug.LogWarning("Please define two layers named 'VUI' and 'Hidden VUI'");
				}
			}
			return _layer;
		}
	}

	public static int hiddenLayer
	{
		get
		{
			if (_hiddenLayer == -1)
			{
				_hiddenLayer = LayerMask.NameToLayer("Hidden VUI");
				if (_hiddenLayer == -1)
				{
					Debug.LogWarning("Please define two layers named 'VUI' and 'Hidden VUI'");
					_hiddenLayer = 31;
				}
			}
			return _hiddenLayer;
		}
	}

	private void OnEnable()
	{
		cachedCamera = GetComponent<Camera>();
		cachedTransform = base.gameObject.transform;
		if (base.gameObject.name == "Capture Camera")
		{
			_main = this;
		}
	}

	public static void CheckMain()
	{
		if (!(_main != null) || !(_main.cachedCamera != null))
		{
			GameObject gameObject = GameObject.Find("Capture Camera");
			if (gameObject != null)
			{
				_main = gameObject.GetComponent<CaptureCamera>();
				return;
			}
			GameObject obj = new GameObject("Capture Camera");
			Camera camera = obj.AddComponent<Camera>();
			camera.depth = 0f;
			camera.cullingMask = 1 << layer;
			camera.clearFlags = CameraClearFlags.Color;
			camera.backgroundColor = Color.clear;
			camera.orthographic = true;
			camera.orthographicSize = 5f;
			camera.nearClipPlane = -30f;
			camera.farClipPlane = 30f;
			camera.enabled = false;
			camera.stereoTargetEye = StereoTargetEyeMask.None;
			camera.allowHDR = false;
			camera.allowMSAA = false;
			obj.AddComponent<CaptureCamera>();
		}
	}

	public static RenderTexture CreateRenderTexture(int width, int height, bool stencilSupport)
	{
		return new RenderTexture(width, height, stencilSupport ? 24 : 0, RenderTextureFormat.ARGB32)
		{
			antiAliasing = 1,
			filterMode = FilterMode.Bilinear,
			anisoLevel = 0,
			useMipMap = false,
			wrapMode = TextureWrapMode.Clamp,
			hideFlags = DisplayObject.hideFlags
		};
	}

	public static void Capture(DisplayObject target, RenderTexture texture, float contentHeight, Vector2 offset)
	{
		CheckMain();
		Matrix4x4 localToWorldMatrix = target.cachedTransform.localToWorldMatrix;
		float magnitude = new Vector4(localToWorldMatrix.m00, localToWorldMatrix.m10, localToWorldMatrix.m20, localToWorldMatrix.m30).magnitude;
		float magnitude2 = new Vector4(localToWorldMatrix.m01, localToWorldMatrix.m11, localToWorldMatrix.m21, localToWorldMatrix.m31).magnitude;
		Vector3 vector = default(Vector3);
		vector.x = localToWorldMatrix.m02;
		vector.y = localToWorldMatrix.m12;
		vector.z = localToWorldMatrix.m22;
		Vector3 upwards = default(Vector3);
		upwards.x = localToWorldMatrix.m01;
		upwards.y = localToWorldMatrix.m11;
		upwards.z = localToWorldMatrix.m21;
		float num = contentHeight * 0.5f;
		Camera camera = _main.cachedCamera;
		camera.targetTexture = texture;
		float num2 = (float)texture.width / (float)texture.height;
		camera.aspect = num2 * magnitude / magnitude2;
		camera.orthographicSize = num * magnitude2;
		_main.cachedTransform.localPosition = target.cachedTransform.TransformPoint(num * num2 - offset.x, 0f - num + offset.y, 0f);
		if (vector != Vector3.zero)
		{
			_main.cachedTransform.localRotation = Quaternion.LookRotation(vector, upwards);
		}
		int childrenLayer = 0;
		if (target.graphics != null)
		{
			childrenLayer = target.graphics.gameObject.layer;
			target.graphics.gameObject.layer = layer;
		}
		if (target is Container)
		{
			childrenLayer = ((((Container)target).numChildren > 0) ? ((Container)target).GetChildAt(0).layer : hiddenLayer);
			((Container)target).SetChildrenLayer(layer);
		}
		RenderTexture active = RenderTexture.active;
		RenderTexture.active = texture;
		GL.Clear(clearDepth: true, clearColor: true, Color.clear);
		camera.Render();
		RenderTexture.active = active;
		if (target.graphics != null)
		{
			target.graphics.gameObject.layer = childrenLayer;
		}
		if (target is Container)
		{
			((Container)target).SetChildrenLayer(childrenLayer);
		}
	}
}
