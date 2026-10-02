using Core.Mark;
using FairyGUI;
using FairyGUI.Utils;
using UnityEngine;

namespace UI;

public class UIBattleInfo_Button_ShowRelic : GButton, IMarkTarget
{
	public GImage image_Tip;

	public GImage image_Hover;

	public GTextField txt_Title;

	public const string URL = "ui://fxejlqlfot0w91";

	bool IMarkTarget.HoverWait => true;

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
	}

	public void OnMarkHoverExit()
	{
		image_Hover.visible = false;
	}

	public void OnMarkSelected()
	{
		OnMarkTipHide();
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

	public static UIBattleInfo_Button_ShowRelic CreateInstance()
	{
		return (UIBattleInfo_Button_ShowRelic)UIPackage.CreateObject("BattleInfo", "BattleInfo_Button_ShowRelic");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		image_Tip = (GImage)GetChildAt(0);
		image_Hover = (GImage)GetChildAt(1);
		txt_Title = (GTextField)GetChildAt(5);
	}
}
