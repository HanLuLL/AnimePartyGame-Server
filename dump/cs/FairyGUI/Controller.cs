using System;
using System.Collections.Generic;
using FairyGUI.Utils;

namespace FairyGUI;

public class Controller : EventDispatcher
{
	public string name;

	internal GComponent parent;

	internal bool autoRadioGroupDepth;

	internal bool changing;

	private int _selectedIndex;

	private int _previousIndex;

	private List<string> _pageIds;

	private List<string> _pageNames;

	private List<ControllerAction> _actions;

	private EventListener _onChanged;

	private static uint _nextPageId;

	public EventListener onChanged => _onChanged ?? (_onChanged = new EventListener(this, "onChanged"));

	public int selectedIndex
	{
		get
		{
			return _selectedIndex;
		}
		set
		{
			if (_selectedIndex != value)
			{
				if (value > _pageIds.Count - 1)
				{
					throw new IndexOutOfRangeException(value.ToString() ?? "");
				}
				changing = true;
				_previousIndex = _selectedIndex;
				_selectedIndex = value;
				parent.ApplyController(this);
				DispatchEvent("onChanged", null);
				changing = false;
			}
		}
	}

	public string selectedPage
	{
		get
		{
			if (_selectedIndex == -1)
			{
				return null;
			}
			return _pageNames[_selectedIndex];
		}
		set
		{
			int num = _pageNames.IndexOf(value);
			if (num == -1)
			{
				num = 0;
			}
			selectedIndex = num;
		}
	}

	public int previsousIndex => _previousIndex;

	public string previousPage
	{
		get
		{
			if (_previousIndex == -1)
			{
				return null;
			}
			return _pageNames[_previousIndex];
		}
	}

	public int pageCount => _pageIds.Count;

	internal string selectedPageId
	{
		get
		{
			if (_selectedIndex == -1)
			{
				return string.Empty;
			}
			return _pageIds[_selectedIndex];
		}
		set
		{
			int num = _pageIds.IndexOf(value);
			if (num != -1)
			{
				selectedIndex = num;
			}
		}
	}

	internal string oppositePageId
	{
		set
		{
			if (_pageIds.IndexOf(value) > 0)
			{
				selectedIndex = 0;
			}
			else if (_pageIds.Count > 1)
			{
				selectedIndex = 1;
			}
		}
	}

	internal string previousPageId
	{
		get
		{
			if (_previousIndex == -1)
			{
				return null;
			}
			return _pageIds[_previousIndex];
		}
	}

	public Controller()
	{
		_pageIds = new List<string>();
		_pageNames = new List<string>();
		_selectedIndex = -1;
		_previousIndex = -1;
	}

	public void Dispose()
	{
		RemoveEventListeners();
	}

	public void SetSelectedIndex(int value)
	{
		if (_selectedIndex != value)
		{
			if (value > _pageIds.Count - 1)
			{
				throw new IndexOutOfRangeException(value.ToString() ?? "");
			}
			changing = true;
			_previousIndex = _selectedIndex;
			_selectedIndex = value;
			parent.ApplyController(this);
			changing = false;
		}
	}

	public void SetSelectedPage(string value)
	{
		int num = _pageNames.IndexOf(value);
		if (num == -1)
		{
			num = 0;
		}
		SetSelectedIndex(num);
	}

	public string GetPageName(int index)
	{
		return _pageNames[index];
	}

	public string GetPageId(int index)
	{
		return _pageIds[index];
	}

	public string GetPageIdByName(string aName)
	{
		int num = _pageNames.IndexOf(aName);
		if (num != -1)
		{
			return _pageIds[num];
		}
		return null;
	}

	public void AddPage(string name)
	{
		if (name == null)
		{
			name = string.Empty;
		}
		AddPageAt(name, _pageIds.Count);
	}

	public void AddPageAt(string name, int index)
	{
		string item = "_" + _nextPageId++;
		if (index == _pageIds.Count)
		{
			_pageIds.Add(item);
			_pageNames.Add(name);
		}
		else
		{
			_pageIds.Insert(index, item);
			_pageNames.Insert(index, name);
		}
	}

	public void RemovePage(string name)
	{
		int num = _pageNames.IndexOf(name);
		if (num != -1)
		{
			_pageIds.RemoveAt(num);
			_pageNames.RemoveAt(num);
			if (_selectedIndex >= _pageIds.Count)
			{
				selectedIndex = _selectedIndex - 1;
			}
			else
			{
				parent.ApplyController(this);
			}
		}
	}

	public void RemovePageAt(int index)
	{
		_pageIds.RemoveAt(index);
		_pageNames.RemoveAt(index);
		if (_selectedIndex >= _pageIds.Count)
		{
			selectedIndex = _selectedIndex - 1;
		}
		else
		{
			parent.ApplyController(this);
		}
	}

	public void ClearPages()
	{
		_pageIds.Clear();
		_pageNames.Clear();
		if (_selectedIndex != -1)
		{
			selectedIndex = -1;
		}
		else
		{
			parent.ApplyController(this);
		}
	}

	public bool HasPage(string aName)
	{
		return _pageNames.IndexOf(aName) != -1;
	}

	internal int GetPageIndexById(string aId)
	{
		return _pageIds.IndexOf(aId);
	}

	internal string GetPageNameById(string aId)
	{
		int num = _pageIds.IndexOf(aId);
		if (num != -1)
		{
			return _pageNames[num];
		}
		return null;
	}

	public void RunActions()
	{
		if (_actions != null)
		{
			int count = _actions.Count;
			for (int i = 0; i < count; i++)
			{
				_actions[i].Run(this, previousPageId, selectedPageId);
			}
		}
	}

	public void Setup(ByteBuffer buffer)
	{
		int position = buffer.position;
		buffer.Seek(position, 0);
		name = buffer.ReadS();
		autoRadioGroupDepth = buffer.ReadBool();
		buffer.Seek(position, 1);
		int num = buffer.ReadShort();
		_pageIds.Capacity = num;
		_pageNames.Capacity = num;
		for (int i = 0; i < num; i++)
		{
			_pageIds.Add(buffer.ReadS());
			_pageNames.Add(buffer.ReadS());
		}
		int num2 = 0;
		if (buffer.version >= 2)
		{
			switch (buffer.ReadByte())
			{
			case 1:
				num2 = buffer.ReadShort();
				break;
			case 2:
				num2 = _pageNames.IndexOf(UIPackage.branch);
				if (num2 == -1)
				{
					num2 = 0;
				}
				break;
			case 3:
				num2 = _pageNames.IndexOf(UIPackage.GetVar(buffer.ReadS()));
				if (num2 == -1)
				{
					num2 = 0;
				}
				break;
			}
		}
		buffer.Seek(position, 2);
		num = buffer.ReadShort();
		if (num > 0)
		{
			if (_actions == null)
			{
				_actions = new List<ControllerAction>(num);
			}
			for (int j = 0; j < num; j++)
			{
				int num3 = buffer.ReadShort();
				num3 += buffer.position;
				ControllerAction controllerAction = ControllerAction.CreateAction((ControllerAction.ActionType)buffer.ReadByte());
				controllerAction.Setup(buffer);
				_actions.Add(controllerAction);
				buffer.position = num3;
			}
		}
		if (parent != null && _pageIds.Count > 0)
		{
			_selectedIndex = num2;
		}
		else
		{
			_selectedIndex = -1;
		}
	}
}
