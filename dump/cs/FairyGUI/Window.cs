using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

public class Window : GComponent
{
	public bool bringToFontOnClick;

	private GComponent _frame;

	private GComponent _contentPane;

	private GObject _modalWaitPane;

	private GObject _closeButton;

	private GObject _dragArea;

	private GObject _contentArea;

	private bool _modal;

	private List<IUISource> _uiSources;

	private bool _inited;

	private bool _loading;

	protected int _requestingCmd;

	public GComponent contentPane
	{
		get
		{
			return _contentPane;
		}
		set
		{
			if (_contentPane == value)
			{
				return;
			}
			if (_contentPane != null)
			{
				RemoveChild(_contentPane);
			}
			_contentPane = value;
			if (_contentPane != null)
			{
				base.gameObjectName = "Window - " + _contentPane.gameObjectName;
				_contentPane.gameObjectName = "ContentPane";
				AddChild(_contentPane);
				SetSize(_contentPane.width, _contentPane.height);
				_contentPane.AddRelation(this, RelationType.Size);
				_contentPane.fairyBatching = true;
				_frame = _contentPane.GetChild("frame") as GComponent;
				if (_frame != null)
				{
					closeButton = _frame.GetChild("closeButton");
					dragArea = _frame.GetChild("dragArea");
					contentArea = _frame.GetChild("contentArea");
				}
			}
			else
			{
				_frame = null;
				base.gameObjectName = "Window";
			}
		}
	}

	public GComponent frame => _frame;

	public GObject closeButton
	{
		get
		{
			return _closeButton;
		}
		set
		{
			if (_closeButton != null)
			{
				_closeButton.onClick.Remove(closeEventHandler);
			}
			_closeButton = value;
			if (_closeButton != null)
			{
				_closeButton.onClick.Add(closeEventHandler);
			}
		}
	}

	public GObject dragArea
	{
		get
		{
			return _dragArea;
		}
		set
		{
			if (_dragArea == value)
			{
				return;
			}
			if (_dragArea != null)
			{
				_dragArea.draggable = false;
				_dragArea.onDragStart.Remove(__dragStart);
			}
			_dragArea = value;
			if (_dragArea != null)
			{
				if (_dragArea is GGraph gGraph && gGraph.shape.isEmpty)
				{
					gGraph.DrawRect(_dragArea.width, _dragArea.height, 0, Color.clear, Color.clear);
				}
				_dragArea.draggable = true;
				_dragArea.onDragStart.Add(__dragStart);
			}
		}
	}

	public GObject contentArea
	{
		get
		{
			return _contentArea;
		}
		set
		{
			_contentArea = value;
		}
	}

	public GObject modalWaitingPane => _modalWaitPane;

	public bool isShowing => base.parent != null;

	public bool isTop
	{
		get
		{
			if (base.parent != null)
			{
				return base.parent.GetChildIndex(this) == base.parent.numChildren - 1;
			}
			return false;
		}
	}

	public bool modal
	{
		get
		{
			return _modal;
		}
		set
		{
			_modal = value;
		}
	}

	public bool modalWaiting
	{
		get
		{
			if (_modalWaitPane != null)
			{
				return _modalWaitPane.inContainer;
			}
			return false;
		}
	}

	public Window()
	{
		_uiSources = new List<IUISource>();
		base.tabStopChildren = true;
		bringToFontOnClick = UIConfig.bringWindowToFrontOnClick;
		base.displayObject.onAddedToStage.Add(__addedToStage);
		base.displayObject.onRemovedFromStage.Add(__removeFromStage);
		base.displayObject.onTouchBegin.AddCapture(__touchBegin);
		base.gameObjectName = "Window";
		SetHome(GRoot.inst);
	}

	public void AddUISource(IUISource source)
	{
		_uiSources.Add(source);
	}

	public void Show()
	{
		GRoot.inst.ShowWindow(this);
	}

	public void ShowPopup()
	{
		GRoot.inst.ShowPopup(this);
	}

	public void ShowOn(GRoot r)
	{
		r.ShowWindow(this);
	}

	public void Hide()
	{
		if (isShowing)
		{
			DoHideAnimation();
		}
	}

	public void HideImmediately()
	{
		base.root.HideWindowImmediately(this);
	}

	public void HidePopup()
	{
		base.root.HidePopup(this);
	}

	public void HideAllPopup()
	{
		base.root.HidePopup();
	}

	public void CenterOn(GRoot r, bool restraint)
	{
		SetXY((int)((r.width - base.width) / 2f), (int)((r.height - base.height) / 2f));
		if (restraint)
		{
			AddRelation(r, RelationType.Center_Center);
			AddRelation(r, RelationType.Middle_Middle);
		}
	}

	public void ToggleStatus()
	{
		if (isTop)
		{
			Hide();
		}
		else
		{
			Show();
		}
	}

	public void BringToFront()
	{
		base.root.BringToFront(this);
	}

	public void ShowModalWait()
	{
		ShowModalWait(0);
	}

	public void ShowModalWait(int requestingCmd)
	{
		if (requestingCmd != 0)
		{
			_requestingCmd = requestingCmd;
		}
		if (UIConfig.windowModalWaiting != null)
		{
			if (_modalWaitPane == null)
			{
				_modalWaitPane = UIPackage.CreateObjectFromURL(UIConfig.windowModalWaiting);
				_modalWaitPane.SetHome(this);
			}
			LayoutModalWaitPane();
			AddChild(_modalWaitPane);
		}
	}

	protected virtual void LayoutModalWaitPane()
	{
		if (_contentArea != null)
		{
			Vector2 pt = _frame.LocalToGlobal(Vector2.zero);
			pt = GlobalToLocal(pt);
			_modalWaitPane.SetXY((float)(int)pt.x + _contentArea.x, (float)(int)pt.y + _contentArea.y);
			_modalWaitPane.SetSize(_contentArea.width, _contentArea.height);
		}
		else
		{
			_modalWaitPane.SetSize(base.width, base.height);
		}
	}

	public bool CloseModalWait()
	{
		return CloseModalWait(0);
	}

	public bool CloseModalWait(int requestingCmd)
	{
		if (requestingCmd != 0 && _requestingCmd != requestingCmd)
		{
			return false;
		}
		_requestingCmd = 0;
		if (_modalWaitPane != null && _modalWaitPane.parent != null)
		{
			RemoveChild(_modalWaitPane);
		}
		return true;
	}

	public void Init()
	{
		if (_inited || _loading)
		{
			return;
		}
		if (_uiSources.Count > 0)
		{
			_loading = false;
			int count = _uiSources.Count;
			for (int i = 0; i < count; i++)
			{
				IUISource iUISource = _uiSources[i];
				if (!iUISource.loaded)
				{
					iUISource.Load(__uiLoadComplete);
					_loading = true;
				}
			}
			if (!_loading)
			{
				_init();
			}
		}
		else
		{
			_init();
		}
	}

	protected virtual void OnInit()
	{
	}

	protected virtual void OnShown()
	{
	}

	protected virtual void OnHide()
	{
	}

	protected virtual void DoShowAnimation()
	{
		OnShown();
	}

	protected virtual void DoHideAnimation()
	{
		HideImmediately();
	}

	private void __uiLoadComplete()
	{
		int count = _uiSources.Count;
		for (int i = 0; i < count; i++)
		{
			if (!_uiSources[i].loaded)
			{
				return;
			}
		}
		_loading = false;
		_init();
	}

	private void _init()
	{
		_inited = true;
		OnInit();
		if (isShowing)
		{
			DoShowAnimation();
		}
	}

	public override void Dispose()
	{
		if (_modalWaitPane != null && _modalWaitPane.parent == null)
		{
			_modalWaitPane.Dispose();
		}
		base.Dispose();
	}

	protected virtual void closeEventHandler(EventContext context)
	{
		Hide();
	}

	private void __addedToStage()
	{
		if (!_inited)
		{
			Init();
		}
		else
		{
			DoShowAnimation();
		}
	}

	private void __removeFromStage()
	{
		CloseModalWait();
		OnHide();
	}

	private void __touchBegin(EventContext context)
	{
		if (isShowing && bringToFontOnClick)
		{
			BringToFront();
		}
	}

	private void __dragStart(EventContext context)
	{
		context.PreventDefault();
		StartDrag((int)context.data);
	}
}
