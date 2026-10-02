using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomList_Com_Difficulty : GComponent
{
	public Controller setColor;

	public GTextField txt_Content;

	public const string URL = "ui://5rtgb50eiorlj97";

	public static UIRoomList_Com_Difficulty CreateInstance()
	{
		return (UIRoomList_Com_Difficulty)UIPackage.CreateObject("RoomList", "RoomList_Com_Difficulty");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		setColor = GetControllerAt(0);
		txt_Content = (GTextField)GetChildAt(1);
	}
}
