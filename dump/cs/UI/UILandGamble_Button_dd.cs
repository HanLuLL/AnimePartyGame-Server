using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandGamble_Button_dd : GButton
{
	public GTextField gold;

	public const string URL = "ui://d1prfn9srum73";

	public static UILandGamble_Button_dd CreateInstance()
	{
		return (UILandGamble_Button_dd)UIPackage.CreateObject("LandGamble", "LandGamble_Button_dd");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		gold = (GTextField)GetChildAt(3);
	}
}
