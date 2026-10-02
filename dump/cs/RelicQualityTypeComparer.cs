using System.Collections.Generic;

public class RelicQualityTypeComparer : IEqualityComparer<RelicQualityType>
{
	public bool Equals(RelicQualityType lhs, RelicQualityType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(RelicQualityType obj)
	{
		return (int)obj;
	}
}
