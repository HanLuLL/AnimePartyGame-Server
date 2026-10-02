using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRookieTask_Com_AchieveLabel : GComponent
{
	public Controller Status;

	public UIRookieTask_Button_TaskStatus btn_taskStatus;

	public GTextField txt_taskDesc;

	public GProgressBar bar_task;

	public GButton rewardItem;

	public UITask_Button_GoWay btn_GoWay;

	public const string URL = "ui://rx1j3readzvf1";

	public static UIRookieTask_Com_AchieveLabel CreateInstance()
	{
		return (UIRookieTask_Com_AchieveLabel)UIPackage.CreateObject("ActivityRookieTask", "RookieTask_Com_AchieveLabel");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
		btn_taskStatus = (UIRookieTask_Button_TaskStatus)GetChildAt(2);
		txt_taskDesc = (GTextField)GetChildAt(3);
		bar_task = (GProgressBar)GetChildAt(4);
		rewardItem = (GButton)GetChildAt(5);
		btn_GoWay = (UITask_Button_GoWay)GetChildAt(6);
	}
}
