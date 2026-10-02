using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;
using party.model;

namespace Core;

public class BuffEffectHandler_10611101 : IBuffEffectHandler
{
	private Effect _curEffects;

	public async UniTask Play(Buff buff, Character target)
	{
		int effectID = buff.BuffId.GetBuffConfigure().EffectID;
		if (effectID != 0)
		{
			if (buff.Progress > 0)
			{
				ReleaseEffect();
			}
			else if (_curEffects == null)
			{
				_curEffects = await SimpleSingletonProvider<EffectManager>.inst.PlayById(effectID, Vector3.zero, Quaternion.identity, target.EffectContainer);
			}
		}
	}

	public async UniTask UpdateEffect(Buff buff, Character target)
	{
		await Play(buff, target);
	}

	private void ReleaseEffect()
	{
		if (!(_curEffects == null))
		{
			_curEffects.ReleaseEffect();
			_curEffects = null;
		}
	}

	public void DestroyEffect(Buff buff, Character target)
	{
		ReleaseEffect();
	}

	public void Dispose()
	{
		_curEffects = null;
	}
}
