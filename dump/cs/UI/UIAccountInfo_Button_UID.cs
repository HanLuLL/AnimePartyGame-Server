using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAccountInfo_Button_UID : GButton
{
	public GTextField txt_UID;

	public const string URL = "ui://iepldke7p5xi2j";

	public static UIAccountInfo_Button_UID CreateInstance()
	{
		return (UIAccountInfo_Button_UID)UIPackage.CreateObject("AccountInfo", "AccountInfo_Button_UID");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_UID = (GTextField)GetChildAt(0);
	}
}
