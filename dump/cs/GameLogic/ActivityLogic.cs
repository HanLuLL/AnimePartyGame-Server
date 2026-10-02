using System.Collections.Generic;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class ActivityLogic : IRPCSync, IReadPoint
{
	public readonly Dictionary<int, ActivityBaseData> activityDict = new Dictionary<int, ActivityBaseData>();

	public readonly ActivitySignal signal = new ActivitySignal();

	private int _ActivityId;

	private int _TaskId;

	public void InitFromServer(Player player)
	{
		RepeatedField<ActivityInfo> activityTask = player.ActivityTask;
		MapField<int, ScratchCardRecord> scratchCard = player.ScratchCard;
		MapField<int, LightGift> lightGift = player.LightGift;
		MapField<int, FlipCardActivity> flipCard = player.FlipCard;
		activityDict.Clear();
		foreach (KeyValuePair<int, LightGift> item in lightGift)
		{
			int key = item.Key;
			LightGift value = item.Value;
			GetLightGiftActivityData(key).UpdateGift(value);
		}
		foreach (KeyValuePair<int, ScratchCardRecord> item2 in scratchCard)
		{
			int key2 = item2.Key;
			ScratchCardRecord value2 = item2.Value;
			GetScratchOffActivityData(key2).UpdateDataByServer(value2);
		}
		foreach (ActivityInfo item3 in activityTask)
		{
			GetTaskActivityData(item3.InfoId)?.UpdateTaskInfo(item3);
		}
		foreach (KeyValuePair<int, FlipCardActivity> item4 in flipCard)
		{
			if (GetTaskActivityData(item4.Key) is BingoActivityData bingoActivityData)
			{
				bingoActivityData.InitDataByServer(item4.Value);
			}
		}
	}

	public ScratchOffActivityData GetScratchOffActivityData(int activityId)
	{
		if (!activityDict.TryGetValue(activityId, out var value))
		{
			value = new ScratchOffActivityData(activityId);
			activityDict.Add(activityId, value);
		}
		return value.child<ScratchOffActivityData>();
	}

	public LightActivityData GetLightGiftActivityData(int activityId)
	{
		if (!activityDict.TryGetValue(activityId, out var value))
		{
			value = new LightActivityData(activityId);
			activityDict.Add(activityId, value);
		}
		return value.child<LightActivityData>();
	}

	public TaskActivityData GetTaskActivityData(int activityId)
	{
		if (!activityDict.TryGetValue(activityId, out var value))
		{
			if (!StaticConfigure.Activity.InfoDict.TryGetValue(activityId, out var value2))
			{
				Debug.LogError($"活动id:{activityId} 该活动不存在");
				return null;
			}
			int uiTab = value2.UiTab;
			value = ((uiTab == 2 || uiTab == 4) ? new ScratchOffActivityData(activityId) : (activityId switch
			{
				605010 => new DiceActivityData(activityId), 
				610010 => new BingoActivityData(activityId), 
				_ => new TaskActivityData(activityId), 
			}));
			activityDict.Add(activityId, value);
		}
		return value.child<TaskActivityData>();
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.ActivityTaskConditionS2C.OnActivityTaskConditionS2CServerCallBackAsync = OnActivityTaskConditionS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.ActivityTaskRewardS2C.OnActivityTaskRewardS2CServerCallBackAsync = OnActivityTaskRewardS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.ScratchCardS2C.OnScratchCardS2CServerCallBackAsync = OnScratchCardS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.NextScratchCardPoolS2C.OnNextScratchCardPoolS2CServerCallBackAsync = OnNextScratchCardPoolS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.ClientCheckTaskS2C.OnClientCheckTaskS2CServerCallBackAsync = OnClientCheckTaskS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.LightGiftS2C.OnLightGiftS2CServerCallBackAsync = OnLightGiftS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.BuyLightGiftS2C.OnBuyLightGiftS2CServerCallBackAsync = OnBuyLightGiftS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.FlipCardS2C.OnFlipCardS2CServerCallBackAsync = OnFlipCardS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.FlipCardProgressRewardS2C.OnFlipCardProgressRewardS2CServerCallBackAsync = OnFlipCardProgressRewardS2CServerCallBackAsync;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.ActivityTaskConditionS2C.OnActivityTaskConditionS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ActivityTaskRewardS2C.OnActivityTaskRewardS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ScratchCardS2C.OnScratchCardS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.NextScratchCardPoolS2C.OnNextScratchCardPoolS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ClientCheckTaskS2C.OnClientCheckTaskS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.LightGiftS2C.OnLightGiftS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.BuyLightGiftS2C.OnBuyLightGiftS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.FlipCardS2C.OnFlipCardS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.FlipCardProgressRewardS2C.OnFlipCardProgressRewardS2CServerCallBackAsync = null;
	}

	public RPCAsyncResult RequestLightGiftC2S(int activityId, int index)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.LightGiftC2S.LightGiftC2SCall(new LightGiftC2S
		{
			ActivityId = activityId,
			Index = index
		});
	}

	private async UniTask OnLightGiftS2CServerCallBack(LightGiftS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			GetLightGiftActivityData(model.ActivityId).UpdateLightData(model.ConfIndex, model.Index);
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestBuyLightGiftC2S(int activityId, int index)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.BuyLightGiftC2S.BuyLightGiftC2SCall(new BuyLightGiftC2S
		{
			ActivityId = activityId,
			Index = index
		});
	}

	private async UniTask OnBuyLightGiftS2CServerCallBack(BuyLightGiftS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			GetLightGiftActivityData(model.LightGift.ActivityId).UpdateGift(model.LightGift);
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnActivityTaskConditionS2CServerCallBack(ActivityTaskConditionS2C model, int errid, bool isdispatch)
	{
		if (errid != 0)
		{
			return;
		}
		foreach (ActivityTaskCondition item in model.Condition)
		{
			TaskActivityData taskActivityData = GetTaskActivityData(item.InfoId);
			taskActivityData.UpdateActivityAchieve(item.Cond, Noop: false);
			taskActivityData.UpdatePlayerAchieve_2(item.Cond1);
		}
		await UniTask.CompletedTask;
	}

	public RPCAsyncResult OnRequestTaskReward(int activityId, BaseTaskData task)
	{
		if (task is MissionData missionData)
		{
			Debug.Log($"[Activity] 调用Mission任务领取 TaskId={missionData._Id}");
			return SimpleSingletonProvider<GameLogicManager>.inst.task.RequestActivityMissionRewardC2S(missionData._Id);
		}
		return RequestActivityTaskRewardC2S(activityId, task._Id);
	}

	public RPCAsyncResult RequestActivityTaskRewardC2S(int activityId, int taskId)
	{
		_ActivityId = activityId;
		_TaskId = taskId;
		return MonoSingletonProvider<NetManager>.inst.RPC.ActivityTaskRewardC2S.ActivityTaskRewardC2SCall(new ActivityTaskRewardC2S
		{
			InfoId = activityId,
			TaskId = taskId
		});
	}

	private async UniTask OnActivityTaskRewardS2CServerCallBack(ActivityTaskRewardS2C model, int errid, bool isdispatch)
	{
		if (errid != 0)
		{
			return;
		}
		if (_ActivityId != 0)
		{
			TaskActivityData taskActivityData = GetTaskActivityData(_ActivityId);
			if (!taskActivityData.taskFinishIds.Contains(_TaskId))
			{
				taskActivityData.taskFinishIds.Add(_TaskId);
			}
		}
		signal.activityStatus.Dispatch(_ActivityId);
		await UniTask.CompletedTask;
	}

	public RPCAsyncResult RequestScratchCardC2S(int activityId, int index)
	{
		_ActivityId = activityId;
		return MonoSingletonProvider<NetManager>.inst.RPC.ScratchCardC2S.ScratchCardC2SCall(new ScratchCardC2S
		{
			ActivityId = activityId,
			Index = index
		});
	}

	private async UniTask OnScratchCardS2CServerCallBack(ScratchCardS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			GetScratchOffActivityData(_ActivityId).UpdateRecord(model.Index, model.ConfigIndex);
			if (SimpleSingletonProvider<UIManager>.inst.currentPanel is ActivityPanel activityPanel)
			{
				await activityPanel.ShowScratchOffResult(model.Index);
			}
		}
	}

	public RPCAsyncResult RequestNextScratchCardPoolC2S(int activityId)
	{
		_ActivityId = activityId;
		return MonoSingletonProvider<NetManager>.inst.RPC.NextScratchCardPoolC2S.NextScratchCardPoolC2SCall(new NextScratchCardPoolC2S
		{
			ActivityId = activityId
		});
	}

	private async UniTask OnNextScratchCardPoolS2CServerCallBack(NextScratchCardPoolS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			GetScratchOffActivityData(_ActivityId).curPoolId = model.PoolId;
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestFlipCardC2S(int activityId, int index)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.FlipCardC2S.FlipCardC2SCall(new FlipCardC2S
		{
			ActivityId = activityId,
			Index = index
		});
	}

	private async UniTask OnFlipCardS2CServerCallBack(FlipCardS2C model, int errId, bool isDispatch)
	{
		if (errId != 0)
		{
			return;
		}
		if (GetTaskActivityData(model.ActivityId) is BingoActivityData bingoActivityData)
		{
			bingoActivityData.FlipCard(model.Index);
			if (SimpleSingletonProvider<UIManager>.inst.currentPanel is ActivityBingoPanel activityBingoPanel && activityBingoPanel.IsOpen())
			{
				activityBingoPanel.RefreshOnFlipCardS2C(model.Index);
			}
		}
		await UniTask.CompletedTask;
	}

	public RPCAsyncResult RequestFlipCardProgressRewardC2S(int activityId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.FlipCardProgressRewardC2S.FlipCardProgressRewardC2SCall(new FlipCardProgressRewardC2S
		{
			ActivityId = activityId
		});
	}

	private async UniTask OnFlipCardProgressRewardS2CServerCallBackAsync(FlipCardProgressRewardS2C model, int errId, bool isDispatch)
	{
		if (errId != 0)
		{
			return;
		}
		if (GetTaskActivityData(model.ActivityId) is BingoActivityData bingoActivityData)
		{
			bingoActivityData.UpdateProgressGrid(model.RoundIndex);
			if (SimpleSingletonProvider<UIManager>.inst.currentPanel is ActivityBingoPanel activityBingoPanel && activityBingoPanel.IsOpen())
			{
				activityBingoPanel.RefreshProgressInfo();
			}
			signal.activityStatus.Dispatch(model.ActivityId);
		}
		await UniTask.CompletedTask;
	}

	public RPCAsyncResult RequestClientCheckTaskC2S(int _param)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.ClientCheckTaskC2S.ClientCheckTaskC2SCall(new ClientCheckTaskC2S
		{
			Param = _param
		});
	}

	private async UniTask OnClientCheckTaskS2CServerCallBack(ClientCheckTaskS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	public void RegisterRed()
	{
	}

	public bool GetStatus(int activityId, int taskId)
	{
		TaskActivityData taskActivityData = GetTaskActivityData(activityId);
		if (taskActivityData == null)
		{
			Debug.LogError("活动Id: {activityId} 非任务类型活动，无法取出任务进度");
			return false;
		}
		return taskActivityData.GetStatus(taskId);
	}

	public bool AdjustActivity(ActivityInfoConfigure activityConfig)
	{
		bool flag = true;
		int signInByActivityId = SimpleSingletonProvider<GameLogicManager>.inst.signIn.GetSignInByActivityId(activityConfig.Id);
		if (StaticConfigure.SignIn.InfoDict.ContainsKey(signInByActivityId))
		{
			flag = !SimpleSingletonProvider<GameLogicManager>.inst.signIn.IsSignInAllCompleted(signInByActivityId);
		}
		if (activityConfig.Id == 512195 || activityConfig.Id == 602060)
		{
			int passIdByActivityId = SimpleSingletonProvider<GameLogicManager>.inst.activityPass.GetPassIdByActivityId(activityConfig.Id);
			if (!SimpleSingletonProvider<GameLogicManager>.inst.activityPass.PassGearData.ContainsKey(passIdByActivityId))
			{
				return false;
			}
		}
		if (activityConfig.Id == 512195 || activityConfig.Id == 602060 || activityConfig.Id == 599990)
		{
			bool flag2 = false;
			foreach (int missionID in activityConfig.MissionIDs)
			{
				MissionData missionData = SimpleSingletonProvider<GameLogicManager>.inst.task.TryGetMissionById(missionID);
				if (missionData != null && missionData.Status != 3)
				{
					flag2 = true;
					break;
				}
			}
			flag = flag && flag2;
		}
		return flag && TimeHelper.ValidityTime(activityConfig.BeginTime, activityConfig.EndTime);
	}

	public ActivityActivityEntrance2Configure GetActivityInfo(int entranceType)
	{
		foreach (ActivityActivityEntrance2Configure activityEntrance in StaticConfigure.Activity.ActivityEntrance2S)
		{
			if (activityEntrance.LocationNumb == entranceType && AdjustActivity(activityEntrance.InfoConfig) && activityEntrance.LanguageType.Contains(GameSettings.languageType))
			{
				return activityEntrance;
			}
		}
		return null;
	}

	public List<ActivityActivityEntrance2Configure> GetActivityInfos(int entranceType)
	{
		List<ActivityActivityEntrance2Configure> list = new List<ActivityActivityEntrance2Configure>(8);
		foreach (ActivityActivityEntrance2Configure activityEntrance in StaticConfigure.Activity.ActivityEntrance2S)
		{
			if (activityEntrance.LocationNumb == entranceType && AdjustActivity(activityEntrance.InfoConfig) && activityEntrance.LanguageType.Contains(GameSettings.languageType))
			{
				list.Add(activityEntrance);
			}
		}
		return list;
	}

	public List<ActivityActivityEntrance2Configure> GetActivityHubData()
	{
		List<ActivityActivityEntrance2Configure> list = new List<ActivityActivityEntrance2Configure>();
		foreach (ActivityActivityEntrance2Configure activityEntrance in StaticConfigure.Activity.ActivityEntrance2S)
		{
			if (activityEntrance.ActivityHubOrder != 0 && AdjustActivity(activityEntrance.InfoConfig) && activityEntrance.LanguageType.Contains(GameSettings.languageType))
			{
				list.Add(activityEntrance);
			}
		}
		list.Sort((ActivityActivityEntrance2Configure x, ActivityActivityEntrance2Configure y) => x.ActivityHubOrder.CompareTo(y.ActivityHubOrder));
		return list;
	}

	public bool CheckWayTypeIsActivity(WayType wayType)
	{
		if (wayType == WayType.Activity || wayType == WayType.ActivityPopup || wayType == WayType.ActivityVa11HallA || wayType == WayType.ActivityStore2)
		{
			return true;
		}
		if (!StaticConfigure.Way.InfoDict.TryGetValue((int)wayType, out var value))
		{
			return false;
		}
		if (value.PanelType == UIPanelType.None)
		{
			return false;
		}
		foreach (ActivityInfoConfigure info in StaticConfigure.Activity.Infos)
		{
			if (info.PanelType == value.PanelType)
			{
				return true;
			}
		}
		return false;
	}
}
