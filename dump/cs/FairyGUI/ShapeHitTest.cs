using UnityEngine;

namespace FairyGUI;

public class ShapeHitTest : IHitTest
{
	public DisplayObject shape;

	public ShapeHitTest(DisplayObject obj)
	{
		shape = obj;
	}

	public bool HitTest(Rect contentRect, Vector2 localPoint)
	{
		if (shape.graphics == null)
		{
			return false;
		}
		if (shape.parent != null)
		{
			localPoint = shape.parent.TransformPoint(localPoint, shape);
		}
		if (!(shape.graphics.meshFactory is IHitTest hitTest))
		{
			return false;
		}
		return hitTest.HitTest(contentRect, localPoint);
	}
}
