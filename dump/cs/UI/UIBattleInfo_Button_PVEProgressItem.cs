using Core.Mark;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UIBattleInfo_Button_PVEProgressItem : GButton, IMarkTarget
{
	private PVEProgressData progressData;

	public Controller type;

	public Controller status;

	public GLoader loader_event;

	public GImage image_Tip;

	public GImage image_Hover;

	public const string URL = "ui://fxejlqlfr1pj8l";

	bool IMarkTarget.HoverWait => true;

	public void Refresh(PVEProgressData _progressData, int gameProgress, int gameMaxProgress)
	{
		progressData = _progressData;
		status.selectedIndex = ((gameProgress == progressData.progress) ? 1 : 0);
		base.grayed = progressData.progress < gameProgress;
		if (progressData.mapEventIds != null)
		{
			if (progressData.progress >= gameMaxProgress)
			{
				type.selectedIndex = 1;
			}
			else
			{
				type.selectedIndex = 0;
			}
		}
		else
		{
			type.selectedIndex = 0;
		}
		image_Hover.visible = false;
	}

	public void OnMarkTipShow()
	{
		if (progressData?.mapEventIds != null && progressData.mapEventIds.Count != 0 && type.selectedIndex == 1)
		{
			image_Tip.visible = true;
		}
	}

	public void OnMarkTipHide()
	{
		if (progressData?.mapEventIds != null && progressData.mapEventIds.Count != 0 && type.selectedIndex == 1)
		{
			image_Tip.visible = false;
		}
	}

	public void OnMarkHoverEnter()
	{
		if (type.selectedIndex == 1)
		{
			image_Hover.visible = true;
			if (progressData?.mapEventIds != null && progressData.mapEventIds.Count != 0)
			{
				string eventCardIds = GetEventCardIds();
				string pveEventMsg = BattlePveEventMessage.GetPveEventMsg(40016, progressData.progress, eventCardIds);
				SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.ShowPreview?.Dispatch(pveEventMsg);
			}
		}
	}

	public void OnMarkHoverExit()
	{
		if (type.selectedIndex == 1)
		{
			image_Hover.visible = false;
		}
	}

	public void OnMarkSelected()
	{
		OnMarkTipHide();
		if (progressData?.mapEventIds != null && progressData.mapEventIds.Count != 0 && type.selectedIndex == 1)
		{
			image_Hover.visible = false;
			string eventCardIds = GetEventCardIds();
			BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
			if (selfPlayerData != null)
			{
				BattlePveEventMessage msgData = new BattlePveEventMessage(selfPlayerData, 40016, progressData.progress, eventCardIds);
				SimpleSingletonProvider<GameLogicManager>.inst.communicate.RequestSendMessageC2S(msgData, 40016);
			}
		}
	}

	private string GetEventCardIds()
	{
		string text = "";
		for (int i = 0; i < progressData.mapEventIds.Count; i++)
		{
			int key = progressData.mapEventIds[i];
			if (StaticConfigure.MapEvent.InfoDict.TryGetValue(key, out var value))
			{
				text += $"{value.CardID}|";
			}
		}
		return text;
	}

	public void TriggerHoverConfirmed()
	{
		OnMarkTipHide();
		image_Hover.visible = false;
		base.onClick.Call();
	}

	public Vector2 GetPosition()
	{
		return LocalToGlobal(Vector2.zero);
	}

	public static UIBattleInfo_Button_PVEProgressItem CreateInstance()
	{
		return (UIBattleInfo_Button_PVEProgressItem)UIPackage.CreateObject("BattleInfo", "BattleInfo_Button_PVEProgressItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(1);
		status = GetControllerAt(2);
		loader_event = (GLoader)GetChildAt(3);
		image_Tip = (GImage)GetChildAt(10);
		image_Hover = (GImage)GetChildAt(11);
	}
}
