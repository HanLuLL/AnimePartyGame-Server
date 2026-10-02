using Google.Protobuf.Collections;
using Tools;
using party.model;

namespace GameLogic;

public class BattleFightData
{
	public bool isReConnect;

	public long battleId;

	public BattleRole attackerInfo;

	public BattleRole defenderInfo;

	public MapField<long, bool> isFinishCard;

	public bool isEnd;

	public bool fightBack;

	public void UpdateData(Battle _battleInfo, bool _isReConnect)
	{
		battleId = _battleInfo.BattleId;
		attackerInfo = _battleInfo.Attacker;
		defenderInfo = _battleInfo.Defender;
		isFinishCard = _battleInfo.CardUseState;
		isEnd = _battleInfo.IsEnd;
		fightBack = _battleInfo.FightBack;
		isReConnect = _isReConnect;
	}

	public void TutorialUpdatePKAtk(long playerId, int ChangeAtk)
	{
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		if (roomInfo != null && roomInfo.MapType == 10 && attackerInfo != null && attackerInfo.PlayerId == playerId)
		{
			attackerInfo.Atk += ChangeAtk;
			attackerInfo.InitAtk += ChangeAtk;
			attackerInfo.MaxAtk += ChangeAtk;
			attackerInfo.MinAtk += ChangeAtk;
		}
	}
}
