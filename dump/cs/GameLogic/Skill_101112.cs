using System.Linq;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.model;
using party.protocol;

namespace GameLogic;

public class Skill_101112 : Skill
{
	public Skill_101112()
	{
		skillId = 101112;
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
		if (model?.EffectDatas == null)
		{
			return;
		}
		HeroAttrEffect heroAttrEffect = model.EffectDatas.FirstOrDefault(delegate(HeroAttrEffect x)
		{
			Buff buff = x.Buff?.Buff;
			return buff != null && buff.BuffId == 10111202;
		});
		if (heroAttrEffect != null)
		{
			DecodeSlot(heroAttrEffect.Buff.Buff.Progress, out var l, out var l2, out var l3);
			if (l == 0 && l2 == 0 && l3 == 0)
			{
				await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(model.PlayerId, skillConfig.PerformSelf, $"主动技{skillConfig.Id} 释放者演出");
			}
		}
	}

	private void DecodeSlot(int code, out int l1, out int l2, out int l3)
	{
		l1 = code / 9;
		code %= 9;
		l2 = code / 3;
		l3 = code % 3;
	}
}
