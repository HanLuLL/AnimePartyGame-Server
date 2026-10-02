using System.Collections.Generic;

public class SummonTypeComparer : IEqualityComparer<SummonType>
{
	public bool Equals(SummonType lhs, SummonType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(SummonType obj)
	{
		return (int)obj;
	}
}
