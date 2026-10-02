using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Pool;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core;

public abstract class BasePool<T> : IBasePool<T> where T : class, new()
{
	protected readonly GameObject prefab;

	protected readonly AsyncOperationHandle<GameObject>? prefabHandle;

	protected readonly ObjectPool<T> pool;

	protected Dictionary<T, bool> Cache;

	protected BasePool(GameObject _prefab, bool collectionCheck = true)
	{
		prefab = _prefab;
		prefabHandle = null;
		Cache = new Dictionary<T, bool>();
		pool = new ObjectPool<T>(OnCreatePoolItem, OnGetPoolItem, OnReleasePoolItem, OnDestroyPoolItem, collectionCheck);
	}

	protected BasePool(AsyncOperationHandle<GameObject> _prefabHandle, bool collectionCheck = true)
	{
		prefabHandle = _prefabHandle;
		prefab = _prefabHandle.Result;
		Cache = new Dictionary<T, bool>();
		pool = new ObjectPool<T>(OnCreatePoolItem, OnGetPoolItem, OnReleasePoolItem, OnDestroyPoolItem, collectionCheck);
	}

	public virtual T Get()
	{
		return pool.Get();
	}

	public virtual void Release(T obj)
	{
		pool.Release(obj);
	}

	public virtual void Clear()
	{
		pool.Clear();
	}

	public virtual T OnCreatePoolItem()
	{
		return null;
	}

	public virtual void OnGetPoolItem(T obj)
	{
		Cache[obj] = true;
	}

	public virtual void OnReleasePoolItem(T obj)
	{
		if (Cache.ContainsKey(obj))
		{
			Cache[obj] = false;
		}
	}

	public virtual void OnDestroyPoolItem(T obj)
	{
	}

	public virtual void OnDestroy()
	{
		List<T> list = new List<T>();
		foreach (var (item, flag2) in Cache)
		{
			if (flag2)
			{
				list.Add(item);
			}
		}
		foreach (T item2 in list)
		{
			Release(item2);
		}
		Cache.Clear();
		if (prefabHandle.HasValue && prefabHandle.Value.IsValid())
		{
			Addressables.Release(prefabHandle.Value);
		}
		pool.Clear();
	}
}
