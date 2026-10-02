using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Btn_Sort : GButton
{
	public Controller sort;

	public const string URL = "ui://7qkd4lqxjhclq4n";

	public static UIHero_Btn_Sort CreateInstance()
	{
		return (UIHero_Btn_Sort)UIPackage.CreateObject("Hero", "Hero_Btn_Sort");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		sort = GetControllerAt(1);
	}
}
