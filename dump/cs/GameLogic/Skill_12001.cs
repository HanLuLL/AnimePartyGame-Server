using Core.Unit;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_12001 : Skill
{
	public Skill_12001()
	{
		skillId = 12001;
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
		await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(_playerId, skillConfig.PerformSelf, "120主动技能释放");
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
			if (!(selfPlayerData.CharacterInst == null) && !(playerData.CharacterInst == null) && playerData.characterType != CharacterType.Monster && playerData.Property.HP.Value != 0 && playerData.CharacterInst.standLand.LandType != LandType.Hospital && !playerData.buffContainer.Contain(1030101) && selfPlayerData.player.Id != playerData.player.Id && selfPlayerData.player.TeamId != playerData.player.TeamId && !(selfPlayerData.CharacterInst == null) && !(playerData.CharacterInst == null) && !SimpleSingletonProvider<LandManager>.inst.CheckDistance(selfPlayerData.CharacterInst.standLand.Id, playerData.CharacterInst.standLand.Id, skillConfig.Params[0], 0))
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
		await UniTask.CompletedTask;
	}
}
