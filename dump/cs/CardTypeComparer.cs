using System.Collections.Generic;

public class CardTypeComparer : IEqualityComparer<CardType>
{
	public bool Equals(CardType lhs, CardType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(CardType obj)
	{
		return (int)obj;
	}
}
