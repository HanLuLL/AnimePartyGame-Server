using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISettingList_Item_BottomBg : GComponent
{
	public GImage zheZhao;

	public const string URL = "ui://h88onq8cu0xa7";

	public static UISettingList_Item_BottomBg CreateInstance()
	{
		return (UISettingList_Item_BottomBg)UIPackage.CreateObject("SettingList", "SettingList_Item_BottomBg");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		zheZhao = (GImage)GetChildAt(0);
	}
}
