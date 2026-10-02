using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRookieTaskInfoWindow : GComponent
{
	public GComponent mohu;

	public GLabel bottom;

	public GList list_Item;

	public const string URL = "ui://mrtmoyvhgzhq0";

	public static UIRookieTaskInfoWindow CreateInstance()
	{
		BindAll();
		return (UIRookieTaskInfoWindow)UIPackage.CreateObject("RookieTaskInfo", "RookieTaskInfoWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://mrtmoyvhgzhq0", typeof(UIRookieTaskInfoWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mohu = (GComponent)GetChildAt(0);
		bottom = (GLabel)GetChildAt(1);
		list_Item = (GList)GetChildAt(2);
	}
}
