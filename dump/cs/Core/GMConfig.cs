using System.Collections.Generic;
using Core.Net;
using Core.Unit;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using party.protocol;

namespace Core;

public static class GMConfig
{
	public static bool ClientCountDown = true;

	public static bool Tutorial = true;

	public static bool Tutorial1001 = true;

	public static bool Tutorial1002 = true;

	public static bool TutorialSingle = true;

	public static bool AFKCheck = true;

	public static int dev_MovePoint;

	public static int dev_AttackerPoint;

	public static int dev_DefenderPoint;

	public static int dev_MoveAgainPoint;

	public static int dev_BloodLossPoint;

	public static int dev_RollGoldPoint;

	public static int dev_LandEventPoint;

	public static int dev_GamblePoint;

	public static int dev_BombPoint;

	public static readonly int[] HeroStandingPainting = new int[4];

	public static bool _Enable => HackerConfig.EnableGm();

	public static void ResetAllGMParams()
	{
		dev_MovePoint = 0;
		dev_AttackerPoint = 0;
		dev_DefenderPoint = 0;
		dev_MoveAgainPoint = 0;
		dev_BloodLossPoint = 0;
		dev_RollGoldPoint = 0;
		dev_LandEventPoint = 0;
		dev_GamblePoint = 0;
		dev_BombPoint = 0;
	}

	public static void RequestChangeHP(int hp)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GmC2S.GmC2SCall(new GmC2S
		{
			Hp = hp
		});
	}

	public static void RequestChangeGold(int gold)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GmC2S.GmC2SCall(new GmC2S
		{
			Gold = gold
		});
	}

	public static void RequestChangeGameProgress(int gp)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GmC2S.GmC2SCall(new GmC2S
		{
			GameProgress = gp
		});
	}

	public static void RequestChangeGameRound(int gd)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GmC2S.GmC2SCall(new GmC2S
		{
			GameRound = gd
		});
	}

	public static void RequestChangeEvent(int EventId)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GmC2S.GmC2SCall(new GmC2S
		{
			EventId = EventId
		});
	}

	public static void RequestChangeDestiny(int DestinyId)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GmC2S.GmC2SCall(new GmC2S
		{
			DestinyId = DestinyId
		});
	}

	public static void RequestChangeDivination(int DivinationId)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GmC2S.GmC2SCall(new GmC2S
		{
			DivinationId = DivinationId
		});
	}

	public static void RequestAddCard(List<int> CardIds)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GmC2S.GmC2SCall(new GmC2S
		{
			AddCardIds = { (IEnumerable<int>)CardIds }
		});
	}

	public static void RequestRemoveCard(List<int> CardIds)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GmC2S.GmC2SCall(new GmC2S
		{
			RemoveCardIds = { (IEnumerable<int>)CardIds }
		});
	}

	public static void RequestAddRelic(int RelicId)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GmC2S.GmC2SCall(new GmC2S
		{
			RelicId = RelicId
		});
	}

	public static void RequestResetSkill(int skillId, long targetId)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GmC2S.GmC2SCall(new GmC2S
		{
			TargetId = targetId,
			SkillId = skillId
		});
	}

	public static void RequestResetRelicCount(int count)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GmC2S.GmC2SCall(new GmC2S
		{
			ReRollNum = count
		});
	}

	public static void GetTogether(int nodeId)
	{
		List<BattlePlayerData> playerDatas = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
		if (playerDatas == null)
		{
			return;
		}
		foreach (BattlePlayerData item in playerDatas)
		{
			Character characterInst = item.CharacterInst;
			characterInst.ResetFromLandId(item.CharacterInst.standLand.Id);
			characterInst.SendCharacter(nodeId, new RepeatedField<int> { item.CharacterInst.standLand.Id });
			SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(characterInst, willMove: false);
		}
	}

	public static void RequestGMForTarget(long targetId, int targetNodeId = 0, int movePoint = 0, int healthVal = 0, int attackVal = 0, int defenseVal = 0, int starLv = 0, int goldCount = 0, int diceAttackPoint = 0, int diceDefensePoint = 0, int monsterId = 0, int createMonsterNodeId = 0, int useCardNum = 0)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GmC2S.GmC2SCall(new GmC2S
		{
			TargetId = targetId,
			TargetNodeId = targetNodeId,
			MovePoint = movePoint,
			HealthVal = healthVal,
			AttackVal = attackVal,
			DefenseVal = defenseVal,
			StarLv = starLv,
			GoldCount = goldCount,
			DiceAttackPoint = diceAttackPoint,
			DiceDefensePoint = diceDefensePoint,
			MonsterId = monsterId,
			CreateMonsterNodeId = createMonsterNodeId,
			UseCardNum = useCardNum
		});
	}

	public static void RequestGMBuffForTarget(long targetId, int buffId, int sourceSkillId, int sourceCardId)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GmC2S.GmC2SCall(new GmC2S
		{
			TargetId = targetId,
			BuffId = buffId,
			SourceSkillId = sourceSkillId,
			SourceCardId = sourceCardId
		});
	}

	public static void RequestGMTermIds(int[] termIds)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GmC2S.GmC2SCall(new GmC2S
		{
			TermIds = { (IEnumerable<int>)termIds }
		});
	}

	public static void RequestGMDifficultyId(int difficultyId)
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GmC2S.GmC2SCall(new GmC2S
		{
			MapDifficultyId = difficultyId
		});
	}
}
