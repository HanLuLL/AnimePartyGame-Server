using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBackgroundPanel : GComponent
{
	public Controller hide;

	public GGraph loader_BG;

	public GLoader loader_Line;

	public GLoader loader_SystemBG;

	public const string URL = "ui://ud5q04r1j6xj3r";

	public static UIBackgroundPanel CreateInstance()
	{
		BindAll();
		return (UIBackgroundPanel)UIPackage.CreateObject("Background", "BackgroundPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://ud5q04r1j6xj3r", typeof(UIBackgroundPanel));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		hide = GetControllerAt(0);
		loader_BG = (GGraph)GetChildAt(1);
		loader_Line = (GLoader)GetChildAt(2);
		loader_SystemBG = (GLoader)GetChildAt(3);
	}
}
