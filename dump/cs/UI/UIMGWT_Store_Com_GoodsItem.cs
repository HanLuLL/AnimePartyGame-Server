using System;
using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class UIMGWT_Store_Com_GoodsItem : GComponent
{
	private CollaborationGoodsConfigure CollaborationGoods;

	private readonly List<int> LabelIds = new List<int>();

	private float autoScrollTime;

	private int _displayCount;

	private bool startScrollStatus;

	private int bg_type = 1;

	private Action<float, float, int> showPopup;

	private List<(int, int)> _items = new List<(int, int)>();

	public Controller BGType;

	public Controller type;

	public Controller open;

	public Controller herostate;

	public Controller moreAward;

	public GList list_Labels;

	public GTextField txt_LabelTitle;

	public GList list_Page;

	public GList Item_List;

	public UIMGWT_Com_HeroItem heroItem;

	public UIMGWT_Com_HeroItem heroItem1;

	public UIMGWT_Com_HeroItem heroItem2;

	public UIMGWT_Store_Buy_Button btn_Purchase;

	public GButton btn_Expand;

	public GButton btn_MoreAward;

	public const string URL = "ui://2p754tqkilj51";

	public void RenderGoodsItem(int GoodsId)
	{
		CollaborationGoodsConfigure collaborationGoods = GoodsId.GetCollaborationGoodsConfigure();
		if (collaborationGoods == null)
		{
			return;
		}
		open.selectedIndex = 0;
		btn_Expand.selected = false;
		if (collaborationGoods.HeroID.Count == 1)
		{
			type.selectedIndex = 0;
			SkinStandingPaintingConfigureItem configStandingPainting = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(collaborationGoods.HeroID[0], 0, 0);
			type.selectedIndex = 0;
			RefreshHeroAnimation(configStandingPainting, heroItem.graph_Skin);
			heroItem.onClick.Set((EventCallback0)delegate
			{
				SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(collaborationGoods.HeroID[0], 1, _Usable: false).Forget();
			});
			heroItem.txt_title.text = collaborationGoods.HeroName.GetLocal(UIStringType.Collaboration);
		}
		else if (collaborationGoods.HeroID.Count > 1)
		{
			type.selectedIndex = 1;
			SkinStandingPaintingConfigureItem configStandingPainting2 = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(collaborationGoods.HeroID[0], 0, 0);
			RefreshHeroAnimation(configStandingPainting2, heroItem1.graph_Skin);
			if (collaborationGoods.MutiHeroName != null && collaborationGoods.MutiHeroName.Count >= 1)
			{
				heroItem1.txt_title.text = collaborationGoods.MutiHeroName[0].GetLocal(UIStringType.Collaboration);
			}
			SkinStandingPaintingConfigureItem configStandingPainting3 = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(collaborationGoods.HeroID[1], 0, 0);
			RefreshHeroAnimation(configStandingPainting3, heroItem2.graph_Skin);
			if (collaborationGoods.MutiHeroName != null && collaborationGoods.MutiHeroName.Count >= 2)
			{
				heroItem2.txt_title.text = collaborationGoods.MutiHeroName[1].GetLocal(UIStringType.Collaboration);
			}
			heroItem1.onClick.Set((EventCallback0)delegate
			{
				SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(collaborationGoods.HeroID[0], 1, _Usable: false).Forget();
			});
			heroItem2.onClick.Set((EventCallback0)delegate
			{
				SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(collaborationGoods.HeroID[1], 1, _Usable: false).Forget();
			});
		}
		CollaborationGoods = collaborationGoods;
		RefreshLabels();
		Item_List.itemRenderer = OnItemRender;
		_items.Clear();
		foreach (int item in collaborationGoods.PlayerPhotoID)
		{
			_items.Add((item, 1));
		}
		foreach (KeyValuePair<int, int> otherReward in collaborationGoods.OtherRewards)
		{
			_items.Add((otherReward.Key, otherReward.Value));
		}
		Item_List.numItems = _items.Count;
		RefreshPurchaseButton();
		RefreshMoreAwardButton();
		RefreshBgType();
	}

	public override void Dispose()
	{
		Clear();
		base.Dispose();
	}

	private async void RefreshHeroAnimation(SkinStandingPaintingConfigureItem standingPainting, GGraph graph)
	{
		if (standingPainting == null)
		{
			graph.visible = false;
			return;
		}
		await SimpleSingletonProvider<ExternalAssetManager>.inst.PlayAnimationInUI(standingPainting, "Walk", graph, 10f);
		graph.visible = true;
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (startScrollStatus && list_Labels.numItems > 1)
		{
			if (autoScrollTime >= 2f)
			{
				list_Labels.scrollPane.ScrollRight(1f, ani: true);
				autoScrollTime = 0f;
			}
			autoScrollTime += Time.deltaTime;
		}
	}

	private void StartAutoScroll(EventContext context)
	{
		list_Labels.onTouchEnd.Retain();
		autoScrollTime = 0f;
		startScrollStatus = true;
		list_Labels.onTouchEnd.Release();
	}

	private void StopAutoScroll(EventContext context)
	{
		list_Labels.onTouchBegin.Retain();
		autoScrollTime = 0f;
		startScrollStatus = false;
		list_Labels.onTouchBegin.Release();
	}

	public void InitComponent()
	{
		list_Labels.SetVirtualAndLoop();
		list_Labels.scrollPane.decelerationRate = 0.05f;
		list_Labels.itemRenderer = RefreshLabelItem;
	}

	public void SetShowPopup(Action<float, float, int> action)
	{
		showPopup = action;
	}

	public void AddEvent()
	{
		list_Labels.scrollPane.onScroll.Add(ScrollActivity);
		list_Labels.onTouchBegin.Add(StopAutoScroll);
		list_Labels.onTouchEnd.Add(StartAutoScroll);
		btn_Expand.onClick.Add(OnBtnExpandClicked);
		btn_MoreAward.onClick.Add(OnBtnMoreAwardClicked);
		base.onRollOver.Add(ItemSelected);
		base.onRollOut.Add(RefreshBgType);
		SimpleSingletonProvider<GameLogicManager>.inst.store.signal.refreshCurShelf.AddListener(RefreshState);
	}

	public void RemoveEvent()
	{
		list_Labels.scrollPane.onScroll.Remove(ScrollActivity);
		list_Labels.onTouchBegin.Remove(StopAutoScroll);
		list_Labels.onTouchEnd.Remove(StartAutoScroll);
		btn_Expand.onClick.Remove(OnBtnExpandClicked);
		btn_MoreAward.onClick.Remove(OnBtnMoreAwardClicked);
		base.onRollOver.Remove(ItemSelected);
		base.onRollOut.Remove(RefreshBgType);
		SimpleSingletonProvider<GameLogicManager>.inst.store.signal.refreshCurShelf.RemoveListener(RefreshState);
	}

	public void Clear()
	{
		startScrollStatus = false;
		CollaborationGoods = null;
		LabelIds.Clear();
		autoScrollTime = 0f;
		_displayCount = 0;
		if (type.selectedIndex == 0)
		{
			SimpleSingletonProvider<ExternalAssetManager>.inst.StopAnimationInUI(heroItem.graph_Skin);
			return;
		}
		SimpleSingletonProvider<ExternalAssetManager>.inst.StopAnimationInUI(heroItem1.graph_Skin);
		SimpleSingletonProvider<ExternalAssetManager>.inst.StopAnimationInUI(heroItem2.graph_Skin);
	}

	private void ScrollActivity(EventContext context)
	{
		if (list_Labels.numItems > 0)
		{
			int selectedIndex = list_Labels.scrollPane.currentPageX % list_Labels.numItems;
			list_Page.selectedIndex = selectedIndex;
		}
	}

	private void RefreshLabels()
	{
		if (CollaborationGoods == null)
		{
			return;
		}
		LabelIds.Clear();
		foreach (int item in CollaborationGoods.AccountBackgroundID)
		{
			LabelIds.Add(item);
		}
		_displayCount = Mathf.Max(LabelIds.Count, CollaborationGoods.AccountBackgroundID.Count);
		list_Labels.numItems = _displayCount;
		list_Labels.scrollPane.touchEffect = _displayCount > 1;
		list_Page.numItems = _displayCount;
		list_Page.visible = _displayCount > 1;
		startScrollStatus = true;
		list_Labels.scrollPane.onScroll.Call();
	}

	private void RefreshLabelItem(int index, GObject item)
	{
		if (!(item is UICom_PlayerLabel com_Label) || _displayCount <= 0)
		{
			return;
		}
		string playerName = SimpleSingletonProvider<GameLogicManager>.inst.account.GetName();
		int level = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo().Level;
		int photoAndBackgroundName = CollaborationGoods.PhotoAndBackgroundName;
		txt_LabelTitle.text = photoAndBackgroundName.GetLocal(UIStringType.Collaboration);
		if (LabelIds.Count > 0)
		{
			int index2 = index % LabelIds.Count;
			int labelItemId = LabelIds[index2];
			item.onClick.Set((EventCallback0)delegate
			{
				SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(labelItemId, 1, _Usable: false).Forget();
			});
			(string, bool) playerLabel = labelItemId.GetItemInfoConfigure().SubMeterID.GetFashionAccountBackgroundConfigure().GetPlayerLabel();
			CommonUIManager.RendererLabelInfo(com_Label, playerName, level);
			CommonUIManager.RendererLabel(UIType.Window, 354, com_Label, playerLabel.Item1, playerLabel.Item2);
		}
		else
		{
			CommonUIManager.RendererLabelInfo(com_Label, playerName, level);
		}
		RepeatedField<int> playerPhotoID = CollaborationGoods.PlayerPhotoID;
		if (playerPhotoID.Count > 0)
		{
			int index3 = index % playerPhotoID.Count;
			string fashionAccountHeadShot = playerPhotoID[index3].GetItemInfoConfigure().SubMeterID.GetFashionAccountHeadShot();
			CommonUIManager.RendererHeadShot(com_Label, fashionAccountHeadShot, isVideo: false);
		}
		else
		{
			CommonUIManager.RendererHeadShot(com_Label, "", isVideo: false);
		}
	}

	private void RefreshState(int shelf)
	{
		RefreshPurchaseButton();
		RefreshMoreAwardButton();
		RefreshBgType();
	}

	private void RefreshMoreAwardButton()
	{
		if (CollaborationGoods.RelatedGoodsId == 0)
		{
			moreAward.selectedIndex = 0;
			return;
		}
		RechargeGoods rechargeGoodsByShopTypeAndGoodsID = SimpleSingletonProvider<GameLogicManager>.inst.store.GetRechargeGoodsByShopTypeAndGoodsID(27, CollaborationGoods.RelatedGoodsId);
		if (rechargeGoodsByShopTypeAndGoodsID != null && rechargeGoodsByShopTypeAndGoodsID.SellOut())
		{
			moreAward.selectedIndex = bg_type;
		}
		else
		{
			moreAward.selectedIndex = 0;
		}
	}

	private void RefreshPurchaseButton()
	{
		RechargeGoods goodsData = SimpleSingletonProvider<GameLogicManager>.inst.store.GetRechargeGoodsByShopTypeAndGoodsID(27, CollaborationGoods.GoodsId);
		if (goodsData.SellOut())
		{
			bg_type = 0;
		}
		else
		{
			bg_type = 1;
		}
		btn_Purchase.Type.selectedIndex = type.selectedIndex;
		string discountPriceText = goodsData.GetDiscountPriceText("0.00");
		btn_Purchase.txt_Price.text = discountPriceText;
		btn_Purchase.txt_Price.AddCurrencySymbols(discountPriceText);
		if (btn_Purchase.Type.selectedIndex == 1)
		{
			string originalPriceText = goodsData.GetOriginalPriceText("0.00");
			btn_Purchase.txt_OriginalPrice.text = originalPriceText;
			btn_Purchase.txt_OriginalPrice.AddCurrencySymbols(originalPriceText);
		}
		btn_Purchase.touchable = !goodsData.SellOut();
		btn_Purchase.grayed = !btn_Purchase.touchable;
		btn_Purchase.onClick.Set((EventCallback0)delegate
		{
			if (!btn_Purchase.grayed)
			{
				OnRequestPurchase(goodsData);
			}
		});
	}

	private void ItemSelected()
	{
		BGType.selectedIndex = 0;
	}

	private void RefreshBgType()
	{
		BGType.selectedIndex = bg_type;
	}

	private async void OnRequestPurchase(RechargeGoods _Goods)
	{
		btn_Purchase.onClick.Retain();
		List<string> ownedHeroNames = GetOwnedHeroNames(CollaborationGoods.HeroID);
		if (ownedHeroNames.Count <= 0)
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.store.RequestCreateOrder(_Goods, 1);
		}
		else
		{
			string arg = string.Join("、", ownedHeroNames);
			await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(string.Format(2800001.GetLocal(UIStringType.Collaboration), arg), delegate
			{
				SimpleSingletonProvider<GameLogicManager>.inst.store.RequestCreateOrder(_Goods, 1).Forget();
			});
		}
		btn_Purchase.onClick.Release();
	}

	private List<string> GetOwnedHeroNames(RepeatedField<int> heroIds)
	{
		List<string> list = new List<string>();
		List<HeroCardData> cardsData = GetCardsData(heroIds);
		for (int i = 0; i < cardsData.Count; i++)
		{
			if (cardsData[i].IsHas)
			{
				CharacterInfoConfigure heroCharacterConfigure = CharacterHandle.GetHeroCharacterConfigure(heroIds[i]);
				list.Add(heroCharacterConfigure.NameID.GetLocal(UIStringType.Character));
			}
		}
		return list;
	}

	private List<HeroCardData> GetCardsData(RepeatedField<int> HeroIDs)
	{
		List<HeroCardData> list = new List<HeroCardData>();
		foreach (int HeroID in HeroIDs)
		{
			list.Add(SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetCardData(HeroID));
		}
		return list;
	}

	private void OnItemRender(int index, GObject item)
	{
		if (item is UIMGWT_Com_CommonItem item2 && index >= 0 && index <= _items.Count)
		{
			(int, int) tuple = _items[index];
			RenderItem(tuple.Item1, tuple.Item2, item2);
		}
	}

	private void RenderItem(int itemId, int count, UIMGWT_Com_CommonItem item)
	{
		item.txt_title.text = $"x{count}";
		item.loader_Icon.url = itemId.GetItemInfoConfigure().ShowIcon;
		item.onClick.Set((EventCallback0)delegate
		{
			SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(itemId, count, _Usable: false).Forget();
		});
	}

	private void OnBtnExpandClicked()
	{
		btn_Expand.onClick.Retain();
		open.selectedIndex = ((open.selectedIndex == 0) ? 1 : 0);
		btn_Expand.onClick.Release();
	}

	private void OnBtnMoreAwardClicked()
	{
		btn_MoreAward.onClick.Retain();
		Vector2 pt = btn_MoreAward.LocalToGlobal(Vector2.zero);
		Vector2 vector = GRoot.inst.GlobalToLocal(pt);
		vector.y += btn_MoreAward.height;
		showPopup?.Invoke(vector.x, vector.y, CollaborationGoods.Index);
		btn_MoreAward.onClick.Release();
	}

	public static UIMGWT_Store_Com_GoodsItem CreateInstance()
	{
		return (UIMGWT_Store_Com_GoodsItem)UIPackage.CreateObject("MGWTStore", "MGWT_Store_Com_GoodsItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		BGType = GetControllerAt(0);
		type = GetControllerAt(1);
		open = GetControllerAt(2);
		herostate = GetControllerAt(3);
		moreAward = GetControllerAt(4);
		list_Labels = (GList)GetChildAt(4);
		txt_LabelTitle = (GTextField)GetChildAt(5);
		list_Page = (GList)GetChildAt(6);
		Item_List = (GList)GetChildAt(7);
		heroItem = (UIMGWT_Com_HeroItem)GetChildAt(8);
		heroItem1 = (UIMGWT_Com_HeroItem)GetChildAt(9);
		heroItem2 = (UIMGWT_Com_HeroItem)GetChildAt(10);
		btn_Purchase = (UIMGWT_Store_Buy_Button)GetChildAt(12);
		btn_Expand = (GButton)GetChildAt(13);
		btn_MoreAward = (GButton)GetChildAt(14);
	}
}
