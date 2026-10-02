using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Button_BreakThrough : GButton
{
	public GLoader loader_Initial;

	public GLoader loader_BreakThrough;

	public GGraph loader_Video;

	public Transition showBreakThrough;

	public const string URL = "ui://7qkd4lqxyhn0q1l";

	public static UIHero_Button_BreakThrough CreateInstance()
	{
		return (UIHero_Button_BreakThrough)UIPackage.CreateObject("Hero", "Hero_Button_BreakThrough");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Initial = (GLoader)GetChildAt(0);
		loader_BreakThrough = (GLoader)GetChildAt(1);
		loader_Video = (GGraph)GetChildAt(2);
		showBreakThrough = GetTransitionAt(0);
	}
}
