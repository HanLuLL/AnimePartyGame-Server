using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_101901 : Skill
{
	public Skill_101901()
	{
		skillId = 101901;
		skillConfig = skillId.GetSkillConfigure();
	}

	public override async UniTask SkillTrigger(long _playerId)
	{
		curPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		await curPlayerData.CharacterInst.SwitchCamera();
		if (!IsEatMonster(_playerId))
		{
			if (skillConfig.PerformSelfs.Count > 0)
			{
				await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(_playerId, skillConfig.PerformSelfs[0], "101901 主动技能释放 召唤小怪");
			}
		}
		else if (skillConfig.PerformSelfs.Count > 1)
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(_playerId, skillConfig.PerformSelfs[1], "101901 主动技能释放 吃掉小怪");
		}
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
		await UniTask.CompletedTask;
	}

	private bool IsEatMonster(long playerId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById == null || playerDataById.CharacterInst == null)
		{
			return false;
		}
		foreach (BattlePlayerData playerData in SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas)
		{
			if (!(playerData.CharacterInst == null) && playerData.Property.HP.Value != 0 && (playerData.player.Hero.HeroId == skillConfig.Params[1] || playerData.player.Hero.HeroId == skillConfig.Params[3]) && SimpleSingletonProvider<LandManager>.inst.CheckDistance(playerDataById.CharacterInst.standLand.Id, playerData.CharacterInst.standLand.Id, skillConfig.Params[0], 0))
			{
				return true;
			}
		}
		return false;
	}
}
