using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Button_ItemGrade : GButton
{
	public Controller state;

	public const string URL = "ui://xsairahjhh8oqa9";

	public static UISinglePlayer_Button_ItemGrade CreateInstance()
	{
		return (UISinglePlayer_Button_ItemGrade)UIPackage.CreateObject("SinglePlayerStart", "SinglePlayer_Button_ItemGrade");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(1);
	}
}
