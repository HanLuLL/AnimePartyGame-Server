using Tools;
using UI;

namespace GameLogic;

public class RechargeGoods : BaseGoodsData
{
	public RechargeStoreGoodsConfigureItem goodsConfig;

	protected RechargeGoods()
	{
	}

	public RechargeGoods(ShopTabType _shopTabType, RechargeStoreGoodsConfigureItem _goodsConfig, int _purchaseNum)
	{
		RefreshRechargeGoodsInfo(_shopTabType, _goodsConfig, _purchaseNum);
	}

	protected void RefreshRechargeGoodsInfo(ShopTabType _shopTabType, RechargeStoreGoodsConfigureItem _goodsConfig, int _purchaseNum)
	{
		goodsConfig = _goodsConfig;
		goodsId = goodsConfig.GoodsID;
		limitNum = goodsConfig.NumLimit;
		remainingNum = ((goodsConfig.NumLimit != 0) ? (goodsConfig.NumLimit - _purchaseNum) : 0);
		hasPurchase = _purchaseNum != 0;
		singleNum = 1;
		currencyID = -1;
		label = (int)goodsConfig.GoodsLabelType;
		itemConfig = goodsConfig.ItemID.GetItemInfoConfigure();
		goodsOrder = goodsConfig.GoodsOrder;
		shopTabType = _shopTabType;
		BeginTime = goodsConfig.BeginTime;
		EndTime = goodsConfig.EndTime;
	}

	public override int GetDiscountPrice()
	{
		return goodsConfig.DiscountPriceCN;
	}

	public override int GetOriginalPrice()
	{
		return goodsConfig.OriginalPriceCN;
	}

	public int GetGoodsBonuses()
	{
		return goodsConfig.GoodsBonuses;
	}

	public int GetItemCount()
	{
		_ = goodsConfig.MerchandiselType;
		_ = 2;
		return goodsConfig.ItemNum;
	}

	public override void UpdatePurchaseRecord()
	{
		int goodsRechargeRecord = SimpleSingletonProvider<GameLogicManager>.inst.store.GetGoodsRechargeRecord((int)shopTabType, goodsId);
		remainingNum = ((goodsConfig.NumLimit != 0) ? (goodsConfig.NumLimit - goodsRechargeRecord) : 0);
		hasPurchase = goodsRechargeRecord != 0;
	}

	public string GetOriginalPriceText(string priceFormat = "0.##")
	{
		return GetPriceText(GetOriginalPrice(), priceFormat);
	}

	public string GetDiscountPriceText(string priceFormat = "0.##")
	{
		return GetPriceText(GetSalePrice(), priceFormat);
	}

	protected override int GetSalePrice()
	{
		if ((object)goodsConfig.BeginTimeLimited != null && (object)goodsConfig.EndTimeLimited != null && TimeHelper.ValidityTime(goodsConfig.BeginTimeLimited, goodsConfig.EndTimeLimited))
		{
			return goodsConfig.DiscountPriceLimitedCN;
		}
		return GetDiscountPrice();
	}

	public override float GetTotalSpending(int count)
	{
		return (float)GetSalePrice() * 0.01f * (float)count;
	}

	public override string GetName()
	{
		if (goodsConfig.MerchandiselType == MerchandiselType.Currency)
		{
			return $"{GetItemCount()}" + 29.GetLocal(UIStringType.RechargeStore);
		}
		if (goodsConfig.Name != 0)
		{
			return goodsConfig.Name.GetLocal(UIStringType.RechargeStore);
		}
		return base.GetName();
	}
}
