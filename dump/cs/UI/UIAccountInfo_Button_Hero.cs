using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAccountInfo_Button_Hero : GButton
{
	public Controller breakthrough;

	public UIAccountInfo_Loader_Hero loader_Character;

	public GTextField txt_chrname;

	public GButton btn_Collect;

	public Transition Cut_in;

	public const string URL = "ui://iepldke7hni04";

	public static UIAccountInfo_Button_Hero CreateInstance()
	{
		return (UIAccountInfo_Button_Hero)UIPackage.CreateObject("AccountInfo", "AccountInfo_Button_Hero");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		breakthrough = GetControllerAt(1);
		loader_Character = (UIAccountInfo_Loader_Hero)GetChildAt(4);
		txt_chrname = (GTextField)GetChildAt(5);
		btn_Collect = (GButton)GetChildAt(6);
		Cut_in = GetTransitionAt(0);
	}
}
