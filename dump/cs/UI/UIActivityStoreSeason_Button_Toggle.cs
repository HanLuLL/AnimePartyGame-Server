using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreSeason_Button_Toggle : GButton
{
	public GGraph di_0;

	public GImage di_1;

	public const string URL = "ui://begz6gfv7vwmp";

	public static UIActivityStoreSeason_Button_Toggle CreateInstance()
	{
		return (UIActivityStoreSeason_Button_Toggle)UIPackage.CreateObject("ActivityStoreSeason", "ActivityStoreSeason_Button_Toggle");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		di_0 = (GGraph)GetChildAt(0);
		di_1 = (GImage)GetChildAt(1);
	}
}
