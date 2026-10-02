using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_Progress : GComponent
{
	public GProgressBar progress_Heart_0;

	public GProgressBar progress_Heart_1;

	public GProgressBar progress_Heart_2;

	public GProgressBar progress_Heart_3;

	public GProgressBar progress_Heart_4;

	public const string URL = "ui://7qkd4lqxg1lk1c";

	public static UIHero_Com_Progress CreateInstance()
	{
		return (UIHero_Com_Progress)UIPackage.CreateObject("Hero", "Hero_Com_Progress");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		progress_Heart_0 = (GProgressBar)GetChildAt(0);
		progress_Heart_1 = (GProgressBar)GetChildAt(1);
		progress_Heart_2 = (GProgressBar)GetChildAt(2);
		progress_Heart_3 = (GProgressBar)GetChildAt(3);
		progress_Heart_4 = (GProgressBar)GetChildAt(4);
	}
}
