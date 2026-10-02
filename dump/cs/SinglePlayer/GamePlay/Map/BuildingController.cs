using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SinglePlayer.AssetsHelper;
using SinglePlayer.GamePlay.Build;
using SinglePlayer.GamePlay.Card;
using SinglePlayer.GamePlay.Relic;
using SinglePlayer.Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay.Map;

public class BuildingController : IController, IInitialize, IDispose
{
	private readonly Dictionary<int, BuildingView> _buildingViewDict = new Dictionary<int, BuildingView>();

	public BuildingOperateMode buildingOperateMode;

	private BuildingView _buildingPreview;

	private int waitAddExpBuildingCount;

	private int _loadId;

	private readonly Dictionary<int, int> relicBuildingViewDict = new Dictionary<int, int>();

	public BuildingControllerFSM Fsm { get; private set; }

	public BuildingFoundation SelectedBuildingFoundation { get; set; }

	public SinglePlayer.GamePlay.Card.Card CurrentCard { get; private set; }

	public BuildingBase CurrentOperateBuilding { get; private set; }

	public async UniTask Initialize()
	{
		await UniTask.CompletedTask;
		Game.GetSystem<MonoBehaviourManager>().Update0.AddListener(Update);
		Game.GetModel<GlobalSignal>().BuildingCreate.AddListener(OnBuildingCreate);
		Game.GetModel<GlobalSignal>().BuildingRemove.AddListener(OnBuildingRemove);
		Game.GetModel<GlobalSignal>().BuildingMoveStart.AddListener(OnBuildingMoveStart);
		Game.GetModel<GlobalSignal>().BuildingMoveSuccess.AddListener(OnBuildingMoveSuccess);
		Game.GetModel<GlobalSignal>().BuildingMoveFailure.AddListener(OnBuildingMoveFailure);
		Game.GetModel<GlobalSignal>().BuildingMoveUpgrade.AddListener(OnBuildingMoveUpgrade);
		Game.GetModel<GlobalSignal>().CardUpgrade.AddListener(OnCardUpgrade);
		Game.GetModel<GlobalSignal>().CardUsed.AddListener(OnCardUsed);
		Game.GetModel<GlobalSignal>().BuildingShow = BuildingPerform;
		Game.GetModel<GlobalSignal>().AddRelic.AddListener(OnAddRelic);
		Fsm = new BuildingControllerFSM();
		Fsm.Start<IdleState>();
	}

	public void Dispose()
	{
		Game.GetModel<GlobalSignal>().BuildingCreate.RemoveListener(OnBuildingCreate);
		Game.GetModel<GlobalSignal>().BuildingRemove.RemoveListener(OnBuildingRemove);
		Game.GetModel<GlobalSignal>().BuildingMoveStart.RemoveListener(OnBuildingMoveStart);
		Game.GetModel<GlobalSignal>().BuildingMoveSuccess.RemoveListener(OnBuildingMoveSuccess);
		Game.GetModel<GlobalSignal>().BuildingMoveFailure.RemoveListener(OnBuildingMoveFailure);
		Game.GetModel<GlobalSignal>().BuildingMoveUpgrade.RemoveListener(OnBuildingMoveUpgrade);
		Game.GetModel<GlobalSignal>().CardUpgrade.RemoveListener(OnCardUpgrade);
		Game.GetModel<GlobalSignal>().CardUsed.RemoveListener(OnCardUsed);
		Game.GetSystem<MonoBehaviourManager>().Update0.RemoveListener(Update);
		Game.GetModel<GlobalSignal>().AddRelic.RemoveListener(OnAddRelic);
	}

	private void Update()
	{
		Fsm.CurrentState?.Update();
	}

	public void AddWaitAddExpBuildingCount()
	{
		waitAddExpBuildingCount++;
	}

	public void CutWaitAddExpBuildingCount()
	{
		waitAddExpBuildingCount--;
	}

	public bool HasWaitAddExpBuilding()
	{
		return waitAddExpBuildingCount > 0;
	}

	public bool TryGetBuildingView(int buildingId, out BuildingView buildingView)
	{
		return _buildingViewDict.TryGetValue(buildingId, out buildingView);
	}

	public void StartDraggingCard(SinglePlayer.GamePlay.Card.Card card)
	{
		CurrentCard = card;
		Fsm.ChangeState<DraggingCardState>();
	}

	public void SelectBuilding(BuildingFoundation buildingFoundation)
	{
		if (!Game.GetSystem<BoardManager>().buildingManager.TryGetBuildingByFoundationId(buildingFoundation.Id, out var building))
		{
			Debug.LogError($"未找到ID为{buildingFoundation.Id}的地基上的建筑物");
			return;
		}
		CurrentCard = building.Card;
		SelectedBuildingFoundation = buildingFoundation;
		CurrentOperateBuilding = building;
	}

	public void ResetBuildingOperateState()
	{
		buildingOperateMode = BuildingOperateMode.None;
		CurrentCard = null;
		CurrentOperateBuilding = null;
		SelectedBuildingFoundation = null;
		_buildingPreview = null;
		_loadId = 0;
	}

	public async UniTask ShowBuildingPreview(BuildingFoundation newTargetFoundation)
	{
		int id = ++_loadId;
		HideBuildingPreview();
		BuildingView buildingView = await Game.GetSystem<SinglePlayerAssetsHelper>().buildingAssetManager.Create(CurrentCard.GetConfigureItem(), CurrentCard.CardConfigure.Rarity);
		if (id != _loadId)
		{
			Game.GetSystem<SinglePlayerAssetsHelper>().buildingAssetManager.Recycle(buildingView);
			return;
		}
		_buildingPreview = buildingView;
		_buildingPreview.GetComponent<BuildingMaterial>().ShowPreview(Game.GetSystem<SinglePlayerAssetsHelper>().buildingAssetManager.PreviewMaterial);
		if (_buildingPreview != null)
		{
			_buildingPreview.transform.SetPositionAndRotation(newTargetFoundation.transform.position, newTargetFoundation.transform.rotation);
		}
	}

	public void DelayHideBuildingPreview()
	{
		_loadId++;
	}

	public void HideBuildingPreview()
	{
		if (_buildingPreview != null)
		{
			Game.GetSystem<SinglePlayerAssetsHelper>().buildingAssetManager.Recycle(_buildingPreview);
			_buildingPreview = null;
		}
	}

	private async UniTask BuildingPerform(AttributeChangeInfo message)
	{
		if (_buildingViewDict.TryGetValue(message.Source.id, out var value) && value.IsVaild())
		{
			switch (message.BuildingShowType)
			{
			case BuildingShowType.ThrowDice:
				await value.PlayThrowDiceShow(message);
				break;
			case BuildingShowType.Pass:
				value.OnPassShow(message);
				break;
			case BuildingShowType.Stay:
				value.PlayStayShow(message);
				break;
			default:
				throw new ArgumentOutOfRangeException();
			case BuildingShowType.None:
				break;
			}
		}
	}

	private async void OnBuildingCreate(int buildingFoundationId, int buildingId, int cardUid)
	{
		HideBuildingPreview();
		BuildingFoundation buildingFoundationById = Game.GetModel<GameData>().MapData.GetBuildingFoundationById(buildingFoundationId);
		if (!(buildingFoundationById == null))
		{
			BuildingView buildingView = await buildingFoundationById.CreateBuilding(buildingId);
			if (buildingView != null)
			{
				_buildingViewDict.Add(buildingId, buildingView);
				Game.GetModel<GlobalSignal>().BuildingViewCreate.Dispatch(buildingView);
			}
		}
	}

	private void OnBuildingRemove(int buildingFoundationId, int cardUid, int buildingId)
	{
		BuildingFoundation buildingFoundationById = Game.GetModel<GameData>().MapData.GetBuildingFoundationById(buildingFoundationId);
		if (!(buildingFoundationById == null))
		{
			buildingFoundationById.HideBuilding();
			_buildingViewDict.Remove(buildingFoundationById.BuildingId);
		}
	}

	private void OnBuildingMoveStart(int buildingFoundationId, int buildingId, int cardUid)
	{
		BuildingFoundation buildingFoundationById = Game.GetModel<GameData>().MapData.GetBuildingFoundationById(buildingFoundationId);
		if (!(buildingFoundationById == null))
		{
			buildingFoundationById.HideBuilding();
		}
	}

	private void OnBuildingMoveSuccess(int source, int id, int target)
	{
		HideBuildingPreview();
		BuildingFoundation buildingFoundationById = Game.GetModel<GameData>().MapData.GetBuildingFoundationById(target);
		if (buildingFoundationById != null)
		{
			buildingFoundationById.CreateBuilding(id).Forget();
		}
	}

	private void OnBuildingMoveFailure(int buildingFoundation, int buildingId, int cardUid)
	{
		HideBuildingPreview();
		BuildingFoundation buildingFoundationById = Game.GetModel<GameData>().MapData.GetBuildingFoundationById(buildingFoundation);
		if (!(buildingFoundationById == null))
		{
			buildingFoundationById.CreateBuilding(buildingId, revert: true).Forget();
			buildingFoundationById.Hide();
		}
	}

	private void OnBuildingMoveUpgrade()
	{
		HideBuildingPreview();
	}

	private void OnCardUpgrade(int cardUid)
	{
		if (Game.GetSystem<BoardManager>().buildingManager.TryGetBuildingByCardUid(cardUid, out var building))
		{
			BuildingFoundation buildingFoundationById = Game.GetModel<GameData>().MapData.GetBuildingFoundationById(building.BuildingFoundationId);
			if (!(buildingFoundationById == null))
			{
				buildingFoundationById.HideBuilding();
				buildingFoundationById.CreateBuilding(building.Id).Forget();
			}
		}
	}

	private void OnCardUsed(int cardUid, bool result)
	{
		if (!result)
		{
			HideBuildingPreview();
		}
	}

	private void OnAddRelic(int relicConfigId)
	{
		if (StaticConfigure.SinglePlayer.RelicDict.TryGetValue(relicConfigId, out var value))
		{
			BuildingFoundation specialBuildingFoundation = GetSpecialBuildingFoundation();
			if (!(specialBuildingFoundation == null) && !string.IsNullOrEmpty(value.Building))
			{
				relicBuildingViewDict.Add(specialBuildingFoundation.Id, relicConfigId);
				specialBuildingFoundation.CreateRelicBuilding(value.Building).Forget();
			}
		}
	}

	public void BuildRelicBuilding()
	{
		foreach (RelicInfo relic in Game.GetModel<GameData>().heroProperty.RelicList)
		{
			OnAddRelic(relic.BuffData.Id);
		}
	}

	private BuildingFoundation GetSpecialBuildingFoundation()
	{
		foreach (BuildingFoundation buildingFoundation in Game.GetModel<GameData>().MapData.BuildingFoundations)
		{
			if (buildingFoundation.Special && !relicBuildingViewDict.ContainsKey(buildingFoundation.Id))
			{
				return buildingFoundation;
			}
		}
		return null;
	}
}
