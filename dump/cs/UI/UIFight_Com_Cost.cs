using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFight_Com_Cost : GComponent
{
	public GTextField txt_Cost;

	public GList list_Cost;

	public const string URL = "ui://8irq146hglhu4j";

	public static UIFight_Com_Cost CreateInstance()
	{
		return (UIFight_Com_Cost)UIPackage.CreateObject("Fight", "Fight_Com_Cost");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Cost = (GTextField)GetChildAt(1);
		list_Cost = (GList)GetChildAt(2);
	}
}
