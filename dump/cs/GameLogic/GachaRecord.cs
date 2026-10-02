using System;
using Tools;
using UI;
using party.model;

namespace GameLogic;

public class GachaRecord
{
	public readonly ItemInfoConfigure itemInfo;

	public readonly int itemCount;

	public bool isConvert;

	public readonly long time;

	public readonly string _FormatTime;

	public GachaRecord(party.model.GachaRecord _record)
	{
		itemInfo = _record.ItemId.GetItemInfoConfigure();
		itemCount = _record.ItemCount;
		isConvert = _record.IsConvert;
		time = _record.Time;
		DateTime dateTime = (time * 1000).StampMillisecondsToDateTime();
		_FormatTime = dateTime.ToUIDateTime_YMDHM();
	}
}
