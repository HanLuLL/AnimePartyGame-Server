using Core;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.protocol;

namespace GameLogic;

public class Skill_10903 : Skill
{
	private UniTaskCompletionSource skillTcs;

	private HeroAttrEffect hpData;

	private HeroAttrEffect placeData;

	public Skill_10903()
	{
		skillId = 10903;
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
		skillTcs = new UniTaskCompletionSource();
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			if (effectData.Place != null)
			{
				placeData = effectData;
			}
			else
			{
				hpData = effectData;
			}
		}
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		await perform.PlayPlayerShow(curPlayerData.player.Id, skillConfig.PerformSelf, "109主动技能释放", ignoreDuration: true);
		if (perform.isCancel)
		{
			skillTcs.TrySetResult();
		}
		await skillTcs.Task;
	}

	public async void TriggerSkillShow(Vector3 offsetVector3)
	{
		await SimpleSingletonProvider<DustbinManager>.inst.TriggerDustbin(curPlayerData.player.Id, placeData.PlayerId, offsetVector3);
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(placeData.PlayerId);
		playerDataById.CharacterInst.SendCharacter(placeData.Place.Place.NodeId, placeData.Place.Place.FrontNodeIds);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(playerDataById.CharacterInst, willMove: false);
		curPlayerData.CharacterInst.characterAnimator.TriggerAnime("Talent_End");
		if (hpData != null)
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(hpData.PlayerId, skillConfig.PerformTarget, "109主动技能受激目标");
		}
		skillTcs.TrySetResult();
	}
}
