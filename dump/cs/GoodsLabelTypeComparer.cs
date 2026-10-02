using System.Collections.Generic;

public class GoodsLabelTypeComparer : IEqualityComparer<GoodsLabelType>
{
	public bool Equals(GoodsLabelType lhs, GoodsLabelType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(GoodsLabelType obj)
	{
		return (int)obj;
	}
}
