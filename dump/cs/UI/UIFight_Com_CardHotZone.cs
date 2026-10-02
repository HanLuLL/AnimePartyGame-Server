using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFight_Com_CardHotZone : GComponent
{
	public GGraph hotZone;

	public Transition hotZoneTrans;

	public const string URL = "ui://8irq146hmjruj";

	public static UIFight_Com_CardHotZone CreateInstance()
	{
		return (UIFight_Com_CardHotZone)UIPackage.CreateObject("Fight", "Fight_Com_CardHotZone");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		hotZone = (GGraph)GetChildAt(0);
		hotZoneTrans = GetTransitionAt(0);
	}
}
