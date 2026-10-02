using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHeiSeTiaoTiao : GComponent
{
	public Transition Loop;

	public const string URL = "ui://aepd0gr2jxlu16";

	public static UIHeiSeTiaoTiao CreateInstance()
	{
		return (UIHeiSeTiaoTiao)UIPackage.CreateObject("MatchSuccess", "黑色条条");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Loop = GetTransitionAt(0);
	}
}
