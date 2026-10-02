using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISettingList_Com_BottomBg : GComponent
{
	public GImage zheZhao;

	public const string URL = "ui://h88onq8cu0xa4";

	public static UISettingList_Com_BottomBg CreateInstance()
	{
		return (UISettingList_Com_BottomBg)UIPackage.CreateObject("SettingList", "SettingList_Com_BottomBg");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		zheZhao = (GImage)GetChildAt(0);
	}
}
