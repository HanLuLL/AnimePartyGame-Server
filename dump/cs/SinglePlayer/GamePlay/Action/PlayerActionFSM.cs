using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay.Action;

public class PlayerActionFSM : ISystem, IInitialize, IDispose
{
	private readonly Dictionary<PlayerActionType, IPlayerActionState> _states = new Dictionary<PlayerActionType, IPlayerActionState>();

	private UniTaskCompletionSource _playerActionTsc;

	public PlayerActionType CurrentState { get; private set; }

	public async UniTask Initialize()
	{
		await UniTask.CompletedTask;
		RegisterState(new PlayerIdleState(PlayerActionType.Idle, this));
		RegisterState(new UseCardState(PlayerActionType.UseCard, this));
		RegisterState(new ThrowDiceState(PlayerActionType.ThrowDice, this));
		RegisterState(new DevelopLandState(PlayerActionType.DevelopLand, this));
		RegisterState(new MovingState(PlayerActionType.Moving, this));
		RegisterState(new MoveStopState(PlayerActionType.MoveStop, this));
		RegisterState(new SettleMissionState(PlayerActionType.SettleMission, this));
	}

	public void Dispose()
	{
	}

	private void RegisterState(PlayerActionState state)
	{
		_states[state.ActionType] = state;
	}

	public async UniTask SwitchState(PlayerActionType nextState)
	{
		if (CurrentState != nextState)
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
				Debug.LogError($"玩家状态机 未注册：{nextState}， 请检查");
			}
		}
	}

	public void OnActionFinished()
	{
		switch (CurrentState)
		{
		case PlayerActionType.UseCard:
		case PlayerActionType.DevelopLand:
			SwitchState(PlayerActionType.Idle).Forget();
			break;
		case PlayerActionType.ThrowDice:
			_playerActionTsc?.TrySetResult();
			break;
		case PlayerActionType.Moving:
			SwitchState(PlayerActionType.MoveStop).Forget();
			break;
		case PlayerActionType.MoveStop:
		case PlayerActionType.SettleMission:
			_playerActionTsc?.TrySetResult();
			break;
		}
	}

	public async UniTask StartAction(PlayerActionType actionState)
	{
		_playerActionTsc = new UniTaskCompletionSource();
		SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts().Token.Register(delegate
		{
			_playerActionTsc.TrySetCanceled();
		});
		CurrentState = PlayerActionType.None;
		await SwitchState(actionState);
		await _playerActionTsc.Task;
	}

	public void TryDispatchPlayerHandle()
	{
		Game.GetModel<GlobalSignal>().Card.Dispatch(t: true);
		Game.GetModel<GlobalSignal>().ThrowDice.Dispatch(t: true);
		Game.GetModel<GlobalSignal>().DevelopLand.Dispatch(t: true);
	}

	public void TryClosePlayerHandle()
	{
		Game.GetModel<GlobalSignal>().Card.Dispatch(t: false);
		Game.GetModel<GlobalSignal>().ThrowDice.Dispatch(t: false);
		Game.GetModel<GlobalSignal>().DevelopLand.Dispatch(t: false);
	}
}
