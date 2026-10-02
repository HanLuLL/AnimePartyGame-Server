using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandBattery_Button_RoleHeadshot : GButton
{
	public GButton com_playerInfo;

	public GGraph effect;

	public const string URL = "ui://dc8zac77rum73";

	public static UILandBattery_Button_RoleHeadshot CreateInstance()
	{
		return (UILandBattery_Button_RoleHeadshot)UIPackage.CreateObject("LandBattery", "LandBattery_Button_RoleHeadshot");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_playerInfo = (GButton)GetChildAt(0);
		effect = (GGraph)GetChildAt(1);
	}
}
