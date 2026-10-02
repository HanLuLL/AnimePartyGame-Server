using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandLottery_Com_Number : GComponent
{
	public Controller numType;

	public const string URL = "ui://d6gnxdx1hmjfx";

	public static UILandLottery_Com_Number CreateInstance()
	{
		return (UILandLottery_Com_Number)UIPackage.CreateObject("LandLottery", "LandLottery_Com_Number");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		numType = GetControllerAt(0);
	}
}
