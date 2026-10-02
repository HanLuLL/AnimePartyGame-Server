using System.Collections.Generic;

public class GachaItemSortTypeComparer : IEqualityComparer<GachaItemSortType>
{
	public bool Equals(GachaItemSortType lhs, GachaItemSortType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(GachaItemSortType obj)
	{
		return (int)obj;
	}
}
