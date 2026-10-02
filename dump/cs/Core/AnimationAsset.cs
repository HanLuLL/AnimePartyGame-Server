using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core;

public class AnimationAsset
{
	private AsyncOperationHandle<IList<Object>> Handle;

	public AnimationClip Clip;

	public AnimationAsset(AsyncOperationHandle<IList<Object>> _handle)
	{
		Handle = _handle;
	}

	public void Dispose()
	{
		if (Handle.IsValid())
		{
			Addressables.Release(Handle);
		}
	}
}
