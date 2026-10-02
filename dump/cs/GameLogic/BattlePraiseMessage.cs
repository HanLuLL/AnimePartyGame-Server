using Core;
using Tools;
using UI;

namespace GameLogic;

public class BattlePraiseMessage : BattleMessage
{
	public BattlePraiseMessage(BattlePlayerData _sender, long targetPlayerId)
		: base(_sender)
	{
		ReadyMsg(targetPlayerId);
	}

	public BattlePraiseMessage(BattlePlayerData _playerData, string[] _msg)
		: base(_playerData)
	{
		if (_msg != null && _msg.Length >= 1 && long.TryParse(_msg[1], out var result))
		{
			ReadyMsg(result);
		}
	}

	private void ReadyMsg(long targetPlayerId)
	{
		MsgType = MessageType.PRAISE;
		SendMsg = $"{(int)MsgType}, {targetPlayerId}";
		UIWeight = 1;
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(targetPlayerId);
		string text = "";
		string text2 = "ffffff";
		if (playerDataById != null)
		{
			text = playerDataById.player.GetNick().GetSubString(14, out var _);
			text2 = GameConfig.HTMLStringRGB(playerDataById.player.Slot);
		}
		shortInfo = string.Format(30009.GetLocal(UIStringType.Chat), "[color=#" + text2 + "]" + text + "[/color]");
	}
}
