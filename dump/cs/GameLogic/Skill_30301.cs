using Cysharp.Threading.Tasks;
using Tools;
using UI;

namespace GameLogic;

public class Skill_30301 : Skill
{
	public Skill_30301()
	{
		skillId = 30301;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override void SkillReleaseAction(long Sn)
	{
		int cardCount = skillConfig.Params[0];
		SimpleSingletonProvider<UIManager>.inst.loseCard.ShowMixCard(Sn, skillId, cardCount).Forget();
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
		int cardCount = curPlayerData.cardContainer.CardCount;
		int num = skillConfig.Params[0];
		if (curPlayerData.CharacterInst.activeSkillVail)
		{
			return cardCount >= num;
		}
		return false;
	}
}
