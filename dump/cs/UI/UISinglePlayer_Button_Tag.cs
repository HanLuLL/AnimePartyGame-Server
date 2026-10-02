using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Button_Tag : GButton
{
	public Controller state;

	public GLoader loader_tag;

	public Transition Cut_in;

	public const string URL = "ui://xsairahjti1tq8o";

	public static UISinglePlayer_Button_Tag CreateInstance()
	{
		return (UISinglePlayer_Button_Tag)UIPackage.CreateObject("SinglePlayerStart", "SinglePlayer_Button_Tag");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(1);
		loader_tag = (GLoader)GetChildAt(3);
		Cut_in = GetTransitionAt(0);
	}
}
