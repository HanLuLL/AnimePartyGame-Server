using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Skill_106112 : Skill
{
	public Skill_106112()
	{
		skillId = 106112;
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
		RepeatedField<int> termIds = null;
		foreach (HeroAttrEffect effectData in model.EffectDatas)
		{
			if (effectData.AddTerm != null)
			{
				termIds = effectData.AddTerm.TermIds;
				break;
			}
		}
		if (termIds != null && termIds.Count != 0)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.TryAddRoomTerms(termIds);
			await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(model.PlayerId, skillConfig.PerformSelf, "技能演出 - 赛事主持");
			await SimpleSingletonProvider<UIManager>.inst.RoomTerms.ShowAddTremWindow(termIds[0]);
		}
	}
}
