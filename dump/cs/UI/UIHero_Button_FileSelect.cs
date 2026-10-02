using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Button_FileSelect : GButton
{
	public Controller tab;

	public GLoader loader_icon;

	public const string URL = "ui://7qkd4lqxkp9fq38";

	public static UIHero_Button_FileSelect CreateInstance()
	{
		return (UIHero_Button_FileSelect)UIPackage.CreateObject("Hero", "Hero_Button_FileSelect");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tab = GetControllerAt(1);
		loader_icon = (GLoader)GetChildAt(3);
	}
}
