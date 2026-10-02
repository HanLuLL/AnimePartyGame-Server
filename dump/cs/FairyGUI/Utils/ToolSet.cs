using System;
using UnityEngine;

namespace FairyGUI.Utils;

public static class ToolSet
{
	public static Color ConvertFromHtmlColor(string str)
	{
		if (str.Length < 7 || str[0] != '#')
		{
			return Color.black;
		}
		if (str.Length == 9)
		{
			return new Color32((byte)(CharToHex(str[3]) * 16 + CharToHex(str[4])), (byte)(CharToHex(str[5]) * 16 + CharToHex(str[6])), (byte)(CharToHex(str[7]) * 16 + CharToHex(str[8])), (byte)(CharToHex(str[1]) * 16 + CharToHex(str[2])));
		}
		return new Color32((byte)(CharToHex(str[1]) * 16 + CharToHex(str[2])), (byte)(CharToHex(str[3]) * 16 + CharToHex(str[4])), (byte)(CharToHex(str[5]) * 16 + CharToHex(str[6])), byte.MaxValue);
	}

	public static Color ColorFromRGB(int value)
	{
		return new Color((float)((value >> 16) & 0xFF) / 255f, (float)((value >> 8) & 0xFF) / 255f, (float)(value & 0xFF) / 255f, 1f);
	}

	public static Color ColorFromRGBA(uint value)
	{
		return new Color((float)((value >> 16) & 0xFF) / 255f, (float)((value >> 8) & 0xFF) / 255f, (float)(value & 0xFF) / 255f, (float)((value >> 24) & 0xFF) / 255f);
	}

	public static int CharToHex(char c)
	{
		if (c >= '0' && c <= '9')
		{
			return c - 48;
		}
		if (c >= 'A' && c <= 'F')
		{
			return 10 + c - 65;
		}
		if (c >= 'a' && c <= 'f')
		{
			return 10 + c - 97;
		}
		return 0;
	}

	public static Rect Intersection(ref Rect rect1, ref Rect rect2)
	{
		if (rect1.width == 0f || rect1.height == 0f || rect2.width == 0f || rect2.height == 0f)
		{
			return new Rect(0f, 0f, 0f, 0f);
		}
		float num = ((rect1.xMin > rect2.xMin) ? rect1.xMin : rect2.xMin);
		float num2 = ((rect1.xMax < rect2.xMax) ? rect1.xMax : rect2.xMax);
		float num3 = ((rect1.yMin > rect2.yMin) ? rect1.yMin : rect2.yMin);
		float num4 = ((rect1.yMax < rect2.yMax) ? rect1.yMax : rect2.yMax);
		if (num > num2 || num3 > num4)
		{
			return new Rect(0f, 0f, 0f, 0f);
		}
		return Rect.MinMaxRect(num, num3, num2, num4);
	}

	public static Rect Union(ref Rect rect1, ref Rect rect2)
	{
		if (rect2.width == 0f || rect2.height == 0f)
		{
			return rect1;
		}
		if (rect1.width == 0f || rect1.height == 0f)
		{
			return rect2;
		}
		float num = Mathf.Min(rect1.x, rect2.x);
		float num2 = Mathf.Min(rect1.y, rect2.y);
		return new Rect(num, num2, Mathf.Max(rect1.xMax, rect2.xMax) - num, Mathf.Max(rect1.yMax, rect2.yMax) - num2);
	}

	public static void SkewMatrix(ref Matrix4x4 matrix, float skewX, float skewY)
	{
		skewX = (0f - skewX) * ((float)Math.PI / 180f);
		skewY = (0f - skewY) * ((float)Math.PI / 180f);
		float num = Mathf.Sin(skewX);
		float num2 = Mathf.Cos(skewX);
		float num3 = Mathf.Sin(skewY);
		float num4 = Mathf.Cos(skewY);
		float m = matrix.m00 * num4 - matrix.m10 * num;
		float m2 = matrix.m00 * num3 + matrix.m10 * num2;
		float m3 = matrix.m01 * num4 - matrix.m11 * num;
		float m4 = matrix.m01 * num3 + matrix.m11 * num2;
		float m5 = matrix.m02 * num4 - matrix.m12 * num;
		float m6 = matrix.m02 * num3 + matrix.m12 * num2;
		matrix.m00 = m;
		matrix.m10 = m2;
		matrix.m01 = m3;
		matrix.m11 = m4;
		matrix.m02 = m5;
		matrix.m12 = m6;
	}

	public static void RotateUV(Vector2[] uv, ref Rect baseUVRect)
	{
		int num = uv.Length;
		float num2 = Mathf.Min(baseUVRect.xMin, baseUVRect.xMax);
		float num3 = baseUVRect.yMin;
		float num4 = baseUVRect.yMax;
		if (num3 > num4)
		{
			num3 = num4;
			num4 = baseUVRect.yMin;
		}
		for (int i = 0; i < num; i++)
		{
			Vector2 vector = uv[i];
			float y = vector.y;
			vector.y = num3 + vector.x - num2;
			vector.x = num2 + num4 - y;
			uv[i] = vector;
		}
	}
}
