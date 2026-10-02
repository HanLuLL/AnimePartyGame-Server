using Core;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.model;
using party.protocol;

namespace GameLogic;

public class Skill_102701 : Skill
{
	private Effect _skillEffectLowProgress;

	private Effect _skillEffectHighProgress;

	public Skill_102701()
	{
		skillId = 102701;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override void InitSkill(Character owner)
	{
		base.InitSkill(owner);
		RefreshEffectByBuff();
		SimpleSingletonProvider<GameLogicManager>.inst.buff.buff.AddListener(RefreshEffectByBuff);
	}

	public override void DisposeSkill()
	{
		ReleaseEffect();
		SimpleSingletonProvider<GameLogicManager>.inst.buff.buff.RemoveListener(RefreshEffectByBuff);
	}

	private void ReleaseEffect()
	{
		if (_skillEffectHighProgress != null)
		{
			_skillEffectHighProgress.ReleaseEffect();
		}
		if (_skillEffectLowProgress != null)
		{
			_skillEffectLowProgress.ReleaseEffect();
		}
		_skillEffectHighProgress = null;
		_skillEffectLowProgress = null;
	}

	private async void RefreshEffectByBuff()
	{
		BuffContainer buffContainer = Owner.player.buffContainer;
		SkillInfoConfigure skillConfigure = 102711.GetSkillConfigure();
		if (buffContainer == null || skillConfigure.BuffId.Count <= 0)
		{
			return;
		}
		Buff buff = buffContainer.GetBuff(skillConfigure.BuffId[0]);
		if (buff != null)
		{
			if (buff.Progress >= 5)
			{
				if (_skillEffectHighProgress == null)
				{
					_skillEffectHighProgress = await Owner.PlayCharacterEffect(102701);
				}
				if (_skillEffectLowProgress != null)
				{
					_skillEffectLowProgress.ReleaseEffect();
					_skillEffectLowProgress = null;
				}
			}
			else
			{
				if (_skillEffectLowProgress == null)
				{
					_skillEffectLowProgress = await Owner.PlayCharacterEffect(102702);
				}
				if (_skillEffectHighProgress != null)
				{
					_skillEffectHighProgress.ReleaseEffect();
					_skillEffectHighProgress = null;
				}
			}
		}
		else
		{
			ReleaseEffect();
		}
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
		await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(model.PlayerId, skillConfig.PerformSelf, "召唤1只小鲤鱼");
	}
}
