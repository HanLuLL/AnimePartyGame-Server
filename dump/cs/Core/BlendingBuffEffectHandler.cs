using Core.Unit;
using Cysharp.Threading.Tasks;
using UI;
using party.model;

namespace Core;

public class BlendingBuffEffectHandler : IBuffEffectHandler
{
	private Effect blendingEffect;

	public async UniTask Play(Buff buff, Character target)
	{
		int num = 0;
		if (buff.Progress == 1)
		{
			num = 30301;
		}
		else if (buff.Progress == 2)
		{
			num = 30303;
		}
		else if (buff.Progress == 3)
		{
			num = 30302;
		}
		if (num != 0)
		{
			EffectInfoConfigure effectDataConfigure = num.GetEffectDataConfigure();
			if (!(blendingEffect != null) || !blendingEffect.effectPoolKey.Equals(effectDataConfigure.EffectName))
			{
				ReleaseEffect();
				blendingEffect = await target.PlayCharacterEffect(num);
			}
		}
	}

	private void ReleaseEffect()
	{
		if (!(blendingEffect == null))
		{
			blendingEffect.ReleaseEffect();
			blendingEffect = null;
		}
	}

	public void DestroyEffect(Buff buff, Character target)
	{
		ReleaseEffect();
	}

	public void Dispose()
	{
		blendingEffect = null;
	}

	public async UniTask UpdateEffect(Buff buff, Character target)
	{
		await Play(buff, target);
	}
}
