using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITask_WeekTaskLiveness : GComponent
{
	public GTextField txt_liveValue;

	public GProgressBar bar_liveness;

	public GList list_Box;

	public const string URL = "ui://hhpzjcmzthbm34";

	public static UITask_WeekTaskLiveness CreateInstance()
	{
		return (UITask_WeekTaskLiveness)UIPackage.CreateObject("Task", "Task_WeekTaskLiveness");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_liveValue = (GTextField)GetChildAt(2);
		bar_liveness = (GProgressBar)GetChildAt(3);
		list_Box = (GList)GetChildAt(4);
	}
}
