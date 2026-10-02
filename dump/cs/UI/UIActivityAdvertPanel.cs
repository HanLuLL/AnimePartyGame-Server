using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityAdvertPanel : GComponent
{
	public Controller language;

	public GLoader loader;

	public GTextField txt_timeTip;

	public UIActivityAdvert_MGWT_Activity_Button btn_activity;

	public UIActivityAdvert_MGWT_Store_Button btn_buy;

	public Transition Cut_in;

	public const string URL = "ui://wkj47h4ilwm10";

	public static UIActivityAdvertPanel CreateInstance()
	{
		BindAll();
		return (UIActivityAdvertPanel)UIPackage.CreateObject("ActivityAdvert", "ActivityAdvertPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://wkj47h4ilwm10", typeof(UIActivityAdvertPanel));
		UIObjectFactory.SetPackageItemExtension("ui://wkj47h4ilwm1o", typeof(UIActivityAdvert_MGWT_Store_Button));
		UIObjectFactory.SetPackageItemExtension("ui://wkj47h4ilwm1p", typeof(UIActivityAdvert_MGWT_Activity_Button));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		language = GetControllerAt(0);
		loader = (GLoader)GetChildAt(0);
		txt_timeTip = (GTextField)GetChildAt(5);
		btn_activity = (UIActivityAdvert_MGWT_Activity_Button)GetChildAt(7);
		btn_buy = (UIActivityAdvert_MGWT_Store_Button)GetChildAt(8);
		Cut_in = GetTransitionAt(0);
	}
}
