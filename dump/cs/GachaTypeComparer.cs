using System.Collections.Generic;

public class GachaTypeComparer : IEqualityComparer<GachaType>
{
	public bool Equals(GachaType lhs, GachaType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(GachaType obj)
	{
		return (int)obj;
	}
}
