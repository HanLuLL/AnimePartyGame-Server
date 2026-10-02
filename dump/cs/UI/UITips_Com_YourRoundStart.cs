using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITips_Com_YourRoundStart : GComponent
{
	public Controller Player;

	public GTextField txt_Content;

	public Transition Show;

	public const string URL = "ui://1jtcsp8mou6os82";

	public static UITips_Com_YourRoundStart CreateInstance()
	{
		return (UITips_Com_YourRoundStart)UIPackage.CreateObject("Tips", "Tips_Com_YourRoundStart");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Player = GetControllerAt(0);
		txt_Content = (GTextField)GetChildAt(1);
		Show = GetTransitionAt(0);
	}
}
