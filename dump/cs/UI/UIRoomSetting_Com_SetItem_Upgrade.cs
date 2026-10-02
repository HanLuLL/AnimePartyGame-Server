using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomSetting_Com_SetItem_Upgrade : GComponent
{
	public GTextField txt_Type;

	public GTextField txt_Content;

	public GImage image_Arrow;

	public GGraph btn_Click;

	public const string URL = "ui://m6sn3r22n2xabz";

	public static UIRoomSetting_Com_SetItem_Upgrade CreateInstance()
	{
		return (UIRoomSetting_Com_SetItem_Upgrade)UIPackage.CreateObject("Common_External", "RoomSetting_Com_SetItem_Upgrade");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Type = (GTextField)GetChildAt(0);
		txt_Content = (GTextField)GetChildAt(2);
		image_Arrow = (GImage)GetChildAt(4);
		btn_Click = (GGraph)GetChildAt(5);
	}
}
