using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Com_Book : GComponent
{
	public UISinglePlayer_Button_Type btn_build;

	public UISinglePlayer_Button_Type btn_relic;

	public UISinglePlayer_Button_BookBack btn_back;

	public GList list_tag;

	public GList list_item;

	public UISinglePlayer_Button_Dropdown btn_grade;

	public Transition Cut_in;

	public const string URL = "ui://xsairahjhh8oq96";

	public static UISinglePlayer_Com_Book CreateInstance()
	{
		return (UISinglePlayer_Com_Book)UIPackage.CreateObject("SinglePlayerStart", "SinglePlayer_Com_Book");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_build = (UISinglePlayer_Button_Type)GetChildAt(0);
		btn_relic = (UISinglePlayer_Button_Type)GetChildAt(1);
		btn_back = (UISinglePlayer_Button_BookBack)GetChildAt(7);
		list_tag = (GList)GetChildAt(9);
		list_item = (GList)GetChildAt(10);
		btn_grade = (UISinglePlayer_Button_Dropdown)GetChildAt(11);
		Cut_in = GetTransitionAt(0);
	}
}
