using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UnityEngine;

namespace Core.Tutorial;

public class TutorialPlayerActionFSM : ISystem, IInitialize, IDispose
{
	private readonly Dictionary<PlayerActionType, IPlayerActionState> _states = new Dictionary<PlayerActionType, IPlayerActionState>();

	private UniTaskCompletionSource _playerActionTsc;

	public long PlayerId;

	public PlayerActionType CurrentState { get; private set; }

	public async UniTask Initialize()
	{
		await UniTask.CompletedTask;
		RegisterState(new TutorialPlayerIdleState(PlayerActionType.Idle, this));
		RegisterState(new TutorialUseCardState(PlayerActionType.UseCard, this));
		RegisterState(new TutorialThrowDiceState(PlayerActionType.ThrowDice, this));
		RegisterState(new TutorialMovingState(PlayerActionType.Moving, this));
		RegisterState(new TutorialMoveStopState(PlayerActionType.MoveStop, this));
		RegisterState(new TutorialEncounterState(PlayerActionType.Encounter, this));
		RegisterState(new TutorialChooseDirState(PlayerActionType.ChooseDir, this));
	}

	public void Dispose()
	{
	}

	private void RegisterState(TutorialPlayerActionState state)
	{
		_states[state.ActionType] = state;
	}

	public async UniTask SwitchState(PlayerActionType nextState)
	{
		if (!TutorialGame.GetSystem<GamePlayManager>().CancellationTokenSource.IsCancellationRequested && CurrentState != nextState)
		{
			if (_states.TryGetValue(CurrentState, out var value))
			{
				await value.OnExit();
			}
			CurrentState = nextState;
			if (_states.TryGetValue(nextState, out var value2))
			{
				await value2.OnEnter();
			}
			else
			{
				Debug.LogError($"玩家状态机  玩家{PlayerId} 未注册：{nextState}， 请检查");
			}
		}
	}

	public void OnActionFinished()
	{
		switch (CurrentState)
		{
		case PlayerActionType.UseCard:
			SwitchState(PlayerActionType.Idle).Forget();
			break;
		case PlayerActionType.Idle:
		case PlayerActionType.ThrowDice:
			_playerActionTsc?.TrySetResult();
			break;
		case PlayerActionType.Moving:
			SwitchState(PlayerActionType.MoveStop).Forget();
			break;
		case PlayerActionType.MoveStop:
			_playerActionTsc?.TrySetResult();
			break;
		}
	}

	public async UniTask StartAction(PlayerActionType actionState, long playerId)
	{
		_playerActionTsc = new UniTaskCompletionSource();
		SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts().Token.Register(delegate
		{
			_playerActionTsc.TrySetCanceled();
		});
		PlayerId = playerId;
		CurrentState = PlayerActionType.None;
		await SwitchState(actionState);
		await _playerActionTsc.Task;
	}

	public void TryDispatchPlayerHandle()
	{
	}

	public void TryClosePlayerHandle()
	{
	}

	public BattlePlayerData GetActionPlayerData()
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(PlayerId);
	}
}
