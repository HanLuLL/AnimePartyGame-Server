using System.Collections.Generic;
using Core.Net;
using Cysharp.Threading.Tasks;
using GameLogic.Data;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class ActivityPassLogic : IRPCSync
{
	public ActivityPassSignal signal = new ActivityPassSignal();

	public ActivityPassData ActivityPassData;

	public Dictionary<int, int> PassGearData = new Dictionary<int, int>();

	private Dictionary<int, int> activityPass;

	public ActivityPassLogic()
	{
		activityPass = new Dictionary<int, int>();
		foreach (BattlePassNewInfoConfigure info in StaticConfigure.BattlePassNew.Infos)
		{
			foreach (BattlePassNewInfoConfigureItem battlePassNewInfoConfigureItem in info.BattlePassNewInfoConfigureItems)
			{
				if (battlePassNewInfoConfigureItem.ActivityId > 0)
				{
					activityPass.Add(battlePassNewInfoConfigureItem.ActivityId, info.Id);
					break;
				}
			}
		}
	}

	public void InitFromServer(MapField<int, ActivityPass> playerBattlePassNew)
	{
		PassGearData.Clear();
		foreach (KeyValuePair<int, ActivityPass> item in playerBattlePassNew)
		{
			PassGearData.Add(item.Value.DefId, item.Value.Gear);
		}
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.ActivityPassGearChangeS2C.OnActivityPassGearChangeS2CServerCallBackAsync = UpdatePassGear;
		MonoSingletonProvider<NetManager>.inst.RPC.GetActivityPassRewardS2C.OnGetActivityPassRewardS2CServerCallBackAsync = OnReceiveReward;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.ActivityPassGearChangeS2C.OnActivityPassGearChangeS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.GetActivityPassRewardS2C.OnGetActivityPassRewardS2CServerCallBackAsync = null;
	}

	public void ChangePass(int passId)
	{
		if (ActivityPassData == null || ActivityPassData.PassInfoConfig.Id != passId)
		{
			ActivityPassData = new ActivityPassData(passId);
		}
		RefreshInfo();
	}

	public void RefreshInfo()
	{
		if (ActivityPassData != null)
		{
			int value = 1;
			if (!PassGearData.TryGetValue(ActivityPassData.PassInfoConfig.Id, out value))
			{
				Debug.LogError($"ServerData PassData not found {ActivityPassData.PassInfoConfig.Id}");
			}
			ActivityPassData.RefreshInfo(value);
		}
	}

	public int GetPassIdByActivityId(int activityId)
	{
		return activityPass[activityId];
	}

	public async UniTask UpdatePassGear(ActivityPassGearChangeS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			PassGearData[model.DefId] = model.Gear;
			if (ActivityPassData != null && ActivityPassData.PassInfoConfig.Id == model.DefId)
			{
				RefreshInfo();
				signal.updateGear.Dispatch();
			}
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestReceiveReward(int passId)
	{
		GetActivityPassRewardC2S getActivityPassRewardC2S = new GetActivityPassRewardC2S();
		getActivityPassRewardC2S.DefId = passId;
		foreach (ActivityPassItemData value in ActivityPassData.PassItemData.Values)
		{
			if (ActivityPassData.PassGear == 1 && value.MissionData.Status == 4)
			{
				getActivityPassRewardC2S.MissionIds.Add(value.MissionData._Id);
			}
			else if (ActivityPassData.PassGear == 2)
			{
				int status = value.MissionData.Status;
				if (status == 2 || status == 4)
				{
					getActivityPassRewardC2S.MissionIds.Add(value.MissionData._Id);
				}
			}
		}
		return MonoSingletonProvider<NetManager>.inst.RPC.GetActivityPassRewardC2S.GetActivityPassRewardC2SCall(getActivityPassRewardC2S);
	}

	public async UniTask OnReceiveReward(GetActivityPassRewardS2C model, int errId, bool isDispatch)
	{
		if (errId != 0)
		{
			return;
		}
		foreach (int missionId in model.MissionIds)
		{
			MissionData missionData = SimpleSingletonProvider<GameLogicManager>.inst.task.TryGetMissionById(missionId);
			if (missionData != null)
			{
				int status = ((model.Gear > 1) ? 3 : 5);
				missionData.UpdateData(missionData.AchieveProgress, status);
			}
		}
		if (ActivityPassData != null && ActivityPassData.PassInfoConfig.Id == model.DefId)
		{
			RefreshInfo();
			SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.activityStatus.Dispatch(ActivityPassData.ActivityId);
			signal.updateTask.Dispatch();
		}
		await UniTask.CompletedTask;
	}
}
