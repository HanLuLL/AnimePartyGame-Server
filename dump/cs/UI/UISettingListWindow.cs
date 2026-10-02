using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISettingListWindow : GComponent
{
	public Controller showDeveloper;

	public Controller tab;

	public GGraph mohu;

	public UISettingList_Developer developer;

	public GGroup tabList;

	public GList list;

	public const string URL = "ui://h88onq8cd11o0";

	public static UISettingListWindow CreateInstance()
	{
		BindAll();
		return (UISettingListWindow)UIPackage.CreateObject("SettingList", "SettingListWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://h88onq8cd11o0", typeof(UISettingListWindow));
		UIObjectFactory.SetPackageItemExtension("ui://h88onq8cgzvse", typeof(UISettingList_Developer));
		UIObjectFactory.SetPackageItemExtension("ui://h88onq8cgzvsg", typeof(UISettingList_ListItem));
		UIObjectFactory.SetPackageItemExtension("ui://h88onq8cu0xa1", typeof(UISettingList_Com_Bg));
		UIObjectFactory.SetPackageItemExtension("ui://h88onq8cu0xa4", typeof(UISettingList_Com_BottomBg));
		UIObjectFactory.SetPackageItemExtension("ui://h88onq8cu0xa6", typeof(UISettingList_Item_Bg));
		UIObjectFactory.SetPackageItemExtension("ui://h88onq8cu0xa7", typeof(UISettingList_Item_BottomBg));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showDeveloper = GetControllerAt(0);
		tab = GetControllerAt(1);
		mohu = (GGraph)GetChildAt(0);
		developer = (UISettingList_Developer)GetChildAt(3);
		tabList = (GGroup)GetChildAt(11);
		list = (GList)GetChildAt(12);
	}
}
