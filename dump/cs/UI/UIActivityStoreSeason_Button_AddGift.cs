using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreSeason_Button_AddGift : GButton
{
	public GLoader loader_Icon;

	public const string URL = "ui://begz6gfv7vwm1c";

	public static UIActivityStoreSeason_Button_AddGift CreateInstance()
	{
		return (UIActivityStoreSeason_Button_AddGift)UIPackage.CreateObject("ActivityStoreSeason", "ActivityStoreSeason_Button_AddGift");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Icon = (GLoader)GetChildAt(0);
	}
}
