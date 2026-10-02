using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBG2 : GComponent
{
	public Transition Cut_in;

	public const string URL = "ui://aepd0gr2jxlu18";

	public static UIBG2 CreateInstance()
	{
		return (UIBG2)UIPackage.CreateObject("MatchSuccess", "BG2");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Cut_in = GetTransitionAt(0);
	}
}
