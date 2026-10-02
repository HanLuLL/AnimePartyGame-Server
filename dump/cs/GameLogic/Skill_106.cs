using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;
using party.protocol;

namespace GameLogic;

public class Skill_106 : Skill
{
	private const int SpecialSkinItemId = 100106004;

	protected void InitConfig(int id)
	{
		skillId = id;
		skillConfig = skillId.GetSkillConfigure();
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
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		if (playerDataById == null)
		{
			Debug.LogError($"无法通过属性变更数据model.PlayerId：{model.PlayerId}， 获取对应玩家的数据");
			await UniTask.CompletedTask;
		}
		else
		{
			SkinStandingPaintingConfigureItem standingPainting = playerDataById.player.standingPainting;
			int performId = ((standingPainting != null && standingPainting.ItemID == 100106004) ? 10605 : skillConfig.PerformSelf);
			await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(model.PlayerId, performId, "106主动技能释放");
		}
	}
}
