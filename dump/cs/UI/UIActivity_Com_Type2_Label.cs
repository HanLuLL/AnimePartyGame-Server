using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Com_Type2_Label : GComponent
{
	public Controller Status;

	public GButton btn_Item;

	public GTextField txt_taskTitle;

	public UIActivity_Button_TaskStatus btn_taskStatus;

	public GProgressBar bar_task;

	public GButton btn_GoWay;

	public const string URL = "ui://vckl96ksjzxa1l";

	public static UIActivity_Com_Type2_Label CreateInstance()
	{
		return (UIActivity_Com_Type2_Label)UIPackage.CreateObject("Activity", "Activity_Com_Type2_Label");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
		btn_Item = (GButton)GetChildAt(2);
		txt_taskTitle = (GTextField)GetChildAt(3);
		btn_taskStatus = (UIActivity_Button_TaskStatus)GetChildAt(5);
		bar_task = (GProgressBar)GetChildAt(7);
		btn_GoWay = (GButton)GetChildAt(8);
	}
}
