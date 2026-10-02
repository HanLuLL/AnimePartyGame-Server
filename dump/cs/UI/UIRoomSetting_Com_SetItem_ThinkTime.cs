using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomSetting_Com_SetItem_ThinkTime : GComponent
{
	public GTextField txt_Type;

	public GRichTextField txt_Content;

	public GImage image_Arrow;

	public GGraph btn_Click;

	public const string URL = "ui://m6sn3r22n2xac0";

	public static UIRoomSetting_Com_SetItem_ThinkTime CreateInstance()
	{
		return (UIRoomSetting_Com_SetItem_ThinkTime)UIPackage.CreateObject("Common_External", "RoomSetting_Com_SetItem_ThinkTime");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Type = (GTextField)GetChildAt(0);
		txt_Content = (GRichTextField)GetChildAt(2);
		image_Arrow = (GImage)GetChildAt(3);
		btn_Click = (GGraph)GetChildAt(4);
	}
}
