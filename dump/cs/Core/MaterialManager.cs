using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;

namespace Core;

public class MaterialManager : SimpleSingletonProvider<MaterialManager>
{
	private readonly Dictionary<string, Material> _materialDict = new Dictionary<string, Material>();

	protected override void InstanceInit()
	{
		base.InstanceInit();
		_materialDict.Clear();
	}

	public async UniTask<Material> GetMaterial(string key)
	{
		if (string.IsNullOrEmpty(key))
		{
			return null;
		}
		if (!_materialDict.TryGetValue(key, out var value))
		{
			value = new Material((await AddressableHelper.LoadAssetAsync<Material>(key)).Result);
			_materialDict.TryAdd(key, value);
		}
		return value;
	}
}
