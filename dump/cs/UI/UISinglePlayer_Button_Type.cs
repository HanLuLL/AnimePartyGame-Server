using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Button_Type : GButton
{
	public Controller state;

	public const string URL = "ui://xsairahjhh8oq9z";

	public static UISinglePlayer_Button_Type CreateInstance()
	{
		return (UISinglePlayer_Button_Type)UIPackage.CreateObject("SinglePlayerStart", "SinglePlayer_Button_Type");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(1);
	}
}
