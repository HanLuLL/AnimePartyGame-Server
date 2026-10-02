using System.Collections.Generic;
using party.model;

namespace GameLogic;

public class ClueMissionTargetData
{
	private readonly PVEMissionClueConfigure config;

	public readonly List<int> targetIds = new List<int>();

	public int type { get; }

	public int targetNum { get; private set; }

	public int TargetCount { get; }

	public int CurrentCount => TargetCount - targetNum;

	public ClueMissionTargetData(PVEMissionClueConfigure clueConfig, MapMissionTarget target)
	{
		config = clueConfig;
		type = (int)target.TargetType;
		targetNum = target.TargetNum;
		targetIds.AddRange(target.TargetId);
		TargetCount = IPVEMissionTargetConfigs.GetTargetCountByMissionTargetConfigs(config);
	}

	public string GetTitle()
	{
		return IPVEMissionTargetConfigs.GetTitleByMissionTargetConfigs(config);
	}

	public string GetProgressDesc()
	{
		return $"[color=#66FF00]{CurrentCount}[/size][/color]\\{TargetCount}";
	}
}
