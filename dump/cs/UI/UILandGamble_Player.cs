using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandGamble_Player : GComponent
{
	public Controller c1;

	public GGraph animation;

	public GTextField addGold;

	public const string URL = "ui://d1prfn9srum7b";

	public static UILandGamble_Player CreateInstance()
	{
		return (UILandGamble_Player)UIPackage.CreateObject("LandGamble", "LandGamble_Player");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		c1 = GetControllerAt(0);
		animation = (GGraph)GetChildAt(0);
		addGold = (GTextField)GetChildAt(3);
	}
}
