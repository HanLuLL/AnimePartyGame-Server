using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore;

namespace FairyGUI;

public class TMPFont : BaseFont
{
	protected TMP_FontAsset _fontAsset;

	private FontStyles _style;

	private float _scale;

	private float _padding;

	private float _stylePadding;

	private float _ascent;

	private float _lineHeight;

	private float _boldMultiplier;

	private FontWeight _defaultFontWeight;

	private FontWeight _fontWeight;

	private TextFormat _format;

	private TMP_Character _char;

	private TMP_Character _lineChar;

	private Material _material;

	private MaterialManager _manager;

	private static Vector3 bottomLeft;

	private static Vector3 topLeft;

	private static Vector3 topRight;

	private static Vector3 bottomRight;

	private static Vector4 uvBottomLeft;

	private static Vector4 uvTopLeft;

	private static Vector4 uvTopRight;

	private static Vector4 uvBottomRight;

	private static Vector4 uv2BottomLeft;

	private static Vector4 uv2TopLeft;

	private static Vector4 uv2TopRight;

	private static Vector4 uv2BottomRight;

	private static Color32[] vertexColors = new Color32[4];

	public TMP_FontAsset fontAsset
	{
		get
		{
			return _fontAsset;
		}
		set
		{
			_fontAsset = value;
			Init();
		}
	}

	public FontWeight fontWeight
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _defaultFontWeight;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_defaultFontWeight = value;
		}
	}

	public TMPFont()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		canTint = true;
		shader = "FairyGUI/TextMeshPro/Distance Field";
		keepCrisp = true;
		_defaultFontWeight = (FontWeight)500;
	}

	public override void Dispose()
	{
		Release();
	}

	private void Release()
	{
		if (_manager != null)
		{
			_manager.onCreateNewMaterial -= OnCreateNewMaterial;
			_manager = null;
		}
		if (mainTexture != null)
		{
			mainTexture.Dispose();
			mainTexture = null;
		}
		if (_material != null)
		{
			Object.DestroyImmediate(_material);
			_material = null;
		}
	}

	private void Init()
	{
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		Release();
		mainTexture = new NTexture(_fontAsset.atlasTexture);
		mainTexture.destroyMethod = DestroyMethod.None;
		_manager = mainTexture.GetMaterialManager(shader);
		_manager.onCreateNewMaterial += OnCreateNewMaterial;
		_material = new Material(((TMP_Asset)_fontAsset).material);
		_material.SetFloat(ShaderUtilities.ID_TextureWidth, mainTexture.width);
		_material.SetFloat(ShaderUtilities.ID_TextureHeight, mainTexture.height);
		_material.SetFloat(ShaderUtilities.ID_GradientScale, fontAsset.atlasPadding + 1);
		_material.SetFloat(ShaderUtilities.ID_WeightNormal, fontAsset.normalStyle);
		_material.SetFloat(ShaderUtilities.ID_WeightBold, fontAsset.boldStyle);
		FaceInfo faceInfo = _fontAsset.faceInfo;
		_ascent = ((FaceInfo)(ref faceInfo)).pointSize;
		faceInfo = _fontAsset.faceInfo;
		_lineHeight = (float)((FaceInfo)(ref faceInfo)).pointSize * 1.25f;
		_lineChar = GetCharacterFromFontAsset(95u, (FontStyles)0);
	}

	private void OnCreateNewMaterial(Material mat)
	{
		mat.SetFloat(ShaderUtilities.ID_TextureWidth, mainTexture.width);
		mat.SetFloat(ShaderUtilities.ID_TextureHeight, mainTexture.height);
		mat.SetFloat(ShaderUtilities.ID_GradientScale, fontAsset.atlasPadding + 1);
		mat.SetFloat(ShaderUtilities.ID_WeightNormal, fontAsset.normalStyle);
		mat.SetFloat(ShaderUtilities.ID_WeightBold, fontAsset.boldStyle);
	}

	public override void UpdateGraphics(NGraphics graphics)
	{
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Invalid comparison between Unknown and I4
		MaterialPropertyBlock materialPropertyBlock = graphics.materialPropertyBlock;
		if (_format.outline > 0f)
		{
			graphics.ToggleKeyword("OUTLINE_ON", enabled: true);
			materialPropertyBlock.SetFloat(ShaderUtilities.ID_OutlineWidth, _format.outline);
			materialPropertyBlock.SetColor(ShaderUtilities.ID_OutlineColor, _format.outlineColor);
		}
		else
		{
			graphics.ToggleKeyword("OUTLINE_ON", enabled: false);
			materialPropertyBlock.SetFloat(ShaderUtilities.ID_OutlineWidth, 0f);
		}
		if (_format.shadowOffset.x != 0f || _format.shadowOffset.y != 0f)
		{
			graphics.ToggleKeyword("UNDERLAY_ON", enabled: true);
			materialPropertyBlock.SetColor(ShaderUtilities.ID_UnderlayColor, _format.shadowColor);
			materialPropertyBlock.SetFloat(ShaderUtilities.ID_UnderlayOffsetX, _format.shadowOffset.x);
			materialPropertyBlock.SetFloat(ShaderUtilities.ID_UnderlayOffsetY, 0f - _format.shadowOffset.y);
			materialPropertyBlock.SetFloat(ShaderUtilities.ID_UnderlaySoftness, _format.underlaySoftness);
		}
		else
		{
			graphics.ToggleKeyword("UNDERLAY_ON", enabled: false);
			materialPropertyBlock.SetFloat(ShaderUtilities.ID_UnderlayOffsetX, 0f);
			materialPropertyBlock.SetFloat(ShaderUtilities.ID_UnderlayOffsetY, 0f);
			materialPropertyBlock.SetFloat(ShaderUtilities.ID_UnderlaySoftness, 0f);
		}
		materialPropertyBlock.SetFloat(ShaderUtilities.ID_FaceDilate, _format.faceDilate);
		materialPropertyBlock.SetFloat(ShaderUtilities.ID_OutlineSoftness, _format.outlineSoftness);
		if (_material.HasProperty(ShaderUtilities.ID_ScaleRatio_A))
		{
			_material.SetFloat(ShaderUtilities.ID_OutlineWidth, materialPropertyBlock.GetFloat(ShaderUtilities.ID_OutlineWidth));
			_material.SetFloat(ShaderUtilities.ID_UnderlayOffsetX, materialPropertyBlock.GetFloat(ShaderUtilities.ID_UnderlayOffsetX));
			_material.SetFloat(ShaderUtilities.ID_UnderlayOffsetY, materialPropertyBlock.GetFloat(ShaderUtilities.ID_UnderlayOffsetY));
			_material.SetFloat(ShaderUtilities.ID_UnderlaySoftness, materialPropertyBlock.GetFloat(ShaderUtilities.ID_UnderlaySoftness));
			_material.SetFloat(ShaderUtilities.ID_FaceDilate, materialPropertyBlock.GetFloat(ShaderUtilities.ID_FaceDilate));
			_material.SetFloat(ShaderUtilities.ID_OutlineSoftness, materialPropertyBlock.GetFloat(ShaderUtilities.ID_OutlineSoftness));
			_padding = ShaderUtilities.GetPadding(_material, false, false);
			materialPropertyBlock.SetFloat(ShaderUtilities.ID_ScaleRatio_A, _material.GetFloat(ShaderUtilities.ID_ScaleRatio_A));
			materialPropertyBlock.SetFloat(ShaderUtilities.ID_ScaleRatio_B, _material.GetFloat(ShaderUtilities.ID_ScaleRatio_B));
			materialPropertyBlock.SetFloat(ShaderUtilities.ID_ScaleRatio_C, _material.GetFloat(ShaderUtilities.ID_ScaleRatio_C));
		}
		if ((_style & 1) == 1)
		{
			if (_material.HasProperty(ShaderUtilities.ID_GradientScale))
			{
				float num = _material.GetFloat(ShaderUtilities.ID_GradientScale);
				_stylePadding = _fontAsset.boldStyle / 4f * num * _material.GetFloat(ShaderUtilities.ID_ScaleRatio_A);
				if (_stylePadding + _padding > num)
				{
					_padding = num - _stylePadding;
				}
			}
			else
			{
				_stylePadding = 0f;
			}
		}
		else if (_material.HasProperty(ShaderUtilities.ID_GradientScale))
		{
			float num2 = _material.GetFloat(ShaderUtilities.ID_GradientScale);
			_stylePadding = _fontAsset.normalStyle / 4f * num2 * _material.GetFloat(ShaderUtilities.ID_ScaleRatio_A);
			if (_stylePadding + _padding > num2)
			{
				_padding = num2 - _stylePadding;
			}
		}
		else
		{
			_stylePadding = 0f;
		}
	}

	public override void SetFormat(TextFormat format, float fontSizeScale)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		_format = format;
		float num = (float)format.size * fontSizeScale;
		if (_format.specialStyle == TextFormat.SpecialStyle.Subscript || _format.specialStyle == TextFormat.SpecialStyle.Superscript)
		{
			num *= 0.58f;
		}
		float num2 = num;
		FaceInfo faceInfo = _fontAsset.faceInfo;
		float num3 = num2 / (float)((FaceInfo)(ref faceInfo)).pointSize;
		faceInfo = _fontAsset.faceInfo;
		_scale = num3 * ((FaceInfo)(ref faceInfo)).scale;
		_style = (FontStyles)0;
		if (format.bold)
		{
			_style = (FontStyles)(_style | 1);
			_fontWeight = (FontWeight)700;
			_boldMultiplier = 1f + _fontAsset.boldSpacing * 0.01f;
		}
		else
		{
			_fontWeight = _defaultFontWeight;
			_boldMultiplier = 1f;
		}
		if (format.italic)
		{
			_style = (FontStyles)(_style | 2);
		}
		format.FillVertexColors(vertexColors);
	}

	public override bool GetGlyph(char ch, out float width, out float height, out float baseline)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		_char = GetCharacterFromFontAsset(ch, _style);
		if (_char != null)
		{
			GlyphMetrics metrics = ((TMP_TextElement)_char).glyph.metrics;
			width = ((GlyphMetrics)(ref metrics)).horizontalAdvance * _boldMultiplier * _scale;
			height = _lineHeight * _scale;
			baseline = _ascent * _scale;
			if (_format.specialStyle == TextFormat.SpecialStyle.Subscript)
			{
				height /= 0.58f;
				baseline /= 0.58f;
			}
			else if (_format.specialStyle == TextFormat.SpecialStyle.Superscript)
			{
				height = height / 0.58f + baseline * 0.33f;
				baseline *= 2.054138f;
			}
			height = Mathf.RoundToInt(height);
			baseline = Mathf.RoundToInt(baseline);
			return true;
		}
		width = 0f;
		height = 0f;
		baseline = 0f;
		return false;
	}

	private TMP_Character GetCharacterFromFontAsset(uint unicode, FontStyles fontStyle)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		bool flag = default(bool);
		return TMP_FontAssetUtilities.GetCharacterFromFontAsset(unicode, _fontAsset, true, fontStyle, _fontWeight, ref flag);
	}

	public override int DrawGlyph(float x, float y, List<Vector3> vertList, List<Vector2> uvList, List<Vector2> uv2List, List<Color32> colList)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Invalid comparison between Unknown and I4
		GlyphMetrics metrics = ((TMP_TextElement)_char).glyph.metrics;
		GlyphRect glyphRect = ((TMP_TextElement)_char).glyph.glyphRect;
		if (_format.specialStyle == TextFormat.SpecialStyle.Subscript)
		{
			y -= (float)Mathf.RoundToInt(_ascent * _scale * 0.33f);
		}
		else if (_format.specialStyle == TextFormat.SpecialStyle.Superscript)
		{
			y += (float)Mathf.RoundToInt(_ascent * _scale * 1.0541381f);
		}
		topLeft.x = x + (((GlyphMetrics)(ref metrics)).horizontalBearingX - _padding - _stylePadding) * _scale;
		topLeft.y = y + (((GlyphMetrics)(ref metrics)).horizontalBearingY + _padding) * _scale;
		bottomRight.x = topLeft.x + (((GlyphMetrics)(ref metrics)).width + _padding * 2f + _stylePadding * 2f) * _scale;
		bottomRight.y = topLeft.y - (((GlyphMetrics)(ref metrics)).height + _padding * 2f) * _scale;
		topRight.x = bottomRight.x;
		topRight.y = topLeft.y;
		bottomLeft.x = topLeft.x;
		bottomLeft.y = bottomRight.y;
		if ((_style & 2) == 2)
		{
			float num = (float)(int)_fontAsset.italicStyle * 0.01f;
			Vector3 vector = new Vector3(num * ((((GlyphMetrics)(ref metrics)).horizontalBearingY + _padding + _stylePadding) * _scale), 0f, 0f);
			Vector3 vector2 = new Vector3(num * ((((GlyphMetrics)(ref metrics)).horizontalBearingY - ((GlyphMetrics)(ref metrics)).height - _padding - _stylePadding) * _scale), 0f, 0f);
			topLeft += vector;
			bottomLeft += vector2;
			topRight += vector;
			bottomRight += vector2;
		}
		vertList.Add(bottomLeft);
		vertList.Add(topLeft);
		vertList.Add(topRight);
		vertList.Add(bottomRight);
		float num2 = ((float)((GlyphRect)(ref glyphRect)).x - _padding - _stylePadding) / (float)_fontAsset.atlasWidth;
		float num3 = ((float)((GlyphRect)(ref glyphRect)).y - _padding - _stylePadding) / (float)_fontAsset.atlasHeight;
		float num4 = ((float)((GlyphRect)(ref glyphRect)).width + _padding * 2f + _stylePadding * 2f) / (float)_fontAsset.atlasWidth;
		float num5 = ((float)((GlyphRect)(ref glyphRect)).height + _padding * 2f + _stylePadding * 2f) / (float)_fontAsset.atlasHeight;
		uvBottomLeft = new Vector2(num2, num3);
		uvTopLeft = new Vector2(num2, num3 + num5);
		uvTopRight = new Vector2(num2 + num4, num3 + num5);
		uvBottomRight = new Vector2(num2 + num4, num3);
		float num6 = _scale * 0.01f;
		if (_format.bold)
		{
			num6 *= -1f;
		}
		uv2BottomLeft = new Vector2(0f, num6);
		uv2TopLeft = new Vector2(511f, num6);
		uv2TopRight = new Vector2(2093567f, num6);
		uv2BottomRight = new Vector2(2093056f, num6);
		uvList.Add(uvBottomLeft);
		uvList.Add(uvTopLeft);
		uvList.Add(uvTopRight);
		uvList.Add(uvBottomRight);
		uv2List.Add(uv2BottomLeft);
		uv2List.Add(uv2TopLeft);
		uv2List.Add(uv2TopRight);
		uv2List.Add(uv2BottomRight);
		colList.Add(vertexColors[0]);
		colList.Add(vertexColors[1]);
		colList.Add(vertexColors[2]);
		colList.Add(vertexColors[3]);
		return 4;
	}

	public override int DrawLine(float x, float y, float width, int fontSize, int type, List<Vector3> vertList, List<Vector2> uvList, List<Vector2> uv2List, List<Color32> colList)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		if (_lineChar == null)
		{
			return 0;
		}
		FaceInfo faceInfo;
		float num;
		float num2;
		if (type == 0)
		{
			faceInfo = _fontAsset.faceInfo;
			num = ((FaceInfo)(ref faceInfo)).underlineThickness;
			faceInfo = _fontAsset.faceInfo;
			num2 = ((FaceInfo)(ref faceInfo)).underlineOffset;
		}
		else
		{
			faceInfo = _fontAsset.faceInfo;
			num = ((FaceInfo)(ref faceInfo)).strikethroughThickness;
			faceInfo = _fontAsset.faceInfo;
			num2 = ((FaceInfo)(ref faceInfo)).strikethroughOffset;
		}
		float num3 = fontSize;
		faceInfo = _fontAsset.faceInfo;
		float num4 = num3 / (float)((FaceInfo)(ref faceInfo)).pointSize;
		faceInfo = _fontAsset.faceInfo;
		float num5 = num4 * ((FaceInfo)(ref faceInfo)).scale;
		GlyphMetrics metrics = ((TMP_TextElement)_lineChar).glyph.metrics;
		float num6 = ((GlyphMetrics)(ref metrics)).width / 2f * num5;
		metrics = ((TMP_TextElement)_lineChar).glyph.metrics;
		if (width < ((GlyphMetrics)(ref metrics)).width * num5)
		{
			num6 = width / 2f;
		}
		num *= num5;
		if (num < 1f)
		{
			num = 1f;
		}
		num2 = Mathf.RoundToInt(num2 * num5);
		y += num2;
		topLeft.x = x;
		topLeft.y = y + _padding * num5;
		bottomRight.x = x + num6;
		bottomRight.y = y - num - _padding * num5;
		topRight.x = bottomRight.x;
		topRight.y = topLeft.y;
		bottomLeft.x = topLeft.x;
		bottomLeft.y = bottomRight.y;
		vertList.Add(bottomLeft);
		vertList.Add(topLeft);
		vertList.Add(topRight);
		vertList.Add(bottomRight);
		topLeft = topRight;
		bottomLeft = bottomRight;
		topRight.x = x + width - num6;
		bottomRight.x = topRight.x;
		vertList.Add(bottomLeft);
		vertList.Add(topLeft);
		vertList.Add(topRight);
		vertList.Add(bottomRight);
		topLeft = topRight;
		bottomLeft = bottomRight;
		topRight.x = x + width;
		bottomRight.x = topRight.x;
		vertList.Add(bottomLeft);
		vertList.Add(topLeft);
		vertList.Add(topRight);
		vertList.Add(bottomRight);
		GlyphRect glyphRect = ((TMP_TextElement)_lineChar).glyph.glyphRect;
		float x2 = ((float)((GlyphRect)(ref glyphRect)).x - _padding) / (float)_fontAsset.atlasWidth;
		glyphRect = ((TMP_TextElement)_lineChar).glyph.glyphRect;
		Vector2 item = new Vector2(x2, ((float)((GlyphRect)(ref glyphRect)).y - _padding) / (float)_fontAsset.atlasHeight);
		float x3 = item.x;
		glyphRect = ((TMP_TextElement)_lineChar).glyph.glyphRect;
		int y2 = ((GlyphRect)(ref glyphRect)).y;
		glyphRect = ((TMP_TextElement)_lineChar).glyph.glyphRect;
		Vector2 item2 = new Vector2(x3, ((float)(y2 + ((GlyphRect)(ref glyphRect)).height) + _padding) / (float)_fontAsset.atlasHeight);
		glyphRect = ((TMP_TextElement)_lineChar).glyph.glyphRect;
		float num7 = (float)((GlyphRect)(ref glyphRect)).x - _padding;
		glyphRect = ((TMP_TextElement)_lineChar).glyph.glyphRect;
		Vector2 item3 = new Vector2((num7 + (float)((GlyphRect)(ref glyphRect)).width / 2f) / (float)_fontAsset.atlasWidth, item2.y);
		Vector2 item4 = new Vector2(item3.x, item.y);
		glyphRect = ((TMP_TextElement)_lineChar).glyph.glyphRect;
		float num8 = (float)((GlyphRect)(ref glyphRect)).x + _padding;
		glyphRect = ((TMP_TextElement)_lineChar).glyph.glyphRect;
		Vector2 item5 = new Vector2((num8 + (float)((GlyphRect)(ref glyphRect)).width / 2f) / (float)_fontAsset.atlasWidth, item2.y);
		Vector2 item6 = new Vector2(item5.x, item.y);
		glyphRect = ((TMP_TextElement)_lineChar).glyph.glyphRect;
		float num9 = (float)((GlyphRect)(ref glyphRect)).x + _padding;
		glyphRect = ((TMP_TextElement)_lineChar).glyph.glyphRect;
		Vector2 item7 = new Vector2((num9 + (float)((GlyphRect)(ref glyphRect)).width) / (float)_fontAsset.atlasWidth, item2.y);
		Vector2 item8 = new Vector2(item7.x, item.y);
		uvList.Add(item);
		uvList.Add(item2);
		uvList.Add(item3);
		uvList.Add(item4);
		uvList.Add(new Vector2(item3.x - item3.x * 0.001f, item.y));
		uvList.Add(new Vector2(item3.x - item3.x * 0.001f, item2.y));
		uvList.Add(new Vector2(item3.x + item3.x * 0.001f, item2.y));
		uvList.Add(new Vector2(item3.x + item3.x * 0.001f, item.y));
		uvList.Add(item6);
		uvList.Add(item5);
		uvList.Add(item7);
		uvList.Add(item8);
		float num10 = num6 / width;
		float x4 = 1f - num10;
		float xScale = num5 * 0.01f;
		uv2List.Add(PackUV(0f, 0f, xScale));
		uv2List.Add(PackUV(0f, 1f, xScale));
		uv2List.Add(PackUV(num10, 1f, xScale));
		uv2List.Add(PackUV(num10, 0f, xScale));
		uv2List.Add(PackUV(num10, 0f, xScale));
		uv2List.Add(PackUV(num10, 1f, xScale));
		uv2List.Add(PackUV(x4, 1f, xScale));
		uv2List.Add(PackUV(x4, 0f, xScale));
		uv2List.Add(PackUV(x4, 0f, xScale));
		uv2List.Add(PackUV(x4, 1f, xScale));
		uv2List.Add(PackUV(1f, 1f, xScale));
		uv2List.Add(PackUV(1f, 0f, xScale));
		for (int i = 0; i < 12; i++)
		{
			colList.Add(vertexColors[0]);
		}
		return 12;
	}

	private Vector2 PackUV(float x, float y, float xScale)
	{
		double num = (int)(x * 511f);
		double num2 = (int)(y * 511f);
		return new Vector2((float)(num * 4096.0 + num2), xScale);
	}

	public override bool HasCharacter(char ch)
	{
		return _fontAsset.HasCharacter(ch, false, false);
	}

	public override int GetLineHeight(int size)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		float lineHeight = _lineHeight;
		float num = size;
		FaceInfo faceInfo = _fontAsset.faceInfo;
		float num2 = num / (float)((FaceInfo)(ref faceInfo)).pointSize;
		faceInfo = _fontAsset.faceInfo;
		return Mathf.RoundToInt(lineHeight * (num2 * ((FaceInfo)(ref faceInfo)).scale));
	}
}
