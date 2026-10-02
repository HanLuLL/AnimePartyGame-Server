using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINewGameLibrary_Com_Difficulty : GComponent
{
	public Controller showSelect;

	public GButton btn_Select;

	public GList list_Difficulty;

	public const string URL = "ui://mc0y3plupj0z7";

	public static UINewGameLibrary_Com_Difficulty CreateInstance()
	{
		return (UINewGameLibrary_Com_Difficulty)UIPackage.CreateObject("NewGameLibrary", "NewGameLibrary_Com_Difficulty");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showSelect = GetControllerAt(0);
		btn_Select = (GButton)GetChildAt(1);
		list_Difficulty = (GList)GetChildAt(2);
	}
}
