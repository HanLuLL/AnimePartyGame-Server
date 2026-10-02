using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;
using party.model;

namespace Core;

public class KongQueBiggerBuffEffectHandler : IBuffEffectHandler
{
	private Effect biggerEffect;

	public async UniTask Play(Buff buff, Character target)
	{
		float scale = 1f + (float)buff.Progress / ((float)buff.Progress + 10f);
		target.SetScale(Character.ScaleSourceType.KongQueKaiPing, scale);
		if (biggerEffect == null)
		{
			BuffInfoConfigure buffConfigure = buff.BuffId.GetBuffConfigure();
			if (buffConfigure.EffectID != 0)
			{
				biggerEffect = await SimpleSingletonProvider<EffectManager>.inst.PlayById(buffConfigure.EffectID, Vector3.zero, Quaternion.identity, target.EffectContainer);
			}
		}
	}

	public async UniTask UpdateEffect(Buff buff, Character target)
	{
		await Play(buff, target);
	}

	private void ReleaseEffect()
	{
		if (!(biggerEffect == null))
		{
			biggerEffect.ReleaseEffect();
			biggerEffect = null;
		}
	}

	public void DestroyEffect(Buff buff, Character target)
	{
		ReleaseEffect();
		target.RemoveScale(Character.ScaleSourceType.KongQueKaiPing);
	}

	public void Dispose()
	{
		biggerEffect = null;
	}
}
