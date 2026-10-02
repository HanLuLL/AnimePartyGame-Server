using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;
using party.model;

namespace Core.Tutorial;

public class TutorialGuide : Core.Unit.Unit
{
	public TutorialScene TutorialScene;

	private async void Start()
	{
		await StaticConfigure.InitAsync();
		GameSettings.InitSetting();
		await SimpleSingletonProvider<UIManager>.inst.AsyncInit();
		SimpleSingletonProvider<GameLogicManager>.inst.InitLogics();
		await CommonUIManager.RegisterCommonExternalPackage();
		await SimpleSingletonProvider<AudioManager>.inst.LoadBank(StaticGlobalData.AUDIO_BANK_UI);
		AccountInfo account = new AccountInfo
		{
			AccountId = 100020uL,
			Nick = "Player",
			PlayerId = 100020L,
			Token = "Test025"
		};
		Player player = new Player
		{
			Id = 100020L,
			Nick = "Player",
			Token = "Test025",
			Level = 16,
			Exp = 18,
			ShopInfo = new PlayerShopInfo(),
			Task = new TaskInfo(),
			Friends = new FriendList()
		};
		SimpleSingletonProvider<GameLogicManager>.inst.InitAccount(account, player);
		SimpleSingletonProvider<GameLogicManager>.inst.Connect();
		if (TutorialScene == TutorialScene.Tutorial1001)
		{
			StartTutorial1001();
		}
		else if (TutorialScene == TutorialScene.Tutorial1002)
		{
			StartTutorial1002();
		}
	}

	private void StartTutorial1001()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.tutorial.StartTutorialScene1001().Forget();
	}

	private void StartTutorial1002()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.tutorial.StartTutorialScene1002().Forget();
	}

	public void AddRelic(int relicId)
	{
		TutorialPlayerActionFSM system = TutorialGame.GetSystem<TutorialPlayerActionFSM>();
		long playerID = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerID();
		if (system.PlayerId != playerID)
		{
			Debug.LogError("当前行动的不是玩家");
		}
		else if (system.CurrentState != PlayerActionType.Idle)
		{
			Debug.LogError("玩家 不是空闲状态");
		}
		else
		{
			TutorialGame.GetSystem<TutorialBoardManager>().relicManager.AddRelic(playerID, relicId).Forget();
		}
	}
}
