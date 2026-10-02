using Tools;
using UI;

namespace GameLogic;

public class BattlePveTaskMessage : BattleMessage
{
	public static readonly int MarkId = 40015;

	public BattlePveTaskMessage(BattlePlayerData _playerData, int taskId)
		: base(_playerData)
	{
		ReadyMsg(taskId);
	}

	public BattlePveTaskMessage(BattlePlayerData _playerData, string[] _msg)
		: base(_playerData)
	{
		if (_msg != null && _msg.Length >= 2 && int.TryParse(_msg[1], out var result))
		{
			ReadyMsg(result);
		}
	}

	private void ReadyMsg(int taskId)
	{
		MsgType = MessageType.PVETASK;
		SendMsg = $"{(int)MsgType}, {taskId}";
		UIWeight = 1;
		shortInfo = GetPveTaskMsg(taskId);
	}

	public static string GetPveTaskMsg(int taskId)
	{
		string local = MarkId.GetLocal(UIStringType.Chat);
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		if (roomInfo != null && roomInfo.MapMissionDict.TryGetValue(taskId, out var value))
		{
			MapMissionTargetData safeByIndex = value.targetsData.GetSafeByIndex(0);
			if (safeByIndex != null)
			{
				string title = safeByIndex.GetTitle();
				return string.Format(local, title);
			}
		}
		return null;
	}
}
