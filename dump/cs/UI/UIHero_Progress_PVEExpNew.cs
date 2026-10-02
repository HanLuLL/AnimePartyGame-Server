using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Progress_PVEExpNew : GProgressBar
{
	public Controller lvVail;

	public Controller maxStatus;

	public GTextField txt_Cur;

	public GTextField txt_Next;

	public const string URL = "ui://7qkd4lqxbfwlq3o";

	public static UIHero_Progress_PVEExpNew CreateInstance()
	{
		return (UIHero_Progress_PVEExpNew)UIPackage.CreateObject("Hero", "Hero_Progress_PVEExpNew");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		lvVail = GetControllerAt(0);
		maxStatus = GetControllerAt(1);
		txt_Cur = (GTextField)GetChildAt(7);
		txt_Next = (GTextField)GetChildAt(8);
	}
}
