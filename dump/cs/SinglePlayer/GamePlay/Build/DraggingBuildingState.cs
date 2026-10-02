namespace SinglePlayer.GamePlay.Build;

public class DraggingBuildingState : DraggingBaseState
{
	public override void Enter()
	{
		if (Game.GetSystem<BoardManager>().buildingManager.TryGetBuildingByFoundationId(_buildingController.SelectedBuildingFoundation.Id, out var building))
		{
			base.Enter();
			Game.GetController<CameraController>().SetEnable(enable: false);
			Game.GetModel<GlobalSignal>().BuildingMoveStart.Dispatch(_buildingController.SelectedBuildingFoundation.Id, building.Id, building.Card.CardConfigure.Id);
			_buildingController.buildingOperateMode = BuildingOperateMode.Building;
		}
	}

	public override void Exit()
	{
		Game.GetController<CameraController>().SetEnable(enable: true);
	}
}
