using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivitySevenDaySignIn_progress : GProgressBar
{
	public GGroup arrow;

	public const string URL = "ui://p8fy3he4rr35y";

	public static UIActivitySevenDaySignIn_progress CreateInstance()
	{
		return (UIActivitySevenDaySignIn_progress)UIPackage.CreateObject("ActivitySevenDaySignIn", "ActivitySevenDaySignIn_progress");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		arrow = (GGroup)GetChildAt(4);
	}
}
