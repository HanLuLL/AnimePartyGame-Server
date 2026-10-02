using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityMGWT_Tab_Button : GButton
{
	public Controller page;

	public Controller ischoose;

	public Controller language;

	public Controller redPoint;

	public const string URL = "ui://wdl8l4hslwm1d";

	public static UIActivityMGWT_Tab_Button CreateInstance()
	{
		return (UIActivityMGWT_Tab_Button)UIPackage.CreateObject("ActivityMGWT", "ActivityMGWT_Tab_Button");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		page = GetControllerAt(0);
		ischoose = GetControllerAt(1);
		language = GetControllerAt(2);
		redPoint = GetControllerAt(3);
	}
}
