using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityComeback_Hero_Button : GButton
{
	public GLoader hero_loader;

	public GTextField hero_title;

	public GTextField hero_name;

	public const string URL = "ui://hconmwfcy9qnx";

	public static UIActivityComeback_Hero_Button CreateInstance()
	{
		return (UIActivityComeback_Hero_Button)UIPackage.CreateObject("ActivityComeback", "ActivityComeback_Hero_Button");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		hero_loader = (GLoader)GetChildAt(1);
		hero_title = (GTextField)GetChildAt(3);
		hero_name = (GTextField)GetChildAt(4);
	}
}
