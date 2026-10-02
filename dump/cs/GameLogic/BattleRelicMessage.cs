using UI;

namespace GameLogic;

public class BattleRelicMessage : BattleMessage
{
	public static readonly int MarkId = 40018;

	public BattleRelicMessage(BattlePlayerData _playerData, int relicId)
		: base(_playerData)
	{
		ReadyMsg(relicId);
	}

	public BattleRelicMessage(BattlePlayerData _playerData, string[] _msg)
		: base(_playerData)
	{
		if (_msg != null && _msg.Length >= 2 && int.TryParse(_msg[1], out var result))
		{
			ReadyMsg(result);
		}
	}

	private void ReadyMsg(int relicId)
	{
		MsgType = MessageType.RELICINFO;
		SendMsg = $"{(int)MsgType}, {relicId}";
		UIWeight = 1;
		shortInfo = GetRelicMsg(relicId);
	}

	public static string GetRelicMsg(int relicId)
	{
		RelicInfoConfigure relicInfoConfigure = relicId.GetRelicInfoConfigure();
		string text = relicInfoConfigure.RelicQualityType switch
		{
			RelicQualityType.Blue => "#004DFF", 
			RelicQualityType.Purple => "#9700E6", 
			RelicQualityType.Orange => "#EE8F00", 
			_ => "#004DFF", 
		};
		if (StaticConfigure.Chat.MarkDict.TryGetValue(MarkId, out var value))
		{
			return string.Format(value.ChatInfo, "[color=" + text + "]" + relicInfoConfigure.NameID.GetLocal(UIStringType.Relic) + "[/color]");
		}
		return null;
	}
}
