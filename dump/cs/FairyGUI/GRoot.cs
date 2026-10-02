using System;
using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

public class GRoot : GComponent
{
	private GGraph _modalLayer;

	private GObject _modalWaitPane;

	private List<GObject> _popupStack;

	private List<GObject> _justClosedPopups;

	private HashSet<GObject> _specialPopups;

	private GObject _tooltipWin;

	private GObject _defaultTooltipWin;

	internal static GRoot _inst;

	public List<GObject> children => _children;

	public static float contentScaleFactor => UIContentScaler.scaleFactor;

	public static int contentScaleLevel => UIContentScaler.scaleLevel;

	public static GRoot inst
	{
		get
		{
			if (_inst == null)
			{
				Stage.Instantiate();
			}
			return _inst;
		}
	}

	public GGraph modalLayer
	{
		get
		{
			if (_modalLayer == null || _modalLayer.isDisposed)
			{
				CreateModalLayer();
			}
			return _modalLayer;
		}
	}

	public bool hasModalWindow
	{
		get
		{
			if (_modalLayer != null)
			{
				return _modalLayer.parent != null;
			}
			return false;
		}
	}

	public bool modalWaiting
	{
		get
		{
			if (_modalWaitPane != null)
			{
				return _modalWaitPane.onStage;
			}
			return false;
		}
	}

	public GObject touchTarget => DisplayObjectToGObject(Stage.inst.touchTarget);

	public bool hasAnyPopup => _popupStack.Count > 0;

	public GObject focus
	{
		get
		{
			GObject gObject = DisplayObjectToGObject(Stage.inst.focus);
			if (gObject != null && !IsAncestorOf(gObject))
			{
				return null;
			}
			return gObject;
		}
		set
		{
			if (value == null)
			{
				Stage.inst.focus = null;
			}
			else
			{
				Stage.inst.focus = value.displayObject;
			}
		}
	}

	public GRoot()
	{
		name = (base.rootContainer.name = (base.rootContainer.gameObject.name = "GRoot"));
		base.opaque = false;
		_popupStack = new List<GObject>();
		_justClosedPopups = new List<GObject>();
		_specialPopups = new HashSet<GObject>();
		Stage.inst.onTouchBegin.AddCapture(__stageTouchBegin);
		Stage.inst.onTouchEnd.AddCapture(__stageTouchEnd);
	}

	public override void Dispose()
	{
		base.Dispose();
		Stage.inst.onTouchBegin.RemoveCapture(__stageTouchBegin);
		Stage.inst.onTouchEnd.RemoveCapture(__stageTouchEnd);
	}

	public void SetContentScaleFactor(int designResolutionX, int designResolutionY)
	{
		SetContentScaleFactor(designResolutionX, designResolutionY, UIContentScaler.ScreenMatchMode.MatchWidthOrHeight);
	}

	public void SetContentScaleFactor(int designResolutionX, int designResolutionY, UIContentScaler.ScreenMatchMode screenMatchMode)
	{
		UIContentScaler component = Stage.inst.gameObject.GetComponent<UIContentScaler>();
		component.designResolutionX = designResolutionX;
		component.designResolutionY = designResolutionY;
		component.scaleMode = UIContentScaler.ScaleMode.ScaleWithScreenSize;
		component.screenMatchMode = screenMatchMode;
		component.ApplyChange();
		ApplyContentScaleFactor();
	}

	public void SetContentScaleFactor(float constantScaleFactor)
	{
		UIContentScaler component = Stage.inst.gameObject.GetComponent<UIContentScaler>();
		component.scaleMode = UIContentScaler.ScaleMode.ConstantPixelSize;
		component.constantScaleFactor = constantScaleFactor;
		component.ApplyChange();
		ApplyContentScaleFactor();
	}

	public void ApplyContentScaleFactor()
	{
		SetSize(Mathf.CeilToInt(Stage.inst.width / UIContentScaler.scaleFactor), Mathf.CeilToInt(Stage.inst.height / UIContentScaler.scaleFactor));
		SetScale(UIContentScaler.scaleFactor, UIContentScaler.scaleFactor);
	}

	public void ShowWindow(Window win)
	{
		AddChild(win);
		AdjustModalLayer();
	}

	public void HideWindow(Window win)
	{
		win.Hide();
	}

	public void HideWindowImmediately(Window win)
	{
		HideWindowImmediately(win, dispose: false);
	}

	public void HideWindowImmediately(Window win, bool dispose)
	{
		if (win.parent == this)
		{
			RemoveChild(win, dispose);
		}
		else if (dispose)
		{
			win.Dispose();
		}
		AdjustModalLayer();
	}

	public void BringToFront(Window win)
	{
		int num = base.numChildren;
		int num2;
		for (num2 = ((_modalLayer == null || _modalLayer.parent == null || win.modal) ? (num - 1) : (GetChildIndex(_modalLayer) - 1)); num2 >= 0; num2--)
		{
			GObject childAt = GetChildAt(num2);
			if (childAt == win)
			{
				return;
			}
			if (childAt is Window)
			{
				break;
			}
		}
		if (num2 >= 0)
		{
			SetChildIndex(win, num2);
		}
	}

	public void ShowModalWait()
	{
		if (UIConfig.globalModalWaiting != null)
		{
			if (_modalWaitPane == null || _modalWaitPane.isDisposed)
			{
				_modalWaitPane = UIPackage.CreateObjectFromURL(UIConfig.globalModalWaiting);
				_modalWaitPane.SetHome(this);
			}
			_modalWaitPane.SetSize(base.width, base.height);
			_modalWaitPane.AddRelation(this, RelationType.Size);
			AddChild(_modalWaitPane);
		}
	}

	public void CloseModalWait()
	{
		if (_modalWaitPane != null && _modalWaitPane.parent != null)
		{
			RemoveChild(_modalWaitPane);
		}
	}

	public void CloseAllExceptModals()
	{
		GObject[] array = _children.ToArray();
		foreach (GObject gObject in array)
		{
			if (gObject is Window && !(gObject as Window).modal)
			{
				HideWindowImmediately(gObject as Window);
			}
		}
	}

	public void CloseAllWindows()
	{
		GObject[] array = _children.ToArray();
		foreach (GObject gObject in array)
		{
			if (gObject is Window)
			{
				HideWindowImmediately(gObject as Window);
			}
		}
	}

	public Window GetTopWindow()
	{
		for (int num = base.numChildren - 1; num >= 0; num--)
		{
			GObject childAt = GetChildAt(num);
			if (childAt is Window)
			{
				return (Window)childAt;
			}
		}
		return null;
	}

	private void CreateModalLayer()
	{
		_modalLayer = new GGraph();
		_modalLayer.DrawRect(base.width, base.height, 0, Color.white, UIConfig.modalLayerColor);
		_modalLayer.AddRelation(this, RelationType.Size);
		GGraph gGraph = _modalLayer;
		string text = (_modalLayer.gameObjectName = "ModalLayer");
		gGraph.name = text;
		_modalLayer.SetHome(this);
	}

	public GObject DisplayObjectToGObject(DisplayObject obj)
	{
		while (obj != null)
		{
			if (obj.gOwner != null)
			{
				return obj.gOwner;
			}
			obj = obj.parent;
		}
		return null;
	}

	private void AdjustModalLayer()
	{
		if (_modalLayer == null || _modalLayer.isDisposed)
		{
			CreateModalLayer();
		}
		int num = base.numChildren;
		if (_modalWaitPane != null && _modalWaitPane.parent != null)
		{
			SetChildIndex(_modalWaitPane, num - 1);
		}
		for (int num2 = num - 1; num2 >= 0; num2--)
		{
			GObject childAt = GetChildAt(num2);
			if (childAt is Window && (childAt as Window).modal)
			{
				if (_modalLayer.parent == null)
				{
					AddChildAt(_modalLayer, num2);
				}
				else
				{
					SetChildIndexBefore(_modalLayer, num2);
				}
				return;
			}
		}
		if (_modalLayer.parent != null)
		{
			RemoveChild(_modalLayer);
		}
	}

	public void ShowPopup(GObject popup)
	{
		ShowPopup(popup, null, PopupDirection.Auto, closeUntilUpEvent: false);
	}

	public void ShowPopup(GObject popup, GObject target)
	{
		ShowPopup(popup, target, PopupDirection.Auto, closeUntilUpEvent: false);
	}

	[Obsolete]
	public void ShowPopup(GObject popup, GObject target, object downward)
	{
		ShowPopup(popup, target, (downward != null) ? ((!(bool)downward) ? PopupDirection.Up : PopupDirection.Down) : PopupDirection.Auto, closeUntilUpEvent: false);
	}

	public void ShowPopup(GObject popup, GObject target, PopupDirection dir)
	{
		ShowPopup(popup, target, dir, closeUntilUpEvent: false);
	}

	public void ShowPopup(GObject popup, GObject target, PopupDirection dir, bool closeUntilUpEvent)
	{
		if (_popupStack.Count > 0)
		{
			int num = _popupStack.IndexOf(popup);
			if (num != -1)
			{
				for (int num2 = _popupStack.Count - 1; num2 >= num; num2--)
				{
					int index = _popupStack.Count - 1;
					GObject gObject = _popupStack[index];
					ClosePopup(gObject);
					_popupStack.RemoveAt(index);
					_specialPopups.Remove(gObject);
				}
			}
		}
		_popupStack.Add(popup);
		if (closeUntilUpEvent)
		{
			_specialPopups.Add(popup);
		}
		if (target != null)
		{
			for (GObject gObject2 = target; gObject2 != null; gObject2 = gObject2.parent)
			{
				if (gObject2.parent == this)
				{
					if (popup.sortingOrder < gObject2.sortingOrder)
					{
						popup.sortingOrder = gObject2.sortingOrder;
					}
					break;
				}
			}
		}
		AddChild(popup);
		AdjustModalLayer();
		if (!(popup is Window) || target != null || dir != PopupDirection.Auto)
		{
			Vector2 poupPosition = GetPoupPosition(popup, target, dir);
			popup.xy = poupPosition;
		}
	}

	[Obsolete]
	public Vector2 GetPoupPosition(GObject popup, GObject target, object downward)
	{
		return GetPoupPosition(popup, target, (downward != null) ? ((!(bool)downward) ? PopupDirection.Up : PopupDirection.Down) : PopupDirection.Auto);
	}

	public Vector2 GetPoupPosition(GObject popup, GObject target, PopupDirection dir)
	{
		Vector2 vector = Vector2.zero;
		Vector2 vector2;
		if (target != null)
		{
			vector2 = target.LocalToRoot(Vector2.zero, this);
			vector = target.LocalToRoot(target.size, this) - vector2;
		}
		else
		{
			vector2 = GlobalToLocal(Stage.inst.touchPosition);
		}
		float num = vector2.x;
		if (num + popup.width > base.width)
		{
			num = num + vector.x - popup.width;
		}
		float num2 = vector2.y + vector.y;
		if ((dir == PopupDirection.Auto && num2 + popup.height > base.height) || dir == PopupDirection.Up)
		{
			num2 = vector2.y - popup.height - 1f;
			if (num2 < 0f)
			{
				num2 = 0f;
				num += vector.x / 2f;
			}
		}
		return new Vector2(Mathf.RoundToInt(num), Mathf.RoundToInt(num2));
	}

	public void TogglePopup(GObject popup)
	{
		TogglePopup(popup, null, PopupDirection.Auto, closeUntilUpEvent: false);
	}

	public void TogglePopup(GObject popup, GObject target)
	{
		TogglePopup(popup, target, PopupDirection.Auto, closeUntilUpEvent: false);
	}

	[Obsolete]
	public void TogglePopup(GObject popup, GObject target, object downward)
	{
		TogglePopup(popup, target, (downward != null) ? ((!(bool)downward) ? PopupDirection.Up : PopupDirection.Down) : PopupDirection.Auto, closeUntilUpEvent: false);
	}

	public void TogglePopup(GObject popup, GObject target, PopupDirection dir)
	{
		TogglePopup(popup, target, dir, closeUntilUpEvent: false);
	}

	public void TogglePopup(GObject popup, GObject target, PopupDirection dir, bool closeUntilUpEvent)
	{
		if (_justClosedPopups.IndexOf(popup) == -1)
		{
			ShowPopup(popup, target, dir, closeUntilUpEvent);
		}
	}

	public void HidePopup()
	{
		HidePopup(null);
	}

	public void HidePopup(GObject popup)
	{
		if (popup != null)
		{
			int num = _popupStack.IndexOf(popup);
			if (num != -1)
			{
				for (int num2 = _popupStack.Count - 1; num2 >= num; num2--)
				{
					int index = _popupStack.Count - 1;
					GObject gObject = _popupStack[index];
					ClosePopup(gObject);
					_popupStack.RemoveAt(index);
					_specialPopups.Remove(gObject);
				}
			}
			return;
		}
		foreach (GObject item in _popupStack)
		{
			ClosePopup(item);
		}
		_popupStack.Clear();
		_specialPopups.Clear();
	}

	private void ClosePopup(GObject target)
	{
		if (target.parent != null)
		{
			if (target is Window)
			{
				((Window)target).Hide();
			}
			else
			{
				RemoveChild(target);
			}
		}
	}

	public void ShowTooltips(string msg)
	{
		ShowTooltips(msg, 0.1f);
	}

	public void ShowTooltips(string msg, float delay)
	{
		if (_defaultTooltipWin == null || _defaultTooltipWin.isDisposed)
		{
			string tooltipsWin = UIConfig.tooltipsWin;
			if (string.IsNullOrEmpty(tooltipsWin))
			{
				Debug.LogWarning("FairyGUI: UIConfig.tooltipsWin not defined");
				return;
			}
			_defaultTooltipWin = UIPackage.CreateObjectFromURL(tooltipsWin);
			_defaultTooltipWin.SetHome(this);
			_defaultTooltipWin.touchable = false;
		}
		_defaultTooltipWin.text = msg;
		ShowTooltipsWin(_defaultTooltipWin, delay);
	}

	public void ShowTooltipsWin(GObject tooltipWin)
	{
		ShowTooltipsWin(tooltipWin, 0.1f);
	}

	public void ShowTooltipsWin(GObject tooltipWin, float delay)
	{
		HideTooltips();
		_tooltipWin = tooltipWin;
		Timers.inst.Add(delay, 1, __showTooltipsWin);
	}

	private void __showTooltipsWin(object param)
	{
		if (_tooltipWin == null)
		{
			return;
		}
		float num = Stage.inst.touchPosition.x + 10f;
		float num2 = Stage.inst.touchPosition.y + 20f;
		Vector2 vector = GlobalToLocal(new Vector2(num, num2));
		num = vector.x;
		num2 = vector.y;
		if (num + _tooltipWin.width > base.width)
		{
			num -= _tooltipWin.width;
		}
		if (num2 + _tooltipWin.height > base.height)
		{
			num2 = num2 - _tooltipWin.height - 1f;
			if (num2 < 0f)
			{
				num2 = 0f;
			}
		}
		_tooltipWin.x = Mathf.RoundToInt(num);
		_tooltipWin.y = Mathf.RoundToInt(num2);
		AddChild(_tooltipWin);
	}

	public void HideTooltips()
	{
		if (_tooltipWin != null)
		{
			if (_tooltipWin.parent != null)
			{
				RemoveChild(_tooltipWin);
			}
			_tooltipWin = null;
		}
	}

	private void __stageTouchBegin(EventContext context)
	{
		if (_tooltipWin != null)
		{
			HideTooltips();
		}
		CheckPopups(touchBegin: true);
	}

	private void __stageTouchEnd(EventContext context)
	{
		CheckPopups(touchBegin: false);
	}

	private void CheckPopups(bool touchBegin)
	{
		if (touchBegin)
		{
			_justClosedPopups.Clear();
		}
		if (_popupStack.Count <= 0)
		{
			return;
		}
		DisplayObject displayObject = Stage.inst.touchTarget;
		bool flag = false;
		while (displayObject != Stage.inst && displayObject != null)
		{
			if (displayObject.gOwner != null)
			{
				int num = _popupStack.IndexOf(displayObject.gOwner);
				if (num != -1)
				{
					for (int num2 = _popupStack.Count - 1; num2 > num; num2--)
					{
						int index = _popupStack.Count - 1;
						GObject gObject = _popupStack[index];
						if (touchBegin != _specialPopups.Contains(gObject))
						{
							ClosePopup(gObject);
							_justClosedPopups.Add(gObject);
							_popupStack.RemoveAt(index);
							_specialPopups.Remove(gObject);
						}
					}
					flag = true;
					break;
				}
			}
			displayObject = displayObject.parent;
		}
		if (flag)
		{
			return;
		}
		for (int num3 = _popupStack.Count - 1; num3 >= 0; num3--)
		{
			GObject gObject2 = _popupStack[num3];
			if (touchBegin != _specialPopups.Contains(gObject2))
			{
				ClosePopup(gObject2);
				_justClosedPopups.Add(gObject2);
				_popupStack.RemoveAt(num3);
				_specialPopups.Remove(gObject2);
			}
		}
	}

	public void PlayOneShotSound(int audioEventID)
	{
		Stage.inst.PlayOneShotSound(audioEventID);
	}
}
