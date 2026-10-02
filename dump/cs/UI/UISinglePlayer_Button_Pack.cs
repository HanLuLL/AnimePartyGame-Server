using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Button_Pack : GButton
{
	public Controller state;

	public const string URL = "ui://xsairahjti1tq95";

	public static UISinglePlayer_Button_Pack CreateInstance()
	{
		return (UISinglePlayer_Button_Pack)UIPackage.CreateObject("SinglePlayerStart", "SinglePlayer_Button_Pack");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(1);
	}
}
