using System;
using UnityEngine;

namespace Tools;

public static class MathUtils
{
	public static readonly float EPSILON = 1E-05f;

	public static readonly float TENTHOUSAND = 0.0001f;

	public static bool UnityEquals(this float self, float other)
	{
		return Mathf.Abs(self - other) < EPSILON;
	}

	public static float DividedTenThousand(this uint self)
	{
		return (float)self * TENTHOUSAND;
	}

	public static float DividedTenThousand(this int self)
	{
		return (float)self * TENTHOUSAND;
	}

	public static float DividedOneThousand(this int self)
	{
		return (float)self * 0.001f;
	}

	public static bool UnityEquals(this double self, double other)
	{
		return Mathf.Abs((float)self - (float)other) < EPSILON;
	}

	public static uint CeilToUInt(float value)
	{
		return (uint)Math.Ceiling(value);
	}

	public static uint FloorToUInt(float value)
	{
		return (uint)Math.Floor(value);
	}

	public static int RoundToInt(double value)
	{
		return (int)Math.Round(value);
	}

	public static uint RoundToUInt(double value)
	{
		return (uint)Math.Round(value);
	}

	public static uint Max(uint a, uint b)
	{
		if (a <= b)
		{
			return b;
		}
		return a;
	}

	public static uint Clamp(uint value, uint min, uint max)
	{
		if (value < min)
		{
			value = min;
		}
		else if (value > max)
		{
			value = max;
		}
		return value;
	}

	public static float Clamp(float value, float min, float max)
	{
		if (value < min)
		{
			value = min;
		}
		else if (value > max)
		{
			value = max;
		}
		return value;
	}

	public static double Clamp(double value, double min, double max)
	{
		if (value < min)
		{
			value = min;
		}
		else if (value > max)
		{
			value = max;
		}
		return value;
	}

	public static bool Contains(this Vector2 rect, float point)
	{
		float num = 0f;
		float num2 = 0f;
		if (rect.x < rect.y)
		{
			num = rect.x;
			num2 = rect.y;
		}
		else
		{
			num = rect.y;
			num2 = rect.x;
		}
		if (point >= num)
		{
			return point <= num2;
		}
		return false;
	}

	public static uint PairingFunction(uint k1, uint k2)
	{
		return (uint)(0.5 * (double)(k1 + k2) * (double)(k1 + k2 + 1) + (double)k2);
	}
}
