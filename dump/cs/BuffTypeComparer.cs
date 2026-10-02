using System.Collections.Generic;

public class BuffTypeComparer : IEqualityComparer<BuffType>
{
	public bool Equals(BuffType lhs, BuffType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(BuffType obj)
	{
		return (int)obj;
	}
}
