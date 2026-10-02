using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_Skill : GComponent
{
	public Controller skillType;

	public GTextField txt_SkillName;

	public GRichTextField txt_SkillDesc;

	public GTextField txt_CD;

	public Transition Cut_in;

	public const string URL = "ui://7qkd4lqxkqgjq2f";

	public static UIHero_Com_Skill CreateInstance()
	{
		return (UIHero_Com_Skill)UIPackage.CreateObject("Hero", "Hero_Com_Skill");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		skillType = GetControllerAt(0);
		txt_SkillName = (GTextField)GetChildAt(1);
		txt_SkillDesc = (GRichTextField)GetChildAt(2);
		txt_CD = (GTextField)GetChildAt(4);
		Cut_in = GetTransitionAt(0);
	}
}
