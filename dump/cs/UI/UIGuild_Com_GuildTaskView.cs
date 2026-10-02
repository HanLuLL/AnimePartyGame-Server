using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;

namespace UI;

public class UIGuild_Com_GuildTaskView : GComponent
{
	private bool _initialized;

	private bool _isShowing;

	public GList task_list;

	public const string URL = "ui://w5bj58pzhtd11l";

	public void Init()
	{
		if (!_initialized)
		{
			if (task_list != null)
			{
				task_list.itemRenderer = RenderTask;
			}
			_initialized = true;
		}
	}

	public void OnShow()
	{
		_isShowing = true;
		RefreshTaskList();
	}

	public void OnHide()
	{
		_isShowing = false;
	}

	public void AddEvent()
	{
	}

	public void RemoveEvent()
	{
	}

	public void AddListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.guild?.signal.guildTasksChanged.AddListener(OnGuildTasksChanged);
	}

	public void RemoveListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.guild?.signal.guildTasksChanged.RemoveListener(OnGuildTasksChanged);
	}

	public void ClearData()
	{
		_isShowing = false;
	}

	private void RefreshTaskList()
	{
		if (task_list != null)
		{
			IReadOnlyList<GuildTaskView> obj = SimpleSingletonProvider<GameLogicManager>.inst.guild?.GuildTaskViews;
			task_list.numItems = obj?.Count ?? 0;
		}
	}

	private void RenderTask(int index, GObject item)
	{
		IReadOnlyList<GuildTaskView> readOnlyList = SimpleSingletonProvider<GameLogicManager>.inst.guild?.GuildTaskViews;
		if (item is UIGuild_Com_TaskLabel uIGuild_Com_TaskLabel && readOnlyList != null && index >= 0 && index < readOnlyList.Count)
		{
			uIGuild_Com_TaskLabel.Bind(readOnlyList[index]);
		}
	}

	private void OnGuildTasksChanged()
	{
		if (_isShowing)
		{
			RefreshTaskList();
		}
	}

	public static UIGuild_Com_GuildTaskView CreateInstance()
	{
		return (UIGuild_Com_GuildTaskView)UIPackage.CreateObject("Guild", "Guild_Com_GuildTaskView");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		task_list = (GList)GetChildAt(4);
	}
}
