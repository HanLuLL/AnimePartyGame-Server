using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Button_Dropdown : GButton
{
	public Controller state;

	public UISinglePlayer_Button_ItemGrade btn_1;

	public UISinglePlayer_Button_ItemGrade btn_2;

	public UISinglePlayer_Button_ItemGrade btn_3;

	public UISinglePlayer_Button_ItemGrade btn_4;

	public const string URL = "ui://xsairahjhh8oqa8";

	public static UISinglePlayer_Button_Dropdown CreateInstance()
	{
		return (UISinglePlayer_Button_Dropdown)UIPackage.CreateObject("SinglePlayerStart", "SinglePlayer_Button_Dropdown");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(1);
		btn_1 = (UISinglePlayer_Button_ItemGrade)GetChildAt(7);
		btn_2 = (UISinglePlayer_Button_ItemGrade)GetChildAt(8);
		btn_3 = (UISinglePlayer_Button_ItemGrade)GetChildAt(9);
		btn_4 = (UISinglePlayer_Button_ItemGrade)GetChildAt(10);
	}
}
