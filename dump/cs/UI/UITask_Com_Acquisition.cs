using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITask_Com_Acquisition : GComponent
{
	public Controller tab;

	public GLoader loader_Character;

	public GList list_Menu;

	public UITask_Com_Acquisition_AcceptInvite com_AcceptInvite;

	public UITask_Com_Acquisition_Invite com_Invite;

	public GButton btn_Rule;

	public Transition Cut_in;

	public const string URL = "ui://hhpzjcmzh38b48";

	public static UITask_Com_Acquisition CreateInstance()
	{
		return (UITask_Com_Acquisition)UIPackage.CreateObject("Task", "Task_Com_Acquisition");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tab = GetControllerAt(0);
		loader_Character = (GLoader)GetChildAt(0);
		list_Menu = (GList)GetChildAt(1);
		com_AcceptInvite = (UITask_Com_Acquisition_AcceptInvite)GetChildAt(2);
		com_Invite = (UITask_Com_Acquisition_Invite)GetChildAt(3);
		btn_Rule = (GButton)GetChildAt(4);
		Cut_in = GetTransitionAt(0);
	}
}
