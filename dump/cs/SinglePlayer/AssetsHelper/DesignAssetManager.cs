using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SinglePlayer.AssetsHelper;

public class DesignAssetManager
{
	private readonly Dictionary<string, Object> AssetsDict = new Dictionary<string, Object>();

	public async UniTask<Object> TryGetAssetsByKey(string key)
	{
		if (!AssetsDict.TryGetValue(key, out var value))
		{
			value = await Addressables.LoadAssetAsync<Object>(key);
			AssetsDict.TryAdd(key, value);
		}
		return value;
	}

	public void Dispose()
	{
		foreach (KeyValuePair<string, Object> item in AssetsDict)
		{
			Addressables.Release(item.Value);
		}
	}
}
