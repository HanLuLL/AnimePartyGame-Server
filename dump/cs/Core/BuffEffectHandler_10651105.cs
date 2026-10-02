using System.Collections.Generic;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;
using party.model;

namespace Core;

public class BuffEffectHandler_10651105 : IBuffEffectHandler
{
	private Dictionary<long, Effect> _curEffects = new Dictionary<long, Effect>();

	private Dictionary<long, int> _lastBuffNums = new Dictionary<long, int>();

	public async UniTask Play(Buff buff, Character target)
	{
		int curBuffNum = buff.Progress;
		int valueOrDefault = _lastBuffNums.GetValueOrDefault(target.player.Id, 0);
		if (curBuffNum >= 4)
		{
			if (valueOrDefault < 4)
			{
				ReleaseEffect(target.player.Id);
			}
			await PlayEffect(106514, target);
		}
		else
		{
			switch (curBuffNum)
			{
			case 3:
				if (valueOrDefault != 3)
				{
					ReleaseEffect(target.player.Id);
				}
				await PlayEffect(106513, target);
				break;
			case 2:
				if (valueOrDefault != 2)
				{
					ReleaseEffect(target.player.Id);
				}
				await PlayEffect(106512, target);
				break;
			case 1:
				if (valueOrDefault != 1)
				{
					ReleaseEffect(target.player.Id);
				}
				await PlayEffect(106511, target);
				break;
			default:
				ReleaseEffect(target.player.Id);
				break;
			}
		}
		_lastBuffNums[target.player.Id] = curBuffNum;
	}

	public async UniTask UpdateEffect(Buff buff, Character target)
	{
		await Play(buff, target);
	}

	private void ReleaseEffect()
	{
		if (_curEffects.Count == 0)
		{
			return;
		}
		foreach (long key in _curEffects.Keys)
		{
			ReleaseEffect(key);
			_lastBuffNums.Remove(key);
		}
		_curEffects.Clear();
	}

	private void ReleaseEffect(long playerId)
	{
		if (_curEffects.TryGetValue(playerId, out var value))
		{
			value.ReleaseEffect();
			_curEffects.Remove(playerId);
		}
	}

	private async UniTask PlayEffect(int effectId, Character target)
	{
		if (!_curEffects.ContainsKey(target.player.Id))
		{
			Dictionary<long, Effect> curEffects = _curEffects;
			long id = target.player.Id;
			curEffects[id] = await SimpleSingletonProvider<EffectManager>.inst.PlayById(effectId, Vector3.zero, Quaternion.identity, target.EffectContainer);
		}
	}

	public void DestroyEffect(Buff buff, Character target)
	{
		ReleaseEffect(target.player.Id);
	}

	public void Dispose()
	{
		_curEffects.Clear();
		_lastBuffNums.Clear();
	}
}
