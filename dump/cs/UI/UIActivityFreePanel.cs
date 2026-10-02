using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityFreePanel : GComponent
{
	public Controller language;

	public Controller StatusRe;

	public GLoader FreeBackGround;

	public GTextField RebateFree_Time;

	public UIActivityFree_Button Receive_btn;

	public GButton Item_Btn;

	public Transition Cutin;

	public const string URL = "ui://tnvzc173esyu0";

	public static UIActivityFreePanel CreateInstance()
	{
		BindAll();
		return (UIActivityFreePanel)UIPackage.CreateObject("ActivityFree", "ActivityFreePanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://tnvzc173esyu0", typeof(UIActivityFreePanel));
		UIObjectFactory.SetPackageItemExtension("ui://tnvzc173esyu4", typeof(UIActivityFree_Button));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		language = GetControllerAt(0);
		StatusRe = GetControllerAt(1);
		FreeBackGround = (GLoader)GetChildAt(0);
		RebateFree_Time = (GTextField)GetChildAt(2);
		Receive_btn = (UIActivityFree_Button)GetChildAt(4);
		Item_Btn = (GButton)GetChildAt(7);
		Cutin = GetTransitionAt(0);
	}
}
