namespace GameLogic;

public class OrderInformation
{
	public readonly int goodsId;

	public readonly int shopType;

	public OrderInformation(int goodsId, int shopType)
	{
		this.goodsId = goodsId;
		this.shopType = shopType;
	}
}
