using System;
using Core.Net;
using GameLogic;
using Google.Protobuf.WellKnownTypes;
using Tools;

namespace UI;

public class ShopTypeData
{
	public ShopTabType tabType;

	public string tabName;

	public int currencyBar;

	public Timestamp beginTime;

	public Timestamp endTime;

	public int ShelfId;

	public bool newGoods => SimpleSingletonProvider<GameLogicManager>.inst.store.NewGoodsStatusByType((int)tabType);

	public string GetTabTime()
	{
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		if (endTime == null)
		{
			return null;
		}
		DateTime dateTime = endTime.ToDateTime();
		return TimeHelper.RefreshTimeText(1010, 1011, serverTime, dateTime);
	}
}
