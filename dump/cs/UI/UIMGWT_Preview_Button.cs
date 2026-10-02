using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIMGWT_Preview_Button : GButton
{
	public Controller language;

	public const string URL = "ui://2p754tqkilj5q";

	public static UIMGWT_Preview_Button CreateInstance()
	{
		return (UIMGWT_Preview_Button)UIPackage.CreateObject("MGWTStore", "MGWT_Preview_Button");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		language = GetControllerAt(1);
	}
}
