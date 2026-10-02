using System.Collections.Generic;

public class BattlePassUITypeComparer : IEqualityComparer<BattlePassUIType>
{
	public bool Equals(BattlePassUIType lhs, BattlePassUIType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(BattlePassUIType obj)
	{
		return (int)obj;
	}
}
