using Cysharp.Threading.Tasks;

namespace Core.Tutorial;

public class TutorialSettleMissionState : TutorialPlayerActionState
{
	public TutorialSettleMissionState(PlayerActionType type, TutorialPlayerActionFSM fsm)
		: base(type, fsm)
	{
	}

	public override async UniTask OnEnter()
	{
		await base.OnEnter();
	}
}
