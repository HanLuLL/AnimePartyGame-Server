using System;
using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

public class GoWrapper : DisplayObject
{
	protected struct RendererInfo
	{
		public Renderer renderer;

		public Material[] materials;

		public int sortingOrder;
	}

	[Obsolete("No need to manually set this flag anymore, coz it will be handled automatically.")]
	public bool supportStencil;

	protected GameObject _wrapTarget;

	protected List<RendererInfo> _renderers;

	protected Dictionary<Material, Material> _materialsBackup;

	protected Canvas _canvas;

	protected bool _cloneMaterial;

	protected bool _shouldCloneMaterial;

	protected static List<Transform> helperTransformList = new List<Transform>();

	private List<Material> helperMaterials = new List<Material>();

	public GameObject wrapTarget
	{
		get
		{
			return _wrapTarget;
		}
		set
		{
			SetWrapTarget(value, cloneMaterial: false);
		}
	}

	public override int renderingOrder
	{
		get
		{
			return base.renderingOrder;
		}
		set
		{
			base.renderingOrder = value;
			if ((UnityEngine.Object)(object)_canvas != null)
			{
				_canvas.sortingOrder = value;
				return;
			}
			int count = _renderers.Count;
			for (int i = 0; i < count; i++)
			{
				RendererInfo rendererInfo = _renderers[i];
				if (rendererInfo.renderer != null)
				{
					if (i != 0 && _renderers[i].sortingOrder != _renderers[i - 1].sortingOrder)
					{
						value = UpdateContext.current.renderingOrder++;
					}
					rendererInfo.renderer.sortingOrder = value;
				}
			}
		}
	}

	public event Action<UpdateContext> onUpdate;

	public GoWrapper()
	{
		_renderers = new List<RendererInfo>();
		_materialsBackup = new Dictionary<Material, Material>();
		CreateGameObject("GoWrapper");
	}

	public GoWrapper(GameObject go)
		: this()
	{
		SetWrapTarget(go, cloneMaterial: false);
	}

	[Obsolete("setWrapTarget is deprecated. Use SetWrapTarget instead.")]
	public void setWrapTarget(GameObject target, bool cloneMaterial)
	{
		SetWrapTarget(target, cloneMaterial);
	}

	public void SetWrapTarget(GameObject target, bool cloneMaterial)
	{
		if (target == null)
		{
			_flags &= ~Flags.SkipBatching;
		}
		else
		{
			_flags |= Flags.SkipBatching;
		}
		InvalidateBatchingState();
		RecoverMaterials();
		_cloneMaterial = cloneMaterial;
		if (_wrapTarget != null)
		{
			_wrapTarget.transform.SetParent(null, worldPositionStays: false);
		}
		_canvas = null;
		_wrapTarget = target;
		_shouldCloneMaterial = false;
		_renderers.Clear();
		if (_wrapTarget != null)
		{
			_wrapTarget.transform.SetParent(base.cachedTransform, worldPositionStays: false);
			_canvas = _wrapTarget.GetComponent<Canvas>();
			if ((UnityEngine.Object)(object)_canvas != null)
			{
				_canvas.renderMode = (RenderMode)2;
				_canvas.worldCamera = StageCamera.main;
				_canvas.overrideSorting = true;
				RectTransform component = ((Component)(object)_canvas).GetComponent<RectTransform>();
				component.pivot = new Vector2(0f, 1f);
				component.position = new Vector3(0f, 0f, 0f);
				SetSize(component.rect.width, component.rect.height);
			}
			else
			{
				CacheRenderers();
				SetSize(0f, 0f);
			}
			SetGoLayers(base.layer);
		}
	}

	public void CacheRenderers()
	{
		if ((UnityEngine.Object)(object)_canvas != null)
		{
			return;
		}
		RecoverMaterials();
		_renderers.Clear();
		Renderer[] componentsInChildren = _wrapTarget.GetComponentsInChildren<Renderer>(includeInactive: true);
		int num = componentsInChildren.Length;
		_renderers.Capacity = num;
		for (int i = 0; i < num; i++)
		{
			Renderer renderer = componentsInChildren[i];
			Material[] sharedMaterials = renderer.sharedMaterials;
			RendererInfo item = new RendererInfo
			{
				renderer = renderer,
				materials = sharedMaterials,
				sortingOrder = renderer.sortingOrder
			};
			_renderers.Add(item);
			if (_cloneMaterial || sharedMaterials == null || (!(renderer is SkinnedMeshRenderer) && !(renderer is MeshRenderer)))
			{
				continue;
			}
			int num2 = sharedMaterials.Length;
			for (int j = 0; j < num2; j++)
			{
				Material material = sharedMaterials[j];
				if (material != null && material.renderQueue != 3000)
				{
					material.renderQueue = 3000;
				}
			}
		}
		_renderers.Sort((RendererInfo c1, RendererInfo c2) => c1.sortingOrder - c2.sortingOrder);
		_shouldCloneMaterial = _cloneMaterial;
	}

	private void CloneMaterials()
	{
		_shouldCloneMaterial = false;
		int count = _renderers.Count;
		for (int i = 0; i < count; i++)
		{
			RendererInfo rendererInfo = _renderers[i];
			Material[] materials = rendererInfo.materials;
			if (materials == null)
			{
				continue;
			}
			bool flag = rendererInfo.renderer is SkinnedMeshRenderer || rendererInfo.renderer is MeshRenderer;
			int num = materials.Length;
			for (int j = 0; j < num; j++)
			{
				Material material = materials[j];
				if (!(material == null))
				{
					if (!_materialsBackup.TryGetValue(material, out var value))
					{
						value = new Material(material);
						_materialsBackup[material] = value;
					}
					materials[j] = value;
					if (flag && material.renderQueue != 3000)
					{
						value.renderQueue = 3000;
					}
				}
			}
			if (rendererInfo.renderer != null)
			{
				rendererInfo.renderer.sharedMaterials = materials;
			}
		}
	}

	private void RecoverMaterials()
	{
		if (_materialsBackup.Count == 0)
		{
			return;
		}
		int count = _renderers.Count;
		for (int i = 0; i < count; i++)
		{
			RendererInfo rendererInfo = _renderers[i];
			if (rendererInfo.renderer == null)
			{
				continue;
			}
			Material[] materials = rendererInfo.materials;
			if (materials == null)
			{
				continue;
			}
			int num = materials.Length;
			for (int j = 0; j < num; j++)
			{
				Material material = materials[j];
				foreach (KeyValuePair<Material, Material> item in _materialsBackup)
				{
					if (item.Value == material)
					{
						materials[j] = item.Key;
					}
				}
			}
			rendererInfo.renderer.sharedMaterials = materials;
		}
		foreach (KeyValuePair<Material, Material> item2 in _materialsBackup)
		{
			UnityEngine.Object.DestroyImmediate(item2.Value);
		}
		_materialsBackup.Clear();
	}

	protected override bool SetLayer(int value, bool fromParent)
	{
		if (base.SetLayer(value, fromParent))
		{
			SetGoLayers(value);
			return true;
		}
		return false;
	}

	protected void SetGoLayers(int layer)
	{
		if (!(_wrapTarget == null))
		{
			_wrapTarget.GetComponentsInChildren(includeInactive: true, helperTransformList);
			int count = helperTransformList.Count;
			for (int i = 0; i < count; i++)
			{
				helperTransformList[i].gameObject.layer = layer;
			}
			helperTransformList.Clear();
		}
	}

	public override void Update(UpdateContext context)
	{
		if (this.onUpdate != null)
		{
			this.onUpdate(context);
		}
		if (_shouldCloneMaterial)
		{
			CloneMaterials();
		}
		ApplyClipping(context);
		base.Update(context);
	}

	protected virtual void ApplyClipping(UpdateContext context)
	{
		int count = _renderers.Count;
		for (int i = 0; i < count; i++)
		{
			Renderer renderer = _renderers[i].renderer;
			if (renderer == null)
			{
				continue;
			}
			renderer.GetSharedMaterials(helperMaterials);
			int count2 = helperMaterials.Count;
			for (int j = 0; j < count2; j++)
			{
				Material material = helperMaterials[j];
				if (material != null)
				{
					context.ApplyClippingProperties(material, isStdMaterial: false);
				}
			}
			helperMaterials.Clear();
		}
	}

	public override void Dispose()
	{
		if ((_flags & Flags.Disposed) != 0)
		{
			return;
		}
		if (_wrapTarget != null)
		{
			UnityEngine.Object.Destroy(_wrapTarget);
			_wrapTarget = null;
			if (_materialsBackup.Count > 0)
			{
				foreach (KeyValuePair<Material, Material> item in _materialsBackup)
				{
					UnityEngine.Object.DestroyImmediate(item.Value);
				}
			}
		}
		_renderers = null;
		_materialsBackup = null;
		_canvas = null;
		base.Dispose();
	}
}
