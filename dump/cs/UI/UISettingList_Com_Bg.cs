using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISettingList_Com_Bg : GComponent
{
	public Controller SetColor;

	public const string URL = "ui://h88onq8cu0xa1";

	public static UISettingList_Com_Bg CreateInstance()
	{
		return (UISettingList_Com_Bg)UIPackage.CreateObject("SettingList", "SettingList_Com_Bg");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		SetColor = GetControllerAt(0);
	}
}
