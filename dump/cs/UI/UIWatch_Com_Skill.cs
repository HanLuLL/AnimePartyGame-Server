using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIWatch_Com_Skill : GComponent
{
	public Controller skillType;

	public GTextField txt_SkillName;

	public GRichTextField txt_SkillDesc;

	public const string URL = "ui://sv6gwhbekqgjn";

	public static UIWatch_Com_Skill CreateInstance()
	{
		return (UIWatch_Com_Skill)UIPackage.CreateObject("Watch", "Watch_Com_Skill");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		skillType = GetControllerAt(0);
		txt_SkillName = (GTextField)GetChildAt(0);
		txt_SkillDesc = (GRichTextField)GetChildAt(1);
	}
}
