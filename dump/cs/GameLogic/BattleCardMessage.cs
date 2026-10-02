using UI;

namespace GameLogic;

public class BattleCardMessage : BattleMessage
{
	public static readonly int MarkId = 40018;

	public BattleCardMessage(BattlePlayerData _playerData, int cardId)
		: base(_playerData)
	{
		ReadyMsg(cardId);
	}

	public BattleCardMessage(BattlePlayerData _playerData, string[] _msg)
		: base(_playerData)
	{
		if (_msg != null && _msg.Length >= 2 && int.TryParse(_msg[1], out var result))
		{
			ReadyMsg(result);
		}
	}

	private void ReadyMsg(int cardId)
	{
		MsgType = MessageType.CARD;
		SendMsg = $"{(int)MsgType}, {cardId}";
		UIWeight = 1;
		shortInfo = GetCardMsg(cardId);
	}

	public static string GetCardMsg(int cardId)
	{
		CardInfoConfigure cardConfigure = cardId.GetCardConfigure();
		string local = cardConfigure.NameID.GetLocal(UIStringType.Card);
		string text = cardConfigure.CardType switch
		{
			CardType.Attack => "FF0000", 
			CardType.Defend => "0099FF", 
			_ => "00CC00", 
		};
		if (StaticConfigure.Chat.MarkDict.TryGetValue(MarkId, out var value))
		{
			return string.Format(value.ChatInfo, "[color=#" + text + "]" + local + "[/color]");
		}
		return null;
	}
}
