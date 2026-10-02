using System;
using UnityEngine.Pool;

namespace SinglePlayer.GamePlay.BuffSystem;

public class BuffPool
{
	private Type _type;

	private IObjectPool<BuffBase> _pool;

	public BuffPool(string className)
	{
		_type = Type.GetType(className);
		_pool = new ObjectPool<BuffBase>(OnCreate, OnGet, OnRelease, OnDestroy);
	}

	public BuffBase Get()
	{
		return _pool.Get();
	}

	public void Release(BuffBase buff)
	{
		_pool.Release(buff);
	}

	private BuffBase OnCreate()
	{
		return Activator.CreateInstance(_type) as BuffBase;
	}

	private void OnGet(BuffBase buff)
	{
	}

	private void OnRelease(BuffBase buff)
	{
		buff.Dispose();
	}

	private void OnDestroy(BuffBase buff)
	{
	}

	public void Dispose()
	{
		_pool.Clear();
		_type = null;
	}
}
