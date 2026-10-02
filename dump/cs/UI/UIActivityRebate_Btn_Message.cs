using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityRebate_Btn_Message : GButton
{
	public GTextField Text;

	public const string URL = "ui://psydn4inrr35k";

	public static UIActivityRebate_Btn_Message CreateInstance()
	{
		return (UIActivityRebate_Btn_Message)UIPackage.CreateObject("ActivityRebate", "ActivityRebate_Btn_Message");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Text = (GTextField)GetChildAt(2);
	}
}
