using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public abstract class Skill_305 : Skill
{
	protected void InitConfig(int temporarySkillId)
	{
		skillId = temporarySkillId;
		skillConfig = temporarySkillId.GetSkillConfigure();
	}

	public override void SkillReleaseAction(long actionSn)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN = 0L;
		SimpleSingletonProvider<GameLogicManager>.inst.card.RequestReleaseSkillC2S(actionSn, skillConfig?.Id ?? skillId);
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
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		return curPlayerData.CharacterInst.activeSkillVail;
	}

	public override async UniTask SkillAttrChange(UpdateHeroAttrS2C model)
	{
		await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(curPlayerData.player.Id, skillConfig.PerformSelf, "技能演出 释放");
		curPlayerData.CharacterInst.characterAnimator.Move(walk: true, 1f, -1);
	}
}
