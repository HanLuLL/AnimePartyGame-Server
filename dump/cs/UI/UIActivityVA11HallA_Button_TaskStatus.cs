using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityVA11HallA_Button_TaskStatus : GButton
{
	public Controller Status;

	public GTextField txt_Running;

	public GTextField txt_Ok;

	public GTextField txt_Finish;

	public const string URL = "ui://zlysd2gupgzf4m";

	public static UIActivityVA11HallA_Button_TaskStatus CreateInstance()
	{
		return (UIActivityVA11HallA_Button_TaskStatus)UIPackage.CreateObject("ActivityVA11HallA", "ActivityVA11HallA_Button_TaskStatus");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(1);
		txt_Running = (GTextField)GetChildAt(2);
		txt_Ok = (GTextField)GetChildAt(3);
		txt_Finish = (GTextField)GetChildAt(4);
	}
}
