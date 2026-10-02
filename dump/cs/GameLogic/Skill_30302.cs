using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;

namespace GameLogic;

public class Skill_30302 : Skill
{
	public Skill_30302()
	{
		skillId = 30302;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override void SkillReleaseAction(long Sn)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction = PlayerActionEnum.CHOOSETARGET_SKILL;
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.selectPlayer.Dispatch(skillId, GetTargetPlayers(), 1);
	}

	public override void RequestReleaseSkillBySelectTarget(long actionSn, List<long> targetIds)
	{
		int cardCount = skillConfig.Params[0];
		SimpleSingletonProvider<UIManager>.inst.loseCard.ShowMixCard(actionSn, skillId, cardCount, targetIds).Forget();
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
		SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		foreach (BattlePlayerData playerData in SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas)
		{
			if (playerData.characterType != CharacterType.Monster && playerData.Property.HP.Value != 0)
			{
				repeatedField.Add(playerData.player.Id);
			}
		}
		return repeatedField;
	}

	public override bool SkillUsable(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		int cardCount = curPlayerData.cardContainer.CardCount;
		int num = skillConfig.Params[0];
		if (curPlayerData.CharacterInst.activeSkillVail)
		{
			return cardCount >= num;
		}
		return false;
	}
}
