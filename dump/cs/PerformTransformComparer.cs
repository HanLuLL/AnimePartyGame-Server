using System.Collections.Generic;

public class PerformTransformComparer : IEqualityComparer<PerformTransform>
{
	public bool Equals(PerformTransform lhs, PerformTransform rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(PerformTransform obj)
	{
		return (int)obj;
	}
}
