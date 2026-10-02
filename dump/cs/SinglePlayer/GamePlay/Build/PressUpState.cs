namespace SinglePlayer.GamePlay.Build;

public class PressUpState : BuildingOperateBaseState
{
	public override void Enter()
	{
		base.Enter();
		if (_buildingController.SelectedBuildingFoundation != null && Game.GetSystem<BoardManager>().buildingManager.TryGetBuildingByFoundationId(_buildingController.SelectedBuildingFoundation.Id, out var building) && _buildingController.TryGetBuildingView(building.Id, out var buildingView))
		{
			Game.GetModel<GlobalSignal>().ShowBuildingInfo.Dispatch(t1: true, building, buildingView.GetPosition());
		}
		_buildingController.Fsm.ChangeState<IdleState>();
	}
}
