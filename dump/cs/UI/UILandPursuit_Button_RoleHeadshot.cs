using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandPursuit_Button_RoleHeadshot : GButton
{
	public GButton com_playerInfo;

	public GGraph effect;

	public const string URL = "ui://dkvy8z1orum73";

	public static UILandPursuit_Button_RoleHeadshot CreateInstance()
	{
		return (UILandPursuit_Button_RoleHeadshot)UIPackage.CreateObject("LandPursuit", "LandPursuit_Button_RoleHeadshot");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_playerInfo = (GButton)GetChildAt(0);
		effect = (GGraph)GetChildAt(1);
	}
}
