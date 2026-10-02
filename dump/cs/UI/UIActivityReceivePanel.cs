using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityReceivePanel : GComponent
{
	public GLoader ReceiveBackGround;

	public GLoader Load_Hero;

	public GTextField tile_txt;

	public GTextField Rebate_Time;

	public GTextField Condition_txt;

	public UIActivityReceive_Button Go_btn;

	public GButton pre_btn;

	public Transition Cutin;

	public const string URL = "ui://50qspitzize70";

	public static UIActivityReceivePanel CreateInstance()
	{
		BindAll();
		return (UIActivityReceivePanel)UIPackage.CreateObject("ActivityReceive", "ActivityReceivePanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://50qspitzize70", typeof(UIActivityReceivePanel));
		UIObjectFactory.SetPackageItemExtension("ui://50qspitzize7d", typeof(UIActivityReceive_Button));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		ReceiveBackGround = (GLoader)GetChildAt(0);
		Load_Hero = (GLoader)GetChildAt(2);
		tile_txt = (GTextField)GetChildAt(3);
		Rebate_Time = (GTextField)GetChildAt(5);
		Condition_txt = (GTextField)GetChildAt(6);
		Go_btn = (UIActivityReceive_Button)GetChildAt(7);
		pre_btn = (GButton)GetChildAt(9);
		Cutin = GetTransitionAt(0);
	}
}
