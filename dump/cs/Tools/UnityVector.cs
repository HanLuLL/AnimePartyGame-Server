using UnityEngine;

namespace Tools;

public static class UnityVector
{
	public static Vector2 ToVector2(this Vector3 v3)
	{
		return new Vector2(v3.x, v3.y);
	}

	public static Vector3 ToVector3(this Vector2 v2)
	{
		return new Vector3(v2.x, v2.y, 0f);
	}

	public static Vector3 Add(this Vector3 v3, Vector2 v2)
	{
		v3.x += v2.x;
		v3.y += v2.y;
		return v3;
	}

	public static Vector3 ReverseX(this Vector3 v3)
	{
		return new Vector3(0f - v3.x, v3.y, v3.z);
	}

	public static Vector3 ReverseY(this Vector3 v3)
	{
		return new Vector3(v3.x, 0f - v3.y, v3.z);
	}

	public static Vector3 ReverseZ(this Vector3 v3)
	{
		return new Vector3(v3.x, v3.y, 0f - v3.z);
	}

	public static Vector2 ReverseX(this Vector2 v2)
	{
		return new Vector2(0f - v2.x, v2.y);
	}

	public static Vector2 ReverseY(this Vector2 v2)
	{
		return new Vector2(v2.x, 0f - v2.y);
	}

	public static Vector2 Reverse(this Vector2 v2)
	{
		return new Vector2(0f - v2.x, 0f - v2.y);
	}

	public static bool Equals(this Vector2 self, Vector2 v2)
	{
		if (self.x.Equals(v2.x))
		{
			return self.y.Equals(v2.y);
		}
		return false;
	}

	public static bool Equals(this Vector3 self, Vector3 v3)
	{
		if (self.x.Equals(v3.x) && self.y.Equals(v3.y))
		{
			return self.z.Equals(v3.z);
		}
		return false;
	}

	public static Vector3 Bezier(float t, Vector3 a, Vector3 b, Vector3 c)
	{
		Vector3 a2 = Vector3.Lerp(a, b, t);
		Vector3 b2 = Vector3.Lerp(b, c, t);
		return Vector3.Lerp(a2, b2, t);
	}
}
