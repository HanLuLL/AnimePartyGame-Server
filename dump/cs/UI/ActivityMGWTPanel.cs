using Core;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class ActivityMGWTPanel : BasePanel<UIActivityMGWTPanel>
{
	private const int ACTIVITY_ID = 609031;

	private int _activityId;

	public ActivityMGWTPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIActivityMGWTPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		_activityId = 0;
		if (objs != null && objs.Length != 0 && objs[0] is int activityId)
		{
			_activityId = activityId;
			return;
		}
		ActivityConfigure activity = StaticConfigure.Activity;
		if (activity?.InfoDict == null || !activity.InfoDict.TryGetValue(609031, out var value) || value == null)
		{
			Debug.LogError($"[ActivityMGWTPanel] 活动配置未加载或活动Id:{609031}缺失，无法打开面板");
		}
		else
		{
			_activityId = value.Id;
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		if (base.ui != null && _activityId != 0)
		{
			base.ui.loader_BG.MallScreen();
			base.ui.com_task.Init(_activityId);
			base.ui.com_store.Init(_activityId);
			base.ui.page.selectedIndex = 0;
			int dataForLanguage = GameSettings.GetDataForLanguage(1, 2, 0, 3);
			base.ui.tab_task.language.selectedIndex = dataForLanguage;
			base.ui.tab_store.language.selectedIndex = dataForLanguage;
			OnTabChanged();
			RefreshTabRedPoint();
		}
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_close.onClick.Add(OnReturnPanel);
		base.ui.tab_task.onClick.Add(OnTabTask);
		base.ui.tab_store.onClick.Add(OnTabStore);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_close.onClick.Remove(OnReturnPanel);
		base.ui.tab_task.onClick.Remove(OnTabTask);
		base.ui.tab_store.onClick.Remove(OnTabStore);
	}

	protected override void AddListener()
	{
		base.AddListener();
		base.ui.com_task.AddListener();
		base.ui.com_store.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.activity?.signal?.activityStatus.AddListener(OnTaskStatusChanged);
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		base.ui.com_task.RemoveListener();
		base.ui.com_store.RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.activity?.signal?.activityStatus.RemoveListener(OnTaskStatusChanged);
	}

	public override void Close()
	{
		base.ui?.com_task?.ClearData();
		base.ui?.com_store?.ClearData();
		base.Close();
	}

	private async void OnReturnPanel()
	{
		if (base.ui?.btn_close != null)
		{
			base.ui.btn_close.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
			base.ui?.btn_close?.onClick.Release();
		}
	}

	private void OnTabTask()
	{
		if (base.ui?.page != null)
		{
			base.ui.page.selectedIndex = 0;
			OnTabChanged();
		}
	}

	private void OnTabStore()
	{
		if (base.ui?.page != null)
		{
			base.ui.page.selectedIndex = 1;
			OnTabChanged();
		}
	}

	private void OnTabChanged()
	{
		if (base.ui?.page != null)
		{
			int selectedIndex = base.ui.page.selectedIndex;
			if (base.ui.tab_task?.ischoose != null)
			{
				base.ui.tab_task.ischoose.selectedIndex = ((selectedIndex == 0) ? 1 : 0);
			}
			if (base.ui.tab_store?.ischoose != null)
			{
				base.ui.tab_store.ischoose.selectedIndex = ((selectedIndex == 1) ? 1 : 0);
			}
			switch (selectedIndex)
			{
			case 0:
				base.ui.com_task?.OnShow();
				break;
			case 1:
				base.ui.com_store?.OnShow();
				break;
			}
			if (SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel is BottomMenuPanel bottomMenuPanel)
			{
				bottomMenuPanel.ChangeShowStatus(status: true);
			}
		}
	}

	private void RefreshTabRedPoint()
	{
		if (base.ui?.tab_store?.redPoint != null)
		{
			base.ui.tab_store.redPoint.selectedIndex = 0;
		}
		if (base.ui?.tab_task?.redPoint != null)
		{
			TaskActivityData taskActivityData = SimpleSingletonProvider<GameLogicManager>.inst.activity?.GetTaskActivityData(_activityId);
			base.ui.tab_task.redPoint.selectedIndex = ((taskActivityData != null && taskActivityData.GetActivityStatus()) ? 1 : 0);
		}
	}

	private void OnTaskStatusChanged(int activityId)
	{
		if (activityId == _activityId)
		{
			RefreshTabRedPoint();
		}
	}
}
