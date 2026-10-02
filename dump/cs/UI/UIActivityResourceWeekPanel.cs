using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityResourceWeekPanel : GComponent
{
	public UIActivityResourceWeek_Com com_panel;

	public const string URL = "ui://rel5h9izuytm0";

	public static UIActivityResourceWeekPanel CreateInstance()
	{
		BindAll();
		return (UIActivityResourceWeekPanel)UIPackage.CreateObject("ActivityResourceWeek", "ActivityResourceWeekPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://rel5h9izuytm0", typeof(UIActivityResourceWeekPanel));
		UIObjectFactory.SetPackageItemExtension("ui://rel5h9izuytm1", typeof(UIActivityResourceWeek_Com));
		UIObjectFactory.SetPackageItemExtension("ui://rel5h9izuytm9", typeof(UIActivityResourceWeek_Button_Toggle));
		UIObjectFactory.SetPackageItemExtension("ui://rel5h9izuytma", typeof(UIActivityResourceWeek_Com_Label));
		UIObjectFactory.SetPackageItemExtension("ui://rel5h9izuytmd", typeof(UIActivityResourceWeek_Button_TaskStatus));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_panel = (UIActivityResourceWeek_Com)GetChildAt(0);
	}
}
