using System.Collections.Generic;

public class BuffRoundCountTypeComparer : IEqualityComparer<BuffRoundCountType>
{
	public bool Equals(BuffRoundCountType lhs, BuffRoundCountType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(BuffRoundCountType obj)
	{
		return (int)obj;
	}
}
