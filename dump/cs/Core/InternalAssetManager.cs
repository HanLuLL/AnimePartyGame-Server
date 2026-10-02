using System.Collections.Generic;
using Core.Camera;
using Core.Scene;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Timeline;
using party.model;

namespace Core;

public class InternalAssetManager : SimpleSingletonProvider<InternalAssetManager>
{
	private readonly Dictionary<string, AsyncOperationHandle> _cacheHandleDict = new Dictionary<string, AsyncOperationHandle>();

	private readonly Dictionary<string, AsyncOperationHandle> _cacheFightTimeline = new Dictionary<string, AsyncOperationHandle>();

	private int _AssetProgress;

	private GameObject battleControllerInstance;

	private readonly string[] fightTimelineKeys = new string[4] { "BattleStep_Draw", "BattleStep_ExHit", "BattleStep_SoloWin", "BattleStep_Win" };

	public object TryGetOperationHandle(string key)
	{
		if (!_cacheHandleDict.TryGetValue(key, out var value))
		{
			Debug.LogError("无法通过Key：" + key + "取得当前");
			return null;
		}
		return value.Result;
	}

	public async UniTask PreLoadBattleAsset()
	{
		_AssetProgress = 5;
		ThreadPriority loadingPriority = Application.backgroundLoadingPriority;
		int uploadingTimeSlice = QualitySettings.asyncUploadTimeSlice;
		Application.backgroundLoadingPriority = ThreadPriority.High;
		QualitySettings.asyncUploadTimeSlice = 32;
		await PreLoadCharacterAsset();
		await PreLoadMonsterAsset();
		await LoadFightTimelineAsset();
		await PreLoadSceneAsset();
		Application.backgroundLoadingPriority = loadingPriority;
		QualitySettings.asyncUploadTimeSlice = uploadingTimeSlice;
	}

	private async UniTask PreLoadCharacterAsset()
	{
		RoomInfo data = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (data == null || data.Players.Count == 0)
		{
			return;
		}
		int weight = Mathf.FloorToInt(60f / (float)data.Players.Count);
		for (int i = 0; i < data.Players.Count; i++)
		{
			if (!LoadBattleAssetsLicense(data))
			{
				return;
			}
			SkinStandingPaintingConfigureItem standingPainting = data.Players[i].standingPainting;
			await SimpleSingletonProvider<CharacterAssetManager>.inst.LoadCharacter(data.Players[i].Id, data.Players[i].Hero.HeroId, standingPainting, CharacterType.Hero);
			UpdateProgress(weight * (i + 1));
		}
		Debug.Log("角色资源加载完成");
	}

	private async UniTask PreLoadMonsterAsset()
	{
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (roomInfo == null)
		{
			return;
		}
		List<int> monsterIds = roomInfo.GetCurrentConfigMonsterIds();
		if (monsterIds != null && monsterIds.Count > 0)
		{
			int weight = Mathf.FloorToInt(20f / (float)monsterIds.Count);
			for (int i = 0; i < monsterIds.Count; i++)
			{
				if (!LoadBattleAssetsLicense(roomInfo))
				{
					return;
				}
				int characterStandingPainting = CharacterHandle.GetCharacterStandingPainting(monsterIds[i]);
				SkinStandingPaintingConfigureItem configStandingPainting = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(0, 0, characterStandingPainting);
				await SimpleSingletonProvider<CharacterAssetManager>.inst.LoadCharacter(0L, monsterIds[i], configStandingPainting, CharacterType.Monster);
				UpdateProgress(60 + weight * (i + 1));
			}
		}
		UpdateProgress(80);
		Debug.Log("怪物资源加载完成");
	}

	private async UniTask PreLoadSceneAsset()
	{
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (LoadBattleAssetsLicense(roomInfo))
		{
			await LoadInternalDependAssets();
			if (LoadBattleAssetsLicense(roomInfo))
			{
				await SimpleSingletonProvider<SceneManager>.inst.PreLoadBattleSceneAsync(roomInfo);
				UpdateProgress(100);
				Debug.Log("场景资源加载完成");
			}
		}
	}

	private void UpdateProgress(int progress)
	{
		_AssetProgress = progress;
		RoomLogic room = SimpleSingletonProvider<GameLogicManager>.inst.room;
		if (room == null)
		{
			return;
		}
		room.RequestRefreshRoomC2S(_AssetProgress);
		if (room.curRoomInfo != null)
		{
			WatchLogic watch = SimpleSingletonProvider<GameLogicManager>.inst.watch;
			if (watch != null && watch.PlayerIsWatcher())
			{
				foreach (RoomPlayer player in room.curRoomInfo.Players)
				{
					player.Progress = _AssetProgress;
				}
			}
			else
			{
				RoomPlayer selfInfo = room.curRoomInfo.GetSelfInfo();
				if (selfInfo != null)
				{
					selfInfo.Progress = _AssetProgress;
				}
			}
		}
		room.signal.UpdateHeroProgress.Dispatch();
	}

	private bool LoadBattleAssetsLicense(RoomInfo roomInfo)
	{
		if (roomInfo != null)
		{
			Room.Types.State state = roomInfo.State;
			if (state == Room.Types.State.Ready1 || state == Room.Types.State.Running)
			{
				return true;
			}
		}
		_AssetProgress = 100;
		Debug.LogWarning($"Room is null:{roomInfo == null} || data.State = {roomInfo?.State}");
		return false;
	}

	public async UniTask Dispose()
	{
		RoomLogic room = SimpleSingletonProvider<GameLogicManager>.inst.room;
		if (room?.curRoomInfo == null)
		{
			return;
		}
		Room.Types.State state = room.curRoomInfo.State;
		if (state != Room.Types.State.Wait && state != Room.Types.State.ChoiceHero)
		{
			await UniTask.WaitUntil(() => _AssetProgress == 100);
		}
		Release();
		TryDestroyBattleAssets();
		await SimpleSingletonProvider<SceneManager>.inst.UnLoadPreBattleSceneHandle();
	}

	public void TryDestroyBattleAssets()
	{
		if (SimpleSingletonProvider<DiceManager>.hasInstance)
		{
			SimpleSingletonProvider<DiceManager>.inst.Dispose();
		}
		if (SimpleSingletonProvider<SummonManager>.hasInstance)
		{
			SimpleSingletonProvider<SummonManager>.inst.Dispose();
		}
		if (SimpleSingletonProvider<CharacterAssetManager>.hasInstance)
		{
			SimpleSingletonProvider<CharacterAssetManager>.inst.Dispose();
		}
		if (SimpleSingletonProvider<ActionListener>.hasInstance)
		{
			SimpleSingletonProvider<ActionListener>.inst.Dispose();
		}
		if (SimpleSingletonProvider<DelaySignalManager>.hasInstance)
		{
			SimpleSingletonProvider<DelaySignalManager>.inst.Dispose();
		}
		Debug.LogWarning("battle assets are destroyed");
	}

	public async UniTask LoadInternalDependAssets()
	{
		await CommonUIManager.RegisterCommonInternalPackage();
		Debug.Log("局内公用UI资源加载完成");
		AsyncOperationHandle<GameObject> asyncOperationHandle = await AddressableHelper.LoadAssetAsync<GameObject>("BattleController");
		Debug.Log("battleController资源加载完成");
		_cacheHandleDict.TryAdd("BattleController", asyncOperationHandle);
		AsyncOperationHandle<GameObject> asyncOperationHandle2 = await AddressableHelper.LoadAssetAsync<GameObject>("CharacterInstance");
		Debug.Log("characterInstance资源加载完成");
		_cacheHandleDict.TryAdd("CharacterInstance", asyncOperationHandle2);
		AsyncOperationHandle<GameObject> asyncOperationHandle3 = await AddressableHelper.LoadAssetAsync<GameObject>("FreeVirtualCamera");
		Debug.Log("freeVirtualCamera资源加载完成");
		_cacheHandleDict.TryAdd("FreeVirtualCamera", asyncOperationHandle3);
		AsyncOperationHandle<GameObject> asyncOperationHandle4 = await AddressableHelper.LoadAssetAsync<GameObject>("InternalAnimationInstance");
		Debug.Log("internalAnimationInstance资源加载完成");
		_cacheHandleDict.TryAdd("InternalAnimationInstance", asyncOperationHandle4);
		AsyncOperationHandle<GameObject> asyncOperationHandle5 = await AddressableHelper.LoadAssetAsync<GameObject>("BattleShow");
		Debug.Log("BattleShowInstance资源加载完成");
		_cacheHandleDict.TryAdd("BattleShow", asyncOperationHandle5);
		AsyncOperationHandle<GameObject> asyncOperationHandle6 = await AddressableHelper.LoadAssetAsync<GameObject>("BattleChainCamera");
		Debug.Log("BattleChainCamera资源加载完成");
		_cacheHandleDict.TryAdd("BattleChainCamera", asyncOperationHandle6);
		AsyncOperationHandle<GameObject> asyncOperationHandle7 = await AddressableHelper.LoadAssetAsync<GameObject>("UIVolume");
		Debug.Log("UIVolume资源加载完成");
		_cacheHandleDict.TryAdd("UIVolume", asyncOperationHandle7);
	}

	public async UniTask InitAssetsAfterLoadedBattle()
	{
		GameObject original = (GameObject)TryGetOperationHandle("BattleController");
		battleControllerInstance = Object.Instantiate(original);
		await battleControllerInstance.GetComponent<BattleSceneController>().InitScene();
		await LoadCharacterVideo();
	}

	private async UniTask LoadCharacterVideo()
	{
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (roomInfo == null)
		{
			return;
		}
		foreach (KeyValuePair<long, RoomPlayer> item in roomInfo.RoomActorDict)
		{
			item.Deconstruct(out var _, out var value);
			SkinStandingPaintingConfigureItem standingPainting = value.standingPainting;
			await SimpleSingletonProvider<CharacterAssetManager>.inst.LoadCharacterVideo(standingPainting);
		}
		List<int> monsterIds = roomInfo.GetCurrentConfigMonsterIds();
		if (monsterIds != null && monsterIds.Count > 0)
		{
			for (int i = 0; i < monsterIds.Count; i++)
			{
				int characterStandingPainting = CharacterHandle.GetCharacterStandingPainting(monsterIds[i]);
				SkinStandingPaintingConfigureItem configStandingPainting = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(0, 0, characterStandingPainting);
				await SimpleSingletonProvider<CharacterAssetManager>.inst.LoadCharacterVideo(configStandingPainting);
			}
		}
	}

	public void Release()
	{
		List<BattlePlayerData> playerDatas = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
		if (playerDatas != null && playerDatas.Count > 0)
		{
			foreach (BattlePlayerData item in playerDatas)
			{
				item.Dispose();
			}
		}
		if (battleControllerInstance != null)
		{
			Object.Destroy(battleControllerInstance);
		}
		foreach (KeyValuePair<string, AsyncOperationHandle> item2 in _cacheFightTimeline)
		{
			if (item2.Value.IsValid())
			{
				Addressables.Release(item2.Value);
			}
		}
		_cacheFightTimeline.Clear();
		foreach (KeyValuePair<string, AsyncOperationHandle> item3 in _cacheHandleDict)
		{
			if (item3.Value.IsValid())
			{
				Addressables.Release(item3.Value);
			}
		}
		_cacheHandleDict.Clear();
		CommonUIManager.RemoveCommonInternalPackage();
	}

	public GameObject GetCharacterInstance()
	{
		return Object.Instantiate((GameObject)TryGetOperationHandle("CharacterInstance"), Vector3.one * 10000f, Quaternion.identity);
	}

	public GameObject GetAnimationInstance()
	{
		return Object.Instantiate((GameObject)TryGetOperationHandle("InternalAnimationInstance"));
	}

	public GameObject GetFreeCameraInstance(string name)
	{
		GameObject original = (GameObject)TryGetOperationHandle("FreeVirtualCamera");
		Transform transform = BattleSceneController.inst.transform;
		GameObject gameObject = Object.Instantiate(original, transform, worldPositionStays: true);
		gameObject.layer = transform.gameObject.layer;
		gameObject.name = name + "_VirtualCamera";
		gameObject.SetActive(value: false);
		return gameObject;
	}

	public GameObject GetUIVolumeInstance()
	{
		GameObject gameObject = (GameObject)TryGetOperationHandle("UIVolume");
		if (gameObject == null)
		{
			return null;
		}
		return Object.Instantiate(gameObject);
	}

	public async UniTask<CinemachineImpulseAsset> GetImpulseAsset(string key)
	{
		if (!_cacheHandleDict.TryGetValue(key, out var value))
		{
			value = await AddressableHelper.LoadAssetAsync<object>(key);
			_cacheHandleDict.TryAdd(key, value);
		}
		if (value.IsValid() && value.IsDone && value.Status == AsyncOperationStatus.Succeeded)
		{
			object result = value.Result;
			if (result is CinemachineImpulseAsset _impulseAsset)
			{
				return _impulseAsset;
			}
		}
		return null;
	}

	public async UniTask LoadFightTimelineAsset()
	{
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (!LoadBattleAssetsLicense(roomInfo))
		{
			return;
		}
		for (int i = 0; i < fightTimelineKeys.Length; i++)
		{
			if (!_cacheFightTimeline.TryGetValue(fightTimelineKeys[i], out var value))
			{
				value = await AddressableHelper.LoadAssetAsync<Object>(fightTimelineKeys[i]);
				_cacheFightTimeline.TryAdd(fightTimelineKeys[i], value);
			}
		}
		if (!LoadBattleAssetsLicense(roomInfo))
		{
			return;
		}
		foreach (KeyValuePair<long, RoomPlayer> item in roomInfo.RoomActorDict)
		{
			item.Deconstruct(out var _, out var value2);
			BattleResourceInfoConfigure battleResConfig = value2.standingPainting.BattleResConfig;
			await LoadBattleRes(battleResConfig);
		}
		if (!LoadBattleAssetsLicense(roomInfo))
		{
			return;
		}
		List<int> monsterIds = roomInfo.GetCurrentConfigMonsterIds();
		if (monsterIds != null && monsterIds.Count > 0)
		{
			for (int i = 0; i < monsterIds.Count; i++)
			{
				int characterStandingPainting = CharacterHandle.GetCharacterStandingPainting(monsterIds[i]);
				BattleResourceInfoConfigure battleResConfig2 = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(0, 0, characterStandingPainting).BattleResConfig;
				await LoadBattleRes(battleResConfig2);
			}
		}
		UpdateProgress(90);
	}

	public async UniTask LoadBattleRes(BattleResourceInfoConfigure battleRes)
	{
		if (!_cacheFightTimeline.TryGetValue(battleRes.HitDirect, out var value))
		{
			value = await AddressableHelper.LoadAssetAsync<Object>(battleRes.HitDirect);
			_cacheFightTimeline.TryAdd(battleRes.HitDirect, value);
		}
		if (!_cacheFightTimeline.TryGetValue(battleRes.ApproachDirect, out var value2))
		{
			value2 = await AddressableHelper.LoadAssetAsync<Object>(battleRes.ApproachDirect);
			_cacheFightTimeline.TryAdd(battleRes.ApproachDirect, value2);
		}
		if (!_cacheFightTimeline.TryGetValue(battleRes.DetachDirect, out var value3))
		{
			value3 = await AddressableHelper.LoadAssetAsync<Object>(battleRes.DetachDirect);
			_cacheFightTimeline.TryAdd(battleRes.DetachDirect, value3);
		}
	}

	public TimelineAsset GetTimelineAsset(string name)
	{
		if (_cacheFightTimeline.TryGetValue(name, out var value) && value.IsValid() && value.IsDone && value.Status == AsyncOperationStatus.Succeeded)
		{
			object result = value.Result;
			TimelineAsset val = (TimelineAsset)((result is TimelineAsset) ? result : null);
			if (val != null)
			{
				return val;
			}
		}
		return null;
	}

	public async UniTask<StoryData> GetStoryDataAsset(string key)
	{
		if (!_cacheHandleDict.TryGetValue(key, out var value))
		{
			value = await AddressableHelper.LoadAssetAsync<object>(key);
			_cacheHandleDict.TryAdd(key, value);
		}
		if (value.IsValid() && value.IsDone && value.Status == AsyncOperationStatus.Succeeded)
		{
			object result = value.Result;
			if (result is StoryData storyData)
			{
				return storyData;
			}
		}
		return null;
	}
}
