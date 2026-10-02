using System.Collections.Generic;

public class GoodsRefreshTypeComparer : IEqualityComparer<GoodsRefreshType>
{
	public bool Equals(GoodsRefreshType lhs, GoodsRefreshType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(GoodsRefreshType obj)
	{
		return (int)obj;
	}
}
