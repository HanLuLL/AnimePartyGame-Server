using UI;

namespace GameLogic;

public class DailRefreshGoods : BaseGoodsData
{
	public ExchangeStoreRefreshPoolConfigureItem goodsConfig;

	public DailRefreshGoods(ShopTabType _shopTabType, ExchangeStoreRefreshPoolConfigureItem _goodsConfig, int _purchaseNum)
	{
		goodsConfig = _goodsConfig;
		goodsId = goodsConfig.GoodsID;
		limitNum = goodsConfig.NumLimit;
		remainingNum = goodsConfig.NumLimit - _purchaseNum;
		hasPurchase = _purchaseNum != 0;
		singleNum = goodsConfig.ItemNum;
		currencyID = goodsConfig.CurrencyID;
		label = 0;
		itemConfig = goodsConfig.ItemID.GetItemInfoConfigure();
		goodsOrder = 0;
		shopTabType = _shopTabType;
	}

	public override int GetOriginalPrice()
	{
		return goodsConfig.OriginalPrice;
	}

	public override int GetDiscountPrice()
	{
		return goodsConfig.DiscountPrice;
	}
}
