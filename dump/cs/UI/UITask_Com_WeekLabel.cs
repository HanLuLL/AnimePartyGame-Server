using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITask_Com_WeekLabel : GComponent
{
	public Controller Status;

	public UITask_Button_TaskStatus btn_taskStatus;

	public GTextField txt_taskTitle;

	public GProgressBar bar_task;

	public GTextField txt_taskDesc;

	public GList list_reward;

	public const string URL = "ui://hhpzjcmzkqgj47";

	public static UITask_Com_WeekLabel CreateInstance()
	{
		return (UITask_Com_WeekLabel)UIPackage.CreateObject("Task", "Task_Com_WeekLabel");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
		btn_taskStatus = (UITask_Button_TaskStatus)GetChildAt(2);
		txt_taskTitle = (GTextField)GetChildAt(5);
		bar_task = (GProgressBar)GetChildAt(6);
		txt_taskDesc = (GTextField)GetChildAt(7);
		list_reward = (GList)GetChildAt(9);
	}
}
