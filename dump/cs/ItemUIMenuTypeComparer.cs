using System.Collections.Generic;

public class ItemUIMenuTypeComparer : IEqualityComparer<ItemUIMenuType>
{
	public bool Equals(ItemUIMenuType lhs, ItemUIMenuType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(ItemUIMenuType obj)
	{
		return (int)obj;
	}
}
