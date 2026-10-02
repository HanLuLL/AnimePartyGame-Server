using System.Collections.Generic;

public class FinishAchieveTypeComparer : IEqualityComparer<FinishAchieveType>
{
	public bool Equals(FinishAchieveType lhs, FinishAchieveType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(FinishAchieveType obj)
	{
		return (int)obj;
	}
}
