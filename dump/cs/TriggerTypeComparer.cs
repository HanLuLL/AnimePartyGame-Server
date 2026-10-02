using System.Collections.Generic;

public class TriggerTypeComparer : IEqualityComparer<TriggerType>
{
	public bool Equals(TriggerType lhs, TriggerType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(TriggerType obj)
	{
		return (int)obj;
	}
}
