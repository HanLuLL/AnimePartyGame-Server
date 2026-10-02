using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Button_UpgradeOperate : GButton
{
	public Controller colorType;

	public const string URL = "ui://7qkd4lqxot0wq2b";

	public static UIHero_Button_UpgradeOperate CreateInstance()
	{
		return (UIHero_Button_UpgradeOperate)UIPackage.CreateObject("Hero", "Hero_Button_UpgradeOperate");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		colorType = GetControllerAt(1);
	}
}
