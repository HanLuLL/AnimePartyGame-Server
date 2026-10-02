using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class PurchaseWindow : BaseWindow
{
	private BaseGoodsData goodsData;

	private int currentSelectCount;

	private BlurBgTextureController blurBgCtrl = new BlurBgTextureController();

	private int minValue => SimpleSingletonProvider<GameLogicManager>.inst.battlePass.BattlePassData.LV + 1;

	private int maxValue => SimpleSingletonProvider<GameLogicManager>.inst.battlePass.BattlePassData.MaxLevel;

	private MapField<int, int> costItem => SimpleSingletonProvider<GameLogicManager>.inst.battlePass.BattlePassData.BattlePassInfo.CostPerLv;

	private int costItemId => costItem.ElementAt(0).Key;

	private int costItemCount => costItem.ElementAt(0).Value;

	protected override bool isGeneralFadeIn => true;

	protected override bool isGeneralFadeOut => true;

	private void AddEvent_BattlePassLevel()
	{
		if (base.contentPane is UIPurchaseWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIPurchaseWindow)
		{
			bottom.btn_Sure.onClick.Add(PurchaseLv);
			uIPurchaseWindow.btn_AddLv.onClick.Add(AddLV);
			uIPurchaseWindow.btn_DelLv.onClick.Add(DelLV);
			uIPurchaseWindow.slider_BattlePassLevel.onChanged.Add(ChangeLevel);
		}
	}

	private void RemoveEvent_BattlePassLevel()
	{
		if (base.contentPane is UIPurchaseWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIPurchaseWindow)
		{
			bottom.btn_Sure.onClick.Remove(PurchaseLv);
			uIPurchaseWindow.btn_AddLv.onClick.Remove(AddLV);
			uIPurchaseWindow.btn_DelLv.onClick.Remove(DelLV);
			uIPurchaseWindow.slider_BattlePassLevel.onChanged.Remove(ChangeLevel);
		}
	}

	public async UniTask ShowPurchaseBattlePassLevel()
	{
		await TryShowAsync();
		if (base.contentPane is UIPurchaseWindow uIPurchaseWindow)
		{
			uIPurchaseWindow.WinType.selectedIndex = 1;
			uIPurchaseWindow.slider_BattlePassLevel.value = minValue;
			uIPurchaseWindow.slider_BattlePassLevel.min = minValue;
			uIPurchaseWindow.slider_BattlePassLevel.max = maxValue;
			uIPurchaseWindow.slider_BattlePassLevel.txt_Min.text = minValue.ToString();
			uIPurchaseWindow.slider_BattlePassLevel.txt_Max.text = maxValue.ToString();
			uIPurchaseWindow.slider_BattlePassLevel.touchable = minValue != maxValue;
			uIPurchaseWindow.loader_Token.url = costItemId.GetItemInfoConfigure().ShowIcon;
			uIPurchaseWindow.slider_BattlePassLevel.onChanged.Call();
		}
	}

	private void PurchaseLv(EventContext context)
	{
		if (!(base.contentPane is UIPurchaseWindow { bottom: var bottom } uIPurchaseWindow))
		{
			return;
		}
		UICom_PopUpWindow_Bottom winBottom = bottom as UICom_PopUpWindow_Bottom;
		if (winBottom == null || uIPurchaseWindow.WinType.selectedIndex != 1)
		{
			return;
		}
		int num = (int)uIPurchaseWindow.slider_BattlePassLevel.value - minValue + 1;
		int num2 = num * costItemCount;
		int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(costItemId);
		if (itemCount < num2)
		{
			if (costItemId == GameSettings.SPECIAL_ITEM_STARDISC_FREE || costItemId == GameSettings.SPECIAL_ITEM_STARDISC_PAY)
			{
				SimpleSingletonProvider<UIManager>.inst.rechargeTip.ShowBattlePassLevelInfo(costItemId, num2, num, (int)uIPurchaseWindow.slider_BattlePassLevel.value).Forget();
				return;
			}
			if (itemCount < num2)
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1015);
				return;
			}
		}
		winBottom.btn_Sure.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.battlePass.RequestBattlePassUpLvC2S(num).OnFinishedOnly.AddOnce(delegate
		{
			Hide();
			winBottom.btn_Sure.onClick.Release();
		});
	}

	private void AddLV()
	{
		if (base.contentPane is UIPurchaseWindow uIPurchaseWindow)
		{
			uIPurchaseWindow.btn_AddLv.onClick.Retain();
			uIPurchaseWindow.slider_BattlePassLevel.value = Mathf.Min(maxValue, (int)uIPurchaseWindow.slider_BattlePassLevel.value + 1);
			uIPurchaseWindow.slider_BattlePassLevel.onChanged.Call();
			uIPurchaseWindow.btn_AddLv.onClick.Release();
		}
	}

	private void DelLV()
	{
		if (base.contentPane is UIPurchaseWindow uIPurchaseWindow)
		{
			uIPurchaseWindow.btn_DelLv.onClick.Retain();
			uIPurchaseWindow.slider_BattlePassLevel.value = Mathf.Max(minValue, (int)uIPurchaseWindow.slider_BattlePassLevel.value - 1);
			uIPurchaseWindow.slider_BattlePassLevel.onChanged.Call();
			uIPurchaseWindow.btn_DelLv.onClick.Release();
		}
	}

	private void ChangeLevel(EventContext context)
	{
		if (base.contentPane is UIPurchaseWindow uIPurchaseWindow)
		{
			double num = uIPurchaseWindow.slider_BattlePassLevel.value - (double)minValue + 1.0;
			int num2 = costItemCount * (int)num;
			int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(costItemId);
			if (num2 <= itemCount)
			{
				uIPurchaseWindow.txt_TokenCount.text = "[color=#FFFFFF]" + num2 + "[/color]";
			}
			else
			{
				uIPurchaseWindow.txt_TokenCount.text = "[color=#FF0000]" + num2 + "[/color]";
			}
		}
	}

	private void AddEvent_Goods()
	{
		if (base.contentPane is UIPurchaseWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIPurchaseWindow)
		{
			uIPurchaseWindow.btn_addNum.onClick.Add(OnAddOneGoods);
			uIPurchaseWindow.btn_delNum.onClick.Add(OnDelOneGoods);
			uIPurchaseWindow.btn_Max.onClick.Add(OnSetMaxSelectGoods);
			uIPurchaseWindow.btn_Min.onClick.Add(OnSetMinSelectGoods);
			bottom.btn_Sure.onClick.Add(OnClickBuyBtn);
			SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.bagMapChanged.AddListener(RefreshToken);
		}
	}

	private void RemoveEvent_Goods()
	{
		if (base.contentPane is UIPurchaseWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIPurchaseWindow)
		{
			uIPurchaseWindow.btn_addNum.onClick.Remove(OnAddOneGoods);
			uIPurchaseWindow.btn_delNum.onClick.Remove(OnDelOneGoods);
			uIPurchaseWindow.btn_Max.onClick.Remove(OnSetMaxSelectGoods);
			uIPurchaseWindow.btn_Min.onClick.Remove(OnSetMinSelectGoods);
			bottom.btn_Sure.onClick.Remove(OnClickBuyBtn);
			SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.bagMapChanged.RemoveListener(RefreshToken);
		}
	}

	private void RefreshToken()
	{
		if (goodsData != null && goodsData.currencyID != 0)
		{
			RefreshGoodsSelectGroup(currentSelectCount);
		}
	}

	public async UniTask ShowPurchaseGoods(BaseGoodsData _goodsData)
	{
		await TryShowAsync();
		if (!(base.contentPane is UIPurchaseWindow uIPurchaseWindow))
		{
			return;
		}
		uIPurchaseWindow.WinType.selectedIndex = 0;
		goodsData = _goodsData;
		SimpleSingletonProvider<WebServerManager>.inst.PostGoodsRecord(goodsData.goodsId, goodsData.shopTabType, LogToServerType.GOODS_DETAIL);
		RefreshGoodsSelectGroup(1);
		uIPurchaseWindow.txt_itemName.text = goodsData.GetName();
		uIPurchaseWindow.txt_itemDes.text = goodsData.itemConfig.DescriptionID.GetLocal(UIStringType.Item);
		uIPurchaseWindow.txt_itemNum.SetVar("curNum", SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(goodsData.itemConfig.Id).ToString()).FlushVars();
		uIPurchaseWindow.multiState.selectedIndex = ((goodsData.RemainingNum() <= 1 && goodsData.LimitNum() != 0) ? 1 : 0);
		((UICom_LitItem)uIPurchaseWindow.btn_itemLoader).loader_Icon.url = goodsData.itemConfig.ShowIcon;
		((UICom_LitItem)uIPurchaseWindow.btn_itemLoader).qualityType.selectedIndex = (int)goodsData.itemConfig.QualityType;
		((UICom_LitItem)uIPurchaseWindow.btn_itemLoader).txt_itemNum.text = goodsData.SingleNum().ToString();
		uIPurchaseWindow.btn_Max.grayed = GetCurMaxPurchaseGoodsCount() <= 1;
		if (goodsData.currencyID > 0)
		{
			uIPurchaseWindow.tokenType.selectedIndex = 0;
			GLoader loader_Currency = uIPurchaseWindow.loader_Currency;
			string url = (uIPurchaseWindow.loader_Currency_Total.url = goodsData.currencyID.GetItemInfoConfigure().ShowIcon);
			loader_Currency.url = url;
			GTextField txt_TotalPrice = uIPurchaseWindow.txt_TotalPrice;
			url = (uIPurchaseWindow.txt_UnitPrice.text = goodsData.SalePrice.ToString());
			txt_TotalPrice.text = url;
			uIPurchaseWindow.txt_OriginalPrice.text = goodsData.GetOriginalPrice().ToString();
		}
		else
		{
			RechargeGoods rechargeGoods = goodsData.child<RechargeGoods>();
			if (rechargeGoods == null)
			{
				return;
			}
			uIPurchaseWindow.tokenType.selectedIndex = 1;
			GRichTextField txt_CashSymbol = uIPurchaseWindow.txt_CashSymbol;
			string url = (uIPurchaseWindow.txt_CashSymbol_Total.text = 1026.GetLocal(UIStringType.Message));
			txt_CashSymbol.text = url;
			GTextField txt_TotalPrice2 = uIPurchaseWindow.txt_TotalPrice;
			url = (uIPurchaseWindow.txt_UnitPrice.text = rechargeGoods.GetDiscountPriceText());
			txt_TotalPrice2.text = url;
			uIPurchaseWindow.txt_OriginalPrice.text = rechargeGoods.GetOriginalPriceText();
			GRichTextField txt_CashSymbol2 = uIPurchaseWindow.txt_CashSymbol;
			url = (uIPurchaseWindow.txt_CashSymbol_Total.text = uIPurchaseWindow.txt_CashSymbol.CURRENCY_SYMBOLS);
			txt_CashSymbol2.text = url;
		}
		uIPurchaseWindow.isDiscount.selectedIndex = ((goodsData.SalePrice != goodsData.GetOriginalPrice()) ? 1 : 0);
		if (goodsData.SalePrice != goodsData.GetOriginalPrice())
		{
			double num = 100.0 - Math.Round((double)goodsData.SalePrice * 100.0 / (double)goodsData.GetOriginalPrice());
			uIPurchaseWindow.txt_DiscountPercent.SetVar("discountPercent", num.ToString("f0")).FlushVars();
		}
		if (goodsData.itemConfig.ItemType == ItemType.Chest)
		{
			RefreshChestItemInfo(goodsData.itemConfig.Id.GetChestInfoConfigure());
			uIPurchaseWindow.itemType.selectedIndex = 1;
		}
		else
		{
			uIPurchaseWindow.itemType.selectedIndex = 0;
		}
	}

	private void RefreshChestItemInfo(ChestInfoConfigure chestConfig)
	{
		if (!(base.contentPane is UIPurchaseWindow uIPurchaseWindow))
		{
			return;
		}
		uIPurchaseWindow.com_Explain.txt_ContentType.text = chestConfig.ContentType.GetLocal(UIStringType.Chest);
		if (chestConfig.IsOptional)
		{
			MapField<int, int> OptionalReward = chestConfig.OptionalReward;
			uIPurchaseWindow.list_ChestItems.itemRenderer = delegate(int index, GObject uiChestItem)
			{
				if (uiChestItem is UIPurchase_Com_ChestItem uIPurchase_Com_ChestItem)
				{
					KeyValuePair<int, int> keyValuePair = OptionalReward.ElementAt(index);
					RefreshItem((UICom_Item)uIPurchase_Com_ChestItem.btn_Item, keyValuePair.Key.GetItemInfoConfigure(), keyValuePair.Value);
				}
			};
			uIPurchaseWindow.list_ChestItems.numItems = OptionalReward.Count;
			return;
		}
		RepeatedField<int> randomReward = chestConfig.RandomReward;
		List<ChestRandomRewardConfigureItem> _list = new List<ChestRandomRewardConfigureItem>();
		foreach (int item in randomReward)
		{
			_list.AddRange(StaticConfigure.Chest.RandomRewardDict[item].ChestRandomRewardConfigureItems);
		}
		uIPurchaseWindow.list_ChestItems.itemRenderer = delegate(int index, GObject uiChestItem)
		{
			if (uiChestItem is UIPurchase_Com_ChestItem uIPurchase_Com_ChestItem)
			{
				ChestRandomRewardConfigureItem chestRandomRewardConfigureItem = _list[index];
				RefreshItem((UICom_Item)uIPurchase_Com_ChestItem.btn_Item, chestRandomRewardConfigureItem.ItemID.GetItemInfoConfigure(), chestRandomRewardConfigureItem.MinCount);
			}
		};
		uIPurchaseWindow.list_ChestItems.numItems = _list.Count;
	}

	private void OnAddOneGoods()
	{
		if (base.contentPane is UIPurchaseWindow uIPurchaseWindow)
		{
			uIPurchaseWindow.btn_addNum.onClick.Retain();
			if (goodsData.LimitNum() == 0)
			{
				RefreshGoodsSelectGroup(currentSelectCount + 1);
			}
			else
			{
				RefreshGoodsSelectGroup(Mathf.Min(currentSelectCount + 1, goodsData.RemainingNum()));
			}
			uIPurchaseWindow.btn_addNum.onClick.Release();
		}
	}

	private void OnDelOneGoods()
	{
		if (base.contentPane is UIPurchaseWindow uIPurchaseWindow)
		{
			uIPurchaseWindow.btn_delNum.onClick.Retain();
			RefreshGoodsSelectGroup(Mathf.Max(currentSelectCount - 1, 1));
			uIPurchaseWindow.btn_delNum.onClick.Release();
		}
	}

	private void OnSetMaxSelectGoods()
	{
		if (base.contentPane is UIPurchaseWindow uIPurchaseWindow)
		{
			uIPurchaseWindow.btn_Max.onClick.Retain();
			RefreshGoodsSelectGroup(Mathf.Max(1, GetCurMaxPurchaseGoodsCount()));
			uIPurchaseWindow.btn_Max.onClick.Release();
		}
	}

	private void OnSetMinSelectGoods()
	{
		if (base.contentPane is UIPurchaseWindow uIPurchaseWindow)
		{
			uIPurchaseWindow.btn_Min.onClick.Retain();
			RefreshGoodsSelectGroup(1);
			uIPurchaseWindow.btn_Min.onClick.Release();
		}
	}

	private void RefreshGoodsSelectGroup(int curNum)
	{
		if (base.contentPane is UIPurchaseWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIPurchaseWindow)
		{
			currentSelectCount = Mathf.Min(curNum, 9999);
			uIPurchaseWindow.txt_itemSelectNum.text = currentSelectCount.ToString();
			float totalSpending = goodsData.GetTotalSpending(currentSelectCount);
			uIPurchaseWindow.txt_TotalPrice.text = totalSpending.ToString("0.##");
			int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(goodsData.currencyID);
			bottom.btn_Sure.touchable = currentSelectCount > 0;
			uIPurchaseWindow.txt_TotalPrice.color = Color.black;
			if (goodsData.currencyID > 0)
			{
				uIPurchaseWindow.txt_TotalPrice.color = (((float)itemCount < totalSpending) ? Color.red : Color.black);
			}
		}
	}

	private int GetCurMaxPurchaseGoodsCount()
	{
		if (goodsData.SalePrice == 0)
		{
			return goodsData.RemainingNum();
		}
		int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(goodsData.currencyID);
		if (goodsData.LimitNum() == 0)
		{
			return Mathf.Min(itemCount / goodsData.SalePrice, 99);
		}
		return Mathf.Min(itemCount / goodsData.SalePrice, goodsData.RemainingNum());
	}

	private void OnClickBuyBtn()
	{
		if (base.contentPane is UIPurchaseWindow uIPurchaseWindow && uIPurchaseWindow.bottom is UICom_PopUpWindow_Bottom && uIPurchaseWindow.WinType.selectedIndex == 0)
		{
			if (uIPurchaseWindow.tokenType.selectedIndex == 0)
			{
				OnBuyGoodsByToken();
			}
			else if (uIPurchaseWindow.tokenType.selectedIndex == 1)
			{
				OnBuyItemByCash();
			}
		}
	}

	private void OnBuyGoodsByToken(bool againSure = false)
	{
		if (SimpleSingletonProvider<UIManager>.inst.guide.isShowing)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.guide.FinishBuyGoodsGuide();
		}
		if (!(base.contentPane is UIPurchaseWindow { bottom: var bottom }))
		{
			return;
		}
		UICom_PopUpWindow_Bottom winBottom = bottom as UICom_PopUpWindow_Bottom;
		if (winBottom == null || goodsData == null)
		{
			return;
		}
		ShopTabType shopTabType = goodsData.shopTabType;
		if ((shopTabType == ShopTabType.Expression || shopTabType == ShopTabType.Skin) && !againSure)
		{
			if (CheckHasExpressionGift())
			{
				SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(1120, delegate
				{
					HideImmediately();
					winBottom.btn_Sure.onClick.Release();
					ExpressionJumpToGift();
				}, delegate
				{
					OnBuyGoodsByToken(againSure: true);
				}).Forget();
				return;
			}
			if (CheckHasSkinSell())
			{
				SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(1142, delegate
				{
					HideImmediately();
					winBottom.btn_Sure.onClick.Release();
					SkinJumpToSkinSell();
				}, delegate
				{
					OnBuyGoodsByToken(againSure: true);
				}).Forget();
				return;
			}
		}
		int num = goodsData.SalePrice * currentSelectCount;
		int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(goodsData.currencyID);
		if (goodsData.currencyID == GameSettings.SPECIAL_ITEM_STARDISC_FREE || goodsData.currencyID == GameSettings.SPECIAL_ITEM_STARDISC_PAY)
		{
			SimpleSingletonProvider<UIManager>.inst.rechargeTip.TryExchangeItem(goodsData.currencyID, num, goodsData.shopTabType, goodsData.goodsId, currentSelectCount, goodsData.itemConfig).Forget();
			return;
		}
		if (itemCount < num && itemCount < num)
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1015);
			return;
		}
		winBottom.btn_Sure.onClick.Retain();
		int chainGiftId = 0;
		if (goodsData.shopTabType == ShopTabType.GiftPackage)
		{
			ExchangeGoods exchangeGoodsByShopTypeAndGoodsID = SimpleSingletonProvider<GameLogicManager>.inst.store.GetExchangeGoodsByShopTypeAndGoodsID((int)goodsData.shopTabType, goodsData.goodsId);
			if (exchangeGoodsByShopTypeAndGoodsID != null && exchangeGoodsByShopTypeAndGoodsID.goodsConfig.Param > 0)
			{
				chainGiftId = exchangeGoodsByShopTypeAndGoodsID.goodsConfig.Param;
			}
		}
		SimpleSingletonProvider<GameLogicManager>.inst.store.RequestMallBuy((int)goodsData.shopTabType, goodsData.goodsId, currentSelectCount, chainGiftId).OnFinishedOnly.AddOnce(delegate
		{
			HideImmediately();
			winBottom.btn_Sure.onClick.Release();
		});
	}

	private async void OnBuyItemByCash()
	{
		if (base.contentPane is UIPurchaseWindow { bottom: var bottom } && bottom is UICom_PopUpWindow_Bottom winBottom)
		{
			winBottom.btn_Sure.onClick.Retain();
			await SimpleSingletonProvider<GameLogicManager>.inst.store.RequestCreateOrder(goodsData, currentSelectCount);
			winBottom.btn_Sure.onClick.Release();
		}
	}

	private void RefreshItem(UICom_Item item, ItemInfoConfigure configure, int count)
	{
		item.loader_Icon.url = configure.ShowIcon;
		item.qualityType.selectedIndex = (int)configure.QualityType;
		item.txt_itemNum.text = count.ToString();
		item.onClick.Set((EventCallback0)delegate
		{
			item.onClick.Retain();
			SimpleSingletonProvider<UIManager>.inst.propDetail.ShowByWin(configure.Id, count, UIWindowType.Purchase);
			item.onClick.Release();
		});
	}

	public void GuideBuyGoods()
	{
		if (base.isShowing && base.contentPane is UIPurchaseWindow { bottom: UICom_PopUpWindow_Bottom bottom })
		{
			Vector2 pt = bottom.btn_Sure.LocalToGlobal(Vector2.zero);
			pt = GRoot.inst.GlobalToLocal(pt);
			SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMask(pt, bottom.btn_Sure.width, bottom.btn_Sure.height, _needTransparentMask: false, isRect: true);
		}
	}

	private bool CheckHasExpressionGift()
	{
		if (goodsData == null || goodsData.itemConfig == null || goodsData.shopTabType != ShopTabType.Expression)
		{
			return false;
		}
		int giftGoodsByGoods = GetGiftGoodsByGoods(goodsData);
		if (giftGoodsByGoods == 0)
		{
			return false;
		}
		ExchangeGoods exchangeGoodsByShopTypeAndGoodsID = SimpleSingletonProvider<GameLogicManager>.inst.store.GetExchangeGoodsByShopTypeAndGoodsID(9, giftGoodsByGoods);
		if (exchangeGoodsByShopTypeAndGoodsID == null)
		{
			return false;
		}
		return TimeHelper.ValidityTime(exchangeGoodsByShopTypeAndGoodsID.BeginTime, exchangeGoodsByShopTypeAndGoodsID.EndTime);
	}

	private bool CheckHasSkinSell()
	{
		if (goodsData == null || goodsData.itemConfig == null || goodsData.shopTabType != ShopTabType.Skin)
		{
			return false;
		}
		SkinSellData skinComboInfo = SimpleSingletonProvider<GameLogicManager>.inst.store.GetSkinComboInfo();
		if (skinComboInfo == null)
		{
			return false;
		}
		int subMeterID = goodsData.itemConfig.SubMeterID;
		RepeatedField<SkinSellInfoConfigureItem> skinSellInfoConfigureItems = skinComboInfo.skinSellConfig.SkinSellInfoConfigureItems;
		if (skinSellInfoConfigureItems == null || skinSellInfoConfigureItems.Count == 0)
		{
			return false;
		}
		foreach (SkinSellInfoConfigureItem item in skinSellInfoConfigureItems)
		{
			if (item.Id == subMeterID)
			{
				return true;
			}
		}
		return false;
	}

	private void ExpressionJumpToGift()
	{
		if (goodsData != null && goodsData.itemConfig != null && goodsData.shopTabType == ShopTabType.Expression)
		{
			int giftGoodsByGoods = GetGiftGoodsByGoods(goodsData);
			if (giftGoodsByGoods != 0)
			{
				SimpleSingletonProvider<UIManager>.inst.GoTargetPanel(UIPanelType.Store, new RepeatedField<int> { 9, giftGoodsByGoods }, WayType.GiftPackageStore).Forget();
			}
		}
	}

	private void SkinJumpToSkinSell()
	{
		if (goodsData != null && goodsData.itemConfig != null && goodsData.shopTabType == ShopTabType.Skin)
		{
			SimpleSingletonProvider<UIManager>.inst.GoWindow(UIWindowType.SkinSell, null, WayType.None).Forget();
		}
	}

	private int GetGiftGoodsByGoods(BaseGoodsData goods)
	{
		if (goods == null || goods.itemConfig == null)
		{
			return 0;
		}
		if (!StaticConfigure.ExchangeStore.GoodsDict.TryGetValue(9, out var value))
		{
			return 0;
		}
		int num = 0;
		foreach (ExchangeStoreGoodsConfigureItem exchangeStoreGoodsConfigureItem in value.ExchangeStoreGoodsConfigureItems)
		{
			ItemInfoConfigure itemInfoConfigure = exchangeStoreGoodsConfigureItem.ItemID.GetItemInfoConfigure();
			if (itemInfoConfigure == null || itemInfoConfigure.ItemType != ItemType.Chest || itemInfoConfigure.SubMeterID == 0)
			{
				continue;
			}
			ChestInfoConfigure chestInfoConfigure = itemInfoConfigure.SubMeterID.GetChestInfoConfigure();
			if (chestInfoConfigure == null || chestInfoConfigure.RandomReward == null || chestInfoConfigure.RandomReward.Count == 0)
			{
				continue;
			}
			foreach (int item in chestInfoConfigure.RandomReward)
			{
				if (!StaticConfigure.Chest.RandomRewardDict.TryGetValue(item, out var value2))
				{
					continue;
				}
				foreach (ChestRandomRewardConfigureItem chestRandomRewardConfigureItem in value2.ChestRandomRewardConfigureItems)
				{
					if (chestRandomRewardConfigureItem.ItemID == goodsData.itemConfig.Id)
					{
						num = exchangeStoreGoodsConfigureItem.GoodsID;
						break;
					}
				}
				if (num != 0)
				{
					break;
				}
			}
			if (num != 0)
			{
				break;
			}
		}
		return num;
	}

	public PurchaseWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIPurchaseWindow.CreateInstance();
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
		if (base.contentPane is UIPurchaseWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIPurchaseWindow && uIPurchaseWindow.mohu is UICom_PopUpWindow_MohuBg)
		{
			bottom.btn_Cancel.onClick.Add(base.Hide);
			bottom.closeButton.onClick.Add(base.Hide);
			uIPurchaseWindow.mohu.onClick.Add(base.Hide);
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.LoseFocus();
			AddEvent_Goods();
			AddEvent_BattlePassLevel();
			blurBgCtrl.OnShown(this);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIPurchaseWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIPurchaseWindow)
		{
			bottom.btn_Cancel.onClick.Remove(base.Hide);
			bottom.closeButton.onClick.Remove(base.Hide);
			uIPurchaseWindow.mohu.onClick.Remove(base.Hide);
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.ResumeFocus();
			RemoveEvent_Goods();
			RemoveEvent_BattlePassLevel();
			blurBgCtrl.OnHide();
		}
	}

	protected override void OnKeyDown(EventContext context)
	{
		if (!SimpleSingletonProvider<UIManager>.inst.propDetail.isShowing && base.contentPane is UIPurchaseWindow uIPurchaseWindow && base.isShowing && uIPurchaseWindow.bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom && context.inputEvent.keyCode == KeyCode.Escape)
		{
			uICom_PopUpWindow_Bottom.btn_Cancel.FireClick(downEffect: true);
			uICom_PopUpWindow_Bottom.closeButton.FireClick(downEffect: true);
			HideImmediately();
		}
	}
}
