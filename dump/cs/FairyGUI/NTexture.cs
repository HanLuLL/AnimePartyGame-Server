using System;
using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

public class NTexture
{
	public Rect uvRect;

	public bool rotated;

	public int refCount;

	public float lastActive;

	public DestroyMethod destroyMethod;

	private Texture _nativeTexture;

	private Texture _alphaTexture;

	private Rect _region;

	private Vector2 _offset;

	private Vector2 _originalSize;

	private NTexture _root;

	private Dictionary<string, MaterialManager> _materialManagers;

	private static NTexture _empty;

	public static NTexture Empty
	{
		get
		{
			if (_empty == null)
			{
				_empty = new NTexture(CreateEmptyTexture());
			}
			return _empty;
		}
	}

	public int width => (int)_region.width;

	public int height => (int)_region.height;

	public Vector2 offset
	{
		get
		{
			return _offset;
		}
		set
		{
			_offset = value;
		}
	}

	public Vector2 originalSize
	{
		get
		{
			return _originalSize;
		}
		set
		{
			_originalSize = value;
		}
	}

	public NTexture root => _root;

	public bool disposed => _root == null;

	public Texture nativeTexture
	{
		get
		{
			if (_root == null)
			{
				return null;
			}
			return _root._nativeTexture;
		}
	}

	public Texture alphaTexture
	{
		get
		{
			if (_root == null)
			{
				return null;
			}
			return _root._alphaTexture;
		}
	}

	public static event Action<Texture> CustomDestroyMethod;

	public event Action<NTexture> onSizeChanged;

	public event Action<NTexture> onRelease;

	internal static Texture2D CreateEmptyTexture()
	{
		Texture2D texture2D = new Texture2D(1, 1, TextureFormat.RGB24, mipChain: false);
		texture2D.name = "White Texture";
		texture2D.hideFlags = DisplayObject.hideFlags;
		texture2D.SetPixel(0, 0, Color.white);
		texture2D.Apply();
		return texture2D;
	}

	public static void DisposeEmpty()
	{
		if (_empty != null)
		{
			NTexture empty = _empty;
			_empty = null;
			empty.Dispose();
		}
	}

	public NTexture(Texture texture)
		: this(texture, null, 1f, 1f)
	{
	}

	public NTexture(Texture texture, Texture alphaTexture, float xScale, float yScale)
	{
		_root = this;
		_nativeTexture = texture;
		_alphaTexture = alphaTexture;
		uvRect = new Rect(0f, 0f, xScale, yScale);
		if (yScale < 0f)
		{
			uvRect.y = 0f - yScale;
			uvRect.yMax = 0f;
		}
		if (xScale < 0f)
		{
			uvRect.x = 0f - xScale;
			uvRect.xMax = 0f;
		}
		if (_nativeTexture != null)
		{
			_originalSize = new Vector2(_nativeTexture.width, _nativeTexture.height);
		}
		_region = new Rect(0f, 0f, _originalSize.x, _originalSize.y);
	}

	public NTexture(Texture texture, Rect region)
	{
		_root = this;
		_nativeTexture = texture;
		_region = region;
		_originalSize = new Vector2(_region.width, _region.height);
		if (_nativeTexture != null)
		{
			uvRect = new Rect(region.x / (float)_nativeTexture.width, 1f - region.yMax / (float)_nativeTexture.height, region.width / (float)_nativeTexture.width, region.height / (float)_nativeTexture.height);
		}
		else
		{
			uvRect.Set(0f, 0f, 1f, 1f);
		}
	}

	public NTexture(NTexture root, Rect region, bool rotated)
	{
		_root = root;
		this.rotated = rotated;
		region.x += root._region.x;
		region.y += root._region.y;
		uvRect = new Rect(region.x * root.uvRect.width / (float)root.width, 1f - region.yMax * root.uvRect.height / (float)root.height, region.width * root.uvRect.width / (float)root.width, region.height * root.uvRect.height / (float)root.height);
		if (rotated)
		{
			float num = region.width;
			region.width = region.height;
			region.height = num;
			num = uvRect.width;
			uvRect.width = uvRect.height;
			uvRect.height = num;
		}
		_region = region;
		_originalSize = _region.size;
	}

	public NTexture(NTexture root, Rect region, bool rotated, Vector2 originalSize, Vector2 offset)
		: this(root, region, rotated)
	{
		_originalSize = originalSize;
		_offset = offset;
	}

	public NTexture(Sprite sprite)
	{
		Rect textureRect = sprite.textureRect;
		textureRect.y = (float)sprite.texture.height - textureRect.yMax;
		_root = this;
		_nativeTexture = sprite.texture;
		_region = textureRect;
		_originalSize = new Vector2(_region.width, _region.height);
		uvRect = new Rect(_region.x / (float)_nativeTexture.width, 1f - _region.yMax / (float)_nativeTexture.height, _region.width / (float)_nativeTexture.width, _region.height / (float)_nativeTexture.height);
	}

	public Rect GetDrawRect(Rect drawRect)
	{
		if (_originalSize.x == _region.width && _originalSize.y == _region.height)
		{
			return drawRect;
		}
		float num = drawRect.width / _originalSize.x;
		float num2 = drawRect.height / _originalSize.y;
		return new Rect(_offset.x * num, _offset.y * num2, _region.width * num, _region.height * num2);
	}

	public void GetUV(Vector2[] uv)
	{
		uv[0] = uvRect.position;
		uv[1] = new Vector2(uvRect.xMin, uvRect.yMax);
		uv[2] = new Vector2(uvRect.xMax, uvRect.yMax);
		uv[3] = new Vector2(uvRect.xMax, uvRect.yMin);
		if (rotated)
		{
			float xMin = uvRect.xMin;
			float yMin = uvRect.yMin;
			float yMax = uvRect.yMax;
			for (int i = 0; i < 4; i++)
			{
				Vector2 vector = uv[i];
				float y = vector.y;
				vector.y = yMin + vector.x - xMin;
				vector.x = xMin + yMax - y;
				uv[i] = vector;
			}
		}
	}

	public MaterialManager GetMaterialManager(string shaderName)
	{
		if (_root != this)
		{
			if (_root == null)
			{
				return null;
			}
			return _root.GetMaterialManager(shaderName);
		}
		if (_materialManagers == null)
		{
			_materialManagers = new Dictionary<string, MaterialManager>();
		}
		if (!_materialManagers.TryGetValue(shaderName, out var value))
		{
			value = new MaterialManager(this, ShaderConfig.GetShader(shaderName));
			_materialManagers.Add(shaderName, value);
		}
		return value;
	}

	public void Unload()
	{
		Unload(destroyMaterials: false);
	}

	public void Unload(bool destroyMaterials)
	{
		if (this == _empty)
		{
			return;
		}
		if (_root != this)
		{
			throw new Exception("Unload is not allow to call on none root NTexture.");
		}
		if (_nativeTexture != null)
		{
			DestroyTexture();
			if (destroyMaterials)
			{
				DestroyMaterials();
			}
			else
			{
				RefreshMaterials();
			}
		}
	}

	public void Reload(Texture nativeTexture, Texture alphaTexture)
	{
		if (_root != this)
		{
			throw new Exception("Reload is not allow to call on none root NTexture.");
		}
		if (_nativeTexture != null && _nativeTexture != nativeTexture)
		{
			DestroyTexture();
		}
		_nativeTexture = nativeTexture;
		_alphaTexture = alphaTexture;
		Vector2 vector = _originalSize;
		if (_nativeTexture != null)
		{
			_originalSize = new Vector2(_nativeTexture.width, _nativeTexture.height);
		}
		else
		{
			_originalSize = Vector2.zero;
		}
		_region = new Rect(0f, 0f, _originalSize.x, _originalSize.y);
		RefreshMaterials();
		if (this.onSizeChanged != null && vector != _originalSize)
		{
			this.onSizeChanged(this);
		}
	}

	private void DestroyTexture()
	{
		switch (destroyMethod)
		{
		case DestroyMethod.Destroy:
			UnityEngine.Object.DestroyImmediate(_nativeTexture, allowDestroyingAssets: true);
			if (_alphaTexture != null)
			{
				UnityEngine.Object.DestroyImmediate(_alphaTexture, allowDestroyingAssets: true);
			}
			break;
		case DestroyMethod.Unload:
			Resources.UnloadAsset(_nativeTexture);
			if (_alphaTexture != null)
			{
				Resources.UnloadAsset(_alphaTexture);
			}
			break;
		case DestroyMethod.ReleaseTemp:
			RenderTexture.ReleaseTemporary((RenderTexture)_nativeTexture);
			if (_alphaTexture is RenderTexture)
			{
				RenderTexture.ReleaseTemporary((RenderTexture)_alphaTexture);
			}
			break;
		case DestroyMethod.Custom:
			if (NTexture.CustomDestroyMethod == null)
			{
				Debug.LogWarning("NTexture.CustomDestroyMethod must be set to handle DestroyMethod.Custom");
				break;
			}
			NTexture.CustomDestroyMethod(_nativeTexture);
			if (_alphaTexture != null)
			{
				NTexture.CustomDestroyMethod(_alphaTexture);
			}
			break;
		}
		_nativeTexture = null;
		_alphaTexture = null;
	}

	private void RefreshMaterials()
	{
		if (_materialManagers != null && _materialManagers.Count > 0)
		{
			Dictionary<string, MaterialManager>.Enumerator enumerator = _materialManagers.GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.Value.RefreshMaterials();
			}
			enumerator.Dispose();
		}
	}

	private void DestroyMaterials()
	{
		if (_materialManagers != null && _materialManagers.Count > 0)
		{
			Dictionary<string, MaterialManager>.Enumerator enumerator = _materialManagers.GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.Value.DestroyMaterials();
			}
			enumerator.Dispose();
		}
	}

	public void AddRef()
	{
		if (_root != null)
		{
			if (_root != this && refCount == 0)
			{
				_root.AddRef();
			}
			refCount++;
		}
	}

	public void ReleaseRef()
	{
		if (_root == null)
		{
			return;
		}
		refCount--;
		if (refCount == 0)
		{
			if (_root != this)
			{
				_root.ReleaseRef();
			}
			if (this.onRelease != null)
			{
				this.onRelease(this);
			}
		}
	}

	public void Dispose()
	{
		if (this != _empty)
		{
			if (_root == this)
			{
				Unload(destroyMaterials: true);
			}
			_root = null;
			this.onSizeChanged = null;
			this.onRelease = null;
		}
	}
}
