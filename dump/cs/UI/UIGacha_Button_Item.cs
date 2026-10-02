using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGacha_Button_Item : GButton
{
	public GLoader icon_loader;

	public GTextField num;

	public const string URL = "ui://j90wpcmni82zqq3c";

	public static UIGacha_Button_Item CreateInstance()
	{
		return (UIGacha_Button_Item)UIPackage.CreateObject("Gacha", "Gacha_Button_Item");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		icon_loader = (GLoader)GetChildAt(0);
		num = (GTextField)GetChildAt(1);
	}
}
