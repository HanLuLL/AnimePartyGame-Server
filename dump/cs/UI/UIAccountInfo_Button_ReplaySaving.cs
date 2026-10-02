using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAccountInfo_Button_ReplaySaving : GButton
{
	public Transition loop;

	public const string URL = "ui://iepldke7it6m32";

	public static UIAccountInfo_Button_ReplaySaving CreateInstance()
	{
		return (UIAccountInfo_Button_ReplaySaving)UIPackage.CreateObject("AccountInfo", "AccountInfo_Button_ReplaySaving");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loop = GetTransitionAt(0);
	}
}
