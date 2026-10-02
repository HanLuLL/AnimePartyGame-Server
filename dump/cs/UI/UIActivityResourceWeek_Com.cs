using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityResourceWeek_Com : GComponent
{
	public Controller showTime;

	public GTextField txt_timeTip;

	public GLoader loader_Activity;

	public GTextField txt_Title;

	public GTextField txt_Desc;

	public UIActivityResourceWeek_Button_Toggle btn_TaskToggle;

	public GList list_ActivityWeekTask;

	public const string URL = "ui://rel5h9izuytm1";

	public static UIActivityResourceWeek_Com CreateInstance()
	{
		return (UIActivityResourceWeek_Com)UIPackage.CreateObject("ActivityResourceWeek", "ActivityResourceWeek_Com");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showTime = GetControllerAt(0);
		txt_timeTip = (GTextField)GetChildAt(3);
		loader_Activity = (GLoader)GetChildAt(6);
		txt_Title = (GTextField)GetChildAt(7);
		txt_Desc = (GTextField)GetChildAt(8);
		btn_TaskToggle = (UIActivityResourceWeek_Button_Toggle)GetChildAt(9);
		list_ActivityWeekTask = (GList)GetChildAt(10);
	}
}
