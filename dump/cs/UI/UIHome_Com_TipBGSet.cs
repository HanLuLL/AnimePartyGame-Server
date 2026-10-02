using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHome_Com_TipBGSet : GComponent
{
	public GTextField Text_DragTip;

	public GTextField Text_ScaleTip;

	public UIHero_Button_Set Btn_Confirm;

	public UIHero_Button_Set Btn_Cancel;

	public UIHero_Button_Set Btn_Reset;

	public const string URL = "ui://u7xbdcgukrvoq4f";

	public static UIHome_Com_TipBGSet CreateInstance()
	{
		return (UIHome_Com_TipBGSet)UIPackage.CreateObject("Home", "Home_Com_TipBGSet");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Text_DragTip = (GTextField)GetChildAt(0);
		Text_ScaleTip = (GTextField)GetChildAt(1);
		Btn_Confirm = (UIHero_Button_Set)GetChildAt(2);
		Btn_Cancel = (UIHero_Button_Set)GetChildAt(3);
		Btn_Reset = (UIHero_Button_Set)GetChildAt(4);
	}
}
