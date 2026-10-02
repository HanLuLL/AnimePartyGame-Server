using UI;
using party.model;

namespace GameLogic;

public class MapMissionTargetData_320650 : MapMissionTargetData
{
	public MapMissionTargetData_320650(int _mapMissionId, MapMissionTarget targetData)
		: base(_mapMissionId, targetData)
	{
	}

	public override string GetTitle()
	{
		return string.Format(base.PVEMissionInfo.MissionDescID.GetLocal(UIStringType.PVEMission), targetNum);
	}
}
