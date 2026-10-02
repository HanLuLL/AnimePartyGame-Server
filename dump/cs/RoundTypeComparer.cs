using System.Collections.Generic;

public class RoundTypeComparer : IEqualityComparer<RoundType>
{
	public bool Equals(RoundType lhs, RoundType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(RoundType obj)
	{
		return (int)obj;
	}
}
