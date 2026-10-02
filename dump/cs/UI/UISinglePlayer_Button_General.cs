using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Button_General : GButton
{
	public Controller type;

	public const string URL = "ui://xsairahjti1tq94";

	public static UISinglePlayer_Button_General CreateInstance()
	{
		return (UISinglePlayer_Button_General)UIPackage.CreateObject("SinglePlayerStart", "SinglePlayer_Button_General");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(1);
	}
}
