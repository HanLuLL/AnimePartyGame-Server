using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_PveLvDesc : GComponent
{
	public Controller status;

	public Controller LvSDi;

	public GTextField txt_LvDesc;

	public GTextField txt_level;

	public GGroup group_root;

	public Transition Cut_in;

	public const string URL = "ui://7qkd4lqxot0wq29";

	public static UIHero_Com_PveLvDesc CreateInstance()
	{
		return (UIHero_Com_PveLvDesc)UIPackage.CreateObject("Hero", "Hero_Com_PveLvDesc");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		LvSDi = GetControllerAt(1);
		txt_LvDesc = (GTextField)GetChildAt(2);
		txt_level = (GTextField)GetChildAt(5);
		group_root = (GGroup)GetChildAt(7);
		Cut_in = GetTransitionAt(0);
	}
}
