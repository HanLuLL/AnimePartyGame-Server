using System;
using UnityEngine;

namespace Tools;

public static class CardHelper
{
	public const float OriginalSize = 1f;

	public const float LookScale = 1.6f;

	private const float angle = 10f;

	private const float maxAngle = 60f;

	public static (float, float) CalculatePosition(Vector2 centerPoint, float CenterRadius, float rotation)
	{
		float f = (float)Math.PI / 2f - rotation * ((float)Math.PI / 180f);
		float item = centerPoint.x + CenterRadius * Mathf.Cos(f);
		float item2 = centerPoint.y - CenterRadius * Mathf.Sin(f);
		return (item, item2);
	}

	public static float CalculateRotation(float index, int cardCount)
	{
		if (cardCount > 0 && cardCount <= 6)
		{
			return (float)(-(cardCount - 1)) * 0.5f * 10f + index * 10f;
		}
		if (cardCount > 6)
		{
			return -30f + index * (60f / (float)(cardCount - 1));
		}
		return 0f;
	}
}
