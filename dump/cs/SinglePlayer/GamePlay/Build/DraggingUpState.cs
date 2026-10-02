using SinglePlayer.GamePlay.Action;

namespace SinglePlayer.GamePlay.Build;

public class DraggingUpState : BuildingOperateBaseState
{
	public override void Enter()
	{
		switch (_buildingController.buildingOperateMode)
		{
		case BuildingOperateMode.Card:
			HandleCardPressUp();
			break;
		case BuildingOperateMode.Building:
			HandleBuildingPressUp();
			break;
		}
		Game.GetModel<GlobalSignal>().DragEnd.Dispatch(_buildingController.SelectedBuildingFoundation != null);
		_buildingController.Fsm.ChangeState<IdleState>();
		Game.GetSystem<PlayerActionFSM>().OnActionFinished();
	}

	public override void Exit()
	{
		_buildingController.ResetBuildingOperateState();
	}

	private void HandleCardPressUp()
	{
		if (_buildingController.SelectedBuildingFoundation != null && !_buildingController.SelectedBuildingFoundation.Wasteland)
		{
			Game.GetSystem<BoardManager>().buildingManager.AddBuilding(_buildingController.SelectedBuildingFoundation.Id, _buildingController.CurrentCard.UID);
		}
		else
		{
			Game.GetModel<GlobalSignal>().CardUsed.Dispatch(_buildingController.CurrentCard.UID, t2: false);
		}
	}

	private void HandleBuildingPressUp()
	{
		if (_buildingController.SelectedBuildingFoundation != null)
		{
			Game.GetSystem<BoardManager>().buildingManager.HandleMoveBuildingToTarget(_buildingController.CurrentOperateBuilding.BuildingFoundationId, _buildingController.SelectedBuildingFoundation.Id);
		}
		else
		{
			Game.GetSystem<BoardManager>().buildingManager.MoveBuildingFailure(_buildingController.CurrentOperateBuilding.BuildingFoundationId);
		}
	}
}
