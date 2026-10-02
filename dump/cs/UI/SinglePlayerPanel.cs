using System;
using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using SinglePlayer;
using SinglePlayer.GamePlay;
using SinglePlayer.GamePlay.Action;
using SinglePlayer.GamePlay.Build;
using SinglePlayer.GamePlay.Card;
using SinglePlayer.GamePlay.Character;
using SinglePlayer.GamePlay.Map;
using SinglePlayer.GamePlay.Relic;
using SinglePlayer.Tools;
using Tools;
using UnityEngine;

namespace UI;

public class SinglePlayerPanel : BasePanel<UISinglePlayerPanel>
{
	private class HeroListener
	{
		public Hero Hero;

		public Action<int> OnHpChange;

		public Action<int> OnGoldChange;
	}

	private UISinglePlayer_Button_DragArea _dragArea;

	private readonly ReactiveProperty<int> _selectBagSlotIndex = new ReactiveProperty<int>(-1);

	private UISinglePlayer_Com_BuildingInfo _buildingInfo;

	private UISinglePlayer_Com_SettleMission _settleMission;

	private GameData _gameData;

	private GlobalSignal _globalSignal;

	private PlayerActionFSM _playerActionFSM;

	private BoardCharacterManager _characterManager;

	private BoardGameManager _gameManager;

	private Dictionary<IUnitView, UISinglePlayer_Com_UnitAttrInfo> _buildingAttrInfos;

	private Dictionary<int, UISinglePlayer_Com_BuildingLevel> _buildingLevelInfo;

	private List<HeroListener> heroListeners = new List<HeroListener>();

	private float fadeTime = 0.2f;

	private GTweener shopFadeTweener;

	private int targetIndex = -1;

	private bool _reclaimLandState;

	private UISinglePlayer_Com_RelicInfo _relicInfo;

	private void DragStart(int cardUID)
	{
		GRoot.inst.ShowPopup(_dragArea);
		_dragArea.ShowCreateInfo(cardUID);
		Game.GetController<CameraController>().SetEnable(enable: false);
	}

	private void DragMoving()
	{
		_dragArea.position = GRoot.inst.GlobalToLocal(Stage.inst.touchPosition);
		if (!_dragArea.visible && !_dragArea.CurrentCard.CanPlaced)
		{
			GRoot.inst.ShowPopup(_dragArea);
			_dragArea.ShowCreateInfo(_dragArea.CurrentCard.UID);
		}
		else if (_dragArea.visible && _dragArea.CurrentCard.CanPlaced)
		{
			_dragArea.HideInfo();
		}
		CheckPurchaseCard();
	}

	private void DragEnd(bool success)
	{
		Game.GetController<CameraController>().SetEnable(enable: true);
		_dragArea.HideInfo();
		TryPurchaseCard();
	}

	private void CheckPurchaseCard()
	{
		SinglePlayer.GamePlay.Card.Card currentCard = _dragArea.CurrentCard;
		if (currentCard == null || currentCard.HasPurchase)
		{
			return;
		}
		for (int i = 0; i < base.ui.list_Bag.numChildren; i++)
		{
			GObject childAt = base.ui.list_Bag.GetChildAt(i);
			if (!(childAt is UISinglePlayer_Com_CardItemSlot))
			{
				return;
			}
			Vector2 touchPosition = Stage.inst.touchPosition;
			Vector2 vector = childAt.LocalToGlobal(Vector2.zero);
			Vector2 vector2 = childAt.LocalToGlobal(new Vector2(childAt.width, childAt.height));
			if (touchPosition.x >= vector.x && touchPosition.x < vector2.x && touchPosition.y >= vector.y && touchPosition.y < vector2.y)
			{
				if (Game.GetSystem<BoardManager>().CheckGold(_dragArea.CurrentCard.Price) && !Game.GetSystem<BoardManager>().cardManager.IsExistInSlot(i))
				{
					_dragArea.HideInfo();
					_selectBagSlotIndex.Value = i;
				}
				return;
			}
		}
		if (_selectBagSlotIndex.Value >= 0 && _selectBagSlotIndex.Value < base.ui.list_Bag.numChildren && base.ui.list_Bag.GetChildAt(_selectBagSlotIndex.Value) is UISinglePlayer_Com_CardItemSlot uISinglePlayer_Com_CardItemSlot)
		{
			uISinglePlayer_Com_CardItemSlot.Clear();
			_selectBagSlotIndex.Value = -1;
		}
	}

	private void TryPurchaseCard()
	{
		if (_selectBagSlotIndex.Value != -1 && !_dragArea.CurrentCard.HasPurchase)
		{
			Game.GetSystem<BoardManager>().cardManager.OnRequestPurchaseCardPutInBag(_dragArea.CurrentCard.UID, _selectBagSlotIndex.Value);
			_selectBagSlotIndex.JustSetValue(-1);
		}
	}

	private void OnBagSlotChange(int previousIndex, int index)
	{
		if (index >= 0 && index < base.ui.list_Bag.numChildren && base.ui.list_Bag.GetChildAt(index) is UISinglePlayer_Com_CardItemSlot uISinglePlayer_Com_CardItemSlot)
		{
			uISinglePlayer_Com_CardItemSlot.RefreshInfo(_dragArea.CurrentCard, CardItemSlotType.Bag);
			uISinglePlayer_Com_CardItemSlot.Cut_in.Play();
			_dragArea.HideInfo();
		}
		if (previousIndex >= 0 && previousIndex < base.ui.list_Bag.numChildren && base.ui.list_Bag.GetChildAt(previousIndex) is UISinglePlayer_Com_CardItemSlot uISinglePlayer_Com_CardItemSlot2)
		{
			uISinglePlayer_Com_CardItemSlot2.Clear();
		}
	}

	private void CheckReturnCardToBag()
	{
		SinglePlayer.GamePlay.Card.Card currentCard = _dragArea.CurrentCard;
		if ((currentCard != null && !currentCard.HasPurchase) || !Game.GetSystem<BoardManager>().buildingManager.TryGetBuildingByCardUid(_dragArea.CurrentCard.UID, out var _))
		{
			return;
		}
		for (int i = 0; i < base.ui.list_Bag.numChildren; i++)
		{
			GObject childAt = base.ui.list_Bag.GetChildAt(i);
			if (!(childAt is UISinglePlayer_Com_CardItemSlot))
			{
				return;
			}
			Vector2 touchPosition = Stage.inst.touchPosition;
			Vector2 vector = childAt.LocalToGlobal(Vector2.zero);
			Vector2 vector2 = childAt.LocalToGlobal(new Vector2(childAt.width, childAt.height));
			if (touchPosition.x >= vector.x && touchPosition.x < vector2.x && touchPosition.y >= vector.y && touchPosition.y < vector2.y)
			{
				if (!Game.GetSystem<BoardManager>().cardManager.IsExistInSlot(i))
				{
					_dragArea.HideInfo();
					_selectBagSlotIndex.Value = i;
				}
				return;
			}
		}
		if (_selectBagSlotIndex.Value >= 0 && _selectBagSlotIndex.Value < base.ui.list_Bag.numChildren && base.ui.list_Bag.GetChildAt(_selectBagSlotIndex.Value) is UISinglePlayer_Com_CardItemSlot uISinglePlayer_Com_CardItemSlot)
		{
			uISinglePlayer_Com_CardItemSlot.Clear();
			_selectBagSlotIndex.Value = -1;
		}
	}

	private void TryReturnCardToBag()
	{
		if (_selectBagSlotIndex.Value != -1 && _dragArea.CurrentCard.HasPurchase)
		{
			Game.GetSystem<BoardManager>().buildingManager.RemoveBuilding(_dragArea.CurrentCard.UID);
			Game.GetSystem<BoardManager>().cardManager.TryAddToBag(_dragArea.CurrentCard.UID, _selectBagSlotIndex.Value);
			_selectBagSlotIndex.JustSetValue(-1);
		}
	}

	public void ShowCardInfo(UISinglePlayer_Com_CardItemSlot slot)
	{
		if (slot.CardInfo == null)
		{
			return;
		}
		Vector2 pt = slot.LocalToGlobal(Vector2.zero);
		Vector2 vector = base.ui.GlobalToLocal(pt);
		_buildingInfo.Refresh(slot.CardInfo);
		_buildingInfo.RefreshDesc(slot.CardInfo);
		Vector2 vector2;
		if (slot.CardInfo.HasPurchase)
		{
			vector2 = vector - new Vector2(_buildingInfo.width + 20f, 0f);
			float num = vector2.y + _buildingInfo.height;
			if (num > GRoot.inst.height)
			{
				float num2 = num - GRoot.inst.height;
				vector2.y -= num2 + 10f;
			}
		}
		else
		{
			vector2 = vector - Vector2.up * _buildingInfo.height;
		}
		GRoot.inst.ShowPopup(_buildingInfo, base.ui);
		_buildingInfo.SetXY(vector2.x, vector2.y);
	}

	private void OnShowBuildingInfo(bool show, BuildingBase buildingBase, Vector3 position)
	{
		if (show)
		{
			_buildingInfo.Refresh(buildingBase.Card);
			_buildingInfo.RefreshDesc(buildingBase);
			Vector2 vector = UIHelper.World2Local(Camera.main, position);
			if (vector.x + _buildingInfo.width > GRoot.inst.width)
			{
				vector.x -= _buildingInfo.width;
			}
			if (vector.y + _buildingInfo.height > GRoot.inst.height)
			{
				vector.y -= _buildingInfo.height;
			}
			GRoot.inst.ShowPopup(_buildingInfo, base.ui);
			_buildingInfo.SetXY(vector.x, vector.y);
		}
	}

	private void OpenDevelopLandOperate(int foundationId)
	{
		BuildingFoundation buildingFoundationById = _gameData.MapData.GetBuildingFoundationById(foundationId);
		if (buildingFoundationById != null && buildingFoundationById.Wasteland)
		{
			BoardFoundationManager foundationManager = Game.GetSystem<BoardManager>().foundationManager;
			string msg = string.Format(1040002.GetLocal(UIStringType.GUI), foundationManager.DevelopLandPrice.ToString());
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(msg, delegate
			{
				foundationManager.TryDevelopLand(foundationId);
				CloseDevelopLandOperate();
			}).Forget();
		}
		else
		{
			CloseDevelopLandOperate();
		}
	}

	private void CloseDevelopLandOperate()
	{
		base.ui.developLand.selectedIndex = 0;
		_playerActionFSM.OnActionFinished();
	}

	private void OnSettleMission()
	{
		(MissionSettleType, List<int>) reward = Game.GetSystem<BoardManager>().missionManager.MissionRewards;
		if (reward.Item1 == MissionSettleType.Card)
		{
			_settleMission.list_Card.itemRenderer = delegate(int index, GObject item)
			{
				UISinglePlayer_SettleMission_Button_Card cardItem = item as UISinglePlayer_SettleMission_Button_Card;
				if (cardItem != null)
				{
					int key = reward.Item2[index];
					if (StaticConfigure.SinglePlayer.CardDict.TryGetValue(key, out var cardConfig))
					{
						cardItem.com_Name.txt_Name.text = cardConfig.NameID.GetLocal(UIStringType.SinglePlayer);
						cardItem.com_Name.type.selectedIndex = cardConfig.GetQualityIndex();
						cardItem.com_Item.loader_Icon.url = cardConfig.Icon;
						cardItem.com_Item.com_Quality.quality.selectedIndex = cardConfig.GetQualityIndex();
						cardItem.list_Tags.itemRenderer = delegate(int i, GObject o)
						{
							RendererTags(i, o, cardConfig);
						};
						cardItem.list_Tags.numItems = cardConfig.CardTag.Count;
						cardItem.txt_Desc.text = ParseSinglePlayerText.ParseAstralCardDesc(cardConfig.Id, cardConfig.SinglePlayerCardConfigureItems[0].Level);
						cardItem.btn_Confirm.onClick.Set((EventCallback0)delegate
						{
							cardItem.onClick.Retain();
							Game.GetSystem<BoardManager>().cardManager.AddCardToBag(cardConfig.Id);
							_playerActionFSM.OnActionFinished();
							base.ui.RemoveChild(_settleMission);
							cardItem.onClick.Release();
						});
					}
				}
			};
			_settleMission.list_Card.numItems = reward.Item2.Count;
		}
		else
		{
			if (reward.Item1 != MissionSettleType.Relic)
			{
				_playerActionFSM.OnActionFinished();
				return;
			}
			_settleMission.list_Card.itemRenderer = delegate(int index, GObject item)
			{
				UISinglePlayer_SettleMission_Button_Card relicItem = item as UISinglePlayer_SettleMission_Button_Card;
				if (relicItem != null)
				{
					int key = reward.Item2[index];
					if (StaticConfigure.SinglePlayer.RelicDict.TryGetValue(key, out var relicConfig))
					{
						relicItem.com_Name.txt_Name.text = relicConfig.NameID.GetLocal(UIStringType.SinglePlayer);
						relicItem.com_Name.type.selectedIndex = relicConfig.GetQualityIndex();
						relicItem.txt_Desc.text = relicConfig.DesiID.GetLocal(UIStringType.SinglePlayer);
						relicItem.com_Item.com_Quality.quality.selectedIndex = relicConfig.GetQualityIndex();
						relicItem.com_Item.loader_Icon.url = relicConfig.Icon;
						relicItem.btn_Confirm.onClick.Set((EventCallback0)delegate
						{
							relicItem.onClick.Retain();
							Game.GetSystem<BoardManager>().relicManager.AddRelic(new RelicInfo(relicConfig.Id));
							_playerActionFSM.OnActionFinished();
							base.ui.RemoveChild(_settleMission);
							relicItem.onClick.Release();
						});
					}
				}
			};
			_settleMission.list_Card.numItems = reward.Item2.Count;
		}
		base.ui.AddChild(_settleMission);
		SimpleSingletonProvider<AudioManager>.inst.SendEvent(10, Stage.inst.gameObject);
		_settleMission.MakeFullScreen();
		_settleMission.position = Vector3.zero;
		if (reward.Item1 == MissionSettleType.Relic && reward.Item2.Count == 0)
		{
			_playerActionFSM.OnActionFinished();
			base.ui.RemoveChild(_settleMission);
		}
	}

	private void RendererTags(int index, GObject gObject, SinglePlayerCardConfigure cardConfigure)
	{
		if (gObject is UISinglePlayer_Com_CardTag2 uISinglePlayer_Com_CardTag)
		{
			SinglePlayerTagType safeByIndex = cardConfigure.CardTag.GetSafeByIndex(index);
			if (safeByIndex >= SinglePlayerTagType.None && (int)safeByIndex < uISinglePlayer_Com_CardTag.type.pageCount)
			{
				uISinglePlayer_Com_CardTag.type.selectedIndex = (int)safeByIndex;
			}
		}
	}

	public SinglePlayerPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UISinglePlayerPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		_gameData = Game.GetModel<GameData>();
		_globalSignal = Game.GetModel<GlobalSignal>();
		_playerActionFSM = Game.GetSystem<PlayerActionFSM>();
		_characterManager = Game.GetSystem<BoardManager>().characterManager;
		_gameManager = Game.GetSystem<BoardManager>().gameManager;
		_buildingAttrInfos = new Dictionary<IUnitView, UISinglePlayer_Com_UnitAttrInfo>();
		_buildingLevelInfo = new Dictionary<int, UISinglePlayer_Com_BuildingLevel>();
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		base.ui.Cut_in.Play();
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		_dragArea = UISinglePlayer_Button_DragArea.Create();
		_buildingInfo = UISinglePlayer_Com_BuildingInfo.CreateInstance();
		_settleMission = UISinglePlayer_Com_SettleMission.CreateInstance();
		_relicInfo = UISinglePlayer_Com_RelicInfo.CreateInstance();
		base.ui.list_Buff.SetVirtual();
	}

	public override void Refresh()
	{
		base.Refresh();
		RefreshCard();
		RefreshDevelopLandTips();
		RefreshGuide();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_Move.onClick.Add(OnMove);
		base.ui.btn_UnLockLand.onClick.Add(OnDevelopLand);
		base.ui.btn_RefreshStore.onClick.Add(OnShopRefresh);
		base.ui.btn_Back.onClick.Add(OnOpenSetting);
		base.ui.btn_guide.onClick.Add(OnOpenGuide);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Move.onClick.Remove(OnMove);
		base.ui.btn_UnLockLand.onClick.Remove(OnDevelopLand);
		base.ui.btn_RefreshStore.onClick.Remove(OnShopRefresh);
		base.ui.btn_Back.onClick.Remove(OnOpenSetting);
		base.ui.btn_guide.onClick.Remove(OnOpenGuide);
	}

	protected override void AddListener()
	{
		base.AddListener();
		_gameData.Round.AddListener(OnRoundChange);
		_gameData.GameProgress.AddListener(OnGameProgressChange);
		_globalSignal.DragStart.AddListener(DragStart);
		_globalSignal.DragMoving.AddListener(DragMoving);
		_globalSignal.DragEnd.AddListener(DragEnd);
		_globalSignal.ShopCardChange.AddListener(RefreshShopCard);
		_globalSignal.BagCardChange.AddListener(RefreshBagCard);
		_characterManager.Hero.Property.Gold.AddListener(OnDispatchGold);
		_globalSignal.CardUsed.AddListener(OnCardUsed);
		_globalSignal.ShowBuildingInfo.AddListener(OnShowBuildingInfo);
		_selectBagSlotIndex.AddListener(OnBagSlotChange);
		_gameData.CardData.FreeRefreshCount.AddListener(OnFreeRefreshCountChange);
		_globalSignal.GetCardPerformance.AddListener(OnPlayCardShow);
		_globalSignal.HeroCreated.AddListener(OnHeroCreated);
		_globalSignal.BuildingViewCreate.AddListener(OnBuildingViewCreated);
		_globalSignal.BuildingRemove.AddListener(OnBuildingRemove);
		_globalSignal.BuildingMoveStart.AddListener(OnBuildingMoveStart);
		_globalSignal.BuildingMoveFailure.AddListener(OnBuildingMoveFailure);
		_globalSignal.CardGainExp.AddListener(OnBuildingExpChanged);
		_globalSignal.CardUpgrade.AddListener(OnBuildingExpChanged);
		_globalSignal.MissionStart.AddListener(OnMissionStart);
		_globalSignal.MissionProgressChange.AddListener(OnMissionProgressChangeStart);
		_globalSignal.MissionStatusChange.AddListener(OnMissionStatusChangeEnd);
		_globalSignal.SettleMission.AddListener(OnSettleMission);
		_gameData.heroProperty.DoubleDiceTime.AddListener(OnDoubleDice);
		_globalSignal.Card.AddListener(OnDispatchUseCard);
		_globalSignal.ThrowDice.AddListener(OnDispatchThrowDice);
		_globalSignal.DevelopLand.AddListener(OnDispatchReclaimLand);
		_globalSignal.SelectDevelopLand.AddListener(OpenDevelopLandOperate);
		_globalSignal.ApplyRelic.AddListener(RefreshBuff);
		_globalSignal.DevelopLandSucceed.AddListener(OnDevelopLandSucceed);
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		_gameData.Round.RemoveListener(OnRoundChange);
		_gameData.GameProgress.RemoveListener(OnGameProgressChange);
		_globalSignal.DragStart.RemoveListener(DragStart);
		_globalSignal.DragMoving.RemoveListener(DragMoving);
		_globalSignal.DragEnd.RemoveListener(DragEnd);
		_globalSignal.ShopCardChange.RemoveListener(RefreshShopCard);
		_globalSignal.BagCardChange.RemoveListener(RefreshBagCard);
		_characterManager.Hero.Property.Gold.RemoveListener(OnDispatchGold);
		_globalSignal.CardUsed.RemoveListener(OnCardUsed);
		_globalSignal.ShowBuildingInfo.RemoveListener(OnShowBuildingInfo);
		_selectBagSlotIndex.RemoveListener(OnBagSlotChange);
		_gameData.CardData.FreeRefreshCount.RemoveListener(OnFreeRefreshCountChange);
		_globalSignal.GetCardPerformance.RemoveListener(OnPlayCardShow);
		_globalSignal.HeroCreated.RemoveListener(OnHeroCreated);
		RemoveHeroListener();
		_globalSignal.BuildingViewCreate.RemoveListener(OnBuildingViewCreated);
		_globalSignal.BuildingRemove.RemoveListener(OnBuildingRemove);
		_globalSignal.BuildingMoveStart.RemoveListener(OnBuildingMoveStart);
		_globalSignal.BuildingMoveFailure.RemoveListener(OnBuildingMoveFailure);
		_globalSignal.CardGainExp.RemoveListener(OnBuildingExpChanged);
		_globalSignal.CardUpgrade.RemoveListener(OnBuildingExpChanged);
		_globalSignal.MissionStart.RemoveListener(OnMissionStart);
		_globalSignal.MissionProgressChange.RemoveListener(OnMissionProgressChangeStart);
		_globalSignal.MissionStatusChange.RemoveListener(OnMissionStatusChangeEnd);
		_globalSignal.SettleMission.RemoveListener(OnSettleMission);
		_gameData.heroProperty.DoubleDiceTime.RemoveListener(OnDoubleDice);
		_globalSignal.Card.RemoveListener(OnDispatchUseCard);
		_globalSignal.ThrowDice.RemoveListener(OnDispatchThrowDice);
		_globalSignal.DevelopLand.RemoveListener(OnDispatchReclaimLand);
		_globalSignal.SelectDevelopLand.RemoveListener(OpenDevelopLandOperate);
		_globalSignal.ApplyRelic.RemoveListener(RefreshBuff);
		_globalSignal.DevelopLandSucceed.RemoveListener(OnDevelopLandSucceed);
	}

	public override void Close()
	{
		base.Close();
		ClearAttrInfos();
	}

	public override void Dispose()
	{
		_dragArea?.Dispose();
		_buildingInfo?.Dispose();
		_settleMission?.Dispose();
		base.Dispose();
	}

	private void OnOpenSetting()
	{
		base.ui.btn_Back.onClick.Retain();
		SimpleSingletonProvider<UIManager>.inst.SinglePlayerSettingInBattle.TryShowAsync().Forget();
		base.ui.btn_Back.onClick.Release();
	}

	private void OnRoundChange(int round)
	{
	}

	private void OnPlayCardShow(int cardUid)
	{
		PlayRewardShow(cardUid);
	}

	private void PlayRewardShow(int cardUid)
	{
		base.ui.com_RewardShow.visible = true;
		SinglePlayer.GamePlay.Card.Card cardByUID = Game.GetSystem<BoardManager>().cardManager.GetCardByUID(cardUid);
		if (cardByUID != null)
		{
			bool num = cardByUID.CardConfigure.Rarity == 4;
			base.ui.com_RewardShow.com_card.Refresh(cardByUID, CardItemSlotType.None);
			int id = (num ? 1001 : 1000);
			base.ui.com_RewardShow.txt_Title.text = id.GetLocal(UIStringType.SinglePlayer);
			base.ui.com_RewardShow.com_card.status.selectedIndex = 1;
			base.ui.com_RewardShow.com_card.type.selectedIndex = 1;
			base.ui.com_RewardShow.com_card.touchable = false;
			base.ui.com_RewardShow.Cut_in.Play();
			base.ui.com_RewardShow.btn_Confirm.onClick.Set((EventCallback0)delegate
			{
				base.ui.com_RewardShow.visible = false;
			});
		}
	}

	private void SetOperateButtonState(bool status)
	{
		base.ui.btn_Move.touchable = status;
		base.ui.btn_Move.grayed = !status;
	}

	public void OnGameStart()
	{
		OnHeroCreated(Game.GetSystem<BoardManager>().characterManager.Hero);
		foreach (BuildingBase buildingDatum in Game.GetModel<GameData>().BuildingData)
		{
			if (Game.GetController<BuildingController>().TryGetBuildingView(buildingDatum.Id, out var buildingView))
			{
				OnBuildingViewCreated(buildingView);
			}
		}
	}

	private void RefreshCard()
	{
		RefreshShopCard();
		RefreshShopCardProbability();
		RefreshBagCard();
	}

	private void RefreshShopCard()
	{
		List<int> shopCards = _gameData.CardData.ShopCards;
		base.ui.list_Shop.itemRenderer = delegate(int index, GObject item)
		{
			if (item is UISinglePlayer_Com_CardItemSlot uISinglePlayer_Com_CardItemSlot)
			{
				SinglePlayer.GamePlay.Card.Card card = null;
				if (shopCards.Count > index && shopCards[index] > 0)
				{
					card = Game.GetSystem<BoardManager>().cardManager.GetCardByUID(shopCards[index]);
				}
				uISinglePlayer_Com_CardItemSlot.Refresh(card, CardItemSlotType.Shop);
			}
		};
		base.ui.list_Shop.numItems = _gameData.CardData.ShopSellCount;
		base.ui.btn_RefreshStore.txt_Gold.text = _gameData.CardData.RefreshPrice.ToString();
	}

	private void RefreshShopCardProbability()
	{
		BoardCardManager cardManager = Game.GetSystem<BoardManager>().cardManager;
		base.ui.Com_CardsProb.txt_Green.SetVar("value", cardManager.GetTheCardSpawnProbability(CardRarity.GREEN).ToString()).FlushVars();
		base.ui.Com_CardsProb.txt_Bule.SetVar("value", cardManager.GetTheCardSpawnProbability(CardRarity.BLUE).ToString()).FlushVars();
		base.ui.Com_CardsProb.txt_Purple.SetVar("value", cardManager.GetTheCardSpawnProbability(CardRarity.PURPLE).ToString()).FlushVars();
		base.ui.Com_CardsProb.txt_Gold.SetVar("value", cardManager.GetTheCardSpawnProbability(CardRarity.GOLDEN).ToString()).FlushVars();
	}

	private void SwitchShopShow(bool status)
	{
		int index;
		if (_playerActionFSM.CurrentState == PlayerActionType.Idle)
		{
			RefreshShopCardProbability();
			index = ((_gameData.MapData.GetLandTypeById(_gameData.heroProperty.StandLandId) != SinglePlayerLandType.Start) ? 1 : 0);
		}
		else
		{
			index = 1;
		}
		ChangeShowShopControllerIndex(index);
	}

	private void ChangeShowShopControllerIndex(int index)
	{
		if (targetIndex == index)
		{
			return;
		}
		targetIndex = index;
		int num = ((index != 0) ? 1 : 0);
		int num2 = ((index == 0) ? 1 : 0);
		GGroup group = base.ui.list_Shop.group;
		group.touchable = false;
		group.alpha = num;
		if (shopFadeTweener != null && !shopFadeTweener._killed)
		{
			shopFadeTweener.Kill(complete: true);
			shopFadeTweener = null;
		}
		if (index == 0)
		{
			base.ui.showShop.selectedIndex = index;
			base.ui.Com_CardsProb.Cut_in.Play();
		}
		shopFadeTweener = GTween.To(num, num2, fadeTime).SetTarget(base.ui.list_Shop.group, TweenPropType.Alpha).OnComplete((GTweenCallback)delegate
		{
			if (index != 0)
			{
				base.ui.showShop.selectedIndex = index;
			}
			group.touchable = true;
		});
	}

	private void RefreshBagCard()
	{
		List<int> bagCards = _gameData.CardData.BagCards;
		base.ui.list_Bag.itemRenderer = delegate(int index, GObject item)
		{
			if (item is UISinglePlayer_Com_CardItemSlot uISinglePlayer_Com_CardItemSlot)
			{
				SinglePlayer.GamePlay.Card.Card card = null;
				uISinglePlayer_Com_CardItemSlot.type.selectedIndex = 1;
				if (bagCards.Count > index && bagCards[index] > 0)
				{
					card = Game.GetSystem<BoardManager>().cardManager.GetCardByUID(bagCards[index]);
				}
				uISinglePlayer_Com_CardItemSlot.Refresh(card, CardItemSlotType.Bag);
			}
		};
		base.ui.list_Bag.numItems = bagCards.Count;
	}

	private void OnDispatchUseCard(bool status)
	{
		base.ui.list_Bag.touchable = status;
		base.ui.list_Shop.touchable = status;
		SetOperateButtonState(status);
		SwitchShopShow(status);
	}

	private void OnShopRefresh()
	{
		base.ui.btn_RefreshStore.onClick.Retain();
		Game.GetSystem<BoardManager>().cardManager.ManualUpdateCardShop();
		base.ui.btn_RefreshStore.onClick.Release();
	}

	private void OnCardUsed(int cardUid, bool result)
	{
		RefreshCard();
	}

	private void OnFreeRefreshCountChange(int count)
	{
		base.ui.btn_RefreshStore.free.selectedIndex = ((count > 0) ? 1 : 0);
	}

	private void OnDispatchThrowDice(bool status)
	{
		SetOperateButtonState(status);
	}

	private void OnMove(EventContext context)
	{
		if (_playerActionFSM.CurrentState == PlayerActionType.Idle)
		{
			base.ui.btn_Move.onClick.Retain();
			_playerActionFSM.SwitchState(PlayerActionType.ThrowDice).Forget();
			base.ui.btn_Move.onClick.Release();
		}
	}

	public async UniTask PlayDiceEffect()
	{
		IReadOnlyList<int> dicePoints = Game.GetSystem<BoardManager>().gameManager.DicePoints;
		if (dicePoints.Count != 0)
		{
			base.ui.btn_Move.grayed = false;
			base.ui.btn_Move.ShowDice.selectedIndex = 1;
			if (dicePoints.Count == 1)
			{
				base.ui.btn_Move.com_Dice.diceCount.selectedIndex = 0;
				ShowDicePoint(base.ui.btn_Move.com_Dice.com_Point, dicePoints[0]);
			}
			else if (dicePoints.Count == 2)
			{
				base.ui.btn_Move.com_Dice.diceCount.selectedIndex = 1;
				ShowDicePoint(base.ui.btn_Move.com_Dice.com_Point_1, dicePoints[0]);
				ShowDicePoint(base.ui.btn_Move.com_Dice.com_Point_2, dicePoints[1]);
			}
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1500);
			base.ui.btn_Move.grayed = true;
			base.ui.btn_Move.ShowDice.selectedIndex = 0;
		}
	}

	private void ShowDicePoint(UISinglePlayer_Com_Point com_Point, int movePoint)
	{
		com_Point.txt_Point.text = movePoint.ToString();
		com_Point.aMovie_Dice.playing = true;
		if (movePoint < 6)
		{
			com_Point.Roll_1_5.Play(1, 0f, 0f, 1f, null);
		}
		else
		{
			com_Point.Roll_6.Play(1, 0f, 0f, 1f, null);
		}
	}

	private void OnDispatchGold(int value)
	{
		base.ui.txt_Gold.text = value.ToString();
		base.ui.txt_StoreGold.text = value.ToString();
		base.ui.btn_RefreshStore.touchable = Game.GetSystem<BoardManager>().cardManager.CanRefresh();
		base.ui.btn_RefreshStore.grayed = !Game.GetSystem<BoardManager>().cardManager.CanRefresh();
		RefreshRemainingRoundTipColor();
		RefreshShopCard();
		RefreshUnLockLand();
	}

	private void OnDoubleDice(int time)
	{
		base.ui.btn_Move.diceCount.selectedIndex = ((time > 0) ? 1 : 0);
	}

	private void OnHeroCreated(Hero hero)
	{
		UISinglePlayer_Com_UnitAttrInfo attInfo = UISinglePlayer_Com_UnitAttrInfo.CreateInstance();
		attInfo.Init(hero.view);
		RegisterAttrInfo(attInfo);
		Action<int> action = delegate(int value)
		{
			if (value < 0)
			{
				attInfo.ShowAttrInfo(PropertyType.Hp, value);
			}
		};
		Action<int> action2 = delegate(int value)
		{
			attInfo.ShowAttrInfo(PropertyType.Gold, value);
			Stage.inst.PlayOneShotSound(207);
		};
		hero.HpChange.AddListener(action);
		hero.GoldChange.AddListener(action2);
		heroListeners.Add(new HeroListener
		{
			Hero = hero,
			OnHpChange = action,
			OnGoldChange = action2
		});
	}

	private void RemoveHeroListener()
	{
		foreach (HeroListener heroListener in heroListeners)
		{
			heroListener.Hero.HpChange.RemoveListener(heroListener.OnHpChange);
			heroListener.Hero.GoldChange.RemoveListener(heroListener.OnGoldChange);
		}
		heroListeners.Clear();
	}

	private void OnBuildingViewCreated(BuildingView buildingView)
	{
		UISinglePlayer_Com_UnitAttrInfo uISinglePlayer_Com_UnitAttrInfo = UISinglePlayer_Com_UnitAttrInfo.CreateInstance();
		uISinglePlayer_Com_UnitAttrInfo.Init(buildingView);
		RegisterAttrInfo(uISinglePlayer_Com_UnitAttrInfo);
		AddBuildingLevelInfo(buildingView);
		Game.GetModel<GlobalSignal>().BuildingAttributeChangeShow.AddListener(OnBuildingAttributeShow);
		_buildingAttrInfos.Add(buildingView, uISinglePlayer_Com_UnitAttrInfo);
	}

	private void OnBuildingRemove(int arg1, int cardConfigureId, int buildingId)
	{
		if (Game.GetController<BuildingController>().TryGetBuildingView(buildingId, out var buildingView))
		{
			_buildingAttrInfos.Remove(buildingView);
			RemoveBuildingLevelInfo(buildingId);
		}
	}

	private void OnBuildingAttributeShow(IUnitView view, AttributeChangeInfo attributeChangeInfo)
	{
		if (_buildingAttrInfos.TryGetValue(view, out var value))
		{
			value.ShowAttrInfo(attributeChangeInfo);
		}
	}

	private void OnBuildingMoveStart(int foundationId, int buildingId, int cardConfigureId)
	{
		if (_buildingLevelInfo.TryGetValue(buildingId, out var value))
		{
			value.Hide();
		}
	}

	private void OnBuildingMoveFailure(int foundationId, int buildingId, int cardUid)
	{
		if (_buildingLevelInfo.TryGetValue(buildingId, out var value))
		{
			value.Show();
		}
	}

	private void OnBuildingExpChanged(int cardUid)
	{
		if (Game.GetSystem<BoardManager>().buildingManager.TryGetBuildingByCardUid(cardUid, out var building) && _buildingLevelInfo.TryGetValue(building.Id, out var value))
		{
			value.Refresh(building.Card);
		}
	}

	private void AddBuildingLevelInfo(BuildingView buildingView)
	{
		UISinglePlayer_Com_BuildingLevel uISinglePlayer_Com_BuildingLevel = UISinglePlayer_Com_BuildingLevel.CreateInstance();
		uISinglePlayer_Com_BuildingLevel.Refresh(buildingView);
		base.ui.com_AttrParent.AddChild(uISinglePlayer_Com_BuildingLevel);
		_buildingLevelInfo.Add(buildingView.BuildingBase.Id, uISinglePlayer_Com_BuildingLevel);
	}

	private void RemoveBuildingLevelInfo(int buildingId)
	{
		if (_buildingLevelInfo.Remove(buildingId, out var value))
		{
			value.Dispose();
		}
	}

	private void RegisterAttrInfo(UISinglePlayer_Com_UnitAttrInfo Com_PlayerAttrInfo)
	{
		base.ui.com_AttrParent.AddChild(Com_PlayerAttrInfo);
		base.ui.InvalidateBatchingState();
	}

	private void ClearAttrInfos()
	{
		foreach (UISinglePlayer_Com_UnitAttrInfo value in _buildingAttrInfos.Values)
		{
			value.Dispose();
		}
		_buildingAttrInfos.Clear();
		base.ui.com_AttrParent.Dispose();
	}

	private void OnGameProgressChange(int progress)
	{
		base.ui.btn_Move.txt_Progress.SetVar("current", progress.ToString()).FlushVars();
		base.ui.btn_Move.txt_Progress.SetVar("max", _gameData.MapData.MaxProgress.ToString()).FlushVars();
		MapMission currentMissionData = Game.GetSystem<BoardManager>().missionManager.GetCurrentMissionData();
		if (currentMissionData != null)
		{
			int remainRound = currentMissionData.Deadline - progress;
			RefreshRemainingRoundTip(remainRound);
			base.ui.slider_Progress.txt_MissionProgress.SetVar("cur", (currentMissionData.MissionConfig.MissionIndex + 1).ToString()).FlushVars();
			base.ui.slider_Progress.txt_MissionProgress.SetVar("max", _gameData.MapData.MissionCount.ToString()).FlushVars();
		}
	}

	private void OnMissionStart()
	{
		MapMission currentMissionData = Game.GetSystem<BoardManager>().missionManager.GetCurrentMissionData();
		if (currentMissionData != null)
		{
			int value = Game.GetModel<GameData>().GameProgress.Value;
			int remainRound = currentMissionData.Deadline - value;
			RefreshRemainingRoundTip(remainRound);
			OnMissionProgressChangeStart(currentMissionData.MissionProgress, currentMissionData.MissionTarget);
		}
	}

	private void RefreshRemainingRoundTip(int remainRound)
	{
		base.ui.RemainingDice.selectedIndex = ((remainRound <= 2) ? 1 : 0);
		base.ui.txt_RemainingRounds.SetVar("round", remainRound.ToString()).FlushVars();
		RefreshRemainingRoundTipColor();
	}

	private void OnMissionProgressChangeStart(int progress, int target)
	{
		base.ui.slider_Progress.min = 0.0;
		base.ui.slider_Progress.max = target;
		base.ui.slider_Progress.value = progress;
	}

	private void OnMissionStatusChangeEnd(int status)
	{
	}

	private void RefreshRemainingRoundTipColor()
	{
		MapMission currentMissionData = Game.GetSystem<BoardManager>().missionManager.GetCurrentMissionData();
		if (currentMissionData != null && currentMissionData.MissionProgress >= currentMissionData.MissionTarget)
		{
			base.ui.RemainingDice.selectedIndex = 0;
		}
	}

	private void OnDispatchReclaimLand(bool status)
	{
		_reclaimLandState = status;
		int developLandPrice = Game.GetSystem<BoardManager>().foundationManager.DevelopLandPrice;
		if (status)
		{
			RefreshUnLockLand();
		}
		else
		{
			base.ui.btn_UnLockLand.touchable = false;
			base.ui.btn_UnLockLand.grayed = true;
		}
		base.ui.btn_UnLockLand.txt_UnlockLand.text = developLandPrice.ToString();
	}

	private void OnDevelopLand()
	{
		if (_playerActionFSM.CurrentState == PlayerActionType.Idle)
		{
			base.ui.btn_UnLockLand.onClick.Retain();
			_playerActionFSM.SwitchState(PlayerActionType.DevelopLand).Forget();
			base.ui.developLand.selectedIndex = 1;
			base.ui.Tips_1.Play();
			base.ui.btn_UnLockLand.onClick.Release();
		}
	}

	private void RefreshDevelopLandTips()
	{
		base.ui.txt_DevelopTip.text = 1040001.GetLocal(UIStringType.GUI);
		if (Game.GetModel<GameData>().MapData.GetWastelandCount() == 0)
		{
			base.ui.btn_UnLockLand.visible = false;
		}
	}

	private void OnDevelopLandSucceed(int remaining)
	{
		if (remaining == 0)
		{
			base.ui.btn_UnLockLand.visible = false;
		}
	}

	private void RefreshUnLockLand()
	{
		if (_reclaimLandState)
		{
			int developLandPrice = Game.GetSystem<BoardManager>().foundationManager.DevelopLandPrice;
			bool flag = Game.GetSystem<BoardManager>().CheckGold(developLandPrice, showMsg: false);
			base.ui.btn_UnLockLand.touchable = flag;
			base.ui.btn_UnLockLand.grayed = !flag;
		}
	}

	private void RefreshBuff()
	{
		List<RelicInfo> relicList = Game.GetModel<GameData>().heroProperty.RelicList;
		base.ui.list_Buff.itemRenderer = delegate(int index, GObject item)
		{
			UISinglePlayer_SettleMission_Button_RelicBuffItem buffItem = item as UISinglePlayer_SettleMission_Button_RelicBuffItem;
			if (buffItem != null)
			{
				if (index >= relicList.Count)
				{
					buffItem.state.selectedIndex = 0;
				}
				else
				{
					buffItem.state.selectedIndex = 1;
					buffItem.loader_Icon.url = relicList[index].BuffData.Config.Icon;
					if (relicList[index].BuffData.ChargeCount > 0)
					{
						buffItem.txt_ChargeCount.text = relicList[index].BuffData.ChargeCount.ToString();
						buffItem.txt_ChargeCount.visible = true;
					}
					else
					{
						buffItem.txt_ChargeCount.visible = false;
					}
					buffItem.onClick.Set((EventCallback0)delegate
					{
						OnOpenRelicInfo(relicList[index], buffItem);
					});
				}
			}
		};
		int numItems = ((relicList.Count < 7) ? 7 : relicList.Count);
		base.ui.list_Buff.numItems = numItems;
	}

	private void OnOpenRelicInfo(RelicInfo relicInfo, UISinglePlayer_SettleMission_Button_RelicBuffItem buffItem)
	{
		if (relicInfo != null)
		{
			_relicInfo.txt_Name.text = relicInfo.BuffData.Config.NameID.GetLocal(UIStringType.SinglePlayer);
			_relicInfo.txt_Desc.text = relicInfo.BuffData.Config.DesiID.GetLocal(UIStringType.SinglePlayer);
			Vector2 pt = buffItem.LocalToGlobal(Vector2.zero);
			Vector2 vector = GRoot.inst.GlobalToLocal(pt);
			vector.x += buffItem.width;
			GRoot.inst.ShowPopup(_relicInfo, base.ui);
			_relicInfo.SetXY(vector.x, vector.y);
		}
	}

	private void RefreshGuide()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.guide.GuideStatus(GuideType.SinglePlayerTutorial))
		{
			SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateGuidedData(2001, 1);
			OnOpenGuide();
		}
	}

	private async void OnOpenGuide()
	{
		base.ui.btn_guide.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.explain.ShowTutorial(1003);
		base.ui.btn_guide.onClick.Release();
	}
}
