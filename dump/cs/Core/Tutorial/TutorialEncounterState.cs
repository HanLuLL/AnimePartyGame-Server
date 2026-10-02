using Cysharp.Threading.Tasks;

namespace Core.Tutorial;

public class TutorialEncounterState : TutorialPlayerActionState
{
	public TutorialEncounterState(PlayerActionType type, TutorialPlayerActionFSM fsm)
		: base(type, fsm)
	{
	}

	public override async UniTask OnEnter()
	{
		await base.OnEnter();
		await TutorialGame.GetSystem<TutorialBoardManager>().gameManager.DealEncounterWithPlayer(_fsm.PlayerId);
	}
}
