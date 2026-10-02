using System;
using System.Collections.Generic;
using Core.Net;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;

namespace GameLogic;

public class BattlePassData
{
	public BattlePassInfoConfigure BattlePassInfo;

	public Dictionary<int, BattlePassReward> battlePassRewardDict;

	public BattlePassReward surpassReward;

	public BattlePassGearType gearType;

	public int Exp;

	private int _LV;

	public Dictionary<int, int> battlePassRewardIds = new Dictionary<int, int>();

	public Dictionary<int, BattlePassTaskData> taskDataDict;

	public List<int> taskIds;

	public List<int> taskFinishIds;

	public Dictionary<int, int> taskProgress;

	public int LV => Mathf.Max(1, _LV);

	public int MaxLevel
	{
		get
		{
			RepeatedField<BattlePassRewardConfigureItem> battlePassRewardConfigureItems = BattlePassInfo.BattlePassRewardConfig.BattlePassRewardConfigureItems;
			return battlePassRewardConfigureItems[battlePassRewardConfigureItems.Count - 1].Level;
		}
	}

	public BattlePassData(BattlePassInfoConfigure _InfoConfigure, BattlePass _BattlePass)
	{
		BattlePassInfo = _InfoConfigure;
		BattlePassRewardConfigure battlePassRewardConfig = BattlePassInfo.BattlePassRewardConfig;
		battlePassRewardDict = new Dictionary<int, BattlePassReward>(battlePassRewardConfig.BattlePassRewardConfigureItems.Count);
		foreach (BattlePassRewardConfigureItem battlePassRewardConfigureItem in battlePassRewardConfig.BattlePassRewardConfigureItems)
		{
			battlePassRewardDict.TryAdd(battlePassRewardConfigureItem.Level, new BattlePassReward(battlePassRewardConfig.Id, battlePassRewardConfigureItem));
		}
		surpassReward = new BattlePassReward(battlePassRewardConfig.Id, MaxLevel + 1, BattlePassInfo.RewardPerLvAfterMax);
		BattlePassTaskConfigure battlePassTaskConfig = BattlePassInfo.BattlePassTaskConfig;
		taskDataDict = new Dictionary<int, BattlePassTaskData>(battlePassTaskConfig.BattlePassTaskConfigureItems.Count);
		taskIds = new List<int>(taskDataDict.Count);
		taskFinishIds = new List<int>(taskDataDict.Count);
		taskProgress = new Dictionary<int, int>();
		foreach (BattlePassTaskConfigureItem battlePassTaskConfigureItem in battlePassTaskConfig.BattlePassTaskConfigureItems)
		{
			taskDataDict.TryAdd(battlePassTaskConfigureItem.Id, new BattlePassTaskData(BattlePassInfo.Id, battlePassTaskConfigureItem));
			taskIds.Add(battlePassTaskConfigureItem.Id);
		}
		UpdateBattlePass(_BattlePass);
	}

	public void UpdateBattlePass(BattlePass battlePass)
	{
		UpdateLVExp(battlePass.Lv, battlePass.Exp);
		gearType = (BattlePassGearType)battlePass.Gear;
		foreach (KeyValuePair<int, int> rewardId in battlePass.RewardIds)
		{
			UpdateBattlePassReward((BattlePassGearType)rewardId.Key, rewardId.Value);
		}
		UpdateTaskInfo(battlePass);
	}

	public void UpdateLVExp(int _lv, int _exp)
	{
		_LV = _lv;
		Exp = _exp;
	}

	private void UpdateBattlePassReward(BattlePassGearType type, int _lv)
	{
		if (battlePassRewardIds.ContainsKey((int)type))
		{
			battlePassRewardIds[(int)type] = _lv;
		}
		else
		{
			battlePassRewardIds.TryAdd((int)type, _lv);
		}
	}

	public void RecordBattlePassRewardLocal()
	{
		if (gearType >= BattlePassGearType.FREE)
		{
			UpdateBattlePassReward(BattlePassGearType.FREE, LV);
		}
		if (gearType >= BattlePassGearType.NORMAL)
		{
			UpdateBattlePassReward(BattlePassGearType.NORMAL, LV);
		}
		if (gearType >= BattlePassGearType.PREMIUM)
		{
			UpdateBattlePassReward(BattlePassGearType.PREMIUM, LV);
		}
	}

	public int GetFinishRewardLV(BattlePassGearType _GearType)
	{
		return battlePassRewardIds.GetValueOrDefault((int)_GearType, 0);
	}

	public void UpdateTaskInfo(BattlePass info)
	{
		taskFinishIds.Clear();
		foreach (int taskRewardI in info.TaskRewardIs)
		{
			taskFinishIds.Add(taskRewardI);
		}
		UpdateTaskAchieve(info.Task, Noop: true);
	}

	public void UpdateTaskAchieve(MapField<int, int> Condition, bool Noop)
	{
		if (Noop)
		{
			taskProgress.Clear();
		}
		foreach (KeyValuePair<int, int> item in Condition)
		{
			taskProgress[item.Key] = item.Value;
		}
	}

	public void UpdateTaskFinishIds(RepeatedField<int> TaskRewardIds)
	{
		taskFinishIds.Clear();
		foreach (int TaskRewardId in TaskRewardIds)
		{
			UpdateTaskFinishIds(TaskRewardId);
		}
	}

	public void UpdateTaskFinishIds(int TaskRewardId)
	{
		if (!taskFinishIds.Contains(TaskRewardId))
		{
			taskFinishIds.Add(TaskRewardId);
		}
	}

	public List<int> GetTaskIdsByType(TaskRefreshType type)
	{
		List<int> list = new List<int>();
		foreach (int taskId in taskIds)
		{
			if (taskDataDict[taskId].taskConfig.TaskRefreshType == type)
			{
				list.Add(taskId);
			}
		}
		list.Sort((int x, int y) => CompareTo(taskDataDict[x], taskDataDict[y]));
		return list;
	}

	private int CompareTo(BattlePassTaskData x, BattlePassTaskData y)
	{
		if (!x._FinishStatus && !x.TaskRunning)
		{
			if (y._FinishStatus || y.TaskRunning)
			{
				return -1;
			}
			return 1;
		}
		if (!x._FinishStatus && x.TaskRunning)
		{
			if (!y._FinishStatus && !y.TaskRunning)
			{
				return 1;
			}
			if (!y._FinishStatus && y.TaskRunning)
			{
				return x.taskConfig.OrderWeight - y.taskConfig.OrderWeight;
			}
			return -1;
		}
		if (!y._FinishStatus || !y.TaskRunning)
		{
			return 1;
		}
		return -1;
	}

	public int GetProgress(int taskID, int _type)
	{
		return taskProgress.GetValueOrDefault(taskID, 0);
	}

	public bool GetStatus(int _taskId)
	{
		return taskFinishIds.Contains(_taskId);
	}

	public bool GetTaskSystemStatus()
	{
		foreach (KeyValuePair<int, BattlePassTaskData> item in taskDataDict)
		{
			if (!item.Value._FinishStatus && !item.Value.TaskRunning)
			{
				return true;
			}
		}
		return false;
	}

	public BattlePassReward GetPopupRewardData()
	{
		if (battlePassRewardDict.TryGetValue(LV, out var value))
		{
			if (value.VailReward)
			{
				return value;
			}
			if (battlePassRewardDict.TryGetValue(LV + 1, out value))
			{
				return value;
			}
		}
		return surpassReward;
	}

	public string GetCutDown()
	{
		if ((object)BattlePassInfo.EndTime != null)
		{
			DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
			DateTime endTime = BattlePassInfo.EndTime.ToDateTime();
			return TimeHelper.RefreshTimeText(1052, 1053, serverTime, endTime);
		}
		return null;
	}

	public string GetTimeText()
	{
		return TimeHelper.GetDurationText(BattlePassInfo.BeginTime, BattlePassInfo.EndTime);
	}

	public RechargeGoods GetNormalGoods()
	{
		BattlePassGoodsConfigure battlePassGoodsConfig = BattlePassInfo.BattlePassGoodsConfig;
		return SimpleSingletonProvider<GameLogicManager>.inst.store.GetRechargeGoodsByShopTypeAndGoodsID(25, battlePassGoodsConfig.NormalGoods);
	}

	public RechargeGoods GetPremiumGoods()
	{
		BattlePassGoodsConfigure battlePassGoodsConfig = BattlePassInfo.BattlePassGoodsConfig;
		if (gearType != BattlePassGearType.NORMAL)
		{
			return SimpleSingletonProvider<GameLogicManager>.inst.store.GetRechargeGoodsByShopTypeAndGoodsID(25, battlePassGoodsConfig.PremiumGoods);
		}
		return SimpleSingletonProvider<GameLogicManager>.inst.store.GetRechargeGoodsByShopTypeAndGoodsID(25, battlePassGoodsConfig.UpgradeGoods);
	}
}
