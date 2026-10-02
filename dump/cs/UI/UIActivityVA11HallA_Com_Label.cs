using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityVA11HallA_Com_Label : GComponent
{
	public Controller Status;

	public GTextField txt_RefreshType;

	public GLoader btn_Item;

	public GTextField txt_Count;

	public GTextField txt_taskTitle;

	public UIActivityVA11HallA_Button_TaskStatus btn_taskStatus;

	public GTextField txt_Progress;

	public GLoader btn_GoWay;

	public const string URL = "ui://zlysd2gupgzf4k";

	public static UIActivityVA11HallA_Com_Label CreateInstance()
	{
		return (UIActivityVA11HallA_Com_Label)UIPackage.CreateObject("ActivityVA11HallA", "ActivityVA11HallA_Com_Label");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
		txt_RefreshType = (GTextField)GetChildAt(1);
		btn_Item = (GLoader)GetChildAt(2);
		txt_Count = (GTextField)GetChildAt(3);
		txt_taskTitle = (GTextField)GetChildAt(4);
		btn_taskStatus = (UIActivityVA11HallA_Button_TaskStatus)GetChildAt(5);
		txt_Progress = (GTextField)GetChildAt(7);
		btn_GoWay = (GLoader)GetChildAt(8);
	}
}
