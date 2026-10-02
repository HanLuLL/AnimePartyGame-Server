using Cysharp.Threading.Tasks;
using SinglePlayer.GamePlay.Action;
using SinglePlayer.GamePlay.Map;
using UnityEngine;

namespace SinglePlayer.GamePlay.Build;

public class DraggingBaseState : BuildingOperateBaseState
{
	private BoardManager _boardManager;

	private GlobalSignal _globalSignal;

	public override void Enter()
	{
		base.Enter();
		_boardManager = Game.GetSystem<BoardManager>();
		_globalSignal = Game.GetModel<GlobalSignal>();
		_globalSignal.DragStart.Dispatch(_buildingController.CurrentCard.UID);
		_playerActionFSM.SwitchState(PlayerActionType.UseCard).Forget();
	}

	public override void Update()
	{
		if (_playerActionFSM.CurrentState != PlayerActionType.UseCard)
		{
			return;
		}
		_globalSignal.DragMoving.Dispatch();
		if (_monoBehaviourManager.IsHolding)
		{
			BuildingFoundation vailBuildingFoundation = GetVailBuildingFoundation(_buildingController.CurrentCard.CardConfigure.Id);
			if (vailBuildingFoundation != _buildingController.SelectedBuildingFoundation)
			{
				if (_buildingController.SelectedBuildingFoundation != null)
				{
					_buildingController.SelectedBuildingFoundation.Show();
				}
				if (vailBuildingFoundation != null && !vailBuildingFoundation.Wasteland && _boardManager.buildingManager.CanUsedCardInBuildingFoundation(vailBuildingFoundation.Id, _buildingController.CurrentCard))
				{
					_buildingController.ShowBuildingPreview(vailBuildingFoundation).Forget();
					_boardManager.cardManager.GetCardByUID(_buildingController.CurrentCard.UID).CanPlaced = true;
					vailBuildingFoundation.Hide();
				}
				else
				{
					_buildingController.HideBuildingPreview();
					_buildingController.CurrentCard.CanPlaced = false;
				}
			}
			if (vailBuildingFoundation == null)
			{
				_buildingController.CurrentCard.CanPlaced = false;
				_buildingController.DelayHideBuildingPreview();
			}
			_buildingController.SelectedBuildingFoundation = vailBuildingFoundation;
		}
		if (_monoBehaviourManager.IsReleased)
		{
			_buildingController.Fsm.ChangeState<DraggingUpState>();
		}
	}

	public override void Exit()
	{
		base.Exit();
		_buildingController.HideBuildingPreview();
	}

	protected virtual BuildingFoundation GetVailBuildingFoundation(int cardConfigureId)
	{
		Transform elementByRay = _mapData.GetElementByRay(Game.BuildingFoundationLayerName);
		if (elementByRay == null || !elementByRay.TryGetComponent<BuildingFoundation>(out var component))
		{
			return null;
		}
		if (!Game.GetSystem<BoardManager>().buildingManager.TryGetBuildingById(component.BuildingId, out var building))
		{
			return null;
		}
		if (building.Card.CardConfigure.Id == cardConfigureId)
		{
			return component;
		}
		return null;
	}
}
