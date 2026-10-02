using UnityEngine;

namespace SinglePlayer.GamePlay.Build;

public class PressDownState : BuildingOperateBaseState
{
	private Vector3 _pressDownPosition;

	public override void Enter()
	{
		_pressDownPosition = _monoBehaviourManager.MouseTouchPosition;
		Game.GetController<CameraController>().SetEnable(enable: false);
	}

	public override void Update()
	{
		if (!_monoBehaviourManager.IsHolding)
		{
			_buildingController.Fsm.ChangeState<PressUpState>();
		}
		else if (_monoBehaviourManager.MouseTouchPosition != Vector3.zero && Vector3.Distance(_monoBehaviourManager.MouseTouchPosition, _pressDownPosition) > 20f)
		{
			_buildingController.Fsm.ChangeState<DraggingBuildingState>();
		}
	}

	public override void Exit()
	{
		Game.GetController<CameraController>().SetEnable(enable: true);
	}
}
