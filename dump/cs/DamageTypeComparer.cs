using System.Collections.Generic;

public class DamageTypeComparer : IEqualityComparer<DamageType>
{
	public bool Equals(DamageType lhs, DamageType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(DamageType obj)
	{
		return (int)obj;
	}
}
