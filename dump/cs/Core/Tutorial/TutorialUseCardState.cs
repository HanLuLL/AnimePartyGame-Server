using Cysharp.Threading.Tasks;

namespace Core.Tutorial;

public class TutorialUseCardState : TutorialPlayerActionState
{
	public TutorialUseCardState(PlayerActionType type, TutorialPlayerActionFSM fsm)
		: base(type, fsm)
	{
	}

	public override async UniTask OnEnter()
	{
		await base.OnEnter();
		_fsm.TryClosePlayerHandle();
	}
}
