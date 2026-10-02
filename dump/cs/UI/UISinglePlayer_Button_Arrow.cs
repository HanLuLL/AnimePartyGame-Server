using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Button_Arrow : GButton
{
	public Controller state;

	public const string URL = "ui://xsairahjhh8oqa7";

	public static UISinglePlayer_Button_Arrow CreateInstance()
	{
		return (UISinglePlayer_Button_Arrow)UIPackage.CreateObject("SinglePlayerStart", "SinglePlayer_Button_Arrow");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(1);
	}
}
