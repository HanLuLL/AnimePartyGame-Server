using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINewGameLibrary_Relic : GComponent
{
	public Controller Search;

	public GButton btn_Return;

	public UINewGameLibrary_btn_search Btn_Del;

	public GTextInput Search_Input;

	public GList list_Relics;

	public GTextField Text_NoSearch;

	public const string URL = "ui://mc0y3plupj0z1r";

	public static UINewGameLibrary_Relic CreateInstance()
	{
		return (UINewGameLibrary_Relic)UIPackage.CreateObject("NewGameLibrary", "NewGameLibrary_Relic");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Search = GetControllerAt(0);
		btn_Return = (GButton)GetChildAt(0);
		Btn_Del = (UINewGameLibrary_btn_search)GetChildAt(2);
		Search_Input = (GTextInput)GetChildAt(3);
		list_Relics = (GList)GetChildAt(5);
		Text_NoSearch = (GTextField)GetChildAt(7);
	}
}
