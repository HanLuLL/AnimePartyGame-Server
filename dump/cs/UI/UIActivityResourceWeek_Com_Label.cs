using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityResourceWeek_Com_Label : GComponent
{
	public Controller Status;

	public UIActivityResourceWeek_Button_TaskStatus btn_taskStatus;

	public GTextField txt_taskTitle;

	public GTextField txt_timeTip;

	public GProgressBar bar_task;

	public GTextField txt_taskDesc;

	public GList list_reward;

	public GTextField txt_RefreshType;

	public const string URL = "ui://rel5h9izuytma";

	public static UIActivityResourceWeek_Com_Label CreateInstance()
	{
		return (UIActivityResourceWeek_Com_Label)UIPackage.CreateObject("ActivityResourceWeek", "ActivityResourceWeek_Com_Label");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
		btn_taskStatus = (UIActivityResourceWeek_Button_TaskStatus)GetChildAt(2);
		txt_taskTitle = (GTextField)GetChildAt(5);
		txt_timeTip = (GTextField)GetChildAt(6);
		bar_task = (GProgressBar)GetChildAt(7);
		txt_taskDesc = (GTextField)GetChildAt(8);
		list_reward = (GList)GetChildAt(10);
		txt_RefreshType = (GTextField)GetChildAt(11);
	}
}
