using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFight_Button_Dodge : GButton
{
	public Controller available;

	public GTextField txt_Point;

	public const string URL = "ui://8irq146hwvm91g";

	public static UIFight_Button_Dodge CreateInstance()
	{
		return (UIFight_Button_Dodge)UIPackage.CreateObject("Fight", "Fight_Button_Dodge");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		available = GetControllerAt(1);
		txt_Point = (GTextField)GetChildAt(4);
	}
}
