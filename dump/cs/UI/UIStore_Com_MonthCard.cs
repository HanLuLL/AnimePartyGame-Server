using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;

namespace UI;

public class UIStore_Com_MonthCard : GComponent
{
	private MonthlyCardData monthlyCardData;

	public Controller language;

	public Controller hasPurchase;

	public Controller country;

	public GGraph loader_bg;

	public GGraph loader_Video;

	public GTextField txt_Time;

	public UIStore_Button_PurchaseMonthCard btn_Purchase;

	public GButton btn_Detail;

	public Transition Cutin;

	public const string URL = "ui://zyd0rl00m1i7qq25";

	public void InitComponents()
	{
		loader_bg.FullScreen();
		loader_Video.SetSize(GRoot.inst.width, GRoot.inst.height);
	}

	public void Refresh(ShopTypeData _typeData)
	{
		ShowVideo();
		monthlyCardData = SimpleSingletonProvider<GameLogicManager>.inst.store.GetMonthlyCardConfig();
		txt_Time.text = string.Format(1039.GetLocal(UIStringType.Message), monthlyCardData.deadlineDay);
		hasPurchase.selectedIndex = (monthlyCardData.HasPurchase() ? 1 : 0);
		btn_Purchase.type.selectedIndex = ((monthlyCardData.deadlineDay > 0) ? 1 : 0);
		language.selectedIndex = GetLanguage();
		SimpleSingletonProvider<WebServerManager>.inst.PostGoodsRecord(monthlyCardData.goodsConfig.GoodsID, ShopTabType.MonthlyCard, LogToServerType.GOODS_DETAIL);
		country.selectedIndex = GameSettings.COUNTRY;
		btn_Purchase.txt_Price.text = monthlyCardData.GetDiscountPriceText();
		btn_Purchase.txt_Price.AddCurrencySymbols(btn_Purchase.txt_Price.text);
	}

	public int GetLanguage()
	{
		return int.Parse(GameSettings.GetDataForLanguage("0", "1", "2", "3"));
	}

	public void AddEvent()
	{
		btn_Purchase.onClick.Add(PurchaseMonthCard);
		btn_Detail.onClick.Add(ShowDetail);
	}

	public void RemoveEvent()
	{
		btn_Purchase.onClick.Remove(PurchaseMonthCard);
		btn_Detail.onClick.Remove(ShowDetail);
	}

	private async void ShowDetail()
	{
		btn_Detail.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.rule.TryShow(1046, 1047);
		btn_Detail.onClick.Release();
	}

	private async void PurchaseMonthCard()
	{
		if (!monthlyCardData.PurchaseLic)
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1038);
			return;
		}
		btn_Purchase.onClick.Retain();
		await SimpleSingletonProvider<GameLogicManager>.inst.store.RequestCreateOrder(monthlyCardData, 1);
		btn_Purchase.onClick.Release();
	}

	private void ShowVideo()
	{
		SimpleSingletonProvider<CriMovieManager>.inst.Play(250.GetVideoKey(), loader_Video).Forget();
	}

	public void Close()
	{
		SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(loader_Video);
	}

	public static UIStore_Com_MonthCard CreateInstance()
	{
		return (UIStore_Com_MonthCard)UIPackage.CreateObject("Store", "Store_Com_MonthCard");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		language = GetControllerAt(0);
		hasPurchase = GetControllerAt(1);
		country = GetControllerAt(2);
		loader_bg = (GGraph)GetChildAt(0);
		loader_Video = (GGraph)GetChildAt(1);
		txt_Time = (GTextField)GetChildAt(37);
		btn_Purchase = (UIStore_Button_PurchaseMonthCard)GetChildAt(39);
		btn_Detail = (GButton)GetChildAt(40);
		Cutin = GetTransitionAt(0);
	}
}
