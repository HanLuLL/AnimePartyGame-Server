using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_30512 : Skill
{
	public Skill_30512()
	{
		skillId = 30512;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override async UniTask SkillTrigger(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		if (!(curPlayerData?.CharacterInst == null))
		{
			await curPlayerData.CharacterInst.SwitchCamera();
		}
	}

	public override void SkillDelete(long _playerId)
	{
	}

	public override bool SkillUsable(long _playerId)
	{
		return true;
	}

	public override async UniTask SkillAttrChange(UpdateHeroAttrS2C model)
	{
		if (model == null || skillConfig == null)
		{
			return;
		}
		BattlePlayerData ownerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		List<long> changeAttrPlayerIds = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetChangeAttrPlayerIds();
		if (changeAttrPlayerIds == null)
		{
			return;
		}
		long playerId = changeAttrPlayerIds[0];
		foreach (long item in changeAttrPlayerIds)
		{
			if (item != model.PlayerId)
			{
				playerId = item;
			}
		}
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById != null)
		{
			int performIndex = GetPerformIndex(CheckLinkageDiff(playerDataById), CheckIsAwakening(ownerData), CheckTalenting(ownerData));
			if (performIndex >= 0)
			{
				ActionEffectShow actionEffectShow = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
				actionEffectShow.PlayPlayerShow(model.PlayerId, skillConfig.PerformSelfs[performIndex], $"被动技{skillConfig.Id} 自身演出").Forget();
				await actionEffectShow.PlayPlayerShow(playerId, skillConfig.PerformTargets[performIndex], $"被动技{skillConfig.Id} 目标演出");
				SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(ownerData.CharacterInst, willMove: true);
			}
		}
	}

	private bool CheckLinkageDiff(BattlePlayerData targetData)
	{
		RoomPlayer player = targetData.player;
		if (player == null)
		{
			return false;
		}
		return player.Hero?.HeroId == 306;
	}

	private bool CheckIsAwakening(BattlePlayerData selfData)
	{
		return selfData?.player?.buffContainer?.GetBuff(3051202) != null;
	}

	private bool CheckTalenting(BattlePlayerData selfData)
	{
		return selfData?.player?.buffContainer?.GetBuff(skillConfig.BuffId[0]) != null;
	}

	private int GetPerformIndex(bool linkageDiff, bool isAwakening, bool isTalenting)
	{
		if (linkageDiff && isAwakening)
		{
			return 3;
		}
		if (linkageDiff)
		{
			return 2;
		}
		if (isAwakening)
		{
			return 1;
		}
		if (isTalenting)
		{
			return 0;
		}
		return -1;
	}
}
