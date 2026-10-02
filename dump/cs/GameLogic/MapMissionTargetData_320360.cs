using UI;
using party.model;

namespace GameLogic;

public class MapMissionTargetData_320360 : MapMissionTargetData
{
	public MapMissionTargetData_320360(int _mapMissionId, MapMissionTarget targetData)
		: base(_mapMissionId, targetData)
	{
	}

	public override string GetTitle()
	{
		string characterName = CharacterHandle.GetCharacterName(targetIds[0]);
		string characterName2 = CharacterHandle.GetCharacterName(targetIds[1]);
		return string.Format(base.PVEMissionInfo.MissionDescID.GetLocal(UIStringType.PVEMission), characterName, characterName2);
	}
}
