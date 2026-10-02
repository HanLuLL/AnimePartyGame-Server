using System.Collections.Generic;

public class DivinationTargetTypeComparer : IEqualityComparer<DivinationTargetType>
{
	public bool Equals(DivinationTargetType lhs, DivinationTargetType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(DivinationTargetType obj)
	{
		return (int)obj;
	}
}
