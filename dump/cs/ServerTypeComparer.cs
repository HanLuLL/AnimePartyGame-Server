using System.Collections.Generic;
using Core.Net;

public class ServerTypeComparer : IEqualityComparer<ServerType>
{
	public bool Equals(ServerType lhs, ServerType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(ServerType obj)
	{
		return (int)obj;
	}
}
