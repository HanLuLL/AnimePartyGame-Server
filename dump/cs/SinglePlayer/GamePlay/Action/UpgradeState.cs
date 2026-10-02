using Cysharp.Threading.Tasks;

namespace SinglePlayer.GamePlay.Action;

public class UpgradeState : PlayerActionState
{
	public UpgradeState(PlayerActionType type, PlayerActionFSM fsm)
		: base(type, fsm)
	{
	}

	public override async UniTask OnEnter()
	{
		await base.OnEnter();
		_fsm.TryClosePlayerHandle();
		await Game.GetSystem<BoardManager>().characterManager.Upgrade();
		_fsm.OnActionFinished();
	}
}
