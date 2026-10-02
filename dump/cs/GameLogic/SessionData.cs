using System.Collections.Generic;
using Core.Net;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.model;

namespace GameLogic;

public class SessionData
{
	public long targetPlayerId;

	private string _TargetHeadURL;

	public readonly List<ChatMessage> messages = new List<ChatMessage>();

	public long lastTime;

	public long readTime;

	public string draft;

	public string TargetHeadURL
	{
		get
		{
			FriendData friendData = SimpleSingletonProvider<GameLogicManager>.inst.friend.GetFriendData(targetPlayerId);
			if (friendData != null)
			{
				_TargetHeadURL = friendData.HeadURL;
			}
			if (string.IsNullOrEmpty(_TargetHeadURL))
			{
				int itemId = SimpleSingletonProvider<GameLogicManager>.inst.fashion.defaultMap[1];
				_TargetHeadURL = itemId.GetItemInfoConfigure().SubMeterID.GetFashionAccountHeadShot();
			}
			return _TargetHeadURL;
		}
	}

	public string TargetName
	{
		get
		{
			FriendData friendData = SimpleSingletonProvider<GameLogicManager>.inst.friend.GetFriendData(targetPlayerId);
			if (friendData != null)
			{
				return SimpleSingletonProvider<GameLogicManager>.inst.friend.GetDisplayNick(targetPlayerId, friendData.Nick, showNote: true);
			}
			return "";
		}
	}

	public string myHeadURL => SimpleSingletonProvider<GameLogicManager>.inst.fashion.GetRunningFashion().headShotId.GetItemInfoConfigure().SubMeterID.GetFashionAccountHeadShot();

	public bool isHaveUnReadMsg => readTime < lastTime;

	public bool TargetOnline => SimpleSingletonProvider<GameLogicManager>.inst.friend.GetFriendData(targetPlayerId)?.IsOnline ?? false;

	public SessionData(long _targetPlayerId)
	{
		targetPlayerId = _targetPlayerId;
	}

	public void UpdateChatMsg(RepeatedField<party.model.ChatMessage> _Messages)
	{
		messages.Clear();
		for (int i = 0; i < _Messages.Count; i++)
		{
			UpdateChatMsg(_Messages[i].SenderId, _Messages[i].ReceiverId, _Messages[i].Message, _Messages[i].Time);
		}
	}

	public void UpdateChatMsg(long senderId, long receiverId, string message, long _Time)
	{
		foreach (ChatMessage item in ChatMessage.Split(senderId, receiverId, message, _Time))
		{
			messages.Add(item);
		}
	}

	public void UpdateSessionTime(long _LastTime, long _ReadTime)
	{
		lastTime = _LastTime;
		readTime = _ReadTime;
	}

	public void FinishRead()
	{
		int num = MonoSingletonProvider<NetManager>.inst.ServerTime.DateTimeToStampForSeconds();
		lastTime = num;
		readTime = num;
	}

	public (string, string) GetTargetDisplay()
	{
		FriendData friendData = SimpleSingletonProvider<GameLogicManager>.inst.friend.GetFriendData(targetPlayerId);
		if (friendData != null)
		{
			string friendNote = SimpleSingletonProvider<GameLogicManager>.inst.friend.GetFriendNote(targetPlayerId);
			return (friendData.Nick, friendNote);
		}
		return (null, null);
	}
}
