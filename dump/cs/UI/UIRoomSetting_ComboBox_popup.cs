using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomSetting_ComboBox_popup : GComponent
{
	public GList list;

	public Transition hi;

	public const string URL = "ui://m6sn3r22ucnq9n";

	public static UIRoomSetting_ComboBox_popup CreateInstance()
	{
		return (UIRoomSetting_ComboBox_popup)UIPackage.CreateObject("Common_External", "RoomSetting_ComboBox_popup");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list = (GList)GetChildAt(1);
		hi = GetTransitionAt(0);
	}
}
