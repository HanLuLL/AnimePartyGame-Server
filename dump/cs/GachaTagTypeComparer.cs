using System.Collections.Generic;

public class GachaTagTypeComparer : IEqualityComparer<GachaTagType>
{
	public bool Equals(GachaTagType lhs, GachaTagType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(GachaTagType obj)
	{
		return (int)obj;
	}
}
