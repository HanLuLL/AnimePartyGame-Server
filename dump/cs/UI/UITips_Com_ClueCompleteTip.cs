using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITips_Com_ClueCompleteTip : GComponent
{
	public Controller fact;

	public Controller Language;

	public GTextField txt_Title;

	public GGroup cn;

	public GGroup jpAndEn;

	public GGroup en;

	public GGroup cn_red;

	public GGroup en_red;

	public GGroup jpAndEn_red;

	public Transition Cut_in;

	public Transition Clue_Cut_in;

	public Transition ZXJL_Cut_in;

	public const string URL = "ui://1jtcsp8mmgj002";

	public static UITips_Com_ClueCompleteTip CreateInstance()
	{
		return (UITips_Com_ClueCompleteTip)UIPackage.CreateObject("Tips", "Tips_Com_ClueCompleteTip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		fact = GetControllerAt(0);
		Language = GetControllerAt(1);
		txt_Title = (GTextField)GetChildAt(1);
		cn = (GGroup)GetChildAt(14);
		jpAndEn = (GGroup)GetChildAt(18);
		en = (GGroup)GetChildAt(22);
		cn_red = (GGroup)GetChildAt(28);
		en_red = (GGroup)GetChildAt(34);
		jpAndEn_red = (GGroup)GetChildAt(36);
		Cut_in = GetTransitionAt(0);
		Clue_Cut_in = GetTransitionAt(1);
		ZXJL_Cut_in = GetTransitionAt(2);
	}
}
