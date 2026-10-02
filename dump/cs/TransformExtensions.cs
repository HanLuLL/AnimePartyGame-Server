using UnityEngine;

public static class TransformExtensions
{
	public static Transform DeepFind(this Transform parent, string targetName)
	{
		Transform transform = parent.Find(targetName);
		if (transform != null)
		{
			return transform;
		}
		for (int i = 0; i < parent.childCount; i++)
		{
			transform = parent.GetChild(i).DeepFind(targetName);
			if (transform != null)
			{
				return transform;
			}
		}
		return transform;
	}
}
