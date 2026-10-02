using System.Collections.Generic;

public class ThinkTypeComparer : IEqualityComparer<ThinkType>
{
	public bool Equals(ThinkType lhs, ThinkType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(ThinkType obj)
	{
		return (int)obj;
	}
}
