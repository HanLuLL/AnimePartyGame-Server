using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomSetting_Com_SetItem : GComponent
{
	public GTextField txt_Type;

	public GRichTextField txt_Content;

	public GImage image_Arrow;

	public GGraph btn_Click;

	public UICom_MapTag com_MapTag;

	public GLoader btn_Explain;

	public const string URL = "ui://m6sn3r22n2xaby";

	public static UIRoomSetting_Com_SetItem CreateInstance()
	{
		return (UIRoomSetting_Com_SetItem)UIPackage.CreateObject("Common_External", "RoomSetting_Com_SetItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Type = (GTextField)GetChildAt(0);
		txt_Content = (GRichTextField)GetChildAt(2);
		image_Arrow = (GImage)GetChildAt(3);
		btn_Click = (GGraph)GetChildAt(4);
		com_MapTag = (UICom_MapTag)GetChildAt(5);
		btn_Explain = (GLoader)GetChildAt(6);
	}
}
