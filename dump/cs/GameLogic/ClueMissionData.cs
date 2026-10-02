using System.Collections.Generic;
using UnityEngine;
using party.model;

namespace GameLogic;

public class ClueMissionData
{
	public readonly List<ClueMissionTargetData> targetsData = new List<ClueMissionTargetData>();

	public int ClueId { get; private set; }

	public Clue serverClue { get; private set; }

	public PVEMissionClueConfigure Config { get; private set; }

	public bool IsCompleted
	{
		get
		{
			Clue clue = serverClue;
			if (clue == null)
			{
				return false;
			}
			return clue.State == Clue.Types.State.Complete;
		}
	}

	public ClueMissionTargetData PrimaryTarget
	{
		get
		{
			if (targetsData.Count <= 0)
			{
				return null;
			}
			return targetsData[0];
		}
	}

	public void UpdateClue(Clue clue)
	{
		serverClue = clue;
		ClueId = clue.ClueId;
		targetsData.Clear();
		if (StaticConfigure.PVEMission == null || !StaticConfigure.PVEMission.ClueDict.TryGetValue(ClueId, out var value))
		{
			Config = null;
			Debug.LogError($"无法通过线索Id:{ClueId}获取 PVEMission.Clue 配置");
			return;
		}
		Config = value;
		foreach (MapMissionTarget target in clue.Targets)
		{
			targetsData.Add(new ClueMissionTargetData(Config, target));
		}
	}
}
