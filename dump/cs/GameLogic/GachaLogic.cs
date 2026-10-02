using System.Collections.Generic;
using Core.Net;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class GachaLogic : IRPCSync
{
	public readonly List<GachaItem> itemList = new List<GachaItem>();

	public GachaSignal signal = new GachaSignal();

	public readonly List<GachaRecord> gachaRecords = new List<GachaRecord>();

	public readonly Dictionary<int, GachaPoolProgress> GachaProgressDict = new Dictionary<int, GachaPoolProgress>();

	public GachaPoolInfo GetGachaPoolInfo(int poolID)
	{
		GachaPoolConfigure gachaPoolConfigure = poolID.GetGachaPoolConfigure();
		if (gachaPoolConfigure == null)
		{
			return null;
		}
		return new GachaPoolInfo(gachaPoolConfigure);
	}

	public Dictionary<int, List<int>> GetBackstageTypesForTable()
	{
		Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>
		{
			[1] = new List<int>(),
			[2] = new List<int>(),
			[4] = new List<int>()
		};
		foreach (GachaBackstageConfigure backstage in StaticConfigure.Gacha.Backstages)
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.gacha.BackstageAvailable(backstage) && dictionary.TryGetValue((int)backstage.GachaTableType, out var value))
			{
				value.Add((int)backstage.GachaType);
			}
		}
		List<int> list = new List<int>();
		foreach (KeyValuePair<int, List<int>> item in dictionary)
		{
			if (item.Value.Count <= 0)
			{
				list.Add(item.Key);
			}
		}
		foreach (int item2 in list)
		{
			dictionary.Remove(item2);
		}
		return dictionary;
	}

	public GachaPoolProgress GetPoolProgress(int poolID)
	{
		if (!GachaProgressDict.TryGetValue(poolID, out var value))
		{
			value = new GachaPoolProgress
			{
				PoolId = poolID
			};
			GachaProgressDict.TryAdd(poolID, value);
		}
		return value;
	}

	public bool GetPoolRedPointStatus(GachaProgressConfigure gachaProgressConfigure)
	{
		GachaPoolProgress poolProgress = GetPoolProgress(gachaProgressConfigure.PoolID);
		foreach (GachaProgressConfigureItem gachaProgressConfigureItem in gachaProgressConfigure.GachaProgressConfigureItems)
		{
			if (!poolProgress.IsFinishRewardByProgress(gachaProgressConfigureItem.Count) && poolProgress.Progress >= gachaProgressConfigureItem.Count && gachaProgressConfigureItem.Reward.Keys.Count > 0)
			{
				return true;
			}
		}
		return false;
	}

	public int GetGachaStatus()
	{
		Dictionary<int, List<int>> backstageTypesForTable = GetBackstageTypesForTable();
		if (!backstageTypesForTable.TryGetValue(1, out var value) || value.Count == 0)
		{
			Debug.LogError("尝试取出角色池失败，需要检查配置");
			return 0;
		}
		foreach (int item in value)
		{
			if (item == 3 || item == 5)
			{
				return 1;
			}
		}
		if (backstageTypesForTable.TryGetValue(2, out var value2) && value2.Count > 0)
		{
			return 2;
		}
		return 0;
	}

	public void InitFromServer(Player player)
	{
		foreach (GachaRecordList gachaRecord in player.GachaRecords)
		{
			GetPoolProgress(gachaRecord.PoolId).UpdateData(gachaRecord.Count, gachaRecord.RewardCount);
		}
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GachaS2C.OnGachaS2CServerCallBackAsync = OnGachaS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.GachaRecordS2C.OnGachaRecordS2CServerCallBackAsync = OnGachaRecordS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.GachaCountRewardS2C.OnGachaCountRewardS2CServerCallBackAsync = OnGachaCountRewardS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.RookieGachaRewardS2C.OnRookieGachaRewardS2CServerCallBackAsync = OnRookieGachaRewardS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.GachaCountS2C.OnGachaCountS2CServerCallBackAsync = OnGachaCountS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.GachaCountS2C.OnGachaCountS2CServerCallBackAsync = OnGachaCountS2CServerCallBack;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GachaS2C.OnGachaS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.GachaRecordS2C.OnGachaRecordS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.GachaCountRewardS2C.OnGachaCountRewardS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.RookieGachaRewardS2C.OnRookieGachaRewardS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.GachaCountS2C.OnGachaCountS2CServerCallBackAsync = null;
	}

	public RPCAsyncResult RequestGachaCSC(int defId, int count)
	{
		if (StaticConfigure.Gacha.BackstageDict.TryGetValue(defId, out var value))
		{
			int num = ((value.GachaType == GachaType.Rookie) ? 8 : (value.CostOnce * count));
			int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(value.TimeLimit);
			if (itemCount > 0)
			{
				if (itemCount >= num)
				{
					SimpleSingletonProvider<GameLogicManager>.inst.bag.UpdateItemCount(value.TimeLimit, itemCount - num);
					num = 0;
				}
				else
				{
					SimpleSingletonProvider<GameLogicManager>.inst.bag.UpdateItemCount(value.TimeLimit, 0);
					num -= itemCount;
				}
			}
			int itemCount2 = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(value.CostItem);
			SimpleSingletonProvider<GameLogicManager>.inst.bag.UpdateItemCount(value.CostItem, itemCount2 - num);
		}
		return MonoSingletonProvider<NetManager>.inst.RPC.GachaC2S.GachaC2SCall(new GachaC2S
		{
			DefId = defId,
			Count = count
		});
	}

	private async UniTask OnGachaS2CServerCallBack(GachaS2C model, int errId, bool isdispatch)
	{
		if (errId == 0)
		{
			itemList.Clear();
			for (int i = 0; i < model.Reward.Count; i++)
			{
				itemList.Add(new GachaItem(model.Reward[i].ItemId, model.Reward[i].Count, GetOwnStatus(model.Reward, model.Reward[i].ItemId, i)));
			}
			GachaPoolProgress poolProgress = GetPoolProgress(model.PoolId);
			if (poolProgress != null)
			{
				poolProgress.Progress = model.Count;
				signal.gachaProgress.Dispatch(model.PoolId);
			}
			await UniTask.CompletedTask;
		}
	}

	private bool GetOwnStatus(RepeatedField<GachaReward> _Reward, int itemId, int index)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(itemId))
		{
			return true;
		}
		for (int i = 0; i < _Reward.Count; i++)
		{
			if (_Reward[i].ItemId == itemId)
			{
				return index != i;
			}
		}
		return false;
	}

	public RPCAsyncResult RequestGachaRecordS2C(int BackstageId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.GachaRecordC2S.GachaRecordC2SCall(new GachaRecordC2S
		{
			Id = BackstageId
		});
	}

	private async UniTask OnGachaRecordS2CServerCallBack(GachaRecordS2C model, int errid, bool isdispatch)
	{
		if (errid != 0)
		{
			return;
		}
		gachaRecords.Clear();
		foreach (party.model.GachaRecord record in model.Records)
		{
			gachaRecords.Add(new GachaRecord(record));
		}
		gachaRecords.Reverse();
		await UniTask.CompletedTask;
	}

	private int OnCompareByTime(GachaRecord x, GachaRecord y)
	{
		if (x.time > y.time)
		{
			return -1;
		}
		return 1;
	}

	public RPCAsyncResult RequestGachaCountRewardS2C(int poolId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.GachaCountRewardC2S.GachaCountRewardC2SCall(new GachaCountRewardC2S
		{
			PoolId = poolId
		});
	}

	private async UniTask OnGachaCountRewardS2CServerCallBack(GachaCountRewardS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			GetPoolProgress(model.PoolId).UpdateRewards(model.RewardCount);
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnGachaCountS2CServerCallBack(GachaCountS2C model, int errid, bool isdispatch)
	{
		await UniTask.CompletedTask;
	}

	public RPCAsyncResult RequestRookieGachaRewardC2S(int poolId, int itemId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.RookieGachaRewardC2S.RookieGachaRewardC2SCall(new RookieGachaRewardC2S
		{
			PoolId = poolId,
			DefId = 9,
			ItemId = itemId
		});
	}

	private async UniTask OnRookieGachaRewardS2CServerCallBack(RookieGachaRewardS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			GetPoolProgress(model.PoolId).UpdateRewards(model.RewardCount);
			await UniTask.CompletedTask;
		}
	}

	public bool BackstageAvailable(GachaBackstageConfigure backstageConfig)
	{
		return TimeHelper.ValidityTime(backstageConfig.BeginDateTime, backstageConfig.EndDateTime);
	}

	public bool GetRedPointStatus()
	{
		Dictionary<int, List<int>> backstageTypesForTable = SimpleSingletonProvider<GameLogicManager>.inst.gacha.GetBackstageTypesForTable();
		if (!backstageTypesForTable.TryGetValue(1, out var value) || value.Count == 0)
		{
			Debug.LogError("尝试取出角色池失败，需要检查配置");
			return false;
		}
		foreach (KeyValuePair<int, List<int>> item in backstageTypesForTable)
		{
			List<GachaBackstageConfigure> list = new List<GachaBackstageConfigure>();
			foreach (int item2 in item.Value)
			{
				GachaBackstageConfigure gachaBackstageConfigure = item2.GetGachaBackstageConfigure();
				list.Add(gachaBackstageConfigure);
			}
			if (GetGachaTabRedPoint(list))
			{
				return true;
			}
		}
		return false;
	}

	public bool GetGachaTabRedPoint(List<GachaBackstageConfigure> list)
	{
		foreach (GachaBackstageConfigure item in list)
		{
			GachaPoolConfigure gachaPoolConfigure = item.PoolID.GetGachaPoolConfigure();
			if (gachaPoolConfigure != null && StaticConfigure.Gacha.ProgressDict.TryGetValue(gachaPoolConfigure.PoolID, out var value) && SimpleSingletonProvider<GameLogicManager>.inst.gacha.GetPoolRedPointStatus(value))
			{
				return true;
			}
		}
		return false;
	}
}
