using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class UIGuild_Com_TaskLabel : GComponent
{
	private int _taskId;

	private int _wayId;

	public Controller Status;

	public GButton btn_Reward;

	public GTextField txt_Title;

	public GProgressBar progress_task;

	public GTextField txt_RefreshType;

	public UIGuild_Common_Button btn_taskStatus;

	public const string URL = "ui://w5bj58pzhtd11p";

	public void Bind(GuildTaskView view)
	{
		GuildMissionConfigure guildMissionConfigure = view?.Configure;
		TaskDSO taskDSO = view?.Task;
		if (guildMissionConfigure == null || taskDSO == null)
		{
			ClearRenderedData();
			return;
		}
		_taskId = guildMissionConfigure.Id;
		int num = Mathf.Max(0, guildMissionConfigure.ParamProgress);
		_wayId = ((num > 0 && taskDSO.Progress < num) ? guildMissionConfigure.Way : 0);
		if (txt_Title != null)
		{
			txt_Title.text = guildMissionConfigure.NameID.GetLocal(UIStringType.Guild);
		}
		RenderReward(guildMissionConfigure);
		RenderProgress(guildMissionConfigure, taskDSO);
		RenderRefreshType(guildMissionConfigure.TaskRefreshType);
		RenderSafeStatus();
	}

	private void RenderReward(GuildMissionConfigure configure)
	{
		if (btn_Reward == null)
		{
			return;
		}
		int num = 0;
		int count = 0;
		foreach (KeyValuePair<int, int> item in configure.Reward)
		{
			if (item.Key > 0 && (num == 0 || item.Key < num))
			{
				num = item.Key;
				count = item.Value;
			}
		}
		UICom_LitItem uICom_LitItem = btn_Reward as UICom_LitItem;
		bool flag = num != 0 && uICom_LitItem != null;
		if (flag)
		{
			CommonUIManager.RendererLitItem(uICom_LitItem, num, count);
		}
		btn_Reward.visible = flag;
	}

	private void RenderProgress(GuildMissionConfigure configure, TaskDSO task)
	{
		if (progress_task != null)
		{
			int num = Mathf.Max(0, configure.ParamProgress);
			progress_task.min = 0.0;
			progress_task.max = num;
			progress_task.value = ((num > 0) ? Mathf.Clamp(task.Progress, 0, num) : 0);
		}
	}

	private void RenderRefreshType(TaskRefreshType refreshType)
	{
		if (txt_RefreshType != null)
		{
			int num = refreshType switch
			{
				TaskRefreshType.Daily => 101, 
				TaskRefreshType.Weekly => 102, 
				TaskRefreshType.Monthly => 103, 
				_ => 0, 
			};
			txt_RefreshType.visible = num != 0;
			txt_RefreshType.text = ((num == 0) ? string.Empty : num.GetLocal(UIStringType.Mission));
		}
	}

	private void RenderSafeStatus()
	{
		if (Status != null)
		{
			Status.selectedIndex = 0;
		}
		if (btn_taskStatus != null)
		{
			btn_taskStatus.visible = _wayId != 0;
			btn_taskStatus.touchable = _wayId != 0;
			btn_taskStatus.onClick.Set(OnClickTaskStatus);
		}
	}

	private async void OnClickTaskStatus()
	{
		if (_taskId != 0 && _wayId != 0)
		{
			await SimpleSingletonProvider<UIManager>.inst.GoWayPanel(_wayId);
		}
	}

	private void ClearRenderedData()
	{
		_taskId = 0;
		_wayId = 0;
		if (txt_Title != null)
		{
			txt_Title.text = string.Empty;
		}
		if (txt_RefreshType != null)
		{
			txt_RefreshType.text = string.Empty;
			txt_RefreshType.visible = false;
		}
		if (btn_Reward != null)
		{
			btn_Reward.visible = false;
		}
		if (btn_taskStatus != null)
		{
			btn_taskStatus.visible = false;
			btn_taskStatus.touchable = false;
		}
		if (progress_task != null)
		{
			progress_task.max = 0.0;
			progress_task.value = 0.0;
		}
		if (Status != null)
		{
			Status.selectedIndex = 0;
		}
	}

	public static UIGuild_Com_TaskLabel CreateInstance()
	{
		return (UIGuild_Com_TaskLabel)UIPackage.CreateObject("Guild", "Guild_Com_TaskLabel");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
		btn_Reward = (GButton)GetChildAt(1);
		txt_Title = (GTextField)GetChildAt(2);
		progress_task = (GProgressBar)GetChildAt(3);
		txt_RefreshType = (GTextField)GetChildAt(4);
		btn_taskStatus = (UIGuild_Common_Button)GetChildAt(6);
	}
}
