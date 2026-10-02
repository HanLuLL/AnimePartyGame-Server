using System.Collections.Generic;
using System.Linq;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;

namespace GameLogic;

public class BingoActivityData : TaskActivityData
{
	public readonly ActivityBingoFlipConfigure BingoActivityBingoConfig;

	public readonly BingoActivityGrid[] BingoGrids;

	public readonly BingoActivityGrid[] ProgressGrids;

	public readonly int CostItemId;

	public readonly int NumPerPage;

	public readonly int NumX;

	public readonly int NumY;

	public readonly int RoundCount;

	public BingoActivityData(int _activityId)
		: base(_activityId)
	{
		if (!StaticConfigure.Activity.BingoFlipDict.TryGetValue(_activityId, out BingoActivityBingoConfig))
		{
			Debug.LogError($"Id:{_activityId} 在 Activity.BingoFlip 中不存在");
		}
		CostItemId = BingoActivityBingoConfig.SpendItemID.Keys.First();
		NumX = BingoActivityBingoConfig.ColCount + 1;
		NumY = BingoActivityBingoConfig.RowCount + 1;
		RoundCount = BingoActivityBingoConfig.RoundCount;
		int num = NumX * NumY * RoundCount;
		NumPerPage = NumX * NumY;
		int gridPoolIds = BingoActivityBingoConfig.GridPoolIds;
		if (!StaticConfigure.Activity.BingoFlipPoolDict.TryGetValue(gridPoolIds, out var value))
		{
			Debug.LogError($"Id:{gridPoolIds} 在 Activity.BingoFlipPool 中不存在");
		}
		BingoGrids = new BingoActivityGrid[num];
		RepeatedField<ActivityBingoFlipPoolConfigureItem> activityBingoFlipPoolConfigureItems = value.ActivityBingoFlipPoolConfigureItems;
		if (activityBingoFlipPoolConfigureItems.Count != num)
		{
			Debug.LogError($"奖池：{gridPoolIds}的数量不为{num}");
		}
		for (int i = 0; i < num; i++)
		{
			ActivityBingoFlipPoolConfigureItem activityBingoFlipPoolConfigureItem = activityBingoFlipPoolConfigureItems[i];
			if (activityBingoFlipPoolConfigureItem.Index != i)
			{
				Debug.LogError($"奖池：{gridPoolIds}的物体配置Id 不是 0~{num - 1}");
			}
			BingoGrids[i] = new BingoActivityGrid(i, activityBingoFlipPoolConfigureItem);
		}
		int progressPoolId = BingoActivityBingoConfig.ProgressPoolId;
		if (!StaticConfigure.Activity.BingoFlipPoolDict.TryGetValue(progressPoolId, out var value2))
		{
			Debug.LogError($"Id:{progressPoolId} 在 Activity.BingoFlipPool 中不存在");
		}
		RepeatedField<ActivityBingoFlipPoolConfigureItem> activityBingoFlipPoolConfigureItems2 = value2.ActivityBingoFlipPoolConfigureItems;
		ProgressGrids = new BingoActivityGrid[activityBingoFlipPoolConfigureItems2.Count];
		for (int j = 0; j < activityBingoFlipPoolConfigureItems2.Count; j++)
		{
			ProgressGrids[j] = new BingoActivityGrid(j, activityBingoFlipPoolConfigureItems2[j]);
		}
	}

	public void InitDataByServer(FlipCardActivity flipCardData)
	{
		for (int i = 0; i < BingoGrids.Length; i++)
		{
			flipCardData.Records.TryGetValue(i, out var value);
			BingoGrids[i].UpdateFlipStatus(value);
		}
		for (int j = 0; j < ProgressGrids.Length; j++)
		{
			flipCardData.ProgressReward.TryGetValue(j, out var value2);
			ProgressGrids[j].UpdateFlipStatus(value2);
		}
	}

	public bool IsVaildPage(int page)
	{
		bool flag = page >= 0 && page < RoundCount;
		if (flag && page > 0)
		{
			flag = IsPageAllFlip(page - 1);
		}
		return flag;
	}

	public int GetPageCompleteCount()
	{
		int num = 0;
		for (int i = 0; i < BingoGrids.Length && BingoGrids[i].IsFlip; i++)
		{
			num++;
		}
		return num / NumPerPage;
	}

	public bool IsPageAllFlip(int pageIndex)
	{
		bool flag = false;
		if (pageIndex >= 0 && pageIndex < RoundCount)
		{
			flag = true;
			int num = pageIndex * NumPerPage;
			int num2 = num + NumPerPage;
			for (int i = num; i < num2; i++)
			{
				flag &= BingoGrids[i].IsFlip;
			}
		}
		return flag;
	}

	public void FlipCard(IEnumerable<int> cards)
	{
		foreach (int card in cards)
		{
			BingoGrids.GetSafeByIndex(card)?.UpdateFlipStatus(isFlip: true);
		}
	}

	public List<MissionData> GetSortedMissionList()
	{
		List<MissionData> list = new List<MissionData>();
		foreach (KeyValuePair<int, BaseTaskData> item2 in taskDataDict)
		{
			if (item2.Value is MissionData item)
			{
				list.Add(item);
			}
		}
		list.Sort(base.CompareTo);
		return list;
	}

	public BingoActivityGrid GetBingoActivityGrid(int id, bool isProgressReward)
	{
		if (!isProgressReward)
		{
			return BingoGrids[id];
		}
		return ProgressGrids[id];
	}

	public void UpdateProgressGrid(RepeatedField<int> roundIndex)
	{
		for (int i = 0; i < roundIndex.Count; i++)
		{
			int index = roundIndex[i];
			ProgressGrids.GetSafeByIndex(index)?.UpdateFlipStatus(isFlip: true);
		}
	}

	public bool IsFinishFlipCard()
	{
		int pageIndex = RoundCount - 1;
		return IsPageAllFlip(pageIndex);
	}

	public bool GetProgressStatus(int index)
	{
		if (ProgressGrids.Length > index)
		{
			return !ProgressGrids[index].IsFlip;
		}
		return false;
	}

	public bool GetProgressRewardStatus()
	{
		int pageCompleteCount = GetPageCompleteCount();
		for (int i = 0; i < pageCompleteCount; i++)
		{
			if (GetProgressStatus(i))
			{
				return true;
			}
		}
		return false;
	}

	public override bool GetActivityStatus()
	{
		bool activityStatus = base.GetActivityStatus();
		bool progressRewardStatus = GetProgressRewardStatus();
		bool flag = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(CostItemId) > 0 && !IsFinishFlipCard();
		return activityStatus || progressRewardStatus || flag;
	}
}
