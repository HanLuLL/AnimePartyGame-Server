using System.Collections.Generic;

public class ItemUIMenuSubTypeComparer : IEqualityComparer<ItemUIMenuSubType>
{
	public bool Equals(ItemUIMenuSubType lhs, ItemUIMenuSubType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(ItemUIMenuSubType obj)
	{
		return (int)obj;
	}
}
