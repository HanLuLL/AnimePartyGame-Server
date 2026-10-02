using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Com_SelectTag : GComponent
{
	public GList list_tag;

	public GTextField txt_num;

	public UISinglePlayer_Button_General btn_book;

	public UISinglePlayer_Button_General btn_canel;

	public UISinglePlayer_Button_General btn_ok;

	public UISinglePlayer_Button_Arrow btn_left;

	public UISinglePlayer_Button_Arrow btn_right;

	public Transition Cut_in;

	public const string URL = "ui://xsairahjti1tq8p";

	public static UISinglePlayer_Com_SelectTag CreateInstance()
	{
		return (UISinglePlayer_Com_SelectTag)UIPackage.CreateObject("SinglePlayerStart", "SinglePlayer_Com_SelectTag");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_tag = (GList)GetChildAt(6);
		txt_num = (GTextField)GetChildAt(8);
		btn_book = (UISinglePlayer_Button_General)GetChildAt(9);
		btn_canel = (UISinglePlayer_Button_General)GetChildAt(10);
		btn_ok = (UISinglePlayer_Button_General)GetChildAt(11);
		btn_left = (UISinglePlayer_Button_Arrow)GetChildAt(12);
		btn_right = (UISinglePlayer_Button_Arrow)GetChildAt(13);
		Cut_in = GetTransitionAt(0);
	}
}
