using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityAdvert_MGWT_Activity_Button : GButton
{
	public Controller language;

	public const string URL = "ui://wkj47h4ilwm1p";

	public static UIActivityAdvert_MGWT_Activity_Button CreateInstance()
	{
		return (UIActivityAdvert_MGWT_Activity_Button)UIPackage.CreateObject("ActivityAdvert", "ActivityAdvert_MGWT_Activity_Button");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		language = GetControllerAt(1);
	}
}
