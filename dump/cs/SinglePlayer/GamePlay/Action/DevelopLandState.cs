using Cysharp.Threading.Tasks;

namespace SinglePlayer.GamePlay.Action;

public class DevelopLandState : PlayerActionState
{
	public DevelopLandState(PlayerActionType type, PlayerActionFSM fsm)
		: base(type, fsm)
	{
	}

	public override async UniTask OnEnter()
	{
		await base.OnEnter();
		_fsm.TryClosePlayerHandle();
		Game.GetSystem<BoardManager>().foundationManager.StartDevelopLand();
	}

	public override UniTask OnExit()
	{
		Game.GetSystem<BoardManager>().foundationManager.CancelDevelopLand();
		return base.OnExit();
	}
}
