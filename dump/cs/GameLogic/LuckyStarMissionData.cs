using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;
using party.model;

namespace GameLogic;

public class LuckyStarMissionData
{
	public int MissionId { get; private set; }

	public int State { get; private set; }

	public List<int> Process { get; private set; }

	public LuckyStarBattleInfoConfigure Config { get; private set; }

	public LuckyStarMissionData(long playerId, LuckyStarMission mission)
	{
		MissionId = mission.DefId;
		Process = new List<int>(mission.Targets);
		State = (int)mission.LuckyStarMissionState;
		if (!StaticConfigure.LuckyStarBattle.InfoDict.TryGetValue(MissionId, out var value))
		{
			Debug.LogError($"通过任务Id:{MissionId}, 无法从 LuckyStarBattle.InfoDict中获取正确的任务配置");
		}
		else
		{
			Config = value;
		}
	}

	public async UniTask UpdateMission(long playerId, LuckyStarMission mission)
	{
		Process.Clear();
		Process.AddRange(mission.Targets);
		if (State == 1 && mission.LuckyStarMissionState == LuckyStarMission.Types.State.Complete)
		{
			State = (int)mission.LuckyStarMissionState;
			await SimpleSingletonProvider<UIManager>.inst.LuckyStarMission.TryShowFinishMission(playerId, this);
		}
		else
		{
			State = (int)mission.LuckyStarMissionState;
		}
	}

	public string GetMissionDesc()
	{
		return Config.DescId.GetLocal(UIStringType.LuckyStarBattle);
	}

	public string GetRewardDesc()
	{
		return Config.RewardDescId.GetLocal(UIStringType.LuckyStarBattle);
	}

	public string GetProgressDesc()
	{
		int num;
		int num2;
		if (MissionId == 41006)
		{
			num = 1;
			num2 = ((State != 2) ? 1 : 0);
		}
		else
		{
			num = Config.MissionParam[0];
			num2 = Process.GetSafeByIndex(0);
		}
		return $"[color=#66FF00]{num - num2}[/color]/{num}";
	}
}
