using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityHubPanel : GComponent
{
	public UIActivityHub_Com_Tab com_ActivityMenu;

	public GButton btn_Return;

	public Transition Cut_in;

	public const string URL = "ui://h82y3ndthe6d0";

	public static UIActivityHubPanel CreateInstance()
	{
		BindAll();
		return (UIActivityHubPanel)UIPackage.CreateObject("ActivityHub", "ActivityHubPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://h82y3ndtb7mb8", typeof(UIActivityHub_Com_SwitchTab));
		UIObjectFactory.SetPackageItemExtension("ui://h82y3ndthe6d0", typeof(UIActivityHubPanel));
		UIObjectFactory.SetPackageItemExtension("ui://h82y3ndthe6d1", typeof(UIActivityHub_Com_Tab));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_ActivityMenu = (UIActivityHub_Com_Tab)GetChildAt(0);
		btn_Return = (GButton)GetChildAt(1);
		Cut_in = GetTransitionAt(0);
	}
}
