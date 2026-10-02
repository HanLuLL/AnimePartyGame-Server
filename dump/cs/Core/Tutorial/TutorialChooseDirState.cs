using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;

namespace Core.Tutorial;

public class TutorialChooseDirState : TutorialPlayerActionState
{
	public TutorialChooseDirState(PlayerActionType type, TutorialPlayerActionFSM fsm)
		: base(type, fsm)
	{
	}

	public override async UniTask OnEnter()
	{
		await base.OnEnter();
		if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial mapGimmickManager_Tutorial)
		{
			await mapGimmickManager_Tutorial.DealChooseDir(_fsm.PlayerId);
		}
	}
}
