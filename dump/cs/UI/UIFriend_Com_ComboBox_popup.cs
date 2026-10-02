using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFriend_Com_ComboBox_popup : GComponent
{
	public GList list;

	public const string URL = "ui://2wec2q8znzp81y";

	public static UIFriend_Com_ComboBox_popup CreateInstance()
	{
		return (UIFriend_Com_ComboBox_popup)UIPackage.CreateObject("Friend", "Friend_Com_ComboBox_popup");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list = (GList)GetChildAt(1);
	}
}
