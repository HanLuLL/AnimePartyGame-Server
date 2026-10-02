using Core.Net;
using Cysharp.Threading.Tasks;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UI;

namespace Core.Scene;

public class HomeSceneController : BaseSceneController
{
	protected override void Awake()
	{
		base.Awake();
	}

	protected override async void Start()
	{
		base.Start();
		await LoadAudioBank();
		await SimpleSingletonProvider<ExternalAssetManager>.inst.LoadDependAssets();
		SimpleSingletonProvider<GameLogicManager>.inst.battle.clientFinishReady = false;
		if (SimpleSingletonProvider<UIManager>.inst.loading.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.loading.SceneCutOut().Forget();
		}
		RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
		if (SimpleSingletonProvider<SceneManager>.inst.lastSceneType == SceneType.Battle)
		{
			if (roomController.RoomValid)
			{
				WatchLogic watch = SimpleSingletonProvider<GameLogicManager>.inst.watch;
				if (watch != null && watch.PlayerIsWatcher())
				{
					SimpleSingletonProvider<GameLogicManager>.inst.room.ClearRoomInfo();
					await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Home);
				}
				else if (!SimpleSingletonProvider<GameLogicManager>.inst.tutorial.IsReturnNovice(roomController.localRoom))
				{
					await TryOpenPanelByRoom(roomController);
				}
				else
				{
					SimpleSingletonProvider<GameLogicManager>.inst.tutorial.StartTutorial().Forget();
				}
			}
			else if (SimpleSingletonProvider<GameLogicManager>.inst.room.voluntaryWithdrawal)
			{
				await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Home);
			}
			else if (roomController.localRoom == null)
			{
				await SimpleSingletonProvider<UIManager>.inst.ReturnThePanelDirectlyInHomeScene(UIPanelType.RoomList, null, UIPanelType.MatchEntrance);
			}
			else
			{
				await TryOpenPanelByRoom(roomController);
			}
		}
		else if (SimpleSingletonProvider<SceneManager>.inst.lastSceneType == SceneType.SelectMusic || SimpleSingletonProvider<SceneManager>.inst.lastSceneType == SceneType.Music)
		{
			ActivityActivityEntrance2Configure activityInfo = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetActivityInfo(4);
			if (activityInfo != null && activityInfo.InfoConfig.UiType == UIType.Panel)
			{
				SimpleSingletonProvider<UIManager>.inst.OpenPanel(activityInfo.InfoConfig.PanelType, new RepeatedField<int> { activityInfo.InfoConfig.Id }).Forget();
			}
		}
		else if (SimpleSingletonProvider<SceneManager>.inst.lastSceneType == SceneType.SinglePlayer)
		{
			await SimpleSingletonProvider<UIManager>.inst.ReturnThePanelDirectlyInHomeScene(UIPanelType.SoloLevel, null, UIPanelType.Home);
		}
		else
		{
			if (string.IsNullOrEmpty(SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo().Nick))
			{
				await SimpleSingletonProvider<UIManager>.inst.PlayerRenameWindow.Display();
				await SimpleSingletonProvider<UIManager>.inst.tutorial.ShowSkipGuide();
			}
			if (SimpleSingletonProvider<GameLogicManager>.inst.tutorial.IsNovice)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.tutorial.StartTutorial().Forget();
			}
			else
			{
				if (roomController.localRoom == null)
				{
					await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Home);
				}
				else
				{
					await TryOpenPanelByRoom(roomController);
				}
				SimpleSingletonProvider<GameLogicManager>.inst.match.RequestRefreshMatchTeamInfoC2S();
			}
		}
		MonoSingletonProvider<NetManager>.inst.SetConnectThreadSpeed(isFast: true);
	}

	private async UniTask TryOpenPanelByRoom(RoomController roomController)
	{
		if (roomController.localRoom.IsMatchRoom)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.room.ClearRoomInfo();
			await SimpleSingletonProvider<UIManager>.inst.ReturnThePanelDirectlyInHomeScene(UIPanelType.MatchEntrance, null);
			if (SimpleSingletonProvider<GameLogicManager>.inst.match != null && SimpleSingletonProvider<GameLogicManager>.inst.match.matchData.InTeam)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.match.RequestRefreshMatchTeamInfoC2S();
			}
		}
		else if (roomController.localRoom.IsCampaign() || SimpleSingletonProvider<GameLogicManager>.inst.tutorial.IsCampaignTutorial(roomController.localRoom))
		{
			SimpleSingletonProvider<GameLogicManager>.inst.room.ClearRoomInfo();
			await SimpleSingletonProvider<UIManager>.inst.ReturnThePanelDirectlyInHomeScene(UIPanelType.Campaign, null, UIPanelType.MatchEntrance, UIPanelType.SoloLevel);
		}
		else if (roomController.localRoom.IsPractice())
		{
			SimpleSingletonProvider<GameLogicManager>.inst.room.ClearRoomInfo();
			await SimpleSingletonProvider<UIManager>.inst.ReturnThePanelDirectlyInHomeScene(UIPanelType.TrainingLevel, null, UIPanelType.MatchEntrance, UIPanelType.SoloLevel);
		}
		else if (roomController.RoomValid)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.room.RequestSyncRoomC2S(roomController.localRoom.Id);
		}
		else
		{
			await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Home);
		}
	}

	protected override SceneType GetSceneType()
	{
		return SceneType.Home;
	}

	protected override void OnDestroy()
	{
		GachaManager.inst.Dispose();
		if (SimpleSingletonProvider<ExternalAssetManager>.hasInstance)
		{
			SimpleSingletonProvider<ExternalAssetManager>.inst.Dispose();
		}
		if (SimpleSingletonProvider<DelaySignalManager>.hasInstance)
		{
			SimpleSingletonProvider<DelaySignalManager>.inst.Dispose();
		}
		UnloadAudioBank();
		base.OnDestroy();
	}

	protected override void OnApplicationFocus(bool focus)
	{
	}

	private async UniTask LoadAudioBank()
	{
		await SimpleSingletonProvider<AudioManager>.inst.LoadBank(StaticGlobalData.AUDIO_BANK_BGM_HOME);
		await SimpleSingletonProvider<AudioManager>.inst.LoadBank(StaticGlobalData.AUDIO_BANK_HERO_HOME);
	}

	private void UnloadAudioBank()
	{
		SimpleSingletonProvider<AudioManager>.inst.UnloadBank(StaticGlobalData.AUDIO_BANK_BGM_HOME);
		SimpleSingletonProvider<AudioManager>.inst.UnloadBank(StaticGlobalData.AUDIO_BANK_HERO_HOME);
	}
}
