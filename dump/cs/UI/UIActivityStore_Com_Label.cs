using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStore_Com_Label : GComponent
{
	public Controller Status;

	public GGraph btn_GoWay;

	public GGroup group_Go;

	public GTextField txt_RefreshType;

	public GButton btn_Item;

	public GTextField txt_taskTitle;

	public UIActivityStore_Button_TaskStatus btn_taskStatus;

	public GProgressBar bar_task;

	public Transition AnXia;

	public Transition Cut_in;

	public const string URL = "ui://88m1yfwgpgrv3x";

	public static UIActivityStore_Com_Label CreateInstance()
	{
		return (UIActivityStore_Com_Label)UIPackage.CreateObject("ActivityStore", "ActivityStore_Com_Label");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
		btn_GoWay = (GGraph)GetChildAt(2);
		group_Go = (GGroup)GetChildAt(3);
		txt_RefreshType = (GTextField)GetChildAt(6);
		btn_Item = (GButton)GetChildAt(7);
		txt_taskTitle = (GTextField)GetChildAt(8);
		btn_taskStatus = (UIActivityStore_Button_TaskStatus)GetChildAt(9);
		bar_task = (GProgressBar)GetChildAt(11);
		AnXia = GetTransitionAt(0);
		Cut_in = GetTransitionAt(1);
	}
}
