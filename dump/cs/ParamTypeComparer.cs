using System.Collections.Generic;

public class ParamTypeComparer : IEqualityComparer<ParamType>
{
	public bool Equals(ParamType lhs, ParamType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(ParamType obj)
	{
		return (int)obj;
	}
}
