using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityAdvert_MGWT_Store_Button : GButton
{
	public Controller language;

	public const string URL = "ui://wkj47h4ilwm1o";

	public static UIActivityAdvert_MGWT_Store_Button CreateInstance()
	{
		return (UIActivityAdvert_MGWT_Store_Button)UIPackage.CreateObject("ActivityAdvert", "ActivityAdvert_MGWT_Store_Button");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		language = GetControllerAt(1);
	}
}
