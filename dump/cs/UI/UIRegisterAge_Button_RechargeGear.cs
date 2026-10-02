using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRegisterAge_Button_RechargeGear : GButton
{
	public GTextField txt_Gear;

	public GTextField txt_Explain;

	public const string URL = "ui://287pjyadwin51";

	public static UIRegisterAge_Button_RechargeGear CreateInstance()
	{
		return (UIRegisterAge_Button_RechargeGear)UIPackage.CreateObject("RegisterAge", "RegisterAge_Button_RechargeGear");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Gear = (GTextField)GetChildAt(1);
		txt_Explain = (GTextField)GetChildAt(2);
	}
}
