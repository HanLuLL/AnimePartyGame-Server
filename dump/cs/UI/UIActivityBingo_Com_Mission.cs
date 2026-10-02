using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityBingo_Com_Mission : GComponent
{
	public Controller Status;

	public UIActivityBingo_Button_TaskStatus btn_taskStatus;

	public GTextField txt_taskDesc;

	public GProgressBar bar_task;

	public GButton rewardItem;

	public GTextField txt_RefreshType;

	public Transition Cut_in;

	public const string URL = "ui://1pgen0em6brr13";

	public static UIActivityBingo_Com_Mission CreateInstance()
	{
		return (UIActivityBingo_Com_Mission)UIPackage.CreateObject("ActivityBingo", "ActivityBingo_Com_Mission");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
		btn_taskStatus = (UIActivityBingo_Button_TaskStatus)GetChildAt(4);
		txt_taskDesc = (GTextField)GetChildAt(5);
		bar_task = (GProgressBar)GetChildAt(6);
		rewardItem = (GButton)GetChildAt(7);
		txt_RefreshType = (GTextField)GetChildAt(8);
		Cut_in = GetTransitionAt(0);
	}
}
