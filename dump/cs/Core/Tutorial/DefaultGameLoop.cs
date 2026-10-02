using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;

namespace Core.Tutorial;

public class DefaultGameLoop : IGameLoop
{
	private GamePlayManager _gamePlayManager;

	public DefaultGameLoop(GamePlayManager gamePlayManager)
	{
		_gamePlayManager = gamePlayManager;
	}

	public async UniTask<bool> Execute()
	{
		int index = 0;
		List<BattlePlayerData> players = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
		while (index < players.Count && index < 30)
		{
			BattlePlayerData player = players[index];
			index++;
			if (await DoPlayerStartAction(player))
			{
				await DoPlayerAction(player);
				await DoMoving(player);
				await DoStopEffect(player);
				await DoPlayerStopAction(player);
				if (_gamePlayManager.CancellationTokenSource.IsCancellationRequested)
				{
					return false;
				}
			}
		}
		return true;
	}

	public void Dispose()
	{
		_gamePlayManager = null;
	}

	private async UniTask<bool> DoPlayerStartAction(BattlePlayerData player)
	{
		if (player.characterType == CharacterType.Monster)
		{
			int id = player.player.characterConfig.Id;
			TutorialBaseMonster monsterById = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetMonsterById(id);
			if (monsterById != null && !monsterById.ActionStatus())
			{
				return false;
			}
		}
		bool result = await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.PlayerStartAction(player);
		await TutorialGame.GetSystem<TutorialBoardManager>().buffManager.ActionStart(player.player.Id);
		return result;
	}

	private async UniTask DoPlayerAction(BattlePlayerData player)
	{
		if (!_gamePlayManager.CancellationTokenSource.IsCancellationRequested)
		{
			await TutorialGame.GetSystem<TutorialPlayerActionFSM>().StartAction(PlayerActionType.Idle, player.player.Id);
		}
	}

	private async UniTask DoMoving(BattlePlayerData player)
	{
		if (!_gamePlayManager.CancellationTokenSource.IsCancellationRequested && player.Property.HP.Value > 0)
		{
			await TutorialGame.GetSystem<TutorialPlayerActionFSM>().StartAction(PlayerActionType.Moving, player.player.Id);
		}
	}

	private async UniTask DoStopEffect(BattlePlayerData player)
	{
		if (!_gamePlayManager.CancellationTokenSource.IsCancellationRequested && player.characterType != CharacterType.Monster && player.Property.HP.Value > 0)
		{
			await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.PlayStopHandler(player);
		}
	}

	private async UniTask DoPlayerStopAction(BattlePlayerData player)
	{
		if (!_gamePlayManager.CancellationTokenSource.IsCancellationRequested && player.characterType != CharacterType.Monster)
		{
			await TutorialGame.GetSystem<TutorialBoardManager>().buffManager.ActionEnd(player.player.Id);
		}
	}

	public async UniTask MoveAgain(BattlePlayerData player)
	{
		if (!_gamePlayManager.CancellationTokenSource.IsCancellationRequested)
		{
			await TutorialGame.GetSystem<TutorialPlayerActionFSM>().StartAction(PlayerActionType.ThrowDice, player.player.Id);
			await DoMoving(player);
			await DoStopEffect(player);
		}
	}
}
