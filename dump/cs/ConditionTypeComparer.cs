using System.Collections.Generic;

public class ConditionTypeComparer : IEqualityComparer<ConditionType>
{
	public bool Equals(ConditionType lhs, ConditionType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(ConditionType obj)
	{
		return (int)obj;
	}
}
