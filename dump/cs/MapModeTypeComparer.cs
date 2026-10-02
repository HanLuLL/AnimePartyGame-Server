using System.Collections.Generic;

public class MapModeTypeComparer : IEqualityComparer<MapModeType>
{
	public bool Equals(MapModeType lhs, MapModeType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(MapModeType obj)
	{
		return (int)obj;
	}
}
