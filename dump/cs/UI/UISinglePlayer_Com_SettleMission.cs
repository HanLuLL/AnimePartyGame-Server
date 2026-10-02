using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Com_SettleMission : GComponent
{
	public GList list_Card;

	public Transition Cut_in;

	public const string URL = "ui://mi9vm3w0fx3oq71";

	public static UISinglePlayer_Com_SettleMission CreateInstance()
	{
		return (UISinglePlayer_Com_SettleMission)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_Com_SettleMission");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_Card = (GList)GetChildAt(5);
		Cut_in = GetTransitionAt(0);
	}
}
