using System;
using Core.Net;
using Tools;
using UI;
using party.model;
using party.protocol;

namespace GameLogic;

public class FriendData
{
	public long playerId;

	public int Score;

	private string _nick;

	public int LV;

	private int headPropId;

	private int labelPropId;

	public long _Time;

	public long loginTime;

	public FriendRoomInfo roomInfo;

	public OperateFriendType opType;

	public bool IsOnline;

	public bool IsBusy;

	public string Nick => _nick;

	public RelationType relation
	{
		get
		{
			bool num = SimpleSingletonProvider<GameLogicManager>.inst.friend.IsFriend(playerId);
			bool flag = SimpleSingletonProvider<GameLogicManager>.inst.friend.IsBlack(playerId);
			if (num)
			{
				if (flag)
				{
					return RelationType.DETERIORATE;
				}
				return RelationType.FRIEND;
			}
			if (flag)
			{
				return RelationType.DISLIKE;
			}
			return RelationType.STRANGERS;
		}
	}

	public string HeadURL => ((headPropId == 0) ? SimpleSingletonProvider<GameLogicManager>.inst.fashion.defaultMap[1] : headPropId).GetItemInfoConfigure().SubMeterID.GetFashionAccountHeadShot();

	public (string, bool) Label => ((labelPropId == 0) ? SimpleSingletonProvider<GameLogicManager>.inst.fashion.defaultMap[2] : labelPropId).GetItemInfoConfigure().SubMeterID.GetFashionAccountBackgroundConfigure().GetPlayerLabel();

	public string offlineTimeDesc
	{
		get
		{
			if (_Time > 0)
			{
				TimeSpan timeSpan = (_Time * 1000).StampMillisecondsToDateTime() - MonoSingletonProvider<NetManager>.inst.ServerTime;
				if (timeSpan.TotalDays > 1.0)
				{
					return string.Format(1040.GetLocal(UIStringType.Message), timeSpan.Days);
				}
				if (timeSpan.TotalHours > 1.0)
				{
					return string.Format(1041.GetLocal(UIStringType.Message), timeSpan.Hours);
				}
				if (timeSpan.TotalMinutes > 0.0)
				{
					return string.Format(1042.GetLocal(UIStringType.Message), timeSpan.Minutes);
				}
			}
			return "";
		}
	}

	public void UpdateFriendData(FriendInfo friendInfo)
	{
		if (playerId == friendInfo.PlayerId && friendInfo.RoomId != 0L && roomInfo != null)
		{
			roomInfo.roomId = friendInfo.RoomId;
			roomInfo.roomServerId = friendInfo.RoomServerId;
		}
		else if (friendInfo.RoomId != 0L)
		{
			roomInfo = new FriendRoomInfo
			{
				roomId = friendInfo.RoomId,
				roomServerId = friendInfo.RoomServerId
			};
		}
		playerId = friendInfo.PlayerId;
		_nick = friendInfo.Name;
		LV = friendInfo.Lv;
		headPropId = friendInfo.HeadIcon;
		labelPropId = friendInfo.Background;
		_Time = friendInfo.OfflineTime;
		loginTime = friendInfo.LoginTime;
		IsOnline = friendInfo.IsOnline;
		IsBusy = friendInfo.IsBusy;
		Score = friendInfo.SignalScore;
		if (friendInfo.RoomId == 0L)
		{
			roomInfo = null;
		}
	}

	public void UpdateFriendData(FriendShowPlayerInfo friendInfo)
	{
		if (playerId != friendInfo.PlayerId)
		{
			roomInfo = null;
			playerId = friendInfo.PlayerId;
		}
		_nick = friendInfo.Name;
		LV = friendInfo.Lv;
		headPropId = friendInfo.HeadIcon;
		labelPropId = friendInfo.Background;
		_Time = friendInfo.Time;
		IsOnline = friendInfo.IsOnline;
		IsBusy = friendInfo.IsBusy;
	}

	public void UpdateFriendData(FriendApply friendInfo)
	{
		if (playerId != friendInfo.PlayerId)
		{
			roomInfo = null;
		}
		playerId = friendInfo.PlayerId;
		_Time = friendInfo.Time;
		opType = (OperateFriendType)friendInfo.OpType;
	}

	public void UpdateFriendData(FriendInviteInfo friendInfo)
	{
		playerId = friendInfo.PlayerId;
		_nick = friendInfo.Name;
		LV = friendInfo.Lv;
		headPropId = friendInfo.HeadIcon;
		labelPropId = friendInfo.Background;
		_Time = friendInfo.Time;
		if (friendInfo.RoomId == 0L)
		{
			roomInfo = null;
			return;
		}
		roomInfo = new FriendRoomInfo
		{
			roomId = friendInfo.RoomId,
			roomServerId = friendInfo.RoomServerId,
			invitePwd = friendInfo.Pwd,
			valid = friendInfo.Valid
		};
	}
}
