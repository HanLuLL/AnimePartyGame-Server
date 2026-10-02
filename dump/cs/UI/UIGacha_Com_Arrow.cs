using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGacha_Com_Arrow : GComponent
{
	public GTextField txt_progressValue;

	public Transition ArrowLoop;

	public Transition Cut_in;

	public const string URL = "ui://j90wpcmnw6jfqq2t";

	public static UIGacha_Com_Arrow CreateInstance()
	{
		return (UIGacha_Com_Arrow)UIPackage.CreateObject("Gacha", "Gacha_Com_Arrow");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_progressValue = (GTextField)GetChildAt(1);
		ArrowLoop = GetTransitionAt(0);
		Cut_in = GetTransitionAt(1);
	}
}
