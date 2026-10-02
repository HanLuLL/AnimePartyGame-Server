using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using SinglePlayer.GamePlay.Character;
using Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Timeline;

namespace SinglePlayer.AssetsHelper;

public class CharacterAssetManager : IDispose
{
	private readonly Dictionary<string, AsyncOperationHandle<GameObject>> _roleCache = new Dictionary<string, AsyncOperationHandle<GameObject>>();

	private AsyncOperationHandle<GameObject> _characterBaseHandle;

	private readonly Dictionary<string, AsyncOperationHandle<TimelineAsset>> _timeline = new Dictionary<string, AsyncOperationHandle<TimelineAsset>>();

	public async UniTask Initialize()
	{
		_characterBaseHandle = await AddressableHelper.LoadAssetAsync<GameObject>("SG_Characterbase");
		await PreLoadTimeline();
	}

	private async UniTask<GameObject> LoadRole(string key)
	{
		if (!_roleCache.TryGetValue(key, out var value))
		{
			value = await AddressableHelper.LoadAssetAsync<GameObject>(key);
			_roleCache.TryAdd(key, value);
		}
		return Object.Instantiate(value.Result);
	}

	public async UniTask<GameObject> LoadHero(int heroId)
	{
		GameObject obj = await LoadRole(string.Format("{0}{1:000}", "SG_Chess_", heroId));
		GameObject gameObject = Object.Instantiate(_characterBaseHandle.Result);
		obj.transform.parent = gameObject.transform.GetChild(0);
		obj.transform.parent.gameObject.AddComponent<SGCharacterAnimatorAudio>();
		return gameObject;
	}

	private async UniTask PreLoadTimeline()
	{
		await LoadTimelineAsync("SG_CharacterRoot_Cheer");
	}

	private async UniTask<TimelineAsset> LoadTimelineAsync(string key)
	{
		if (_timeline.TryGetValue(key, out var value))
		{
			return value.Result;
		}
		value = await AddressableHelper.LoadAssetAsync<TimelineAsset>(key);
		_timeline.Add(key, value);
		return value.Result;
	}

	public async UniTask<TimelineAsset> GetTimelineAsset(string key)
	{
		if (_timeline.TryGetValue(key, out var value))
		{
			return value.Result;
		}
		return await LoadTimelineAsync(key);
	}

	private void ReleaseHandle()
	{
		foreach (KeyValuePair<string, AsyncOperationHandle<TimelineAsset>> item in _timeline)
		{
			Addressables.Release(item.Value);
		}
		_timeline.Clear();
	}

	public UniTask<GameObject> LoadMonster(string key)
	{
		return LoadRole(key);
	}

	public void Dispose()
	{
		if (_roleCache.Count > 0)
		{
			foreach (KeyValuePair<string, AsyncOperationHandle<GameObject>> item in _roleCache)
			{
				Addressables.Release(item.Value);
			}
		}
		_roleCache.Clear();
		SimpleSingletonProvider<DiceManager>.inst.Dispose();
		Addressables.Release(_characterBaseHandle);
		ReleaseHandle();
	}
}
