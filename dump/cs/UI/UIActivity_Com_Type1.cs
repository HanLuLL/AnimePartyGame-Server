using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Com_Type1 : GComponent
{
	public Controller showTime;

	public GList list_ActivityTab;

	public GTextField txt_timeTip;

	public GLoader loader_Activity;

	public GTextField txt_Title;

	public GTextField txt_Desc;

	public UIActivity_Button_Toggle btn_TaskToggle;

	public GList list_ActivityTask;

	public const string URL = "ui://vckl96ksbakuq";

	public static UIActivity_Com_Type1 CreateInstance()
	{
		return (UIActivity_Com_Type1)UIPackage.CreateObject("Activity", "Activity_Com_Type1");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showTime = GetControllerAt(0);
		list_ActivityTab = (GList)GetChildAt(2);
		txt_timeTip = (GTextField)GetChildAt(4);
		loader_Activity = (GLoader)GetChildAt(7);
		txt_Title = (GTextField)GetChildAt(8);
		txt_Desc = (GTextField)GetChildAt(9);
		btn_TaskToggle = (UIActivity_Button_Toggle)GetChildAt(10);
		list_ActivityTask = (GList)GetChildAt(11);
	}
}
