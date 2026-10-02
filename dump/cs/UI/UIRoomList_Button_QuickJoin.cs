using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomList_Button_QuickJoin : GComponent
{
	public Controller GameMode;

	public GButton btn_SelectDifficulty;

	public GButton btn_QuickJoin;

	public const string URL = "ui://5rtgb50em49ij99";

	public static UIRoomList_Button_QuickJoin CreateInstance()
	{
		return (UIRoomList_Button_QuickJoin)UIPackage.CreateObject("RoomList", "RoomList_Button_QuickJoin");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		GameMode = GetControllerAt(0);
		btn_SelectDifficulty = (GButton)GetChildAt(0);
		btn_QuickJoin = (GButton)GetChildAt(1);
	}
}
