using System.Collections.Generic;

public class WayTypeComparer : IEqualityComparer<WayType>
{
	public bool Equals(WayType lhs, WayType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(WayType obj)
	{
		return (int)obj;
	}
}
