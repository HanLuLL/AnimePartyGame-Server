using Cysharp.Threading.Tasks;

namespace SinglePlayer.GamePlay.Action;

public class SettleMissionState : PlayerActionState
{
	public SettleMissionState(PlayerActionType type, PlayerActionFSM fsm)
		: base(type, fsm)
	{
	}

	public override async UniTask OnEnter()
	{
		await base.OnEnter();
		if (!Game.GetSystem<BoardManager>().missionManager.SettleMission())
		{
			_fsm.OnActionFinished();
		}
		else
		{
			Game.GetModel<GlobalSignal>().SettleMission.Dispatch();
		}
	}
}
