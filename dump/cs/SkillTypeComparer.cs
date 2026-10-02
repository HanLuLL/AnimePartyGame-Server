using System.Collections.Generic;

public class SkillTypeComparer : IEqualityComparer<SkillType>
{
	public bool Equals(SkillType lhs, SkillType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(SkillType obj)
	{
		return (int)obj;
	}
}
