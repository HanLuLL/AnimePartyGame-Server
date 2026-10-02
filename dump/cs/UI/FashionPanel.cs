using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using party.protocol;

namespace UI;

public class FashionPanel : BasePanel<UIFashionPanel>
{
	private const int DefaultKvNameMessageId = 1121;

	private const int DefaultKvDescMessageId = 1122;

	private ShowingFashion curShowingFashion;

	private List<ItemInfoConfigure> curItems;

	private Action continueAction;

	private const string Default_KV_Icon = "UT_Item_KV_76000";

	private bool IsInit;

	private UIFashion_Button_Type selectTab;

	public FashionPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIFashionPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		ShowCurPlan();
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.list_Items.scrollPane.scrollStep = UIConfig.defaultScrollStep;
		base.ui.list_Items.itemRenderer = RendererItem;
		base.ui.list_Tab.itemRenderer = RendererTab;
		base.ui.list_Tab.foldInvisibleItems = true;
		base.ui.list_Tab.align = AlignType.Center;
		base.ui.graph_1.SetSize(GRoot.inst.width, base.ui.graph_1.height);
		base.ui.graph_2.SetSize(GRoot.inst.width, base.ui.graph_2.height);
	}

	public override void Refresh()
	{
		base.Refresh();
		if (curShowingFashion == null)
		{
			curShowingFashion = SimpleSingletonProvider<GameLogicManager>.inst.fashion.GetCurShowingFashion();
		}
		base.ui.list_Tab.numItems = SimpleSingletonProvider<GameLogicManager>.inst.fashion.defaultMap.Count;
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_Return.onClick.Add(ReturnPanel);
		base.ui.slot.onChanged.Add(SwitchSlot);
		base.ui.list_Items.onClickItem.Add(SelectItem);
		base.ui.list_Tab.onClickItem.Add(RefreshRed);
		base.ui.btn_Save.onClick.Add(OnClickChange);
		base.ui.btn_reset.onClick.Add(OnClickReset);
		base.ui.btn_Video.AddEvent();
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Return.onClick.Remove(ReturnPanel);
		base.ui.slot.onChanged.Remove(SwitchSlot);
		base.ui.list_Items.onClickItem.Remove(SelectItem);
		base.ui.list_Tab.onClickItem.Remove(RefreshRed);
		base.ui.btn_Save.onClick.Remove(OnClickChange);
		base.ui.btn_reset.onClick.Remove(OnClickReset);
		base.ui.btn_Video.RemoveEvent();
	}

	protected override void AddListener()
	{
		base.AddListener();
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
	}

	public override void Close()
	{
		base.ui.btn_Video.CloseVideo();
		base.ui.com_MainBack.CloseVideo();
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private async void ReturnPanel()
	{
		if (base.ui.btn_Save.Status.selectedIndex == 1)
		{
			continueAction = ReturnPanel;
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(1079, ExecuteAction, CancelAction).Forget();
		}
		else
		{
			base.ui.btn_Return.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Home);
			base.ui.btn_Return.onClick.Release();
		}
	}

	private void ShowCurPlan()
	{
		curShowingFashion = SimpleSingletonProvider<GameLogicManager>.inst.fashion.GetCurShowingFashion();
		base.ui.slot.onChanged.Call();
		base.ui.btn_Save.Status.selectedIndex = 0;
		RefreshSlotByData();
	}

	private void RefreshSlotByData()
	{
		RefreshSlot(1, curShowingFashion.headShotId);
		RefreshSlot(2, curShowingFashion.labelId);
		RefreshSlot(3, curShowingFashion.cardId);
		RefreshSlot(4, curShowingFashion.diceId);
		RefreshSlot(5, curShowingFashion.killEffectId);
		RefreshSlot(6, curShowingFashion.KvId);
	}

	private void SwitchSlot()
	{
		base.ui.list_Tab.onClickItem.Retain();
		int num = base.ui.slot.selectedIndex + 1;
		if (!IsInit)
		{
			if (base.ui.list_Tab.GetChildAt(num - 1) is UIFashion_Button_Type uIFashion_Button_Type)
			{
				selectTab = uIFashion_Button_Type;
			}
			IsInit = true;
		}
		switch (num)
		{
		case 1:
			curItems = StaticConfigure.Fashion.GetHeadShotConfigs();
			RendererLabel();
			break;
		case 2:
			curItems = StaticConfigure.Fashion.GetBackgroundConfigs();
			RendererLabel();
			break;
		case 3:
			curItems = StaticConfigure.Fashion.GetCardBackConfigs();
			RendererCard(GetCurShowingItemIdByType(3));
			break;
		case 4:
			curItems = StaticConfigure.Fashion.GetDiceConfigs();
			RefreshShowVideo();
			break;
		case 5:
			curItems = StaticConfigure.Fashion.GetEffectConfigs();
			RefreshShowVideo();
			break;
		case 6:
			curItems = StaticConfigure.Fashion.GetMainBGConfigs();
			if (curItems != null && curItems.Count > 0)
			{
				curItems.Insert(0, null);
			}
			RefreshShowMainVideo();
			break;
		}
		base.ui.list_Items.touchable = false;
		base.ui.list_Items.numItems = 0;
		base.ui.list_Items.numItems = curItems?.Count ?? 0;
		base.ui.list_Items.touchable = true;
		if (curItems != null)
		{
			int num2 = num;
			if (num2 == 6 && curShowingFashion.KvId == 0)
			{
				int index = base.ui.list_Items.ItemIndexToChildIndex(0);
				base.ui.list_Items.ScrollToView(index);
			}
			else
			{
				int curShowingItemIdByType = GetCurShowingItemIdByType(num2);
				for (int i = 0; i < curItems.Count; i++)
				{
					ItemInfoConfigure itemInfoConfigure = curItems[i];
					if (itemInfoConfigure != null && itemInfoConfigure.Id == curShowingItemIdByType)
					{
						int index2 = base.ui.list_Items.ItemIndexToChildIndex(i);
						base.ui.list_Items.ScrollToView(index2);
						break;
					}
				}
			}
		}
		base.ui.list_Tab.onClickItem.Release();
	}

	private void SelectItem(EventContext context)
	{
		if (!(context.data is UIFashion_Button_PropItem uIFashion_Button_PropItem))
		{
			return;
		}
		base.ui.list_Items.onClickItem.Retain();
		bool num = base.ui.slot.selectedIndex + 1 == 6 && base.ui.list_Items.selectedIndex == 0;
		int num2 = (int)uIFashion_Button_PropItem.data;
		if (num && SimpleSingletonProvider<GameLogicManager>.inst.fashion.HasDefaultKVUpdateRed())
		{
			SimpleSingletonProvider<GameLogicManager>.inst.fashion.SaveReadDefaultKVId();
			SimpleSingletonProvider<GameLogicManager>.inst.fashion.RegisterRed();
			base.ui.list_Tab.numItems = SimpleSingletonProvider<GameLogicManager>.inst.fashion.defaultMap.Count;
		}
		if (num2 == 0)
		{
			uIFashion_Button_PropItem.redpoint.selectedIndex = 0;
		}
		if (uIFashion_Button_PropItem.redpoint.selectedIndex == 1)
		{
			uIFashion_Button_PropItem.redpoint.selectedIndex = 0;
			SimpleSingletonProvider<GameLogicManager>.inst.account.RemoveNewItemById(num2);
			SimpleSingletonProvider<GameLogicManager>.inst.account.RequestClientDataUploadS2C(ClientDataUploadC2S.Types.OpsData.NewItemData);
			ItemInfoConfigure itemInfoConfigure = num2.GetItemInfoConfigure();
			if (!SimpleSingletonProvider<GameLogicManager>.inst.account.OnTypeGetNewData(itemInfoConfigure.ItemType))
			{
				base.ui.list_Tab.numItems = SimpleSingletonProvider<GameLogicManager>.inst.fashion.defaultMap.Count;
			}
		}
		RendereUIAndSelectItem(num2);
		base.ui.list_Items.onClickItem.Release();
	}

	private void RefreshRed(EventContext context)
	{
		if (!(context.data is UIFashion_Button_Type uIFashion_Button_Type) || selectTab == uIFashion_Button_Type)
		{
			return;
		}
		if (selectTab != null)
		{
			int childIndex = base.ui.list_Tab.GetChildIndex(selectTab);
			if (childIndex == -1)
			{
				return;
			}
			if (selectTab.redpoint.selectedIndex == 1)
			{
				ItemInfoConfigure itemInfoConfigure = GetCurShowingItemIdByType(childIndex + 1).GetItemInfoConfigure();
				SimpleSingletonProvider<GameLogicManager>.inst.fashion.ClearNewChestSetSilent(itemInfoConfigure.ItemType);
				if (itemInfoConfigure.ItemType == ItemType.Kv)
				{
					SimpleSingletonProvider<GameLogicManager>.inst.fashion.SaveReadDefaultKVId();
				}
				base.ui.list_Tab.numItems = SimpleSingletonProvider<GameLogicManager>.inst.fashion.defaultMap.Count;
			}
		}
		selectTab = uIFashion_Button_Type;
	}

	private void RendereUIAndSelectItem(int _itemId)
	{
		int num = base.ui.slot.selectedIndex + 1;
		int num2 = ((num != 6 || curShowingFashion.KvId != 0) ? GetCurShowingItemIdByType(num) : 0);
		if (_itemId != num2)
		{
			UpdateCurShowingItemId(_itemId);
			base.ui.btn_Save.Status.selectedIndex = (curShowingFashion.IsChange() ? 1 : 0);
		}
		if (num == 2 || num == 1)
		{
			RendererLabel();
		}
		if (num == 3)
		{
			RendererCard(_itemId);
		}
		if (num == 6)
		{
			RefreshShowMainVideo();
		}
		else
		{
			RefreshShowVideo();
		}
		RefreshFashionInfo((num == 6 && _itemId == 0) ? null : _itemId.GetItemInfoConfigure());
		RefreshSlot(num, GetCurShowingItemIdByType(num));
	}

	private void RefreshFashionInfo(ItemInfoConfigure _itemConfig)
	{
		if (_itemConfig == null)
		{
			base.ui.txt_Name.text = 1121.GetLocal(UIStringType.Message);
			base.ui.txt_Desc.text = 1122.GetLocal(UIStringType.Message);
			((UIButton_Way)base.ui.btn_link).Refresh(0);
		}
		else
		{
			base.ui.txt_Name.text = _itemConfig.NameID.GetLocal(UIStringType.Item);
			base.ui.txt_Desc.text = _itemConfig.DescriptionID.GetLocal(UIStringType.Item);
			((UIButton_Way)base.ui.btn_link).Refresh((_itemConfig.WayList.Count > 0) ? _itemConfig.WayList[0] : 0);
		}
	}

	private void OnClickChange(EventContext context)
	{
		if (base.ui.btn_Save.Status.selectedIndex == 0 || !curShowingFashion.IsChange())
		{
			return;
		}
		if (!curShowingFashion.Savelicence())
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1031);
		}
		else if (base.ui.btn_Save.Status.selectedIndex == 1)
		{
			base.ui.btn_Save.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.fashion.RequestSetFashionC2S(curShowingFashion.planID, curShowingFashion.HandleInfo()).OnFinishedOnly.AddOnce(delegate
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1030);
				base.ui.btn_Save.Status.selectedIndex = 0;
				base.ui.btn_Save.onClick.Release();
			});
		}
	}

	private async void OnClickReset(EventContext context)
	{
		if (curShowingFashion.IsChange())
		{
			base.ui.btn_Save.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(1032, delegate
			{
				curShowingFashion.SetFashion();
				base.ui.slot.onChanged.Call();
				base.ui.btn_Save.Status.selectedIndex = (curShowingFashion.IsChange() ? 1 : 0);
				RefreshSlotByData();
			});
			base.ui.btn_Save.onClick.Release();
		}
	}

	private void CancelAction()
	{
		continueAction = null;
	}

	private void ExecuteAction()
	{
		base.ui.btn_Save.Status.selectedIndex = ((!IsRunningPlan()) ? 1 : 0);
		continueAction();
	}

	private void RefreshSlot(int slotIndex, int itemId)
	{
		if (GetSlotCom(slotIndex) is UICom_Item uICom_Item)
		{
			bool flag = slotIndex == 6 && curShowingFashion != null && curShowingFashion.KvId == 0;
			ItemInfoConfigure itemInfoConfigure = ((itemId == 0) ? null : itemId.GetItemInfoConfigure());
			uICom_Item.qualityType.selectedIndex = (int)(flag ? QualityType.Green : itemInfoConfigure.QualityType);
			uICom_Item.loader_Icon.url = (flag ? "UT_Item_KV_76000" : itemInfoConfigure.ShowIcon);
			if (flag)
			{
				uICom_Item.grayed = false;
			}
			else
			{
				uICom_Item.grayed = !SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(itemId);
			}
		}
	}

	private GComponent GetSlotCom(int slotIndex)
	{
		return slotIndex switch
		{
			1 => base.ui.com_Item_Head, 
			2 => base.ui.com_Item_Label, 
			3 => base.ui.com_Item_Card, 
			4 => base.ui.com_Item_Dice, 
			5 => base.ui.com_Item_Effect, 
			6 => base.ui.com_Item_Main, 
			_ => null, 
		};
	}

	private void RendererTab(int index, GObject item)
	{
		if (!(item is UIFashion_Button_Type uIFashion_Button_Type))
		{
			return;
		}
		int curShowingItemIdByType = GetCurShowingItemIdByType(index + 1);
		bool flag;
		if (index + 1 == 6)
		{
			if (RunTimeRemoteConfigHandler.IsAuditMode)
			{
				uIFashion_Button_Type.visible = false;
			}
			else
			{
				uIFashion_Button_Type.visible = true;
			}
			ItemInfoConfigure itemInfoConfigure = SimpleSingletonProvider<GameLogicManager>.inst.fashion.GetCurrentKVId().GetItemInfoConfigure();
			flag = (itemInfoConfigure != null && SimpleSingletonProvider<GameLogicManager>.inst.account.OnTypeGetNewData(itemInfoConfigure.ItemType)) || SimpleSingletonProvider<GameLogicManager>.inst.fashion.HasDefaultKVUpdateRed();
		}
		else
		{
			if (curShowingItemIdByType == 0)
			{
				return;
			}
			ItemInfoConfigure itemInfoConfigure2 = curShowingItemIdByType.GetItemInfoConfigure();
			flag = itemInfoConfigure2 != null && SimpleSingletonProvider<GameLogicManager>.inst.account.OnTypeGetNewData(itemInfoConfigure2.ItemType);
		}
		uIFashion_Button_Type.redpoint.selectedIndex = (flag ? 1 : 0);
	}

	private void RendererItem(int index, GObject item)
	{
		UIFashion_Button_PropItem _item = item as UIFashion_Button_PropItem;
		if (_item == null)
		{
			return;
		}
		_item.com_Item.visible = false;
		bool isDefaultKvItem = base.ui.slot.selectedIndex + 1 == 6 && index == 0;
		_item.Cut_in.Play(1, 0.005f * (float)index, delegate
		{
			int num = base.ui.slot.selectedIndex + 1;
			bool flag = (isDefaultKvItem ? (curShowingFashion.KvId == 0) : ((num == 6) ? (curItems[index].Id == curShowingFashion.KvId) : (curItems[index].Id == GetCurShowingItemIdByType(num))));
			_item.selected = flag;
			if (flag)
			{
				RefreshFashionInfo(isDefaultKvItem ? null : curItems[index]);
			}
		});
		if (isDefaultKvItem)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.fashion.GetCurrentKVId();
			_item.data = 0;
			if (_item.com_Item is UICom_Item uICom_Item)
			{
				_item.redpoint.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.fashion.HasDefaultKVUpdateRed() ? 1 : 0);
				uICom_Item.grayed = false;
				uICom_Item.qualityType.selectedIndex = 2;
				uICom_Item.loader_Icon.url = "UT_Item_KV_76000";
			}
		}
		else
		{
			_item.data = curItems[index].Id;
			if (_item.com_Item is UICom_Item uICom_Item2)
			{
				uICom_Item2.grayed = !SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(curItems[index].Id);
				_item.redpoint.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.account.OnIDGetNewData(curItems[index].Id) ? 1 : 0);
				uICom_Item2.qualityType.selectedIndex = (int)curItems[index].QualityType;
				uICom_Item2.loader_Icon.url = curItems[index].ShowIcon;
			}
		}
	}

	private int GetCurShowingItemIdByType(int type)
	{
		if (curShowingFashion == null)
		{
			return 0;
		}
		switch (type)
		{
		case 1:
			return curShowingFashion.headShotId;
		case 2:
			return curShowingFashion.labelId;
		case 3:
			return curShowingFashion.cardId;
		case 4:
			return curShowingFashion.diceId;
		case 5:
			return curShowingFashion.killEffectId;
		case 6:
			if (curShowingFashion.KvId != 0)
			{
				return curShowingFashion.KvId;
			}
			return SimpleSingletonProvider<GameLogicManager>.inst.fashion.GetCurrentKVId();
		default:
			return 0;
		}
	}

	private void UpdateCurShowingItemId(int _ItemId)
	{
		curShowingFashion?.UpdateItemId(base.ui.slot.selectedIndex + 1, _ItemId);
	}

	private void RendererLabel()
	{
		ItemInfoConfigure itemInfoConfigure = GetCurShowingItemIdByType(2).GetItemInfoConfigure();
		UICom_PlayerLabel com_Label = (UICom_PlayerLabel)base.ui.com_Label;
		CommonUIManager.RendererLabelInfo(com_Label, SimpleSingletonProvider<GameLogicManager>.inst.account.GetName(), SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo().Level);
		(string, bool) playerLabel = itemInfoConfigure.SubMeterID.GetFashionAccountBackgroundConfigure().GetPlayerLabel();
		CommonUIManager.RendererLabel(UIType.Panel, (int)base.config.PanelType, com_Label, playerLabel.Item1, playerLabel.Item2);
		ItemInfoConfigure itemInfoConfigure2 = GetCurShowingItemIdByType(1).GetItemInfoConfigure();
		CommonUIManager.RendererHeadShot(com_Label, itemInfoConfigure2.SubMeterID.GetFashionAccountHeadShot(), isVideo: false);
	}

	private void RendererCard(int _cardItemId)
	{
		FashionCardBackConfigure fashionCardBackConfigure = _cardItemId.GetItemInfoConfigure().SubMeterID.GetFashionCardBackConfigure();
		CommonUIManager.RendererCardBack((UICom_CardBack)base.ui.com_CardBack, fashionCardBackConfigure).Forget();
	}

	private void RefreshShowVideo()
	{
		int num = base.ui.slot.selectedIndex + 1;
		if (num == 4 || num == 5)
		{
			ItemInfoConfigure itemInfoConfigure = GetCurShowingItemIdByType(num).GetItemInfoConfigure();
			string videoKey = "";
			switch (num)
			{
			case 4:
				videoKey = itemInfoConfigure.SubMeterID.GetFashionDiceConfigure().PreviewVideo;
				break;
			case 5:
				videoKey = itemInfoConfigure.SubMeterID.GetFashionFashionEffectConfigure().PreviewVideo;
				break;
			}
			base.ui.btn_Video.RefreshVideo(videoKey);
		}
	}

	private void RefreshShowMainVideo()
	{
		int num = base.ui.slot.selectedIndex + 1;
		if (num == 6)
		{
			string kVVideoKey = GetCurShowingItemIdByType(num).GetItemInfoConfigure().SubMeterID.GetKVVideoKey();
			base.ui.com_MainBack.RefreshVideo(kVVideoKey);
		}
	}

	private bool IsRunningPlan()
	{
		if (curShowingFashion == null)
		{
			return false;
		}
		return curShowingFashion.planID == SimpleSingletonProvider<GameLogicManager>.inst.fashion.RunningPlan;
	}
}
