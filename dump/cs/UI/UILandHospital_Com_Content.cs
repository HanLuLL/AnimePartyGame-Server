using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandHospital_Com_Content : GComponent
{
	public Controller viewType;

	public GButton btn_noSick;

	public GButton btn_check;

	public Transition checking;

	public Transition checkIn;

	public Transition checkOut;

	public Transition Cut_in;

	public const string URL = "ui://daex1j06rum70";

	public static UILandHospital_Com_Content CreateInstance()
	{
		return (UILandHospital_Com_Content)UIPackage.CreateObject("LandHospital", "LandHospital_Com_Content");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		viewType = GetControllerAt(0);
		btn_noSick = (GButton)GetChildAt(7);
		btn_check = (GButton)GetChildAt(8);
		checking = GetTransitionAt(0);
		checkIn = GetTransitionAt(1);
		checkOut = GetTransitionAt(2);
		Cut_in = GetTransitionAt(3);
	}
}
