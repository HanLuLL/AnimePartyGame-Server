using UnityEngine;

namespace FairyGUI;

public class ColliderHitTest : IHitTest
{
	public Collider collider;

	public virtual bool HitTest(Rect contentRect, Vector2 localPoint)
	{
		if (!HitTestContext.GetRaycastHitFromCache(HitTestContext.camera, out var hit))
		{
			return false;
		}
		if ((Object)(object)((RaycastHit)(ref hit)).collider != (Object)(object)collider)
		{
			return false;
		}
		return true;
	}
}
