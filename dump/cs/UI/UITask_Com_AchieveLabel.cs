using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITask_Com_AchieveLabel : GComponent
{
	public Controller Status;

	public UITask_Button_TaskStatus btn_taskStatus;

	public GTextField txt_taskTitle;

	public GProgressBar bar_task;

	public GTextField txt_taskDesc;

	public GList list_reward;

	public GLoader load_Icon;

	public const string URL = "ui://hhpzjcmzthbm33";

	public static UITask_Com_AchieveLabel CreateInstance()
	{
		return (UITask_Com_AchieveLabel)UIPackage.CreateObject("Task", "Task_Com_AchieveLabel");
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
		load_Icon = (GLoader)GetChildAt(10);
	}
}
