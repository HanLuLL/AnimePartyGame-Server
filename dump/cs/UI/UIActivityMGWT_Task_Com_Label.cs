using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityMGWT_Task_Com_Label : GComponent
{
	public Controller Status;

	public GTextField txt_RefreshType;

	public GButton btn_Item;

	public GTextField txt_taskTitle;

	public UIActivityMGWT_Task_Button_TaskStatus btn_taskStatus;

	public GProgressBar bar_task;

	public Transition AnXia;

	public Transition Cut_in;

	public const string URL = "ui://wdl8l4hslwm1l";

	public static UIActivityMGWT_Task_Com_Label CreateInstance()
	{
		return (UIActivityMGWT_Task_Com_Label)UIPackage.CreateObject("ActivityMGWT", "ActivityMGWT_Task_Com_Label");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
		txt_RefreshType = (GTextField)GetChildAt(2);
		btn_Item = (GButton)GetChildAt(3);
		txt_taskTitle = (GTextField)GetChildAt(4);
		btn_taskStatus = (UIActivityMGWT_Task_Button_TaskStatus)GetChildAt(5);
		bar_task = (GProgressBar)GetChildAt(6);
		AnXia = GetTransitionAt(0);
		Cut_in = GetTransitionAt(1);
	}
}
