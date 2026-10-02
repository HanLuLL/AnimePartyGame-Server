using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINewGameLibrary_Com_ComboBox_popup : GComponent
{
	public GList list;

	public const string URL = "ui://mc0y3plus8o93q";

	public static UINewGameLibrary_Com_ComboBox_popup CreateInstance()
	{
		return (UINewGameLibrary_Com_ComboBox_popup)UIPackage.CreateObject("NewGameLibrary", "NewGameLibrary_Com_ComboBox_popup");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list = (GList)GetChildAt(1);
	}
}
