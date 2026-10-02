using System;
using System.Collections.Generic;
using Core.Net;
using Tools;
using party.protocol;

namespace GameLogic;

public class RoomListData
{
	public float _currentRefreshTime;

	public readonly List<RoomShortInfo> roomInfos = new List<RoomShortInfo>();

	public readonly Dictionary<long, RoomShortInfo> roomInfoDict = new Dictionary<long, RoomShortInfo>();

	private long curTime;

	private readonly long activeTime = StaticGlobalData.ROOM_ACTIVE_TIME * 60;

	public RoomShortInfo GetRoomLabelById(long roomId)
	{
		return roomInfoDict.GetValueOrDefault(roomId);
	}

	public void UpdateRoomList(QueryRoomS2C model)
	{
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		roomInfos.Clear();
		roomInfoDict.Clear();
		foreach (RoomShortInfo item in model.Items)
		{
			if (serverTime.DateTimeToStampForSeconds() - item.CreateTime < 7200)
			{
				roomInfos.Add(item);
				roomInfoDict.Add(item.Id, item);
			}
		}
		curTime = MonoSingletonProvider<NetManager>.inst.ServerTime.DateTimeToStampForSeconds();
		roomInfos.Sort(CompareToRoom);
	}

	private int CompareToRoom(RoomShortInfo x, RoomShortInfo y)
	{
		if (x.PlayerCount == 4 != (y.PlayerCount == 4))
		{
			if (x.PlayerCount != 4)
			{
				return -1;
			}
			return 1;
		}
		if (x.IsPwd != y.IsPwd)
		{
			if (!x.IsPwd)
			{
				return -1;
			}
			return 1;
		}
		return CompareToTime(x, y);
	}

	private int CompareToTime(RoomShortInfo x, RoomShortInfo y)
	{
		long num = curTime - x.CreateTime;
		long num2 = curTime - y.CreateTime;
		if (num <= activeTime != num2 <= activeTime)
		{
			if (num > activeTime)
			{
				return 1;
			}
			return -1;
		}
		if (x.PlayerCount != y.PlayerCount)
		{
			if (x.PlayerCount <= y.PlayerCount)
			{
				return 1;
			}
			return -1;
		}
		if (x.CreateTime != y.CreateTime)
		{
			if (x.CreateTime <= y.CreateTime)
			{
				return 1;
			}
			return -1;
		}
		return 0;
	}
}
