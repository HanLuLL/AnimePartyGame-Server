using System.Collections.Generic;

public class ItemConsumeTypeComparer : IEqualityComparer<ItemConsumeType>
{
	public bool Equals(ItemConsumeType lhs, ItemConsumeType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(ItemConsumeType obj)
	{
		return (int)obj;
	}
}
