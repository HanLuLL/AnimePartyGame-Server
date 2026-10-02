using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Button_BookBack : GButton
{
	public Controller state;

	public const string URL = "ui://xsairahjhh8oq9y";

	public static UISinglePlayer_Button_BookBack CreateInstance()
	{
		return (UISinglePlayer_Button_BookBack)UIPackage.CreateObject("SinglePlayerStart", "SinglePlayer_Button_BookBack");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(1);
	}
}
