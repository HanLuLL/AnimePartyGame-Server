using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIInfoWindow : GComponent
{
	public GGraph mohu;

	public GTextField txt_Title;

	public GLabel com_Content;

	public GButton btn_Sure;

	public Transition Cut_in;

	public const string URL = "ui://zhzrfum9dtry7";

	public static UIInfoWindow CreateInstance()
	{
		BindAll();
		return (UIInfoWindow)UIPackage.CreateObject("Info", "InfoWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://zhzrfum9dtry4", typeof(UIInfo_Button_Grip));
		UIObjectFactory.SetPackageItemExtension("ui://zhzrfum9dtry7", typeof(UIInfoWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mohu = (GGraph)GetChildAt(0);
		txt_Title = (GTextField)GetChildAt(3);
		com_Content = (GLabel)GetChildAt(4);
		btn_Sure = (GButton)GetChildAt(5);
		Cut_in = GetTransitionAt(0);
	}
}
