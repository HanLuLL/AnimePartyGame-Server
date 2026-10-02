using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITips_Com_RoundReward : GComponent
{
	public GTextField txt_RoundRewardTitle;

	public GTextField txt_GoldNum;

	public GTextField txt_CardNum;

	public Transition Cut_in;

	public const string URL = "ui://1jtcsp8mine4s7q";

	public static UITips_Com_RoundReward CreateInstance()
	{
		return (UITips_Com_RoundReward)UIPackage.CreateObject("Tips", "Tips_Com_RoundReward");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_RoundRewardTitle = (GTextField)GetChildAt(1);
		txt_GoldNum = (GTextField)GetChildAt(2);
		txt_CardNum = (GTextField)GetChildAt(3);
		Cut_in = GetTransitionAt(0);
	}
}
