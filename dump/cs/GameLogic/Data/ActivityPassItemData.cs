using Tools;
using UnityEngine;

namespace GameLogic.Data;

public class ActivityPassItemData
{
	public BattlePassNewInfoConfigureItem PassInfoConfig;

	public MissionDataConfigure MissionTaskConfig;

	public MissionData MissionData;

	public ActivityPassItemReward FreeReward;

	public ActivityPassItemReward PremiumReward;

	public ActivityPassData ActivityPassData { get; }

	public ActivityPassItemData(BattlePassNewInfoConfigureItem _config, ActivityPassData data)
	{
		ActivityPassData = data;
		if (_config == null)
		{
			Debug.LogError("invalid pass Item");
			return;
		}
		PassInfoConfig = _config;
		int taskId = PassInfoConfig.TaskId;
		if (!StaticConfigure.Mission.DataDict.TryGetValue(taskId, out MissionTaskConfig))
		{
			Debug.LogError($"invalid pass[{ActivityPassData.PassInfoConfig.Id}] mission id {taskId}");
			return;
		}
		FreeReward = new ActivityPassItemReward(PassInfoConfig.FreeRewards, this);
		PremiumReward = new ActivityPassItemReward(PassInfoConfig.PremiumRewards, this);
	}

	public void RefreshInfo()
	{
		MissionData = SimpleSingletonProvider<GameLogicManager>.inst.task.TryGetMissionById(MissionTaskConfig.Id);
		if (MissionData != null)
		{
			FreeReward.RefreshInfo(MissionData, _isPremium: false);
			PremiumReward.RefreshInfo(MissionData, _isPremium: true);
		}
	}
}
