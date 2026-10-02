using UnityEngine;

namespace Tools;

public static class ComponentExt
{
	public static T CheckComponent<T>(this GameObject go) where T : Component
	{
		if (!go)
		{
			return null;
		}
		return go.GetComponent<T>() ?? go.AddComponent<T>();
	}
}
