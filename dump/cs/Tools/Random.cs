using System.Collections.Generic;
using UnityEngine;

namespace Tools;

public static class Random
{
	public static int RandomByWeights(List<int> weights)
	{
		if (weights.Count == 0)
		{
			return -1;
		}
		int num = 0;
		foreach (int weight in weights)
		{
			num += weight;
		}
		float num2 = UnityEngine.Random.Range(0f, (float)num * 1f);
		int num3 = 0;
		for (int i = 0; i < weights.Count; i++)
		{
			num3 += weights[i];
			if (num2 < (float)num3)
			{
				return i;
			}
		}
		return 0;
	}
}
