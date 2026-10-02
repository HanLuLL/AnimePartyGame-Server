using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivitySevenDaySignInPanel : GComponent
{
	public GLoader bg;

	public UIActivitySevenDaySignIn_Btn day1;

	public UIActivitySevenDaySignIn_Btn day2;

	public UIActivitySevenDaySignIn_Btn day3;

	public UIActivitySevenDaySignIn_Btn day4;

	public UIActivitySevenDaySignIn_Btn day5;

	public UIActivitySevenDaySignIn_Btn day6;

	public UIActivitySevenDaySignIn_Btn day7;

	public GTextField txt_duration;

	public UIActivitySevenDaySignIn_progress ActivitySevenDaySignIn_Progress;

	public Transition Cut_in;

	public const string URL = "ui://p8fy3he4rr350";

	public static UIActivitySevenDaySignInPanel CreateInstance()
	{
		BindAll();
		return (UIActivitySevenDaySignInPanel)UIPackage.CreateObject("ActivitySevenDaySignIn", "ActivitySevenDaySignInPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://p8fy3he4rr350", typeof(UIActivitySevenDaySignInPanel));
		UIObjectFactory.SetPackageItemExtension("ui://p8fy3he4rr3513", typeof(UIActivitySevenDaySignIn_Btn));
		UIObjectFactory.SetPackageItemExtension("ui://p8fy3he4rr35y", typeof(UIActivitySevenDaySignIn_progress));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		bg = (GLoader)GetChildAt(0);
		day1 = (UIActivitySevenDaySignIn_Btn)GetChildAt(2);
		day2 = (UIActivitySevenDaySignIn_Btn)GetChildAt(3);
		day3 = (UIActivitySevenDaySignIn_Btn)GetChildAt(4);
		day4 = (UIActivitySevenDaySignIn_Btn)GetChildAt(5);
		day5 = (UIActivitySevenDaySignIn_Btn)GetChildAt(6);
		day6 = (UIActivitySevenDaySignIn_Btn)GetChildAt(7);
		day7 = (UIActivitySevenDaySignIn_Btn)GetChildAt(8);
		txt_duration = (GTextField)GetChildAt(9);
		ActivitySevenDaySignIn_Progress = (UIActivitySevenDaySignIn_progress)GetChildAt(12);
		Cut_in = GetTransitionAt(0);
	}
}
