using System.Collections.Generic;

public class ChoosingTimeTypeComparer : IEqualityComparer<ChoosingTimeType>
{
	public bool Equals(ChoosingTimeType lhs, ChoosingTimeType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(ChoosingTimeType obj)
	{
		return (int)obj;
	}
}
