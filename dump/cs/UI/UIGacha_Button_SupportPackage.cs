using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGacha_Button_SupportPackage : GButton
{
	public GLoader loader_Icon;

	public const string URL = "ui://j90wpcmni82zqq37";

	public static UIGacha_Button_SupportPackage CreateInstance()
	{
		return (UIGacha_Button_SupportPackage)UIPackage.CreateObject("Gacha", "Gacha_Button_SupportPackage");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Icon = (GLoader)GetChildAt(0);
	}
}
