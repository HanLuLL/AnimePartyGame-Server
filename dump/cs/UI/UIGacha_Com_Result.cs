using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGacha_Com_Result : GComponent
{
	public Controller againGacha;

	public UIGacha_Button_ResultItem com_Result_Item_0;

	public UIGacha_Button_ResultItem com_Result_Item_1;

	public UIGacha_Button_ResultItem com_Result_Item_2;

	public UIGacha_Button_ResultItem com_Result_Item_3;

	public UIGacha_Button_ResultItem com_Result_Item_4;

	public UIGacha_Button_ResultItem com_Result_Item_5;

	public UIGacha_Button_ResultItem com_Result_Item_6;

	public UIGacha_Button_ResultItem com_Result_Item_7;

	public UIGacha_Button_ResultItem com_Result_Item_8;

	public UIGacha_Button_ResultItem com_Result_Item_9;

	public UIGacha_Button_ResultItem com_Result_Item;

	public GButton btn_Sure_Again;

	public GButton btn_Sure_NoAgain;

	public GButton btn_GachaAgain;

	public Transition showResult_Single;

	public Transition showResult_Multi;

	public const string URL = "ui://j90wpcmnabqa19";

	public static UIGacha_Com_Result CreateInstance()
	{
		return (UIGacha_Com_Result)UIPackage.CreateObject("Gacha", "Gacha_Com_Result");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		againGacha = GetControllerAt(0);
		com_Result_Item_0 = (UIGacha_Button_ResultItem)GetChildAt(0);
		com_Result_Item_1 = (UIGacha_Button_ResultItem)GetChildAt(1);
		com_Result_Item_2 = (UIGacha_Button_ResultItem)GetChildAt(2);
		com_Result_Item_3 = (UIGacha_Button_ResultItem)GetChildAt(3);
		com_Result_Item_4 = (UIGacha_Button_ResultItem)GetChildAt(4);
		com_Result_Item_5 = (UIGacha_Button_ResultItem)GetChildAt(5);
		com_Result_Item_6 = (UIGacha_Button_ResultItem)GetChildAt(6);
		com_Result_Item_7 = (UIGacha_Button_ResultItem)GetChildAt(7);
		com_Result_Item_8 = (UIGacha_Button_ResultItem)GetChildAt(8);
		com_Result_Item_9 = (UIGacha_Button_ResultItem)GetChildAt(9);
		com_Result_Item = (UIGacha_Button_ResultItem)GetChildAt(10);
		btn_Sure_Again = (GButton)GetChildAt(11);
		btn_Sure_NoAgain = (GButton)GetChildAt(12);
		btn_GachaAgain = (GButton)GetChildAt(13);
		showResult_Single = GetTransitionAt(0);
		showResult_Multi = GetTransitionAt(1);
	}
}
