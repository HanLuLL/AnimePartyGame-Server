using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_InvitePlayer : GComponent
{
	public Controller status;

	public GGraph btn_Invite;

	public GRichTextField txt_Invite;

	public const string URL = "ui://m6sn3r22gib2q3p";

	public static UICom_InvitePlayer CreateInstance()
	{
		return (UICom_InvitePlayer)UIPackage.CreateObject("Common_External", "Com_InvitePlayer");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		btn_Invite = (GGraph)GetChildAt(0);
		txt_Invite = (GRichTextField)GetChildAt(1);
	}
}
