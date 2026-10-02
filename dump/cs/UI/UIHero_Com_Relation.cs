using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_Relation : GComponent
{
	public Controller rewardNum;

	public Controller showLock;

	public Controller language;

	public Controller releationPowerUp;

	public GButton com_Item_0;

	public GButton com_Item_1;

	public GTextField txt_Level;

	public GProgressBar progress_Relation;

	public Transition Cut_in;

	public const string URL = "ui://7qkd4lqxg1lkq";

	public static UIHero_Com_Relation CreateInstance()
	{
		return (UIHero_Com_Relation)UIPackage.CreateObject("Hero", "Hero_Com_Relation");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		rewardNum = GetControllerAt(0);
		showLock = GetControllerAt(1);
		language = GetControllerAt(2);
		releationPowerUp = GetControllerAt(3);
		com_Item_0 = (GButton)GetChildAt(2);
		com_Item_1 = (GButton)GetChildAt(3);
		txt_Level = (GTextField)GetChildAt(7);
		progress_Relation = (GProgressBar)GetChildAt(8);
		Cut_in = GetTransitionAt(0);
	}
}
