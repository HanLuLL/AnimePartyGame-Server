using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace FairyGUI;

public class NGraphics : IMeshFactory
{
	public class VertexMatrix
	{
		public Vector3 cameraPos;

		public Matrix4x4 matrix;
	}

	private class StencilEraser
	{
		public GameObject gameObject;

		public MeshFilter meshFilter;

		public MeshRenderer meshRenderer;

		public bool enabled
		{
			get
			{
				return meshRenderer.enabled;
			}
			set
			{
				meshRenderer.enabled = value;
			}
		}

		public StencilEraser(Transform parent)
		{
			gameObject = new GameObject("StencilEraser");
			gameObject.transform.SetParent(parent, worldPositionStays: false);
			meshFilter = gameObject.AddComponent<MeshFilter>();
			meshRenderer = gameObject.AddComponent<MeshRenderer>();
			meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
			meshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
			meshRenderer.receiveShadows = false;
			gameObject.layer = parent.gameObject.layer;
			gameObject.hideFlags = parent.gameObject.hideFlags;
			meshFilter.hideFlags = parent.gameObject.hideFlags;
			meshRenderer.hideFlags = parent.gameObject.hideFlags;
		}
	}

	public BlendMode blendMode;

	public bool dontClip;

	private NTexture _texture;

	private string _shader;

	private Material _material;

	private int _customMatarial;

	private MaterialManager _manager;

	private string[] _shaderKeywords;

	private int _materialFlags;

	private IMeshFactory _meshFactory;

	private float _alpha;

	private Color _color;

	private bool _meshDirty;

	private Rect _contentRect;

	private FlipType _flip;

	private VertexMatrix _vertexMatrix;

	private bool hasAlphaBackup;

	private List<byte> _alphaBackup;

	internal int _maskFlag;

	private StencilEraser _stencilEraser;

	private MaterialPropertyBlock _propertyBlock;

	private bool _blockUpdated;

	public GameObject gameObject { get; private set; }

	public MeshFilter meshFilter { get; private set; }

	public MeshRenderer meshRenderer { get; private set; }

	public Mesh mesh { get; private set; }

	public IMeshFactory meshFactory
	{
		get
		{
			return _meshFactory;
		}
		set
		{
			if (_meshFactory != value)
			{
				_meshFactory = value;
				_meshDirty = true;
			}
		}
	}

	public Rect contentRect
	{
		get
		{
			return _contentRect;
		}
		set
		{
			_contentRect = value;
			_meshDirty = true;
		}
	}

	public FlipType flip
	{
		get
		{
			return _flip;
		}
		set
		{
			if (_flip != value)
			{
				_flip = value;
				_meshDirty = true;
			}
		}
	}

	public NTexture texture
	{
		get
		{
			return _texture;
		}
		set
		{
			if (_texture != value)
			{
				value?.AddRef();
				if (_texture != null)
				{
					_texture.ReleaseRef();
				}
				_texture = value;
				if (_customMatarial != 0 && _material != null)
				{
					_material.mainTexture = ((_texture != null) ? _texture.nativeTexture : null);
				}
				_meshDirty = true;
				UpdateManager();
			}
		}
	}

	public string shader
	{
		get
		{
			return _shader;
		}
		set
		{
			_shader = value;
			UpdateManager();
		}
	}

	public Material material
	{
		get
		{
			if (_customMatarial == 0 && _material == null && _manager != null)
			{
				_material = _manager.GetMaterial(_materialFlags, blendMode, 0u);
			}
			return _material;
		}
		set
		{
			if ((_customMatarial & 0x80) != 0 && _material != null)
			{
				UnityEngine.Object.DestroyImmediate(_material);
			}
			_material = value;
			if (_material != null)
			{
				_customMatarial = 1;
				if (_material.HasProperty(ShaderConfig.ID_Stencil) || _material.HasProperty(ShaderConfig.ID_ClipBox))
				{
					_customMatarial |= 2;
				}
				meshRenderer.sharedMaterial = _material;
				if (_texture != null)
				{
					_material.mainTexture = _texture.nativeTexture;
				}
			}
			else
			{
				_customMatarial = 0;
				meshRenderer.sharedMaterial = null;
			}
		}
	}

	public string[] materialKeywords
	{
		get
		{
			return _shaderKeywords;
		}
		set
		{
			_shaderKeywords = value;
			UpdateMaterialFlags();
		}
	}

	public bool enabled
	{
		get
		{
			return meshRenderer.enabled;
		}
		set
		{
			meshRenderer.enabled = value;
		}
	}

	public int sortingOrder
	{
		get
		{
			return meshRenderer.sortingOrder;
		}
		set
		{
			meshRenderer.sortingOrder = value;
		}
	}

	public Color color
	{
		get
		{
			return _color;
		}
		set
		{
			_color = value;
		}
	}

	public VertexMatrix vertexMatrix
	{
		get
		{
			return _vertexMatrix;
		}
		set
		{
			_vertexMatrix = value;
			_meshDirty = true;
		}
	}

	public MaterialPropertyBlock materialPropertyBlock
	{
		get
		{
			if (_propertyBlock == null)
			{
				_propertyBlock = new MaterialPropertyBlock();
			}
			_blockUpdated = true;
			return _propertyBlock;
		}
	}

	public event Action meshModifier;

	public NGraphics(GameObject gameObject)
	{
		this.gameObject = gameObject;
		_alpha = 1f;
		_shader = ShaderConfig.imageShader;
		_color = Color.white;
		_meshFactory = this;
		meshFilter = gameObject.AddComponent<MeshFilter>();
		meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
		meshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
		meshRenderer.receiveShadows = false;
		mesh = new Mesh();
		mesh.name = gameObject.name;
		mesh.MarkDynamic();
		meshFilter.mesh = mesh;
		meshFilter.hideFlags = DisplayObject.hideFlags;
		meshRenderer.hideFlags = DisplayObject.hideFlags;
		mesh.hideFlags = DisplayObject.hideFlags;
		Stats.LatestGraphicsCreation++;
	}

	public T GetMeshFactory<T>() where T : IMeshFactory, new()
	{
		if (!(_meshFactory is T))
		{
			_meshFactory = new T();
			_meshDirty = true;
		}
		return (T)_meshFactory;
	}

	public void SetShaderAndTexture(string shader, NTexture texture)
	{
		_shader = shader;
		if (_texture != texture)
		{
			this.texture = texture;
		}
		else
		{
			UpdateManager();
		}
	}

	public void SetMaterial(Material material)
	{
		this.material = material;
		_customMatarial |= 128;
	}

	public void ToggleKeyword(string keyword, bool enabled)
	{
		if (enabled)
		{
			if (_shaderKeywords == null)
			{
				_shaderKeywords = new string[1] { keyword };
				UpdateMaterialFlags();
			}
			else if (Array.IndexOf(_shaderKeywords, keyword) == -1)
			{
				Array.Resize(ref _shaderKeywords, _shaderKeywords.Length + 1);
				_shaderKeywords[_shaderKeywords.Length - 1] = keyword;
				UpdateMaterialFlags();
			}
		}
		else if (_shaderKeywords != null)
		{
			int num = Array.IndexOf(_shaderKeywords, keyword);
			if (num != -1)
			{
				_shaderKeywords[num] = null;
				UpdateMaterialFlags();
			}
		}
	}

	private void UpdateManager()
	{
		if (_texture != null)
		{
			_manager = _texture.GetMaterialManager(_shader);
		}
		else
		{
			_manager = null;
		}
		UpdateMaterialFlags();
	}

	private void UpdateMaterialFlags()
	{
		if (_customMatarial != 0)
		{
			if (material != null)
			{
				material.shaderKeywords = _shaderKeywords;
			}
		}
		else if (_shaderKeywords != null && _manager != null)
		{
			_materialFlags = _manager.GetFlagsByKeywords(_shaderKeywords);
		}
		else
		{
			_materialFlags = 0;
		}
	}

	internal void _SetStencilEraserOrder(int value)
	{
		_stencilEraser.meshRenderer.sortingOrder = value;
	}

	public void Tint()
	{
		if (_meshDirty)
		{
			return;
		}
		int vertexCount = mesh.vertexCount;
		if (vertexCount != 0)
		{
			VertexBuffer vertexBuffer = VertexBuffer.Begin();
			mesh.GetColors(vertexBuffer.colors);
			List<Color32> colors = vertexBuffer.colors;
			for (int i = 0; i < vertexCount; i++)
			{
				Color32 value = _color;
				value.a = (byte)(_alpha * (float)(int)(hasAlphaBackup ? _alphaBackup[i] : byte.MaxValue));
				colors[i] = value;
			}
			mesh.SetColors(vertexBuffer.colors);
			vertexBuffer.End();
		}
	}

	private void ChangeAlpha(float value)
	{
		_alpha = value;
		int vertexCount = mesh.vertexCount;
		if (vertexCount != 0)
		{
			VertexBuffer vertexBuffer = VertexBuffer.Begin();
			mesh.GetColors(vertexBuffer.colors);
			List<Color32> colors = vertexBuffer.colors;
			for (int i = 0; i < vertexCount; i++)
			{
				Color32 value2 = colors[i];
				value2.a = (byte)(_alpha * (float)(int)(hasAlphaBackup ? _alphaBackup[i] : byte.MaxValue));
				colors[i] = value2;
			}
			mesh.SetColors(vertexBuffer.colors);
			vertexBuffer.End();
		}
	}

	public void SetMeshDirty()
	{
		_meshDirty = true;
	}

	public bool UpdateMesh()
	{
		if (_meshDirty)
		{
			UpdateMeshNow();
			return true;
		}
		return false;
	}

	public void Dispose()
	{
		if (mesh != null)
		{
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(mesh);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(mesh);
			}
			mesh = null;
		}
		if ((_customMatarial & 0x80) != 0 && _material != null)
		{
			UnityEngine.Object.DestroyImmediate(_material);
		}
		if (_texture != null)
		{
			_texture.ReleaseRef();
			_texture = null;
		}
		_manager = null;
		_material = null;
		meshRenderer = null;
		meshFilter = null;
		_stencilEraser = null;
		this.meshModifier = null;
	}

	public void Update(UpdateContext context, float alpha, bool grayed)
	{
		Stats.GraphicsCount++;
		if (_meshDirty)
		{
			_alpha = alpha;
			UpdateMeshNow();
		}
		else if (_alpha != alpha)
		{
			ChangeAlpha(alpha);
		}
		if (_propertyBlock != null && _blockUpdated)
		{
			meshRenderer.SetPropertyBlock(_propertyBlock);
			_blockUpdated = false;
		}
		if (_customMatarial != 0)
		{
			if ((_customMatarial & 2) != 0 && _material != null)
			{
				context.ApplyClippingProperties(_material, isStdMaterial: false);
				context.ApplyGrayProperties(_material);
			}
		}
		else
		{
			if (_manager != null)
			{
				if (_maskFlag == 1)
				{
					_material = _manager.GetMaterial(8 | _materialFlags, BlendMode.Normal, context.clipInfo.clipId);
					context.ApplyAlphaMaskProperties(_material, erasing: false);
				}
				else
				{
					int num = _materialFlags;
					if (grayed)
					{
						num |= 0x10;
					}
					if (context.clipped)
					{
						if (context.stencilReferenceValue > 0)
						{
							num |= 4;
						}
						if (context.rectMaskDepth > 0)
						{
							num = ((!context.clipInfo.soft) ? (num | 1) : (num | 2));
						}
						_material = _manager.GetMaterial(num, blendMode, context.clipInfo.clipId);
						if (_manager.firstMaterialInFrame)
						{
							context.ApplyClippingProperties(_material, isStdMaterial: true);
						}
					}
					else
					{
						_material = _manager.GetMaterial(num, blendMode, 0u);
					}
				}
			}
			else
			{
				_material = null;
			}
			if ((object)_material != meshRenderer.sharedMaterial)
			{
				meshRenderer.sharedMaterial = _material;
			}
		}
		if (_maskFlag == 0)
		{
			return;
		}
		if (_maskFlag == 1)
		{
			_maskFlag = 2;
			return;
		}
		if (_stencilEraser != null)
		{
			_stencilEraser.enabled = false;
		}
		_maskFlag = 0;
	}

	internal void _PreUpdateMask(UpdateContext context, uint maskId)
	{
		if (_maskFlag == 0)
		{
			if (_stencilEraser == null)
			{
				_stencilEraser = new StencilEraser(gameObject.transform);
				_stencilEraser.meshFilter.mesh = mesh;
			}
			else
			{
				_stencilEraser.enabled = true;
			}
		}
		_maskFlag = 1;
		if (_manager != null)
		{
			Material material = _manager.GetMaterial(8 | _materialFlags, BlendMode.Normal, maskId);
			if ((object)material != _stencilEraser.meshRenderer.sharedMaterial)
			{
				_stencilEraser.meshRenderer.sharedMaterial = material;
			}
			context.ApplyAlphaMaskProperties(material, erasing: true);
		}
	}

	private void UpdateMeshNow()
	{
		_meshDirty = false;
		if (_texture == null || _meshFactory == null)
		{
			if (mesh.vertexCount > 0)
			{
				mesh.Clear();
				if (this.meshModifier != null)
				{
					this.meshModifier();
				}
			}
			return;
		}
		VertexBuffer vertexBuffer = VertexBuffer.Begin();
		vertexBuffer.contentRect = _contentRect;
		vertexBuffer.uvRect = _texture.uvRect;
		if (_texture != null)
		{
			vertexBuffer.textureSize = new Vector2(_texture.width, _texture.height);
		}
		else
		{
			vertexBuffer.textureSize = new Vector2(0f, 0f);
		}
		if (_flip != FlipType.None)
		{
			if (_flip == FlipType.Horizontal || _flip == FlipType.Both)
			{
				float xMin = vertexBuffer.uvRect.xMin;
				vertexBuffer.uvRect.xMin = vertexBuffer.uvRect.xMax;
				vertexBuffer.uvRect.xMax = xMin;
			}
			if (_flip == FlipType.Vertical || _flip == FlipType.Both)
			{
				float yMin = vertexBuffer.uvRect.yMin;
				vertexBuffer.uvRect.yMin = vertexBuffer.uvRect.yMax;
				vertexBuffer.uvRect.yMax = yMin;
			}
		}
		vertexBuffer.vertexColor = _color;
		_meshFactory.OnPopulateMesh(vertexBuffer);
		int currentVertCount = vertexBuffer.currentVertCount;
		if (currentVertCount == 0)
		{
			if (mesh.vertexCount > 0)
			{
				mesh.Clear();
				if (this.meshModifier != null)
				{
					this.meshModifier();
				}
			}
			vertexBuffer.End();
			return;
		}
		if (_texture.rotated)
		{
			float xMin2 = _texture.uvRect.xMin;
			float yMin2 = _texture.uvRect.yMin;
			float yMax = _texture.uvRect.yMax;
			for (int i = 0; i < currentVertCount; i++)
			{
				Vector2 value = vertexBuffer.uvs[i];
				float y = value.y;
				value.y = yMin2 + value.x - xMin2;
				value.x = xMin2 + yMax - y;
				vertexBuffer.uvs[i] = value;
			}
		}
		hasAlphaBackup = vertexBuffer._alphaInVertexColor;
		if (hasAlphaBackup)
		{
			if (_alphaBackup == null)
			{
				_alphaBackup = new List<byte>();
			}
			else
			{
				_alphaBackup.Clear();
			}
			for (int j = 0; j < currentVertCount; j++)
			{
				Color32 value2 = vertexBuffer.colors[j];
				_alphaBackup.Add(value2.a);
				value2.a = (byte)((float)(int)value2.a * _alpha);
				vertexBuffer.colors[j] = value2;
			}
		}
		else if (_alpha != 1f)
		{
			for (int k = 0; k < currentVertCount; k++)
			{
				Color32 value3 = vertexBuffer.colors[k];
				value3.a = (byte)((float)(int)value3.a * _alpha);
				vertexBuffer.colors[k] = value3;
			}
		}
		if (_vertexMatrix != null)
		{
			Vector3 cameraPos = _vertexMatrix.cameraPos;
			Vector3 vector = new Vector3(cameraPos.x, cameraPos.y, 0f);
			vector -= _vertexMatrix.matrix.MultiplyPoint(vector);
			for (int l = 0; l < currentVertCount; l++)
			{
				Vector3 point = vertexBuffer.vertices[l];
				point = _vertexMatrix.matrix.MultiplyPoint(point);
				point += vector;
				Vector3 vector2 = point - cameraPos;
				float num = (0f - cameraPos.z) / vector2.z;
				point.x = cameraPos.x + num * vector2.x;
				point.y = cameraPos.y + num * vector2.y;
				point.z = 0f;
				vertexBuffer.vertices[l] = point;
			}
		}
		mesh.Clear();
		mesh.SetVertices(vertexBuffer.vertices);
		if (vertexBuffer._isArbitraryQuad)
		{
			mesh.SetUVs(0, vertexBuffer.FixUVForArbitraryQuad());
		}
		else
		{
			mesh.SetUVs(0, vertexBuffer.uvs);
		}
		mesh.SetColors(vertexBuffer.colors);
		mesh.SetTriangles(vertexBuffer.triangles, 0);
		if (vertexBuffer.uvs2.Count == vertexBuffer.uvs.Count)
		{
			mesh.SetUVs(1, vertexBuffer.uvs2);
		}
		vertexBuffer.End();
		if (this.meshModifier != null)
		{
			this.meshModifier();
		}
	}

	public void OnPopulateMesh(VertexBuffer vb)
	{
		Rect drawRect = texture.GetDrawRect(vb.contentRect);
		vb.AddQuad(drawRect, vb.vertexColor, vb.uvRect);
		vb.AddTriangles();
		vb._isArbitraryQuad = _vertexMatrix != null;
	}
}
