using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UI;

public class TextureManager : SimpleSingletonProvider<TextureManager>
{
	private readonly Dictionary<string, NTexture> _cachedTextureDict = new Dictionary<string, NTexture>();

	protected override void InstanceInit()
	{
		base.InstanceInit();
		NTexture.CustomDestroyMethod += OnCustomDestroyMethod;
	}

	protected override void OnDestroyInstance()
	{
		base.OnDestroyInstance();
		NTexture.CustomDestroyMethod -= OnCustomDestroyMethod;
	}

	public async UniTask AsyncLoad(string url, Action<NTexture> onSucceeded, Action onFailed)
	{
		if (string.IsNullOrEmpty(url))
		{
			return;
		}
		if (_cachedTextureDict.TryGetValue(url, out var value))
		{
			if (value.disposed)
			{
				_cachedTextureDict.Remove(url);
				await AsyncLoad(url, onSucceeded, onFailed);
			}
			else
			{
				onSucceeded?.Invoke(value);
			}
			return;
		}
		AsyncOperationHandle<Texture> handle = await AddressableHelper.LoadAssetAsync<Texture>(url);
		if (handle.IsDone && handle.Status == AsyncOperationStatus.Succeeded)
		{
			if (!_cachedTextureDict.TryGetValue(url, out var value2))
			{
				value2 = new NTexture(handle.Result);
				value2.destroyMethod = DestroyMethod.Custom;
				value2.onRelease += OnRelease;
				_cachedTextureDict.Add(url, value2);
			}
			else
			{
				Addressables.Release(handle);
			}
			onSucceeded?.Invoke(value2);
		}
		else
		{
			onFailed?.Invoke();
		}
	}

	private void OnRelease(NTexture nTexture)
	{
		nTexture.onRelease -= OnRelease;
		nTexture.Dispose();
	}

	private void OnCustomDestroyMethod(Texture tex)
	{
		Addressables.Release(tex);
	}

	public void Clear()
	{
		foreach (KeyValuePair<string, NTexture> item in _cachedTextureDict)
		{
			while (item.Value.refCount > 0)
			{
				item.Value.ReleaseRef();
			}
		}
		_cachedTextureDict.Clear();
	}
}
