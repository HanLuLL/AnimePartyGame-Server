using UI;

namespace GameLogic;

public class BattleSkillMessage : BattleMessage
{
	public int CurMarkId;

	public BattleSkillMessage(BattlePlayerData _playerData, int cd)
		: base(_playerData)
	{
		ReadyMsg(cd);
	}

	public BattleSkillMessage(BattlePlayerData _playerData, string[] _msg)
		: base(_playerData)
	{
		if (_msg != null && _msg.Length >= 2 && int.TryParse(_msg[1], out var result))
		{
			ReadyMsg(result);
		}
	}

	private void ReadyMsg(int cd)
	{
		MsgType = MessageType.SKILL;
		SendMsg = $"{(int)MsgType}, {cd}";
		UIWeight = 1;
		(shortInfo, CurMarkId) = GetSkillMsg(cd);
	}

	public static (string, int) GetSkillMsg(int cd)
	{
		int num = ((cd == 0) ? 40010 : 40011);
		return (string.Format(num.GetLocal(UIStringType.Chat), cd), num);
	}
}
