using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityComeback_Task_Com_Label : GComponent
{
	public Controller Status;

	public GTextField txt_RefreshType;

	public GButton btn_Item;

	public GTextField txt_taskTitle;

	public UIActivityComeback_Task_Button_TaskStatus btn_taskStatus;

	public GProgressBar bar_task;

	public GGraph btn_GoWay;

	public GGroup group_Go;

	public Transition AnXia;

	public Transition Cut_in;

	public const string URL = "ui://hconmwfcy9qnc";

	public static UIActivityComeback_Task_Com_Label CreateInstance()
	{
		return (UIActivityComeback_Task_Com_Label)UIPackage.CreateObject("ActivityComeback", "ActivityComeback_Task_Com_Label");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
		txt_RefreshType = (GTextField)GetChildAt(2);
		btn_Item = (GButton)GetChildAt(3);
		txt_taskTitle = (GTextField)GetChildAt(4);
		btn_taskStatus = (UIActivityComeback_Task_Button_TaskStatus)GetChildAt(5);
		bar_task = (GProgressBar)GetChildAt(7);
		btn_GoWay = (GGraph)GetChildAt(10);
		group_Go = (GGroup)GetChildAt(11);
		AnXia = GetTransitionAt(0);
		Cut_in = GetTransitionAt(1);
	}
}
