using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAccountInfo_Button_Replay : GButton
{
	public Controller type;

	public GTextField txt_Save;

	public GTextField txt_Play;

	public GTextField txt_Delete;

	public GTextField txt_Saved;

	public const string URL = "ui://iepldke7it6m2x";

	public static UIAccountInfo_Button_Replay CreateInstance()
	{
		return (UIAccountInfo_Button_Replay)UIPackage.CreateObject("AccountInfo", "AccountInfo_Button_Replay");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(1);
		txt_Save = (GTextField)GetChildAt(5);
		txt_Play = (GTextField)GetChildAt(6);
		txt_Delete = (GTextField)GetChildAt(7);
		txt_Saved = (GTextField)GetChildAt(8);
	}
}
