using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINewGameLibrary_btn_search : GButton
{
	public Controller Type;

	public const string URL = "ui://mc0y3plupj0z1n";

	public static UINewGameLibrary_btn_search CreateInstance()
	{
		return (UINewGameLibrary_btn_search)UIPackage.CreateObject("NewGameLibrary", "NewGameLibrary_btn_search");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Type = GetControllerAt(1);
	}
}
