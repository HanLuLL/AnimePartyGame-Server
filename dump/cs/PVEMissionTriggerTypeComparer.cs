using System.Collections.Generic;

public class PVEMissionTriggerTypeComparer : IEqualityComparer<PVEMissionTriggerType>
{
	public bool Equals(PVEMissionTriggerType lhs, PVEMissionTriggerType rhs)
	{
		return lhs == rhs;
	}

	public int GetHashCode(PVEMissionTriggerType obj)
	{
		return (int)obj;
	}
}
