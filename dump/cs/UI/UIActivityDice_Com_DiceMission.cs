using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityDice_Com_DiceMission : GComponent
{
	public Controller Status;

	public UIActivityDice_Button_GoWay btn_GoWay;

	public UIActivityDice_Button_TaskStatus btn_taskStatus;

	public GTextField txt_taskDesc;

	public GProgressBar bar_task;

	public GButton rewardItem;

	public Transition Cut_in;

	public const string URL = "ui://lypih982lbjz12";

	public static UIActivityDice_Com_DiceMission CreateInstance()
	{
		return (UIActivityDice_Com_DiceMission)UIPackage.CreateObject("ActivityDice", "ActivityDice_Com_DiceMission");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
		btn_GoWay = (UIActivityDice_Button_GoWay)GetChildAt(0);
		btn_taskStatus = (UIActivityDice_Button_TaskStatus)GetChildAt(3);
		txt_taskDesc = (GTextField)GetChildAt(4);
		bar_task = (GProgressBar)GetChildAt(5);
		rewardItem = (GButton)GetChildAt(6);
		Cut_in = GetTransitionAt(0);
	}
}
