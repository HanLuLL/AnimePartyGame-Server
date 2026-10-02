using System;
using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

public class CustomEase
{
	private Vector2[] _points;

	private int _pointDensity;

	private static GPath helperPath = new GPath();

	public CustomEase(int pointDensity = 200)
	{
		_points = new Vector2[pointDensity + 1];
		_pointDensity = pointDensity;
	}

	public void Create(IEnumerable<GPathPoint> pathPoints)
	{
		helperPath.Create(pathPoints);
		for (int i = 0; i <= _pointDensity; i++)
		{
			Vector3 pointAt = helperPath.GetPointAt((float)i / (float)_pointDensity);
			_points[i] = pointAt;
		}
		_points[0] = Vector2.zero;
		_points[_pointDensity] = Vector2.one;
		Array.Sort(_points, (Vector2 p1, Vector2 p2) => p1.x.CompareTo(p2.x));
	}

	public float Evaluate(float time)
	{
		if (time <= 0f)
		{
			return 0f;
		}
		if (time >= 1f)
		{
			return 1f;
		}
		int num = 0;
		int num2 = _pointDensity;
		int num3 = 0;
		while (num != num2)
		{
			num3 = num + (int)((float)(num2 - num) / 2f);
			float x = _points[num3].x;
			if (time == x)
			{
				break;
			}
			if (time > x)
			{
				if (num == num3)
				{
					num3 = num2;
					break;
				}
				num = num3;
			}
			else
			{
				if (num2 == num3)
				{
					num3 = num;
					break;
				}
				num2 = num3;
			}
		}
		Vector2 vector = _points[num3];
		Vector2 vector2 = ((num3 != _pointDensity) ? _points[num3 + 1] : Vector2.one);
		float num4 = (vector2.y - vector.y) / (vector2.x - vector.x);
		if (float.IsNaN(num4))
		{
			num4 = 0f;
		}
		return vector.y + (time - vector.x) * num4;
	}
}
