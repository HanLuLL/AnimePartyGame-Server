using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class ActivityHubPanel : BasePanel<UIActivityHubPanel>
{
	private int _curActivityId;

	private List<ActivityActivityEntrance2Configure> _entranceInfos;

	private UIActivityHub_Com_SwitchTab _selectTab;

	public ActivityHubPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIActivityHubPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		if (objs != null && objs.Length > 0)
		{
			RepeatedField<int> repeatedField = objs[0] as RepeatedField<int>;
			_curActivityId = repeatedField[0];
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.com_ActivityMenu.list.itemRenderer = RendererActivityButton;
	}

	public override void Refresh()
	{
		base.Refresh();
		_entranceInfos = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetActivityHubData();
		if (_entranceInfos.Count > 0)
		{
			base.ui.com_ActivityMenu.list.numItems = _entranceInfos.Count;
			int b = _entranceInfos.FindIndex((ActivityActivityEntrance2Configure x) => x.ActivityID == _curActivityId);
			int index = Mathf.Max(0, b);
			if (base.ui.com_ActivityMenu.list.GetChildAt(index) is UIActivityHub_Com_SwitchTab uIActivityHub_Com_SwitchTab)
			{
				uIActivityHub_Com_SwitchTab.selected = true;
				uIActivityHub_Com_SwitchTab.onClick.Call();
				uIActivityHub_Com_SwitchTab.Switch_in.Play();
			}
		}
		else
		{
			base.ui.com_ActivityMenu.list.numItems = 0;
		}
		UniTask.NextFrame().ContinueWith((Action)OnShelfTabScroll).Forget();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_Return.onClick.Add(ReturnHome);
		base.ui.com_ActivityMenu.list.scrollPane.onScroll.Add(OnShelfTabScroll);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Return.onClick.Remove(ReturnHome);
		base.ui.com_ActivityMenu.list.scrollPane.onScroll.Remove(OnShelfTabScroll);
	}

	protected override void AddListener()
	{
		base.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.activityStatus.AddListener(OnSignalActivityStatus);
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.activityStatus.RemoveListener(OnSignalActivityStatus);
	}

	public override void Close()
	{
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private void ReturnHome(EventContext context)
	{
		base.ui.btn_Return.onClick.Retain();
		SimpleSingletonProvider<UIManager>.inst.ReturnHomePanelDirectly().Forget();
		base.ui.btn_Return.onClick.Release();
	}

	private void OnShelfTabScroll()
	{
		ScrollPane scrollPane = base.ui.com_ActivityMenu.list.scrollPane;
		if (scrollPane != null)
		{
			if (!(scrollPane.contentHeight > scrollPane.viewHeight + 0.5f))
			{
				base.ui.com_ActivityMenu.up_btn.visible = false;
				base.ui.com_ActivityMenu.down_btn.visible = false;
			}
			else
			{
				base.ui.com_ActivityMenu.down_btn.visible = scrollPane.percY < 1f;
				base.ui.com_ActivityMenu.up_btn.visible = scrollPane.percY > 0f;
			}
		}
	}

	private void OnSignalActivityStatus(int activityId)
	{
		if (_curActivityId == activityId && _selectTab != null && StaticConfigure.Activity.InfoDict.TryGetValue(_curActivityId, out var _))
		{
			TaskActivityData taskActivityData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(_curActivityId);
			_selectTab.redPoint.selectedIndex = (taskActivityData.GetActivityStatus() ? 1 : 0);
		}
	}

	private void RendererActivityButton(int index, GObject item)
	{
		UIActivityHub_Com_SwitchTab btn_Tab = item as UIActivityHub_Com_SwitchTab;
		if (btn_Tab != null && StaticConfigure.Activity.InfoDict.TryGetValue(_entranceInfos[index].ActivityID, out var _))
		{
			btn_Tab.txt_title.text = _entranceInfos[index].Title.GetLocal(UIStringType.Activity);
			btn_Tab.txt_selectTitle.text = _entranceInfos[index].Title.GetLocal(UIStringType.Activity);
			TaskActivityData taskActivityData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(_entranceInfos[index].ActivityID);
			btn_Tab.redPoint.selectedIndex = (taskActivityData.GetActivityStatus() ? 1 : 0);
			btn_Tab.onClick.Set((EventCallback0)delegate
			{
				OpenActivityDetail(index, btn_Tab).Forget();
			});
		}
	}

	private async UniTask OpenActivityDetail(int index, UIActivityHub_Com_SwitchTab btn_Tab)
	{
		btn_Tab.onClick.Retain();
		_curActivityId = _entranceInfos[index].ActivityID;
		if (_selectTab != null)
		{
			_selectTab.selected = false;
		}
		_selectTab = btn_Tab;
		btn_Tab.selected = true;
		await OpenActivity(_entranceInfos[index]);
		btn_Tab.onClick.Release();
	}

	private async UniTask OpenActivity(ActivityActivityEntrance2Configure entranceInfo)
	{
		if (entranceInfo.InfoConfig.UiType == UIType.Panel)
		{
			if (entranceInfo.InfoConfig.PanelType != SimpleSingletonProvider<UIManager>.inst.currentPanel.config.PanelType || SimpleSingletonProvider<UIManager>.inst.currentPanel == null)
			{
				await SimpleSingletonProvider<UIManager>.inst.OpenPanel(entranceInfo.InfoConfig.PanelType, new RepeatedField<int> { entranceInfo.ActivityID });
			}
			else
			{
				SimpleSingletonProvider<UIManager>.inst.currentPanel.Show(new RepeatedField<int> { entranceInfo.ActivityID });
			}
		}
		else if (entranceInfo.InfoConfig.UiType == UIType.Window)
		{
			await SimpleSingletonProvider<UIManager>.inst.activityPopup.ShowActivity(entranceInfo.ActivityID);
		}
	}
}
