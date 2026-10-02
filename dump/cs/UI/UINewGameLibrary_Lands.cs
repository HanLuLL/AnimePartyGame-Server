using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINewGameLibrary_Lands : GComponent
{
	public Controller Search;

	public GButton btn_Return;

	public UINewGameLibrary_btn_search Btn_Del;

	public GTextInput Search_Input;

	public GList list_Lands;

	public GTextField Text_NoSearch;

	public const string URL = "ui://mc0y3plupj0z1p";

	public static UINewGameLibrary_Lands CreateInstance()
	{
		return (UINewGameLibrary_Lands)UIPackage.CreateObject("NewGameLibrary", "NewGameLibrary_Lands");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Search = GetControllerAt(0);
		btn_Return = (GButton)GetChildAt(0);
		Btn_Del = (UINewGameLibrary_btn_search)GetChildAt(2);
		Search_Input = (GTextInput)GetChildAt(3);
		list_Lands = (GList)GetChildAt(5);
		Text_NoSearch = (GTextField)GetChildAt(7);
	}
}
