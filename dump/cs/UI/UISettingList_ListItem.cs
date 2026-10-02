using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISettingList_ListItem : GComponent
{
	public GTextField title;

	public const string URL = "ui://h88onq8cgzvsg";

	public static UISettingList_ListItem CreateInstance()
	{
		return (UISettingList_ListItem)UIPackage.CreateObject("SettingList", "SettingList_ListItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		title = (GTextField)GetChildAt(0);
	}
}
