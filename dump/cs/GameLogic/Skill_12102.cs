using System.Collections.Generic;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_12102 : Skill
{
	private int _CurrentAddDamage;

	public Skill_12102()
	{
		skillId = 12102;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override void InitSkill(Character owner)
	{
		base.InitSkill(owner);
		_CurrentAddDamage = 0;
		UpdateCardDesc();
		SimpleSingletonProvider<GameLogicManager>.inst.buff.buff.AddListener(UpdateCardDesc);
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
		SkinStandingPaintingConfigureItem standingPainting = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId).player.standingPainting;
		int performId = skillConfig.PerformSelf;
		if (standingPainting != null && standingPainting.ItemID == 100121005)
		{
			performId = 12101;
		}
		await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(model.PlayerId, performId, "121 PVE主动技能释放");
	}

	public override void DisposeSkill()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.buff.buff.RemoveListener(UpdateCardDesc);
	}

	private void UpdateCardDesc()
	{
		BuffContainer buffContainer = Owner.player.buffContainer;
		long id = Owner.player.Id;
		SkillInfoConfigure skillConfigure = 12112.GetSkillConfigure();
		if (buffContainer == null || buffContainer._buffDict.Count <= 0 || skillConfig.BuffId.Count <= 0)
		{
			return;
		}
		int num = skillConfigure.BuffId[0];
		foreach (var (_, buff2) in buffContainer._buffDict)
		{
			if (buff2.BuffId == num)
			{
				if (_CurrentAddDamage != buff2.Progress)
				{
					_CurrentAddDamage = buff2.Progress;
					SimpleSingletonProvider<GameLogicManager>.inst.card.signal.resetSelfCardList.Dispatch(id);
				}
				break;
			}
		}
	}
}
