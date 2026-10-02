using System.Collections.Generic;

public class SkillEffectTypeComparer : IEqualityComparer<SkillEffectType>
{
	public bool Equals(SkillEffectType lhs, SkillEffectType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(SkillEffectType obj)
	{
		return (int)obj;
	}
}
