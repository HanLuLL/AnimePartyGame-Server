using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_104501 : Skill
{
	public Skill_104501()
	{
		skillId = 104501;
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
		return true;
	}

	public override async UniTask SkillAttrChange(UpdateHeroAttrS2C model)
	{
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		await perform.PlayPlayerShow(model.PlayerId, skillConfig.PerformSelf, $"主动技{skillConfig.Id} 释放者演出");
		SimpleSingletonProvider<GameLogicManager>.inst.battle.GetChangeAttrPlayerIds();
		for (int i = 0; i < model.EffectDatas.Count; i++)
		{
			HeroPlaceChangeS2C place = model.EffectDatas[i].Place;
			if (place != null)
			{
				BattlePlayerData sendPlayer = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(place.PlayerId);
				SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(sendPlayer.CharacterInst, willMove: true);
				await perform.PlayPlayerShow(place.PlayerId, skillConfig.PerformTargets[0], $"主动技{skillConfig.Id}传送开始");
				if (perform.isCancel)
				{
					break;
				}
				SimpleSingletonProvider<GameLogicManager>.inst.battle.TakeOutHeroAttr(model.EffectDatas[i].PlayerId);
				sendPlayer.CharacterInst.SendCharacter(model.EffectDatas[i].Place.Place.NodeId, model.EffectDatas[i].Place.Place.FrontNodeIds);
				await perform.PlayPlayerShow(place.PlayerId, skillConfig.PerformTargets[1], $"主动技{skillConfig.Id}传送结束");
				if (perform.isCancel)
				{
					break;
				}
				SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(sendPlayer.CharacterInst, willMove: false);
			}
		}
	}
}
