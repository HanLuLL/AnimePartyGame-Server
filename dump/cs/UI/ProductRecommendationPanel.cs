using System.Collections.Generic;
using Core;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class ProductRecommendationPanel : BasePanel<UIProductRecommendationPanel>
{
	private List<DailRefreshGoods> GoodsDatas;

	public ProductRecommendationPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIProductRecommendationPanel.CreateInstance();
		base.Create();
	}

	protected override async void InitData(params object[] objs)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.guide.GuideStatus(GuideType.ProductRecommendation))
		{
			GoodsDatas = SimpleSingletonProvider<GameLogicManager>.inst.store.GetDailyRefresh();
			GoodsDatas.Sort(ToCompare);
			if (GoodsDatas.Count > 0 && GoodsDatas[0].SalePrice == 0 && !GoodsDatas[0].SellOut() && !GoodsDatas[0].IsOwn())
			{
				await SimpleSingletonProvider<UIManager>.inst.guide.ShowTransparent();
				SimpleSingletonProvider<GameLogicManager>.inst.guide.TriggerGuide(200100);
			}
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		base.ui.com_GiftpackBanner.Show();
		base.ui.btn_ExchangeStore.redPoint.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.store.ExchangeNewGoodsStatus() ? 1 : 0);
		base.ui.btn_RechargeStore.redPoint.selectedIndex = ((SimpleSingletonProvider<GameLogicManager>.inst.store.Get7DailyGiftStatus(ShopTabType.Day7GiftPackagePveYear) || SimpleSingletonProvider<GameLogicManager>.inst.store.Get7DailyGiftStatus(ShopTabType.Day7GiftPackage) || SimpleSingletonProvider<GameLogicManager>.inst.store.Get7DailyGiftStatus(ShopTabType.Day7GiftPackagePve) || SimpleSingletonProvider<GameLogicManager>.inst.store.RechargeNewGoodsStatus()) ? 1 : 0);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.com_Goods.list_Goods.itemRenderer = RendererGoods;
		base.ui.com_GiftpackBanner.InitComponent();
	}

	public override void Refresh()
	{
		base.Refresh();
		base.ui.language.selectedIndex = ((GameSettings.languageType != LanguageType.SimplifiedChinese) ? 1 : 0);
		RefreshDailRefreshGoods(0);
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_ExchangeStore.onClick.Add(OpenExchangeStore);
		base.ui.btn_RechargeStore.onClick.Add(OpenRechargeStore);
		base.ui.btn_Return.onClick.Add(ReturnPanel);
		base.ui.com_GiftpackBanner.AddEvent();
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_ExchangeStore.onClick.Remove(OpenExchangeStore);
		base.ui.btn_RechargeStore.onClick.Remove(OpenRechargeStore);
		base.ui.btn_Return.onClick.Remove(ReturnPanel);
		base.ui.com_GiftpackBanner.RemoveEvent();
	}

	protected override void AddListener()
	{
		base.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.store.signal.refreshCurShelf.AddListener(RefreshDailRefreshGoods);
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.store.signal.refreshCurShelf.RemoveListener(RefreshDailRefreshGoods);
	}

	public override void Close()
	{
		base.Close();
		SimpleSingletonProvider<UIManager>.inst.guide.HideGuide();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private async void ReturnPanel()
	{
		base.ui.btn_Return.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Home);
		base.ui.btn_Return.onClick.Release();
	}

	private async void OpenExchangeStore()
	{
		base.ui.btn_ExchangeStore.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Store, StaticConfigure.ExchangeStore.Shelfs[0].ShopTabTypes[0]);
		base.ui.btn_ExchangeStore.onClick.Release();
	}

	private async void OpenRechargeStore()
	{
		base.ui.btn_RechargeStore.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Store, StaticConfigure.RechargeStore.Shelfs[0].ShopTabTypes[0]);
		base.ui.btn_RechargeStore.onClick.Release();
	}

	private void RefreshDailRefreshGoods(int ShopTab)
	{
		base.ui.com_Goods.txt_Time.text = TimeHelper.GetDailyTime();
		if (StaticConfigure.ExchangeStore.InfoDict.TryGetValue(2, out var value))
		{
			base.ui.com_Goods.txt_Title.text = value.NameID.GetLocal(UIStringType.ExchangeStore);
		}
		GoodsDatas = SimpleSingletonProvider<GameLogicManager>.inst.store.GetDailyRefresh();
		GoodsDatas.Sort(ToCompare);
		base.ui.com_Goods.list_Goods.numItems = GoodsDatas?.Count ?? 0;
	}

	private void RendererGoods(int index, GObject item)
	{
		if (item is UIProductRecommendation_Button_GoodsItem uIProductRecommendation_Button_GoodsItem && index < GoodsDatas.Count)
		{
			uIProductRecommendation_Button_GoodsItem.isEmpty.selectedIndex = 0;
			uIProductRecommendation_Button_GoodsItem.InitData(GoodsDatas[index]);
		}
	}

	private int ToCompare(DailRefreshGoods x, DailRefreshGoods y)
	{
		if (x.SellOut().CompareTo(y.SellOut()) != 0)
		{
			return x.SellOut().CompareTo(y.SellOut());
		}
		if (x.IsOwn().CompareTo(y.IsOwn()) != 0)
		{
			return x.IsOwn().CompareTo(y.IsOwn());
		}
		return x.SalePrice.CompareTo(y.SalePrice);
	}

	public async void GuideBuyGoods()
	{
		Vector2 pt = base.ui.com_MaskGoods.LocalToGlobal(Vector2.zero);
		pt = GRoot.inst.GlobalToLocal(pt);
		await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMask(pt, base.ui.com_MaskGoods.width, base.ui.com_MaskGoods.height, _needTransparentMask: false, isRect: true);
	}
}
