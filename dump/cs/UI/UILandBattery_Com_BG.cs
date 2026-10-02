using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandBattery_Com_BG : GComponent
{
	public Controller dialogType;

	public GButton btn_Leave;

	public GButton btn_SureTarget;

	public Transition t0;

	public const string URL = "ui://dc8zac77hmjfi";

	public static UILandBattery_Com_BG CreateInstance()
	{
		return (UILandBattery_Com_BG)UIPackage.CreateObject("LandBattery", "LandBattery_Com_BG");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		dialogType = GetControllerAt(0);
		btn_Leave = (GButton)GetChildAt(4);
		btn_SureTarget = (GButton)GetChildAt(5);
		t0 = GetTransitionAt(0);
	}
}
