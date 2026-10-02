using System;
using System.Collections.Generic;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class GComboBox : GComponent
{
	public int visibleItemCount;

	public GComponent dropdown;

	public int sound;

	public float soundVolumeScale;

	protected GObject _titleObject;

	protected GObject _iconObject;

	protected GList _list;

	protected List<string> _items;

	protected List<string> _icons;

	protected List<string> _values;

	protected PopupDirection _popupDirection;

	protected Controller _selectionController;

	private bool _itemsUpdated;

	private int _selectedIndex;

	private Controller _buttonController;

	private bool _down;

	private bool _over;

	private EventListener _onChanged;

	public EventListener onChanged => _onChanged ?? (_onChanged = new EventListener(this, "onChanged"));

	public override string icon
	{
		get
		{
			if (_iconObject != null)
			{
				return _iconObject.icon;
			}
			return null;
		}
		set
		{
			if (_iconObject != null)
			{
				_iconObject.icon = value;
			}
			UpdateGear(7);
		}
	}

	public string title
	{
		get
		{
			if (_titleObject != null)
			{
				return _titleObject.text;
			}
			return null;
		}
		set
		{
			if (_titleObject != null)
			{
				_titleObject.text = value;
			}
			UpdateGear(6);
		}
	}

	public override string text
	{
		get
		{
			return title;
		}
		set
		{
			title = value;
		}
	}

	public Color titleColor
	{
		get
		{
			return GetTextField()?.color ?? Color.black;
		}
		set
		{
			GTextField textField = GetTextField();
			if (textField != null)
			{
				textField.color = value;
			}
		}
	}

	public int titleFontSize
	{
		get
		{
			return GetTextField()?.textFormat.size ?? 0;
		}
		set
		{
			GTextField textField = GetTextField();
			if (textField != null)
			{
				TextFormat textFormat = textField.textFormat;
				textFormat.size = value;
				textField.textFormat = textFormat;
			}
		}
	}

	public string[] items
	{
		get
		{
			return _items.ToArray();
		}
		set
		{
			_items.Clear();
			if (value != null)
			{
				_items.AddRange(value);
			}
			ApplyListChange();
		}
	}

	public string[] icons
	{
		get
		{
			if (_icons == null)
			{
				return null;
			}
			return _icons.ToArray();
		}
		set
		{
			iconList.Clear();
			if (value != null)
			{
				_icons.AddRange(value);
			}
			ApplyListChange();
		}
	}

	public string[] values
	{
		get
		{
			return _values.ToArray();
		}
		set
		{
			_values.Clear();
			if (value != null)
			{
				_values.AddRange(value);
			}
		}
	}

	public List<string> itemList => _items;

	public List<string> valueList => _values;

	public List<string> iconList => _icons ?? (_icons = new List<string>());

	public int selectedIndex
	{
		get
		{
			return _selectedIndex;
		}
		set
		{
			if (_selectedIndex == value)
			{
				return;
			}
			_selectedIndex = value;
			if (_selectedIndex >= 0 && _selectedIndex < _items.Count)
			{
				text = _items[_selectedIndex];
				if (_icons != null && _selectedIndex < _icons.Count)
				{
					icon = _icons[_selectedIndex];
				}
			}
			else
			{
				text = string.Empty;
				if (_icons != null)
				{
					icon = null;
				}
			}
			UpdateSelectionController();
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

	public string value
	{
		get
		{
			if (_selectedIndex >= 0 && _selectedIndex < _values.Count)
			{
				return _values[_selectedIndex];
			}
			return null;
		}
		set
		{
			int num = _values.IndexOf(value);
			if (num == -1 && value == null)
			{
				num = _values.IndexOf(string.Empty);
			}
			if (num == -1)
			{
				num = 0;
			}
			selectedIndex = num;
		}
	}

	public PopupDirection popupDirection
	{
		get
		{
			return _popupDirection;
		}
		set
		{
			_popupDirection = value;
		}
	}

	public GComboBox()
	{
		visibleItemCount = UIConfig.defaultComboBoxVisibleItemCount;
		_itemsUpdated = true;
		_selectedIndex = -1;
		_items = new List<string>();
		_values = new List<string>();
		_popupDirection = PopupDirection.Auto;
		soundVolumeScale = 1f;
	}

	public void ApplyListChange()
	{
		if (_items.Count > 0)
		{
			if (_selectedIndex >= _items.Count)
			{
				_selectedIndex = _items.Count - 1;
			}
			else if (_selectedIndex == -1)
			{
				_selectedIndex = 0;
			}
			text = _items[_selectedIndex];
			if (_icons != null && _selectedIndex < _icons.Count)
			{
				icon = _icons[_selectedIndex];
			}
		}
		else
		{
			text = string.Empty;
			if (_icons != null)
			{
				icon = null;
			}
			_selectedIndex = -1;
		}
		_itemsUpdated = true;
	}

	public GTextField GetTextField()
	{
		if (_titleObject is GTextField)
		{
			return (GTextField)_titleObject;
		}
		if (_titleObject is GLabel)
		{
			return ((GLabel)_titleObject).GetTextField();
		}
		if (_titleObject is GButton)
		{
			return ((GButton)_titleObject).GetTextField();
		}
		return null;
	}

	protected void SetState(string value)
	{
		if (_buttonController != null)
		{
			_buttonController.selectedPage = value;
		}
	}

	protected void SetCurrentState()
	{
		if (base.grayed && _buttonController != null && _buttonController.HasPage("disabled"))
		{
			SetState("disabled");
		}
		else if (dropdown != null && dropdown.parent != null)
		{
			SetState("down");
		}
		else
		{
			SetState(_over ? "over" : "up");
		}
	}

	protected override void HandleGrayedChanged()
	{
		if (_buttonController != null && _buttonController.HasPage("disabled"))
		{
			if (base.grayed)
			{
				SetState("disabled");
			}
			else
			{
				SetState("up");
			}
		}
		else
		{
			base.HandleGrayedChanged();
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

	private void UpdateSelectionController()
	{
		if (_selectionController != null && !_selectionController.changing && _selectedIndex < _selectionController.pageCount)
		{
			Controller controller = _selectionController;
			_selectionController = null;
			controller.selectedIndex = _selectedIndex;
			_selectionController = controller;
		}
	}

	public override void Dispose()
	{
		if (dropdown != null)
		{
			dropdown.Dispose();
			dropdown = null;
		}
		_selectionController = null;
		base.Dispose();
	}

	protected override void ConstructExtension(ByteBuffer buffer)
	{
		buffer.Seek(0, 6);
		_buttonController = GetController("button");
		_titleObject = GetChild("title");
		_iconObject = GetChild("icon");
		string text = buffer.ReadS();
		if (text != null)
		{
			dropdown = UIPackage.CreateObjectFromURL(text) as GComponent;
			if (dropdown == null)
			{
				Debug.LogWarning("FairyGUI: " + base.resourceURL + " should be a component.");
				return;
			}
			_list = dropdown.GetChild("list") as GList;
			if (_list == null)
			{
				Debug.LogWarning("FairyGUI: " + base.resourceURL + ": should container a list component named list.");
				return;
			}
			_list.onClickItem.Add(__clickItem);
			_list.AddRelation(dropdown, RelationType.Width);
			_list.RemoveRelation(dropdown, RelationType.Height);
			dropdown.AddRelation(_list, RelationType.Height);
			dropdown.RemoveRelation(_list, RelationType.Width);
			dropdown.SetHome(this);
		}
		base.displayObject.onRollOver.Add(__rollover);
		base.displayObject.onRollOut.Add(__rollout);
		base.displayObject.onTouchBegin.Add(__touchBegin);
		base.displayObject.onTouchEnd.Add(__touchEnd);
		base.displayObject.onClick.Add(__click);
	}

	public override void Setup_AfterAdd(ByteBuffer buffer, int beginPos)
	{
		base.Setup_AfterAdd(buffer, beginPos);
		if (!buffer.Seek(beginPos, 6) || (ObjectType)buffer.ReadByte() != packageItem.objectType)
		{
			return;
		}
		int num = buffer.ReadShort();
		string text;
		for (int i = 0; i < num; i++)
		{
			int num2 = buffer.ReadShort();
			num2 += buffer.position;
			_items.Add(buffer.ReadS());
			_values.Add(buffer.ReadS());
			text = buffer.ReadS();
			if (text != null)
			{
				if (_icons == null)
				{
					_icons = new List<string>();
				}
				_icons.Add(text);
			}
			buffer.position = num2;
		}
		text = buffer.ReadS();
		if (text != null)
		{
			this.text = text;
			_selectedIndex = _items.IndexOf(text);
		}
		else if (_items.Count > 0)
		{
			_selectedIndex = 0;
			this.text = _items[0];
		}
		else
		{
			_selectedIndex = -1;
		}
		text = buffer.ReadS();
		if (text != null)
		{
			icon = text;
		}
		if (buffer.ReadBool())
		{
			titleColor = buffer.ReadColor();
		}
		int num3 = buffer.ReadInt();
		if (num3 > 0)
		{
			visibleItemCount = num3;
		}
		_popupDirection = (PopupDirection)buffer.ReadByte();
		num3 = buffer.ReadShort();
		if (num3 >= 0)
		{
			_selectionController = base.parent.GetControllerAt(num3);
		}
		if (buffer.version >= 5)
		{
			text = buffer.ReadS();
			if (!string.IsNullOrEmpty(text))
			{
				sound = Convert.ToInt32(text);
			}
			soundVolumeScale = buffer.ReadFloat();
		}
	}

	public void UpdateDropdownList()
	{
		if (_itemsUpdated)
		{
			_itemsUpdated = false;
			RenderDropdownList();
			_list.ResizeToFit(visibleItemCount);
		}
	}

	protected void ShowDropdown()
	{
		UpdateDropdownList();
		if (_list.selectionMode == ListSelectionMode.Single)
		{
			_list.selectedIndex = -1;
		}
		dropdown.width = base.width;
		_list.EnsureBoundsCorrect();
		base.root.TogglePopup(dropdown, this, _popupDirection);
		if (dropdown.parent != null)
		{
			dropdown.displayObject.onRemovedFromStage.Add(__popupWinClosed);
			SetState("down");
		}
	}

	protected virtual void RenderDropdownList()
	{
		_list.RemoveChildrenToPool();
		int count = _items.Count;
		for (int i = 0; i < count; i++)
		{
			GObject gObject = _list.AddItemFromPool();
			gObject.text = _items[i];
			gObject.icon = ((_icons != null && i < _icons.Count) ? _icons[i] : null);
			gObject.name = ((i < _values.Count) ? _values[i] : string.Empty);
		}
	}

	private void __popupWinClosed(object obj)
	{
		dropdown.displayObject.onRemovedFromStage.Remove(__popupWinClosed);
		SetCurrentState();
		RequestFocus();
	}

	private void __clickItem(EventContext context)
	{
		if (dropdown.parent is GRoot)
		{
			((GRoot)dropdown.parent).HidePopup(dropdown);
		}
		_selectedIndex = int.MinValue;
		selectedIndex = _list.GetChildIndex((GObject)context.data);
		DispatchEvent("onChanged", null);
	}

	private void __rollover()
	{
		_over = true;
		if (!_down && (dropdown == null || dropdown.parent == null))
		{
			SetCurrentState();
		}
	}

	private void __rollout()
	{
		_over = false;
		if (!_down && (dropdown == null || dropdown.parent == null))
		{
			SetCurrentState();
		}
	}

	private void __touchBegin(EventContext context)
	{
		if (!(context.initiator is InputTextField))
		{
			_down = true;
			if (dropdown != null)
			{
				ShowDropdown();
			}
			context.CaptureTouch();
		}
	}

	private void __touchEnd(EventContext context)
	{
		if (_down)
		{
			_down = false;
			if (dropdown != null && dropdown.parent != null)
			{
				SetCurrentState();
			}
		}
	}

	private void __click()
	{
		if ((long)sound > 0L)
		{
			Stage.inst.PlayOneShotSound(sound);
		}
	}
}
