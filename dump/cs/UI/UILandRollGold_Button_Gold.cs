using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandRollGold_Button_Gold : GButton
{
	public GTextField txt_Gold;

	public Transition goldSFX;

	public const string URL = "ui://dfabevk6p4oi6";

	public static UILandRollGold_Button_Gold CreateInstance()
	{
		return (UILandRollGold_Button_Gold)UIPackage.CreateObject("LandRollGold", "LandRollGold_Button_Gold");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Gold = (GTextField)GetChildAt(3);
		goldSFX = GetTransitionAt(0);
	}
}
