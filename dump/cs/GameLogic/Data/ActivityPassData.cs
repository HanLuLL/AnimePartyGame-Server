using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace GameLogic.Data;

public class ActivityPassData
{
	public BattlePassNewInfoConfigure PassInfoConfig;

	public Dictionary<int, ActivityPassItemData> PassItemData;

	public int PassGear;

	public int ActivityId;

	public ActivityPassData(int passId)
	{
		if (!StaticConfigure.BattlePassNew.InfoDict.TryGetValue(passId, out PassInfoConfig))
		{
			Debug.LogError($"invalid pass id {passId}");
			return;
		}
		PassItemData = new Dictionary<int, ActivityPassItemData>(PassInfoConfig.BattlePassNewInfoConfigureItems.Count);
		for (int i = 0; i < PassInfoConfig.BattlePassNewInfoConfigureItems.Count; i++)
		{
			ActivityId = PassInfoConfig.BattlePassNewInfoConfigureItems[i].ActivityId;
			PassItemData.Add(i, new ActivityPassItemData(PassInfoConfig.BattlePassNewInfoConfigureItems[i], this));
		}
	}

	public void RefreshInfo(int passGear)
	{
		PassGear = passGear;
		foreach (ActivityPassItemData value in PassItemData.Values)
		{
			value.RefreshInfo();
		}
	}

	public RechargeGoods GetPremiumGoods(ShopTabType shopTabType)
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.store.GetRechargeGoodsByShopTypeAndGoodsID((int)shopTabType, PassInfoConfig.Id);
	}
}
