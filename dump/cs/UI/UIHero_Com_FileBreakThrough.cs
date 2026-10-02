using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_FileBreakThrough : GComponent
{
	public Controller talentStatus;

	public Controller ConditionsMet;

	public Controller language;

	public GTextField txt_DescTitle;

	public UIHero_Com_FileBreakThroughDesc com_Desc;

	public GTextField txt_Tips;

	public GList list_talentMaterials;

	public GButton btn_talentBreak;

	public Transition Cut_in;

	public Transition Finish;

	public const string URL = "ui://7qkd4lqxkp9fq39";

	public static UIHero_Com_FileBreakThrough CreateInstance()
	{
		return (UIHero_Com_FileBreakThrough)UIPackage.CreateObject("Hero", "Hero_Com_FileBreakThrough");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		talentStatus = GetControllerAt(0);
		ConditionsMet = GetControllerAt(1);
		language = GetControllerAt(2);
		txt_DescTitle = (GTextField)GetChildAt(2);
		com_Desc = (UIHero_Com_FileBreakThroughDesc)GetChildAt(3);
		txt_Tips = (GTextField)GetChildAt(5);
		list_talentMaterials = (GList)GetChildAt(7);
		btn_talentBreak = (GButton)GetChildAt(8);
		Cut_in = GetTransitionAt(0);
		Finish = GetTransitionAt(1);
	}
}
