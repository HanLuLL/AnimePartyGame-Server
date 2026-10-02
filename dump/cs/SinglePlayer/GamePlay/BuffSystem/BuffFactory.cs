using System.Collections.Generic;
using UnityEngine;

namespace SinglePlayer.GamePlay.BuffSystem;

public static class BuffFactory
{
	private static Dictionary<uint, BuffPool> _poolDict = new Dictionary<uint, BuffPool>();

	public static BuffBase Create(uint configId)
	{
		string className = $"SinglePlayer.GamePlay.BuffSystem.Buff{configId}";
		if (!_poolDict.TryGetValue(configId, out var value))
		{
			value = new BuffPool(className);
			_poolDict.Add(configId, value);
		}
		return value.Get();
	}

	public static void Release(BuffBase buff)
	{
		BuffPool value;
		if (buff == null)
		{
			Debug.LogError("释放Buff失败，该Buff为null");
		}
		else if (!_poolDict.TryGetValue(buff.Configure.Id, out value))
		{
			Debug.LogError($"释放Buff失败，未找到{buff.Configure.Id}对应的Buff Pool");
		}
		else
		{
			value.Release(buff);
		}
	}

	public static void Dispose()
	{
		foreach (KeyValuePair<uint, BuffPool> item in _poolDict)
		{
			item.Value.Dispose();
		}
		_poolDict.Clear();
		_poolDict = null;
	}
}
