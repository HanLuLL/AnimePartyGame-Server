using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITask_Com_Acquisition_AcceptInvite : GComponent
{
	public Controller status;

	public GTextInput Input_Code;

	public GButton btn_SureCode;

	public GLoader loader_Icon;

	public GTextField txt_itemNum;

	public GList list_Task;

	public const string URL = "ui://hhpzjcmzh38b4g";

	public static UITask_Com_Acquisition_AcceptInvite CreateInstance()
	{
		return (UITask_Com_Acquisition_AcceptInvite)UIPackage.CreateObject("Task", "Task_Com_Acquisition_AcceptInvite");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		Input_Code = (GTextInput)GetChildAt(5);
		btn_SureCode = (GButton)GetChildAt(6);
		loader_Icon = (GLoader)GetChildAt(9);
		txt_itemNum = (GTextField)GetChildAt(12);
		list_Task = (GList)GetChildAt(17);
	}
}
