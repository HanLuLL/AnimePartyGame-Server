using UI;
using UnityEngine;

namespace GameLogic;

public class BattlePveEventMessage : BattleMessage
{
	public BattlePveEventMessage(BattlePlayerData _playerData, int markId, int progress, string eventCard)
		: base(_playerData)
	{
		ReadyMsg(markId, progress, eventCard);
	}

	public BattlePveEventMessage(BattlePlayerData _playerData, string[] _msg)
		: base(_playerData)
	{
		if (_msg != null && _msg.Length >= 4 && int.TryParse(_msg[1], out var result) && int.TryParse(_msg[2], out var result2))
		{
			ReadyMsg(result, result2, _msg[3]);
		}
	}

	private void ReadyMsg(int markId, int progress, string eventCard)
	{
		MsgType = MessageType.PVEEVENT;
		SendMsg = $"{(int)MsgType}, {markId}, {progress}, {eventCard}";
		UIWeight = 1;
		shortInfo = GetPveEventMsg(markId, progress, eventCard);
	}

	public static string GetPveEventMsg(int markId, int progress, string eventCard)
	{
		if (StaticConfigure.Chat.MarkDict.TryGetValue(markId, out var value))
		{
			string[] array = eventCard.Split('|');
			string text = "";
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				if (int.TryParse(array2[i], out var result) && StaticConfigure.MapEvent.MapEventCardDict.TryGetValue(result, out var value2))
				{
					if (!string.IsNullOrEmpty(text))
					{
						text += " & ";
					}
					text += value2.NameID.GetLocal(UIStringType.MapEvent);
				}
			}
			return string.Format(value.ChatInfo, progress, "[color=#FF0000]" + text + "[/color]");
		}
		Debug.LogError($"Chat.Mark中 无法获取 标记Id:{markId}的配置");
		return null;
	}
}
