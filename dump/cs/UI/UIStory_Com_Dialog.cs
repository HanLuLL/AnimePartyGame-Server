using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStory_Com_Dialog : GButton
{
	public Controller frame;

	public GLoader loader_Style;

	public GRichTextField txt_Dialog;

	public Transition Loop;

	public const string URL = "ui://abmw5cfoec7w4";

	public static UIStory_Com_Dialog CreateInstance()
	{
		return (UIStory_Com_Dialog)UIPackage.CreateObject("Story", "Story_Com_Dialog");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		frame = GetControllerAt(1);
		loader_Style = (GLoader)GetChildAt(1);
		txt_Dialog = (GRichTextField)GetChildAt(2);
		Loop = GetTransitionAt(0);
	}
}
