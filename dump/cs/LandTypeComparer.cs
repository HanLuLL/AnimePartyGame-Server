using System.Collections.Generic;

public class LandTypeComparer : IEqualityComparer<LandType>
{
	public bool Equals(LandType lhs, LandType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(LandType obj)
	{
		return (int)obj;
	}
}
