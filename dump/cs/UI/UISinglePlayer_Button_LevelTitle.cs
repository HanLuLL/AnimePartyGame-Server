using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Button_LevelTitle : GButton
{
	public Controller type;

	public const string URL = "ui://xsairahjhh8oq9v";

	public static UISinglePlayer_Button_LevelTitle CreateInstance()
	{
		return (UISinglePlayer_Button_LevelTitle)UIPackage.CreateObject("SinglePlayerStart", "SinglePlayer_Button_LevelTitle");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(1);
	}
}
