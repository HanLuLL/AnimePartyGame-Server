using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core;

public class SummonManager : SimpleSingletonProvider<SummonManager>
{
	private readonly Dictionary<string, SummonPool> _SummonPoolCache = new Dictionary<string, SummonPool>();

	private bool _disposed;

	protected override void InstanceInit()
	{
		base.InstanceInit();
		_disposed = false;
	}

	private async UniTask<GameObject> GetSummonFromPool(string effectName)
	{
		if (string.IsNullOrEmpty(effectName) || IsInvalidLoad())
		{
			return null;
		}
		GameObject result;
		if (_SummonPoolCache.TryGetValue(effectName, out var value))
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
			if (!_SummonPoolCache.TryGetValue(effectName, out var value2))
			{
				value2 = new SummonPool(asyncOperationHandle);
				_SummonPoolCache.Add(effectName, value2);
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

	public async UniTask<T> CreateSummon<T>(string summonName, Vector3 position, Quaternion rotation, Transform parent = null, float scale = 1f) where T : SummonBase
	{
		if (string.IsNullOrEmpty(summonName))
		{
			return null;
		}
		GameObject gameObject = await GetSummonFromPool(summonName);
		if (gameObject == null)
		{
			return null;
		}
		T val = gameObject.AddComponent<T>();
		if (val == null)
		{
			Debug.LogError("获取" + summonName + "实体实例失败");
			return null;
		}
		if (parent != null)
		{
			val.transform.SetParent(parent);
			val.transform.localPosition = Vector3.zero;
			val.transform.localRotation = Quaternion.identity;
		}
		else
		{
			val.transform.position = position;
			val.transform.rotation = rotation;
		}
		val.transform.localScale = Vector3.one * scale;
		return val;
	}

	public void ReleaseSummon(SummonBase summon)
	{
		if (summon != null && _SummonPoolCache.TryGetValue(summon.SummonName, out var value))
		{
			value.Release(summon.gameObject);
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
		foreach (KeyValuePair<string, SummonPool> item in _SummonPoolCache)
		{
			item.Value.OnDestroy();
		}
		_SummonPoolCache.Clear();
	}
}
