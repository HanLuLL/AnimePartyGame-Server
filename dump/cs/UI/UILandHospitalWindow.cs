using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandHospitalWindow : GComponent
{
	public UILandHospital_Com_Content com_Content;

	public const string URL = "ui://daex1j06psttj";

	public static UILandHospitalWindow CreateInstance()
	{
		BindAll();
		return (UILandHospitalWindow)UIPackage.CreateObject("LandHospital", "LandHospitalWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://daex1j06psttj", typeof(UILandHospitalWindow));
		UIObjectFactory.SetPackageItemExtension("ui://daex1j06rum70", typeof(UILandHospital_Com_Content));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Content = (UILandHospital_Com_Content)GetChildAt(1);
	}
}
