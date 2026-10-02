using System.Collections.Generic;

public class ShopTabTypeComparer : IEqualityComparer<ShopTabType>
{
	public bool Equals(ShopTabType lhs, ShopTabType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(ShopTabType obj)
	{
		return (int)obj;
	}
}
