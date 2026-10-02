using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;

namespace Core.Tutorial;

public class TutorialBoardManager : ISystem, IInitialize, IDispose
{
	public readonly TutorialBoardGameManager gameManager = new TutorialBoardGameManager();

	public readonly TutorialBoardCharacterManager characterManager = new TutorialBoardCharacterManager();

	public readonly TutorialBoardCardManager cardManager = new TutorialBoardCardManager();

	public readonly TutorialBoardRelicManager relicManager = new TutorialBoardRelicManager();

	public readonly TutorialBoardMissionManager missionManager = new TutorialBoardMissionManager();

	public readonly TutorialBoardLandManager landManager = new TutorialBoardLandManager();

	public readonly TutorialBoardBuffManager buffManager = new TutorialBoardBuffManager();

	public async UniTask Initialize()
	{
		await UniTask.CompletedTask;
		gameManager.Initialize();
		cardManager.Initialize();
		missionManager.Initialize();
		relicManager.Initialize();
		characterManager.Initialize();
		landManager.Initialize();
		buffManager.Initialize();
	}

	public void Dispose()
	{
		gameManager.Dispose();
		cardManager.Dispose();
		missionManager.Dispose();
		relicManager.Dispose();
		characterManager.Dispose();
		landManager.Dispose();
		buffManager.Dispose();
	}

	public async UniTask DispatchDeadEvent(long player)
	{
		if (!(SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1002 mapGimmickManager_Tutorial) || mapGimmickManager_Tutorial.BossData.player.Id != player)
		{
			await buffManager.DealDeadEvent(player);
			await missionManager.DealDeadEvent(player);
		}
		else
		{
			gameManager.SetTutorialStatus(TutorialStatus.Success);
		}
	}

	public async UniTask OnPKStart(long attackPlayerId, long defendPlayerId)
	{
		await buffManager.OnPKStart(attackPlayerId, defendPlayerId);
	}

	public async UniTask OnPKEnd(long attackPlayerId, long defendPlayerId)
	{
		long actionPlayer = TutorialGame.GetSystem<TutorialPlayerActionFSM>().PlayerId;
		await buffManager.OnPKEnd(actionPlayer, defendPlayerId);
		await characterManager.OnPKEnd(actionPlayer);
	}
}
