using System.Collections.Generic;

public class MerchandiselTypeComparer : IEqualityComparer<MerchandiselType>
{
	public bool Equals(MerchandiselType lhs, MerchandiselType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(MerchandiselType obj)
	{
		return (int)obj;
	}
}
