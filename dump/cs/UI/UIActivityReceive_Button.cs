using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityReceive_Button : GButton
{
	public Controller StatusRe;

	public Controller typeRe;

	public const string URL = "ui://50qspitzize7d";

	public static UIActivityReceive_Button CreateInstance()
	{
		return (UIActivityReceive_Button)UIPackage.CreateObject("ActivityReceive", "ActivityReceive_Button");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		StatusRe = GetControllerAt(1);
		typeRe = GetControllerAt(2);
	}
}
