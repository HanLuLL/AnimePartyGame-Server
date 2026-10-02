using Core;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;

namespace GameLogic;

public class Skill_102601 : Skill
{
	private Effect _skillEffect;

	public Skill_102601()
	{
		skillId = 102601;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override void InitSkill(Character owner)
	{
		base.InitSkill(owner);
		if (Owner != null && Owner.player != null && Owner.player.Property != null)
		{
			Owner.player.Property.activeSkillCD.AddListener(RefreshEffectByCD);
			Owner.player.Property.ChangeHP.AddListener(RefreshEffectByHPChange, excuteImmediately: false);
		}
	}

	private void RefreshEffectByHPChange(int changeHp)
	{
		SkillInfoConfigure skillConfigure = 102611.GetSkillConfigure();
		if (skillConfigure.Params.Count > 2 && Mathf.Abs(changeHp) > skillConfigure.Params[2])
		{
			ReleaseEffect();
		}
	}

	private async void RefreshEffectByCD(int cd)
	{
		if (cd < 1)
		{
			if (_skillEffect == null)
			{
				_skillEffect = await Owner.PlayCharacterEffect(102602);
			}
		}
		else
		{
			ReleaseEffect();
		}
	}

	private void ReleaseEffect()
	{
		if (_skillEffect != null)
		{
			_skillEffect.ReleaseEffect();
		}
		_skillEffect = null;
	}

	public override void DisposeSkill()
	{
		ReleaseEffect();
		if (Owner != null && Owner.player != null && Owner.player.Property != null)
		{
			Owner.player.Property.activeSkillCD.RemoveListener(RefreshEffectByCD);
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
}
