using Cysharp.Threading.Tasks;

namespace SinglePlayer.GamePlay.Action;

public class PlayerIdleState : PlayerActionState
{
	public PlayerIdleState(PlayerActionType type, PlayerActionFSM fsm)
		: base(type, fsm)
	{
	}

	public override async UniTask OnEnter()
	{
		await base.OnEnter();
		_fsm.TryDispatchPlayerHandle();
	}
}
