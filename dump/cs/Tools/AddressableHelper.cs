using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Tools;

public static class AddressableHelper
{
	public static async UniTask<AsyncOperationHandle<SceneInstance>> LoadSceneAsync(object key, LoadSceneMode mode = LoadSceneMode.Single, bool activateOnLoad = true, int priority = 999)
	{
		AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(key, mode, activateOnLoad, priority);
		await handle;
		if (handle.Status == AsyncOperationStatus.Succeeded)
		{
			return handle;
		}
		throw new FileLoadException($"#Addressable#加载场景失败，请检查！ key:{key}, status:{handle.Status}, error:{handle.OperationException}");
	}

	public static async UniTask<AsyncOperationHandle<TObject>> LoadAssetAsync<TObject>(object key)
	{
		AsyncOperationHandle<TObject> handle = Addressables.LoadAssetAsync<TObject>(key);
		await handle;
		if (handle.Status == AsyncOperationStatus.Succeeded)
		{
			return handle;
		}
		throw new FileLoadException($"#Addressable#加载资源失败，请检查！ key:{key}, status:{handle.Status}, error:{handle.OperationException}");
	}

	public static async UniTask<AsyncOperationHandle<IList<TObject>>> LoadAssetsAsync<TObject>(List<string> keys, Action<TObject> callback = null, Addressables.MergeMode mode = Addressables.MergeMode.Intersection)
	{
		AsyncOperationHandle<IList<TObject>> handle = Addressables.LoadAssetsAsync(keys, callback, mode);
		await handle;
		if (handle.Status == AsyncOperationStatus.Succeeded)
		{
			return handle;
		}
		throw new FileLoadException($"#Addressable#加载资源失败，请检查！ keys:{keys.ToListString()}, status:{handle.Status}, error:{handle.OperationException}");
	}
}
