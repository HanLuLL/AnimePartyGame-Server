using System;
using Core;
using Cysharp.Threading.Tasks;
using SinglePlayer.AssetsHelper;
using SinglePlayer.Scene;
using Tools;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace SinglePlayer.GamePlay.Character;

public class MonsterController : IController, IInitialize, IDispose
{
	private Transform _spawnPointMonster;

	private SinglePlayerSceneController _sceneController;

	public MonsterView UnitView { get; private set; }

	public async UniTask Initialize()
	{
		await UniTask.CompletedTask;
		_sceneController = UnityEngine.Object.FindObjectOfType<SinglePlayerSceneController>();
		GameObject gameObject = GameObject.Find("SpawnPoint_Monster");
		if (gameObject == null)
		{
			Debug.LogError("未找到怪物生成点 SpawnPoint_Monster");
		}
		_spawnPointMonster = gameObject.transform;
		Game.GetModel<GlobalSignal>().MonsterCreated.AddListener(OnMonsterCreated);
		Game.GetModel<GlobalSignal>().MonsterDisposed.AddListener(OnMonsterDisposed);
		Game.GetModel<GlobalSignal>().MonsterExit = MonsterExit;
		Game.GetModel<GlobalSignal>().MonsterEnter = MonsterEnter;
	}

	public void Dispose()
	{
		Game.GetModel<GlobalSignal>().MonsterCreated.RemoveListener(OnMonsterCreated);
		Game.GetModel<GlobalSignal>().MonsterDisposed.RemoveListener(OnMonsterDisposed);
		Game.GetModel<GlobalSignal>().MonsterExit = null;
		Game.GetModel<GlobalSignal>().MonsterEnter = null;
		if (UnitView != null)
		{
			UnityEngine.Object.Destroy(UnitView.gameObject);
		}
	}

	private async void OnMonsterCreated(Monster monster)
	{
		try
		{
			if (!StaticConfigure.SinglePlayer.MonsterDict.TryGetValue(monster.Property.Id, out var value))
			{
				Debug.LogError($"未找到怪物配置ID:{monster.Property.Id}");
				return;
			}
			GameObject gameObject = await Game.GetSystem<SinglePlayerAssetsHelper>().characterAssetManager.LoadMonster(value.Model);
			_ = ((Component)(object)gameObject.GetComponentInChildren<Animator>()).gameObject;
			monster.view = gameObject.AddComponent<MonsterView>();
			monster.view.Initialize(monster);
			UnitView = monster.view;
			gameObject.transform.parent = _spawnPointMonster;
			gameObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.ToString());
		}
	}

	private void OnMonsterDisposed(Monster monster)
	{
		UnitView.Dispose();
		UnityEngine.Object.Destroy(UnitView.gameObject);
	}

	private async UniTask MonsterExit()
	{
		UnityEngine.Object obj = await Game.GetSystem<SinglePlayerAssetsHelper>().designAssetManager.TryGetAssetsByKey("MonsterIn");
		if (obj == null)
		{
			return;
		}
		TimelineAsset val = (TimelineAsset)(object)((obj is TimelineAsset) ? obj : null);
		if ((UnityEngine.Object)(object)val != null)
		{
			if ((UnityEngine.Object)(object)_sceneController.MonsterPlayableDirector == null)
			{
				Debug.LogError("场景控制未关联怪物进场退场PlayableDirector组件");
				return;
			}
			_sceneController.MonsterPlayableDirector.Play((PlayableAsset)(object)val);
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds((float)((PlayableAsset)(object)val).duration));
		}
	}

	private async UniTask MonsterEnter()
	{
		UnityEngine.Object obj = await Game.GetSystem<SinglePlayerAssetsHelper>().designAssetManager.TryGetAssetsByKey("MonsterOut");
		if (obj == null)
		{
			return;
		}
		TimelineAsset val = (TimelineAsset)(object)((obj is TimelineAsset) ? obj : null);
		if ((UnityEngine.Object)(object)val != null)
		{
			if ((UnityEngine.Object)(object)_sceneController.MonsterPlayableDirector == null)
			{
				Debug.LogError("场景控制未关联怪物进场退场PlayableDirector组件");
				return;
			}
			_sceneController.MonsterPlayableDirector.Play((PlayableAsset)(object)val);
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds((float)((PlayableAsset)(object)val).duration));
		}
	}
}
