using System.Collections.Generic;

public class ServerErrorShowTypeComparer : IEqualityComparer<ServerErrorShowType>
{
	public bool Equals(ServerErrorShowType lhs, ServerErrorShowType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(ServerErrorShowType obj)
	{
		return (int)obj;
	}
}
