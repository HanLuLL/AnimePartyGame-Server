using GameLogic;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class ActivityComebackPanel : BasePanel<UIActivityComebackPanel>
{
	private int _activityId;

	public ActivityComebackPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIActivityComebackPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		if (objs != null && objs.Length != 0 && objs[0] is int activityId)
		{
			_activityId = activityId;
			return;
		}
		ComebackConfigure comeback = StaticConfigure.Comeback;
		if (comeback?.ParamsDict == null || !comeback.ParamsDict.TryGetValue(1, out var value) || value == null)
		{
			Debug.LogError("[ActivityComebackPanel] Comeback 配置未加载或主键 1 缺失，无法打开面板");
		}
		else
		{
			_activityId = value.ActivityId;
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		base.ui.loader_BG.MallScreen();
		base.ui.task_Com.Init(_activityId);
		base.ui.shop_Com.Init(_activityId);
		base.ui.gift_Com.Init(_activityId);
		base.ui.seven_Com.Init(_activityId);
		if (base.ui.tabType.selectedIndex == 0)
		{
			OnTabChanged();
		}
		else
		{
			base.ui.tabType.selectedIndex = 0;
		}
		RefreshAllRedPoints();
	}

	protected override void InitComponents()
	{
		base.InitComponents();
	}

	public override void Refresh()
	{
		base.Refresh();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_Back.onClick.Add(OnReturnPanel);
		base.ui.btn_Rule.onClick.Add(OnShowRule);
		base.ui.tabType.onChanged.Add(OnTabChanged);
		base.ui.gift_Com.AddEvent();
		base.ui.seven_Com.AddEvent();
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Back.onClick.Remove(OnReturnPanel);
		base.ui.btn_Rule.onClick.Remove(OnShowRule);
		base.ui.tabType.onChanged.Remove(OnTabChanged);
		base.ui.gift_Com.RemoveEvent();
		base.ui.seven_Com.RemoveEvent();
	}

	protected override void AddListener()
	{
		base.AddListener();
		base.ui.task_Com.AddListener();
		base.ui.shop_Com.AddListener();
		base.ui.gift_Com.AddListener();
		base.ui.seven_Com.AddListener();
		ComebackLogic comeback = SimpleSingletonProvider<GameLogicManager>.inst.comeback;
		if (comeback != null)
		{
			comeback.signal.infoUpdated.AddListener(OnRedPointInfoUpdated);
			comeback.signal.freeGiftClaimed.AddListener(OnRedPointFreeGiftClaimed);
			comeback.signal.surveyStateUpdated.AddListener(OnRedPointSurveyStateUpdated);
			comeback.signal.signInUpdated.AddListener(OnRedPointSignInUpdated);
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.activity?.signal != null)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.activityStatus.AddListener(OnRedPointTaskStatusChanged);
		}
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		base.ui.task_Com.RemoveListener();
		base.ui.shop_Com.RemoveListener();
		base.ui.gift_Com.RemoveListener();
		base.ui.seven_Com.RemoveListener();
		ComebackLogic comeback = SimpleSingletonProvider<GameLogicManager>.inst.comeback;
		if (comeback != null)
		{
			comeback.signal.infoUpdated.RemoveListener(OnRedPointInfoUpdated);
			comeback.signal.freeGiftClaimed.RemoveListener(OnRedPointFreeGiftClaimed);
			comeback.signal.surveyStateUpdated.RemoveListener(OnRedPointSurveyStateUpdated);
			comeback.signal.signInUpdated.RemoveListener(OnRedPointSignInUpdated);
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.activity?.signal != null)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.activityStatus.RemoveListener(OnRedPointTaskStatusChanged);
		}
	}

	public override void Close()
	{
		base.Close();
		base.ui?.task_Com?.ClearData();
		base.ui?.shop_Com?.ClearData();
		base.ui?.gift_Com?.ClearData();
		base.ui?.seven_Com?.ClearData();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private async void OnReturnPanel()
	{
		base.ui.btn_Back.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
		base.ui.btn_Back.onClick.Release();
	}

	private async void OnShowRule()
	{
		base.ui.btn_Rule.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.rule.TryShow(1129, 1130);
		base.ui.btn_Rule.onClick.Release();
	}

	private void OnTabChanged()
	{
		switch (base.ui.tabType.selectedIndex)
		{
		case 0:
			base.ui.gift_Com.OnShow();
			if (SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel is BottomMenuPanel bottomMenuPanel4)
			{
				bottomMenuPanel4.ChangeShowStatus(status: false);
			}
			break;
		case 1:
			base.ui.seven_Com.OnShow();
			if (SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel is BottomMenuPanel bottomMenuPanel2)
			{
				bottomMenuPanel2.ChangeShowStatus(status: false);
			}
			break;
		case 2:
			base.ui.task_Com.OnShow();
			if (SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel is BottomMenuPanel bottomMenuPanel3)
			{
				bottomMenuPanel3.ChangeShowStatus(status: true);
			}
			break;
		case 3:
			base.ui.shop_Com.OnShow();
			if (SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel is BottomMenuPanel bottomMenuPanel)
			{
				bottomMenuPanel.ChangeShowStatus(status: true);
			}
			break;
		}
	}

	public void RefreshAllRedPoints()
	{
		if (base.ui != null)
		{
			base.ui.gift_Com?.RefreshRedPoints();
			base.ui.task_Com?.RefreshRedPoints();
			base.ui.seven_Com?.RefreshRedPoints();
			RefreshTabRedPoints();
		}
	}

	private void RefreshTabRedPoints()
	{
		if (base.ui.tabList == null)
		{
			return;
		}
		for (int i = 0; i < base.ui.tabList.numItems; i++)
		{
			if (base.ui.tabList.GetChildAt(i) is UIActivityComeback_Tab_Button { redPoint: not null } uIActivityComeback_Tab_Button)
			{
				uIActivityComeback_Tab_Button.redPoint.selectedIndex = (ComputeTabRedPoint(i) ? 1 : 0);
			}
		}
	}

	private bool ComputeTabRedPoint(int tabIndex)
	{
		return tabIndex switch
		{
			0 => base.ui.gift_Com?.HasRedPoint ?? false, 
			1 => base.ui.seven_Com?.HasRedPoint ?? false, 
			2 => base.ui.task_Com?.HasRedPoint ?? false, 
			3 => false, 
			_ => false, 
		};
	}

	private void OnRedPointInfoUpdated(ReturnInfo _)
	{
		RefreshAllRedPoints();
	}

	private void OnRedPointFreeGiftClaimed(bool _)
	{
		RefreshAllRedPoints();
	}

	private void OnRedPointSurveyStateUpdated(int _)
	{
		RefreshAllRedPoints();
	}

	private void OnRedPointSignInUpdated(ReturnSignIn _)
	{
		RefreshAllRedPoints();
	}

	private void OnRedPointTaskStatusChanged(int activityId)
	{
		RefreshAllRedPoints();
	}
}
