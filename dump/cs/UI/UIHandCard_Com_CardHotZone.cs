using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHandCard_Com_CardHotZone : GComponent
{
	public GGraph hotZone;

	public Transition hotZoneTrans;

	public const string URL = "ui://vflhnh8dorg7c";

	public static UIHandCard_Com_CardHotZone CreateInstance()
	{
		return (UIHandCard_Com_CardHotZone)UIPackage.CreateObject("HandCard", "HandCard_Com_CardHotZone");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		hotZone = (GGraph)GetChildAt(0);
		hotZoneTrans = GetTransitionAt(0);
	}
}
