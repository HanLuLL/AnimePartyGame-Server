using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIInviteFriend_Button_PlayerItem : GButton
{
	public Controller status;

	public GComponent com_PlayerLabel;

	public GGroup group_btn;

	public Transition Cut_in;

	public const string URL = "ui://24i5r2i07h6s5";

	public static UIInviteFriend_Button_PlayerItem CreateInstance()
	{
		return (UIInviteFriend_Button_PlayerItem)UIPackage.CreateObject("Invite", "InviteFriend_Button_PlayerItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
		com_PlayerLabel = (GComponent)GetChildAt(1);
		group_btn = (GGroup)GetChildAt(4);
		Cut_in = GetTransitionAt(0);
	}
}
