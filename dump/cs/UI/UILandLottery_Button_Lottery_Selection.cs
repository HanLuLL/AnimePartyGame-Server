using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandLottery_Button_Lottery_Selection : GButton
{
	public GTextField txt;

	public const string URL = "ui://d6gnxdx1rum7i";

	public static UILandLottery_Button_Lottery_Selection CreateInstance()
	{
		return (UILandLottery_Button_Lottery_Selection)UIPackage.CreateObject("LandLottery", "LandLottery_Button_Lottery_Selection");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt = (GTextField)GetChildAt(2);
	}
}
