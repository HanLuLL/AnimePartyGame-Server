using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Button_Confirm : GButton
{
	public Transition Cut_in;

	public const string URL = "ui://xsairahjpmrnq8n";

	public static UISinglePlayer_Button_Confirm CreateInstance()
	{
		return (UISinglePlayer_Button_Confirm)UIPackage.CreateObject("SinglePlayerStart", "SinglePlayer_Button_Confirm");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Cut_in = GetTransitionAt(0);
	}
}
