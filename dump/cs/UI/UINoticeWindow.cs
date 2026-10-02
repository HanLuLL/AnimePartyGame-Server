using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINoticeWindow : GComponent
{
	public GGraph mohu;

	public GList list_Tab;

	public GList list_Content;

	public GButton btn_Notice;

	public const string URL = "ui://bwvbo0x0dx3g7";

	public static UINoticeWindow CreateInstance()
	{
		BindAll();
		return (UINoticeWindow)UIPackage.CreateObject("Notice", "NoticeWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://bwvbo0x0dx3g3", typeof(UINotice_Com_NoticeImage));
		UIObjectFactory.SetPackageItemExtension("ui://bwvbo0x0dx3g4", typeof(UINotice_Com_NoticeText));
		UIObjectFactory.SetPackageItemExtension("ui://bwvbo0x0dx3g7", typeof(UINoticeWindow));
		UIObjectFactory.SetPackageItemExtension("ui://bwvbo0x0gd4da", typeof(UINotice_Button_Grip));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mohu = (GGraph)GetChildAt(0);
		list_Tab = (GList)GetChildAt(4);
		list_Content = (GList)GetChildAt(5);
		btn_Notice = (GButton)GetChildAt(6);
	}
}
