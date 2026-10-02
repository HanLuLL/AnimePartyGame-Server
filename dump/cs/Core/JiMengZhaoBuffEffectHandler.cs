using System.Collections.Generic;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;
using party.model;

namespace Core;

public class JiMengZhaoBuffEffectHandler : IBuffEffectHandler
{
	private readonly Dictionary<long, Effect> buffEffectDict = new Dictionary<long, Effect>();

	public async UniTask Play(Buff buff, Character target)
	{
		BuffInfoConfigure buffConfigure = buff.BuffId.GetBuffConfigure();
		BattlePlayerData playerDataByHeroId = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataByHeroId(123);
		if (playerDataByHeroId == null || !buffConfigure.SkinReplaceEffectID.TryGetValue(playerDataByHeroId.player.standingPainting.ItemID, out var value))
		{
			value = buffConfigure.EffectID;
		}
		if (value != 0)
		{
			Effect effect = await SimpleSingletonProvider<EffectManager>.inst.PlayById(value, Vector3.zero, Quaternion.identity, target.EffectContainer);
			if (effect != null)
			{
				buffEffectDict.TryAdd(buff.UniqueId, effect);
			}
		}
	}

	public UniTask UpdateEffect(Buff buff, Character target)
	{
		return UniTask.CompletedTask;
	}

	public void DestroyEffect(Buff buff, Character target)
	{
		if (buffEffectDict.TryGetValue(buff.UniqueId, out var value) && value != null)
		{
			value.ReleaseEffect();
			buffEffectDict.Remove(buff.UniqueId);
		}
	}

	public void Dispose()
	{
		buffEffectDict.Clear();
	}
}
