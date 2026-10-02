using System.Collections.Generic;
using UnityEngine;

namespace Tools;

public static class CollectionExtensions
{
	public static T Get<T>(this IList<T> list, int index, T defaultValue = default(T))
	{
		if (list == null)
		{
			Debug.LogError("list is null");
			return defaultValue;
		}
		if (index < 0 || index >= list.Count)
		{
			Debug.LogError("index out of range");
			return defaultValue;
		}
		return list[index];
	}
}
