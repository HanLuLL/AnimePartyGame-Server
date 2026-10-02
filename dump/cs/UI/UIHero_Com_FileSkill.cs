using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_FileSkill : GComponent
{
	public Controller Talent;

	public UIHero_Button_SwitchMode btn_Switch;

	public GList list_Skills;

	public GTextField txt_ATK;

	public GTextField txt_DEF;

	public GTextField txt_HP;

	public UIHero_Com_Level com_Level;

	public Transition Cut_in;

	public const string URL = "ui://7qkd4lqxkp9fq36";

	public static UIHero_Com_FileSkill CreateInstance()
	{
		return (UIHero_Com_FileSkill)UIPackage.CreateObject("Hero", "Hero_Com_FileSkill");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Talent = GetControllerAt(0);
		btn_Switch = (UIHero_Button_SwitchMode)GetChildAt(1);
		list_Skills = (GList)GetChildAt(2);
		txt_ATK = (GTextField)GetChildAt(4);
		txt_DEF = (GTextField)GetChildAt(6);
		txt_HP = (GTextField)GetChildAt(8);
		com_Level = (UIHero_Com_Level)GetChildAt(10);
		Cut_in = GetTransitionAt(0);
	}
}
