using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISettingList_Developer : GComponent
{
	public GTextField title;

	public const string URL = "ui://h88onq8cgzvse";

	public static UISettingList_Developer CreateInstance()
	{
		return (UISettingList_Developer)UIPackage.CreateObject("SettingList", "SettingList_Developer");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		title = (GTextField)GetChildAt(0);
	}
}
