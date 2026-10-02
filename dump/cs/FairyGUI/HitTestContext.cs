using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

public class HitTestContext
{
	public static Vector3 screenPoint;

	public static Vector3 worldPoint;

	public static Vector3 direction;

	public static bool forTouch;

	public static Camera camera;

	public static int layerMask = -1;

	public static float maxDistance = float.PositiveInfinity;

	public static Camera cachedMainCamera;

	private static Dictionary<Camera, RaycastHit?> raycastHits = new Dictionary<Camera, RaycastHit?>();

	public static bool GetRaycastHitFromCache(Camera camera, out RaycastHit hit)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		if (!raycastHits.TryGetValue(camera, out var value))
		{
			if (Physics.Raycast(camera.ScreenPointToRay(screenPoint), ref hit, maxDistance, layerMask))
			{
				raycastHits[camera] = hit;
				return true;
			}
			raycastHits[camera] = null;
			return false;
		}
		if (!value.HasValue)
		{
			hit = default(RaycastHit);
			return false;
		}
		hit = value.Value;
		return true;
	}

	public static void CacheRaycastHit(Camera camera, ref RaycastHit hit)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		raycastHits[camera] = hit;
	}

	public static void ClearRaycastHitCache()
	{
		raycastHits.Clear();
	}
}
