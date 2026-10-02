using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Button_Item : GButton
{
	public Controller rare;

	public GLoader loader_rare;

	public GLoader loader_icon;

	public GRichTextField txt_desc;

	public Transition Cut_in;

	public const string URL = "ui://xsairahjhh8oqa0";

	public static UISinglePlayer_Button_Item CreateInstance()
	{
		return (UISinglePlayer_Button_Item)UIPackage.CreateObject("SinglePlayerStart", "SinglePlayer_Button_Item");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		rare = GetControllerAt(1);
		loader_rare = (GLoader)GetChildAt(1);
		loader_icon = (GLoader)GetChildAt(3);
		txt_desc = (GRichTextField)GetChildAt(4);
		Cut_in = GetTransitionAt(0);
	}
}
