using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public abstract class Skill_129_Passive : Skill
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
		ActionEffectShow actionEffectShow = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			actionEffectShow.PlayPlayerShow(effectData.PlayerId, skillConfig.PerformTargets[1], "技能演出 - 塞克斯被动技能 目标玩家").Forget();
		}
		if (StaticConfigure.Perform.InfoDict.TryGetValue(skillConfig.PerformTargets[1], out var value))
		{
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(value.TotalTime);
		}
	}
}
