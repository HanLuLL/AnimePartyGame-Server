using System;
using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

public class MaterialManager
{
	private class MaterialRef
	{
		public Material material;

		public int frame;

		public BlendMode blendMode;

		public uint group;
	}

	public bool firstMaterialInFrame;

	private NTexture _texture;

	private Shader _shader;

	private List<string> _addKeywords;

	private Dictionary<int, List<MaterialRef>> _materials;

	private bool _combineTexture;

	private const int internalKeywordsCount = 6;

	private static string[] internalKeywords = new string[6] { "CLIPPED", "SOFT_CLIPPED", null, "ALPHA_MASK", "GRAYED", "COLOR_FILTER" };

	public event Action<Material> onCreateNewMaterial;

	internal MaterialManager(NTexture texture, Shader shader)
	{
		_texture = texture;
		_shader = shader;
		_materials = new Dictionary<int, List<MaterialRef>>();
		_combineTexture = texture.alphaTexture != null;
	}

	public int GetFlagsByKeywords(IList<string> keywords)
	{
		if (_addKeywords == null)
		{
			_addKeywords = new List<string>();
		}
		int num = 0;
		for (int i = 0; i < keywords.Count; i++)
		{
			string text = keywords[i];
			if (!string.IsNullOrEmpty(text))
			{
				int num2 = _addKeywords.IndexOf(text);
				if (num2 == -1)
				{
					num2 = _addKeywords.Count;
					_addKeywords.Add(text);
				}
				num += 1 << num2 + 6;
			}
		}
		return num;
	}

	public Material GetMaterial(int flags, BlendMode blendMode, uint group)
	{
		if (blendMode != BlendMode.Normal && BlendModeUtils.Factors[(int)blendMode].pma)
		{
			flags |= 0x20;
		}
		if (!_materials.TryGetValue(flags, out var value))
		{
			value = new List<MaterialRef>();
			_materials[flags] = value;
		}
		int frameCount = Time.frameCount;
		int count = value.Count;
		MaterialRef materialRef = null;
		for (int i = 0; i < count; i++)
		{
			MaterialRef materialRef2 = value[i];
			if (materialRef2.group == group && materialRef2.blendMode == blendMode)
			{
				if (materialRef2.frame != frameCount)
				{
					firstMaterialInFrame = true;
					materialRef2.frame = frameCount;
				}
				else
				{
					firstMaterialInFrame = false;
				}
				if (_combineTexture)
				{
					materialRef2.material.SetTexture(ShaderConfig.ID_AlphaTex, _texture.alphaTexture);
				}
				return materialRef2.material;
			}
			if (materialRef == null && (materialRef2.frame > frameCount || materialRef2.frame < frameCount - 1))
			{
				materialRef = materialRef2;
			}
		}
		if (materialRef == null)
		{
			materialRef = new MaterialRef
			{
				material = CreateMaterial(flags)
			};
			value.Add(materialRef);
		}
		else if (_combineTexture)
		{
			materialRef.material.SetTexture(ShaderConfig.ID_AlphaTex, _texture.alphaTexture);
		}
		if (materialRef.blendMode != blendMode)
		{
			BlendModeUtils.Apply(materialRef.material, blendMode);
			materialRef.blendMode = blendMode;
		}
		materialRef.group = group;
		materialRef.frame = frameCount;
		firstMaterialInFrame = true;
		return materialRef.material;
	}

	private Material CreateMaterial(int flags)
	{
		Material material = new Material(_shader);
		material.mainTexture = _texture.nativeTexture;
		if (_texture.alphaTexture != null)
		{
			material.EnableKeyword("COMBINED");
			material.SetTexture(ShaderConfig.ID_AlphaTex, _texture.alphaTexture);
		}
		for (int i = 0; i < 6; i++)
		{
			if ((flags & (1 << i)) != 0)
			{
				string text = internalKeywords[i];
				if (text != null)
				{
					material.EnableKeyword(text);
				}
			}
		}
		if (_addKeywords != null)
		{
			int count = _addKeywords.Count;
			for (int j = 0; j < count; j++)
			{
				if ((flags & (1 << j + 6)) != 0)
				{
					material.EnableKeyword(_addKeywords[j]);
				}
			}
		}
		material.hideFlags = DisplayObject.hideFlags;
		if (this.onCreateNewMaterial != null)
		{
			this.onCreateNewMaterial(material);
		}
		return material;
	}

	public void DestroyMaterials()
	{
		Dictionary<int, List<MaterialRef>>.Enumerator enumerator = _materials.GetEnumerator();
		while (enumerator.MoveNext())
		{
			List<MaterialRef> value = enumerator.Current.Value;
			if (Application.isPlaying)
			{
				int count = value.Count;
				for (int i = 0; i < count; i++)
				{
					UnityEngine.Object.Destroy(value[i].material);
				}
			}
			else
			{
				int count2 = value.Count;
				for (int j = 0; j < count2; j++)
				{
					UnityEngine.Object.DestroyImmediate(value[j].material);
				}
			}
			value.Clear();
		}
		enumerator.Dispose();
	}

	public void RefreshMaterials()
	{
		_combineTexture = _texture.alphaTexture != null;
		Dictionary<int, List<MaterialRef>>.Enumerator enumerator = _materials.GetEnumerator();
		while (enumerator.MoveNext())
		{
			List<MaterialRef> value = enumerator.Current.Value;
			int count = value.Count;
			for (int i = 0; i < count; i++)
			{
				Material material = value[i].material;
				material.mainTexture = _texture.nativeTexture;
				if (_combineTexture)
				{
					material.EnableKeyword("COMBINED");
					material.SetTexture(ShaderConfig.ID_AlphaTex, _texture.alphaTexture);
				}
			}
		}
		enumerator.Dispose();
	}
}
