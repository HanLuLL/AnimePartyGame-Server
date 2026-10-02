using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomList_Com_ThinkTime : GComponent
{
	public GTextField txt_TimePlan;

	public const string URL = "ui://5rtgb50eiorlj94";

	public static UIRoomList_Com_ThinkTime CreateInstance()
	{
		return (UIRoomList_Com_ThinkTime)UIPackage.CreateObject("RoomList", "RoomList_Com_ThinkTime");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_TimePlan = (GTextField)GetChildAt(1);
	}
}
