using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SinglePlayer.GamePlay;
using SinglePlayer.GamePlay.Build;
using Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Timeline;

namespace SinglePlayer.AssetsHelper;

public class BuildingAssetManager
{
	private readonly Dictionary<string, BuildingPool> _buildingPoolCache = new Dictionary<string, BuildingPool>();

	private Transform _root;

	private AsyncOperationHandle<Material> _handle;

	private AsyncOperationHandle<GameObject> _buildingBaseHandle;

	private Dictionary<string, AsyncOperationHandle<TimelineAsset>> _timeline = new Dictionary<string, AsyncOperationHandle<TimelineAsset>>();

	public Material PreviewMaterial => _handle.Result;

	public async UniTask Initialize()
	{
		_root = new GameObject("Building Root").transform;
		_handle = await AddressableHelper.LoadAssetAsync<Material>("BuildingPreview");
		_buildingBaseHandle = await AddressableHelper.LoadAssetAsync<GameObject>("SG_BuildBase");
		await PreLoadTimeline();
	}

	private string GetBuildingPlinthKey(int rarity)
	{
		return Game.GetModel<GameData>().CurrentLevelConfig.Plinth.GetSafeByIndex(rarity);
	}

	public async UniTask<BuildingView> Create(SinglePlayerCardConfigureItem config, int rarity)
	{
		GameObject gameObject = await CreateBuilding("SG_BuildBase");
		gameObject.transform.SetParent(_root);
		BuildingView view = gameObject.GetComponent<BuildingView>();
		view.Init(config, await CreateBuilding(config.Plinth), await CreateBuilding(config.Building));
		return view;
	}

	public async UniTask<BuildingView> CreateRelicBuilding(string key)
	{
		GameObject gameObject = await CreateBuilding("SG_BuildBase");
		gameObject.transform.SetParent(_root);
		BuildingView view = gameObject.GetComponent<BuildingView>();
		view.CombineBuilding(await CreateBuilding(key));
		return view;
	}

	public async UniTask<BuildingView> Create(BuildingBase buildingBase, bool revert = false)
	{
		BuildingView obj = await Create(buildingBase.GetConfigureItem(), buildingBase.Card.CardConfigure.Rarity);
		obj.SetBuildingBase(buildingBase, revert);
		return obj;
	}

	public void Recycle(BuildingView view)
	{
		if (!string.IsNullOrEmpty(view.Config.Plinth))
		{
			RecycleBuilding(view.Config.Plinth, view.Plinth);
		}
		RecycleBuilding(view.Config.Building, view.Building);
		RecycleBuilding("SG_BuildBase", view.gameObject);
		view.Clear();
	}

	private async UniTask<GameObject> CreateBuilding(string buildingName)
	{
		if (buildingName == "")
		{
			return null;
		}
		GameObject result;
		if (_buildingPoolCache.TryGetValue(buildingName, out var value))
		{
			result = value.Get();
		}
		else
		{
			AsyncOperationHandle<GameObject> asyncOperationHandle = await AddressableHelper.LoadAssetAsync<GameObject>(buildingName);
			if (!_buildingPoolCache.TryGetValue(buildingName, out var value2))
			{
				value2 = new BuildingPool(asyncOperationHandle);
				_buildingPoolCache.Add(buildingName, value2);
			}
			else
			{
				Addressables.Release(asyncOperationHandle);
			}
			result = value2.Get();
		}
		return result;
	}

	public bool ContainPool(string buildingName)
	{
		return _buildingPoolCache.ContainsKey(buildingName);
	}

	private void RecycleBuilding(string effectName, GameObject _effect)
	{
		if (_effect != null && _buildingPoolCache.TryGetValue(effectName, out var value))
		{
			value.Release(_effect);
		}
	}

	private async UniTask PreLoadTimeline()
	{
		await LoadTimelineAsync("SG_BuildEnter");
		await LoadTimelineAsync("SG_BuildingTrigger");
		await LoadTimelineAsync("SG_BuildLevelUp");
		await LoadTimelineAsync("SG_BuildingGainExp");
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

	public void Dispose()
	{
		foreach (KeyValuePair<string, BuildingPool> item in _buildingPoolCache)
		{
			item.Value.OnDestroy();
		}
		_buildingPoolCache.Clear();
		Addressables.Release(_handle);
		Addressables.Release(_buildingBaseHandle);
		ReleaseHandle();
	}
}
