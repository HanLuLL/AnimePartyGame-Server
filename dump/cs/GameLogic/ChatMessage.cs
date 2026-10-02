using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Tools;

namespace GameLogic;

public class ChatMessage
{
	public readonly long senderId;

	public long receiverId;

	public readonly bool fromMe;

	public readonly string msg;

	public readonly string _FormatTime;

	public MessageType Type;

	private const string EMOJI_TOKEN_PATTERN = "\\[e:(\\d+)\\]";

	public ChatMessage(long _senderId, long _receiverId, string _Msg, long _Time, MessageType type = MessageType.NONE)
	{
		senderId = _senderId;
		receiverId = _receiverId;
		fromMe = SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(senderId);
		msg = _Msg;
		DateTime time = (_Time * 1000).StampMillisecondsToDateTime();
		_FormatTime = time.ToUIDateTime_YMDHM();
		Type = type;
	}

	public static List<ChatMessage> Split(long senderId, long receiverId, string raw, long time)
	{
		List<ChatMessage> list = new List<ChatMessage>();
		if (string.IsNullOrEmpty(raw))
		{
			list.Add(new ChatMessage(senderId, receiverId, string.Empty, time));
			return list;
		}
		int num = 0;
		foreach (Match item in Regex.Matches(raw, "\\[e:(\\d+)\\]"))
		{
			if (TryGetLegalExpression(item, out var expressionId))
			{
				if (item.Index > num)
				{
					list.Add(new ChatMessage(senderId, receiverId, raw.Substring(num, item.Index - num), time));
				}
				list.Add(new ChatMessage(senderId, receiverId, expressionId.ToString(), time, MessageType.EXPRESSION));
				num = item.Index + item.Length;
			}
		}
		if (num < raw.Length)
		{
			list.Add(new ChatMessage(senderId, receiverId, raw.Substring(num), time));
		}
		return list;
	}

	private static bool TryGetLegalExpression(Match match, out int expressionId)
	{
		expressionId = 0;
		if (!int.TryParse(match.Groups[1].Value, out expressionId))
		{
			return false;
		}
		if (!StaticConfigure.Item.InfoDict.TryGetValue(expressionId, out var value))
		{
			return false;
		}
		return value.ItemType == ItemType.HeroExpression;
	}
}
