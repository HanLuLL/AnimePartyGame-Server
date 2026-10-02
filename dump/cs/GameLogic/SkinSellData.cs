using System.Collections.Generic;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;

namespace GameLogic;

public class SkinSellData : ExchangeGoods
{
	public struct RewardItemData
	{
		public int ItemID;

		public string IconUrl;

		public int Count;
	}

	public readonly SkinSellInfoConfigure skinSellConfig;

	public SkinSellData(ShopTabType _shopTabType, ExchangeStoreGoodsConfigureItem _goodsConfig, int _purchaseNum)
		: base(_shopTabType, _goodsConfig, _purchaseNum)
	{
		int param = goodsConfig.Param;
		if (!StaticConfigure.SkinSell.InfoDict.TryGetValue(param, out skinSellConfig))
		{
			Debug.LogError($"无法通过param：{param} 在SkinSell.InfoDict获取相应的配置");
		}
	}

	public SkinSellInfoConfigureItem GetSkinChestByIndex(int index)
	{
		if (skinSellConfig == null)
		{
			return null;
		}
		if (skinSellConfig.SkinSellInfoConfigureItems.Count > index)
		{
			return skinSellConfig.SkinSellInfoConfigureItems[index];
		}
		Debug.LogError($"当前获取第{index}商品信息失败，请检查配置SkinSell.InfoDict");
		return null;
	}

	public List<SkinSellInfoConfigure> GetSkinSellChestMore()
	{
		if (skinSellConfig == null)
		{
			return null;
		}
		List<SkinSellInfoConfigure> list = new List<SkinSellInfoConfigure>();
		for (int i = 0; i < skinSellConfig.SplitSale.Count; i++)
		{
			if (StaticConfigure.SkinSell.InfoDict.TryGetValue(skinSellConfig.SplitSale[i], out var value))
			{
				list.Add(value);
			}
		}
		return list;
	}

	public bool IsOwnGoods(int itemId)
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(itemId);
	}

	public bool CheckValidWay(int skinComboId)
	{
		return skinComboId == goodsId;
	}

	public override int GetDiscountPrice()
	{
		RepeatedField<SkinSellInfoConfigureItem> skinSellInfoConfigureItems = skinSellConfig.SkinSellInfoConfigureItems;
		int num = 0;
		foreach (SkinSellInfoConfigureItem item in skinSellInfoConfigureItems)
		{
			if (!SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(item.ItemID))
			{
				num += item.DiscountPrice;
			}
		}
		return num;
	}

	public override int GetOriginalPrice()
	{
		RepeatedField<SkinSellInfoConfigureItem> skinSellInfoConfigureItems = skinSellConfig.SkinSellInfoConfigureItems;
		int num = 0;
		foreach (SkinSellInfoConfigureItem item in skinSellInfoConfigureItems)
		{
			if (!SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(item.ItemID))
			{
				num += item.OriginalPrice;
			}
		}
		return num;
	}

	public bool HasPurchaseCombo()
	{
		UpdatePurchaseRecord();
		if (SellOut() || IsOwn())
		{
			return true;
		}
		RepeatedField<SkinSellInfoConfigureItem> skinSellInfoConfigureItems = skinSellConfig.SkinSellInfoConfigureItems;
		for (int i = 0; i < skinSellInfoConfigureItems.Count; i++)
		{
			if (skinSellInfoConfigureItems[i].ItemID != 0 && !IsOwnGoods(skinSellInfoConfigureItems[i].ItemID))
			{
				return false;
			}
		}
		return true;
	}

	public List<RewardItemData> GetRewardItems()
	{
		RepeatedField<SkinSellInfoConfigureItem> skinSellInfoConfigureItems = skinSellConfig.SkinSellInfoConfigureItems;
		List<RewardItemData> list = new List<RewardItemData>();
		foreach (SkinSellInfoConfigureItem item in skinSellInfoConfigureItems)
		{
			if (item.ItemID != 0)
			{
				continue;
			}
			List<ChestRandomRewardConfigure> randomChestReward = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetRandomChestReward(item.Id);
			if (randomChestReward == null)
			{
				continue;
			}
			foreach (ChestRandomRewardConfigure item2 in randomChestReward)
			{
				foreach (ChestRandomRewardConfigureItem chestRandomRewardConfigureItem in item2.ChestRandomRewardConfigureItems)
				{
					ItemInfoConfigure itemInfoConfigure = chestRandomRewardConfigureItem.ItemID.GetItemInfoConfigure();
					if (itemInfoConfigure != null)
					{
						list.Add(new RewardItemData
						{
							ItemID = itemInfoConfigure.Id,
							IconUrl = itemInfoConfigure.ShowIcon,
							Count = chestRandomRewardConfigureItem.MinCount
						});
					}
				}
			}
		}
		return list;
	}
}
