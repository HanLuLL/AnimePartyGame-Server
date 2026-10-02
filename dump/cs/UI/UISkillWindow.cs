using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISkillWindow : GComponent
{
	public GGraph loader_Character;

	public const string URL = "ui://scgxumixfqxk0";

	public static UISkillWindow CreateInstance()
	{
		BindAll();
		return (UISkillWindow)UIPackage.CreateObject("Skill", "SkillWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://scgxumixfqxk0", typeof(UISkillWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Character = (GGraph)GetChildAt(0);
	}
}
