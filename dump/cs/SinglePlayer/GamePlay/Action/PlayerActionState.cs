using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SinglePlayer.GamePlay.Action;

public abstract class PlayerActionState : IPlayerActionState
{
	public PlayerActionType ActionType;

	protected readonly PlayerActionFSM _fsm;

	public PlayerActionState(PlayerActionType type, PlayerActionFSM fsm)
	{
		ActionType = type;
		_fsm = fsm;
	}

	public virtual async UniTask OnEnter()
	{
		Debug.Log($"进入{ActionType}");
		_fsm.TryClosePlayerHandle();
		await UniTask.CompletedTask;
	}

	public virtual async UniTask OnExit()
	{
		Debug.Log($"离开{ActionType}");
		await UniTask.CompletedTask;
	}
}
