using System;
using Core;
using Core.Scene;
using Cysharp.Threading.Tasks;
using GameLogic;
using SinglePlayer.AssetsHelper;
using SinglePlayer.GamePlay;
using SinglePlayer.GamePlay.Action;
using SinglePlayer.GamePlay.Character;
using SinglePlayer.GamePlay.Map;
using SinglePlayer.Tools;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.Playables;

namespace SinglePlayer.Scene;

public class SinglePlayerSceneController : BaseSceneController, IController
{
	private bool _isInitialized;

	[field: SerializeField]
	public PlayableDirector MonsterPlayableDirector { get; private set; }

	protected override SceneType GetSceneType()
	{
		return SceneType.SinglePlayer;
	}

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		base.Start();
		InitScene().Forget();
	}

	private async UniTask InitScene()
	{
		_ = 3;
		try
		{
			await LoadAudioBank();
			await InitGameLogic();
			BuildBattleFieldData();
			await OpenUI();
			SimpleSingletonProvider<UIManager>.inst.loading.SceneCutOut().Forget();
			await BuildBattleFieldGameObject();
		}
		catch (Exception arg)
		{
			if (SimpleSingletonProvider<UIManager>.inst.loading.isShowing)
			{
				SimpleSingletonProvider<UIManager>.inst.loading.SceneCutOut().Forget();
			}
			Debug.LogError($"单人玩法加载报错：{arg}");
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOK(11027, delegate
			{
				SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer.ResetSinglePlayer();
			}).Forget();
		}
	}

	protected override void Update()
	{
		base.Update();
		if (_isInitialized)
		{
			Game.Tick();
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		UnloadAudioBank();
		try
		{
			Game.Dispose();
		}
		catch (Exception arg)
		{
			Debug.LogError($"{arg}");
		}
		UIDGenerator.Reset();
	}

	public async UniTask StartGame()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer?.FinishRestartGame();
		((await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.SinglePlayer)) as SinglePlayerPanel)?.OnGameStart();
		Game.GetController<CameraController>().CloseStartCamera();
		Game.GetSystem<GamePlayManager>().Run().Forget();
	}

	private async UniTask LoadAudioBank()
	{
		await SimpleSingletonProvider<AudioManager>.inst.LoadBank(StaticGlobalData.AUDIO_BANK_BGM_BATTLE);
		await SimpleSingletonProvider<AudioManager>.inst.LoadBank(StaticGlobalData.AUDIO_BANK_BGM_ROLE_COMMON);
	}

	private void UnloadAudioBank()
	{
		SimpleSingletonProvider<AudioManager>.inst.UnloadBank(StaticGlobalData.AUDIO_BANK_BGM_BATTLE);
		SimpleSingletonProvider<AudioManager>.inst.UnloadBank(StaticGlobalData.AUDIO_BANK_BGM_ROLE_COMMON);
	}

	private async UniTask InitGameLogic()
	{
		await Game.RegisterModel<GMData>();
		await Game.RegisterModel<GlobalSignal>();
		await Game.RegisterModel<GameData>();
		await Game.RegisterSystem(GetComponent<MonoBehaviourManager>());
		await Game.RegisterSystem<GamePlayManager>();
		await Game.RegisterSystem<PlayerActionFSM>();
		await Game.RegisterSystem<SinglePlayerAssetsHelper>();
		await Game.RegisterSystem<BoardManager>();
		await Game.RegisterController(this);
		await Game.RegisterController<BuildingController>();
		await Game.RegisterController<MonsterController>();
		await Game.RegisterController(UnityEngine.Object.FindObjectOfType<CameraController>());
		_isInitialized = true;
	}

	private void BuildBattleFieldData()
	{
		Game.GetSystem<BoardManager>().characterManager.BuildBattleFieldData();
	}

	private async UniTask BuildBattleFieldGameObject()
	{
		Game.GetSystem<BoardManager>().buildingManager.BuildBattleFieldGameObject();
		await Game.GetSystem<BoardManager>().characterManager.BuildBattleFieldGameObject();
		Game.GetController<BuildingController>().BuildRelicBuilding();
	}

	private async UniTask OpenUI()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer != null)
		{
			await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.SinglePlayerStart);
		}
		else
		{
			await StartGame();
		}
	}
}
