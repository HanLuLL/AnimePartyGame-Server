using Core.Mark;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UIHandCard_Com_Skill : GComponent, IMarkTarget
{
	public Controller showCD;

	public GImage image_Tip;

	public GImage image_Hover;

	public UIHandCard_Button_Skill btn_Skill;

	public GGraph di_zhezhao;

	public GTextField txt_CD;

	public const string URL = "ui://vflhnh8dqs184m";

	bool IMarkTarget.HoverWait => false;

	public void OnMarkTipShow()
	{
		image_Tip.visible = true;
	}

	public void OnMarkTipHide()
	{
		image_Tip.visible = false;
	}

	public void OnMarkHoverEnter()
	{
		image_Hover.visible = true;
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (selfPlayerData != null)
		{
			string item = BattleSkillMessage.GetSkillMsg(selfPlayerData.Property.activeSkillCD.Value).Item1;
			SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.ShowPreview?.Dispatch(item);
		}
	}

	public void OnMarkHoverExit()
	{
		image_Hover.visible = false;
	}

	public void OnMarkSelected()
	{
		OnMarkTipHide();
		image_Hover.visible = false;
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (selfPlayerData != null)
		{
			BattleSkillMessage battleSkillMessage = new BattleSkillMessage(selfPlayerData, selfPlayerData.Property.activeSkillCD.Value);
			SimpleSingletonProvider<GameLogicManager>.inst.communicate.RequestSendMessageC2S(battleSkillMessage, battleSkillMessage.CurMarkId);
		}
	}

	public void TriggerHoverConfirmed()
	{
	}

	public Vector2 GetPosition()
	{
		return LocalToGlobal(Vector2.zero);
	}

	public static UIHandCard_Com_Skill CreateInstance()
	{
		return (UIHandCard_Com_Skill)UIPackage.CreateObject("HandCard", "HandCard_Com_Skill");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showCD = GetControllerAt(0);
		image_Tip = (GImage)GetChildAt(0);
		image_Hover = (GImage)GetChildAt(1);
		btn_Skill = (UIHandCard_Button_Skill)GetChildAt(2);
		di_zhezhao = (GGraph)GetChildAt(3);
		txt_CD = (GTextField)GetChildAt(4);
	}
}
