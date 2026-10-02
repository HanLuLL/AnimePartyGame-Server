using System.Collections.Generic;

public class ItemOutputTypeComparer : IEqualityComparer<ItemOutputType>
{
	public bool Equals(ItemOutputType lhs, ItemOutputType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(ItemOutputType obj)
	{
		return (int)obj;
	}
}
