using Cysharp.Threading.Tasks;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace Core.Unit;

public class CharacterShowComponent
{
	public Character Owner;

	protected const int DeadEffectId = 32;

	protected Effect DeadEffect;

	public virtual void InitComponent(Character character)
	{
		Owner = character;
	}

	public virtual string TryGetAttackTimelineAsset(MapField<int, string> attackDict)
	{
		BattleFightData battleFightData = SimpleSingletonProvider<GameLogicManager>.inst.fight.battleFightData;
		if (battleFightData == null)
		{
			return null;
		}
		if (attackDict.Count > 1 && attackDict.TryGetValue(battleFightData.attackerInfo.Point, out var value))
		{
			return value;
		}
		if (attackDict.TryGetValue(0, out var value2))
		{
			return value2;
		}
		Debug.LogError("无法获取对战资源配置中Attack资源");
		return null;
	}

	public virtual void Dispose()
	{
	}

	public async UniTask UpdateHealthStatus(bool dead)
	{
		if (dead)
		{
			await Dead();
			if (Owner != null && Owner.player != null)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.battle.TryShowFirstDeadTip(Owner.player.Id);
			}
		}
		else
		{
			await Resurrection();
		}
	}

	protected virtual async UniTask Dead()
	{
		if (Owner.player.characterType == CharacterType.Hero && Owner.canStep != 0)
		{
			SimpleSingletonProvider<RoadLineManager>.inst.DestroyRoad();
		}
		Owner.ResetStep(0);
		Owner.characterAnimator.grayed = true;
		ReleaseDeadEffect();
		await Owner.characterAnimator.Die(die: true);
		if (Owner.player.characterType == CharacterType.Hero)
		{
			DeadEffect = await Owner.PlayCharacterEffect(32);
		}
		else if (Owner.player.characterType == CharacterType.Monster)
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(Owner.player.Id, 8000300, "死亡离场");
		}
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.deadDeal.Dispatch(Owner.player.Id);
	}

	protected virtual async UniTask Resurrection()
	{
		await Owner.SwitchCamera();
		ReleaseDeadEffect();
		Owner.characterAnimator.grayed = false;
		await SimpleSingletonProvider<EffectManager>.inst.PlayById(31, Owner.transform.position, Quaternion.identity);
		await Owner.characterAnimator.Die(die: false);
	}

	protected void ReleaseDeadEffect()
	{
		if (!(DeadEffect == null))
		{
			DeadEffect.ReleaseEffect();
			DeadEffect = null;
		}
	}

	public virtual void RefreshAttrInfo()
	{
	}

	public virtual float GetMoveSpeedAdditionRate()
	{
		return 1f;
	}

	public virtual void UpdateStopStatus()
	{
	}

	public virtual void UpdateAfterReconnect()
	{
	}
}
