using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Button_Level : GButton
{
	public Controller type;

	public const string URL = "ui://xsairahjhh8oq9w";

	public static UISinglePlayer_Button_Level CreateInstance()
	{
		return (UISinglePlayer_Button_Level)UIPackage.CreateObject("SinglePlayerStart", "SinglePlayer_Button_Level");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(1);
	}
}
