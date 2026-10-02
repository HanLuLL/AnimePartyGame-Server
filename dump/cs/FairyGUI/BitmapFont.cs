using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

public class BitmapFont : BaseFont
{
	public class BMGlyph
	{
		public float x;

		public float y;

		public float width;

		public float height;

		public int advance;

		public int lineHeight;

		public Vector2[] uv = new Vector2[4];

		public int channel;
	}

	public int size;

	public bool resizable;

	public bool hasChannel;

	protected Dictionary<int, BMGlyph> _dict;

	protected BMGlyph _glyph;

	private float _scale;

	private static Vector3 bottomLeft;

	private static Vector3 topLeft;

	private static Vector3 topRight;

	private static Vector3 bottomRight;

	private static Color32[] vertexColors = new Color32[4];

	public BitmapFont()
	{
		canTint = true;
		hasChannel = false;
		customOutline = true;
		shader = ShaderConfig.bmFontShader;
		_dict = new Dictionary<int, BMGlyph>();
		_scale = 1f;
	}

	public void AddChar(char ch, BMGlyph glyph)
	{
		_dict[ch] = glyph;
	}

	public override void SetFormat(TextFormat format, float fontSizeScale)
	{
		if (resizable)
		{
			_scale = (float)format.size / (float)size * fontSizeScale;
		}
		else
		{
			_scale = fontSizeScale;
		}
		if (canTint)
		{
			format.FillVertexColors(vertexColors);
		}
	}

	public override bool GetGlyph(char ch, out float width, out float height, out float baseline)
	{
		if (ch == ' ')
		{
			width = Mathf.RoundToInt((float)size * _scale / 2f);
			height = Mathf.RoundToInt((float)size * _scale);
			baseline = height;
			_glyph = null;
			return true;
		}
		if (_dict.TryGetValue(ch, out _glyph))
		{
			width = Mathf.RoundToInt((float)_glyph.advance * _scale);
			height = Mathf.RoundToInt((float)_glyph.lineHeight * _scale);
			baseline = height;
			return true;
		}
		width = 0f;
		height = 0f;
		baseline = 0f;
		return false;
	}

	public override int DrawGlyph(float x, float y, List<Vector3> vertList, List<Vector2> uvList, List<Vector2> uv2List, List<Color32> colList)
	{
		if (_glyph == null)
		{
			return 0;
		}
		topLeft.x = x + _glyph.x * _scale;
		topLeft.y = y + ((float)_glyph.lineHeight - _glyph.y) * _scale;
		bottomRight.x = x + (_glyph.x + _glyph.width) * _scale;
		bottomRight.y = topLeft.y - _glyph.height * _scale;
		topRight.x = bottomRight.x;
		topRight.y = topLeft.y;
		bottomLeft.x = topLeft.x;
		bottomLeft.y = bottomRight.y;
		vertList.Add(bottomLeft);
		vertList.Add(topLeft);
		vertList.Add(topRight);
		vertList.Add(bottomRight);
		uvList.AddRange(_glyph.uv);
		if (hasChannel)
		{
			Vector2 item = new Vector2(_glyph.channel, 0f);
			uv2List.Add(item);
			uv2List.Add(item);
			uv2List.Add(item);
			uv2List.Add(item);
		}
		if (canTint)
		{
			colList.Add(vertexColors[0]);
			colList.Add(vertexColors[1]);
			colList.Add(vertexColors[2]);
			colList.Add(vertexColors[3]);
		}
		else
		{
			colList.Add(Color.white);
			colList.Add(Color.white);
			colList.Add(Color.white);
			colList.Add(Color.white);
		}
		return 4;
	}

	public override bool HasCharacter(char ch)
	{
		if (ch != ' ')
		{
			return _dict.ContainsKey(ch);
		}
		return true;
	}

	public override int GetLineHeight(int size)
	{
		if (_dict.Count > 0)
		{
			using (Dictionary<int, BMGlyph>.Enumerator enumerator = _dict.GetEnumerator())
			{
				enumerator.MoveNext();
				if (resizable)
				{
					return Mathf.RoundToInt((float)enumerator.Current.Value.lineHeight * (float)size / (float)this.size);
				}
				return enumerator.Current.Value.lineHeight;
			}
		}
		return 0;
	}
}
