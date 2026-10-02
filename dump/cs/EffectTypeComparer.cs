using System.Collections.Generic;

public class EffectTypeComparer : IEqualityComparer<EffectType>
{
	public bool Equals(EffectType lhs, EffectType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(EffectType obj)
	{
		return (int)obj;
	}
}
