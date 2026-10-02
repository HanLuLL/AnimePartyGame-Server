using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandPursuitWindow : GComponent
{
	public Controller dialogType;

	public Controller Hide;

	public GTextField txt_HideTip;

	public GButton btn_Stay;

	public GButton btn_Pursuit;

	public GList list_Players;

	public Transition cut_in;

	public const string URL = "ui://dkvy8z1orum70";

	public static UILandPursuitWindow CreateInstance()
	{
		BindAll();
		return (UILandPursuitWindow)UIPackage.CreateObject("LandPursuit", "LandPursuitWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://dkvy8z1orum70", typeof(UILandPursuitWindow));
		UIObjectFactory.SetPackageItemExtension("ui://dkvy8z1orum73", typeof(UILandPursuit_Button_RoleHeadshot));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		dialogType = GetControllerAt(0);
		Hide = GetControllerAt(1);
		txt_HideTip = (GTextField)GetChildAt(0);
		btn_Stay = (GButton)GetChildAt(5);
		btn_Pursuit = (GButton)GetChildAt(6);
		list_Players = (GList)GetChildAt(8);
		cut_in = GetTransitionAt(0);
	}
}
