using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreSeason_Button_Gacha : GButton
{
	public GTextField txt_Name;

	public GLoader loader_Icon;

	public GTextField txt_Count;

	public const string URL = "ui://begz6gfv7vwm19";

	public static UIActivityStoreSeason_Button_Gacha CreateInstance()
	{
		return (UIActivityStoreSeason_Button_Gacha)UIPackage.CreateObject("ActivityStoreSeason", "ActivityStoreSeason_Button_Gacha");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Name = (GTextField)GetChildAt(1);
		loader_Icon = (GLoader)GetChildAt(2);
		txt_Count = (GTextField)GetChildAt(3);
	}
}
