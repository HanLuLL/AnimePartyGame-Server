using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFashion_Button_PropItem : GButton
{
	public Controller redpoint;

	public GButton com_Item;

	public Transition Cut_in;

	public const string URL = "ui://dl889m5qlsb44e";

	public static UIFashion_Button_PropItem CreateInstance()
	{
		return (UIFashion_Button_PropItem)UIPackage.CreateObject("Fashion", "Fashion_Button_PropItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redpoint = GetControllerAt(1);
		com_Item = (GButton)GetChildAt(1);
		Cut_in = GetTransitionAt(0);
	}
}
