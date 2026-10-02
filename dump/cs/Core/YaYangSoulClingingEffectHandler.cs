using Core.Unit;
using Cysharp.Threading.Tasks;
using UI;
using party.model;

namespace Core;

public class YaYangSoulClingingEffectHandler : IBuffEffectHandler
{
	private SoulClingingEffect _soulClingingEffect;

	public async UniTask Play(Buff buff, Character target)
	{
		int effectID = buff.BuffId.GetBuffConfigure().EffectID;
		if (effectID != 0)
		{
			if (_soulClingingEffect == null && await target.PlayCharacterEffect(effectID) is SoulClingingEffect soulClingingEffect)
			{
				_soulClingingEffect = soulClingingEffect;
			}
			_soulClingingEffect.PlayClinging(buff.Progress);
		}
	}

	private void ReleaseEffect()
	{
		if (!(_soulClingingEffect == null))
		{
			_soulClingingEffect.ReleaseEffect();
			_soulClingingEffect = null;
		}
	}

	public void DestroyEffect(Buff buff, Character target)
	{
		ReleaseEffect();
	}

	public void Dispose()
	{
		_soulClingingEffect = null;
	}

	public async UniTask UpdateEffect(Buff buff, Character target)
	{
		await Play(buff, target);
	}
}
