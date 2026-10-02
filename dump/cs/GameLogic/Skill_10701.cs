using System.Collections.Generic;
using Core;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_10701 : Skill
{
	public Skill_10701()
	{
		skillId = 10701;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override void SkillReleaseAction(long Sn)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction = PlayerActionEnum.CHOOSETARGET_SKILL;
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.selectPlayer.Dispatch(skillId, GetTargetPlayers(), skillConfig.Params[1]);
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
			if (playerData.characterType != CharacterType.Monster && playerData.Property.HP.Value != 0 && playerData.cardContainer.CardCount != 0 && selfPlayerData.player.Id != playerData.player.Id && selfPlayerData.player.TeamId != playerData.player.TeamId && !(selfPlayerData.CharacterInst == null) && !(playerData.CharacterInst == null) && SimpleSingletonProvider<LandManager>.inst.CheckDistance(selfPlayerData.CharacterInst.standLand.Id, playerData.CharacterInst.standLand.Id, skillConfig.Params[0], 0))
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
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		List<long> _list = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetChangeAttrPlayerIds();
		for (int i = 0; i < _list.Count; i++)
		{
			if (_list[i] != model.PlayerId)
			{
				await perform.PlayPlayerShow(_list[i], skillConfig.PerformTarget, "107主动技能目标玩家");
				if (perform.isCancel)
				{
					return;
				}
			}
		}
		if (model.PlayerId == curPlayerData.player.Id)
		{
			await perform.PlayPlayerShow(model.PlayerId, skillConfig.PerformSelf, "107主动技能释放");
		}
	}
}
