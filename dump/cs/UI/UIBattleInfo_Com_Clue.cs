using System;
using System.Linq;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;
using UnityTimer;

namespace UI;

public class UIBattleInfo_Com_Clue : GComponent
{
	private Timer _timer;

	public Controller showTaskInfo;

	public GList list_Task;

	public GTextField txt_tip;

	public GGroup taskGroup;

	public UIBattleInfo_Button_Clue btn_Task;

	public Transition Cut_in;

	public Transition Cut_out;

	public const string URL = "ui://fxejlqlfmgj001";

	private RoomInfo roomInfo => SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;

	public void InitComponents()
	{
		list_Task.itemRenderer = RendererClueItem;
	}

	public void Refresh()
	{
		int count = roomInfo.ClueMissions.Count;
		btn_Task.txt_Count.text = $"{roomInfo.ClueNum}/{count}";
		list_Task.numItems = count;
		base.visible = count > 0;
		txt_tip.text = 10099.GetLocal(UIStringType.PVEMission);
	}

	public void AddEvent()
	{
		btn_Task.onClick.Add(SwitchTaskShow);
	}

	public void RemoveEvent()
	{
		btn_Task.onClick.Remove(SwitchTaskShow);
	}

	public void AddListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.clueChange.AddListener(Refresh);
	}

	public void RemoveListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.clueChange.RemoveListener(Refresh);
	}

	public void Close()
	{
		showTaskInfo.selectedIndex = 0;
		btn_Task.selected = false;
	}

	private void SwitchTaskShow()
	{
		btn_Task.onClick.Retain();
		SwitchTaskInfo(showTaskInfo.selectedIndex == 0);
		btn_Task.onClick.Release();
	}

	private void RendererClueItem(int index, GObject item)
	{
		if (item is UIBattleInfo_Com_ClueItem uIBattleInfo_Com_ClueItem && index >= 0 && index < roomInfo.ClueMissions.Count)
		{
			ClueMissionData value = roomInfo.ClueMissions.ElementAt(index).Value;
			ClueMissionTargetData primaryTarget = value.PrimaryTarget;
			uIBattleInfo_Com_ClueItem.completed.selectedIndex = (value.IsCompleted ? 1 : 0);
			uIBattleInfo_Com_ClueItem.txt_Title.text = primaryTarget?.GetTitle() ?? string.Empty;
			uIBattleInfo_Com_ClueItem.txt_Progress.text = primaryTarget?.GetProgressDesc() ?? string.Empty;
			uIBattleInfo_Com_ClueItem.showLine.selectedIndex = ((uIBattleInfo_Com_ClueItem.txt_Title.height > 40f) ? 1 : 0);
		}
	}

	private void SwitchTaskInfo(bool show)
	{
		if (show)
		{
			showTaskInfo.selectedIndex = 1;
			Cut_out.Stop();
			Cut_in.Play();
			return;
		}
		Cut_in.Stop();
		Cut_out.Play();
		_timer = Timer.Register(0f, 0.5f, (Action)delegate
		{
			showTaskInfo.selectedIndex = 0;
		}, (Action)null, (Action)null, (Action)null, (Action)null, (Action<float>)null, (Action)null, false, -1f, false, (GameObject)null);
	}

	public static UIBattleInfo_Com_Clue CreateInstance()
	{
		return (UIBattleInfo_Com_Clue)UIPackage.CreateObject("BattleInfo", "BattleInfo_Com_Clue");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showTaskInfo = GetControllerAt(0);
		list_Task = (GList)GetChildAt(1);
		txt_tip = (GTextField)GetChildAt(3);
		taskGroup = (GGroup)GetChildAt(5);
		btn_Task = (UIBattleInfo_Button_Clue)GetChildAt(6);
		Cut_in = GetTransitionAt(0);
		Cut_out = GetTransitionAt(1);
	}
}
