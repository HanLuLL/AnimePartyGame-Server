using Google.Protobuf.WellKnownTypes;
using Tools;
using UI;

namespace GameLogic;

public abstract class BaseGoodsData : IChild<BaseGoodsData>, IGoods
{
	public int goodsId;

	protected int limitNum;

	protected int remainingNum;

	protected bool hasPurchase;

	protected int singleNum;

	public int goodsOrder;

	public ShopTabType shopTabType;

	public Timestamp BeginTime;

	public Timestamp EndTime;

	public ItemInfoConfigure itemConfig;

	public int currencyID;

	public int label;

	public int SalePrice => GetSalePrice();

	public int LimitNum()
	{
		return limitNum;
	}

	public int RemainingNum()
	{
		return remainingNum;
	}

	public virtual bool HasPurchase()
	{
		return hasPurchase;
	}

	public int SingleNum()
	{
		return singleNum;
	}

	public bool IsOwn()
	{
		if (StaticConfigure.Item.TagDict.TryGetValue((int)itemConfig.ItemType, out var value))
		{
			if (value.MaxCount == 1 && SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(itemConfig.Id))
			{
				return !HasPurchase();
			}
			return false;
		}
		return false;
	}

	public bool SellOut()
	{
		if (LimitNum() != 0)
		{
			return RemainingNum() <= 0;
		}
		return false;
	}

	public virtual string GetName()
	{
		return itemConfig.NameID.GetLocal(UIStringType.Item);
	}

	public virtual void UpdatePurchaseRecord()
	{
	}

	public T child<T>() where T : BaseGoodsData
	{
		return this as T;
	}

	public abstract int GetOriginalPrice();

	public abstract int GetDiscountPrice();

	protected virtual int GetSalePrice()
	{
		return GetDiscountPrice();
	}

	public virtual float GetTotalSpending(int count)
	{
		return GetSalePrice() * count;
	}

	public string GetPriceText(int price, string priceFormat = "0.##")
	{
		return ((float)price * 0.01f).ToString(priceFormat);
	}
}
