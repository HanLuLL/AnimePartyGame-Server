using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISettlement_Btn_Score : GButton
{
	public GTextField txt_score;

	public const string URL = "ui://wwhrkd30hh8oz";

	public static UISettlement_Btn_Score CreateInstance()
	{
		return (UISettlement_Btn_Score)UIPackage.CreateObject("SinglePlayerSettlement", "Settlement_Btn_Score");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_score = (GTextField)GetChildAt(4);
	}
}
