using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using SinglePlayer.AssetsHelper;
using SinglePlayer.GamePlay.Build;
using SinglePlayer.GamePlay.Card;
using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay.Map;

public class BuildingFoundation : MonoBehaviour
{
	private int _buildingId;

	private BuildingView _buildingView;

	private SinglePlayerCardConfigureItem _config;

	private MeshRenderer _renderer;

	[SerializeField]
	public bool Wasteland;

	[SerializeField]
	private Transform _stone;

	[SerializeField]
	private Transform _shovel;

	[SerializeField]
	private Transform _platform;

	[SerializeField]
	public Transform UIInfoRoot;

	private Effect _buyLandEffect;

	private Queue<GlobalSignal.ActionAsync> _effectOperationQueue = new Queue<GlobalSignal.ActionAsync>();

	[field: SerializeField]
	public int Id { get; private set; }

	[field: SerializeField]
	public bool Special { get; private set; }

	public int BuildingId => _buildingId;

	private void Awake()
	{
		_renderer = GetComponentInChildren<MeshRenderer>();
		if (Special && TryGetComponent<Collider>(out var component))
		{
			component.enabled = false;
		}
		ChangeWastelandStatus(Wasteland);
	}

	public void Init()
	{
		Game.GetModel<GlobalSignal>().BuildingMoveSuccess.AddListener(OnBuildingMoveSuccess);
		Game.GetModel<GlobalSignal>().BuildingMoveUpgrade.AddListener(OnBuildingMoveUpgrade);
		Game.GetModel<GlobalSignal>().DragStart.AddListener(OnDragCardStart);
		Game.GetModel<GlobalSignal>().DragEnd.AddListener(OnDragCardEnd);
	}

	public void Dispose()
	{
		Game.GetModel<GlobalSignal>().BuildingMoveSuccess.RemoveListener(OnBuildingMoveSuccess);
		Game.GetModel<GlobalSignal>().BuildingMoveUpgrade.RemoveListener(OnBuildingMoveUpgrade);
		Game.GetModel<GlobalSignal>().DragStart.RemoveListener(OnDragCardStart);
		Game.GetModel<GlobalSignal>().DragEnd.RemoveListener(OnDragCardEnd);
	}

	public async UniTask<BuildingView> CreateBuilding(int buildingId, bool revert = false)
	{
		_buildingId = buildingId;
		if (!Game.GetSystem<BoardManager>().buildingManager.TryGetBuildingById(buildingId, out var building))
		{
			Debug.LogError($"#建筑物模块# 创建建筑表现失败，未找到Id={buildingId}的数据");
			return null;
		}
		_config = building.GetConfigureItem();
		if (_config == null)
		{
			return null;
		}
		_buildingView = await Game.GetSystem<SinglePlayerAssetsHelper>().buildingAssetManager.Create(building, revert);
		if (_buildingView != null)
		{
			_buildingView.transform.SetPositionAndRotation(base.transform.position, base.transform.rotation);
		}
		Hide();
		return _buildingView;
	}

	public async UniTask CreateRelicBuilding(string key)
	{
		BuildingView buildingView = await Game.GetSystem<SinglePlayerAssetsHelper>().buildingAssetManager.CreateRelicBuilding(key);
		if (buildingView != null)
		{
			buildingView.transform.SetPositionAndRotation(base.transform.position, base.transform.rotation);
		}
		buildingView.Play("SG_BuildEnter").Forget();
	}

	public void HideBuilding()
	{
		if (_buildingView != null)
		{
			Game.GetSystem<SinglePlayerAssetsHelper>().buildingAssetManager.Recycle(_buildingView);
			_buildingView = null;
		}
		_buildingId = 0;
		_config = null;
		Show();
	}

	public bool HasBuilding()
	{
		BuildingBase building;
		return Game.GetSystem<BoardManager>().buildingManager.TryGetBuildingByFoundationId(Id, out building);
	}

	public void Show()
	{
		if (_buildingId == 0 && !Special)
		{
			_renderer.enabled = true;
		}
	}

	public void Hide()
	{
		if (!Special)
		{
			_renderer.enabled = false;
		}
	}

	private void OnBuildingMoveStart(int foundationId, int buildingId, int cardConfigureId)
	{
		if (Id != foundationId && Game.GetSystem<BoardManager>().buildingManager.TryGetBuildingById(BuildingId, out var building) && building.Card.Level.Value != SinglePlayer.GamePlay.Card.Card.MaxLevel() && Game.GetSystem<BoardManager>().buildingManager.TryGetBuildingById(buildingId, out var building2) && building2.Card.Level.Value != SinglePlayer.GamePlay.Card.Card.MaxLevel() && building.Card.CardConfigure.Id == cardConfigureId)
		{
			Game.GetModel<GlobalSignal>().BuildingShowLevel.Dispatch(building.Id);
			AddEffectOperationQueue(PlayBuyLandEffect);
		}
	}

	private void OnBuildingMoveSuccess(int arg1, int buildingId, int arg3)
	{
		Game.GetModel<GlobalSignal>().BuildingHideLevel.Dispatch();
		AddEffectOperationQueue(CloseBuyLandEffect);
	}

	private void OnBuildingMoveFailure(int arg1, int buildingId, int arg3)
	{
		Game.GetModel<GlobalSignal>().BuildingHideLevel.Dispatch();
		AddEffectOperationQueue(CloseBuyLandEffect);
	}

	private void OnBuildingMoveUpgrade()
	{
		Game.GetModel<GlobalSignal>().BuildingHideLevel.Dispatch();
		AddEffectOperationQueue(CloseBuyLandEffect);
	}

	private void OnDragCardStart(int cardUid)
	{
		if (!Game.GetSystem<BoardManager>().buildingManager.TryGetBuildingById(BuildingId, out var building))
		{
			if (Game.GetController<BuildingController>().CurrentOperateBuilding == null && !Wasteland && !Special)
			{
				AddEffectOperationQueue(PlayBuyLandEffect);
			}
			return;
		}
		SinglePlayer.GamePlay.Card.Card cardByUID = Game.GetSystem<BoardManager>().cardManager.GetCardByUID(cardUid);
		if (cardByUID != null && building.Card.UID != cardUid && building.Card.Level.Value != SinglePlayer.GamePlay.Card.Card.MaxLevel() && building.Card.CardConfigure.Id == cardByUID.CardConfigure.Id)
		{
			AddEffectOperationQueue(PlayBuyLandEffect);
		}
	}

	private void OnDragCardEnd(bool cardUid)
	{
		if (!Game.GetSystem<BoardManager>().buildingManager.TryGetBuildingById(BuildingId, out var building))
		{
			if (!Wasteland && !Special)
			{
				AddEffectOperationQueue(CloseBuyLandEffect);
			}
		}
		else if (building.Card.Level.Value != SinglePlayer.GamePlay.Card.Card.MaxLevel())
		{
			AddEffectOperationQueue(CloseBuyLandEffect);
		}
	}

	private void ChangeWastelandStatus(bool wasteland)
	{
		_stone.gameObject.SetActiveEx(wasteland);
		_platform.gameObject.SetActiveEx(!wasteland);
	}

	public void SwitchSelectStatus(bool status)
	{
	}

	public void SwitchDevelopStatus(bool status)
	{
		if (status)
		{
			AddEffectOperationQueue(PlayBuyLandEffect);
		}
		else
		{
			AddEffectOperationQueue(CloseBuyLandEffect);
		}
		SwitchSelectStatus(status: false);
	}

	public void FinishDevelopLand()
	{
		Wasteland = false;
		_stone.gameObject.SetActiveEx(active: false);
		_platform.gameObject.SetActiveEx(active: true);
		AddEffectOperationQueue(CloseBuyLandEffect);
	}

	private async UniTask PlayBuyLandEffect()
	{
		_buyLandEffect = await SimpleSingletonProvider<EffectManager>.inst.PlayById(3110, base.transform.position, Quaternion.identity);
	}

	private async UniTask CloseBuyLandEffect()
	{
		await UniTask.CompletedTask;
		if (_buyLandEffect != null)
		{
			_buyLandEffect.ReleaseEffect();
			_buyLandEffect = null;
		}
	}

	private void AddEffectOperationQueue(GlobalSignal.ActionAsync action)
	{
		_effectOperationQueue.Enqueue(action);
		if (_effectOperationQueue.Count == 1)
		{
			TryExecuteEffectOperationQueue().Forget();
		}
	}

	private async UniTask TryExecuteEffectOperationQueue()
	{
		if (_effectOperationQueue.Count > 0)
		{
			await _effectOperationQueue.Peek()();
			_effectOperationQueue.Dequeue();
			await TryExecuteEffectOperationQueue();
		}
	}
}
