using Core.Audio;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Tools;
using UnityEngine;

namespace UI;

public abstract class BasePanel<T> : IBasePanel where T : GComponent
{
	protected bool _isInlineForReturnButton;

	private UIPanelStatusType _status;

	protected bool _FocusStatus;

	public UIPanelConfigure config { get; }

	public Signal onShown { get; }

	public Signal onClose { get; }

	protected T ui { get; set; }

	public T UIObject => ui;

	protected BasePanel(UIPanelConfigure config)
	{
		this.config = config;
		_status = UIPanelStatusType.None;
		onShown = new Signal();
		onClose = new Signal();
	}

	protected virtual void Create()
	{
		_status = UIPanelStatusType.Create;
		InitComponents();
	}

	protected virtual void InitData(params object[] objs)
	{
	}

	public virtual void Show(params object[] objs)
	{
		if (ui == null)
		{
			Create();
		}
		FullScreen();
		if (_status != UIPanelStatusType.Show)
		{
			GRoot.inst.AddChild(ui);
		}
		if (ui != null)
		{
			ui.fairyBatching = true;
			ui.sortingOrder = config.Layer;
		}
		InitData(objs);
		if (_status == UIPanelStatusType.Show)
		{
			SetTouchable(touchable: true);
		}
		else
		{
			SetTouchable(touchable: false);
			AddEvent();
			AddListener();
			Refresh();
			SetTouchable(touchable: true);
		}
		_FocusStatus = true;
		InitTouchable();
		PlayBGM();
		_status = UIPanelStatusType.Show;
		onShown.Dispatch();
	}

	protected virtual void InitComponents()
	{
	}

	public virtual void Refresh()
	{
		foreach (GObject child in ui._children)
		{
			if (child is GButton gButton)
			{
				gButton.onClick.Release();
			}
		}
	}

	protected virtual void AddEvent()
	{
	}

	protected virtual void RemoveEvent()
	{
	}

	protected virtual void AddListener()
	{
		Stage.inst.onStageResized.Add(OnStageResized);
	}

	protected virtual void RemoveListener()
	{
		Stage.inst.onStageResized.Remove(OnStageResized);
	}

	public virtual void LoseFocus()
	{
		_FocusStatus = false;
	}

	public virtual void ResumeFocus()
	{
		if (ui != null)
		{
			_FocusStatus = true;
		}
	}

	public virtual void PlayBGM()
	{
		BGMHelper.TryPlayBGM(config.BGMConfigID);
	}

	public virtual void Close()
	{
		if (ui != null && _status != UIPanelStatusType.Close)
		{
			_status = UIPanelStatusType.Close;
			SetTouchable(touchable: false);
			RemoveEvent();
			RemoveListener();
			onClose.Dispatch();
			GRoot.inst.RemoveChild(ui);
			CommonUIManager.StopVideo(UIType.Panel, (int)config.PanelType);
		}
	}

	public virtual void Dispose()
	{
		if (_status != UIPanelStatusType.Dispose)
		{
			Close();
			_status = UIPanelStatusType.Dispose;
			UIPackage.RemovePackage(config.PackageName);
			ui?.Dispose();
			ui = null;
		}
	}

	public bool IsOpen()
	{
		return _status == UIPanelStatusType.Show;
	}

	public virtual void SetTouchable(bool touchable)
	{
		if (ui != null)
		{
			ui.touchableAll = touchable;
		}
	}

	public virtual void InitTouchable()
	{
	}

	public virtual void SetUIVisible(bool status)
	{
		if (ui != null)
		{
			ui.visible = status;
		}
	}

	public virtual void CoverMode(bool inCoverMode)
	{
	}

	public virtual void AdultMode(bool inAdultMode)
	{
	}

	protected virtual void FullScreen()
	{
		if (ui != null)
		{
			UIPanelType panelType = config.PanelType;
			if (panelType == UIPanelType.Login || panelType == UIPanelType.Background || panelType == UIPanelType.BattleSettlement)
			{
				ui.MakeFullScreen();
				return;
			}
			ui.AddRelation(GRoot.inst, RelationType.Height);
			ui.AddRelation(GRoot.inst, RelationType.Center_Center);
			int num = Mathf.CeilToInt(Screen.safeArea.width / GRoot.contentScaleFactor);
			ui.SetSize(num, GRoot.inst.height);
			ui.Center();
		}
	}

	private async void OnStageResized()
	{
		await UniTask.DelayFrame(2);
		FullScreen();
	}
}
