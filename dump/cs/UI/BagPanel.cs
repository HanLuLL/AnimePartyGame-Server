using System.Collections.Generic;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class BagPanel : BasePanel<UIBagPanel>
{
	private List<int> _currentList;

	private UIButton_SwitchTab selectTab;

	public BagPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIBagPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		_currentList = new List<int>();
		base.ui.com_Tab.list.itemRenderer = RendererTab;
		base.ui.list_Items.SetVirtual();
		base.ui.list_Items.itemRenderer = RendererItem;
	}

	public override void Refresh()
	{
		base.Refresh();
		base.ui.com_Tab.list.numItems = StaticConfigure.Item.UIs.Count;
		base.ui.com_Tab.list.GetChildAt(0).onClick.Call();
		RefreshList();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_Return.onClick.Add(OnReturnToLastPanel);
		base.ui.com_Tab.list.onClickItem.Add(OnMenuItemSelected);
		base.ui.sortType.onChanged.Add(OnQualitySorted);
		base.ui.list_Items.onClickItem.Add(OnClickItem);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Return.onClick.Remove(OnReturnToLastPanel);
		base.ui.com_Tab.list.onClickItem.Remove(OnMenuItemSelected);
		base.ui.sortType.onChanged.Remove(OnQualitySorted);
		base.ui.list_Items.onClickItem.Remove(OnClickItem);
	}

	protected override void AddListener()
	{
		base.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.bagMapChanged.AddListener(RefreshByBagChange);
		SimpleSingletonProvider<GameLogicManager>.inst.bag.bagRedSignal.AddListener(OnBagRedSignalChanged);
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.bagMapChanged.RemoveListener(RefreshByBagChange);
		SimpleSingletonProvider<GameLogicManager>.inst.bag.bagRedSignal.RemoveListener(OnBagRedSignalChanged);
	}

	public override void Close()
	{
		if (selectTab != null)
		{
			selectTab.setStatus.selectedIndex = 0;
			selectTab = null;
		}
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private async void OnReturnToLastPanel()
	{
		await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
	}

	private void OnMenuItemSelected(EventContext context)
	{
		if (!(context.data is UIButton_SwitchTab uIButton_SwitchTab))
		{
			return;
		}
		if (selectTab != null)
		{
			selectTab.setStatus.selectedIndex = 0;
			int childIndex = base.ui.com_Tab.list.GetChildIndex(selectTab);
			if (childIndex >= 0 && childIndex < StaticConfigure.Item.UIs.Count && StaticConfigure.Item.UIs[childIndex].MenuSubType == ItemUIMenuSubType.Chest)
			{
				base.ui.com_Tab.list.numItems = StaticConfigure.Item.UIs.Count;
			}
		}
		if (selectTab == uIButton_SwitchTab)
		{
			selectTab.setStatus.selectedIndex = 1;
			return;
		}
		selectTab = uIButton_SwitchTab;
		selectTab.setStatus.selectedIndex = 1;
		int childIndex2 = base.ui.com_Tab.list.GetChildIndex(selectTab);
		if (childIndex2 >= 0 && childIndex2 < StaticConfigure.Item.UIs.Count && StaticConfigure.Item.UIs[childIndex2].MenuSubType == ItemUIMenuSubType.Chest && selectTab.redPoint.selectedIndex == 1)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.bag.ClearNewChestSetSilent();
		}
		RefreshList();
	}

	private void OnQualitySorted()
	{
		RefreshList();
	}

	private void RendererTab(int index, GObject item)
	{
		if (item is UIButton_SwitchTab uIButton_SwitchTab)
		{
			uIButton_SwitchTab.title = StaticConfigure.Item.UIs[index].FilterNameID.GetLocal(UIStringType.Item);
			if (StaticConfigure.Item.UIs[index].MenuSubType == ItemUIMenuSubType.Chest)
			{
				uIButton_SwitchTab.redPoint.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.bag.GetSystemStatus() ? 1 : 0);
			}
		}
	}

	private void RendererItem(int index, GObject item)
	{
		if (item is UIBag_Com_Item uIBag_Com_Item)
		{
			int num = _currentList[index];
			BagItem item2 = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItem(num);
			uIBag_Com_Item.recycle.selectedIndex = (((object)item2.config.EndDateTime != null) ? 1 : 0);
			uIBag_Com_Item.data = num;
			if (uIBag_Com_Item.btn_item is UICom_LitItem uICom_LitItem)
			{
				uICom_LitItem.qualityType.selectedIndex = (int)item2.config.QualityType;
				uICom_LitItem.loader_Icon.url = item2.config.ShowIcon;
				string arg = ((item2.count.Value >= 0) ? "[color=#FFFFFF]" : "[color=#FF0000]");
				uICom_LitItem.txt_itemNum.text = $"{arg}{Mathf.Abs(item2.count.Value)}[/color]";
				string text = ((item2.count.Value >= 0) ? "x" : "[color=#FF0000]-[/color]");
				uICom_LitItem.txt_Symbol.text = text;
			}
		}
	}

	private async void OnClickItem(EventContext context)
	{
		if (context.data is UIBag_Com_Item uIBag_Com_Item)
		{
			BagItem item = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItem((int)uIBag_Com_Item.data);
			await SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(item.config.Id, item.count.Value, _Usable: true);
		}
	}

	private void OnBagRedSignalChanged(bool value)
	{
		base.ui.com_Tab.list.numItems = StaticConfigure.Item.UIs.Count;
	}

	private void RefreshByBagChange()
	{
		base.ui.com_Tab.list.numItems = StaticConfigure.Item.UIs.Count;
		RefreshList();
	}

	private void RefreshList()
	{
		base.ui.list_Items.numItems = 0;
		int selectedIndex = base.ui.com_Tab.list.selectedIndex;
		_currentList.Clear();
		base.ui.com_Tab.txt_Select.text = StaticConfigure.Item.UIs[selectedIndex].FilterNameID.GetLocal(UIStringType.Item);
		foreach (ItemType itemType in StaticConfigure.Item.UIs[selectedIndex].ItemTypes)
		{
			List<int> itemsByType = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemsByType(itemType);
			if (itemsByType == null || itemsByType.Count == 0)
			{
				continue;
			}
			if (itemType == ItemType.Activity)
			{
				for (int num = itemsByType.Count - 1; num >= 0; num--)
				{
					ItemInfoConfigure itemInfoConfigure = itemsByType[num].GetItemInfoConfigure();
					if (itemInfoConfigure == null || !itemInfoConfigure.IsClientShow)
					{
						itemsByType.RemoveAt(num);
					}
				}
			}
			_currentList.AddRange(itemsByType);
		}
		_currentList.Sort(OnItemSorted);
		base.ui.list_Items.numItems = _currentList.Count;
		base.ui.list_Items.RefreshVirtualList();
	}

	private int OnItemSorted(int x, int y)
	{
		BagItem item = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItem(x);
		BagItem item2 = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItem(y);
		if (item.config.QualityType == item2.config.QualityType)
		{
			return item.config.Id - item2.config.Id;
		}
		if (base.ui.sortType.selectedIndex == 0)
		{
			return item2.config.QualityType - item.config.QualityType;
		}
		return item.config.QualityType - item2.config.QualityType;
	}
}
