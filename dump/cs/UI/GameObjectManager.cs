using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UI;

public class GameObjectManager : SimpleSingletonProvider<GameObjectManager>
{
	private readonly Dictionary<string, GameObject> _gameObjects = new Dictionary<string, GameObject>();

	public async UniTask<GameObject> ShowGameObject(string key, GGraph graph, float scale = 1f)
	{
		graph.visible = true;
		return ShowGameObject(await Load(key), graph, scale);
	}

	public GameObject ShowGameObject(GameObject _prefab, GGraph graph, float scale = 1f)
	{
		GameObject gameObject = Object.Instantiate(_prefab);
		if (graph.displayObject is GoWrapper goWrapper)
		{
			Object.Destroy(goWrapper.wrapTarget);
			goWrapper.wrapTarget = gameObject;
		}
		else
		{
			graph.SetNativeObject(new GoWrapper(gameObject));
		}
		graph.displayObject.scale = Vector2.one * scale;
		return gameObject;
	}

	public async UniTask<GameObject> ShowEffectInUI(string _effectName, GGraph graph, float scale = 1f)
	{
		GameObject obj = await ShowGameObject(_effectName, graph, scale);
		obj.GetComponent<GameEffect>()?.Play(null, null);
		return obj;
	}

	public void Stop(GGraph graph)
	{
		if (graph?.displayObject is GoWrapper goWrapper && !(goWrapper.wrapTarget == null))
		{
			Object.Destroy(goWrapper.wrapTarget);
			graph.visible = false;
		}
	}

	private async UniTask<GameObject> Load(string key)
	{
		if (string.IsNullOrEmpty(key))
		{
			return null;
		}
		if (_gameObjects.TryGetValue(key, out var value))
		{
			return value;
		}
		AsyncOperationHandle<GameObject> handle = await AddressableHelper.LoadAssetAsync<GameObject>(key);
		if (handle.IsDone && handle.Status == AsyncOperationStatus.Succeeded)
		{
			if (_gameObjects.TryGetValue(key, out var value2))
			{
				Addressables.Release(handle);
				return value2;
			}
			GameObject result = handle.Result;
			_gameObjects.Add(key, result);
			return result;
		}
		return null;
	}

	public void Clear()
	{
		foreach (GameObject value in _gameObjects.Values)
		{
			Addressables.Release(value);
		}
		_gameObjects.Clear();
	}
}
