using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_12002 : Skill
{
	public Skill_12002()
	{
		skillId = 12002;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override void SkillReleaseAction(long Sn)
	{
		SimpleSingletonProvider<UIManager>.inst.battleSelectMonster.ShowSkillVailMonsterTarget(GetTargetPlayers(), skillId, 1, Sn);
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
			if (IsVailMonster(playerData) && playerData.CharacterInst.standLand.LandType != LandType.Hospital && selfPlayerData.player.TeamId != playerData.player.TeamId)
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
