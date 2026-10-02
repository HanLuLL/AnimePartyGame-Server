using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITips_Com_LotteryRound : GComponent
{
	public Transition Cut_in;

	public const string URL = "ui://1jtcsp8mmques86";

	public static UITips_Com_LotteryRound CreateInstance()
	{
		return (UITips_Com_LotteryRound)UIPackage.CreateObject("Tips", "Tips_Com_LotteryRound");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Cut_in = GetTransitionAt(0);
	}
}
