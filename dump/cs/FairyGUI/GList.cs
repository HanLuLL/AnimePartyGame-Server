using System;
using System.Collections.Generic;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class GList : GComponent
{
	private class ItemInfo
	{
		public Vector2 size;

		public GObject obj;

		public uint updateFlag;

		public bool selected;
	}

	public string defaultItem;

	public bool foldInvisibleItems;

	public ListSelectionMode selectionMode;

	public ListItemRenderer itemRenderer;

	public ListItemProvider itemProvider;

	public bool scrollItemToViewOnClick;

	private ListLayoutType _layout;

	private int _lineCount;

	private int _columnCount;

	private int _lineGap;

	private int _columnGap;

	private AlignType _align;

	private VertAlignType _verticalAlign;

	private bool _autoResizeItem;

	private Controller _selectionController;

	private GObjectPool _pool;

	private int _lastSelectedIndex;

	private EventListener _onClickItem;

	private EventListener _onRightClickItem;

	private bool _virtual;

	private bool _loop;

	private int _numItems;

	private int _realNumItems;

	private int _firstIndex;

	private int _curLineItemCount;

	private int _curLineItemCount2;

	private Vector2 _itemSize;

	private int _virtualListChanged;

	private uint itemInfoVer;

	private int _miscFlags;

	private List<ItemInfo> _virtualItems;

	private EventCallback1 _itemClickDelegate;

	public EventListener onClickItem => _onClickItem ?? (_onClickItem = new EventListener(this, "onClickItem"));

	public EventListener onRightClickItem => _onRightClickItem ?? (_onRightClickItem = new EventListener(this, "onRightClickItem"));

	public ListLayoutType layout
	{
		get
		{
			return _layout;
		}
		set
		{
			if (_layout != value)
			{
				_layout = value;
				SetBoundsChangedFlag();
				if (_virtual)
				{
					SetVirtualListChangedFlag(layoutChanged: true);
				}
			}
		}
	}

	public int lineCount
	{
		get
		{
			return _lineCount;
		}
		set
		{
			if (_lineCount == value)
			{
				return;
			}
			_lineCount = value;
			if (_layout == ListLayoutType.FlowVertical || _layout == ListLayoutType.Pagination)
			{
				SetBoundsChangedFlag();
				if (_virtual)
				{
					SetVirtualListChangedFlag(layoutChanged: true);
				}
			}
		}
	}

	public int columnCount
	{
		get
		{
			return _columnCount;
		}
		set
		{
			if (_columnCount == value)
			{
				return;
			}
			_columnCount = value;
			if (_layout == ListLayoutType.FlowHorizontal || _layout == ListLayoutType.Pagination)
			{
				SetBoundsChangedFlag();
				if (_virtual)
				{
					SetVirtualListChangedFlag(layoutChanged: true);
				}
			}
		}
	}

	public int lineGap
	{
		get
		{
			return _lineGap;
		}
		set
		{
			if (_lineGap != value)
			{
				_lineGap = value;
				SetBoundsChangedFlag();
				if (_virtual)
				{
					SetVirtualListChangedFlag(layoutChanged: true);
				}
			}
		}
	}

	public int columnGap
	{
		get
		{
			return _columnGap;
		}
		set
		{
			if (_columnGap != value)
			{
				_columnGap = value;
				SetBoundsChangedFlag();
				if (_virtual)
				{
					SetVirtualListChangedFlag(layoutChanged: true);
				}
			}
		}
	}

	public AlignType align
	{
		get
		{
			return _align;
		}
		set
		{
			if (_align != value)
			{
				_align = value;
				SetBoundsChangedFlag();
				if (_virtual)
				{
					SetVirtualListChangedFlag(layoutChanged: true);
				}
			}
		}
	}

	public VertAlignType verticalAlign
	{
		get
		{
			return _verticalAlign;
		}
		set
		{
			if (_verticalAlign != value)
			{
				_verticalAlign = value;
				SetBoundsChangedFlag();
				if (_virtual)
				{
					SetVirtualListChangedFlag(layoutChanged: true);
				}
			}
		}
	}

	public bool autoResizeItem
	{
		get
		{
			return _autoResizeItem;
		}
		set
		{
			if (_autoResizeItem != value)
			{
				_autoResizeItem = value;
				SetBoundsChangedFlag();
				if (_virtual)
				{
					SetVirtualListChangedFlag(layoutChanged: true);
				}
			}
		}
	}

	public Vector2 defaultItemSize
	{
		get
		{
			return _itemSize;
		}
		set
		{
			_itemSize = value;
			if (_virtual)
			{
				if (_layout == ListLayoutType.SingleColumn || _layout == ListLayoutType.FlowHorizontal)
				{
					base.scrollPane.scrollStep = _itemSize.y;
				}
				else
				{
					base.scrollPane.scrollStep = _itemSize.x;
				}
				SetVirtualListChangedFlag(layoutChanged: true);
			}
		}
	}

	public GObjectPool itemPool => _pool;

	public int selectedIndex
	{
		get
		{
			if (_virtual)
			{
				int realNumItems = _realNumItems;
				for (int i = 0; i < realNumItems; i++)
				{
					ItemInfo itemInfo = _virtualItems[i];
					if ((itemInfo.obj is GButton && ((GButton)itemInfo.obj).selected) || (itemInfo.obj == null && itemInfo.selected))
					{
						if (_loop)
						{
							return i % _numItems;
						}
						return i;
					}
				}
			}
			else
			{
				int count = _children.Count;
				for (int j = 0; j < count; j++)
				{
					GButton gButton = _children[j].asButton;
					if (gButton != null && gButton.selected)
					{
						return j;
					}
				}
			}
			return -1;
		}
		set
		{
			if (value >= 0 && value < numItems)
			{
				if (selectionMode != ListSelectionMode.Single)
				{
					ClearSelection();
				}
				AddSelection(value, scrollItToView: false);
			}
			else
			{
				ClearSelection();
			}
		}
	}

	public Controller selectionController
	{
		get
		{
			return _selectionController;
		}
		set
		{
			_selectionController = value;
		}
	}

	public GObject touchItem
	{
		get
		{
			GObject gObject = GRoot.inst.touchTarget;
			for (GObject gObject2 = gObject.parent; gObject2 != null; gObject2 = gObject2.parent)
			{
				if (gObject2 == this)
				{
					return gObject;
				}
				gObject = gObject2;
			}
			return null;
		}
	}

	public bool isVirtual => _virtual;

	public int numItems
	{
		get
		{
			if (_virtual)
			{
				return _numItems;
			}
			return _children.Count;
		}
		set
		{
			if (_virtual)
			{
				if (itemRenderer == null)
				{
					throw new Exception("FairyGUI: Set itemRenderer first!");
				}
				_numItems = value;
				if (_loop)
				{
					_realNumItems = _numItems * 6;
				}
				else
				{
					_realNumItems = _numItems;
				}
				int count = _virtualItems.Count;
				if (_realNumItems > count)
				{
					for (int i = count; i < _realNumItems; i++)
					{
						ItemInfo itemInfo = new ItemInfo();
						itemInfo.size = _itemSize;
						_virtualItems.Add(itemInfo);
					}
				}
				else
				{
					for (int j = _realNumItems; j < count; j++)
					{
						_virtualItems[j].selected = false;
					}
				}
				if (_virtualListChanged != 0)
				{
					Timers.inst.Remove(RefreshVirtualList);
				}
				RefreshVirtualList(null);
				return;
			}
			int count2 = _children.Count;
			if (value > count2)
			{
				for (int k = count2; k < value; k++)
				{
					if (itemProvider == null)
					{
						AddItemFromPool();
					}
					else
					{
						AddItemFromPool(itemProvider(k));
					}
				}
			}
			else
			{
				RemoveChildrenToPool(value, count2);
			}
			if (itemRenderer != null)
			{
				for (int l = 0; l < value; l++)
				{
					itemRenderer(l, GetChildAt(l));
				}
			}
		}
	}

	public GList()
	{
		_trackBounds = true;
		base.opaque = true;
		scrollItemToViewOnClick = true;
		base.container = new Container();
		base.rootContainer.AddChild(base.container);
		base.rootContainer.gameObject.name = "GList";
		_pool = new GObjectPool(base.container.cachedTransform);
		_itemClickDelegate = __clickItem;
	}

	public override void Dispose()
	{
		_pool.Clear();
		if (_virtualListChanged != 0)
		{
			Timers.inst.Remove(RefreshVirtualList);
		}
		_selectionController = null;
		scrollItemToViewOnClick = false;
		itemRenderer = null;
		itemProvider = null;
		base.Dispose();
	}

	public GObject GetFromPool(string url)
	{
		if (string.IsNullOrEmpty(url))
		{
			url = defaultItem;
		}
		GObject gObject = _pool.GetObject(url);
		if (gObject != null)
		{
			gObject.visible = true;
		}
		return gObject;
	}

	private void ReturnToPool(GObject obj)
	{
		_pool.ReturnObject(obj);
	}

	public GObject AddItemFromPool()
	{
		GObject fromPool = GetFromPool(null);
		return AddChild(fromPool);
	}

	public GObject AddItemFromPool(string url)
	{
		GObject fromPool = GetFromPool(url);
		return AddChild(fromPool);
	}

	public override GObject AddChildAt(GObject child, int index)
	{
		base.AddChildAt(child, index);
		if (child is GButton)
		{
			GButton obj = (GButton)child;
			obj.selected = false;
			obj.changeStateOnClick = false;
		}
		child.onClick.Add(_itemClickDelegate);
		child.onRightClick.Add(_itemClickDelegate);
		return child;
	}

	public override GObject RemoveChildAt(int index, bool dispose)
	{
		GObject gObject = base.RemoveChildAt(index, dispose);
		gObject.onClick.Remove(_itemClickDelegate);
		gObject.onRightClick.Remove(_itemClickDelegate);
		return gObject;
	}

	public void RemoveChildToPoolAt(int index)
	{
		GObject obj = RemoveChildAt(index);
		ReturnToPool(obj);
	}

	public void RemoveChildToPool(GObject child)
	{
		RemoveChild(child);
		ReturnToPool(child);
	}

	public void RemoveChildrenToPool()
	{
		RemoveChildrenToPool(0, -1);
	}

	public void RemoveChildrenToPool(int beginIndex, int endIndex)
	{
		if (endIndex < 0 || endIndex >= _children.Count)
		{
			endIndex = _children.Count - 1;
		}
		for (int i = beginIndex; i <= endIndex; i++)
		{
			RemoveChildToPoolAt(beginIndex);
		}
	}

	public List<int> GetSelection()
	{
		return GetSelection(null);
	}

	public List<int> GetSelection(List<int> result)
	{
		if (result == null)
		{
			result = new List<int>();
		}
		if (_virtual)
		{
			int realNumItems = _realNumItems;
			for (int i = 0; i < realNumItems; i++)
			{
				ItemInfo itemInfo = _virtualItems[i];
				if ((!(itemInfo.obj is GButton) || !((GButton)itemInfo.obj).selected) && (itemInfo.obj != null || !itemInfo.selected))
				{
					continue;
				}
				int item = i;
				if (_loop)
				{
					item = i % _numItems;
					if (result.Contains(item))
					{
						continue;
					}
				}
				result.Add(item);
			}
		}
		else
		{
			int count = _children.Count;
			for (int j = 0; j < count; j++)
			{
				GButton gButton = _children[j].asButton;
				if (gButton != null && gButton.selected)
				{
					result.Add(j);
				}
			}
		}
		return result;
	}

	public void AddSelection(int index, bool scrollItToView)
	{
		if (selectionMode == ListSelectionMode.None)
		{
			return;
		}
		CheckVirtualList();
		if (selectionMode == ListSelectionMode.Single)
		{
			ClearSelection();
		}
		if (scrollItToView)
		{
			ScrollToView(index);
		}
		_lastSelectedIndex = index;
		GButton gButton = null;
		if (_virtual)
		{
			ItemInfo itemInfo = _virtualItems[index];
			if (itemInfo.obj != null)
			{
				gButton = itemInfo.obj.asButton;
			}
			itemInfo.selected = true;
		}
		else
		{
			gButton = GetChildAt(index).asButton;
		}
		if (gButton != null && !gButton.selected)
		{
			gButton.selected = true;
			UpdateSelectionController(index);
		}
	}

	public void RemoveSelection(int index)
	{
		if (selectionMode == ListSelectionMode.None)
		{
			return;
		}
		GButton gButton = null;
		if (_virtual)
		{
			ItemInfo itemInfo = _virtualItems[index];
			if (itemInfo.obj != null)
			{
				gButton = itemInfo.obj.asButton;
			}
			itemInfo.selected = false;
		}
		else
		{
			gButton = GetChildAt(index).asButton;
		}
		if (gButton != null)
		{
			gButton.selected = false;
		}
	}

	public void ClearSelection()
	{
		if (_virtual)
		{
			int realNumItems = _realNumItems;
			for (int i = 0; i < realNumItems; i++)
			{
				ItemInfo itemInfo = _virtualItems[i];
				if (itemInfo.obj is GButton)
				{
					((GButton)itemInfo.obj).selected = false;
				}
				itemInfo.selected = false;
			}
			return;
		}
		int count = _children.Count;
		for (int j = 0; j < count; j++)
		{
			GButton gButton = _children[j].asButton;
			if (gButton != null)
			{
				gButton.selected = false;
			}
		}
	}

	private void ClearSelectionExcept(GObject g)
	{
		if (_virtual)
		{
			int realNumItems = _realNumItems;
			for (int i = 0; i < realNumItems; i++)
			{
				ItemInfo itemInfo = _virtualItems[i];
				if (itemInfo.obj != g)
				{
					if (itemInfo.obj is GButton)
					{
						((GButton)itemInfo.obj).selected = false;
					}
					itemInfo.selected = false;
				}
			}
			return;
		}
		int count = _children.Count;
		for (int j = 0; j < count; j++)
		{
			GButton gButton = _children[j].asButton;
			if (gButton != null && gButton != g)
			{
				gButton.selected = false;
			}
		}
	}

	public void SelectAll()
	{
		CheckVirtualList();
		int num = -1;
		if (_virtual)
		{
			int realNumItems = _realNumItems;
			for (int i = 0; i < realNumItems; i++)
			{
				ItemInfo itemInfo = _virtualItems[i];
				if (itemInfo.obj is GButton && !((GButton)itemInfo.obj).selected)
				{
					((GButton)itemInfo.obj).selected = true;
					num = i;
				}
				itemInfo.selected = true;
			}
		}
		else
		{
			int count = _children.Count;
			for (int j = 0; j < count; j++)
			{
				GButton gButton = _children[j].asButton;
				if (gButton != null && !gButton.selected)
				{
					gButton.selected = true;
					num = j;
				}
			}
		}
		if (num != -1)
		{
			UpdateSelectionController(num);
		}
	}

	public void SelectNone()
	{
		ClearSelection();
	}

	public void SelectReverse()
	{
		CheckVirtualList();
		int num = -1;
		if (_virtual)
		{
			int realNumItems = _realNumItems;
			for (int i = 0; i < realNumItems; i++)
			{
				ItemInfo itemInfo = _virtualItems[i];
				if (itemInfo.obj is GButton)
				{
					((GButton)itemInfo.obj).selected = !((GButton)itemInfo.obj).selected;
					if (((GButton)itemInfo.obj).selected)
					{
						num = i;
					}
				}
				itemInfo.selected = !itemInfo.selected;
			}
		}
		else
		{
			int count = _children.Count;
			for (int j = 0; j < count; j++)
			{
				GButton gButton = _children[j].asButton;
				if (gButton != null)
				{
					gButton.selected = !gButton.selected;
					if (gButton.selected)
					{
						num = j;
					}
				}
			}
		}
		if (num != -1)
		{
			UpdateSelectionController(num);
		}
	}

	public void EnableSelectionFocusEvents(bool enabled)
	{
		if ((_miscFlags & 2) != 0 != enabled)
		{
			if (enabled)
			{
				_miscFlags |= 2;
				base.tabStopChildren = true;
				base.onFocusIn.Add(NotifySelection);
				base.onFocusOut.Add(NotifySelection);
			}
			else
			{
				_miscFlags &= 253;
				base.onFocusIn.Remove(NotifySelection);
				base.onFocusOut.Remove(NotifySelection);
			}
		}
	}

	private void NotifySelection(EventContext context)
	{
		string strType = ((context.type == "onFocusIn") ? "onListFocusIn" : "onListFocusOut");
		int count = _children.Count;
		for (int i = 0; i < count; i++)
		{
			GButton gButton = _children[i].asButton;
			if (gButton != null && gButton.selected)
			{
				gButton.DispatchEvent(strType);
			}
		}
	}

	public void EnableArrowKeyNavigation(bool enabled)
	{
		if (enabled)
		{
			base.tabStopChildren = true;
			base.onKeyDown.Add(__keydown);
		}
		else
		{
			base.tabStopChildren = false;
			base.onKeyDown.Remove(__keydown);
		}
	}

	private void __keydown(EventContext context)
	{
		int num = -1;
		switch (context.inputEvent.keyCode)
		{
		case KeyCode.LeftArrow:
			num = HandleArrowKey(7);
			break;
		case KeyCode.RightArrow:
			num = HandleArrowKey(3);
			break;
		case KeyCode.UpArrow:
			num = HandleArrowKey(1);
			break;
		case KeyCode.DownArrow:
			num = HandleArrowKey(5);
			break;
		}
		if (num != -1)
		{
			num = ItemIndexToChildIndex(num);
			if (num != -1)
			{
				DispatchItemEvent(GetChildAt(num), context);
			}
			context.StopPropagation();
		}
	}

	public int HandleArrowKey(int dir)
	{
		int num = selectedIndex;
		if (num == -1)
		{
			return -1;
		}
		int num2 = num;
		switch (dir)
		{
		case 1:
			if (_layout == ListLayoutType.SingleColumn || _layout == ListLayoutType.FlowVertical)
			{
				num2--;
			}
			else
			{
				if (_layout != ListLayoutType.FlowHorizontal && _layout != ListLayoutType.Pagination)
				{
					break;
				}
				if (_virtual)
				{
					num2 -= _curLineItemCount;
					break;
				}
				GObject gObject3 = _children[num2];
				int num5 = 0;
				int num6;
				for (num6 = num2 - 1; num6 >= 0; num6--)
				{
					GObject gObject4 = _children[num6];
					if (gObject4.y != gObject3.y)
					{
						gObject3 = gObject4;
						break;
					}
					num5++;
				}
				while (num6 >= 0)
				{
					if (_children[num6].y != gObject3.y)
					{
						num2 = num6 + num5 + 1;
						break;
					}
					num6--;
				}
			}
			break;
		case 3:
			if (_layout == ListLayoutType.SingleRow || _layout == ListLayoutType.FlowHorizontal || _layout == ListLayoutType.Pagination)
			{
				num2++;
			}
			else
			{
				if (_layout != ListLayoutType.FlowVertical)
				{
					break;
				}
				if (_virtual)
				{
					num2 += _curLineItemCount;
					break;
				}
				GObject gObject7 = _children[num2];
				int num8 = 0;
				int count2 = _children.Count;
				int j;
				for (j = num2 + 1; j < count2; j++)
				{
					GObject gObject8 = _children[j];
					if (gObject8.x != gObject7.x)
					{
						gObject7 = gObject8;
						break;
					}
					num8++;
				}
				for (; j < count2; j++)
				{
					if (_children[j].x != gObject7.x)
					{
						num2 = j - num8 - 1;
						break;
					}
				}
			}
			break;
		case 5:
			if (_layout == ListLayoutType.SingleColumn || _layout == ListLayoutType.FlowVertical)
			{
				num2++;
			}
			else
			{
				if (_layout != ListLayoutType.FlowHorizontal && _layout != ListLayoutType.Pagination)
				{
					break;
				}
				if (_virtual)
				{
					num2 += _curLineItemCount;
					break;
				}
				GObject gObject5 = _children[num2];
				int num7 = 0;
				int count = _children.Count;
				int i;
				for (i = num2 + 1; i < count; i++)
				{
					GObject gObject6 = _children[i];
					if (gObject6.y != gObject5.y)
					{
						gObject5 = gObject6;
						break;
					}
					num7++;
				}
				for (; i < count; i++)
				{
					if (_children[i].y != gObject5.y)
					{
						num2 = i - num7 - 1;
						break;
					}
				}
			}
			break;
		case 7:
			if (_layout == ListLayoutType.SingleRow || _layout == ListLayoutType.FlowHorizontal || _layout == ListLayoutType.Pagination)
			{
				num2--;
			}
			else
			{
				if (_layout != ListLayoutType.FlowVertical)
				{
					break;
				}
				if (_virtual)
				{
					num2 -= _curLineItemCount;
					break;
				}
				GObject gObject = _children[num2];
				int num3 = 0;
				int num4;
				for (num4 = num2 - 1; num4 >= 0; num4--)
				{
					GObject gObject2 = _children[num4];
					if (gObject2.x != gObject.x)
					{
						gObject = gObject2;
						break;
					}
					num3++;
				}
				while (num4 >= 0)
				{
					if (_children[num4].x != gObject.x)
					{
						num2 = num4 + num3 + 1;
						break;
					}
					num4--;
				}
			}
			break;
		}
		if (num2 != num && num2 >= 0 && num2 < numItems)
		{
			ClearSelection();
			AddSelection(num2, scrollItToView: true);
			return num2;
		}
		return -1;
	}

	private void __clickItem(EventContext context)
	{
		GObject gObject = context.sender as GObject;
		if (gObject is GButton && selectionMode != ListSelectionMode.None)
		{
			SetSelectionOnEvent(gObject, context.inputEvent);
		}
		if (base.scrollPane != null && scrollItemToViewOnClick)
		{
			base.scrollPane.ScrollToView(gObject, ani: true);
		}
		DispatchItemEvent(gObject, context);
	}

	protected virtual void DispatchItemEvent(GObject item, EventContext context)
	{
		if (context.type == item.onRightClick.type)
		{
			DispatchEvent("onRightClickItem", item);
		}
		else
		{
			DispatchEvent("onClickItem", item);
		}
	}

	private void SetSelectionOnEvent(GObject item, InputEvent evt)
	{
		bool flag = false;
		GButton gButton = (GButton)item;
		int num = ChildIndexToItemIndex(GetChildIndex(item));
		if (selectionMode == ListSelectionMode.Single)
		{
			if (!gButton.selected)
			{
				ClearSelectionExcept(gButton);
				gButton.selected = true;
			}
		}
		else if (evt.shift)
		{
			if (!gButton.selected)
			{
				if (_lastSelectedIndex != -1)
				{
					int num2 = Math.Min(_lastSelectedIndex, num);
					int val = Math.Max(_lastSelectedIndex, num);
					val = Math.Min(val, numItems - 1);
					if (_virtual)
					{
						for (int i = num2; i <= val; i++)
						{
							ItemInfo itemInfo = _virtualItems[i];
							if (itemInfo.obj is GButton)
							{
								((GButton)itemInfo.obj).selected = true;
							}
							itemInfo.selected = true;
						}
					}
					else
					{
						for (int j = num2; j <= val; j++)
						{
							GButton gButton2 = GetChildAt(j).asButton;
							if (gButton2 != null && !gButton2.selected)
							{
								gButton2.selected = true;
							}
						}
					}
					flag = true;
				}
				else
				{
					gButton.selected = true;
				}
			}
		}
		else if (evt.ctrlOrCmd || selectionMode == ListSelectionMode.Multiple_SingleClick)
		{
			gButton.selected = !gButton.selected;
		}
		else if (!gButton.selected)
		{
			ClearSelectionExcept(gButton);
			gButton.selected = true;
		}
		else if (evt.button == 0)
		{
			ClearSelectionExcept(gButton);
		}
		if (!flag)
		{
			_lastSelectedIndex = num;
		}
		if (gButton.selected)
		{
			UpdateSelectionController(num);
		}
	}

	public void ResizeToFit()
	{
		ResizeToFit(int.MaxValue, 0);
	}

	public void ResizeToFit(int itemCount)
	{
		ResizeToFit(itemCount, 0);
	}

	public void ResizeToFit(int itemCount, int minSize)
	{
		EnsureBoundsCorrect();
		int num = numItems;
		if (itemCount > num)
		{
			itemCount = num;
		}
		if (_virtual)
		{
			int num2 = Mathf.CeilToInt((float)itemCount / (float)_curLineItemCount);
			if (_layout == ListLayoutType.SingleColumn || _layout == ListLayoutType.FlowHorizontal)
			{
				base.viewHeight = (float)num2 * _itemSize.y + (float)(Math.Max(0, num2 - 1) * _lineGap);
			}
			else
			{
				base.viewWidth = (float)num2 * _itemSize.x + (float)(Math.Max(0, num2 - 1) * _columnGap);
			}
			return;
		}
		if (itemCount == 0)
		{
			if (_layout == ListLayoutType.SingleColumn || _layout == ListLayoutType.FlowHorizontal)
			{
				base.viewHeight = minSize;
			}
			else
			{
				base.viewWidth = minSize;
			}
			return;
		}
		int num3 = itemCount - 1;
		GObject gObject = null;
		while (num3 >= 0)
		{
			gObject = GetChildAt(num3);
			if (!foldInvisibleItems || gObject.visible)
			{
				break;
			}
			num3--;
		}
		if (num3 < 0)
		{
			if (_layout == ListLayoutType.SingleColumn || _layout == ListLayoutType.FlowHorizontal)
			{
				base.viewHeight = minSize;
			}
			else
			{
				base.viewWidth = minSize;
			}
		}
		else if (_layout == ListLayoutType.SingleColumn || _layout == ListLayoutType.FlowHorizontal)
		{
			float num4 = gObject.y + gObject.height;
			if (num4 < (float)minSize)
			{
				num4 = minSize;
			}
			base.viewHeight = num4;
		}
		else
		{
			float num4 = gObject.x + gObject.width;
			if (num4 < (float)minSize)
			{
				num4 = minSize;
			}
			base.viewWidth = num4;
		}
	}

	protected override void HandleSizeChanged()
	{
		base.HandleSizeChanged();
		SetBoundsChangedFlag();
		if (_virtual)
		{
			SetVirtualListChangedFlag(layoutChanged: true);
		}
	}

	public override void HandleControllerChanged(Controller c)
	{
		base.HandleControllerChanged(c);
		if (_selectionController == c)
		{
			selectedIndex = c.selectedIndex;
		}
	}

	private void UpdateSelectionController(int index)
	{
		if (_selectionController != null && !_selectionController.changing && index < _selectionController.pageCount)
		{
			Controller controller = _selectionController;
			_selectionController = null;
			controller.selectedIndex = index;
			_selectionController = controller;
		}
	}

	public void ScrollToView(int index)
	{
		ScrollToView(index, ani: false);
	}

	public void ScrollToView(int index, bool ani)
	{
		ScrollToView(index, ani, setFirst: false);
	}

	public void ScrollToView(int index, bool ani, bool setFirst)
	{
		if (_virtual)
		{
			if (_numItems == 0)
			{
				return;
			}
			CheckVirtualList();
			if (index >= _virtualItems.Count)
			{
				throw new Exception("Invalid child index: " + index + ">" + _virtualItems.Count);
			}
			if (_loop)
			{
				index = Mathf.FloorToInt((float)_firstIndex / (float)_numItems) * _numItems + index;
			}
			ItemInfo itemInfo = _virtualItems[index];
			Rect rect;
			if (_layout == ListLayoutType.SingleColumn || _layout == ListLayoutType.FlowHorizontal)
			{
				float num = 0f;
				for (int i = _curLineItemCount - 1; i < index; i += _curLineItemCount)
				{
					num += _virtualItems[i].size.y + (float)_lineGap;
				}
				rect = new Rect(0f, num, _itemSize.x, itemInfo.size.y);
			}
			else if (_layout == ListLayoutType.SingleRow || _layout == ListLayoutType.FlowVertical)
			{
				float num2 = 0f;
				for (int j = _curLineItemCount - 1; j < index; j += _curLineItemCount)
				{
					num2 += _virtualItems[j].size.x + (float)_columnGap;
				}
				rect = new Rect(num2, 0f, itemInfo.size.x, _itemSize.y);
			}
			else
			{
				int num3 = index / (_curLineItemCount * _curLineItemCount2);
				rect = new Rect((float)num3 * base.viewWidth + (float)(index % _curLineItemCount) * (itemInfo.size.x + (float)_columnGap), (float)(index / _curLineItemCount % _curLineItemCount2) * (itemInfo.size.y + (float)_lineGap), itemInfo.size.x, itemInfo.size.y);
			}
			if (base.scrollPane != null)
			{
				base.scrollPane.ScrollToView(rect, ani, setFirst);
			}
			else if (base.parent != null && base.parent.scrollPane != null)
			{
				base.parent.scrollPane.ScrollToView(TransformRect(rect, base.parent), ani, setFirst);
			}
		}
		else
		{
			GObject childAt = GetChildAt(index);
			if (base.scrollPane != null)
			{
				base.scrollPane.ScrollToView(childAt, ani, setFirst);
			}
			else if (base.parent != null && base.parent.scrollPane != null)
			{
				base.parent.scrollPane.ScrollToView(childAt, ani, setFirst);
			}
		}
	}

	public override int GetFirstChildInView()
	{
		return ChildIndexToItemIndex(base.GetFirstChildInView());
	}

	public int ChildIndexToItemIndex(int index)
	{
		if (!_virtual)
		{
			return index;
		}
		if (_layout == ListLayoutType.Pagination)
		{
			for (int i = _firstIndex; i < _realNumItems; i++)
			{
				if (_virtualItems[i].obj != null)
				{
					index--;
					if (index < 0)
					{
						return i;
					}
				}
			}
			return index;
		}
		index += _firstIndex;
		if (_loop && _numItems > 0)
		{
			index %= _numItems;
		}
		return index;
	}

	public int ItemIndexToChildIndex(int index)
	{
		if (!_virtual)
		{
			return index;
		}
		if (_layout == ListLayoutType.Pagination)
		{
			return GetChildIndex(_virtualItems[index].obj);
		}
		if (_loop && _numItems > 0)
		{
			int num = _firstIndex % _numItems;
			index = ((index < num) ? (_numItems - num + index) : (index - num));
		}
		else
		{
			index -= _firstIndex;
		}
		return index;
	}

	public void SetVirtual()
	{
		SetVirtual(loop: false);
	}

	public void SetVirtualAndLoop()
	{
		SetVirtual(loop: true);
	}

	private void SetVirtual(bool loop)
	{
		if (_virtual)
		{
			return;
		}
		if (base.scrollPane == null)
		{
			Debug.LogError("FairyGUI: Virtual list must be scrollable!");
		}
		if (loop)
		{
			if (_layout == ListLayoutType.FlowHorizontal || _layout == ListLayoutType.FlowVertical)
			{
				Debug.LogError("FairyGUI: Loop list is not supported for FlowHorizontal or FlowVertical layout!");
			}
			base.scrollPane.bouncebackEffect = false;
		}
		_virtual = true;
		_loop = loop;
		_virtualItems = new List<ItemInfo>();
		RemoveChildrenToPool();
		if (_itemSize.x == 0f || _itemSize.y == 0f)
		{
			GObject fromPool = GetFromPool(null);
			if (fromPool == null)
			{
				Debug.LogError("FairyGUI: Virtual List must have a default list item resource.");
				_itemSize = new Vector2(100f, 100f);
			}
			else
			{
				_itemSize = fromPool.size;
				_itemSize.x = Mathf.CeilToInt(_itemSize.x);
				_itemSize.y = Mathf.CeilToInt(_itemSize.y);
				ReturnToPool(fromPool);
			}
		}
		if (_layout == ListLayoutType.SingleColumn || _layout == ListLayoutType.FlowHorizontal)
		{
			base.scrollPane.scrollStep = _itemSize.y;
			if (_loop)
			{
				base.scrollPane._loop = 2;
			}
		}
		else
		{
			base.scrollPane.scrollStep = _itemSize.x;
			if (_loop)
			{
				base.scrollPane._loop = 1;
			}
		}
		base.scrollPane.onScroll.AddCapture(__scrolled);
		SetVirtualListChangedFlag(layoutChanged: true);
	}

	public void RefreshVirtualList()
	{
		if (!_virtual)
		{
			throw new Exception("FairyGUI: not virtual list");
		}
		SetVirtualListChangedFlag(layoutChanged: false);
	}

	private void CheckVirtualList()
	{
		if (_virtualListChanged != 0)
		{
			RefreshVirtualList(null);
			Timers.inst.Remove(RefreshVirtualList);
		}
	}

	private void SetVirtualListChangedFlag(bool layoutChanged)
	{
		if (layoutChanged)
		{
			_virtualListChanged = 2;
		}
		else if (_virtualListChanged == 0)
		{
			_virtualListChanged = 1;
		}
		Timers.inst.CallLater(RefreshVirtualList);
	}

	private void RefreshVirtualList(object param)
	{
		bool num = _virtualListChanged == 2;
		_virtualListChanged = 0;
		_miscFlags |= 1;
		if (num)
		{
			if (_layout == ListLayoutType.SingleColumn || _layout == ListLayoutType.SingleRow)
			{
				_curLineItemCount = 1;
			}
			else if (_layout == ListLayoutType.FlowHorizontal)
			{
				if (_columnCount > 0)
				{
					_curLineItemCount = _columnCount;
				}
				else
				{
					_curLineItemCount = Mathf.FloorToInt((base.scrollPane.viewWidth + (float)_columnGap) / (_itemSize.x + (float)_columnGap));
					if (_curLineItemCount <= 0)
					{
						_curLineItemCount = 1;
					}
				}
			}
			else if (_layout == ListLayoutType.FlowVertical)
			{
				if (_lineCount > 0)
				{
					_curLineItemCount = _lineCount;
				}
				else
				{
					_curLineItemCount = Mathf.FloorToInt((base.scrollPane.viewHeight + (float)_lineGap) / (_itemSize.y + (float)_lineGap));
					if (_curLineItemCount <= 0)
					{
						_curLineItemCount = 1;
					}
				}
			}
			else
			{
				if (_columnCount > 0)
				{
					_curLineItemCount = _columnCount;
				}
				else
				{
					_curLineItemCount = Mathf.FloorToInt((base.scrollPane.viewWidth + (float)_columnGap) / (_itemSize.x + (float)_columnGap));
					if (_curLineItemCount <= 0)
					{
						_curLineItemCount = 1;
					}
				}
				if (_lineCount > 0)
				{
					_curLineItemCount2 = _lineCount;
				}
				else
				{
					_curLineItemCount2 = Mathf.FloorToInt((base.scrollPane.viewHeight + (float)_lineGap) / (_itemSize.y + (float)_lineGap));
					if (_curLineItemCount2 <= 0)
					{
						_curLineItemCount2 = 1;
					}
				}
			}
		}
		float num2 = 0f;
		float num3 = 0f;
		if (_realNumItems > 0)
		{
			int num4 = Mathf.CeilToInt((float)_realNumItems / (float)_curLineItemCount) * _curLineItemCount;
			int num5 = Math.Min(_curLineItemCount, _realNumItems);
			if (_layout == ListLayoutType.SingleColumn || _layout == ListLayoutType.FlowHorizontal)
			{
				for (int i = 0; i < num4; i += _curLineItemCount)
				{
					num2 += _virtualItems[i].size.y + (float)_lineGap;
				}
				if (num2 > 0f)
				{
					num2 -= (float)_lineGap;
				}
				if (_autoResizeItem)
				{
					num3 = base.scrollPane.viewWidth;
				}
				else
				{
					for (int j = 0; j < num5; j++)
					{
						num3 += _virtualItems[j].size.x + (float)_columnGap;
					}
					if (num3 > 0f)
					{
						num3 -= (float)_columnGap;
					}
				}
			}
			else if (_layout == ListLayoutType.SingleRow || _layout == ListLayoutType.FlowVertical)
			{
				for (int k = 0; k < num4; k += _curLineItemCount)
				{
					num3 += _virtualItems[k].size.x + (float)_columnGap;
				}
				if (num3 > 0f)
				{
					num3 -= (float)_columnGap;
				}
				if (_autoResizeItem)
				{
					num2 = base.scrollPane.viewHeight;
				}
				else
				{
					for (int l = 0; l < num5; l++)
					{
						num2 += _virtualItems[l].size.y + (float)_lineGap;
					}
					if (num2 > 0f)
					{
						num2 -= (float)_lineGap;
					}
				}
			}
			else
			{
				num3 = (float)Mathf.CeilToInt((float)num4 / (float)(_curLineItemCount * _curLineItemCount2)) * base.viewWidth;
				num2 = base.viewHeight;
			}
		}
		HandleAlign(num3, num2);
		base.scrollPane.SetContentSize(num3, num2);
		_miscFlags &= 254;
		HandleScroll(forceUpdate: true);
	}

	private void __scrolled(EventContext context)
	{
		HandleScroll(forceUpdate: false);
	}

	private int GetIndexOnPos1(ref float pos, bool forceUpdate)
	{
		if (_realNumItems < _curLineItemCount)
		{
			pos = 0f;
			return 0;
		}
		if (base.numChildren > 0 && !forceUpdate)
		{
			float num = GetChildAt(0).y;
			if (num + (float)((_lineGap <= 0) ? (-_lineGap) : 0) > pos)
			{
				for (int num2 = _firstIndex - _curLineItemCount; num2 >= 0; num2 -= _curLineItemCount)
				{
					num -= _virtualItems[num2].size.y + (float)_lineGap;
					if (num <= pos)
					{
						pos = num;
						return num2;
					}
				}
				pos = 0f;
				return 0;
			}
			float num3 = ((_lineGap > 0) ? _lineGap : 0);
			for (int i = _firstIndex; i < _realNumItems; i += _curLineItemCount)
			{
				float num4 = num + _virtualItems[i].size.y;
				if (num4 + num3 > pos)
				{
					pos = num;
					return i;
				}
				num = num4 + (float)_lineGap;
			}
			pos = num;
			return _realNumItems - _curLineItemCount;
		}
		float num5 = 0f;
		float num6 = ((_lineGap > 0) ? _lineGap : 0);
		for (int j = 0; j < _realNumItems; j += _curLineItemCount)
		{
			float num7 = num5 + _virtualItems[j].size.y;
			if (num7 + num6 > pos)
			{
				pos = num5;
				return j;
			}
			num5 = num7 + (float)_lineGap;
		}
		pos = num5;
		return _realNumItems - _curLineItemCount;
	}

	private int GetIndexOnPos2(ref float pos, bool forceUpdate)
	{
		if (_realNumItems < _curLineItemCount)
		{
			pos = 0f;
			return 0;
		}
		if (base.numChildren > 0 && !forceUpdate)
		{
			float num = GetChildAt(0).x;
			if (num + (float)((_columnGap <= 0) ? (-_columnGap) : 0) > pos)
			{
				for (int num2 = _firstIndex - _curLineItemCount; num2 >= 0; num2 -= _curLineItemCount)
				{
					num -= _virtualItems[num2].size.x + (float)_columnGap;
					if (num <= pos)
					{
						pos = num;
						return num2;
					}
				}
				pos = 0f;
				return 0;
			}
			float num3 = ((_columnGap > 0) ? _columnGap : 0);
			for (int i = _firstIndex; i < _realNumItems; i += _curLineItemCount)
			{
				float num4 = num + _virtualItems[i].size.x;
				if (num4 + num3 > pos)
				{
					pos = num;
					return i;
				}
				num = num4 + (float)_columnGap;
			}
			pos = num;
			return _realNumItems - _curLineItemCount;
		}
		float num5 = 0f;
		float num6 = ((_columnGap > 0) ? _columnGap : 0);
		for (int j = 0; j < _realNumItems; j += _curLineItemCount)
		{
			float num7 = num5 + _virtualItems[j].size.x;
			if (num7 + num6 > pos)
			{
				pos = num5;
				return j;
			}
			num5 = num7 + (float)_columnGap;
		}
		pos = num5;
		return _realNumItems - _curLineItemCount;
	}

	private int GetIndexOnPos3(ref float pos, bool forceUpdate)
	{
		if (_realNumItems < _curLineItemCount)
		{
			pos = 0f;
			return 0;
		}
		float num = base.viewWidth;
		int num2 = Mathf.FloorToInt(pos / num);
		int num3 = num2 * (_curLineItemCount * _curLineItemCount2);
		float num4 = (float)num2 * num;
		float num5 = ((_columnGap > 0) ? _columnGap : 0);
		for (int i = 0; i < _curLineItemCount; i++)
		{
			float num6 = num4 + _virtualItems[num3 + i].size.x;
			if (num6 + num5 > pos)
			{
				pos = num4;
				return num3 + i;
			}
			num4 = num6 + (float)_columnGap;
		}
		pos = num4;
		return num3 + _curLineItemCount - 1;
	}

	private void HandleScroll(bool forceUpdate)
	{
		if ((_miscFlags & 1) != 0)
		{
			return;
		}
		if (_layout == ListLayoutType.SingleColumn || _layout == ListLayoutType.FlowHorizontal)
		{
			int num = 0;
			while (HandleScroll1(forceUpdate))
			{
				num++;
				forceUpdate = false;
				if (num > 20)
				{
					Debug.Log("FairyGUI: list will never be filled as the item renderer function always returns a different size.");
					break;
				}
			}
			HandleArchOrder1();
		}
		else if (_layout == ListLayoutType.SingleRow || _layout == ListLayoutType.FlowVertical)
		{
			int num2 = 0;
			while (HandleScroll2(forceUpdate))
			{
				num2++;
				forceUpdate = false;
				if (num2 > 20)
				{
					Debug.Log("FairyGUI: list will never be filled as the item renderer function always returns a different size.");
					break;
				}
			}
			HandleArchOrder2();
		}
		else
		{
			HandleScroll3(forceUpdate);
		}
		_boundsChanged = false;
	}

	private bool HandleScroll1(bool forceUpdate)
	{
		float pos = base.scrollPane.scrollingPosY;
		float num = pos + base.scrollPane.viewHeight;
		bool flag = num == base.scrollPane.contentHeight;
		int indexOnPos = GetIndexOnPos1(ref pos, forceUpdate);
		if (indexOnPos == _firstIndex && !forceUpdate)
		{
			return false;
		}
		int firstIndex = _firstIndex;
		_firstIndex = indexOnPos;
		int i = indexOnPos;
		bool flag2 = firstIndex > indexOnPos;
		int num2 = base.numChildren;
		int num3 = firstIndex + num2 - 1;
		int num4 = (flag2 ? num3 : firstIndex);
		float num5 = 0f;
		float num6 = pos;
		float num7 = 0f;
		float num8 = 0f;
		string text = defaultItem;
		int num9 = (int)((base.scrollPane.viewWidth - (float)(_columnGap * (_curLineItemCount - 1))) / (float)_curLineItemCount);
		itemInfoVer++;
		for (; i < _realNumItems; i++)
		{
			if (!flag && !(num6 < num))
			{
				break;
			}
			ItemInfo itemInfo = _virtualItems[i];
			if (itemInfo.obj == null || forceUpdate)
			{
				if (itemProvider != null)
				{
					text = itemProvider(i % _numItems);
					if (text == null)
					{
						text = defaultItem;
					}
					text = UIPackage.NormalizeURL(text);
				}
				if (itemInfo.obj != null && itemInfo.obj.resourceURL != text)
				{
					if (itemInfo.obj is GButton)
					{
						itemInfo.selected = ((GButton)itemInfo.obj).selected;
					}
					RemoveChildToPool(itemInfo.obj);
					itemInfo.obj = null;
				}
			}
			bool flag3;
			if (itemInfo.obj == null)
			{
				if (flag2)
				{
					for (int num10 = num4; num10 >= firstIndex; num10--)
					{
						ItemInfo itemInfo2 = _virtualItems[num10];
						if (itemInfo2.obj != null && itemInfo2.updateFlag != itemInfoVer && itemInfo2.obj.resourceURL == text)
						{
							if (itemInfo2.obj is GButton)
							{
								itemInfo2.selected = ((GButton)itemInfo2.obj).selected;
							}
							itemInfo.obj = itemInfo2.obj;
							itemInfo2.obj = null;
							if (num10 == num4)
							{
								num4--;
							}
							break;
						}
					}
				}
				else
				{
					for (int j = num4; j <= num3; j++)
					{
						ItemInfo itemInfo3 = _virtualItems[j];
						if (itemInfo3.obj != null && itemInfo3.updateFlag != itemInfoVer && itemInfo3.obj.resourceURL == text)
						{
							if (itemInfo3.obj is GButton)
							{
								itemInfo3.selected = ((GButton)itemInfo3.obj).selected;
							}
							itemInfo.obj = itemInfo3.obj;
							itemInfo3.obj = null;
							if (j == num4)
							{
								num4++;
							}
							break;
						}
					}
				}
				if (itemInfo.obj != null)
				{
					SetChildIndex(itemInfo.obj, flag2 ? (i - indexOnPos) : base.numChildren);
				}
				else
				{
					itemInfo.obj = _pool.GetObject(text);
					if (flag2)
					{
						AddChildAt(itemInfo.obj, i - indexOnPos);
					}
					else
					{
						AddChild(itemInfo.obj);
					}
				}
				if (itemInfo.obj is GButton)
				{
					((GButton)itemInfo.obj).selected = itemInfo.selected;
				}
				flag3 = true;
			}
			else
			{
				flag3 = forceUpdate;
			}
			if (flag3)
			{
				if (_autoResizeItem && (_layout == ListLayoutType.SingleColumn || _columnCount > 0))
				{
					itemInfo.obj.SetSize(num9, itemInfo.obj.height, ignorePivot: true);
				}
				itemRenderer(i % _numItems, itemInfo.obj);
				if (i % _curLineItemCount == 0)
				{
					num7 += (float)Mathf.CeilToInt(itemInfo.obj.size.y) - itemInfo.size.y;
					if (i == indexOnPos && firstIndex > indexOnPos)
					{
						num8 = (float)Mathf.CeilToInt(itemInfo.obj.size.y) - itemInfo.size.y;
					}
				}
				itemInfo.size.x = Mathf.CeilToInt(itemInfo.obj.size.x);
				itemInfo.size.y = Mathf.CeilToInt(itemInfo.obj.size.y);
			}
			itemInfo.updateFlag = itemInfoVer;
			itemInfo.obj.SetXY(num5, num6);
			if (i == indexOnPos)
			{
				num += itemInfo.size.y;
			}
			num5 += itemInfo.size.x + (float)_columnGap;
			if (i % _curLineItemCount == _curLineItemCount - 1)
			{
				num5 = 0f;
				num6 += itemInfo.size.y + (float)_lineGap;
			}
		}
		for (int k = 0; k < num2; k++)
		{
			ItemInfo itemInfo4 = _virtualItems[firstIndex + k];
			if (itemInfo4.updateFlag != itemInfoVer && itemInfo4.obj != null)
			{
				if (itemInfo4.obj is GButton)
				{
					itemInfo4.selected = ((GButton)itemInfo4.obj).selected;
				}
				RemoveChildToPool(itemInfo4.obj);
				itemInfo4.obj = null;
			}
		}
		num2 = _children.Count;
		for (int l = 0; l < num2; l++)
		{
			GObject obj = _virtualItems[indexOnPos + l].obj;
			if (_children[l] != obj)
			{
				SetChildIndex(obj, l);
			}
		}
		if (num7 != 0f || num8 != 0f)
		{
			base.scrollPane.ChangeContentSizeOnScrolling(0f, num7, 0f, num8);
		}
		if (i > 0 && base.numChildren > 0 && base.container.y <= 0f && GetChildAt(0).y > 0f - base.container.y)
		{
			return true;
		}
		return false;
	}

	private bool HandleScroll2(bool forceUpdate)
	{
		float pos = base.scrollPane.scrollingPosX;
		float num = pos + base.scrollPane.viewWidth;
		bool flag = pos == base.scrollPane.contentWidth;
		int indexOnPos = GetIndexOnPos2(ref pos, forceUpdate);
		if (indexOnPos == _firstIndex && !forceUpdate)
		{
			return false;
		}
		int firstIndex = _firstIndex;
		_firstIndex = indexOnPos;
		int i = indexOnPos;
		bool flag2 = firstIndex > indexOnPos;
		int num2 = base.numChildren;
		int num3 = firstIndex + num2 - 1;
		int num4 = (flag2 ? num3 : firstIndex);
		float num5 = pos;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		string text = defaultItem;
		int num9 = (int)((base.scrollPane.viewHeight - (float)(_lineGap * (_curLineItemCount - 1))) / (float)_curLineItemCount);
		itemInfoVer++;
		for (; i < _realNumItems; i++)
		{
			if (!flag && !(num5 < num))
			{
				break;
			}
			ItemInfo itemInfo = _virtualItems[i];
			if (itemInfo.obj == null || forceUpdate)
			{
				if (itemProvider != null)
				{
					text = itemProvider(i % _numItems);
					if (text == null)
					{
						text = defaultItem;
					}
					text = UIPackage.NormalizeURL(text);
				}
				if (itemInfo.obj != null && itemInfo.obj.resourceURL != text)
				{
					if (itemInfo.obj is GButton)
					{
						itemInfo.selected = ((GButton)itemInfo.obj).selected;
					}
					RemoveChildToPool(itemInfo.obj);
					itemInfo.obj = null;
				}
			}
			bool flag3;
			if (itemInfo.obj == null)
			{
				if (flag2)
				{
					for (int num10 = num4; num10 >= firstIndex; num10--)
					{
						ItemInfo itemInfo2 = _virtualItems[num10];
						if (itemInfo2.obj != null && itemInfo2.updateFlag != itemInfoVer && itemInfo2.obj.resourceURL == text)
						{
							if (itemInfo2.obj is GButton)
							{
								itemInfo2.selected = ((GButton)itemInfo2.obj).selected;
							}
							itemInfo.obj = itemInfo2.obj;
							itemInfo2.obj = null;
							if (num10 == num4)
							{
								num4--;
							}
							break;
						}
					}
				}
				else
				{
					for (int j = num4; j <= num3; j++)
					{
						ItemInfo itemInfo3 = _virtualItems[j];
						if (itemInfo3.obj != null && itemInfo3.updateFlag != itemInfoVer && itemInfo3.obj.resourceURL == text)
						{
							if (itemInfo3.obj is GButton)
							{
								itemInfo3.selected = ((GButton)itemInfo3.obj).selected;
							}
							itemInfo.obj = itemInfo3.obj;
							itemInfo3.obj = null;
							if (j == num4)
							{
								num4++;
							}
							break;
						}
					}
				}
				if (itemInfo.obj != null)
				{
					SetChildIndex(itemInfo.obj, flag2 ? (i - indexOnPos) : base.numChildren);
				}
				else
				{
					itemInfo.obj = _pool.GetObject(text);
					if (flag2)
					{
						AddChildAt(itemInfo.obj, i - indexOnPos);
					}
					else
					{
						AddChild(itemInfo.obj);
					}
				}
				if (itemInfo.obj is GButton)
				{
					((GButton)itemInfo.obj).selected = itemInfo.selected;
				}
				flag3 = true;
			}
			else
			{
				flag3 = forceUpdate;
			}
			if (flag3)
			{
				if (_autoResizeItem && (_layout == ListLayoutType.SingleRow || _lineCount > 0))
				{
					itemInfo.obj.SetSize(itemInfo.obj.width, num9, ignorePivot: true);
				}
				itemRenderer(i % _numItems, itemInfo.obj);
				if (i % _curLineItemCount == 0)
				{
					num7 += (float)Mathf.CeilToInt(itemInfo.obj.size.x) - itemInfo.size.x;
					if (i == indexOnPos && firstIndex > indexOnPos)
					{
						num8 = (float)Mathf.CeilToInt(itemInfo.obj.size.x) - itemInfo.size.x;
					}
				}
				itemInfo.size.x = Mathf.CeilToInt(itemInfo.obj.size.x);
				itemInfo.size.y = Mathf.CeilToInt(itemInfo.obj.size.y);
			}
			itemInfo.updateFlag = itemInfoVer;
			itemInfo.obj.SetXY(num5, num6);
			if (i == indexOnPos)
			{
				num += itemInfo.size.x;
			}
			num6 += itemInfo.size.y + (float)_lineGap;
			if (i % _curLineItemCount == _curLineItemCount - 1)
			{
				num6 = 0f;
				num5 += itemInfo.size.x + (float)_columnGap;
			}
		}
		for (int k = 0; k < num2; k++)
		{
			ItemInfo itemInfo4 = _virtualItems[firstIndex + k];
			if (itemInfo4.updateFlag != itemInfoVer && itemInfo4.obj != null)
			{
				if (itemInfo4.obj is GButton)
				{
					itemInfo4.selected = ((GButton)itemInfo4.obj).selected;
				}
				RemoveChildToPool(itemInfo4.obj);
				itemInfo4.obj = null;
			}
		}
		num2 = _children.Count;
		for (int l = 0; l < num2; l++)
		{
			GObject obj = _virtualItems[indexOnPos + l].obj;
			if (_children[l] != obj)
			{
				SetChildIndex(obj, l);
			}
		}
		if (num7 != 0f || num8 != 0f)
		{
			base.scrollPane.ChangeContentSizeOnScrolling(num7, 0f, num8, 0f);
		}
		if (i > 0 && base.numChildren > 0 && base.container.x <= 0f && GetChildAt(0).x > 0f - base.container.x)
		{
			return true;
		}
		return false;
	}

	private void HandleScroll3(bool forceUpdate)
	{
		float pos = base.scrollPane.scrollingPosX;
		int indexOnPos = GetIndexOnPos3(ref pos, forceUpdate);
		if (indexOnPos == _firstIndex && !forceUpdate)
		{
			return;
		}
		int firstIndex = _firstIndex;
		_firstIndex = indexOnPos;
		int i = firstIndex;
		int count = _virtualItems.Count;
		int num = _curLineItemCount * _curLineItemCount2;
		int num2 = indexOnPos % _curLineItemCount;
		float num3 = base.viewWidth;
		int num4 = indexOnPos / num * num;
		int num5 = num4 + num * 2;
		string url = defaultItem;
		int num6 = (int)((base.scrollPane.viewWidth - (float)(_columnGap * (_curLineItemCount - 1))) / (float)_curLineItemCount);
		int num7 = (int)((base.scrollPane.viewHeight - (float)(_lineGap * (_curLineItemCount2 - 1))) / (float)_curLineItemCount2);
		itemInfoVer++;
		for (int j = num4; j < num5; j++)
		{
			if (j >= _realNumItems)
			{
				continue;
			}
			int num8 = j % _curLineItemCount;
			if (j - num4 < num)
			{
				if (num8 < num2)
				{
					continue;
				}
			}
			else if (num8 > num2)
			{
				continue;
			}
			_virtualItems[j].updateFlag = itemInfoVer;
		}
		GObject child = null;
		int num9 = 0;
		for (int k = num4; k < num5; k++)
		{
			if (k >= _realNumItems)
			{
				continue;
			}
			ItemInfo itemInfo = _virtualItems[k];
			if (itemInfo.updateFlag != itemInfoVer)
			{
				continue;
			}
			bool flag;
			if (itemInfo.obj == null)
			{
				for (; i < count; i++)
				{
					ItemInfo itemInfo2 = _virtualItems[i];
					if (itemInfo2.obj != null && itemInfo2.updateFlag != itemInfoVer)
					{
						if (itemInfo2.obj is GButton)
						{
							itemInfo2.selected = ((GButton)itemInfo2.obj).selected;
						}
						itemInfo.obj = itemInfo2.obj;
						itemInfo2.obj = null;
						break;
					}
				}
				if (num9 == -1)
				{
					num9 = GetChildIndex(child) + 1;
				}
				if (itemInfo.obj == null)
				{
					if (itemProvider != null)
					{
						url = itemProvider(k % _numItems);
						if (url == null)
						{
							url = defaultItem;
						}
						url = UIPackage.NormalizeURL(url);
					}
					itemInfo.obj = _pool.GetObject(url);
					AddChildAt(itemInfo.obj, num9);
				}
				else
				{
					num9 = SetChildIndexBefore(itemInfo.obj, num9);
				}
				num9++;
				if (itemInfo.obj is GButton)
				{
					((GButton)itemInfo.obj).selected = itemInfo.selected;
				}
				flag = true;
			}
			else
			{
				flag = forceUpdate;
				num9 = -1;
				child = itemInfo.obj;
			}
			if (!flag)
			{
				continue;
			}
			if (_autoResizeItem)
			{
				if (_curLineItemCount == _columnCount && _curLineItemCount2 == _lineCount)
				{
					itemInfo.obj.SetSize(num6, num7, ignorePivot: true);
				}
				else if (_curLineItemCount == _columnCount)
				{
					itemInfo.obj.SetSize(num6, itemInfo.obj.height, ignorePivot: true);
				}
				else if (_curLineItemCount2 == _lineCount)
				{
					itemInfo.obj.SetSize(itemInfo.obj.width, num7, ignorePivot: true);
				}
			}
			itemRenderer(k % _numItems, itemInfo.obj);
			itemInfo.size.x = Mathf.CeilToInt(itemInfo.obj.size.x);
			itemInfo.size.y = Mathf.CeilToInt(itemInfo.obj.size.y);
		}
		float num10 = (float)(num4 / num) * num3;
		float num11 = num10;
		float num12 = 0f;
		float num13 = 0f;
		for (int l = num4; l < num5; l++)
		{
			if (l >= _realNumItems)
			{
				continue;
			}
			ItemInfo itemInfo3 = _virtualItems[l];
			if (itemInfo3.updateFlag == itemInfoVer)
			{
				itemInfo3.obj.SetXY(num11, num12);
			}
			if (itemInfo3.size.y > num13)
			{
				num13 = itemInfo3.size.y;
			}
			if (l % _curLineItemCount == _curLineItemCount - 1)
			{
				num11 = num10;
				num12 += num13 + (float)_lineGap;
				num13 = 0f;
				if (l == num4 + num - 1)
				{
					num10 += num3;
					num11 = num10;
					num12 = 0f;
				}
			}
			else
			{
				num11 += itemInfo3.size.x + (float)_columnGap;
			}
		}
		for (int m = i; m < count; m++)
		{
			ItemInfo itemInfo4 = _virtualItems[m];
			if (itemInfo4.updateFlag != itemInfoVer && itemInfo4.obj != null)
			{
				if (itemInfo4.obj is GButton)
				{
					itemInfo4.selected = ((GButton)itemInfo4.obj).selected;
				}
				RemoveChildToPool(itemInfo4.obj);
				itemInfo4.obj = null;
			}
		}
	}

	private void HandleArchOrder1()
	{
		if (base.childrenRenderOrder != ChildrenRenderOrder.Arch)
		{
			return;
		}
		float num = base.scrollPane.posY + base.viewHeight / 2f;
		float num2 = 2.1474836E+09f;
		int num3 = 0;
		int num4 = base.numChildren;
		for (int i = 0; i < num4; i++)
		{
			GObject childAt = GetChildAt(i);
			if (!foldInvisibleItems || childAt.visible)
			{
				float num5 = Mathf.Abs(num - childAt.y - childAt.height / 2f);
				if (num5 < num2)
				{
					num2 = num5;
					num3 = i;
				}
			}
		}
		base.apexIndex = num3;
	}

	private void HandleArchOrder2()
	{
		if (base.childrenRenderOrder != ChildrenRenderOrder.Arch)
		{
			return;
		}
		float num = base.scrollPane.posX + base.viewWidth / 2f;
		float num2 = 2.1474836E+09f;
		int num3 = 0;
		int num4 = base.numChildren;
		for (int i = 0; i < num4; i++)
		{
			GObject childAt = GetChildAt(i);
			if (!foldInvisibleItems || childAt.visible)
			{
				float num5 = Mathf.Abs(num - childAt.x - childAt.width / 2f);
				if (num5 < num2)
				{
					num2 = num5;
					num3 = i;
				}
			}
		}
		base.apexIndex = num3;
	}

	protected internal override void GetSnappingPosition(ref float xValue, ref float yValue)
	{
		if (_virtual)
		{
			if (_layout == ListLayoutType.SingleColumn || _layout == ListLayoutType.FlowHorizontal)
			{
				float num = yValue;
				int indexOnPos = GetIndexOnPos1(ref yValue, forceUpdate: false);
				if (indexOnPos < _virtualItems.Count && num - yValue > _virtualItems[indexOnPos].size.y / 2f && indexOnPos < _realNumItems)
				{
					yValue += _virtualItems[indexOnPos].size.y + (float)_lineGap;
				}
			}
			else if (_layout == ListLayoutType.SingleRow || _layout == ListLayoutType.FlowVertical)
			{
				float num2 = xValue;
				int indexOnPos2 = GetIndexOnPos2(ref xValue, forceUpdate: false);
				if (indexOnPos2 < _virtualItems.Count && num2 - xValue > _virtualItems[indexOnPos2].size.x / 2f && indexOnPos2 < _realNumItems)
				{
					xValue += _virtualItems[indexOnPos2].size.x + (float)_columnGap;
				}
			}
			else
			{
				float num3 = xValue;
				int indexOnPos3 = GetIndexOnPos3(ref xValue, forceUpdate: false);
				if (indexOnPos3 < _virtualItems.Count && num3 - xValue > _virtualItems[indexOnPos3].size.x / 2f && indexOnPos3 < _realNumItems)
				{
					xValue += _virtualItems[indexOnPos3].size.x + (float)_columnGap;
				}
			}
		}
		else
		{
			base.GetSnappingPosition(ref xValue, ref yValue);
		}
	}

	private void HandleAlign(float contentWidth, float contentHeight)
	{
		Vector2 zero = Vector2.zero;
		if (contentHeight < base.viewHeight)
		{
			if (_verticalAlign == VertAlignType.Middle)
			{
				zero.y = (int)((base.viewHeight - contentHeight) / 2f);
			}
			else if (_verticalAlign == VertAlignType.Bottom)
			{
				zero.y = base.viewHeight - contentHeight;
			}
		}
		if (contentWidth < base.viewWidth)
		{
			if (_align == AlignType.Center)
			{
				zero.x = (int)((base.viewWidth - contentWidth) / 2f);
			}
			else if (_align == AlignType.Right)
			{
				zero.x = base.viewWidth - contentWidth;
			}
		}
		if (zero != _alignOffset)
		{
			_alignOffset = zero;
			if (base.scrollPane != null)
			{
				base.scrollPane.AdjustMaskContainer();
			}
			else
			{
				base.container.SetXY((float)_margin.left + _alignOffset.x, (float)_margin.top + _alignOffset.y);
			}
		}
	}

	protected override void UpdateBounds()
	{
		if (_virtual)
		{
			return;
		}
		int count = _children.Count;
		int num = 0;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = base.viewWidth;
		float num7 = base.viewHeight;
		float num8;
		float num9;
		if (_layout == ListLayoutType.SingleColumn)
		{
			for (int i = 0; i < count; i++)
			{
				GObject childAt = GetChildAt(i);
				if (!foldInvisibleItems || childAt.visible)
				{
					if (num3 != 0f)
					{
						num3 += (float)_lineGap;
					}
					childAt.y = num3;
					if (_autoResizeItem)
					{
						childAt.SetSize(num6, childAt.height, ignorePivot: true);
					}
					num3 += (float)Mathf.CeilToInt(childAt.height);
					if (childAt.width > num4)
					{
						num4 = childAt.width;
					}
				}
			}
			num8 = num3;
			if (num8 <= num7 && _autoResizeItem && base.scrollPane != null && base.scrollPane._displayInDemand && base.scrollPane.vtScrollBar != null)
			{
				num6 += base.scrollPane.vtScrollBar.width;
				for (int i = 0; i < count; i++)
				{
					GObject childAt = GetChildAt(i);
					if (!foldInvisibleItems || childAt.visible)
					{
						childAt.SetSize(num6, childAt.height, ignorePivot: true);
						if (childAt.width > num4)
						{
							num4 = childAt.width;
						}
					}
				}
			}
			num9 = Mathf.CeilToInt(num4);
		}
		else if (_layout == ListLayoutType.SingleRow)
		{
			for (int i = 0; i < count; i++)
			{
				GObject childAt = GetChildAt(i);
				if (!foldInvisibleItems || childAt.visible)
				{
					if (num2 != 0f)
					{
						num2 += (float)_columnGap;
					}
					childAt.x = num2;
					if (_autoResizeItem)
					{
						childAt.SetSize(childAt.width, num7, ignorePivot: true);
					}
					num2 += (float)Mathf.CeilToInt(childAt.width);
					if (childAt.height > num5)
					{
						num5 = childAt.height;
					}
				}
			}
			num9 = num2;
			if (num9 <= num6 && _autoResizeItem && base.scrollPane != null && base.scrollPane._displayInDemand && base.scrollPane.hzScrollBar != null)
			{
				num7 += base.scrollPane.hzScrollBar.height;
				for (int i = 0; i < count; i++)
				{
					GObject childAt = GetChildAt(i);
					if (!foldInvisibleItems || childAt.visible)
					{
						childAt.SetSize(childAt.width, num7, ignorePivot: true);
						if (childAt.height > num5)
						{
							num5 = childAt.height;
						}
					}
				}
			}
			num8 = Mathf.CeilToInt(num5);
		}
		else if (_layout == ListLayoutType.FlowHorizontal)
		{
			if (_autoResizeItem && _columnCount > 0)
			{
				float num10 = 0f;
				int num11 = 0;
				for (int i = 0; i < count; i++)
				{
					GObject childAt = GetChildAt(i);
					if (foldInvisibleItems && !childAt.visible)
					{
						continue;
					}
					num10 += (float)childAt.sourceWidth;
					num++;
					if (num != _columnCount && i != count - 1)
					{
						continue;
					}
					float num12 = num6 - (float)((num - 1) * _columnGap);
					float num13 = 1f;
					num2 = 0f;
					for (num = num11; num <= i; num++)
					{
						childAt = GetChildAt(num);
						if (!foldInvisibleItems || childAt.visible)
						{
							childAt.SetXY(num2, num3);
							float num14 = (float)childAt.sourceWidth / num10;
							childAt.SetSize(Mathf.Round(num14 / num13 * num12), childAt.height, ignorePivot: true);
							num12 -= childAt.width;
							num13 -= num14;
							num2 += childAt.width + (float)_columnGap;
							if (childAt.height > num5)
							{
								num5 = childAt.height;
							}
						}
					}
					num3 += (float)(Mathf.CeilToInt(num5) + _lineGap);
					num5 = 0f;
					num = 0;
					num11 = i + 1;
					num10 = 0f;
				}
				num8 = num3 + (float)Mathf.CeilToInt(num5);
				num9 = num6;
			}
			else
			{
				for (int i = 0; i < count; i++)
				{
					GObject childAt = GetChildAt(i);
					if (!foldInvisibleItems || childAt.visible)
					{
						if (num2 != 0f)
						{
							num2 += (float)_columnGap;
						}
						if ((_columnCount != 0 && num >= _columnCount) || (_columnCount == 0 && num2 + childAt.width > num6 && num5 != 0f))
						{
							num2 = 0f;
							num3 += (float)(Mathf.CeilToInt(num5) + _lineGap);
							num5 = 0f;
							num = 0;
						}
						childAt.SetXY(num2, num3);
						num2 += (float)Mathf.CeilToInt(childAt.width);
						if (num2 > num4)
						{
							num4 = num2;
						}
						if (childAt.height > num5)
						{
							num5 = childAt.height;
						}
						num++;
					}
				}
				num8 = num3 + (float)Mathf.CeilToInt(num5);
				num9 = Mathf.CeilToInt(num4);
			}
		}
		else if (_layout == ListLayoutType.FlowVertical)
		{
			if (_autoResizeItem && _lineCount > 0)
			{
				float num15 = 0f;
				int num16 = 0;
				for (int i = 0; i < count; i++)
				{
					GObject childAt = GetChildAt(i);
					if (foldInvisibleItems && !childAt.visible)
					{
						continue;
					}
					num15 += (float)childAt.sourceHeight;
					num++;
					if (num != _lineCount && i != count - 1)
					{
						continue;
					}
					float num17 = num7 - (float)((num - 1) * _lineGap);
					float num18 = 1f;
					num3 = 0f;
					for (num = num16; num <= i; num++)
					{
						childAt = GetChildAt(num);
						if (!foldInvisibleItems || childAt.visible)
						{
							childAt.SetXY(num2, num3);
							float num19 = (float)childAt.sourceHeight / num15;
							childAt.SetSize(childAt.width, Mathf.Round(num19 / num18 * num17), ignorePivot: true);
							num17 -= childAt.height;
							num18 -= num19;
							num3 += childAt.height + (float)_lineGap;
							if (childAt.width > num4)
							{
								num4 = childAt.width;
							}
						}
					}
					num2 += (float)(Mathf.CeilToInt(num4) + _columnGap);
					num4 = 0f;
					num = 0;
					num16 = i + 1;
					num15 = 0f;
				}
				num9 = num2 + (float)Mathf.CeilToInt(num4);
				num8 = num7;
			}
			else
			{
				for (int i = 0; i < count; i++)
				{
					GObject childAt = GetChildAt(i);
					if (!foldInvisibleItems || childAt.visible)
					{
						if (num3 != 0f)
						{
							num3 += (float)_lineGap;
						}
						if ((_lineCount != 0 && num >= _lineCount) || (_lineCount == 0 && num3 + childAt.height > num7 && num4 != 0f))
						{
							num3 = 0f;
							num2 += (float)(Mathf.CeilToInt(num4) + _columnGap);
							num4 = 0f;
							num = 0;
						}
						childAt.SetXY(num2, num3);
						num3 += childAt.height;
						if (num3 > num5)
						{
							num5 = num3;
						}
						if (childAt.width > num4)
						{
							num4 = childAt.width;
						}
						num++;
					}
				}
				num9 = num2 + (float)Mathf.CeilToInt(num4);
				num8 = Mathf.CeilToInt(num5);
			}
		}
		else
		{
			int num20 = 0;
			int num21 = 0;
			float num22 = 0f;
			if (_autoResizeItem && _lineCount > 0)
			{
				num22 = Mathf.Floor((num7 - (float)((_lineCount - 1) * _lineGap)) / (float)_lineCount);
			}
			if (_autoResizeItem && _columnCount > 0)
			{
				float num23 = 0f;
				int num24 = 0;
				for (int i = 0; i < count; i++)
				{
					GObject childAt = GetChildAt(i);
					if (foldInvisibleItems && !childAt.visible)
					{
						continue;
					}
					if (num == 0 && ((_lineCount != 0 && num21 >= _lineCount) || (_lineCount == 0 && num3 + ((_lineCount > 0) ? num22 : childAt.height) > num7)))
					{
						num20++;
						num3 = 0f;
						num21 = 0;
					}
					num23 += (float)childAt.sourceWidth;
					num++;
					if (num != _columnCount && i != count - 1)
					{
						continue;
					}
					float num25 = num6 - (float)((num - 1) * _columnGap);
					float num26 = 1f;
					num2 = 0f;
					for (num = num24; num <= i; num++)
					{
						childAt = GetChildAt(num);
						if (!foldInvisibleItems || childAt.visible)
						{
							childAt.SetXY((float)num20 * num6 + num2, num3);
							float num27 = (float)childAt.sourceWidth / num23;
							childAt.SetSize(Mathf.Round(num27 / num26 * num25), (_lineCount > 0) ? num22 : childAt.height, ignorePivot: true);
							num25 -= childAt.width;
							num26 -= num27;
							num2 += childAt.width + (float)_columnGap;
							if (childAt.height > num5)
							{
								num5 = childAt.height;
							}
						}
					}
					num3 += (float)(Mathf.CeilToInt(num5) + _lineGap);
					num5 = 0f;
					num = 0;
					num24 = i + 1;
					num23 = 0f;
					num21++;
				}
			}
			else
			{
				for (int i = 0; i < count; i++)
				{
					GObject childAt = GetChildAt(i);
					if (foldInvisibleItems && !childAt.visible)
					{
						continue;
					}
					if (num2 != 0f)
					{
						num2 += (float)_columnGap;
					}
					if (_autoResizeItem && _lineCount > 0)
					{
						childAt.SetSize(childAt.width, num22, ignorePivot: true);
					}
					if ((_columnCount != 0 && num >= _columnCount) || (_columnCount == 0 && num2 + childAt.width > num6 && num5 != 0f))
					{
						num2 = 0f;
						num3 += num5 + (float)_lineGap;
						num5 = 0f;
						num = 0;
						num21++;
						if ((_lineCount != 0 && num21 >= _lineCount) || (_lineCount == 0 && num3 + childAt.height > num7 && num4 != 0f))
						{
							num20++;
							num3 = 0f;
							num21 = 0;
						}
					}
					childAt.SetXY((float)num20 * num6 + num2, num3);
					num2 += (float)Mathf.CeilToInt(childAt.width);
					if (num2 > num4)
					{
						num4 = num2;
					}
					if (childAt.height > num5)
					{
						num5 = childAt.height;
					}
					num++;
				}
			}
			num8 = ((num20 > 0) ? num7 : (num3 + (float)Mathf.CeilToInt(num5)));
			num9 = (float)(num20 + 1) * num6;
		}
		HandleAlign(num9, num8);
		SetBounds(0f, 0f, num9, num8);
		InvalidateBatchingState(childChanged: true);
	}

	public override void Setup_BeforeAdd(ByteBuffer buffer, int beginPos)
	{
		base.Setup_BeforeAdd(buffer, beginPos);
		buffer.Seek(beginPos, 5);
		_layout = (ListLayoutType)buffer.ReadByte();
		selectionMode = (ListSelectionMode)buffer.ReadByte();
		_align = (AlignType)buffer.ReadByte();
		_verticalAlign = (VertAlignType)buffer.ReadByte();
		_lineGap = buffer.ReadShort();
		_columnGap = buffer.ReadShort();
		_lineCount = buffer.ReadShort();
		_columnCount = buffer.ReadShort();
		_autoResizeItem = buffer.ReadBool();
		_childrenRenderOrder = (ChildrenRenderOrder)buffer.ReadByte();
		_apexIndex = buffer.ReadShort();
		if (buffer.ReadBool())
		{
			_margin.top = buffer.ReadInt();
			_margin.bottom = buffer.ReadInt();
			_margin.left = buffer.ReadInt();
			_margin.right = buffer.ReadInt();
		}
		OverflowType overflowType = (OverflowType)buffer.ReadByte();
		if (overflowType == OverflowType.Scroll)
		{
			int num = buffer.position;
			buffer.Seek(beginPos, 7);
			SetupScroll(buffer);
			buffer.position = num;
		}
		else
		{
			SetupOverflow(overflowType);
		}
		if (buffer.ReadBool())
		{
			int num2 = buffer.ReadInt();
			int num3 = buffer.ReadInt();
			base.clipSoftness = new Vector2(num2, num3);
		}
		if (buffer.version >= 2)
		{
			scrollItemToViewOnClick = buffer.ReadBool();
			foldInvisibleItems = buffer.ReadBool();
		}
		buffer.Seek(beginPos, 8);
		defaultItem = buffer.ReadS();
		ReadItems(buffer);
	}

	protected virtual void ReadItems(ByteBuffer buffer)
	{
		int num = buffer.ReadShort();
		for (int i = 0; i < num; i++)
		{
			int num2 = buffer.ReadShort();
			num2 += buffer.position;
			string text = buffer.ReadS();
			if (text == null)
			{
				text = defaultItem;
				if (string.IsNullOrEmpty(text))
				{
					buffer.position = num2;
					continue;
				}
			}
			GObject fromPool = GetFromPool(text);
			if (fromPool != null)
			{
				AddChild(fromPool);
				SetupItem(buffer, fromPool);
			}
			buffer.position = num2;
		}
	}

	protected void SetupItem(ByteBuffer buffer, GObject obj)
	{
		string text = buffer.ReadS();
		if (text != null)
		{
			obj.text = text;
		}
		text = buffer.ReadS();
		if (text != null && obj is GButton)
		{
			(obj as GButton).selectedTitle = text;
		}
		text = buffer.ReadS();
		if (text != null)
		{
			obj.icon = text;
		}
		text = buffer.ReadS();
		if (text != null && obj is GButton)
		{
			(obj as GButton).selectedIcon = text;
		}
		text = buffer.ReadS();
		if (text != null)
		{
			obj.name = text;
		}
		if (!(obj is GComponent))
		{
			return;
		}
		int num = buffer.ReadShort();
		for (int i = 0; i < num; i++)
		{
			Controller controller = ((GComponent)obj).GetController(buffer.ReadS());
			text = buffer.ReadS();
			if (controller != null)
			{
				controller.selectedPageId = text;
			}
		}
		if (buffer.version < 2)
		{
			return;
		}
		num = buffer.ReadShort();
		for (int j = 0; j < num; j++)
		{
			string path = buffer.ReadS();
			int num2 = buffer.ReadShort();
			string text2 = buffer.ReadS();
			GObject childByPath = ((GComponent)obj).GetChildByPath(path);
			if (childByPath != null)
			{
				switch (num2)
				{
				case 0:
					childByPath.text = text2;
					break;
				case 1:
					childByPath.icon = text2;
					break;
				}
			}
		}
	}

	public override void Setup_AfterAdd(ByteBuffer buffer, int beginPos)
	{
		base.Setup_AfterAdd(buffer, beginPos);
		buffer.Seek(beginPos, 6);
		int num = buffer.ReadShort();
		if (num != -1)
		{
			_selectionController = base.parent.GetControllerAt(num);
		}
	}
}
