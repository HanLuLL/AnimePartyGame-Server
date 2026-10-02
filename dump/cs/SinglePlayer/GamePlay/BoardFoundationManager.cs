using Cysharp.Threading.Tasks;
using FairyGUI;
using SinglePlayer.GamePlay.Action;
using SinglePlayer.GamePlay.Map;
using SinglePlayer.Tools;
using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay;

public class BoardFoundationManager
{
	private MapData _mapData;

	private PlayerActionFSM _playerActionFSM;

	private BuildingFoundation _buildingFoundation;

	public int DevelopLandPrice
	{
		get
		{
			SinglePlayerParamConfigure safeByIndex = StaticConfigure.SinglePlayer.Params.GetSafeByIndex(0);
			if (safeByIndex == null || safeByIndex.DevelopLand.Count == 0)
			{
				Debug.LogError("无法取得 单人玩法参数配置");
				return 0;
			}
			int developLandCount = Game.GetModel<GameData>().heroProperty.DevelopLandCount;
			int safeByIndex2 = safeByIndex.DevelopLand.GetSafeByIndex(0);
			int safeByIndex3 = safeByIndex.DevelopLand.GetSafeByIndex(1);
			int safeByIndex4 = safeByIndex.DevelopLand.GetSafeByIndex(2);
			return Mathf.Clamp(safeByIndex2 + developLandCount * safeByIndex3, safeByIndex2, safeByIndex4);
		}
	}

	public void Initialize()
	{
		_mapData = Game.GetModel<GameData>().MapData;
		_playerActionFSM = Game.GetSystem<PlayerActionFSM>();
		Game.GetSystem<MonoBehaviourManager>().Update1.AddListener(Update1);
	}

	public void Dispose()
	{
		Game.GetSystem<MonoBehaviourManager>().Update1.AddListener(Update1);
	}

	private void Update1(bool isDown, bool isReleased, bool isHolding)
	{
		if (_playerActionFSM.CurrentState == PlayerActionType.DevelopLand && isDown && !Stage.isTouchOnUI)
		{
			if (_buildingFoundation != null)
			{
				_buildingFoundation.SwitchSelectStatus(status: false);
			}
			Transform elementByRay = _mapData.GetElementByRay(Game.BuildingFoundationLayerName);
			if (elementByRay == null || !elementByRay.TryGetComponent<BuildingFoundation>(out _buildingFoundation) || !_buildingFoundation.Wasteland)
			{
				Game.GetModel<GlobalSignal>().SelectDevelopLand.Dispatch(0);
				return;
			}
			_buildingFoundation.SwitchSelectStatus(status: true);
			Game.GetModel<GlobalSignal>().SelectDevelopLand.Dispatch(_buildingFoundation.Id);
		}
	}

	public void StopDevelopLand()
	{
	}

	public void CancelDevelopLand()
	{
		foreach (BuildingFoundation buildingFoundation in _mapData.BuildingFoundations)
		{
			if (buildingFoundation.Wasteland)
			{
				buildingFoundation.SwitchDevelopStatus(status: false);
			}
		}
	}

	public void StartDevelopLand()
	{
		foreach (BuildingFoundation buildingFoundation in _mapData.BuildingFoundations)
		{
			if (buildingFoundation.Wasteland)
			{
				buildingFoundation.SwitchDevelopStatus(status: true);
			}
		}
	}

	public void TryDevelopLand(int id)
	{
		int developLandPrice = DevelopLandPrice;
		BuildingFoundation buildingFoundationById = _mapData.GetBuildingFoundationById(id);
		if (buildingFoundationById == null || !buildingFoundationById.Wasteland)
		{
			Debug.LogError($"选中的地基：{id} 存在问题, 荒地：{buildingFoundationById?.Wasteland}");
		}
		else if (Game.GetSystem<BoardManager>().CheckGold(developLandPrice))
		{
			Game.GetSystem<BoardManager>().characterManager.ChangeHeroGold(-developLandPrice).Forget();
			Game.GetModel<GameData>().heroProperty.AddDevelopCount();
			buildingFoundationById.FinishDevelopLand();
			Game.GetModel<GlobalSignal>().DevelopLandSucceed.Dispatch(_mapData.GetWastelandCount());
		}
	}
}
