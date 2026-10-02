using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINewGameLibrary_Com_ComboBox : GComboBox
{
	public Controller SetArrow;

	public const string URL = "ui://mc0y3plus8o93n";

	public static UINewGameLibrary_Com_ComboBox CreateInstance()
	{
		return (UINewGameLibrary_Com_ComboBox)UIPackage.CreateObject("NewGameLibrary", "NewGameLibrary_Com_ComboBox");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		SetArrow = GetControllerAt(1);
	}
}
