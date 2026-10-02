using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;
using party.protocol;

namespace GameLogic;

public abstract class Skill_119 : Skill
{
	protected void InitConfig(int id)
	{
		skillId = id;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override async UniTask SkillTrigger(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		await curPlayerData.CharacterInst.SwitchCamera();
		await SimpleSingletonProvider<UIManager>.inst.skill.ShowSkillWin(curPlayerData.player.Id);
		await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(curPlayerData.player.Id, skillConfig.PerformSelf, "技能演出 释放");
		curPlayerData.CharacterInst.characterAnimator.Move(walk: true, 1f, -1);
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
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		if (playerDataById == null)
		{
			Debug.LogError($"无法通过属性变更数据model.PlayerId：{model.PlayerId}， 获取对应玩家的数据");
			return;
		}
		int performTargetId = skillConfig.PerformTarget;
		SkinStandingPaintingConfigureItem standingPainting = playerDataById.player.standingPainting;
		if (standingPainting != null && standingPainting.ItemID == 100119004)
		{
			performTargetId = 11903;
		}
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		List<long> playerIds = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetChangeAttrPlayerIds();
		for (int i = 0; i < playerIds.Count; i++)
		{
			if (playerIds[i] != model.PlayerId)
			{
				await perform.PlayPlayerShow(playerIds[i], performTargetId, $"主动技{skillConfig.Id} 目标演出");
				if (perform.isCancel)
				{
					break;
				}
			}
		}
	}
}
