using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools;

public static class SafeListAccessExtensions
{
	public static T GetSafeByIndex<T>(this IList<T> list, int index)
	{
		if (list == null)
		{
			Debug.LogError($"列表为 null，无法访问索引 {index}");
			return default(T);
		}
		if (index < 0 || index >= list.Count)
		{
			Debug.LogError($"索引越界：index={index}, count={list.Count}");
			return default(T);
		}
		return list[index];
	}

	public static IList<T> RandomTake<T>(this IList<T> source, int count)
	{
		if (source == null)
		{
			throw new ArgumentNullException("source");
		}
		if (count <= 0)
		{
			return new List<T>();
		}
		count = Mathf.Min(count, source.Count);
		List<T> list = new List<T>(source);
		for (int i = 0; i < count; i++)
		{
			int num = UnityEngine.Random.Range(i, list.Count);
			int index = i;
			List<T> list2 = list;
			int index2 = num;
			T val = list[num];
			T val2 = list[i];
			T val3 = (list[index] = val);
			val3 = (list2[index2] = val2);
		}
		return list.GetRange(0, count);
	}
}
