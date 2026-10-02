using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRegisterAgeWindow : GComponent
{
	public GGraph mohu;

	public GButton btn_Close;

	public GTextField txt_Title;

	public GTextField txt_Explain;

	public GList list_Gear;

	public GButton btn_Cancel;

	public GButton btn_Sure;

	public const string URL = "ui://287pjyadwin50";

	public static UIRegisterAgeWindow CreateInstance()
	{
		BindAll();
		return (UIRegisterAgeWindow)UIPackage.CreateObject("RegisterAge", "RegisterAgeWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://287pjyadwin50", typeof(UIRegisterAgeWindow));
		UIObjectFactory.SetPackageItemExtension("ui://287pjyadwin51", typeof(UIRegisterAge_Button_RechargeGear));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mohu = (GGraph)GetChildAt(0);
		btn_Close = (GButton)GetChildAt(2);
		txt_Title = (GTextField)GetChildAt(3);
		txt_Explain = (GTextField)GetChildAt(4);
		list_Gear = (GList)GetChildAt(5);
		btn_Cancel = (GButton)GetChildAt(6);
		btn_Sure = (GButton)GetChildAt(7);
	}
}
