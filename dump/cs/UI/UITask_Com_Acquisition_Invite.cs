using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITask_Com_Acquisition_Invite : GComponent
{
	public GTextField txt_InviteCode;

	public GButton btn_CopyCode;

	public GList list_Task;

	public const string URL = "ui://hhpzjcmzh38b4h";

	public static UITask_Com_Acquisition_Invite CreateInstance()
	{
		return (UITask_Com_Acquisition_Invite)UIPackage.CreateObject("Task", "Task_Com_Acquisition_Invite");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_InviteCode = (GTextField)GetChildAt(5);
		btn_CopyCode = (GButton)GetChildAt(6);
		list_Task = (GList)GetChildAt(9);
	}
}
