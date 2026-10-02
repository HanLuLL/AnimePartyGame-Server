using Cysharp.Threading.Tasks;
using GameLogic;
using SinglePlayer.GamePlay.Action;
using SinglePlayer.GamePlay.Map;
using Tools;
using UI;
using UnityEngine;

namespace SinglePlayer.GamePlay;

public class DefaultGameLoop : IGameLoop
{
	private GamePlayManager _gamePlayManager;

	public DefaultGameLoop(GamePlayManager gamePlayManager)
	{
		_gamePlayManager = gamePlayManager;
	}

	public async UniTask<bool> Execute()
	{
		GameData gameData = Game.GetModel<GameData>();
		gameData.SetRoundTiming(RoundTiming.UserOperation);
		await DoMapMissionSettlement();
		GameStatus status = Game.GetModel<GameData>().GetGameStatus();
		if (status != GameStatus.Running)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.friend.HandleSinglePlayerDataAfterBattle();
			if (await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.SinglePlayerSettlement) is SinglePlayerSettlementPanel singlePlayerSettlementPanel)
			{
				singlePlayerSettlementPanel.ShowResult(status);
			}
			Game.GetModel<GameData>().UploadDataToServer(status);
			return false;
		}
		if (status != GameStatus.Running)
		{
			Game.GetModel<GameData>().UploadDataToServer(status);
			return false;
		}
		await DoMapMission();
		await DoSomething1();
		await DoSomething2();
		await DoSomething3();
		if (Game.GetSystem<BoardManager>().cardManager.HasWaitDisplayRemoveCards)
		{
			await UniTask.Delay(300);
			Game.GetSystem<BoardManager>().cardManager.RemoveWaitDisplayRemoveCards();
		}
		int frame = 0;
		int maxFrame = 300;
		BoardCharacterManager characterManager = Game.GetSystem<BoardManager>().characterManager;
		BuildingController buildingController = Game.GetController<BuildingController>();
		bool isWaitSuccess = false;
		while (frame < maxFrame)
		{
			frame++;
			if (!buildingController.HasWaitAddExpBuilding() && !characterManager.IsProcessing)
			{
				isWaitSuccess = true;
				break;
			}
			await UniTask.DelayFrame(1);
		}
		if (!isWaitSuccess)
		{
			Debug.LogError("等待超时！ 建筑行为等待失败");
		}
		gameData.SetRoundTiming(RoundTiming.UserOperationEnd);
		Game.GetModel<GameData>().UploadDataToServer(status);
		return true;
	}

	public void Dispose()
	{
		_gamePlayManager = null;
	}

	private async UniTask DoSomething1()
	{
		await Game.GetSystem<PlayerActionFSM>().StartAction(PlayerActionType.Idle);
	}

	private async UniTask DoSomething2()
	{
		if (!_gamePlayManager.CancellationTokenSource.IsCancellationRequested)
		{
			await Game.GetSystem<PlayerActionFSM>().StartAction(PlayerActionType.Moving);
		}
	}

	private async UniTask DoSomething3()
	{
		if (!_gamePlayManager.CancellationTokenSource.IsCancellationRequested)
		{
			int standLandId = Game.GetModel<GameData>().heroProperty.StandLandId;
			Land landById = Game.GetModel<GameData>().MapData.GetLandById(standLandId);
			if (landById != null && landById.landComponent != null)
			{
				await landById.landComponent.StepOnEffect();
			}
		}
	}

	private async UniTask DoMapMission()
	{
		await Game.GetSystem<BoardManager>().missionManager.TryStartMission();
	}

	private async UniTask DoMapMissionSettlement()
	{
		MapMission currentMission = Game.GetSystem<BoardManager>().missionManager.GetCurrentMissionData();
		if (currentMission != null)
		{
			if (currentMission.GetStatus() == MapMissionStatus.Success)
			{
				await Game.GetSystem<PlayerActionFSM>().StartAction(PlayerActionType.SettleMission);
			}
			currentMission.SettleMissionStatus();
		}
	}
}
