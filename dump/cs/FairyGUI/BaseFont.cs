using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

public class BaseFont
{
	public string name;

	public NTexture mainTexture;

	public bool canTint;

	public bool customBold;

	public bool customBoldAndItalic;

	public bool customOutline;

	public string shader;

	public bool keepCrisp;

	public int version;

	protected internal static bool textRebuildFlag;

	protected const float SupScale = 0.58f;

	protected const float SupOffset = 0.33f;

	public virtual void UpdateGraphics(NGraphics graphics)
	{
	}

	public virtual void SetFormat(TextFormat format, float fontSizeScale)
	{
	}

	public virtual void PrepareCharacters(string text)
	{
	}

	public virtual bool GetGlyph(char ch, out float width, out float height, out float baseline)
	{
		width = 0f;
		height = 0f;
		baseline = 0f;
		return false;
	}

	public virtual int DrawGlyph(float x, float y, List<Vector3> vertList, List<Vector2> uvList, List<Vector2> uv2List, List<Color32> colList)
	{
		return 0;
	}

	public virtual int DrawLine(float x, float y, float width, int fontSize, int type, List<Vector3> vertList, List<Vector2> uvList, List<Vector2> uv2List, List<Color32> colList)
	{
		return 0;
	}

	public virtual bool HasCharacter(char ch)
	{
		return false;
	}

	public virtual int GetLineHeight(int size)
	{
		return 0;
	}

	public virtual void Dispose()
	{
	}
}
