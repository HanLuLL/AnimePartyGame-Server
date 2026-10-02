using System.Collections.Generic;

public class ItemTypeComparer : IEqualityComparer<ItemType>
{
	public bool Equals(ItemType lhs, ItemType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(ItemType obj)
	{
		return (int)obj;
	}
}
