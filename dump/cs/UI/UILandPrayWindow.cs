using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandPrayWindow : GComponent
{
	public GLoader loader;

	public GButton btn_Cancel;

	public GButton btn_Sure;

	public const string URL = "ui://b48mmxv7iorl0";

	public static UILandPrayWindow CreateInstance()
	{
		BindAll();
		return (UILandPrayWindow)UIPackage.CreateObject("LandPray", "LandPrayWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://b48mmxv7iorl0", typeof(UILandPrayWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader = (GLoader)GetChildAt(0);
		btn_Cancel = (GButton)GetChildAt(1);
		btn_Sure = (GButton)GetChildAt(2);
	}
}
