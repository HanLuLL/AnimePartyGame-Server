using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIInviteFriend_Button_Grip : GButton
{
	public GGraph grip;

	public const string URL = "ui://24i5r2i07h6sk";

	public static UIInviteFriend_Button_Grip CreateInstance()
	{
		return (UIInviteFriend_Button_Grip)UIPackage.CreateObject("Invite", "InviteFriend_Button_Grip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		grip = (GGraph)GetChildAt(0);
	}
}
