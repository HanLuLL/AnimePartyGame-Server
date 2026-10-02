using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_12302 : Skill
{
	public Skill_12302()
	{
		skillId = 12302;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override void SkillReleaseAction(long Sn)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction = PlayerActionEnum.CHOOSETARGET_SKILL;
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.selectPlayer.Dispatch(skillId, GetTargetPlayers(), 1);
	}

	public override async UniTask SkillTrigger(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		await curPlayerData.CharacterInst.SwitchCamera();
		await SimpleSingletonProvider<UIManager>.inst.skill.ShowSkillWin(curPlayerData.player.Id);
		await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(_playerId, skillConfig.PerformSelf, "123主动技能释放");
	}

	private RepeatedField<long> GetTargetPlayers()
	{
		RepeatedField<long> repeatedField = new RepeatedField<long>();
		foreach (BattlePlayerData playerData in SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas)
		{
			if (playerData.characterType != CharacterType.Monster && playerData.Property.HP.Value != 0)
			{
				repeatedField.Add(playerData.player.Id);
			}
		}
		return repeatedField;
	}

	public override void SkillDelete(long _playerId)
	{
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
			await perform.PlayPlayerShow(_list[i], skillConfig.PerformTarget, "123主动技能目标玩家");
			if (perform.isCancel)
			{
				break;
			}
		}
	}
}
