using UnityEngine;

namespace FairyGUI;

public class RectHitTest : IHitTest
{
	public Rect rect;

	public bool HitTest(Rect contentRect, Vector2 localPoint)
	{
		return rect.Contains(localPoint);
	}
}
