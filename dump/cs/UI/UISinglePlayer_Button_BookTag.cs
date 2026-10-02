using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Button_BookTag : GButton
{
	public Controller state;

	public GLoader loader_icon;

	public const string URL = "ui://xsairahjhh8oqa6";

	public static UISinglePlayer_Button_BookTag CreateInstance()
	{
		return (UISinglePlayer_Button_BookTag)UIPackage.CreateObject("SinglePlayerStart", "SinglePlayer_Button_BookTag");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(1);
		loader_icon = (GLoader)GetChildAt(2);
	}
}
