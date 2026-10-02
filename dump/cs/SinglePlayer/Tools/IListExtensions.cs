using System.Collections.Generic;
using Google.Protobuf.Collections;

namespace SinglePlayer.Tools;

public static class IListExtensions
{
	public static bool ContainsAny(this RepeatedField<int> source, IList<int> targets)
	{
		if (source == null)
		{
			return false;
		}
		if (targets == null)
		{
			return false;
		}
		foreach (int item in source)
		{
			if (targets.Contains(item))
			{
				return true;
			}
		}
		return false;
	}
}
