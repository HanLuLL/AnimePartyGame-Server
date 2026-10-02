using System.Collections.Generic;
using Core;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;
using party.protocol;

namespace GameLogic;

public abstract class Skill
{
	public int skillId;

	public SkillInfoConfigure skillConfig;

	protected BattlePlayerData curPlayerData;

	protected Character Owner;

	public virtual void InitSkill(Character owner)
	{
		Owner = owner;
	}

	public virtual void DisposeSkill()
	{
	}

	public abstract UniTask SkillTrigger(long _playerId);

	public abstract void SkillDelete(long _playerId);

	public abstract bool SkillUsable(long _playerId);

	public virtual async UniTask SkillAttrChange(UpdateHeroAttrS2C model)
	{
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		await perform.PlayPlayerShow(model.PlayerId, skillConfig.PerformSelf, $"主动技{skillConfig.Id} 释放者演出");
		if (perform.isCancel)
		{
			return;
		}
		List<long> playerIds = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetChangeAttrPlayerIds();
		for (int i = 0; i < playerIds.Count; i++)
		{
			await perform.PlayPlayerShow(playerIds[i], skillConfig.PerformTarget, $"主动技{skillConfig.Id} 目标演出");
			if (perform.isCancel)
			{
				break;
			}
		}
	}

	public virtual float MoveEffectSpeed(long playerId)
	{
		return 1f;
	}

	public virtual void SkillReleaseAction(long actionSn)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN = 0L;
		SimpleSingletonProvider<GameLogicManager>.inst.card.RequestReleaseSkillC2S(actionSn, skillConfig.Id);
	}

	public virtual void RequestReleaseSkillBySelectTarget(long actionSn, List<long> targetIds)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.action.CardSN = 0L;
		SimpleSingletonProvider<GameLogicManager>.inst.card.RequestReleaseSkillC2S(actionSn, skillConfig.Id, targetIds);
	}

	protected bool IsVailMonster(BattlePlayerData data)
	{
		if (data.CharacterInst == null)
		{
			return false;
		}
		if (data.characterType == CharacterType.Hero)
		{
			return false;
		}
		if (data.Property.NotSelect.Value)
		{
			return false;
		}
		if (data.Property.HP.Value == 0)
		{
			return false;
		}
		return true;
	}
}
