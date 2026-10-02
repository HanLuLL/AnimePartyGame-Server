using UnityEngine;

namespace FairyGUI;

public class MeshColliderHitTest : ColliderHitTest
{
	public Vector2 lastHit;

	public MeshColliderHitTest(MeshCollider collider)
	{
		base.collider = (Collider)(object)collider;
	}

	public override bool HitTest(Rect contentRect, Vector2 localPoint)
	{
		if (!HitTestContext.GetRaycastHitFromCache(HitTestContext.camera, out var hit))
		{
			return false;
		}
		if ((Object)(object)((RaycastHit)(ref hit)).collider != (Object)(object)collider)
		{
			return false;
		}
		lastHit = new Vector2(((RaycastHit)(ref hit)).textureCoord.x * contentRect.width, (1f - ((RaycastHit)(ref hit)).textureCoord.y) * contentRect.height);
		HitTestContext.direction = Vector3.back;
		HitTestContext.worldPoint = StageCamera.main.ScreenToWorldPoint(new Vector2(lastHit.x, (float)Screen.height - lastHit.y));
		return true;
	}
}
