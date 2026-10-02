using System.Collections.Generic;

public class MonsterTypeComparer : IEqualityComparer<MonsterType>
{
	public bool Equals(MonsterType lhs, MonsterType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(MonsterType obj)
	{
		return (int)obj;
	}
}
