using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_ComboBox_popup : GComponent
{
	public GList list;

	public const string URL = "ui://7qkd4lqxjhclq4l";

	public static UIHero_Com_ComboBox_popup CreateInstance()
	{
		return (UIHero_Com_ComboBox_popup)UIPackage.CreateObject("Hero", "Hero_Com_ComboBox_popup");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list = (GList)GetChildAt(1);
	}
}
