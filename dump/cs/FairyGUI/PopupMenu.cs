using System;
using UnityEngine;

namespace FairyGUI;

public class PopupMenu : EventDispatcher
{
	protected GComponent _contentPane;

	protected GList _list;

	protected GObject _expandingItem;

	private PopupMenu _parentMenu;

	private TimerCallback _showSubMenu;

	private TimerCallback _closeSubMenu;

	private EventListener _onPopup;

	private EventListener _onClose;

	public int visibleItemCount;

	public bool hideOnClickItem;

	public bool autoSize;

	private const string EVENT_TYPE = "PopupMenuItemClick";

	public EventListener onPopup => _onPopup ?? (_onPopup = new EventListener(this, "onPopup"));

	public EventListener onClose => _onClose ?? (_onClose = new EventListener(this, "onClose"));

	public int itemCount => _list.numChildren;

	public GComponent contentPane => _contentPane;

	public GList list => _list;

	public PopupMenu()
	{
		Create(null);
	}

	public PopupMenu(string resourceURL)
	{
		Create(resourceURL);
	}

	private void Create(string resourceURL)
	{
		if (resourceURL == null)
		{
			resourceURL = UIConfig.popupMenu;
			if (resourceURL == null)
			{
				Debug.LogError("FairyGUI: UIConfig.popupMenu not defined");
				return;
			}
		}
		_contentPane = UIPackage.CreateObjectFromURL(resourceURL).asCom;
		_contentPane.onAddedToStage.Add(__addedToStage);
		_contentPane.onRemovedFromStage.Add(__removeFromStage);
		_contentPane.focusable = false;
		_list = _contentPane.GetChild("list").asList;
		_list.RemoveChildrenToPool();
		_list.AddRelation(_contentPane, RelationType.Width);
		_list.RemoveRelation(_contentPane, RelationType.Height);
		_contentPane.AddRelation(_list, RelationType.Height);
		_list.onClickItem.Add(__clickItem);
		hideOnClickItem = true;
		_showSubMenu = __showSubMenu;
		_closeSubMenu = CloseSubMenu;
	}

	public GButton AddItem(string caption, EventCallback0 callback)
	{
		GButton gButton = CreateItem(caption, callback);
		_list.AddChild(gButton);
		return gButton;
	}

	public GButton AddItem(string caption, EventCallback1 callback)
	{
		GButton gButton = CreateItem(caption, callback);
		_list.AddChild(gButton);
		return gButton;
	}

	public GButton AddItemAt(string caption, int index, EventCallback1 callback)
	{
		GButton gButton = CreateItem(caption, callback);
		_list.AddChildAt(gButton, index);
		return gButton;
	}

	public GButton AddItemAt(string caption, int index, EventCallback0 callback)
	{
		GButton gButton = CreateItem(caption, callback);
		_list.AddChildAt(gButton, index);
		return gButton;
	}

	private GButton CreateItem(string caption, Delegate callback)
	{
		GButton asButton = _list.GetFromPool(_list.defaultItem).asButton;
		asButton.title = caption;
		asButton.grayed = false;
		Controller controller = asButton.GetController("checked");
		if (controller != null)
		{
			controller.selectedIndex = 0;
		}
		asButton.RemoveEventListeners("PopupMenuItemClick");
		if (callback is EventCallback0)
		{
			asButton.AddEventListener("PopupMenuItemClick", (EventCallback0)callback);
		}
		else
		{
			asButton.AddEventListener("PopupMenuItemClick", (EventCallback1)callback);
		}
		asButton.onRollOver.Add(__rollOver);
		asButton.onRollOut.Add(__rollOut);
		return asButton;
	}

	public void AddSeperator()
	{
		AddSeperator(-1);
	}

	public void AddSeperator(int index)
	{
		if (UIConfig.popupMenu_seperator == null)
		{
			Debug.LogError("FairyGUI: UIConfig.popupMenu_seperator not defined");
			return;
		}
		if (index == -1)
		{
			_list.AddItemFromPool(UIConfig.popupMenu_seperator);
			return;
		}
		GObject fromPool = _list.GetFromPool(UIConfig.popupMenu_seperator);
		_list.AddChildAt(fromPool, index);
	}

	public string GetItemName(int index)
	{
		return _list.GetChildAt(index).asButton.name;
	}

	public void SetItemText(string name, string caption)
	{
		_list.GetChild(name).asButton.title = caption;
	}

	public void SetItemVisible(string name, bool visible)
	{
		GButton asButton = _list.GetChild(name).asButton;
		if (asButton.visible != visible)
		{
			asButton.visible = visible;
			_list.SetBoundsChangedFlag();
		}
	}

	public void SetItemGrayed(string name, bool grayed)
	{
		_list.GetChild(name).asButton.grayed = grayed;
	}

	public void SetItemCheckable(string name, bool checkable)
	{
		Controller controller = _list.GetChild(name).asButton.GetController("checked");
		if (controller == null)
		{
			return;
		}
		if (checkable)
		{
			if (controller.selectedIndex == 0)
			{
				controller.selectedIndex = 1;
			}
		}
		else
		{
			controller.selectedIndex = 0;
		}
	}

	public void SetItemChecked(string name, bool check)
	{
		Controller controller = _list.GetChild(name).asButton.GetController("checked");
		if (controller != null)
		{
			controller.selectedIndex = ((!check) ? 1 : 2);
		}
	}

	[Obsolete("Use IsItemChecked instead")]
	public bool isItemChecked(string name)
	{
		return IsItemChecked(name);
	}

	public bool IsItemChecked(string name)
	{
		Controller controller = _list.GetChild(name).asButton.GetController("checked");
		if (controller != null)
		{
			return controller.selectedIndex == 2;
		}
		return false;
	}

	public void RemoveItem(string name)
	{
		GComponent asCom = _list.GetChild(name).asCom;
		if (asCom != null)
		{
			asCom.RemoveEventListeners("PopupMenuItemClick");
			if (asCom.data is PopupMenu)
			{
				((PopupMenu)asCom.data).Dispose();
				asCom.data = null;
			}
			int childIndex = _list.GetChildIndex(asCom);
			_list.RemoveChildToPoolAt(childIndex);
		}
	}

	public void ClearItems()
	{
		_list.RemoveChildrenToPool();
	}

	public void Dispose()
	{
		int numChildren = _list.numChildren;
		for (int i = 0; i < numChildren; i++)
		{
			GObject childAt = _list.GetChildAt(i);
			if (childAt.data is PopupMenu)
			{
				((PopupMenu)childAt.data).Dispose();
			}
		}
		_contentPane.Dispose();
	}

	public void Show()
	{
		Show(null, PopupDirection.Auto);
	}

	public void Show(GObject target)
	{
		Show(target, PopupDirection.Auto, null);
	}

	[Obsolete]
	public void Show(GObject target, object downward)
	{
		Show(target, (downward != null) ? ((!(bool)downward) ? PopupDirection.Up : PopupDirection.Down) : PopupDirection.Auto, null);
	}

	public void Show(GObject target, PopupDirection dir)
	{
		Show(target, PopupDirection.Auto, null);
	}

	public void Show(GObject target, PopupDirection dir, PopupMenu parentMenu)
	{
		((target != null) ? target.root : GRoot.inst).ShowPopup(contentPane, (target is GRoot) ? null : target, dir);
		_parentMenu = parentMenu;
	}

	public void Hide()
	{
		if (contentPane.parent != null)
		{
			((GRoot)contentPane.parent).HidePopup(contentPane);
		}
	}

	private void ShowSubMenu(GObject item)
	{
		_expandingItem = item;
		PopupMenu obj = item.data as PopupMenu;
		if (item is GButton)
		{
			((GButton)item).selected = true;
		}
		obj.Show(item, PopupDirection.Auto, this);
		Vector2 vector = contentPane.LocalToRoot(new Vector2(item.x + item.width - 5f, item.y - 5f), item.root);
		obj.contentPane.position = vector;
	}

	private void CloseSubMenu(object param)
	{
		if (!contentPane.isDisposed && _expandingItem != null)
		{
			if (_expandingItem is GButton)
			{
				((GButton)_expandingItem).selected = false;
			}
			PopupMenu popupMenu = (PopupMenu)_expandingItem.data;
			if (popupMenu != null)
			{
				_expandingItem = null;
				popupMenu.Hide();
			}
		}
	}

	private void __clickItem(EventContext context)
	{
		GButton asButton = ((GObject)context.data).asButton;
		if (asButton == null)
		{
			return;
		}
		if (asButton.grayed)
		{
			_list.selectedIndex = -1;
			return;
		}
		Controller controller = asButton.GetController("checked");
		if (controller != null && controller.selectedIndex != 0)
		{
			if (controller.selectedIndex == 1)
			{
				controller.selectedIndex = 2;
			}
			else
			{
				controller.selectedIndex = 1;
			}
		}
		if (hideOnClickItem)
		{
			if (_parentMenu != null)
			{
				_parentMenu.Hide();
			}
			Hide();
		}
		asButton.DispatchEvent("PopupMenuItemClick", asButton);
	}

	private void __addedToStage()
	{
		DispatchEvent("onPopup", null);
		if (autoSize)
		{
			_list.EnsureBoundsCorrect();
			int numChildren = _list.numChildren;
			float num = -1000f;
			for (int i = 0; i < numChildren; i++)
			{
				GButton asButton = _list.GetChildAt(i).asButton;
				if (asButton == null)
				{
					continue;
				}
				GTextField textField = asButton.GetTextField();
				if (textField != null)
				{
					float num2 = textField.textWidth - textField.width;
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			if (contentPane.width + num > (float)contentPane.initWidth)
			{
				contentPane.width += num;
			}
			else
			{
				contentPane.width = contentPane.initWidth;
			}
		}
		_list.selectedIndex = -1;
		_list.ResizeToFit((visibleItemCount > 0) ? visibleItemCount : int.MaxValue, 10);
	}

	private void __removeFromStage()
	{
		_parentMenu = null;
		if (_expandingItem != null)
		{
			Timers.inst.Add(0f, 1, _closeSubMenu);
		}
		DispatchEvent("onClose", null);
	}

	private void __rollOver(EventContext context)
	{
		GObject gObject = (GObject)context.sender;
		if (gObject.data is PopupMenu || _expandingItem != null)
		{
			Timers.inst.Add(0.1f, 1, _showSubMenu, gObject);
		}
	}

	private void __showSubMenu(object param)
	{
		if (contentPane.isDisposed)
		{
			return;
		}
		GObject gObject = (GObject)param;
		if (contentPane.root == null)
		{
			return;
		}
		if (_expandingItem != null)
		{
			if (_expandingItem == gObject)
			{
				return;
			}
			CloseSubMenu(null);
		}
		if (gObject.data is PopupMenu)
		{
			ShowSubMenu(gObject);
		}
	}

	private void __rollOut(EventContext context)
	{
		if (_expandingItem == null)
		{
			return;
		}
		Timers.inst.Remove(_showSubMenu);
		if (contentPane.root != null)
		{
			PopupMenu popupMenu = (PopupMenu)_expandingItem.data;
			Vector2 vector = popupMenu.contentPane.GlobalToLocal(context.inputEvent.position);
			if (vector.x >= 0f && vector.y >= 0f && vector.x < popupMenu.contentPane.width && vector.y < popupMenu.contentPane.height)
			{
				return;
			}
		}
		CloseSubMenu(null);
	}
}
