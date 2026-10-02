using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITask_Com_Seven : GComponent
{
	public Controller mouseStatus;

	public Controller taskStatus;

	public UITask_Com_SevenSelectDay com_SelectDay;

	public GList list_Task;

	public GButton btn_GetAllReward;

	public const string URL = "ui://hhpzjcmzl6o63v";

	public static UITask_Com_Seven CreateInstance()
	{
		return (UITask_Com_Seven)UIPackage.CreateObject("Task", "Task_Com_Seven");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mouseStatus = GetControllerAt(0);
		taskStatus = GetControllerAt(1);
		com_SelectDay = (UITask_Com_SevenSelectDay)GetChildAt(1);
		list_Task = (GList)GetChildAt(2);
		btn_GetAllReward = (GButton)GetChildAt(4);
	}
}
