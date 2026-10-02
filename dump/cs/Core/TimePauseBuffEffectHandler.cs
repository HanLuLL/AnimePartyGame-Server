using System.Collections.Generic;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;
using party.model;

namespace Core;

public class TimePauseBuffEffectHandler : IBuffEffectHandler
{
	private readonly Dictionary<long, Effect> timePauseBuffs = new Dictionary<long, Effect>();

	public async UniTask Play(Buff buff, Character target)
	{
		BuffInfoConfigure buffConfigure = buff.BuffId.GetBuffConfigure();
		target.characterAnimator.PauseAnimation();
		if (!timePauseBuffs.ContainsKey(target.player.Id))
		{
			Effect value = await SimpleSingletonProvider<EffectManager>.inst.PlayById(buffConfigure.EffectID, Vector3.zero, Quaternion.identity, target.EffectContainer);
			timePauseBuffs.Add(target.player.Id, value);
		}
	}

	public async UniTask UpdateEffect(Buff buff, Character target)
	{
		await Play(buff, target);
	}

	public void DestroyEffect(Buff buff, Character target)
	{
		target.characterAnimator.ResumeAnimation();
		if (timePauseBuffs.ContainsKey(target.player.Id))
		{
			timePauseBuffs[target.player.Id].ReleaseEffect();
			timePauseBuffs.Remove(target.player.Id);
		}
	}

	public void Dispose()
	{
		timePauseBuffs.Clear();
	}
}
