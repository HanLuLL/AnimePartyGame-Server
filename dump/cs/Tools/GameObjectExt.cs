using UnityEngine;

namespace Tools;

public static class GameObjectExt
{
	public static void SetActiveEx(this GameObject go, bool active)
	{
		if (!(go == null) && go.activeSelf != active)
		{
			go.SetActive(active);
		}
	}
}
