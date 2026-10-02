using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGacha_Button_TabSelect : GButton
{
	public Controller redPoint;

	public GGraph zhezhao;

	public GTextField txt_SubTitle;

	public const string URL = "ui://j90wpcmndmntqq2r";

	public static UIGacha_Button_TabSelect CreateInstance()
	{
		return (UIGacha_Button_TabSelect)UIPackage.CreateObject("Gacha", "Gacha_Button_TabSelect");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redPoint = GetControllerAt(1);
		zhezhao = (GGraph)GetChildAt(0);
		txt_SubTitle = (GTextField)GetChildAt(5);
	}
}
