using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Com_Type4_TaskLabel : GComponent
{
	public GButton btn_Item;

	public GTextField txt_ItemCount;

	public GTextField txt_taskTitle;

	public GTextField txt_Progress;

	public UIActivity_Button_Type4_TaskStatus btn_taskStatus;

	public const string URL = "ui://c1v285vtpu2ik";

	public static UIActivity_Com_Type4_TaskLabel CreateInstance()
	{
		return (UIActivity_Com_Type4_TaskLabel)UIPackage.CreateObject("ActivityNgo", "Activity_Com_Type4_TaskLabel");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_Item = (GButton)GetChildAt(1);
		txt_ItemCount = (GTextField)GetChildAt(2);
		txt_taskTitle = (GTextField)GetChildAt(4);
		txt_Progress = (GTextField)GetChildAt(5);
		btn_taskStatus = (UIActivity_Button_Type4_TaskStatus)GetChildAt(6);
	}
}
