using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

public class DynamicFont : BaseFont
{
	private Font _font;

	private int _size;

	private float _ascent;

	private float _lineHeight;

	private float _scale;

	private TextFormat _format;

	private FontStyle _style;

	private bool _boldVertice;

	private CharacterInfo _char;

	private CharacterInfo _lineChar;

	private bool _gotLineChar;

	private static Vector3 bottomLeft;

	private static Vector3 topLeft;

	private static Vector3 topRight;

	private static Vector3 bottomRight;

	private static Vector2 uvBottomLeft;

	private static Vector2 uvTopLeft;

	private static Vector2 uvTopRight;

	private static Vector2 uvBottomRight;

	private static Color32[] vertexColors = new Color32[4];

	private static Vector3[] BOLD_OFFSET = new Vector3[4]
	{
		new Vector3(-0.5f, 0f, 0f),
		new Vector3(0.5f, 0f, 0f),
		new Vector3(0f, -0.5f, 0f),
		new Vector3(0f, 0.5f, 0f)
	};

	public Font nativeFont
	{
		get
		{
			return _font;
		}
		set
		{
			if ((Object)(object)_font != null)
			{
				Font.textureRebuilt -= textureRebuildCallback;
			}
			_font = value;
			Font.textureRebuilt += textureRebuildCallback;
			((Object)(object)_font).hideFlags = DisplayObject.hideFlags;
			_font.material.hideFlags = DisplayObject.hideFlags;
			_font.material.mainTexture.hideFlags = DisplayObject.hideFlags;
			if (mainTexture != null)
			{
				mainTexture.Dispose();
			}
			mainTexture = new NTexture(_font.material.mainTexture);
			mainTexture.destroyMethod = DestroyMethod.None;
			_ascent = _font.fontSize;
			_lineHeight = (float)_font.fontSize * 1.25f;
		}
	}

	public DynamicFont()
	{
		canTint = true;
		keepCrisp = true;
		customOutline = true;
		shader = ShaderConfig.textShader;
	}

	public DynamicFont(string name, Font font)
		: this()
	{
		base.name = name;
		nativeFont = font;
	}

	public override void Dispose()
	{
		Font.textureRebuilt -= textureRebuildCallback;
	}

	public override void SetFormat(TextFormat format, float fontSizeScale)
	{
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		_format = format;
		float num = (float)format.size * fontSizeScale;
		if (keepCrisp)
		{
			num *= UIContentScaler.scaleFactor;
		}
		if (_format.specialStyle == TextFormat.SpecialStyle.Subscript || _format.specialStyle == TextFormat.SpecialStyle.Superscript)
		{
			num *= 0.58f;
		}
		_size = Mathf.FloorToInt(num);
		if (_size == 0)
		{
			_size = 1;
		}
		_scale = (float)_size / (float)_font.fontSize;
		if (format.bold && !customBold)
		{
			if (format.italic)
			{
				if (customBoldAndItalic)
				{
					_style = (FontStyle)2;
				}
				else
				{
					_style = (FontStyle)3;
				}
			}
			else
			{
				_style = (FontStyle)1;
			}
		}
		else if (format.italic)
		{
			_style = (FontStyle)2;
		}
		else
		{
			_style = (FontStyle)0;
		}
		_boldVertice = format.bold && (customBold || (format.italic && customBoldAndItalic));
		format.FillVertexColors(vertexColors);
	}

	public override void PrepareCharacters(string text)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		_font.RequestCharactersInTexture(text, _size, _style);
	}

	public override bool GetGlyph(char ch, out float width, out float height, out float baseline)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		if (!_font.GetCharacterInfo(ch, ref _char, _size, _style))
		{
			if (ch != ' ')
			{
				width = (height = (baseline = 0f));
				return false;
			}
			_font.RequestCharactersInTexture(" ", _size, _style);
			_font.GetCharacterInfo(ch, ref _char, _size, _style);
		}
		width = ((CharacterInfo)(ref _char)).advance;
		height = _lineHeight * _scale;
		baseline = _ascent * _scale;
		if (_boldVertice)
		{
			width += 1f;
		}
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
		if (keepCrisp)
		{
			width /= UIContentScaler.scaleFactor;
			height /= UIContentScaler.scaleFactor;
			baseline /= UIContentScaler.scaleFactor;
		}
		return true;
	}

	public override int DrawGlyph(float x, float y, List<Vector3> vertList, List<Vector2> uvList, List<Vector2> uv2List, List<Color32> colList)
	{
		topLeft.x = ((CharacterInfo)(ref _char)).minX;
		topLeft.y = ((CharacterInfo)(ref _char)).maxY;
		bottomRight.x = ((CharacterInfo)(ref _char)).maxX;
		if (((CharacterInfo)(ref _char)).glyphWidth == 0)
		{
			bottomRight.x = topLeft.x + (float)(_size / 2);
		}
		bottomRight.y = ((CharacterInfo)(ref _char)).minY;
		if (keepCrisp)
		{
			topLeft /= UIContentScaler.scaleFactor;
			bottomRight /= UIContentScaler.scaleFactor;
		}
		if (_format.specialStyle == TextFormat.SpecialStyle.Subscript)
		{
			y -= (float)Mathf.RoundToInt(_ascent * _scale * 0.33f);
		}
		else if (_format.specialStyle == TextFormat.SpecialStyle.Superscript)
		{
			y += (float)Mathf.RoundToInt(_ascent * _scale * 1.0541381f);
		}
		topLeft.x += x;
		topLeft.y += y;
		bottomRight.x += x;
		bottomRight.y += y;
		topRight.x = bottomRight.x;
		topRight.y = topLeft.y;
		bottomLeft.x = topLeft.x;
		bottomLeft.y = bottomRight.y;
		vertList.Add(bottomLeft);
		vertList.Add(topLeft);
		vertList.Add(topRight);
		vertList.Add(bottomRight);
		uvBottomLeft = ((CharacterInfo)(ref _char)).uvBottomLeft;
		uvTopLeft = ((CharacterInfo)(ref _char)).uvTopLeft;
		uvTopRight = ((CharacterInfo)(ref _char)).uvTopRight;
		uvBottomRight = ((CharacterInfo)(ref _char)).uvBottomRight;
		uvList.Add(uvBottomLeft);
		uvList.Add(uvTopLeft);
		uvList.Add(uvTopRight);
		uvList.Add(uvBottomRight);
		colList.Add(vertexColors[0]);
		colList.Add(vertexColors[1]);
		colList.Add(vertexColors[2]);
		colList.Add(vertexColors[3]);
		if (_boldVertice)
		{
			for (int i = 0; i < 4; i++)
			{
				Vector3 vector = BOLD_OFFSET[i];
				vertList.Add(bottomLeft + vector);
				vertList.Add(topLeft + vector);
				vertList.Add(topRight + vector);
				vertList.Add(bottomRight + vector);
				uvList.Add(uvBottomLeft);
				uvList.Add(uvTopLeft);
				uvList.Add(uvTopRight);
				uvList.Add(uvBottomRight);
				colList.Add(vertexColors[0]);
				colList.Add(vertexColors[1]);
				colList.Add(vertexColors[2]);
				colList.Add(vertexColors[3]);
			}
			return 20;
		}
		return 4;
	}

	public override int DrawLine(float x, float y, float width, int fontSize, int type, List<Vector3> vertList, List<Vector2> uvList, List<Vector2> uv2List, List<Color32> colList)
	{
		if (!_gotLineChar)
		{
			_gotLineChar = true;
			_font.RequestCharactersInTexture("_", 50, (FontStyle)0);
			_font.GetCharacterInfo('_', ref _lineChar, 50, (FontStyle)0);
		}
		float num = Mathf.Max(1f, (float)fontSize / 16f);
		float num2 = ((type != 0) ? ((float)Mathf.RoundToInt(_ascent * 0.4f * (float)fontSize / (float)_font.fontSize)) : ((float)Mathf.RoundToInt((float)((CharacterInfo)(ref _lineChar)).minY * (float)fontSize / 50f + num)));
		if (num < 1f)
		{
			num = 1f;
		}
		topLeft.x = x;
		topLeft.y = y + num2;
		bottomRight.x = x + width;
		bottomRight.y = topLeft.y - num;
		topRight.x = bottomRight.x;
		topRight.y = topLeft.y;
		bottomLeft.x = topLeft.x;
		bottomLeft.y = bottomRight.y;
		vertList.Add(bottomLeft);
		vertList.Add(topLeft);
		vertList.Add(topRight);
		vertList.Add(bottomRight);
		uvBottomLeft = ((CharacterInfo)(ref _lineChar)).uvBottomLeft;
		uvTopLeft = ((CharacterInfo)(ref _lineChar)).uvTopLeft;
		uvTopRight = ((CharacterInfo)(ref _lineChar)).uvTopRight;
		uvBottomRight = ((CharacterInfo)(ref _lineChar)).uvBottomRight;
		Vector2 item = default(Vector2);
		if (((CharacterInfo)(ref _lineChar)).uvBottomLeft.x != ((CharacterInfo)(ref _lineChar)).uvBottomRight.x)
		{
			item.x = (((CharacterInfo)(ref _lineChar)).uvBottomLeft.x + ((CharacterInfo)(ref _lineChar)).uvBottomRight.x) * 0.5f;
		}
		else
		{
			item.x = (((CharacterInfo)(ref _lineChar)).uvBottomLeft.x + ((CharacterInfo)(ref _lineChar)).uvTopLeft.x) * 0.5f;
		}
		if (((CharacterInfo)(ref _lineChar)).uvBottomLeft.y != ((CharacterInfo)(ref _lineChar)).uvTopLeft.y)
		{
			item.y = (((CharacterInfo)(ref _lineChar)).uvBottomLeft.y + ((CharacterInfo)(ref _lineChar)).uvTopLeft.y) * 0.5f;
		}
		else
		{
			item.y = (((CharacterInfo)(ref _lineChar)).uvBottomLeft.y + ((CharacterInfo)(ref _lineChar)).uvBottomRight.y) * 0.5f;
		}
		uvList.Add(item);
		uvList.Add(item);
		uvList.Add(item);
		uvList.Add(item);
		colList.Add(vertexColors[0]);
		colList.Add(vertexColors[1]);
		colList.Add(vertexColors[2]);
		colList.Add(vertexColors[3]);
		if (_boldVertice)
		{
			for (int i = 0; i < 4; i++)
			{
				Vector3 vector = BOLD_OFFSET[i];
				vertList.Add(bottomLeft + vector);
				vertList.Add(topLeft + vector);
				vertList.Add(topRight + vector);
				vertList.Add(bottomRight + vector);
				uvList.Add(item);
				uvList.Add(item);
				uvList.Add(item);
				uvList.Add(item);
				colList.Add(vertexColors[0]);
				colList.Add(vertexColors[1]);
				colList.Add(vertexColors[2]);
				colList.Add(vertexColors[3]);
			}
			return 20;
		}
		return 4;
	}

	public override bool HasCharacter(char ch)
	{
		return _font.HasCharacter(ch);
	}

	public override int GetLineHeight(int size)
	{
		return Mathf.RoundToInt(_lineHeight * (float)size / (float)_font.fontSize);
	}

	private void textureRebuildCallback(Font targetFont)
	{
		if (!((Object)(object)_font != (Object)(object)targetFont))
		{
			if (mainTexture == null || !Application.isPlaying)
			{
				mainTexture = new NTexture(_font.material.mainTexture);
				mainTexture.destroyMethod = DestroyMethod.None;
			}
			else
			{
				mainTexture.Reload(_font.material.mainTexture, null);
			}
			_gotLineChar = false;
			BaseFont.textRebuildFlag = true;
			version++;
		}
	}
}
