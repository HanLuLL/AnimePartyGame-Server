using System.Collections.Generic;

public class FashionTypeComparer : IEqualityComparer<FashionType>
{
	public bool Equals(FashionType lhs, FashionType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(FashionType obj)
	{
		return (int)obj;
	}
}
