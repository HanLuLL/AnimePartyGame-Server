using Cysharp.Threading.Tasks;

namespace Core.Tutorial;

public abstract class TutorialPlayerActionState : IPlayerActionState
{
	public readonly PlayerActionType ActionType;

	protected readonly TutorialPlayerActionFSM _fsm;

	protected TutorialPlayerActionState(PlayerActionType type, TutorialPlayerActionFSM fsm)
	{
		ActionType = type;
		_fsm = fsm;
	}

	public virtual async UniTask OnEnter()
	{
		_fsm.TryClosePlayerHandle();
		await UniTask.CompletedTask;
	}

	public virtual async UniTask OnExit()
	{
		await UniTask.CompletedTask;
	}
}
