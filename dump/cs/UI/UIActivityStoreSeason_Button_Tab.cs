using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreSeason_Button_Tab : GButton
{
	public Controller redStatus;

	public const string URL = "ui://begz6gfvok4t21";

	public static UIActivityStoreSeason_Button_Tab CreateInstance()
	{
		return (UIActivityStoreSeason_Button_Tab)UIPackage.CreateObject("ActivityStoreSeason", "ActivityStoreSeason_Button_Tab");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redStatus = GetControllerAt(1);
	}
}
