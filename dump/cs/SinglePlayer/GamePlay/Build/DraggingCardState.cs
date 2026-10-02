using SinglePlayer.GamePlay.Map;
using UnityEngine;

namespace SinglePlayer.GamePlay.Build;

public class DraggingCardState : DraggingBaseState
{
	public override void Enter()
	{
		base.Enter();
		_buildingController.buildingOperateMode = BuildingOperateMode.Card;
	}

	protected override BuildingFoundation GetVailBuildingFoundation(int cardConfigureId)
	{
		Transform elementByRay = _mapData.GetElementByRay(Game.BuildingFoundationLayerName);
		if (elementByRay != null && elementByRay.TryGetComponent<BuildingFoundation>(out var component))
		{
			return component;
		}
		return null;
	}
}
