using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITask_Com_SevenSelectDay : GComponent
{
	public Controller TabDay;

	public UITask_Button_SevenDay btn_0;

	public UITask_Button_SevenDay btn_1;

	public UITask_Button_SevenDay btn_2;

	public UITask_Button_SevenDay btn_3;

	public UITask_Button_SevenDay btn_4;

	public UITask_Button_SevenDay btn_5;

	public UITask_Button_SevenDay btn_6;

	public const string URL = "ui://hhpzjcmzl6o63w";

	public static UITask_Com_SevenSelectDay CreateInstance()
	{
		return (UITask_Com_SevenSelectDay)UIPackage.CreateObject("Task", "Task_Com_SevenSelectDay");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		TabDay = GetControllerAt(0);
		btn_0 = (UITask_Button_SevenDay)GetChildAt(0);
		btn_1 = (UITask_Button_SevenDay)GetChildAt(1);
		btn_2 = (UITask_Button_SevenDay)GetChildAt(2);
		btn_3 = (UITask_Button_SevenDay)GetChildAt(3);
		btn_4 = (UITask_Button_SevenDay)GetChildAt(4);
		btn_5 = (UITask_Button_SevenDay)GetChildAt(5);
		btn_6 = (UITask_Button_SevenDay)GetChildAt(6);
	}
}
