using Cysharp.Threading.Tasks;
using SinglePlayer.GamePlay.Map;

namespace SinglePlayer.GamePlay.Action;

public class MoveStopState : PlayerActionState
{
	public MoveStopState(PlayerActionType type, PlayerActionFSM fsm)
		: base(type, fsm)
	{
	}

	public override async UniTask OnEnter()
	{
		await base.OnEnter();
		GameData model = Game.GetModel<GameData>();
		int standLandId = model.heroProperty.StandLandId;
		Land landById = model.MapData.GetLandById(standLandId);
		if (landById != null)
		{
			Game.GetModel<GlobalSignal>().MoveStop.Dispatch(landById);
		}
		if (Game.GetSystem<BoardManager>().characterManager.CanMove())
		{
			await _fsm.SwitchState(PlayerActionType.Moving);
			return;
		}
		Game.GetSystem<BoardManager>().buildingManager.DoEffect();
		await Game.GetSystem<BoardManager>().characterManager.PlayAttributeShow();
		_fsm.OnActionFinished();
	}
}
