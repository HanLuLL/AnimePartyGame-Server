using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINewGameLibrary_Cards : GComponent
{
	public Controller Search;

	public Controller showFilter;

	public GTextField Text_NoSearch;

	public GList list_Cards;

	public GButton btn_Return;

	public UINewGameLibrary_btn_search Btn_Del;

	public GTextInput Search_Input;

	public UINewGameLibrary_Com_ComboBox DropDown_Filter;

	public const string URL = "ui://mc0y3plupj0z1m";

	public static UINewGameLibrary_Cards CreateInstance()
	{
		return (UINewGameLibrary_Cards)UIPackage.CreateObject("NewGameLibrary", "NewGameLibrary_Cards");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Search = GetControllerAt(0);
		showFilter = GetControllerAt(1);
		Text_NoSearch = (GTextField)GetChildAt(1);
		list_Cards = (GList)GetChildAt(2);
		btn_Return = (GButton)GetChildAt(3);
		Btn_Del = (UINewGameLibrary_btn_search)GetChildAt(5);
		Search_Input = (GTextInput)GetChildAt(6);
		DropDown_Filter = (UINewGameLibrary_Com_ComboBox)GetChildAt(8);
	}
}
