using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomList_Button_GameMode : GButton
{
	public Controller GameMode;

	public GLoader loader_Icon_Up;

	public GLoader loader_Icon_Down;

	public GTextField txt_Title;

	public GTextField txt_Explain;

	public const string URL = "ui://5rtgb50eglhuj90";

	public static UIRoomList_Button_GameMode CreateInstance()
	{
		return (UIRoomList_Button_GameMode)UIPackage.CreateObject("RoomList", "RoomList_Button_GameMode");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		GameMode = GetControllerAt(1);
		loader_Icon_Up = (GLoader)GetChildAt(0);
		loader_Icon_Down = (GLoader)GetChildAt(1);
		txt_Title = (GTextField)GetChildAt(4);
		txt_Explain = (GTextField)GetChildAt(5);
	}
}
