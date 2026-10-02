using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_30412 : Skill
{
	public Skill_30412()
	{
		skillId = 30412;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override async UniTask SkillTrigger(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		await curPlayerData.CharacterInst.SwitchCamera();
	}

	public override void SkillDelete(long _playerId)
	{
	}

	public override bool SkillUsable(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		return curPlayerData.CharacterInst.activeSkillVail;
	}

	public override async UniTask SkillAttrChange(UpdateHeroAttrS2C model)
	{
		if (model == null)
		{
			return;
		}
		BattlePlayerData attrPlayer = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		if (attrPlayer == null || attrPlayer.CharacterInst == null)
		{
			return;
		}
		SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(attrPlayer.CharacterInst, willMove: false);
		attrPlayer.CharacterInst.characterAnimator.Move(walk: false);
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		List<long> playerIds = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetChangeAttrPlayerIds();
		for (int i = 0; i < playerIds.Count; i++)
		{
			int performId = ((playerIds[i] == model.PlayerId) ? skillConfig.PerformSelf : skillConfig.PerformTarget);
			await perform.PlayPlayerShow(playerIds[i], performId, $"被动技{skillConfig.Id} 目标演出");
			if (perform.isCancel)
			{
				return;
			}
		}
		SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(attrPlayer.CharacterInst, willMove: true);
	}
}
