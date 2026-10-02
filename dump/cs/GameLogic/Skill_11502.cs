using System.Collections.Generic;
using Core;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.protocol;

namespace GameLogic;

public class Skill_11502 : Skill
{
	public Skill_11502()
	{
		skillId = 11502;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override void SkillReleaseAction(long Sn)
	{
		SimpleSingletonProvider<UIManager>.inst.battleSelectMonster.ShowSkillVailMonsterTarget(GetTargetPlayers(), skillId, skillConfig.Params[1], Sn);
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

	private RepeatedField<long> GetTargetPlayers()
	{
		RepeatedField<long> repeatedField = new RepeatedField<long>();
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		foreach (BattlePlayerData playerData in SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas)
		{
			if (IsVailMonster(playerData) && !(selfPlayerData.CharacterInst == null) && selfPlayerData.player.TeamId != playerData.player.TeamId && SimpleSingletonProvider<LandManager>.inst.CheckDistance(selfPlayerData.CharacterInst.standLand.Id, playerData.CharacterInst.standLand.Id, skillConfig.Params[0], 0))
			{
				repeatedField.Add(playerData.player.Id);
			}
		}
		return repeatedField;
	}

	public override bool SkillUsable(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		if (curPlayerData.CharacterInst.activeSkillVail)
		{
			return GetTargetPlayers().Count > 0;
		}
		return false;
	}

	public override async UniTask SkillAttrChange(UpdateHeroAttrS2C model)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		if (playerDataById == null)
		{
			Debug.LogError($"无法通过属性变更数据model.PlayerId：{model.PlayerId}， 获取对应玩家的数据");
			await UniTask.CompletedTask;
			return;
		}
		SkinStandingPaintingConfigureItem standingPainting = playerDataById.player.standingPainting;
		if (standingPainting == null || standingPainting.ItemID != 100115004)
		{
			await base.SkillAttrChange(model);
			return;
		}
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		await perform.PlayPlayerShow(model.PlayerId, 11503, "太刀 特殊皮100115004演出 出刀起手演出");
		if (perform.isCancel)
		{
			return;
		}
		List<long> playerIds = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetChangeAttrPlayerIds();
		for (int i = 0; i < playerIds.Count; i++)
		{
			await perform.PlayPlayerShow(playerIds[i], 11504, "太刀 特殊皮100115004演出 目标演出");
			if (perform.isCancel)
			{
				return;
			}
		}
		await perform.PlayPlayerShow(model.PlayerId, 11505, "太刀 特殊皮100115004演出 收刀演出");
	}
}
