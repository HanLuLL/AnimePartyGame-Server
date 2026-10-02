using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SinglePlayer.GamePlay.Map;

namespace SinglePlayer.GamePlay;

public class BoardMissionManager
{
	private readonly MissionRewardDropTable _missionRewardDrop = new MissionRewardDropTable();

	private (MissionSettleType, List<int>) _missionRewards;

	public (MissionSettleType, List<int>) MissionRewards => _missionRewards;

	public void Initialize()
	{
		Game.GetModel<GameData>().heroProperty.Gold.AddListener(OnGoldChange, excuteImmediately: false);
	}

	public void Dispose()
	{
		Game.GetModel<GameData>().heroProperty.Gold.RemoveListener(OnGoldChange);
	}

	public MapMission GetCurrentMissionData()
	{
		List<MapMission> mapMissionData = Game.GetModel<GameData>().MapData.MapMissionData;
		int value = Game.GetModel<GameData>().GameProgress.Value;
		for (int i = 0; i < mapMissionData.Count; i++)
		{
			if (mapMissionData[i].Deadline >= value && mapMissionData[i].Status != MapMissionStatus.Success)
			{
				return mapMissionData[i];
			}
		}
		return null;
	}

	public int GetMissionScore()
	{
		int num = 0;
		int value = Game.GetModel<GameData>().GameProgress.Value;
		foreach (MapMission mapMissionDatum in Game.GetModel<GameData>().MapData.MapMissionData)
		{
			if (value >= mapMissionDatum.Deadline)
			{
				if (mapMissionDatum.Status == MapMissionStatus.Success)
				{
					num += mapMissionDatum.MissionTarget;
				}
				if (mapMissionDatum.Status == MapMissionStatus.Failure)
				{
					num += mapMissionDatum.MissionProgress;
					break;
				}
			}
		}
		return num;
	}

	public bool CanCreateNextMonster()
	{
		return false;
	}

	public void CreateNextMonster()
	{
	}

	private void OnGoldChange(int pre, int cur)
	{
		if (GetCurrentMissionData() is IMapMission_Gold mapMission_Gold)
		{
			mapMission_Gold.UpdateGoldProgress(cur - pre);
		}
	}

	public async UniTask TryStartMission()
	{
		MapMission currentMission = GetCurrentMissionData();
		if (currentMission != null && currentMission.Status == MapMissionStatus.None)
		{
			await currentMission.StartMission();
		}
		Game.GetModel<GlobalSignal>().MissionProgressChange.Dispatch(currentMission.MissionProgress, currentMission.MissionTarget);
	}

	public bool SettleMission()
	{
		MapMission currentMissionData = Game.GetSystem<BoardManager>().missionManager.GetCurrentMissionData();
		if (currentMissionData == null || currentMissionData.GetStatus() != MapMissionStatus.Success)
		{
			return false;
		}
		if (currentMissionData.MissionConfig.CardReward.Count != 0)
		{
			_missionRewards.Item1 = MissionSettleType.Card;
			_missionRewards.Item2 = _missionRewardDrop.GetReward(3, MissionSettleType.Card, currentMissionData.MissionConfig.CardReward);
			return true;
		}
		if (currentMissionData.MissionConfig.RelicReward.Count != 0)
		{
			_missionRewards.Item1 = MissionSettleType.Relic;
			_missionRewards.Item2 = _missionRewardDrop.GetReward(3, MissionSettleType.Relic, currentMissionData.MissionConfig.RelicReward);
			return true;
		}
		return false;
	}
}
