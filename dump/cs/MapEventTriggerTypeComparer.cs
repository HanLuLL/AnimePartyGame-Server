using System.Collections.Generic;

public class MapEventTriggerTypeComparer : IEqualityComparer<MapEventTriggerType>
{
	public bool Equals(MapEventTriggerType lhs, MapEventTriggerType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(MapEventTriggerType obj)
	{
		return (int)obj;
	}
}
