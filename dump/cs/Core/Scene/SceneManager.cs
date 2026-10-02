using System;
using Core.Audio;
using Core.Net;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Core.Scene;

public class SceneManager : SimpleSingletonProvider<SceneManager>
{
	private const string EMPTY_SCENE_NAME = "Empty";

	public const string LOGIN_SCENE_NAME = "Login";

	public const string HOME_SCENE_NAME = "Home";

	public bool gameInfoStatus;

	private SceneInstance _preSceneIns;

	private SceneInstance _preloadBattleSceneIns;

	public SceneType lastSceneType { get; private set; }

	public ReactiveProperty<SceneType> currentType { get; private set; }

	public SceneReactiveProperty loadingScene { get; private set; }

	protected override void InstanceInit()
	{
		base.InstanceInit();
		lastSceneType = SceneType.None;
		currentType = new ReactiveProperty<SceneType>(SceneType.None);
		loadingScene = new SceneReactiveProperty(new Scene());
	}

	public async UniTask LoadSceneAsync(SceneType sceneType, string sceneName)
	{
		if (NeedShowLoading(sceneType))
		{
			await SimpleSingletonProvider<UIManager>.inst.loading.SceneCutIn();
		}
		lastSceneType = currentType.Value;
		loadingScene.UpdateScene(sceneType, SceneStateType.Begin, sceneName);
		if (sceneType != SceneType.Battle)
		{
			SimpleSingletonProvider<AudioManager>.inst.StopAll();
			BGMHelper.Clear();
			SimpleSingletonProvider<UIManager>.inst.UnloadWhenEnterNewScene();
			await Resources.UnloadUnusedAssets();
			GC.Collect();
			AsyncOperationHandle<SceneInstance> preSceneInsHandle = await AddressableHelper.LoadSceneAsync(loadingScene.Value.sceneName);
			if (_preSceneIns.Scene.IsValid())
			{
				await Addressables.UnloadSceneAsync(_preSceneIns);
			}
			_preSceneIns = preSceneInsHandle.Result;
			loadingScene.UpdateStateType(SceneStateType.End);
		}
	}

	public async UniTask<AsyncOperationHandle<SceneInstance>> PreLoadBattleSceneAsync(RoomInfo roomInfo)
	{
		await LoadSceneAsync(SceneType.Battle, roomInfo.SceneConfig.AssetName);
		AsyncOperationHandle<SceneInstance> result = await AddressableHelper.LoadSceneAsync(loadingScene.Value.sceneName, LoadSceneMode.Single, activateOnLoad: false);
		_preloadBattleSceneIns = result.Result;
		return result;
	}

	public async UniTask ActivateScene()
	{
		SimpleSingletonProvider<UIManager>.inst.UnloadWhenEnterNewScene();
		await _preloadBattleSceneIns.ActivateAsync();
		await SimpleSingletonProvider<InternalAssetManager>.inst.InitAssetsAfterLoadedBattle();
		await Resources.UnloadUnusedAssets();
		GC.Collect();
		if (_preSceneIns.Scene.IsValid())
		{
			await Addressables.UnloadSceneAsync(_preSceneIns);
		}
		_preSceneIns = _preloadBattleSceneIns;
		loadingScene.UpdateStateType(SceneStateType.End);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.BattleBGM?.PlayBattleBGM(0L, reset: true);
	}

	public async UniTask UnLoadPreBattleSceneHandle()
	{
		if (_preloadBattleSceneIns.Scene.IsValid())
		{
			await _preloadBattleSceneIns.ActivateAsync();
			currentType.Value = SceneType.Battle;
			await Resources.UnloadUnusedAssets();
			GC.Collect();
			if (_preSceneIns.Scene.IsValid())
			{
				await Addressables.UnloadSceneAsync(_preSceneIns);
			}
			_preSceneIns = _preloadBattleSceneIns;
		}
		if (MonoSingletonProvider<NetManager>.inst.IsConnected)
		{
			await LoadSceneAsync(SceneType.Home, "Home");
		}
	}

	private bool NeedShowLoading(SceneType sceneType)
	{
		if ((sceneType != SceneType.SinglePlayer || currentType.Value != SceneType.Home) && (sceneType != SceneType.SinglePlayer || currentType.Value != SceneType.SinglePlayer))
		{
			return sceneType == SceneType.Home;
		}
		return true;
	}
}
