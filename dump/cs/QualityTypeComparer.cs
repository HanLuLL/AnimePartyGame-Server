using System.Collections.Generic;

public class QualityTypeComparer : IEqualityComparer<QualityType>
{
	public bool Equals(QualityType lhs, QualityType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(QualityType obj)
	{
		return (int)obj;
	}
}
