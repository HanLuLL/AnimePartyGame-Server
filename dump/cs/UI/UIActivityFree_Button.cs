using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityFree_Button : GButton
{
	public Controller StatusRe;

	public const string URL = "ui://tnvzc173esyu4";

	public static UIActivityFree_Button CreateInstance()
	{
		return (UIActivityFree_Button)UIPackage.CreateObject("ActivityFree", "ActivityFree_Button");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		StatusRe = GetControllerAt(1);
	}
}
