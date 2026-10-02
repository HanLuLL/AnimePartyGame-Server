using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIATM_Button_RoleHeadshot : GButton
{
	public GButton com_playerInfo;

	public GGraph effect;

	public const string URL = "ui://lv54en7xbnngh";

	public static UIATM_Button_RoleHeadshot CreateInstance()
	{
		return (UIATM_Button_RoleHeadshot)UIPackage.CreateObject("ATM", "ATM_Button_RoleHeadshot");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_playerInfo = (GButton)GetChildAt(0);
		effect = (GGraph)GetChildAt(1);
	}
}
