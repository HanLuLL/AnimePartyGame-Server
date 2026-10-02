using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIShowSkin_Button_SelectInfo : GButton
{
	public Controller type;

	public const string URL = "ui://zfulrgf7ofajqqd";

	public static UIShowSkin_Button_SelectInfo CreateInstance()
	{
		return (UIShowSkin_Button_SelectInfo)UIPackage.CreateObject("ShowSkin", "ShowSkin_Button_SelectInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
	}
}
