using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomList_Com_Speed : GComponent
{
	public GTextField txt_Speed;

	public const string URL = "ui://5rtgb50eiorlj95";

	public static UIRoomList_Com_Speed CreateInstance()
	{
		return (UIRoomList_Com_Speed)UIPackage.CreateObject("RoomList", "RoomList_Com_Speed");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Speed = (GTextField)GetChildAt(2);
	}
}
