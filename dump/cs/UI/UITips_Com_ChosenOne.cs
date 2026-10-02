using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITips_Com_ChosenOne : GComponent
{
	public GTextField txt_Title;

	public GTextField txt_GoldNum;

	public Transition Cut_in;

	public const string URL = "ui://1jtcsp8mwz8ns8b";

	public static UITips_Com_ChosenOne CreateInstance()
	{
		return (UITips_Com_ChosenOne)UIPackage.CreateObject("Tips", "Tips_Com_ChosenOne");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Title = (GTextField)GetChildAt(2);
		txt_GoldNum = (GTextField)GetChildAt(4);
		Cut_in = GetTransitionAt(0);
	}
}
