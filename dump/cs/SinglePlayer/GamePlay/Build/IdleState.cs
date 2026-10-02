using SinglePlayer.GamePlay.Action;
using SinglePlayer.GamePlay.Map;
using UnityEngine;

namespace SinglePlayer.GamePlay.Build;

public class IdleState : BuildingOperateBaseState
{
	public override void Update()
	{
		if (_playerActionFSM.CurrentState != PlayerActionType.Idle || !_monoBehaviourManager.IsPressedDown)
		{
			return;
		}
		Transform elementByRay = _mapData.GetElementByRay(Game.BuildingFoundationLayerName);
		if (!(elementByRay == null))
		{
			BuildingFoundation component = elementByRay.GetComponent<BuildingFoundation>();
			if (!(component == null) && component.HasBuilding())
			{
				_buildingController.SelectBuilding(component);
				_buildingController.Fsm.ChangeState<PressDownState>();
			}
		}
	}
}
