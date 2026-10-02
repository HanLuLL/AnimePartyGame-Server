using System;
using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;

namespace UI;

public class StorePanel : BasePanel<UIStorePanel>
{
	public const string Store_ExchangeCharacterPath = "UT_Store_ExchangeCharacter";

	public const string Store_ExchangeCharacterSFWPath = "UT_Store_ExchangeCharacter_sfw";

	private StoreType storeType;

	private ShopTabType mustShowType;

	private int mustShowGoodsId;

	private StoreType curStoreType;

	private int _curSkinGroupIndex;

	private readonly List<StoreTabData> tabDatas = new List<StoreTabData>();

	private int _curShelfDataIndex;

	private List<BaseGoodsData> GoodsDatas;

	private ShopTypeData curShopData;

	private Dictionary<int, List<BaseGoodsData>> skinGoodsDatas = new Dictionary<int, List<BaseGoodsData>>(8);

	private const int WeekLimitPropId = 6;

	private const ShopTabType ShowWeekLimitTabType = ShopTabType.Pve;

	public StorePanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIStorePanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		if (objs == null || objs.Length == 0)
		{
			mustShowType = ShopTabType.None;
		}
		else if (objs[0] is RepeatedField<int> repeatedField)
		{
			mustShowType = (ShopTabType)repeatedField[0];
			mustShowGoodsId = ((repeatedField.Count > 1) ? repeatedField[1] : 0);
		}
		else
		{
			mustShowType = (ShopTabType)objs[0];
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.com_ShelfTab.list_tab.itemRenderer = RendererShelfTab;
		base.ui.com_Recharge.list_SubTab.itemRenderer = RendererSubTab;
		base.ui.com_Exchange.list_SubTab.itemRenderer = RendererSubTab;
		base.ui.com_Exchange.list_ExchangeGoods.SetVirtual();
		base.ui.com_Exchange.list_ExchangeGoods.itemRenderer = RendererGoods;
		base.ui.com_Recharge.list_RechargeGoods.SetVirtual();
		base.ui.com_Recharge.list_RechargeGoods.itemRenderer = RendererGoods;
		base.ui.com_GiftPackage.list_GiftPackage.SetVirtual();
		base.ui.com_GiftPackage.list_GiftPackage.itemRenderer = RendererGiftPackage;
		base.ui.com_Skin.list_SkinTab.SetVirtual();
		base.ui.com_Skin.list_SkinTab.itemRenderer = RendererSkinGroupTab;
		base.ui.com_Skin.list_Skin.SetVirtual();
		base.ui.com_Skin.list_Skin.itemRenderer = RendererSkin;
		base.ui.com_7Day_PVP.InitComponents();
		base.ui.com_7Day_PVE.InitComponents();
		base.ui.com_Anniversary_2nd.InitComponents();
		base.ui.com_Recommend.InitComponents();
		base.ui.com_MonthCard.InitComponents();
	}

	public override void Refresh()
	{
		base.Refresh();
		RefreshTab();
		base.ui.com_Exchange.Character.url = (GameSettings.angelMode ? "UT_Store_ExchangeCharacter_sfw" : "UT_Store_ExchangeCharacter");
		UniTask.NextFrame().ContinueWith((Action)OnShelfTabScroll).Forget();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_Return.onClick.Add(ReturnPanel);
		base.ui.com_7Day_PVP.AddEvent();
		base.ui.com_7Day_PVE.AddEvent();
		base.ui.com_Anniversary_2nd.AddEvent();
		base.ui.com_CharacterGift.AddEvent();
		base.ui.com_MonthCard.AddEvent();
		base.ui.com_Recommend.AddEvent();
		base.ui.btn_PaymentServicesAct.onClick.Add(ShowPaymentServicesAct);
		base.ui.btn_SpecifiedCommercialTransactions.onClick.Add(ShowSpecifiedCommercialTransactions);
		base.ui.com_ShelfTab.list_tab.scrollPane.onScroll.Add(OnShelfTabScroll);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Return.onClick.Remove(ReturnPanel);
		base.ui.com_7Day_PVP.RemoveEvent();
		base.ui.com_7Day_PVE.RemoveEvent();
		base.ui.com_Anniversary_2nd.RemoveEvent();
		base.ui.com_CharacterGift.RemoveEvent();
		base.ui.com_MonthCard.RemoveEvent();
		base.ui.com_Recommend.RemoveEvent();
		base.ui.btn_PaymentServicesAct.onClick.Remove(ShowPaymentServicesAct);
		base.ui.btn_SpecifiedCommercialTransactions.onClick.Remove(ShowSpecifiedCommercialTransactions);
		base.ui.com_ShelfTab.list_tab.scrollPane.onScroll.Remove(OnShelfTabScroll);
	}

	protected override void AddListener()
	{
		base.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.store.signal.refreshCurShelf.AddListener(RefreshCurShelfData);
		base.ui.com_7Day_PVP.AddListener();
		base.ui.com_7Day_PVE.AddListener();
		base.ui.com_Anniversary_2nd.AddListener();
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.store.signal.refreshCurShelf.RemoveListener(RefreshCurShelfData);
		base.ui.com_7Day_PVP.RemoveListener();
		base.ui.com_7Day_PVE.RemoveListener();
		base.ui.com_Anniversary_2nd.RemoveListener();
	}

	public override void Close()
	{
		if (base.ui != null)
		{
			base.ui.com_MonthCard.Close();
			base.ui.com_Exchange.list_ExchangeGoods.numItems = 0;
			base.ui.com_Recharge.list_RechargeGoods.numItems = 0;
		}
		curShopData = null;
		mustShowType = ShopTabType.None;
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private async void ReturnPanel()
	{
		base.ui.btn_Return.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
		base.ui.btn_Return.onClick.Release();
	}

	private void ShowPaymentServicesAct()
	{
		base.ui.btn_PaymentServicesAct.onClick.Retain();
		SimpleSingletonProvider<UIManager>.inst.info.ShowPaymentServicesAct();
		base.ui.btn_PaymentServicesAct.onClick.Release();
	}

	private void ShowSpecifiedCommercialTransactions()
	{
		base.ui.btn_SpecifiedCommercialTransactions.onClick.Retain();
		SimpleSingletonProvider<UIManager>.inst.info.ShowSpecifiedCommercialTransactions();
		base.ui.btn_SpecifiedCommercialTransactions.onClick.Release();
	}

	private void OnShelfTabScroll()
	{
		ScrollPane scrollPane = base.ui.com_ShelfTab.list_tab.scrollPane;
		if (scrollPane != null)
		{
			if (!(scrollPane.contentHeight > scrollPane.viewHeight + 0.5f))
			{
				base.ui.com_ShelfTab.up_btn.visible = false;
				base.ui.com_ShelfTab.down_btn.visible = false;
			}
			else
			{
				base.ui.com_ShelfTab.down_btn.visible = scrollPane.percY < 1f;
				base.ui.com_ShelfTab.up_btn.visible = scrollPane.percY > 0f;
			}
		}
	}

	private void RefreshCurShelfData(int ShopTab)
	{
		base.ui.com_ShelfTab.list_tab.touchable = false;
		if (ShopTab == 13 || ShopTab == 37)
		{
			RefreshShelfTab();
			RefreshCurShelf();
		}
		if (curShopData != null)
		{
			RefreshGoods(curShopData);
		}
		base.ui.com_ShelfTab.list_tab.touchable = true;
	}

	private void RefreshTab()
	{
		ReadyTabDatas();
		RefreshShelfTab();
		RefreshCurShelf();
		if (mustShowType != ShopTabType.None)
		{
			SwitchMustShowTab(mustShowType);
		}
		else
		{
			base.ui.com_ShelfTab.list_tab.GetChildAt(0).onClick.Call();
		}
	}

	private void ReadyTabDatas()
	{
		tabDatas.Clear();
		curStoreType = SimpleSingletonProvider<GameLogicManager>.inst.store.GetStoreType(mustShowType);
		if (curStoreType == StoreType.EXCHARGE)
		{
			foreach (ExchangeStoreShelfConfigure shelf in StaticConfigure.ExchangeStore.Shelfs)
			{
				if (shelf.IsShow)
				{
					StoreTabData storeTabData = new StoreTabData(shelf.Id, shelf.ShopTabTypes, shelf.NameID.GetLocal(UIStringType.ExchangeStore), shelf.TabOrder);
					if (storeTabData._IsShow)
					{
						tabDatas.Add(storeTabData);
					}
				}
			}
		}
		else
		{
			foreach (RechargeStoreShelfConfigure shelf2 in StaticConfigure.RechargeStore.Shelfs)
			{
				StoreTabData storeTabData2 = new StoreTabData(shelf2.Id, shelf2.ShopTabTypes, shelf2.NameID.GetLocal(UIStringType.RechargeStore), shelf2.Order);
				if (storeTabData2._IsShow)
				{
					tabDatas.Add(storeTabData2);
				}
			}
		}
		tabDatas.Sort((StoreTabData x, StoreTabData y) => x.tabOrder.CompareTo(y.tabOrder));
	}

	private void RefreshShelfTab()
	{
		base.ui.com_ShelfTab.list_tab.numItems = tabDatas.Count;
	}

	private void RendererShelfTab(int index, GObject item)
	{
		if (item is UIStore_SwitchTab_Button uIStore_SwitchTab_Button)
		{
			StoreTabData storeTabData = tabDatas[index];
			uIStore_SwitchTab_Button.text = storeTabData.parentName;
			uIStore_SwitchTab_Button.redpoint.selectedIndex = ((!storeTabData.newGoods && (!storeTabData.has7DailyGift_PVEYear || !SimpleSingletonProvider<GameLogicManager>.inst.store.Get7DailyGiftStatus(ShopTabType.Day7GiftPackagePveYear)) && (!storeTabData.has7DailyGift_PVE || !SimpleSingletonProvider<GameLogicManager>.inst.store.Get7DailyGiftStatus(ShopTabType.Day7GiftPackagePve)) && (!storeTabData.has7DailyGift_PVP || !SimpleSingletonProvider<GameLogicManager>.inst.store.Get7DailyGiftStatus(ShopTabType.Day7GiftPackage))) ? 1 : 0);
			uIStore_SwitchTab_Button.onClick.Set((EventCallback0)delegate
			{
				base.ui.com_ShelfTab.list_tab.selectedIndex = index;
				item.onClick.Retain();
				_curShelfDataIndex = index;
				RefreshSubTab();
				item.onClick.Release();
			});
		}
	}

	private void RefreshSubTab()
	{
		StoreTabData storeTabData = tabDatas[_curShelfDataIndex];
		if (storeTabData.shopTypes.Count == 1)
		{
			base.ui.com_Recharge.list_SubTab.visible = false;
			base.ui.com_Exchange.list_SubTab.visible = false;
			RefreshGoods(storeTabData.shopTypes[0]);
			return;
		}
		base.ui.com_Recharge.list_SubTab.visible = true;
		base.ui.com_Exchange.list_SubTab.visible = true;
		int index = 0;
		if (mustShowType != ShopTabType.None)
		{
			index = storeTabData.GetTabNodeIndex(mustShowType);
		}
		if (curStoreType == StoreType.EXCHARGE)
		{
			base.ui.com_Exchange.list_SubTab.numItems = storeTabData.shopTypes.Count;
			base.ui.com_Exchange.list_SubTab.GetChildAt(index).onClick.Call();
		}
		else
		{
			base.ui.com_Recharge.list_SubTab.numItems = storeTabData.shopTypes.Count;
			base.ui.com_Recharge.list_SubTab.GetChildAt(index).onClick.Call();
		}
	}

	private void RendererSubTab(int index, GObject item)
	{
		if (!(item is UIStore_GoodsList_SwitchTabButton uIStore_GoodsList_SwitchTabButton))
		{
			return;
		}
		StoreTabData storeTabData = tabDatas[_curShelfDataIndex];
		ShopTypeData shopType = storeTabData.shopTypes[index];
		uIStore_GoodsList_SwitchTabButton.redpoint.selectedIndex = ((!shopType.newGoods && (shopType.tabType != ShopTabType.Day7GiftPackagePveYear || !SimpleSingletonProvider<GameLogicManager>.inst.store.Get7DailyGiftStatus(ShopTabType.Day7GiftPackagePveYear)) && (shopType.tabType != ShopTabType.Day7GiftPackagePve || !SimpleSingletonProvider<GameLogicManager>.inst.store.Get7DailyGiftStatus(ShopTabType.Day7GiftPackagePve)) && (shopType.tabType != ShopTabType.Day7GiftPackage || !SimpleSingletonProvider<GameLogicManager>.inst.store.Get7DailyGiftStatus(ShopTabType.Day7GiftPackage))) ? 1 : 0);
		uIStore_GoodsList_SwitchTabButton.text = shopType.tabName;
		uIStore_GoodsList_SwitchTabButton.onClick.Set((EventCallback0)delegate
		{
			if (curStoreType == StoreType.EXCHARGE)
			{
				base.ui.com_Exchange.list_SubTab.selectedIndex = index;
			}
			else
			{
				base.ui.com_Recharge.list_SubTab.selectedIndex = index;
			}
			item.onClick.Retain();
			RefreshGoods(shopType);
			item.onClick.Release();
		});
	}

	public void SwitchMustShowTab(RepeatedField<int> wayParam)
	{
		mustShowType = (ShopTabType)wayParam[0];
		mustShowGoodsId = ((wayParam.Count > 1) ? wayParam[1] : 0);
		if (storeType == SimpleSingletonProvider<GameLogicManager>.inst.store.GetStoreType(mustShowType))
		{
			SwitchMustShowTab(mustShowType);
		}
		else
		{
			RefreshTab();
		}
	}

	public void SwitchMustShowTab(ShopTabType type)
	{
		for (int i = 0; i < tabDatas.Count; i++)
		{
			if (tabDatas[i].GetTabNodeIndex(type) != -1)
			{
				if (base.ui.com_ShelfTab.list_tab.GetChildAt(i) is UIStore_SwitchTab_Button uIStore_SwitchTab_Button)
				{
					uIStore_SwitchTab_Button.selected = true;
					uIStore_SwitchTab_Button.onClick.Call();
					uIStore_SwitchTab_Button.Switch_in.Play();
				}
				mustShowType = ShopTabType.None;
				return;
			}
		}
		mustShowType = ShopTabType.None;
		base.ui.com_ShelfTab.list_tab.GetChildAt(0).onClick.Call();
	}

	private void RefreshGoods(ShopTypeData typeData)
	{
		curShopData = typeData;
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.switchTokenList.Dispatch(typeData.currencyBar);
		if (typeData.tabType == ShopTabType.Day7GiftPackage)
		{
			base.ui.goodsType.selectedIndex = 3;
			base.ui.com_7Day_PVP.Refresh(typeData);
			return;
		}
		if (typeData.tabType == ShopTabType.Day7GiftPackagePve)
		{
			base.ui.goodsType.selectedIndex = 7;
			base.ui.com_7Day_PVE.Refresh(typeData);
			return;
		}
		if (typeData.tabType == ShopTabType.Day7GiftPackagePveYear)
		{
			base.ui.goodsType.selectedIndex = 8;
			base.ui.com_Anniversary_2nd.Refresh(typeData);
			return;
		}
		if (typeData.tabType == ShopTabType.BeginnerPack)
		{
			base.ui.goodsType.selectedIndex = 4;
			base.ui.com_CharacterGift.Refresh(typeData);
			return;
		}
		if (typeData.tabType == ShopTabType.MonthlyCard)
		{
			base.ui.goodsType.selectedIndex = 5;
			base.ui.com_MonthCard.Refresh(typeData);
			return;
		}
		if (typeData.tabType == ShopTabType.AlternateRecommendation)
		{
			base.ui.goodsType.selectedIndex = 6;
			base.ui.com_Recommend.Refresh(typeData);
			return;
		}
		GoodsDatas = SimpleSingletonProvider<GameLogicManager>.inst.store.GetGoodsByShopType(curShopData.tabType);
		GoodsDatas.Sort(ToCompare);
		if (typeData.tabType == ShopTabType.Skin)
		{
			int num = 0;
			skinGoodsDatas.Clear();
			foreach (BaseGoodsData goodsData in GoodsDatas)
			{
				int skinGroupIdByGoodsId = SimpleSingletonProvider<GameLogicManager>.inst.store.GetSkinGroupIdByGoodsId(goodsData.goodsId);
				if (skinGroupIdByGoodsId != 0)
				{
					if (mustShowGoodsId != 0 && goodsData.goodsId == mustShowGoodsId)
					{
						num = skinGroupIdByGoodsId;
					}
					if (!skinGoodsDatas.ContainsKey(skinGroupIdByGoodsId))
					{
						skinGoodsDatas.Add(skinGroupIdByGoodsId, new List<BaseGoodsData>(8));
					}
					skinGoodsDatas[skinGroupIdByGoodsId].Add(goodsData);
				}
			}
			base.ui.goodsType.selectedIndex = 1;
			base.ui.com_Skin.list_SkinTab.numItems = skinGoodsDatas.Keys.Count;
			base.ui.com_Skin.list_SkinTab.scrollPane.touchEffect = skinGoodsDatas.Keys.Count > 5;
			List<int> sortSkinGoodsGroupConfigures = SimpleSingletonProvider<GameLogicManager>.inst.store.GetSortSkinGoodsGroupConfigures();
			int index = ((num != 0) ? sortSkinGoodsGroupConfigures.IndexOf(num) : 0);
			if (num == 0)
			{
				num = sortSkinGoodsGroupConfigures[0];
			}
			base.ui.com_Skin.list_SkinTab.GetChildAt(index).onClick.Call();
			SetMustShowCommonGoods(base.ui.com_Skin.list_Skin, skinGoodsDatas[num]);
			UpdateRedStatus_Store(typeData.tabType);
		}
		else if (typeData.tabType == ShopTabType.GiftPackage)
		{
			base.ui.goodsType.selectedIndex = 2;
			base.ui.com_GiftPackage.list_GiftPackage.numItems = GoodsDatas?.Count ?? 0;
			SetMustShowCommonGoods(base.ui.com_GiftPackage.list_GiftPackage, GoodsDatas);
			UpdateRedStatus_Store(typeData.tabType);
		}
		else
		{
			base.ui.goodsType.selectedIndex = 0;
			int num2 = GoodsDatas?.Count ?? 0;
			if (SimpleSingletonProvider<GameLogicManager>.inst.store.GetStoreType(typeData.tabType) == StoreType.EXCHARGE)
			{
				base.ui.storeType.selectedIndex = 0;
				RefreshGoodsItem(base.ui.com_Exchange.list_ExchangeGoods, num2, 3);
				SetMustShowCommonGoods(base.ui.com_Exchange.list_ExchangeGoods, GoodsDatas);
			}
			else
			{
				base.ui.storeType.selectedIndex = 1;
				RefreshGoodsItem(base.ui.com_Recharge.list_RechargeGoods, num2, 5);
				SetMustShowCommonGoods(base.ui.com_Recharge.list_RechargeGoods, GoodsDatas);
			}
			RefreshPropText(typeData);
			if (typeData.newGoods)
			{
				UpdateRedStatus_Store(typeData.tabType);
			}
		}
	}

	private void RefreshGoodsItem(GList list, int num, int maxNum)
	{
		list.numItems = 0;
		list.numItems = ((num < maxNum * 2) ? (maxNum * 2) : ((num + maxNum - 1) / maxNum * maxNum));
	}

	private void SetMustShowCommonGoods(GList list, List<BaseGoodsData> goodsDatas)
	{
		if (mustShowGoodsId == 0)
		{
			return;
		}
		for (int i = 0; i < goodsDatas.Count; i++)
		{
			if (goodsDatas[i].shopTabType == mustShowType && goodsDatas[i].goodsId == mustShowGoodsId)
			{
				list.ScrollToView(i);
				int index = list.ItemIndexToChildIndex(i);
				((GButton)list.GetChildAt(index)).onClick.Call();
				mustShowGoodsId = 0;
				break;
			}
		}
	}

	public void RefreshCurShelf()
	{
		base.ui.com_ShelfTab.list_tab.touchable = false;
		if (curShopData != null)
		{
			RefreshGoods(curShopData);
		}
		base.ui.com_ShelfTab.list_tab.touchable = true;
	}

	private int ToCompare(BaseGoodsData x, BaseGoodsData y)
	{
		if (x.SellOut().CompareTo(y.SellOut()) != 0)
		{
			return x.SellOut().CompareTo(y.SellOut());
		}
		if (x.IsOwn().CompareTo(y.IsOwn()) != 0)
		{
			return x.IsOwn().CompareTo(y.IsOwn());
		}
		return x.goodsOrder.CompareTo(y.goodsOrder);
	}

	private int ToCompareSkinChest(BaseGoodsData x, BaseGoodsData y)
	{
		bool flag = SimpleSingletonProvider<GameLogicManager>.inst.store.IsPurchaseChestGoods(x.itemConfig.SubMeterID);
		bool value = SimpleSingletonProvider<GameLogicManager>.inst.store.IsPurchaseChestGoods(y.itemConfig.SubMeterID);
		int num = flag.CompareTo(value);
		if (num == 0)
		{
			return x.goodsOrder.CompareTo(y.goodsOrder);
		}
		return num;
	}

	private void RendererGoods(int index, GObject item)
	{
		if (item is UIButton_GoodsItem_Store uIButton_GoodsItem_Store)
		{
			if (index < GoodsDatas.Count)
			{
				uIButton_GoodsItem_Store.isEmpty.selectedIndex = 0;
				uIButton_GoodsItem_Store.InitData((int)curShopData.tabType, GoodsDatas[index]);
			}
			else
			{
				uIButton_GoodsItem_Store.grayed = false;
				uIButton_GoodsItem_Store.touchable = false;
				uIButton_GoodsItem_Store.isEmpty.selectedIndex = 1;
			}
		}
	}

	private void RendererSkinGroupTab(int index, GObject item)
	{
		UIStore_Button_SkinTab btn = item as UIStore_Button_SkinTab;
		if (btn == null)
		{
			return;
		}
		List<int> groups = SimpleSingletonProvider<GameLogicManager>.inst.store.GetSortSkinGoodsGroupConfigures();
		int key = groups[index];
		if (StaticConfigure.ExchangeStore.SkinGroupDict.TryGetValue(key, out var value))
		{
			btn.icon = value.Icon;
			btn.text = value.NameID.GetLocal(UIStringType.ExchangeStore);
			btn.Cut_in.Play();
			btn.onClick.Set((EventCallback0)delegate
			{
				btn.onClick.Retain();
				btn.Cut_in.Stop();
				btn.selected = true;
				base.ui.com_Skin.list_SkinTab.selectedIndex = index;
				base.ui.com_Skin.list_Skin.numItems = 0;
				_curSkinGroupIndex = index;
				skinGoodsDatas.TryGetValue(groups[_curSkinGroupIndex], out var value2);
				int numItems = value2?.Count ?? 0;
				base.ui.com_Skin.list_Skin.numItems = numItems;
				btn.onClick.Release();
			});
		}
	}

	private void RendererSkin(int index, GObject item)
	{
		if (item is UIStore_Button_SkinItem uIStore_Button_SkinItem && curShopData != null && skinGoodsDatas != null && skinGoodsDatas.Count != 0)
		{
			List<int> sortSkinGoodsGroupConfigures = SimpleSingletonProvider<GameLogicManager>.inst.store.GetSortSkinGoodsGroupConfigures();
			if (skinGoodsDatas.TryGetValue(sortSkinGoodsGroupConfigures[_curSkinGroupIndex], out var value))
			{
				uIStore_Button_SkinItem.InitData((int)curShopData.tabType, value[index]);
			}
		}
	}

	private void RendererGiftPackage(int index, GObject item)
	{
		if (item is UIStore_Item_GiftPackage uIStore_Item_GiftPackage)
		{
			uIStore_Item_GiftPackage.InitData(GoodsDatas[index]);
		}
	}

	private void RefreshPropText(ShopTypeData typeData)
	{
		if (typeData.tabType == ShopTabType.Pve)
		{
			base.ui.com_Exchange.txt_timeTip.visible = false;
			base.ui.com_Recharge.txt_timeTip.visible = false;
			WeeklyLimitPropData weeklyLimitPropData = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetWeeklyLimitPropData(6);
			if (curStoreType == StoreType.EXCHARGE)
			{
				base.ui.com_Exchange.loader_LimitProp.url = weeklyLimitPropData.itemInfo.ShowIcon;
				base.ui.com_Exchange.txt_LimitPropDesc.text = 1057.GetLocal(UIStringType.Message);
				base.ui.com_Exchange.txt_LimitPropCount.text = $"{weeklyLimitPropData.Count}/{weeklyLimitPropData.LimitCount}";
				base.ui.com_Exchange.group_LimitProp.visible = true;
			}
			else
			{
				base.ui.com_Recharge.loader_LimitProp.url = weeklyLimitPropData.itemInfo.ShowIcon;
				base.ui.com_Recharge.txt_LimitPropDesc.text = 1057.GetLocal(UIStringType.Message);
				base.ui.com_Recharge.txt_LimitPropCount.text = $"{weeklyLimitPropData.Count}/{weeklyLimitPropData.LimitCount}";
				base.ui.com_Recharge.group_LimitProp.visible = true;
			}
		}
		else
		{
			base.ui.com_Exchange.group_LimitProp.visible = false;
			base.ui.com_Recharge.group_LimitProp.visible = false;
			string tabTime = typeData.GetTabTime();
			if (string.IsNullOrEmpty(tabTime))
			{
				base.ui.com_Exchange.txt_timeTip.visible = false;
				base.ui.com_Recharge.txt_timeTip.visible = false;
			}
			else if (curStoreType == StoreType.EXCHARGE)
			{
				base.ui.com_Exchange.txt_timeTip.text = tabTime;
				base.ui.com_Exchange.txt_timeTip.visible = true;
			}
			else
			{
				base.ui.com_Recharge.txt_timeTip.text = tabTime;
				base.ui.com_Recharge.txt_timeTip.visible = true;
			}
		}
	}

	private void UpdateRedStatus_Store(ShopTabType tabType)
	{
		List<int> newGoodsByType = SimpleSingletonProvider<GameLogicManager>.inst.store.GetNewGoodsByType((int)tabType);
		if (newGoodsByType.Count != 0)
		{
			LocalCache.UpdateStoreGoodsCache(newGoodsByType);
			RefreshShelfTab();
		}
	}
}
