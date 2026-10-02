using System.Collections.Generic;

public class ServerErrorDealTypeComparer : IEqualityComparer<ServerErrorDealType>
{
	public bool Equals(ServerErrorDealType lhs, ServerErrorDealType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(ServerErrorDealType obj)
	{
		return (int)obj;
	}
}
