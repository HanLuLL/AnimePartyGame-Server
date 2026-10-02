using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISettlement_Com_Info : GComponent
{
	public Controller NewRecord;

	public UISettlement_Btn_Score btn_score_1;

	public UISettlement_Btn_Score btn_score_2;

	public UISettlement_Btn_Score btn_score_3;

	public GList list_coin;

	public const string URL = "ui://wwhrkd30hh8oy";

	public static UISettlement_Com_Info CreateInstance()
	{
		return (UISettlement_Com_Info)UIPackage.CreateObject("SinglePlayerSettlement", "Settlement_Com_Info");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		NewRecord = GetControllerAt(0);
		btn_score_1 = (UISettlement_Btn_Score)GetChildAt(4);
		btn_score_2 = (UISettlement_Btn_Score)GetChildAt(5);
		btn_score_3 = (UISettlement_Btn_Score)GetChildAt(6);
		list_coin = (GList)GetChildAt(7);
	}
}
