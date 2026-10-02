using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class RechargeTipWindow : BaseWindow
{
	private BlurBgTextureController blurBgCtrl = new BlurBgTextureController();

	protected override bool isGeneralFadeIn => true;

	protected override bool isGeneralFadeOut => true;

	public RechargeTipWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIRechargeTipWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShowAsync()
	{
		if (!base.isShowing)
		{
			await blurBgCtrl.CreateBlurTex();
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIRechargeTipWindow uIRechargeTipWindow && uIRechargeTipWindow.mohu is UICom_PopUpWindow_MohuBg && uIRechargeTipWindow.bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom)
		{
			uIRechargeTipWindow.mohu.onClick.Add(base.Hide);
			uICom_PopUpWindow_Bottom.btn_Cancel.onClick.Add(base.Hide);
			uICom_PopUpWindow_Bottom.closeButton.onClick.Add(base.Hide);
			blurBgCtrl.OnShown(this);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIRechargeTipWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIRechargeTipWindow)
		{
			uIRechargeTipWindow.mohu.onClick.Remove(base.Hide);
			bottom.btn_Cancel.onClick.Remove(base.Hide);
			bottom.closeButton.onClick.Remove(base.Hide);
			blurBgCtrl.OnHide();
		}
	}

	protected override void OnKeyDown(EventContext context)
	{
		if (base.contentPane is UIRechargeTipWindow uIRechargeTipWindow && base.isShowing && uIRechargeTipWindow.bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom && context.inputEvent.keyCode == KeyCode.Escape)
		{
			uICom_PopUpWindow_Bottom.btn_Cancel.FireClick(downEffect: true);
			uICom_PopUpWindow_Bottom.closeButton.FireClick(downEffect: true);
			uICom_PopUpWindow_Bottom.btn_Sure_Only.FireClick(downEffect: true);
			Hide();
		}
	}

	public async UniTask TryExchangeItem(int currencyId, int needCount, ShopTabType shopTabType, int goodsId, int targetCount, ItemInfoConfigure goodsItem, int msgId = 1071, int titleId = 1074)
	{
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		UIRechargeTipWindow win = gComponent as UIRechargeTipWindow;
		if (win == null)
		{
			return;
		}
		GLabel bottom = win.bottom;
		UICom_PopUpWindow_Bottom winBottom = bottom as UICom_PopUpWindow_Bottom;
		if (winBottom == null)
		{
			return;
		}
		win.type.selectedIndex = 0;
		int totalToken = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(currencyId);
		ItemInfoConfigure itemInfoConfigure = currencyId.GetItemInfoConfigure();
		RefreshTokenInfo(win.com_ExchangeItem.com_LeftCurrency, itemInfoConfigure.Icon, totalToken);
		RefreshTokenInfo(win.com_ExchangeItem.com_RightCurrency, itemInfoConfigure.Icon, totalToken - needCount);
		string arg = $"[color=#FF0000]{needCount}[/color]{itemInfoConfigure.NameID.GetLocal(UIStringType.Item)}";
		string text = $"[color=#FF0000]{targetCount}[/color]";
		if (goodsItem != null)
		{
			text = text + "<img src='" + goodsItem.ShowIcon + "' width='40' height='40'/>" + goodsItem.NameID.GetLocal(UIStringType.Item);
		}
		win.com_ExchangeItem.txt_TotalInfo.text = string.Format(msgId.GetLocal(UIStringType.Message), arg, text);
		if (goodsItem != null)
		{
			winBottom.text = string.Format(titleId.GetLocal(UIStringType.Message), goodsItem.NameID.GetLocal(UIStringType.Item));
		}
		else
		{
			winBottom.text = titleId.GetLocal(UIStringType.Message);
		}
		winBottom.btn_Sure.visible = totalToken >= needCount;
		win.com_ExchangeItem.btn_Purchase.visible = totalToken < needCount;
		win.com_ExchangeItem.btn_Purchase.onClick.Set((EventCallback0)delegate
		{
			win.com_ExchangeItem.btn_Purchase.onClick.Retain();
			if (currencyId == GameSettings.SPECIAL_ITEM_STARDISC_FREE)
			{
				TryExchangeToken(currencyId, needCount - totalToken, winBottom.text).Forget();
			}
			else if (currencyId == GameSettings.SPECIAL_ITEM_STARDISC_PAY)
			{
				GoRecharge();
			}
			win.com_ExchangeItem.btn_Purchase.onClick.Release();
		});
		winBottom.btn_Sure.onClick.Release();
		if (shopTabType == ShopTabType.None)
		{
			return;
		}
		winBottom.btn_Sure.onClick.Set((EventCallback0)delegate
		{
			winBottom.btn_Sure.onClick.Retain();
			int chainGiftId = 0;
			if (shopTabType == ShopTabType.GiftPackage)
			{
				ExchangeGoods exchangeGoodsByShopTypeAndGoodsID = SimpleSingletonProvider<GameLogicManager>.inst.store.GetExchangeGoodsByShopTypeAndGoodsID((int)shopTabType, goodsId);
				if (exchangeGoodsByShopTypeAndGoodsID != null && exchangeGoodsByShopTypeAndGoodsID.goodsConfig.Param > 0)
				{
					chainGiftId = exchangeGoodsByShopTypeAndGoodsID.goodsConfig.Param;
				}
			}
			SimpleSingletonProvider<GameLogicManager>.inst.store.RequestMallBuy((int)shopTabType, goodsId, targetCount, chainGiftId).OnFinishedOnly.AddOnce(delegate
			{
				if (SimpleSingletonProvider<UIManager>.inst.purchase.isShowing)
				{
					SimpleSingletonProvider<UIManager>.inst.purchase.Hide();
				}
				Hide();
				winBottom.btn_Sure.onClick.Release();
			});
		});
	}

	private void RefreshTokenInfo(GLabel com_Currency, string tokenUrl, int count)
	{
		com_Currency.icon = tokenUrl;
		string arg = ((count < 0) ? "[color=#FF0000]" : "[color=#FFFFFF]");
		com_Currency.title = $"{arg}{count}[/color]";
	}

	private void GoRecharge()
	{
		SimpleSingletonProvider<UIManager>.inst.TryHideWindows();
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is StorePanel storePanel)
		{
			storePanel.Show(ShopTabType.Recharge);
			storePanel.Refresh();
		}
		else
		{
			SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Store, ShopTabType.Recharge).Forget();
		}
		Hide();
	}

	public async UniTask ShowBattlePassLevelInfo(int currencyId, int needToken, int deltaLV, int targetLv)
	{
		await TryExchangeItem(currencyId, needToken, ShopTabType.None, 0, targetLv, null, 1076, 1077);
		if (!(base.contentPane is UIRechargeTipWindow { bottom: var bottom }))
		{
			return;
		}
		UICom_PopUpWindow_Bottom winBottom = bottom as UICom_PopUpWindow_Bottom;
		if (winBottom == null)
		{
			return;
		}
		winBottom.btn_Sure.onClick.Set((EventCallback0)delegate
		{
			winBottom.btn_Sure.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.battlePass.RequestBattlePassUpLvC2S(deltaLV).OnFinishedOnly.AddOnce(delegate
			{
				if (SimpleSingletonProvider<UIManager>.inst.purchase.isShowing)
				{
					SimpleSingletonProvider<UIManager>.inst.purchase.Hide();
				}
				Hide();
				winBottom.btn_Sure.onClick.Release();
			});
		});
	}

	public async UniTask TryExchangeToken(int currencyId, int needCount, string title = null)
	{
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		UIRechargeTipWindow win = gComponent as UIRechargeTipWindow;
		if (win == null)
		{
			return;
		}
		GLabel bottom = win.bottom;
		UICom_PopUpWindow_Bottom winBottom = bottom as UICom_PopUpWindow_Bottom;
		if (winBottom == null)
		{
			return;
		}
		win.type.selectedIndex = 1;
		ItemInfoConfigure PAY_CURRENCY_ITEM = GameSettings.SPECIAL_ITEM_STARDISC_PAY.GetItemInfoConfigure();
		int PAY_CURRENCY_COUNT = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(GameSettings.SPECIAL_ITEM_STARDISC_PAY);
		ItemInfoConfigure currencyItem = currencyId.GetItemInfoConfigure();
		int MAX_PAY_CURRENCY_COUNT = Mathf.Max(1, PAY_CURRENCY_COUNT);
		int MIN_PAY_CURRENCY_COUNT = 1;
		int b = MAX_PAY_CURRENCY_COUNT * GameSettings.CURRENCY_EXCHANGE_RATE;
		int curExchangeResultCount = Mathf.Min(needCount, b);
		int CURRENT_COST_PAY_TOKEN_COUNT = Mathf.FloorToInt((float)curExchangeResultCount * 1f / (float)GameSettings.CURRENCY_EXCHANGE_RATE);
		win.com_ExchangeCurrency.txt_Tip.text = string.Format(1071.GetLocal(UIStringType.Message), $"[color=#FF3300]{CURRENT_COST_PAY_TOKEN_COUNT}[/color]{PAY_CURRENCY_ITEM.NameID.GetLocal(UIStringType.Item)}", $"[color=#FF3300]{curExchangeResultCount}[/color]{currencyItem.NameID.GetLocal(UIStringType.Item)}");
		winBottom.text = (string.IsNullOrEmpty(title) ? 1003.GetLocal(UIStringType.GUI) : title);
		RefreshTokenInfo(win.com_ExchangeCurrency.com_LeftCurrency, PAY_CURRENCY_ITEM.Icon, CURRENT_COST_PAY_TOKEN_COUNT);
		RefreshTokenInfo(win.com_ExchangeCurrency.com_RightCurrency, currencyItem.Icon, curExchangeResultCount);
		win.com_ExchangeCurrency.slider_Count.max = MAX_PAY_CURRENCY_COUNT;
		win.com_ExchangeCurrency.slider_Count.min = 0.0;
		win.com_ExchangeCurrency.slider_Count.txt_Max.text = MAX_PAY_CURRENCY_COUNT.ToString();
		win.com_ExchangeCurrency.slider_Count.value = CURRENT_COST_PAY_TOKEN_COUNT;
		win.com_ExchangeCurrency.slider_Count.onChanged.Set((EventCallback0)delegate
		{
			CURRENT_COST_PAY_TOKEN_COUNT = (int)win.com_ExchangeCurrency.slider_Count.value;
			if (CURRENT_COST_PAY_TOKEN_COUNT < MIN_PAY_CURRENCY_COUNT)
			{
				win.com_ExchangeCurrency.slider_Count.value = (CURRENT_COST_PAY_TOKEN_COUNT = MIN_PAY_CURRENCY_COUNT);
			}
			curExchangeResultCount = CURRENT_COST_PAY_TOKEN_COUNT * GameSettings.CURRENCY_EXCHANGE_RATE;
			win.com_ExchangeCurrency.txt_Tip.text = string.Format(1071.GetLocal(UIStringType.Message), $"[color=#FF3300]{CURRENT_COST_PAY_TOKEN_COUNT}[/color]{PAY_CURRENCY_ITEM.NameID.GetLocal(UIStringType.Item)}", $"[color=#FF3300]{curExchangeResultCount}[/color]{currencyItem.NameID.GetLocal(UIStringType.Item)}");
			win.com_ExchangeCurrency.com_LeftCurrency.title = CURRENT_COST_PAY_TOKEN_COUNT.ToString();
			win.com_ExchangeCurrency.com_RightCurrency.title = curExchangeResultCount.ToString();
			win.com_ExchangeCurrency.btn_Purchase.visible = CURRENT_COST_PAY_TOKEN_COUNT > PAY_CURRENCY_COUNT;
			winBottom.btn_Sure.visible = CURRENT_COST_PAY_TOKEN_COUNT <= PAY_CURRENCY_COUNT;
		});
		win.com_ExchangeCurrency.btn_Min.onClick.Set((EventCallback0)delegate
		{
			win.com_ExchangeCurrency.slider_Count.UpdateValueAndChange(MIN_PAY_CURRENCY_COUNT);
		});
		win.com_ExchangeCurrency.btn_Max.onClick.Set((EventCallback0)delegate
		{
			win.com_ExchangeCurrency.slider_Count.UpdateValueAndChange(MAX_PAY_CURRENCY_COUNT);
		});
		win.com_ExchangeCurrency.btn_DelCount.onClick.Set((EventCallback0)delegate
		{
			int num = (int)win.com_ExchangeCurrency.slider_Count.value;
			win.com_ExchangeCurrency.slider_Count.UpdateValueAndChange(Mathf.Max(MIN_PAY_CURRENCY_COUNT, num - 1));
		});
		win.com_ExchangeCurrency.btn_AddCount.onClick.Set((EventCallback0)delegate
		{
			int num = (int)win.com_ExchangeCurrency.slider_Count.value;
			win.com_ExchangeCurrency.slider_Count.UpdateValueAndChange(Mathf.Min(MAX_PAY_CURRENCY_COUNT, num + 1));
		});
		win.com_ExchangeCurrency.btn_Purchase.visible = CURRENT_COST_PAY_TOKEN_COUNT > PAY_CURRENCY_COUNT;
		winBottom.btn_Sure.visible = CURRENT_COST_PAY_TOKEN_COUNT <= PAY_CURRENCY_COUNT;
		win.com_ExchangeCurrency.btn_Purchase.onClick.Set((EventCallback0)delegate
		{
			win.com_ExchangeItem.btn_Purchase.onClick.Retain();
			GoRecharge();
			win.com_ExchangeItem.btn_Purchase.onClick.Release();
		});
		winBottom.btn_Sure.onClick.Release();
		winBottom.btn_Sure.onClick.Set((EventCallback0)delegate
		{
			winBottom.btn_Sure.onClick.Retain();
			int payTokenCount = (int)win.com_ExchangeCurrency.slider_Count.value;
			SimpleSingletonProvider<GameLogicManager>.inst.store.RequestTransferStarDiscC2S(payTokenCount).OnFinishedOnly.AddOnce(delegate
			{
				Hide();
				winBottom.btn_Sure.onClick.Release();
			});
		});
	}
}
