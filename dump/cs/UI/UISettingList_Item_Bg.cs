using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISettingList_Item_Bg : GComponent
{
	public Controller SetColor;

	public const string URL = "ui://h88onq8cu0xa6";

	public static UISettingList_Item_Bg CreateInstance()
	{
		return (UISettingList_Item_Bg)UIPackage.CreateObject("SettingList", "SettingList_Item_Bg");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		SetColor = GetControllerAt(0);
	}
}
