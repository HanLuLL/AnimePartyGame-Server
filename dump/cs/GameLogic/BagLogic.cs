using System.Collections.Generic;
using Core.Net;
using Core.Scene;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class BagLogic : IRPCSync, IReadPoint
{
	private BagContainer _container;

	private Dictionary<int, WeeklyLimitPropData> WeeklyLimitPropDataDict = new Dictionary<int, WeeklyLimitPropData>();

	public readonly ReactiveProperty<bool> bagRedSignal = new ReactiveProperty<bool>(initialValue: false);

	public BagSignal signal { get; private set; }

	public int capacity => _container.capacity;

	public void InitFromServer(RepeatedField<ItemEtc> protoItems, MapField<int, int> WeeklyLimits)
	{
		_container = new BagContainer();
		signal = new BagSignal();
		foreach (ItemEtc protoItem in protoItems)
		{
			InitItem(protoItem);
		}
		UpdateWeeklyLimitPropData(WeeklyLimits);
	}

	private void InitItem(ItemEtc protoData)
	{
		BagItem bagItem = CreateItem(protoData);
		if (bagItem != null)
		{
			_container.Add(bagItem);
		}
	}

	private void TryConvertParticularItem(BagItem item, ItemEtc protoData)
	{
		if (item.config.ItemType == ItemType.Chest && item is ChestItem chestItem)
		{
			chestItem.Init(item.config.SubMeterID);
		}
	}

	private BagItem CreateItem(ItemEtc protoData)
	{
		if (protoData == null)
		{
			Debug.LogError("服务器的道具为null");
			return null;
		}
		if (!StaticConfigure.Item.InfoDict.TryGetValue(protoData.ItemId, out var value))
		{
			Debug.LogError($"未找到道具配置:{protoData.ItemId}");
			return null;
		}
		BagItem bagItem = ((value.ItemType != ItemType.Chest) ? new BagItem() : new ChestItem());
		BagItem bagItem2 = bagItem;
		bagItem2.Init(value, protoData.Count);
		TryConvertParticularItem(bagItem2, protoData);
		return bagItem2;
	}

	public void UpdateItemCount(int id, int newCount)
	{
		_container.UpdateItemCount(id, newCount);
	}

	private void RemoveItem(int id)
	{
		_container.Remove(id);
	}

	public bool ExistItem(int itemId)
	{
		return _container.ExistItem(itemId);
	}

	public BagItem GetItem(int id)
	{
		return _container.GetItem(id);
	}

	public T GetItem<T>(int id) where T : BagItem
	{
		return _container.GetItem<T>(id);
	}

	public List<int> GetItemsByType(ItemType itemType)
	{
		return _container.GetIDs(itemType);
	}

	public int GetItemCount(int itemID)
	{
		return _container.GetItemCount(itemID);
	}

	public List<ChestRandomRewardConfigure> GetRandomChestReward(int itemId)
	{
		ItemInfoConfigure itemInfoConfigure = itemId.GetItemInfoConfigure();
		if (itemInfoConfigure == null)
		{
			return null;
		}
		ChestInfoConfigure chestInfoConfigure = itemInfoConfigure.SubMeterID.GetChestInfoConfigure();
		if (chestInfoConfigure.IsOptional)
		{
			Debug.LogError($"道具id: {itemId} 对应的是一个可选宝箱并非随机宝箱");
			return null;
		}
		List<ChestRandomRewardConfigure> list = new List<ChestRandomRewardConfigure>();
		foreach (int item in chestInfoConfigure.RandomReward)
		{
			ChestRandomRewardConfigure chestRandomRewardConfigure = item.GetChestRandomRewardConfigure();
			list.Add(chestRandomRewardConfigure);
		}
		return list;
	}

	public void TryGetSkinInfoFromRandomChest(int chestId, out ItemInfoConfigure skinItemInfo, out ItemInfoConfigure labelItemInfo)
	{
		skinItemInfo = null;
		labelItemInfo = null;
		List<ChestRandomRewardConfigure> randomChestReward = GetRandomChestReward(chestId);
		if (randomChestReward == null)
		{
			return;
		}
		foreach (ChestRandomRewardConfigure item in randomChestReward)
		{
			foreach (ChestRandomRewardConfigureItem chestRandomRewardConfigureItem in item.ChestRandomRewardConfigureItems)
			{
				ItemInfoConfigure itemInfoConfigure = chestRandomRewardConfigureItem.ItemID.GetItemInfoConfigure();
				if (itemInfoConfigure != null)
				{
					if (itemInfoConfigure.ItemType == ItemType.HeroStandingPainting)
					{
						skinItemInfo = itemInfoConfigure;
					}
					else if (itemInfoConfigure.ItemType == ItemType.AccountBackground)
					{
						labelItemInfo = itemInfoConfigure;
					}
				}
			}
		}
	}

	public List<KeyValuePair<int, int>> PropItemsSort(List<KeyValuePair<int, int>> items)
	{
		if (items.Count > 1)
		{
			items.Sort(delegate(KeyValuePair<int, int> a, KeyValuePair<int, int> b)
			{
				ItemInfoConfigure itemInfoConfigure = a.Key.GetItemInfoConfigure();
				ItemInfoConfigure itemInfoConfigure2 = b.Key.GetItemInfoConfigure();
				int num = ((int)(itemInfoConfigure2?.QualityType ?? QualityType.None)).CompareTo((int)(itemInfoConfigure?.QualityType ?? QualityType.None));
				return (num != 0) ? num : (itemInfoConfigure?.Id ?? 0).CompareTo(itemInfoConfigure2?.Id ?? 0);
			});
		}
		return items;
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.BagItemChangeS2C.OnBagItemChangeS2CServerCallBackAsync = OnBagItemChangeS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.UseTreasureS2C.OnUseTreasureS2CServerCallBackAsync = OnUseTreasureS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.UseTreasureAutoTransformS2C.OnUseTreasureAutoTransformS2CServerCallBackAsync = OnUseTreasureAutoTransformS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.CheatItemS2C.OnCheatItemS2CServerCallBackAsync = OnCheatItemS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.ChangeItemLimitS2C.OnChangeItemLimitS2CServerCallBackAsync = OnChangeItemLimitS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.CleanItemLimitS2C.OnCleanItemLimitS2CServerCallBackAsync = OnCleanItemLimitS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.BagExpiredTransformNotify.OnBagExpiredTransformNotifyServerCallBackAsync = OnBagExpiredTransformNotifyServerCallBack;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.BagItemChangeS2C.OnBagItemChangeS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.UseTreasureS2C.OnUseTreasureS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.CheatItemS2C.OnCheatItemS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ChangeItemLimitS2C.OnChangeItemLimitS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.CleanItemLimitS2C.OnCleanItemLimitS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.BagExpiredTransformNotify.OnBagExpiredTransformNotifyServerCallBackAsync = null;
	}

	private async UniTask OnBagItemChangeS2CServerCallBack(BagItemChangeS2C protoData, int errId, bool isdispatch)
	{
		if (errId != 0)
		{
			return;
		}
		if (protoData?.Item != null)
		{
			if (protoData == null || protoData.Item.Count != 0)
			{
				if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle && (!(SimpleSingletonProvider<UIManager>.inst.currentPanel is GachaPanel gachaPanel) || gachaPanel.IsIdleStatus()))
				{
					await SimpleSingletonProvider<UIManager>.inst.reward.ShowReward(protoData);
				}
				foreach (ItemEtc item in protoData.Item)
				{
					if (item.Count == 0)
					{
						RemoveItem(item.ItemId);
						signal.onItemRemoved.Dispatch(item.ItemId);
					}
					else if (ExistItem(item.ItemId))
					{
						UpdateItemCount(item.ItemId, item.Count);
						signal.onItemUpdated.Dispatch(item.ItemId, item.Count);
					}
					else
					{
						InitItem(item);
						signal.onItemAdded.Dispatch(item.ItemId);
					}
					SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateNewData(item.ItemId);
					signal.onItemCountChanged.Dispatch(item.ItemId, item.Count);
				}
				SimpleSingletonProvider<GameLogicManager>.inst.account.RequestClientDataUploadS2C(ClientDataUploadC2S.Types.OpsData.NewItemData);
				RegisterRed();
				signal.bagMapChanged.Dispatch();
				await UniTask.CompletedTask;
				return;
			}
		}
		Debug.LogError("收到的服务器道具推送消息为null");
	}

	private async UniTask OnUseTreasureS2CServerCallBack(UseTreasureS2C protoData, int errId, bool isdispatch)
	{
		if (errId == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnUseTreasureAutoTransformS2CServerCallBack(UseTreasureAutoTransformS2C protoData, int errId, bool isdispatch)
	{
		if (errId == 0)
		{
			await SimpleSingletonProvider<UIManager>.inst.reward.ShowAutoTransformReward(protoData);
		}
	}

	private async UniTask OnCheatItemS2CServerCallBack(CheatItemS2C protoData, int errId, bool isdispatch)
	{
		if (errId == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnBagExpiredTransformNotifyServerCallBack(BagExpiredTransformNotify model, int errId, bool isDispatch)
	{
		if (errId == 0 && model != null && SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle && (!(SimpleSingletonProvider<UIManager>.inst.currentPanel is GachaPanel gachaPanel) || gachaPanel.IsIdleStatus()) && model.ExpiredItems != null && model.ExpiredItems.Count != 0 && model.RewardItems != null && model.RewardItems.Count != 0)
		{
			SimpleSingletonProvider<UIManager>.inst.reward.ShowReward(model);
			await UniTask.CompletedTask;
		}
	}

	public void RegisterRed()
	{
		bagRedSignal.Value = GetSystemStatus();
	}

	public void ClearNewChestSetSilent()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.account.OnTypeToRemoveNewItemData(ItemType.Chest);
	}

	public bool GetSystemStatus()
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.account.OnTypeGetNewData(ItemType.Chest);
	}

	private void UpdateWeeklyLimitPropData(MapField<int, int> weeklyLimits)
	{
		foreach (KeyValuePair<int, int> weeklyLimit in weeklyLimits)
		{
			if (WeeklyLimitPropDataDict.TryGetValue(weeklyLimit.Key, out var value))
			{
				value.Count = weeklyLimit.Value;
				continue;
			}
			WeeklyLimitPropDataDict.TryAdd(weeklyLimit.Key, new WeeklyLimitPropData
			{
				Count = weeklyLimit.Value,
				itemId = weeklyLimit.Key
			});
		}
	}

	private async UniTask OnChangeItemLimitS2CServerCallBack(ChangeItemLimitS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			UpdateWeeklyLimitPropData(model.WeeklyLimits);
			await UniTask.CompletedTask;
		}
	}

	public WeeklyLimitPropData GetWeeklyLimitPropData(int itemId)
	{
		if (!WeeklyLimitPropDataDict.TryGetValue(itemId, out var value))
		{
			value = new WeeklyLimitPropData
			{
				Count = 0,
				itemId = itemId
			};
			WeeklyLimitPropDataDict.TryAdd(itemId, value);
		}
		return value;
	}

	private async UniTask OnCleanItemLimitS2CServerCallBack(CleanItemLimitS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			if (model.WeeklyClean)
			{
				WeeklyLimitPropDataDict.Clear();
			}
			await UniTask.CompletedTask;
		}
	}
}
