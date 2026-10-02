using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Net;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class UIActivityComeback_Seven_Com : GComponent
{
	private int _signInGroupId;

	private int _signInRechargeId;

	private SignInDataConfigure _signInDataConfigure;

	private List<SignInDataConfigureItem> _items;

	private RechargeGoods _rechargeGoods;

	public GList itemList;

	public UIActivityComeback_Buy_Button btn_Buy;

	public GLoader loader_Title;

	public const string URL = "ui://hconmwfcy9qn8";

	public bool HasRedPoint { get; private set; }

	public void Init(int activityId)
	{
		ComebackParamsConfigure comebackParamsConfigure = StaticConfigure.Comeback?.ParamsDict?.GetValueOrDefault(1);
		if (comebackParamsConfigure == null)
		{
			Debug.LogError("[UIActivityComeback_Seven_Com] Comeback.ParamsDict[1] 为空，无法初始化");
			return;
		}
		_signInGroupId = comebackParamsConfigure.SignInId;
		_signInRechargeId = comebackParamsConfigure.SignInRechargeId;
		if (!StaticConfigure.SignIn.InfoDict.TryGetValue(_signInGroupId, out var value) || value == null)
		{
			Debug.LogError($"[UIActivityComeback_Seven_Com] STRSignInInfoDict[{_signInGroupId}] 不存在");
			return;
		}
		if (!StaticConfigure.SignIn.DataDict.TryGetValue(value.SigninRewardID, out _signInDataConfigure) || _signInDataConfigure == null)
		{
			Debug.LogError($"[UIActivityComeback_Seven_Com] SignIn.DataDict[{value.SigninRewardID}] 不存在");
			return;
		}
		TaskActivityData taskActivityData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(activityId);
		if (taskActivityData != null)
		{
			RepeatedField<string> titleImages = taskActivityData.activityConfig.TitleImages;
			loader_Title.url = GameSettings.GetDataForLanguage(titleImages[1], titleImages[2], titleImages[0], titleImages[3]);
		}
		_items = _signInDataConfigure.SignInDataConfigureItems.OrderBy((SignInDataConfigureItem x) => x.Day).ToList();
		_rechargeGoods = FindRechargeGoods(_signInRechargeId);
		if (itemList != null)
		{
			itemList.SetVirtual();
			itemList.itemRenderer = RendererListItem;
		}
	}

	private RechargeGoods FindRechargeGoods(int goodsId)
	{
		if (goodsId == 0)
		{
			return null;
		}
		StoreLogic storeLogic = SimpleSingletonProvider<GameLogicManager>.inst?.store;
		if (storeLogic == null)
		{
			return null;
		}
		RechargeGoods rechargeGoodsByShopTypeAndGoodsID = storeLogic.GetRechargeGoodsByShopTypeAndGoodsID(42, goodsId);
		if (rechargeGoodsByShopTypeAndGoodsID != null)
		{
			return rechargeGoodsByShopTypeAndGoodsID;
		}
		foreach (BaseGoodsData item in storeLogic.GetRechargeGoodsByShopType(42))
		{
			if (item is RechargeGoods rechargeGoods && rechargeGoods.goodsId == goodsId)
			{
				return rechargeGoods;
			}
		}
		Debug.LogError($"[UIActivityComeback_Seven_Com] 找不到 GoodsID={goodsId} 的进阶档商品");
		return null;
	}

	public void ClearData()
	{
		_signInGroupId = 0;
		_signInRechargeId = 0;
		_signInDataConfigure = null;
		_items = null;
		_rechargeGoods = null;
	}

	public override void Dispose()
	{
		_signInGroupId = 0;
		_signInRechargeId = 0;
		_signInDataConfigure = null;
		_items = null;
		_rechargeGoods = null;
		base.Dispose();
	}

	public void OnShow()
	{
		RefreshBuyButton();
		RefreshList();
	}

	public void RefreshRedPoints()
	{
		ReturnInfo returnInfo = SimpleSingletonProvider<GameLogicManager>.inst.comeback?.Data?.ReturnInfo;
		HasRedPoint = returnInfo != null && ScanHasClaimable(returnInfo.SignIn);
	}

	private bool ScanHasClaimable(ReturnSignIn signIn)
	{
		if (signIn == null)
		{
			return false;
		}
		int unlockDay = signIn.UnlockDay;
		if (unlockDay <= 0)
		{
			return false;
		}
		RepeatedField<int> freeClaimedDays = signIn.FreeClaimedDays;
		RepeatedField<int> advClaimedDays = signIn.AdvClaimedDays;
		for (int i = 1; i <= unlockDay; i++)
		{
			if (freeClaimedDays == null || !freeClaimedDays.Contains(i))
			{
				return true;
			}
			if (signIn.AdvUnlocked && advClaimedDays != null && !advClaimedDays.Contains(i))
			{
				return true;
			}
		}
		return false;
	}

	private void RefreshBuyButton()
	{
		if (btn_Buy != null)
		{
			bool valueOrDefault = (SimpleSingletonProvider<GameLogicManager>.inst.comeback?.Data?.ReturnInfo)?.SignIn?.AdvUnlocked == true;
			btn_Buy.enabled = !valueOrDefault;
			if (btn_Buy.txt_price != null && _rechargeGoods != null)
			{
				string discountPriceText = _rechargeGoods.GetDiscountPriceText();
				btn_Buy.txt_price.text = discountPriceText;
				btn_Buy.txt_price.AddCurrencySymbols(discountPriceText);
			}
		}
	}

	private void RefreshList()
	{
		if (_items != null && itemList != null)
		{
			itemList.numItems = _items.Count;
		}
	}

	private void RendererListItem(int index, GObject item)
	{
		if (item is UIActivityComeback_ListItem uIActivityComeback_ListItem && _items != null && index >= 0 && index < _items.Count)
		{
			SignInDataConfigureItem signInDataConfigureItem = _items[index];
			int day = signInDataConfigureItem.Day;
			ReturnInfo obj = SimpleSingletonProvider<GameLogicManager>.inst.comeback?.Data?.ReturnInfo;
			int valueOrDefault = (obj?.SignIn?.UnlockDay).GetValueOrDefault();
			bool valueOrDefault2 = obj?.SignIn?.AdvUnlocked == true;
			bool valueOrDefault3 = obj?.SignIn?.FreeClaimedDays?.Contains(day) == true;
			bool valueOrDefault4 = obj?.SignIn?.AdvClaimedDays?.Contains(day) == true;
			if (uIActivityComeback_ListItem.day != null)
			{
				uIActivityComeback_ListItem.day.selectedIndex = day - 1;
			}
			bool flag = day <= valueOrDefault;
			bool flag2 = flag && valueOrDefault2;
			if (uIActivityComeback_ListItem.freeItem != null && uIActivityComeback_ListItem.freeItem.isLock != null)
			{
				uIActivityComeback_ListItem.freeItem.isLock.selectedIndex = (flag ? 1 : 0);
			}
			if (uIActivityComeback_ListItem.paidItem != null && uIActivityComeback_ListItem.paidItem.isLock != null)
			{
				uIActivityComeback_ListItem.paidItem.isLock.selectedIndex = (flag2 ? 1 : 0);
			}
			if (uIActivityComeback_ListItem.freeItem != null)
			{
				RenderRewardItem(uIActivityComeback_ListItem.freeItem, signInDataConfigureItem.Reward, valueOrDefault3, day, isAdvanced: false, !flag);
			}
			if (uIActivityComeback_ListItem.paidItem != null)
			{
				RenderRewardItem(uIActivityComeback_ListItem.paidItem, signInDataConfigureItem.RechargeReward, valueOrDefault4, day, isAdvanced: true, !flag2);
			}
		}
	}

	private void RenderRewardItem(UIActivityComeback_Button_RewardItem btnItem, MapField<int, int> rewardMap, bool claimed, int day, bool isAdvanced, bool isLocked)
	{
		if (btnItem == null)
		{
			return;
		}
		if (rewardMap == null || rewardMap.Count == 0)
		{
			btnItem.visible = false;
			return;
		}
		btnItem.visible = true;
		KeyValuePair<int, int> keyValuePair = rewardMap.First();
		int itemId = keyValuePair.Key;
		int count = keyValuePair.Value;
		if (btnItem.loader_Item != null)
		{
			ItemInfoConfigure itemInfoConfigure = itemId.GetItemInfoConfigure();
			if (itemInfoConfigure != null && !string.IsNullOrEmpty(itemInfoConfigure.ShowIcon))
			{
				btnItem.loader_Item.url = itemInfoConfigure.ShowIcon;
			}
		}
		if (btnItem.txt_ItemFreeNum != null)
		{
			btnItem.txt_ItemFreeNum.text = $"x{count}";
		}
		if (btnItem.isLock != null && btnItem.isLock.selectedIndex == 0)
		{
			if (btnItem.isClaimed != null)
			{
				btnItem.isClaimed.selectedIndex = 1;
			}
			if (btnItem.taskComplete != null)
			{
				btnItem.taskComplete.selectedIndex = 1;
			}
		}
		else
		{
			if (btnItem.isClaimed != null)
			{
				btnItem.isClaimed.selectedIndex = ((!claimed) ? 1 : 0);
			}
			if (btnItem.taskComplete != null)
			{
				btnItem.taskComplete.selectedIndex = 0;
			}
		}
		btnItem.onClick.Set((EventCallback0)delegate
		{
			OnRewardItemClick(itemId, count, isAdvanced, isLocked, claimed);
		});
	}

	private async void OnBuyClick()
	{
		if (btn_Buy == null || _rechargeGoods == null)
		{
			return;
		}
		ReturnInfo obj = SimpleSingletonProvider<GameLogicManager>.inst.comeback?.Data?.ReturnInfo;
		if (obj != null && obj.SignIn?.AdvUnlocked == true)
		{
			return;
		}
		btn_Buy.onClick.Retain();
		try
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.store.RequestCreateOrder(_rechargeGoods, 1);
		}
		finally
		{
			btn_Buy.onClick.Release();
		}
	}

	private async void OnRewardItemClick(int itemId, int count, bool isAdvanced, bool isLocked, bool claimed)
	{
		if (isLocked)
		{
			await SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(itemId, count, _Usable: false);
		}
		else if (!claimed)
		{
			RPCAsyncResult rPCAsyncResult = SimpleSingletonProvider<GameLogicManager>.inst.comeback?.RequestSignInClaim(_signInGroupId, isAdvanced);
			if (rPCAsyncResult != null)
			{
				await rPCAsyncResult;
			}
		}
	}

	public void AddEvent()
	{
		if (btn_Buy != null)
		{
			btn_Buy.onClick.Add(OnBuyClick);
		}
	}

	public void RemoveEvent()
	{
		btn_Buy?.onClick.Remove(OnBuyClick);
	}

	public void AddListener()
	{
		ComebackLogic comeback = SimpleSingletonProvider<GameLogicManager>.inst.comeback;
		if (comeback != null)
		{
			comeback.signal.infoUpdated.AddListener(OnInfoUpdated);
			comeback.signal.signInUpdated.AddListener(OnSignInUpdated);
		}
	}

	public void RemoveListener()
	{
		ComebackLogic comeback = SimpleSingletonProvider<GameLogicManager>.inst.comeback;
		if (comeback != null)
		{
			comeback.signal.infoUpdated.RemoveListener(OnInfoUpdated);
			comeback.signal.signInUpdated.RemoveListener(OnSignInUpdated);
		}
	}

	private void OnInfoUpdated(ReturnInfo _)
	{
		OnShow();
		RefreshRedPoints();
	}

	private void OnSignInUpdated(ReturnSignIn _)
	{
		OnShow();
		RefreshRedPoints();
	}

	public static UIActivityComeback_Seven_Com CreateInstance()
	{
		return (UIActivityComeback_Seven_Com)UIPackage.CreateObject("ActivityComeback", "ActivityComeback_Seven_Com");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		itemList = (GList)GetChildAt(7);
		btn_Buy = (UIActivityComeback_Buy_Button)GetChildAt(9);
		loader_Title = (GLoader)GetChildAt(12);
	}
}
