using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UIBattleInfo_Com_Asymmetrical : GComponent
{
	private const int TotalScore = 8;

	private GTweener progressTweener;

	public UIBattleInfo_Com_Asymmetrical_Progress com_Progress;

	public GTextField txt_Round;

	public const string URL = "ui://fxejlqlfcz3799";

	public void InitComponents()
	{
	}

	public void Refresh()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo != null)
		{
			UpdateScore(curRoomInfo.info.SpecialScore);
		}
	}

	private void UpdateScore(int score)
	{
		float endValue = 0f;
		StopProgressShow();
		List<GObject> children = com_Progress.list._children;
		for (int i = 0; i < children.Count; i++)
		{
			if (score > i)
			{
				Vector2 pt = children[i].LocalToGlobal(Vector2.zero);
				pt = com_Progress.GlobalToLocal(pt);
				endValue = ((i == children.Count - 1) ? com_Progress.width : (pt.x + children[i].width));
			}
		}
		progressTweener = com_Progress.bar.TweenMoveX(endValue, 0.2f).OnComplete((GTweenCallback)delegate
		{
			com_Progress.list.itemRenderer = delegate(int index, GObject item)
			{
				if (item is UIBattleInfo_Com_AsymmetricalItem uIBattleInfo_Com_AsymmetricalItem)
				{
					uIBattleInfo_Com_AsymmetricalItem.status.selectedIndex = ((score > index) ? 1 : 0);
				}
			};
			com_Progress.list.numItems = com_Progress.list.numItems;
		}).SetEase(EaseType.Linear);
	}

	public void AddEvent()
	{
	}

	public void RemoveEvent()
	{
	}

	public void AddListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.gameModePlay.signal.score.AddListener(UpdateScore);
	}

	public void RemoveListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.gameModePlay.signal.score.RemoveListener(UpdateScore);
	}

	public void Close()
	{
		StopProgressShow();
	}

	public void DisposeProgress()
	{
	}

	private void StopProgressShow()
	{
		progressTweener?.Kill();
		progressTweener = null;
	}

	public void RefreshRound(int round)
	{
		txt_Round.text = (BattleConfig.AsymmetricalDefenderWinRound - round).ToString();
	}

	public static UIBattleInfo_Com_Asymmetrical CreateInstance()
	{
		return (UIBattleInfo_Com_Asymmetrical)UIPackage.CreateObject("BattleInfo", "BattleInfo_Com_Asymmetrical");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Progress = (UIBattleInfo_Com_Asymmetrical_Progress)GetChildAt(1);
		txt_Round = (GTextField)GetChildAt(3);
	}
}
