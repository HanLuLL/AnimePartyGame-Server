using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAccountInfoo_Com_PlayerLevel : GComponent
{
	public Controller avtiveLevel;

	public Controller slot;

	public GGraph graph_Effect;

	public const string URL = "ui://iepldke7zhztk";

	public static UIAccountInfoo_Com_PlayerLevel CreateInstance()
	{
		return (UIAccountInfoo_Com_PlayerLevel)UIPackage.CreateObject("AccountInfo", "AccountInfoo_Com_PlayerLevel");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		avtiveLevel = GetControllerAt(0);
		slot = GetControllerAt(1);
		graph_Effect = (GGraph)GetChildAt(5);
	}
}
