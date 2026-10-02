using System.Collections.Generic;
using System.Text;

namespace Tools;

public static class SystemCollectionsExt
{
	public static string ToListString<T>(this IList<T> self)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("[");
		for (int i = 0; i < self.Count; i++)
		{
			stringBuilder.Append(self[i].ToString());
			if (i < self.Count - 1)
			{
				stringBuilder.Append(",");
			}
		}
		stringBuilder.Append("]");
		return stringBuilder.ToString();
	}
}
