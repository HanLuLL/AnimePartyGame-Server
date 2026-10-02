using UnityEngine;

namespace Tools;

public static class TransformExt
{
	public static void CopyFrom(this Transform self, Transform target)
	{
		self.position = target.position;
		self.localScale = target.localScale;
	}

	public static void CopyScale(this Transform self, Transform target)
	{
		self.localScale = target.localScale;
	}

	public static void CopyPostion(this Transform self, Transform target)
	{
		self.position = target.position;
	}
}
