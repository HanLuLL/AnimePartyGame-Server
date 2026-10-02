using Cysharp.Threading.Tasks;

namespace SinglePlayer.GamePlay.Action;

public class UseCardState : PlayerActionState
{
	public UseCardState(PlayerActionType type, PlayerActionFSM fsm)
		: base(type, fsm)
	{
	}

	public override async UniTask OnEnter()
	{
		await base.OnEnter();
		_fsm.TryClosePlayerHandle();
	}
}
