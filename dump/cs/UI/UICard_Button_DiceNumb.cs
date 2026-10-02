using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICard_Button_DiceNumb : GButton
{
	public Controller numType;

	public const string URL = "ui://bi8fdi6nqy402d";

	public static UICard_Button_DiceNumb CreateInstance()
	{
		return (UICard_Button_DiceNumb)UIPackage.CreateObject("Card", "Card_Button_DiceNumb");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		numType = GetControllerAt(1);
	}
}
