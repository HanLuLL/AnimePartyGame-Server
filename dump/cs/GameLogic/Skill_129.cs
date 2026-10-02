using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public abstract class Skill_129 : Skill
{
	public bool TriggerSkill;

	private List<int> targetLandIds;

	protected void InitConfig(int id)
	{
		skillId = id;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override async void SkillReleaseAction(long Sn)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction = PlayerActionEnum.CHOOSETARGET_SKILL;
		(await SimpleSingletonProvider<UIManager>.inst.cardWindow.ShowCard()).RefreshSkill_SelectLand(_obstacleRange: skillConfig.Params.GetSafeByIndex(0), skillId: skillId, _obstacleNum: 1, _Sn: Sn, _onSelectLand: OnSelectLands);
	}

	public override async UniTask SkillTrigger(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		await curPlayerData.CharacterInst.SwitchCamera();
		await SimpleSingletonProvider<UIManager>.inst.skill.ShowSkillWin(curPlayerData.player.Id);
		TriggerSkill = true;
	}

	public override void SkillDelete(long _playerId)
	{
	}

	public override bool SkillUsable(long _playerId)
	{
		TriggerSkill = false;
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		return curPlayerData.CharacterInst.activeSkillVail;
	}

	private void OnSelectLands(List<int> landIds)
	{
		targetLandIds = landIds;
	}

	public override async UniTask SkillAttrChange(UpdateHeroAttrS2C model)
	{
		await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(model.PlayerId, skillConfig.PerformSelf, "129主动技能 自己表现");
		List<int> list = targetLandIds;
		if (list != null && list.Count > 0)
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayLandShow(targetLandIds[0], skillConfig.PerformTargets[0], "129主动技能 地图格表现");
		}
	}
}
