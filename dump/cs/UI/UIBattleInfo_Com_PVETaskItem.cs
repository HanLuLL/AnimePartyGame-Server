using Core.Mark;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UIBattleInfo_Com_PVETaskItem : GComponent, IMarkTarget
{
	public int MapMissionId;

	public GTextField txt_Title;

	public GTextField txt_Progress;

	public GTextField txt_Desc;

	public GTextField txt_Desc_Failed;

	public GImage image_Hover;

	public const string URL = "ui://fxejlqlfg1i48s";

	bool IMarkTarget.HoverWait => false;

	public void OnMarkHoverEnter()
	{
		image_Hover.visible = true;
		string pveTaskMsg = BattlePveTaskMessage.GetPveTaskMsg(MapMissionId);
		SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.ShowPreview?.Dispatch(pveTaskMsg);
	}

	public void OnMarkHoverExit()
	{
		image_Hover.visible = false;
	}

	public void OnMarkSelected()
	{
		image_Hover.visible = false;
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (selfPlayerData != null)
		{
			BattlePveTaskMessage msgData = new BattlePveTaskMessage(selfPlayerData, MapMissionId);
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.RequestSendMessageC2S(msgData, BattlePveTaskMessage.MarkId);
		}
	}

	public void TriggerHoverConfirmed()
	{
	}

	public Vector2 GetPosition()
	{
		return LocalToGlobal(Vector2.zero);
	}

	public static UIBattleInfo_Com_PVETaskItem CreateInstance()
	{
		return (UIBattleInfo_Com_PVETaskItem)UIPackage.CreateObject("BattleInfo", "BattleInfo_Com_PVETaskItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Title = (GTextField)GetChildAt(1);
		txt_Progress = (GTextField)GetChildAt(2);
		txt_Desc = (GTextField)GetChildAt(3);
		txt_Desc_Failed = (GTextField)GetChildAt(4);
		image_Hover = (GImage)GetChildAt(5);
	}
}
