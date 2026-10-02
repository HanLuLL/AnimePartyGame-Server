using System.Collections.Generic;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using party.model;

namespace Core;

public class SoulLinkBuffEffectHandler : IBuffEffectHandler
{
	private readonly Dictionary<string, SoulLinkEffect> soulLinkDict = new Dictionary<string, SoulLinkEffect>();

	public async UniTask Play(Buff buff, Character target)
	{
		if (!soulLinkDict.ContainsKey(buff.RelationId))
		{
			BattlePlayerData p1 = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(target.player.Id);
			BattlePlayerData p2 = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(buff.TargetIds[0]);
			if (p1 != null && p2 != null && p1.CharacterInst != null && p2.CharacterInst != null)
			{
				SoulLinkEffect component = (await SimpleSingletonProvider<EffectManager>.inst.GetEffectInstance(2000700, null)).GetComponent<SoulLinkEffect>();
				component.UpdateTarget(p1.CharacterInst.gameObject, p2.CharacterInst.gameObject);
				soulLinkDict[buff.RelationId] = component;
			}
		}
	}

	public void DestroyEffect(Buff buff, Character target)
	{
		if (soulLinkDict.TryGetValue(buff.RelationId, out var value) && value != null)
		{
			SimpleSingletonProvider<EffectManager>.inst.Stop(2000700, value.gameObject);
			soulLinkDict.Remove(buff.RelationId);
		}
	}

	public void Dispose()
	{
		soulLinkDict.Clear();
	}

	public UniTask UpdateEffect(Buff buff, Character target)
	{
		return UniTask.CompletedTask;
	}
}
