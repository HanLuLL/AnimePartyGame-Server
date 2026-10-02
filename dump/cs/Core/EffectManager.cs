using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core;

public class EffectManager : SimpleSingletonProvider<EffectManager>
{
	private readonly Dictionary<string, EffectPool> _EffectPoolCache = new Dictionary<string, EffectPool>();

	private bool _disposed;

	private async UniTask<GameObject> GetEffectFromPool(string effectName)
	{
		if (string.IsNullOrEmpty(effectName) || IsInvalidLoad())
		{
			return null;
		}
		GameObject result;
		if (_EffectPoolCache.TryGetValue(effectName, out var value))
		{
			result = value.Get();
		}
		else
		{
			AsyncOperationHandle<GameObject> asyncOperationHandle = await AddressableHelper.LoadAssetAsync<GameObject>(effectName);
			if (IsInvalidLoad() || !asyncOperationHandle.IsValid() || asyncOperationHandle.Result == null)
			{
				ReleaseHandle(asyncOperationHandle);
				return null;
			}
			if (!_EffectPoolCache.TryGetValue(effectName, out var value2))
			{
				value2 = new EffectPool(asyncOperationHandle);
				_EffectPoolCache.Add(effectName, value2);
			}
			else
			{
				Addressables.Release(asyncOperationHandle);
			}
			result = value2.Get();
		}
		return result;
	}

	private bool IsInvalidLoad()
	{
		return _disposed;
	}

	private static void ReleaseHandle(AsyncOperationHandle<GameObject> handle)
	{
		if (handle.IsValid())
		{
			Addressables.Release(handle);
		}
	}

	public void Stop(string effectName, GameObject _effect)
	{
		if (_effect != null && _EffectPoolCache.TryGetValue(effectName, out var value))
		{
			value.Release(_effect);
		}
	}

	public void Stop(int effectId, GameObject _effect)
	{
		string effectName = effectId.GetEffectDataConfigure().EffectName;
		if (_effect != null && _EffectPoolCache.TryGetValue(effectName, out var value))
		{
			value.Release(_effect);
		}
	}

	public void Dispose()
	{
		if (_disposed)
		{
			return;
		}
		_disposed = true;
		OnDestroyInstance();
		foreach (KeyValuePair<string, EffectPool> item in _EffectPoolCache)
		{
			item.Value.OnDestroy();
		}
		_EffectPoolCache.Clear();
	}

	public async UniTask<Effect> PlayById(int effectId, Vector3 position, Quaternion rotation, Transform parent = null, Action onComplete = null, float scale = 1f)
	{
		EffectInfoConfigure effectDataConfigure = effectId.GetEffectDataConfigure();
		if (effectDataConfigure == null)
		{
			return null;
		}
		return await PlayByName(effectDataConfigure.EffectName, position, rotation, parent, onComplete, scale);
	}

	public async UniTask<Effect> PlayByName(string effectName, Vector3 position, Quaternion rotation, Transform parent = null, Action onComplete = null, float scale = 1f)
	{
		if (string.IsNullOrEmpty(effectName))
		{
			return null;
		}
		GameObject gameObject = await GetEffectFromPool(effectName);
		if (gameObject == null)
		{
			return null;
		}
		Effect component = gameObject.GetComponent<Effect>();
		if (component == null)
		{
			Debug.LogError("获取" + effectName + "特效失败");
			return null;
		}
		if (parent != null)
		{
			component.transform.SetParent(parent);
			component.transform.localPosition = Vector3.zero;
			component.transform.localRotation = Quaternion.identity;
		}
		else
		{
			component.transform.position = position;
			component.transform.rotation = rotation;
		}
		component.transform.localScale = Vector3.one * scale;
		component.Play(effectName, onComplete);
		return component;
	}

	public async UniTask<GameObject> GetEffectInstance(int effectId, Transform parent)
	{
		string effectName = effectId.GetEffectDataConfigure().EffectName;
		if (string.IsNullOrEmpty(effectName))
		{
			return null;
		}
		GameObject gameObject = await GetEffectFromPool(effectName);
		if (gameObject == null)
		{
			return null;
		}
		if (parent != null)
		{
			gameObject.transform.SetParent(parent);
		}
		return gameObject;
	}

	public async UniTask PreLoadEffect(string effectName)
	{
		if (!string.IsNullOrEmpty(effectName) && !IsInvalidLoad() && !_EffectPoolCache.ContainsKey(effectName))
		{
			AsyncOperationHandle<GameObject> asyncOperationHandle = await AddressableHelper.LoadAssetAsync<GameObject>(effectName);
			EffectPool value;
			if (IsInvalidLoad() || !asyncOperationHandle.IsValid() || asyncOperationHandle.Result == null)
			{
				ReleaseHandle(asyncOperationHandle);
			}
			else if (!_EffectPoolCache.TryGetValue(effectName, out value))
			{
				value = new EffectPool(asyncOperationHandle);
				_EffectPoolCache.Add(effectName, value);
			}
			else
			{
				Addressables.Release(asyncOperationHandle);
			}
		}
	}

	public async UniTask PlayGoldShow(int detal, Vector3 pos, Quaternion rotation, Transform parent = null)
	{
		string effectName = ((detal > 0) ? "Gold_UP" : "Gold_Down");
		int num = Mathf.Min(Mathf.Abs(detal), 5);
		for (int i = 0; i < num; i++)
		{
			await PlayByName(effectName, pos, rotation);
			if (await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(0.10000000149011612)))
			{
				break;
			}
		}
	}

	public async UniTask PreLoadFightEffect(long playerId)
	{
		FashionEffectConfigure KillEffectConfig = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.OverKillResultConfig(playerId);
		await PreLoadEffect(KillEffectConfig.KillVfx.GetEffectDataConfigure().EffectName);
		await PreLoadEffect(KillEffectConfig.FlyVfx.GetEffectDataConfigure().EffectName);
	}
}
