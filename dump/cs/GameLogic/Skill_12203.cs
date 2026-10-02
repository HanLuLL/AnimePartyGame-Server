using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_12203 : Skill_122
{
	public Skill_12203()
	{
		skillId = 12203;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override async void SkillReleaseAction(long Sn)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction = PlayerActionEnum.CHOOSETARGET_SKILL;
		(await SimpleSingletonProvider<UIManager>.inst.cardWindow.ShowCard()).RefreshSkill_SelectLand(_obstacleRange: skillConfig.Params.GetSafeByIndex(5), skillId: skillId, _obstacleNum: 1, _Sn: SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN);
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

	public override async UniTask SkillAttrChange(UpdateHeroAttrS2C model)
	{
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		bool isPlayAttackShow = false;
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			if (effectData.Place != null)
			{
				BattlePlayerData playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(effectData.Place.PlayerId);
				await perform.PlayPlayerShow(model.PlayerId, skillConfig.PerformSelfs.GetSafeByIndex(1), "传送 开始", ignoreDuration: false, effectData);
				if (perform.isCancel)
				{
					return;
				}
				playerData.CharacterInst.SendCharacter(effectData.Place.Place.NodeId, effectData.Place.Place.FrontNodeIds);
				await perform.PlayPlayerShow(model.PlayerId, skillConfig.PerformSelfs.GetSafeByIndex(2), "传送 结束", ignoreDuration: false, effectData);
				if (perform.isCancel)
				{
					return;
				}
				SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(playerData.CharacterInst, willMove: false);
			}
			if (!isPlayAttackShow)
			{
				isPlayAttackShow = effectData.Hp != null;
			}
		}
		if (isPlayAttackShow)
		{
			await perform.PlayPlayerShow(model.PlayerId, skillConfig.PerformSelf, "122主动技能 自己表现");
			if (perform.isCancel)
			{
				return;
			}
		}
		await PlayAttackPerform(model);
	}
}
