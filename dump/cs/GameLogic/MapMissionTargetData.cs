using System.Collections.Generic;
using UI;
using party.model;

namespace GameLogic;

public class MapMissionTargetData
{
	public int MapMissionId;

	public readonly List<int> targetIds = new List<int>();

	public readonly int targetNum;

	public readonly int type;

	public PVEMissionInfoConfigure PVEMissionInfo => MapMissionId.GetPVEMissionInfoConfigure();

	public MapMissionTargetData(int _mapMissionId, MapMissionTarget targetData)
	{
		MapMissionId = _mapMissionId;
		targetIds.AddRange(targetData.TargetId);
		targetNum = targetData.TargetNum;
		type = (int)targetData.TargetType;
	}

	public virtual string GetTitle()
	{
		if (PVEMissionInfo.MissionTargetConfigs == null || PVEMissionInfo.MissionTargetConfigs.Count == 0)
		{
			return GetTitleByMissionTargetType();
		}
		return IPVEMissionTargetConfigs.GetTitleByMissionTargetConfigs(PVEMissionInfo);
	}

	public virtual string GetProgressDesc()
	{
		int num = ((PVEMissionInfo.MissionTargetConfigs == null || PVEMissionInfo.MissionTargetConfigs.Count == 0) ? GetTargetCountByMissionTargetType() : IPVEMissionTargetConfigs.GetTargetCountByMissionTargetConfigs(PVEMissionInfo));
		return $"[color=#66FF00]{num - targetNum}[/size][/color]\\{num}";
	}

	private string GetTitleByMissionTargetType()
	{
		if (type == 2 || type == 3)
		{
			return string.Format(PVEMissionInfo.MissionDescID.GetLocal(UIStringType.PVEMission), PVEMissionInfo.MissionParam[0]);
		}
		string characterName = CharacterHandle.GetCharacterName(targetIds[0]);
		if (type == 5)
		{
			return string.Format(PVEMissionInfo.MissionDescID.GetLocal(UIStringType.PVEMission), characterName);
		}
		return PVEMissionInfo.MissionDescID.GetLocal(UIStringType.PVEMission) + characterName;
	}

	private int GetTargetCountByMissionTargetType()
	{
		return PVEMissionInfo.MissionParam[0];
	}
}
