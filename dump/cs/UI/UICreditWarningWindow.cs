using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICreditWarningWindow : GComponent
{
	public Controller state;

	public GRichTextField txt_desc;

	public GTextField txt_time;

	public GButton btn_ok;

	public GTextField txt_title;

	public GRichTextField txt_desc_2;

	public const string URL = "ui://trgir7dot1xw0";

	public static UICreditWarningWindow CreateInstance()
	{
		BindAll();
		return (UICreditWarningWindow)UIPackage.CreateObject("CreditWarning", "CreditWarningWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://trgir7dot1xw0", typeof(UICreditWarningWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(0);
		txt_desc = (GRichTextField)GetChildAt(1);
		txt_time = (GTextField)GetChildAt(2);
		btn_ok = (GButton)GetChildAt(3);
		txt_title = (GTextField)GetChildAt(4);
		txt_desc_2 = (GRichTextField)GetChildAt(5);
	}
}
