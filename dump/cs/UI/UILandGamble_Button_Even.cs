using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandGamble_Button_Even : GButton
{
	public GTextField gold;

	public const string URL = "ui://d1prfn9srum77";

	public static UILandGamble_Button_Even CreateInstance()
	{
		return (UILandGamble_Button_Even)UIPackage.CreateObject("LandGamble", "LandGamble_Button_Even");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		gold = (GTextField)GetChildAt(3);
	}
}
