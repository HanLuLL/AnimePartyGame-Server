using System.Collections.Generic;

public class CardTargetTypeComparer : IEqualityComparer<CardTargetType>
{
	public bool Equals(CardTargetType lhs, CardTargetType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(CardTargetType obj)
	{
		return (int)obj;
	}
}
