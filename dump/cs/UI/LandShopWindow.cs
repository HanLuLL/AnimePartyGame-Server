using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;
using party.protocol;

namespace UI;

public class LandShopWindow : BaseWindow
{
	private BattlePlayerData shoppingPlayer;

	private List<(int, int)> CardGoods = new List<(int, int)>(3);

	private RepeatedField<bool> alreadyCards;

	private readonly List<int> selectedCards = new List<int>();

	private readonly List<UILandShop_Button_Card> cardItems = new List<UILandShop_Button_Card>();

	private int _freeCardPrice;

	private Action CardShopAction => SimpleSingletonProvider<GameLogicManager>.inst.land.CardShopAction;

	private PVEShopBuyC2S PVEShopData => SimpleSingletonProvider<GameLogicManager>.inst.land.PVEShopData;

	private int MaxGold
	{
		get
		{
			if (shoppingPlayer == null)
			{
				return 0;
			}
			return shoppingPlayer.Property.gold.Value;
		}
	}

	private int _needCostGoldCount => selectedCards.Where((int index) => index >= 0 && index < CardGoods.Count).Sum((int index) => CardGoods[index].Item2);

	public LandShopWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UILandShopWindow.CreateInstance();
		base.OnInit();
	}

	public async UniTask TryShow()
	{
		if (!base.isShowing)
		{
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	private void OnHideControllerChange()
	{
		if (base.contentPane is UILandShopWindow uILandShopWindow)
		{
			bool flag = uILandShopWindow.Hide.selectedIndex == 0;
			base.BgLoader.visible = flag;
			if (flag)
			{
				selectedCards.Clear();
				cardItems.Clear();
				uILandShopWindow.list_Card.numItems = CardGoods.Count;
				RefreshShopCostGold(0);
			}
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (!(base.contentPane is UILandShopWindow uILandShopWindow))
		{
			return;
		}
		uILandShopWindow.txt_HideTip.text = 11023.GetLocal(UIStringType.Message);
		uILandShopWindow.Hide.selectedIndex = 0;
		uILandShopWindow.btn_Pay.onClick.Add(OnRequestPayCoin_Shop);
		uILandShopWindow.btn_Leave.onClick.Add(OnRequestLeaveShop);
		uILandShopWindow.btn_Transfer.onClick.Add(TryOpenATM);
		shoppingPlayer?.Property.gold.AddListener(OnMyGoldChanged);
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		if (roomInfo != null)
		{
			int mapType = roomInfo.MapType;
			if (mapType != 6)
			{
				if (mapType == 10)
				{
					goto IL_00eb;
				}
			}
			else if (roomInfo.MapId == 101)
			{
				goto IL_00eb;
			}
		}
		bool flag = false;
		goto IL_00f1;
		IL_00f1:
		if (flag)
		{
			uILandShopWindow.btn_Transfer.visible = false;
			GObject child = uILandShopWindow.GetChild("n30");
			if (child != null)
			{
				child.visible = false;
			}
		}
		uILandShopWindow.Hide.onChanged.Add(OnHideControllerChange);
		return;
		IL_00eb:
		flag = true;
		goto IL_00f1;
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UILandShopWindow uILandShopWindow)
		{
			SimpleSingletonProvider<GameObjectManager>.inst.Stop(uILandShopWindow.effect);
			for (int i = 0; i < cardItems.Count; i++)
			{
				SimpleSingletonProvider<GameObjectManager>.inst.Stop(cardItems[i].downEffect);
			}
			uILandShopWindow.list_Card.numItems = 0;
			uILandShopWindow.IsShopEvent.selectedIndex = 0;
			uILandShopWindow.btn_Pay.onClick.Remove(OnRequestPayCoin_Shop);
			uILandShopWindow.btn_Leave.onClick.Remove(OnRequestLeaveShop);
			uILandShopWindow.btn_Transfer.onClick.Remove(TryOpenATM);
			shoppingPlayer.Property.gold.RemoveListener(OnMyGoldChanged);
			uILandShopWindow.Hide.onChanged.Remove(OnHideControllerChange);
		}
	}

	public async void OpenPVPCardShop(ShopBuyC2S _PVPShopData)
	{
		shoppingPlayer = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(CardShopAction.PlayerId);
		CardGoods.Clear();
		foreach (int card in _PVPShopData.Cards)
		{
			CardGoods.Add((card, _PVPShopData.Gold));
		}
		alreadyCards = _PVPShopData.Alreadys;
		await ShowCardShop(0);
	}

	public async UniTask OpenPVECardShop()
	{
		shoppingPlayer = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(CardShopAction.PlayerId);
		UpdatePVECardShopData();
		await ShowCardShop(1);
	}

	public void UpdatePVECardShopData()
	{
		if (PVEShopData == null)
		{
			return;
		}
		CardGoods.Clear();
		if (StaticConfigure.Skill.InfoDict.TryGetValue(10112, out var value))
		{
			RepeatedField<int> repeatedField = value.Params;
			_freeCardPrice = repeatedField[repeatedField.Count - 1];
		}
		else
		{
			_freeCardPrice = 0;
		}
		RepeatedField<bool> talentSkillFreeCard = PVEShopData.TalentSkillFreeCard;
		for (int i = 0; i < PVEShopData.Cards.Count; i++)
		{
			int item = PVEShopData.Cards[i];
			if (talentSkillFreeCard.Count > i && talentSkillFreeCard.GetSafeByIndex(i))
			{
				CardGoods.Add((item, Mathf.Max(0, _freeCardPrice - PVEShopData.DisCountGold)));
			}
			else
			{
				CardGoods.Add((item, PVEShopData.Gold - PVEShopData.DisCountGold));
			}
		}
		alreadyCards = PVEShopData.Alreadys;
	}

	public async UniTask ShowCardShop(int type)
	{
		await TryShow();
		GComponent gComponent = base.contentPane;
		if (gComponent is UILandShopWindow win)
		{
			win.type.selectedIndex = type;
			selectedCards.Clear();
			cardItems.Clear();
			bool flag = shoppingPlayer.buffContainer.Contain(3001201);
			win.IsShopEvent.selectedIndex = (flag ? 1 : 0);
			if (flag && StaticConfigure.Effect.InfoDict.TryGetValue(8000700, out var value))
			{
				await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(value.EffectName, win.effect, 100f);
			}
			win.list_Card.itemRenderer = RendererShop;
			win.list_Card.numItems = CardGoods.Count;
			RefreshShopCostGold(0);
			win.btn_Pay.onClick.Release();
			win.btn_Leave.onClick.Release();
		}
	}

	private async void RendererShop(int index, GObject item)
	{
		if (item is UILandShop_Button_Card btn)
		{
			btn.isSold.selectedIndex = ((alreadyCards != null && alreadyCards.Count > index && alreadyCards[index]) ? 1 : 0);
			btn.touchable = alreadyCards == null || alreadyCards.Count <= index || !alreadyCards[index];
			btn.grayed = !btn.touchable;
			btn.selected = false;
			RefreshCardBtn(btn, CardGoods[index]);
			btn.data = index;
			btn.onTouchBegin.Set(ChooseCard);
			cardItems.Add(btn);
			if (StaticConfigure.Effect.InfoDict.TryGetValue(8000701, out var value))
			{
				await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(value.EffectName, btn.downEffect, 80f);
			}
			btn.downEffect.visible = false;
		}
	}

	private void RefreshCardBtn(UILandShop_Button_Card btn, (int, int) cardData)
	{
		CardInfoConfigure cardConfigure = cardData.Item1.GetCardConfigure();
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (SimpleSingletonProvider<GameLogicManager>.inst.card.cardActions.TryGetValue(cardConfigure.Id, out var value))
		{
			string content = value.CardDescription(selfPlayerData.player.Id, replaceDamage: true);
			string local = cardConfigure.NameID.GetLocal(UIStringType.Card);
			string cardIndex = ((cardConfigure.CardNumb == 0) ? "" : cardConfigure.CardNumb.GetLocal(UIStringType.Card));
			string cardTips = ((cardConfigure.CommentId == 0) ? "" : cardConfigure.CommentId.GetLocal(UIStringType.Card));
			int costValue = value.GetCostValue(selfPlayerData.player.Id);
			CommonUIManager.RendererCardInRoom(selfPlayerData.player.Id, btn.com_Card as UICom_Card, cardConfigure.GetBattlePlayerCardView(selfPlayerData.player.Id), local, content, cardIndex, cardTips, costValue, cardConfigure.CardType, cardConfigure.CardTargetType);
		}
		else
		{
			Debug.LogError($"cardActions TryGet Action is Failure by CardIs:{cardConfigure.Id}");
		}
		btn.isFree.selectedIndex = ((cardData.Item2 <= _freeCardPrice) ? 1 : 0);
	}

	private void ChooseCard(EventContext context)
	{
		if (context.sender is UILandShop_Button_Card uILandShop_Button_Card)
		{
			uILandShop_Button_Card.onTouchBegin.Retain();
			int item = (int)uILandShop_Button_Card.data;
			if (selectedCards.Contains(item))
			{
				uILandShop_Button_Card.downEffect.visible = false;
				selectedCards.Remove(item);
			}
			else
			{
				uILandShop_Button_Card.downEffect.visible = true;
				selectedCards.Add(item);
			}
			RefreshShopCostGold(_needCostGoldCount);
			uILandShop_Button_Card.onTouchBegin.Release();
		}
	}

	private void OnMyGoldChanged(int newGold)
	{
		if (base.contentPane is UILandShopWindow uILandShopWindow)
		{
			uILandShopWindow.myGold.text = newGold.ToString();
		}
	}

	private void RefreshShopCostGold(int _cost)
	{
		if (base.contentPane is UILandShopWindow uILandShopWindow)
		{
			uILandShopWindow.btn_Pay.txt_Cost.SetVar("gold", _cost.ToString()).FlushVars();
		}
	}

	private void OnRequestLeaveShop()
	{
		selectedCards.Clear();
		RequestShopBuy(isClose: true);
	}

	private void OnRequestPayCoin_Shop()
	{
		if (_needCostGoldCount > MaxGold)
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(10013);
		}
		else if (selectedCards.Count == 0)
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(10014);
		}
		else
		{
			RequestShopBuy(isClose: false);
		}
	}

	private void RequestShopBuy(bool isClose)
	{
		if (CardShopAction == null || CardShopAction.Sn == 0L || !(base.contentPane is UILandShopWindow uILandShopWindow))
		{
			return;
		}
		uILandShopWindow.btn_Pay.onClick.Retain();
		uILandShopWindow.btn_Leave.onClick.Retain();
		if (uILandShopWindow.type.selectedIndex == 0)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.land.RequestShopBuyC2S(CardShopAction.Sn, selectedCards);
		}
		else if (uILandShopWindow.type.selectedIndex == 1)
		{
			RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
			if (roomInfo != null && roomInfo.MapType == 10)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.tutorial.RequestPVEShopBuyC2S(CardShopAction.Sn, selectedCards, 0L, isClose);
			}
			else
			{
				SimpleSingletonProvider<GameLogicManager>.inst.land.RequestPVEShopBuyC2S(CardShopAction.Sn, selectedCards, 0L, isClose);
			}
		}
	}

	public void FinishShopBuy(long playerId, RepeatedField<int> boughtCardIds)
	{
		foreach (UILandShop_Button_Card cardItem in cardItems)
		{
			if (cardItem.isSold.selectedIndex != 1)
			{
				cardItem.isSold.selectedIndex = (boughtCardIds.Contains((int)cardItem.data) ? 1 : 0);
				cardItem.selected = false;
				cardItem.grayed = cardItem.isSold.selectedIndex == 1;
			}
		}
		RefreshShopCostGold(0);
	}

	private async void TryOpenATM()
	{
		GComponent gComponent = base.contentPane;
		if (gComponent is UILandShopWindow win && win.type.selectedIndex != 0 && MaxGold >= PVEShopData.AssistGold && PVEShopData.AssistPlayer == 0L)
		{
			win.btn_Transfer.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.atm.ShowATM();
			Hide();
			win.btn_Transfer.onClick.Release();
		}
	}
}
