using System;
using UnityEngine;

namespace FairyGUI;

[ExecuteInEditMode]
[AddComponentMenu("FairyGUI/UI Painter")]
[RequireComponent(typeof(MeshCollider), typeof(MeshRenderer))]
public class UIPainter : MonoBehaviour, EMRenderTarget
{
	public string packageName;

	public string componentName;

	public int sortingOrder;

	[SerializeField]
	private string packagePath;

	[SerializeField]
	private Camera renderCamera;

	[SerializeField]
	private bool fairyBatching;

	[SerializeField]
	private bool touchDisabled;

	private GComponent _ui;

	[NonSerialized]
	private bool _created;

	[NonSerialized]
	private bool _captured;

	[NonSerialized]
	private Renderer _renderer;

	[NonSerialized]
	private RenderTexture _texture;

	private Action _captureDelegate;

	public Container container { get; private set; }

	public GComponent ui
	{
		get
		{
			if (!_created && Application.isPlaying)
			{
				CreateUI();
			}
			return _ui;
		}
	}

	public int EM_sortingOrder => sortingOrder;

	private void OnEnable()
	{
		if (Application.isPlaying)
		{
			if (container == null)
			{
				CreateContainer();
				if (!string.IsNullOrEmpty(packagePath) && UIPackage.GetByName(packageName) == null)
				{
					UIPackage.AddPackage(packagePath);
				}
			}
		}
		else
		{
			EMRenderSupport.Add(this);
		}
	}

	private void OnDisable()
	{
		if (!Application.isPlaying)
		{
			EMRenderSupport.Remove(this);
		}
	}

	private void OnGUI()
	{
		if (!Application.isPlaying)
		{
			EM_BeforeUpdate();
		}
	}

	private void Start()
	{
		base.useGUILayout = false;
		if (!_created && Application.isPlaying)
		{
			CreateUI();
		}
	}

	private void OnDestroy()
	{
		if (Application.isPlaying)
		{
			if (_ui != null)
			{
				_ui.Dispose();
				_ui = null;
			}
			container.Dispose();
			container = null;
		}
		else
		{
			EMRenderSupport.Remove(this);
		}
		DestroyTexture();
	}

	private void CreateContainer()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		container = new Container("UIPainter");
		container.renderMode = (RenderMode)2;
		container.renderCamera = renderCamera;
		container.touchable = !touchDisabled;
		container.fairyBatching = fairyBatching;
		container._panelOrder = sortingOrder;
		container.hitArea = new MeshColliderHitTest(base.gameObject.GetComponent<MeshCollider>());
		SetSortingOrder(sortingOrder, apply: true);
		container.layer = CaptureCamera.hiddenLayer;
	}

	public void SetSortingOrder(int value, bool apply)
	{
		sortingOrder = value;
		container._panelOrder = value;
		if (apply)
		{
			Stage.inst.ApplyPanelOrder(container);
		}
	}

	public void CreateUI()
	{
		if (_ui != null)
		{
			_ui.Dispose();
			_ui = null;
			DestroyTexture();
		}
		_created = true;
		if (string.IsNullOrEmpty(packageName) || string.IsNullOrEmpty(componentName))
		{
			return;
		}
		_ui = (GComponent)UIPackage.CreateObject(packageName, componentName);
		if (_ui != null)
		{
			container.AddChild(_ui.displayObject);
			container.size = _ui.size;
			_texture = CaptureCamera.CreateRenderTexture(Mathf.RoundToInt(_ui.width), Mathf.RoundToInt(_ui.height), UIConfig.depthSupportForPaintingMode);
			_renderer = GetComponent<Renderer>();
			if (!(_renderer != null))
			{
				return;
			}
			_renderer.sharedMaterial.mainTexture = _texture;
			_captureDelegate = Capture;
			if (_renderer.sharedMaterial.renderQueue == 3000)
			{
				container.onUpdate += delegate
				{
					UpdateContext.OnEnd += _captureDelegate;
				};
			}
		}
		else
		{
			Debug.LogError("Create " + componentName + "@" + packageName + " failed!");
		}
	}

	private void Capture()
	{
		CaptureCamera.Capture(container, _texture, container.size.y, Vector2.zero);
		if (_renderer != null)
		{
			_renderer.sortingOrder = container.renderingOrder;
		}
	}

	private void DestroyTexture()
	{
		if (_texture != null)
		{
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(_texture);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(_texture);
			}
			_texture = null;
			if (_renderer != null)
			{
				_renderer.sharedMaterial.mainTexture = null;
			}
		}
	}

	private void CaptureInEditMode()
	{
		if (!EMRenderSupport.packageListReady || UIPackage.GetByName(packageName) == null)
		{
			return;
		}
		_captured = true;
		DisplayObject.hideFlags = HideFlags.DontSaveInEditor;
		GComponent gComponent = (GComponent)UIPackage.CreateObject(packageName, componentName);
		if (gComponent != null)
		{
			DestroyTexture();
			_texture = CaptureCamera.CreateRenderTexture(Mathf.RoundToInt(gComponent.width), Mathf.RoundToInt(gComponent.height), stencilSupport: false);
			Container container = (Container)gComponent.displayObject;
			container.layer = CaptureCamera.layer;
			container.gameObject.hideFlags = HideFlags.None;
			container.gameObject.SetActive(value: true);
			GameObject obj = new GameObject("Temp Capture Camera");
			Camera camera = obj.AddComponent<Camera>();
			camera.depth = 0f;
			camera.cullingMask = 1 << CaptureCamera.layer;
			camera.clearFlags = CameraClearFlags.Depth;
			camera.orthographic = true;
			camera.nearClipPlane = -30f;
			camera.farClipPlane = 30f;
			camera.enabled = false;
			camera.targetTexture = _texture;
			float num = (camera.orthographicSize = (float)_texture.height / 2f);
			obj.transform.localPosition = container.cachedTransform.TransformPoint(num * camera.aspect, 0f - num, 0f);
			UpdateContext updateContext = new UpdateContext();
			updateContext.Begin();
			gComponent.displayObject.Update(updateContext);
			updateContext.End();
			updateContext.Begin();
			gComponent.displayObject.Update(updateContext);
			updateContext.End();
			RenderTexture active = RenderTexture.active;
			RenderTexture.active = _texture;
			GL.Clear(clearDepth: true, clearColor: true, Color.clear);
			camera.Render();
			RenderTexture.active = active;
			camera.targetTexture = null;
			gComponent.Dispose();
			UnityEngine.Object.DestroyImmediate(obj);
			if (_renderer != null)
			{
				_renderer.sharedMaterial.mainTexture = _texture;
			}
		}
	}

	public void ApplyModifiedProperties(bool sortingOrderChanged)
	{
		if (sortingOrderChanged)
		{
			if (Application.isPlaying)
			{
				SetSortingOrder(sortingOrder, apply: true);
			}
			else
			{
				EMRenderSupport.orderChanged = true;
			}
		}
	}

	public void OnUpdateSource(object[] data)
	{
		if (!Application.isPlaying)
		{
			packageName = (string)data[0];
			packagePath = (string)data[1];
			componentName = (string)data[2];
			if ((bool)data[3])
			{
				_captured = false;
			}
		}
	}

	public void EM_BeforeUpdate()
	{
		if (_renderer == null)
		{
			_renderer = GetComponent<Renderer>();
		}
		if (_renderer != null && _renderer.sharedMaterial.mainTexture != _texture)
		{
			_renderer.sharedMaterial.mainTexture = _texture;
		}
		if (packageName != null && componentName != null && !_captured)
		{
			CaptureInEditMode();
		}
	}

	public void EM_Update(UpdateContext context)
	{
		if (_renderer != null)
		{
			_renderer.sortingOrder = context.renderingOrder++;
		}
	}

	public void EM_Reload()
	{
		_captured = false;
	}
}
