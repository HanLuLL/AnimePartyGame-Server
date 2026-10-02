using UnityEngine;

namespace FairyGUI;

public class TextFormat
{
	public enum SpecialStyle
	{
		None,
		Superscript,
		Subscript
	}

	public float faceDilate;

	public float outlineSoftness;

	public float underlaySoftness;

	public int size;

	public string font;

	public Color color;

	public int lineSpacing;

	public int letterSpacing;

	public bool bold;

	public bool underline;

	public bool italic;

	public bool strikethrough;

	public Color32[] gradientColor;

	public AlignType align;

	public SpecialStyle specialStyle;

	public float outline;

	public Color outlineColor;

	public Vector2 shadowOffset;

	public Color shadowColor;

	public TextFormat()
	{
		color = Color.black;
		size = 12;
		lineSpacing = 3;
		outlineColor = (shadowColor = Color.black);
	}

	public void SetColor(uint value)
	{
		uint num = (value >> 16) & 0xFF;
		uint num2 = (value >> 8) & 0xFF;
		uint num3 = value & 0xFF;
		float r = (float)num / 255f;
		float g = (float)num2 / 255f;
		float b = (float)num3 / 255f;
		color = new Color(r, g, b, 1f);
	}

	public bool EqualStyle(TextFormat aFormat)
	{
		if (size == aFormat.size && color == aFormat.color && bold == aFormat.bold && underline == aFormat.underline && italic == aFormat.italic && strikethrough == aFormat.strikethrough && gradientColor == aFormat.gradientColor && align == aFormat.align)
		{
			return specialStyle == aFormat.specialStyle;
		}
		return false;
	}

	public void CopyFrom(TextFormat source)
	{
		size = source.size;
		font = source.font;
		color = source.color;
		lineSpacing = source.lineSpacing;
		letterSpacing = source.letterSpacing;
		bold = source.bold;
		underline = source.underline;
		italic = source.italic;
		strikethrough = source.strikethrough;
		if (source.gradientColor != null)
		{
			gradientColor = new Color32[4];
			source.gradientColor.CopyTo(gradientColor, 0);
		}
		else
		{
			gradientColor = null;
		}
		align = source.align;
		specialStyle = source.specialStyle;
	}

	public void FillVertexColors(Color32[] vertexColors)
	{
		if (gradientColor == null)
		{
			vertexColors[0] = (vertexColors[1] = (vertexColors[2] = (vertexColors[3] = color)));
			return;
		}
		vertexColors[0] = gradientColor[1];
		vertexColors[1] = gradientColor[0];
		vertexColors[2] = gradientColor[2];
		vertexColors[3] = gradientColor[3];
	}
}
