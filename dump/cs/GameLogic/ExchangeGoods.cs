using Tools;
using UI;

namespace GameLogic;

public class ExchangeGoods : BaseGoodsData
{
	public ExchangeStoreGoodsConfigureItem goodsConfig;

	public ExchangeGoods(ShopTabType _shopTabType, ExchangeStoreGoodsConfigureItem _goodsConfig, int _purchaseNum)
	{
		goodsConfig = _goodsConfig;
		goodsId = goodsConfig.GoodsID;
		limitNum = goodsConfig.NumLimit;
		remainingNum = goodsConfig.NumLimit - _purchaseNum;
		hasPurchase = _purchaseNum != 0;
		singleNum = goodsConfig.ItemNum;
		currencyID = goodsConfig.CurrencyID;
		label = (int)goodsConfig.GoodsLabelType;
		itemConfig = goodsConfig.ItemID.GetItemInfoConfigure();
		goodsOrder = goodsConfig.GoodsOrder;
		shopTabType = _shopTabType;
		BeginTime = goodsConfig.BeginTime;
		EndTime = goodsConfig.EndTime;
	}

	public override int GetDiscountPrice()
	{
		return goodsConfig.DiscountPrice;
	}

	public override int GetOriginalPrice()
	{
		return goodsConfig.OriginalPrice;
	}

	protected override int GetSalePrice()
	{
		if ((object)goodsConfig.BeginTimeLimited != null && (object)goodsConfig.EndTimeLimited != null && TimeHelper.ValidityTime(goodsConfig.BeginTimeLimited, goodsConfig.EndTimeLimited))
		{
			return goodsConfig.DiscountPriceLimited;
		}
		return GetDiscountPrice();
	}

	public override void UpdatePurchaseRecord()
	{
		int goodsRecord = SimpleSingletonProvider<GameLogicManager>.inst.store.GetGoodsRecord((int)shopTabType, goodsId);
		remainingNum = goodsConfig.NumLimit - goodsRecord;
		hasPurchase = goodsRecord != 0;
	}
}
