using System.Collections.Generic;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class DiscountWindow : BaseWindow
{
	public DiscountWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIDiscountWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShowAsync()
	{
		Show();
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIDiscountWindow uIDiscountWindow)
		{
			uIDiscountWindow.mohu.onClick.Add(base.Hide);
			uIDiscountWindow.btn_Return.onClick.Add(base.Hide);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIDiscountWindow uIDiscountWindow)
		{
			uIDiscountWindow.mohu.onClick.Remove(base.Hide);
			uIDiscountWindow.btn_Return.onClick.Remove(base.Hide);
		}
	}

	public async UniTask ShowEXSkinDiscount(BaseGoodsData goodsData, List<int> CouponsIds)
	{
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		UIDiscountWindow win = gComponent as UIDiscountWindow;
		if (win == null)
		{
			return;
		}
		win.txt_Price.visible = false;
		int selectedCoupons = 0;
		float curPrice = goodsData.SalePrice;
		GLabel bottom = win.bottom;
		UICom_PopUpWindow_Bottom winBottom = bottom as UICom_PopUpWindow_Bottom;
		if (winBottom == null)
		{
			return;
		}
		winBottom.closeButton.onClick.Set((EventCallback0)delegate
		{
			winBottom.closeButton.onClick.Retain();
			Hide();
			winBottom.closeButton.onClick.Release();
		});
		win.list_Coupons.itemRenderer = delegate(int index, GObject item)
		{
			if (item is UIDiscount_Button_Coupons uIDiscount_Button_Coupons)
			{
				ItemInfoConfigure itemInfoConfigure = CouponsIds[index].GetItemInfoConfigure();
				uIDiscount_Button_Coupons.loader_Icon.url = itemInfoConfigure.ShowIcon;
				uIDiscount_Button_Coupons.txt_Title.text = itemInfoConfigure.NameID.GetLocal(UIStringType.Item);
				uIDiscount_Button_Coupons.txt_Time.text = TimeHelper.RefreshTimeText(1033, 1034, MonoSingletonProvider<NetManager>.inst.ServerTime, itemInfoConfigure.EndDateTime.ToDateTime());
				int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(itemInfoConfigure.Id);
				uIDiscount_Button_Coupons.txt_Count.SetVar("count", itemCount.ToString()).FlushVars();
				uIDiscount_Button_Coupons.selected = false;
			}
		};
		win.list_Coupons.onClickItem.Set(delegate(EventContext context)
		{
			if (context.data is GButton gButton && win.list_Coupons.selectedIndex >= 0 && win.list_Coupons.selectedIndex < CouponsIds.Count)
			{
				int num = CouponsIds[win.list_Coupons.selectedIndex];
				if (selectedCoupons != num)
				{
					selectedCoupons = num;
					int discount = num.GetCouponsInfoConfigure().Discount;
					curPrice = Mathf.Floor(Mathf.Max(1f, (float)(goodsData.SalePrice * discount) * 0.01f));
					win.txt_Price.SetVar("token", "<img src='UT_Item_Currency_8' width='50' height='50'/>").FlushVars();
					win.txt_Price.SetVar("curPrice", $"{curPrice}").SetVar("Difference", $"{(float)goodsData.SalePrice - curPrice}").FlushVars();
					win.txt_Price.visible = true;
				}
				else
				{
					curPrice = goodsData.SalePrice;
					selectedCoupons = 0;
					win.txt_Price.visible = false;
					gButton.selected = false;
				}
			}
		});
		win.list_Coupons.numItems = CouponsIds.Count;
		win.btn_Cash.onClick.Set((EventCallback0)delegate
		{
			int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(8);
			if (curPrice > (float)itemCount)
			{
				SimpleSingletonProvider<UIManager>.inst.rechargeTip.TryExchangeItem(goodsData.currencyID, (int)curPrice, goodsData.shopTabType, goodsData.goodsId, 1, goodsData.itemConfig).Forget();
			}
			else
			{
				win.btn_Cash.onClick.Retain();
				if (selectedCoupons == 0)
				{
					SimpleSingletonProvider<UIManager>.inst.purchase.ShowPurchaseGoods(goodsData).Forget();
					Hide();
					win.btn_Cash.onClick.Release();
				}
				else
				{
					SimpleSingletonProvider<GameLogicManager>.inst.store.RequestMallBuy((int)goodsData.shopTabType, goodsData.goodsId, goodsData.LimitNum(), 0, selectedCoupons).OnFinishedOnly.AddOnce(delegate
					{
						Hide();
						win.btn_Cash.onClick.Release();
					});
				}
			}
		});
	}

	public async UniTask ShowSkinDiscount(BaseGoodsData goodsData)
	{
		int salePrice = goodsData.SalePrice;
		List<int> itemsByType = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemsByType(ItemType.Coupons);
		List<int> CouponsIds = new List<int>();
		if (itemsByType != null && itemsByType.Count > 0)
		{
			foreach (int item in itemsByType)
			{
				CouponsInfoConfigure couponsInfoConfigure = item.GetCouponsInfoConfigure();
				if (Mathf.Floor(Mathf.Max(1f, (float)(goodsData.GetOriginalPrice() * couponsInfoConfigure.Discount) * 0.01f)) < (float)salePrice)
				{
					CouponsIds.Add(item);
				}
			}
		}
		if (CouponsIds.Count == 0)
		{
			RequestCreateOrder(goodsData, 1, 0).Forget();
			return;
		}
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		UIDiscountWindow win = gComponent as UIDiscountWindow;
		if (win == null)
		{
			return;
		}
		win.txt_Price.visible = false;
		int selectedCoupons = 0;
		win.list_Coupons.itemRenderer = delegate(int index, GObject item)
		{
			if (item is UIDiscount_Button_Coupons uIDiscount_Button_Coupons)
			{
				ItemInfoConfigure itemInfoConfigure = CouponsIds[index].GetItemInfoConfigure();
				uIDiscount_Button_Coupons.loader_Icon.url = itemInfoConfigure.ShowIcon;
				uIDiscount_Button_Coupons.txt_Title.text = itemInfoConfigure.NameID.GetLocal(UIStringType.Item);
				uIDiscount_Button_Coupons.txt_Time.text = TimeHelper.RefreshTimeText(1033, 1034, MonoSingletonProvider<NetManager>.inst.ServerTime, itemInfoConfigure.EndDateTime.ToDateTime());
				int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(itemInfoConfigure.Id);
				uIDiscount_Button_Coupons.txt_Count.SetVar("count", itemCount.ToString()).FlushVars();
				uIDiscount_Button_Coupons.selected = false;
			}
		};
		win.list_Coupons.onClickItem.Set(delegate(EventContext context)
		{
			if (context.data is GButton gButton && win.list_Coupons.selectedIndex >= 0 && win.list_Coupons.selectedIndex < CouponsIds.Count)
			{
				int num = CouponsIds[win.list_Coupons.selectedIndex];
				if (selectedCoupons != num)
				{
					selectedCoupons = num;
					int discount = num.GetCouponsInfoConfigure().Discount;
					float num2 = Mathf.Floor(Mathf.Max(1f, (float)(goodsData.GetOriginalPrice() * discount) * 0.01f));
					win.txt_Price.SetVar("token", win.txt_Price.CURRENCY_SYMBOLS).SetVar("curPrice", goodsData.GetPriceText((int)num2)).SetVar("Difference", goodsData.GetPriceText((int)((float)goodsData.GetOriginalPrice() - num2)))
						.FlushVars();
					win.txt_Price.visible = true;
				}
				else
				{
					selectedCoupons = 0;
					win.txt_Price.visible = false;
					gButton.selected = false;
				}
			}
		});
		win.list_Coupons.numItems = CouponsIds.Count;
		win.btn_Cash.onClick.Set((EventCallback0)delegate
		{
			win.btn_Cash.onClick.Retain();
			RequestCreateOrder(goodsData, 1, selectedCoupons).Forget();
		});
	}

	private async UniTask RequestCreateOrder(BaseGoodsData goodsData, int count, int CouponsId)
	{
		if (CouponsId == 0)
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.store.RequestCreateOrder(goodsData, count, CouponsId);
		}
		else
		{
			ItemInfoConfigure itemInfoConfigure = CouponsId.GetItemInfoConfigure();
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(string.Format(1071.GetLocal(UIStringType.Message), itemInfoConfigure.NameID.GetLocal(UIStringType.Item), goodsData.GetName()), delegate
			{
				SimpleSingletonProvider<GameLogicManager>.inst.store.RequestCreateOrder(goodsData, count, CouponsId).Forget();
				Hide();
			}).Forget();
		}
		if (base.contentPane is UIDiscountWindow uIDiscountWindow)
		{
			uIDiscountWindow.btn_Cash.onClick.Release();
		}
	}
}
