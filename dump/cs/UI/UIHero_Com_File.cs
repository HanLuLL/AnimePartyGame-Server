using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_File : GComponent
{
	public Controller tab;

	public Controller hasTalent;

	public GTextField txt_CharaterName;

	public GTextField txt_CharaterNick;

	public UIHero_Button_FileSelect btn_Skill;

	public UIHero_Button_FileSelect btn_Upgrade;

	public UIHero_Button_FileSelect btn_Talent;

	public UIHero_Button_FileSelect btn_Archive;

	public UIHero_Com_FileSkill com_FileSkill;

	public UIHero_Com_FileUpgrade com_FileUpgrade;

	public UIHero_Com_FileBreakThrough com_FileBreakThough;

	public UIHero_Com_FileArchive com_InfoArchive;

	public GGraph com_MaskMaterial;

	public GButton btn_Collect;

	public GTextField txt_CollectTip;

	public Transition Open;

	public Transition Upgrade_Cut_in;

	public Transition Skill_Cut_in;

	public Transition Info_Cut_in;

	public Transition Potential_Cut_in;

	public const string URL = "ui://7qkd4lqxg1lkm";

	public static UIHero_Com_File CreateInstance()
	{
		return (UIHero_Com_File)UIPackage.CreateObject("Hero", "Hero_Com_File");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tab = GetControllerAt(0);
		hasTalent = GetControllerAt(1);
		txt_CharaterName = (GTextField)GetChildAt(0);
		txt_CharaterNick = (GTextField)GetChildAt(1);
		btn_Skill = (UIHero_Button_FileSelect)GetChildAt(3);
		btn_Upgrade = (UIHero_Button_FileSelect)GetChildAt(4);
		btn_Talent = (UIHero_Button_FileSelect)GetChildAt(5);
		btn_Archive = (UIHero_Button_FileSelect)GetChildAt(6);
		com_FileSkill = (UIHero_Com_FileSkill)GetChildAt(7);
		com_FileUpgrade = (UIHero_Com_FileUpgrade)GetChildAt(8);
		com_FileBreakThough = (UIHero_Com_FileBreakThrough)GetChildAt(9);
		com_InfoArchive = (UIHero_Com_FileArchive)GetChildAt(10);
		com_MaskMaterial = (GGraph)GetChildAt(11);
		btn_Collect = (GButton)GetChildAt(13);
		txt_CollectTip = (GTextField)GetChildAt(14);
		Open = GetTransitionAt(0);
		Upgrade_Cut_in = GetTransitionAt(1);
		Skill_Cut_in = GetTransitionAt(2);
		Info_Cut_in = GetTransitionAt(3);
		Potential_Cut_in = GetTransitionAt(4);
	}
}
