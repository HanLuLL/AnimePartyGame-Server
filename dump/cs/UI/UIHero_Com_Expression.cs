using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_Expression : GButton
{
	public Controller type;

	public Controller empty;

	public GLoader loader_Expression_Image;

	public GGraph loader_Expression_Video;

	public Transition Cut_in;

	public const string URL = "ui://7qkd4lqxg1lk1a";

	public static UIHero_Com_Expression CreateInstance()
	{
		return (UIHero_Com_Expression)UIPackage.CreateObject("Hero", "Hero_Com_Expression");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(1);
		empty = GetControllerAt(2);
		loader_Expression_Image = (GLoader)GetChildAt(5);
		loader_Expression_Video = (GGraph)GetChildAt(6);
		Cut_in = GetTransitionAt(0);
	}
}
