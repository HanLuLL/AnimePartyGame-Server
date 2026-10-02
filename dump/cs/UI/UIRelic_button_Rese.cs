using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRelic_button_Rese : GButton
{
	public GTextField txt_Count;

	public const string URL = "ui://kjm6oxy2o8128";

	public static UIRelic_button_Rese CreateInstance()
	{
		return (UIRelic_button_Rese)UIPackage.CreateObject("Relic", "Relic_button_Rese");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Count = (GTextField)GetChildAt(2);
	}
}
