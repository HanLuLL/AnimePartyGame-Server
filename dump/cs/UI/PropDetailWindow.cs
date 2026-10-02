using System;
using System.Collections.Generic;
using System.Linq;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.protocol;

namespace UI;

public class PropDetailWindow : BaseWindow
{
	private BlurBgTextureController blurBgCtrl = new BlurBgTextureController();

	private int itemId;

	private int itemNum;

	private BagItem propItem;

	private int currentSelectNum;

	private bool Usable;

	private UIWindowType preWindowType;

	protected override bool isGeneralFadeIn => true;

	protected override bool isGeneralFadeOut => true;

	public PropDetailWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIPropDetailWindow.CreateInstance();
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
		if (base.contentPane is UIPropDetailWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIPropDetailWindow && uIPropDetailWindow.mohu is UICom_PopUpWindow_MohuBg)
		{
			uIPropDetailWindow.btn_addNum.onClick.Add(OnAddOne);
			uIPropDetailWindow.btn_delNum.onClick.Add(OnDelOne);
			uIPropDetailWindow.btn_Max.onClick.Add(OnSetMaxSelect);
			uIPropDetailWindow.btn_Min.onClick.Add(OnSetMinSelect);
			uIPropDetailWindow.mohu.onClick.Add(CloseWin);
			bottom.btn_Cancel.onClick.Add(CloseWin);
			bottom.closeButton.onClick.Add(CloseWin);
			bottom.btn_Sure_Only.onClick.Add(CloseWin);
			bottom.btn_Sure.onClick.Add(OnClickConfirm);
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.LoseFocus();
			blurBgCtrl.OnShown(this);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIPropDetailWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIPropDetailWindow)
		{
			uIPropDetailWindow.btn_addNum.onClick.Remove(OnAddOne);
			uIPropDetailWindow.btn_delNum.onClick.Remove(OnDelOne);
			uIPropDetailWindow.btn_Max.onClick.Remove(OnSetMaxSelect);
			uIPropDetailWindow.btn_Min.onClick.Remove(OnSetMinSelect);
			uIPropDetailWindow.mohu.onClick.Remove(CloseWin);
			bottom.btn_Cancel.onClick.Remove(CloseWin);
			bottom.closeButton.onClick.Remove(CloseWin);
			bottom.btn_Sure_Only.onClick.Remove(CloseWin);
			bottom.btn_Sure.onClick.Remove(OnClickConfirm);
			if (!SimpleSingletonProvider<UIManager>.inst.purchase.isShowing)
			{
				SimpleSingletonProvider<UIManager>.inst.currentPanel?.ResumeFocus();
			}
			blurBgCtrl.OnHide();
		}
	}

	protected override void OnKeyDown(EventContext context)
	{
		if (base.contentPane is UIPropDetailWindow uIPropDetailWindow && base.isShowing && uIPropDetailWindow.bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom && context.inputEvent.keyCode == KeyCode.Escape)
		{
			uICom_PopUpWindow_Bottom.btn_Cancel.FireClick(downEffect: true);
			uICom_PopUpWindow_Bottom.closeButton.FireClick(downEffect: true);
			uICom_PopUpWindow_Bottom.btn_Sure_Only.FireClick(downEffect: true);
			CloseWin();
		}
	}

	public async UniTask ShowWin(int _itemId, int _Num, bool _Usable)
	{
		await TryShowAsync();
		itemId = _itemId;
		itemNum = _Num;
		Usable = _Usable;
		RefreshInfo(itemId, itemNum, Usable);
	}

	private void RefreshInfo(int _itemId, int _itemNum, bool _Usable)
	{
		if (!(base.contentPane is UIPropDetailWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIPropDetailWindow))
		{
			return;
		}
		uIPropDetailWindow.type.selectedIndex = 0;
		ItemInfoConfigure itemInfoConfigure = _itemId.GetItemInfoConfigure();
		uIPropDetailWindow.txt_itemName.text = itemInfoConfigure.NameID.GetLocal(UIStringType.Item);
		uIPropDetailWindow.txt_itemDes.text = itemInfoConfigure.DescriptionID.GetLocal(UIStringType.Item);
		RefreshRecycleInfo(itemInfoConfigure, _itemNum);
		RefreshItemWay(itemInfoConfigure);
		int num = MaxCount();
		RefreshItem((UICom_LitItem)uIPropDetailWindow.btn_item, itemInfoConfigure, _itemNum, chest: false);
		if (_Usable && num > 0)
		{
			propItem = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItem(_itemId);
			if (propItem.config.ItemType == ItemType.Chest && propItem is ChestItem chestItem)
			{
				RefreshSelectGroup(1);
				uIPropDetailWindow.chestType.selectedIndex = ((!chestItem.chestConfig.IsOptional) ? 1 : 0);
				uIPropDetailWindow.com_Explain.txt_ContentType.text = chestItem.chestConfig.ContentType.GetLocal(UIStringType.Chest);
				uIPropDetailWindow.itemType.selectedIndex = 1;
				bottom.btnsState.selectedIndex = 1;
				if (chestItem.chestConfig.IsOptional)
				{
					MapField<int, int> OptionalReward = chestItem.chestConfig.OptionalReward;
					uIPropDetailWindow.list_ChestItems.itemRenderer = delegate(int index, GObject uiChestItem)
					{
						if (uiChestItem is UIPropDetail_Com_ChestItem uIPropDetail_Com_ChestItem)
						{
							KeyValuePair<int, int> keyValuePair = OptionalReward.ElementAt(index);
							RefreshItem((UICom_LitItem)uIPropDetail_Com_ChestItem.btn_Item, keyValuePair.Key.GetItemInfoConfigure(), keyValuePair.Value, chest: true);
						}
					};
					uIPropDetailWindow.list_ChestItems.numItems = OptionalReward.Count;
				}
				else
				{
					RepeatedField<int> randomReward = chestItem.chestConfig.RandomReward;
					List<ChestRandomRewardConfigureItem> _list = new List<ChestRandomRewardConfigureItem>();
					foreach (int item in randomReward)
					{
						_list.AddRange(StaticConfigure.Chest.RandomRewardDict[item].ChestRandomRewardConfigureItems);
					}
					uIPropDetailWindow.list_ChestItems.itemRenderer = delegate(int index, GObject uiChestItem)
					{
						if (uiChestItem is UIPropDetail_Com_ChestItem uIPropDetail_Com_ChestItem)
						{
							ChestRandomRewardConfigureItem chestRandomRewardConfigureItem = _list[index];
							RefreshItem((UICom_LitItem)uIPropDetail_Com_ChestItem.btn_Item, chestRandomRewardConfigureItem.ItemID.GetItemInfoConfigure(), chestRandomRewardConfigureItem.MinCount, chest: true);
						}
					};
					uIPropDetailWindow.list_ChestItems.numItems = _list.Count;
				}
			}
			else
			{
				uIPropDetailWindow.itemType.selectedIndex = 0;
				bottom.btnsState.selectedIndex = 0;
			}
			if (uIPropDetailWindow.chestType.selectedIndex == 2)
			{
				bottom.btn_Sure.grayed = !propItem.enableUse;
				bottom.btn_Sure.touchable = propItem.enableUse;
			}
			else
			{
				bottom.btn_Sure.grayed = false;
				bottom.btn_Sure.touchable = true;
			}
		}
		else
		{
			uIPropDetailWindow.chestType.selectedIndex = 0;
			uIPropDetailWindow.itemType.selectedIndex = 0;
			bottom.btnsState.selectedIndex = 0;
		}
	}

	private void RefreshRecycleInfo(ItemInfoConfigure itemConfig, int num)
	{
		if (!(base.contentPane is UIPropDetailWindow uIPropDetailWindow))
		{
			return;
		}
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		if ((object)itemConfig.EndDateTime == null)
		{
			uIPropDetailWindow.recycle.selectedIndex = 0;
			return;
		}
		DateTime dateTime = itemConfig.EndDateTime.ToDateTime();
		if (serverTime > dateTime)
		{
			uIPropDetailWindow.recycle.selectedIndex = 0;
			return;
		}
		uIPropDetailWindow.recycle.selectedIndex = 1;
		TimeSpan timeSpan = dateTime - serverTime;
		KeyValuePair<int, int> keyValuePair = itemConfig.OuttimeTransform.ElementAt(0);
		ItemInfoConfigure itemInfoConfigure = keyValuePair.Key.GetItemInfoConfigure();
		uIPropDetailWindow.txt_Recycle.text = string.Format(1037.GetLocal(UIStringType.Message), timeSpan.Days.ToString().PadLeft(2, '0'), timeSpan.Hours.ToString().PadLeft(2, '0'), timeSpan.Minutes.ToString().PadLeft(2, '0'), Mathf.Max(0, keyValuePair.Value * num).ToString(), itemInfoConfigure.NameID.GetLocal(UIStringType.Item));
	}

	private int MaxCount()
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(itemId);
	}

	private void RefreshItem(UICom_LitItem item, ItemInfoConfigure configure, int count, bool chest)
	{
		item.loader_Icon.url = configure.ShowIcon;
		item.qualityType.selectedIndex = (int)configure.QualityType;
		string arg = ((count >= 0) ? "[color=#FFFFFF]" : "[color=#FF0000]");
		item.txt_itemNum.text = $"{arg}{Mathf.Abs(count)}[/color]";
		string text = ((count >= 0) ? "x" : "[color=#FF0000]-[/color]");
		item.txt_Symbol.text = text;
		item.onClick.Retain();
		if (chest)
		{
			item.onClick.Release();
			item.onClick.Set((EventCallback0)delegate
			{
				ShowByCurWin(configure.Id, count);
			});
		}
	}

	private void OnAddOne()
	{
		if (base.contentPane is UIPropDetailWindow uIPropDetailWindow && MaxCount() != 1)
		{
			uIPropDetailWindow.btn_addNum.onClick.Retain();
			RefreshSelectGroup(Mathf.Min(MaxCount(), currentSelectNum + 1));
			uIPropDetailWindow.btn_addNum.onClick.Release();
		}
	}

	private void OnDelOne()
	{
		if (base.contentPane is UIPropDetailWindow uIPropDetailWindow)
		{
			uIPropDetailWindow.btn_delNum.onClick.Retain();
			RefreshSelectGroup(Mathf.Max(currentSelectNum - 1, 1));
			uIPropDetailWindow.btn_delNum.onClick.Release();
		}
	}

	private void OnSetMaxSelect(EventContext context)
	{
		if (base.contentPane is UIPropDetailWindow uIPropDetailWindow)
		{
			uIPropDetailWindow.btn_Max.onClick.Retain();
			RefreshSelectGroup(MaxCount());
			uIPropDetailWindow.btn_Max.onClick.Release();
		}
	}

	private void OnSetMinSelect(EventContext context)
	{
		if (base.contentPane is UIPropDetailWindow uIPropDetailWindow)
		{
			uIPropDetailWindow.btn_Min.onClick.Retain();
			RefreshSelectGroup(1);
			uIPropDetailWindow.btn_Min.onClick.Release();
		}
	}

	private void RefreshSelectGroup(int curNum)
	{
		if (base.contentPane is UIPropDetailWindow uIPropDetailWindow)
		{
			int num = (currentSelectNum = Mathf.Min(curNum, 99));
			uIPropDetailWindow.txt_itemSelectNum.text = num.ToString();
		}
	}

	private void OnClickConfirm(EventContext context)
	{
		if (base.contentPane is UIPropDetailWindow uIPropDetailWindow && uIPropDetailWindow.itemType.selectedIndex != 0)
		{
			if (uIPropDetailWindow.chestType.selectedIndex == 0)
			{
				OnOpenChest(context);
			}
			else if (uIPropDetailWindow.chestType.selectedIndex == 1)
			{
				OnUseGoods(context);
			}
		}
	}

	private async void OnOpenChest(EventContext context)
	{
		if (base.contentPane is UIPropDetailWindow { bottom: var bottom } && bottom is UICom_PopUpWindow_Bottom winBottom)
		{
			winBottom.btn_Sure.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.boxProp.ShowWin(propItem);
			winBottom.btn_Sure.onClick.Release();
			Hide();
		}
	}

	private void OnUseGoods(EventContext context)
	{
		if (!(base.contentPane is UIPropDetailWindow { bottom: var bottom }))
		{
			return;
		}
		UICom_PopUpWindow_Bottom winBottom = bottom as UICom_PopUpWindow_Bottom;
		if (winBottom != null)
		{
			winBottom.btn_Sure.onClick.Retain();
			MonoSingletonProvider<NetManager>.inst.RPC.UseTreasureC2S.UseTreasureC2SCall(new UseTreasureC2S
			{
				DefId = propItem.config.Id,
				Count = currentSelectNum
			}).OnFinishedOnly.AddOnce(delegate
			{
				winBottom.btn_Sure.onClick.Release();
				Hide();
			});
		}
	}

	private void ShowByCurWin(int _itemId, int _itemNum)
	{
		ShowByWin(_itemId, _itemNum, UIWindowType.PropDetail);
	}

	public async void ShowByWin(int _itemId, int _itemNum, UIWindowType _PreWindowType)
	{
		await TryShowAsync();
		RefreshInfo(_itemId, _itemNum, _Usable: false);
		preWindowType = _PreWindowType;
	}

	private void CloseWin()
	{
		if (preWindowType == UIWindowType.PropDetail)
		{
			RefreshInfo(itemId, itemNum, Usable);
			preWindowType = UIWindowType.None;
		}
		else
		{
			preWindowType = UIWindowType.None;
			Hide();
		}
	}

	private void RefreshItemWay(ItemInfoConfigure _itemConfig)
	{
		if (base.contentPane is UIPropDetailWindow uIPropDetailWindow)
		{
			uIPropDetailWindow.wayAvaliable.selectedIndex = ((_itemConfig.WayList.Count > 0) ? 1 : 0);
			if (_itemConfig.WayList.Count != 0)
			{
				RefreshWays(uIPropDetailWindow.list_PropWay, _itemConfig.WayList);
			}
		}
	}

	private void RefreshWays(GList _list, RepeatedField<int> wayList)
	{
		_list.itemRenderer = delegate(int index, GObject item)
		{
			if (item is UIButton_Way uIButton_Way)
			{
				uIButton_Way.Refresh(wayList[index]);
			}
		};
		_list.numItems = wayList.Count;
	}

	public async void ShowGiftWayWin()
	{
		await TryShowAsync();
		if (base.contentPane is UIPropDetailWindow uIPropDetailWindow)
		{
			uIPropDetailWindow.type.selectedIndex = 1;
			RepeatedField<FavorWayConfigure> ways = StaticConfigure.Favor.Ways;
			RefreshWays(uIPropDetailWindow.list_Way, ways[0].WayList);
		}
	}
}
