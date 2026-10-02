using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomList_PlayerLabel_Loader : GComponent
{
	public Controller type;

	public GLoader loader_Image;

	public GGraph loader_Video;

	public const string URL = "ui://5rtgb50em2t88a";

	public static UIRoomList_PlayerLabel_Loader CreateInstance()
	{
		return (UIRoomList_PlayerLabel_Loader)UIPackage.CreateObject("RoomList", "RoomList_PlayerLabel_Loader");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		loader_Image = (GLoader)GetChildAt(0);
		loader_Video = (GGraph)GetChildAt(1);
	}
}
