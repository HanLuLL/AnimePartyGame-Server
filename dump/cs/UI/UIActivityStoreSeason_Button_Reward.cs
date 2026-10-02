using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreSeason_Button_Reward : GButton
{
	public Controller status;

	public GButton btn_Item;

	public const string URL = "ui://begz6gfvtaoa39";

	public static UIActivityStoreSeason_Button_Reward CreateInstance()
	{
		return (UIActivityStoreSeason_Button_Reward)UIPackage.CreateObject("ActivityStoreSeason", "ActivityStoreSeason_Button_Reward");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		btn_Item = (GButton)GetChildAt(1);
	}
}
