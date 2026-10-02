using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_FileUpgrade : GComponent
{
	public Controller operatetype;

	public GList list_LvInfo;

	public UIHero_Button_UpgradeOperate btn_AutoUpgrade;

	public GList list_Prop;

	public UIHero_Button_UpgradeOperate btn_CancelUpgrade;

	public GButton btn_Upgrade;

	public UIHero_Progress_PVEExpNew progress_Exp;

	public GGraph com_MaskText;

	public GGraph com_MaskUPbtn;

	public Transition Cut_in;

	public const string URL = "ui://7qkd4lqxkp9fq37";

	public static UIHero_Com_FileUpgrade CreateInstance()
	{
		return (UIHero_Com_FileUpgrade)UIPackage.CreateObject("Hero", "Hero_Com_FileUpgrade");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		operatetype = GetControllerAt(0);
		list_LvInfo = (GList)GetChildAt(1);
		btn_AutoUpgrade = (UIHero_Button_UpgradeOperate)GetChildAt(4);
		list_Prop = (GList)GetChildAt(5);
		btn_CancelUpgrade = (UIHero_Button_UpgradeOperate)GetChildAt(6);
		btn_Upgrade = (GButton)GetChildAt(7);
		progress_Exp = (UIHero_Progress_PVEExpNew)GetChildAt(8);
		com_MaskText = (GGraph)GetChildAt(9);
		com_MaskUPbtn = (GGraph)GetChildAt(10);
		Cut_in = GetTransitionAt(0);
	}
}
