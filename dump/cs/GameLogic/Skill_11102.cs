using System.Collections.Generic;
using System.Linq;
using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_11102 : Skill
{
	public Skill_11102()
	{
		skillId = 11102;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override void SkillReleaseAction(long Sn)
	{
		int safeByIndex = skillConfig.Params.GetSafeByIndex(0);
		SimpleSingletonProvider<UIManager>.inst.battleSelectMonster.ShowSkillVailSummonTarget(safeByIndex, skillId, 1, Sn);
	}

	public override void RequestReleaseSkillBySelectTarget(long actionSn, List<long> targetIds)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN = 0L;
		SimpleSingletonProvider<GameLogicManager>.inst.card.RequestReleaseSkillC2S(actionSn, skillConfig.Id, null, null, targetIds);
	}

	public override async UniTask SkillTrigger(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		await curPlayerData.CharacterInst.SwitchCamera();
		await SimpleSingletonProvider<UIManager>.inst.skill.ShowSkillWin(curPlayerData.player.Id);
	}

	public override void SkillDelete(long _playerId)
	{
	}

	public override bool SkillUsable(long _playerId)
	{
		int safeByIndex = skillConfig.Params.GetSafeByIndex(0);
		List<LandBuffData> summonById = SimpleSingletonProvider<GameLogicManager>.inst.battle.summon.GetSummonById(safeByIndex);
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		if (curPlayerData.CharacterInst.activeSkillVail)
		{
			return summonById.Count > 0;
		}
		return false;
	}

	public override async UniTask SkillAttrChange(UpdateHeroAttrS2C model)
	{
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		BattlePlayerData playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(playerData.CharacterInst, willMove: true);
		HeroAttrEffect placeAttr = model.EffectDatas.FirstOrDefault((HeroAttrEffect x) => x.Place != null);
		if (placeAttr == null)
		{
			return;
		}
		await perform.PlayPlayerShow(model.PlayerId, skillConfig.PerformSelfs.GetSafeByIndex(1), "传送 开始", ignoreDuration: false, placeAttr);
		if (!perform.isCancel)
		{
			playerData.CharacterInst.SendCharacter(placeAttr.Place.Place.NodeId, placeAttr.Place.Place.FrontNodeIds);
			await perform.PlayPlayerShow(model.PlayerId, skillConfig.PerformSelfs.GetSafeByIndex(0), "传送 结束", ignoreDuration: false, placeAttr);
			if (!perform.isCancel)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(playerData.CharacterInst, willMove: false);
				playerData.CharacterInst.ResetFromLandId(-1);
				playerData.CharacterInst.ShowWalkDirections(null);
			}
		}
	}
}
