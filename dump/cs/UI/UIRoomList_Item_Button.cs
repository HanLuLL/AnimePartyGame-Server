using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomList_Item_Button : GButton
{
	public Controller state;

	public UIRoomList_PlayerLabel_Loader com_PlayerLabel;

	public GLoader loader_HeadIcon;

	public GTextField txt_MapName;

	public GTextField txt_Num;

	public GTextField txt_MaxNum;

	public GList list_RoomSetting;

	public GImage image_isBlack;

	public Transition Cut_in;

	public Transition ClickScale;

	public const string URL = "ui://5rtgb50eq6fh7u";

	public static UIRoomList_Item_Button CreateInstance()
	{
		return (UIRoomList_Item_Button)UIPackage.CreateObject("RoomList", "RoomList_Item_Button");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(0);
		com_PlayerLabel = (UIRoomList_PlayerLabel_Loader)GetChildAt(2);
		loader_HeadIcon = (GLoader)GetChildAt(5);
		txt_MapName = (GTextField)GetChildAt(7);
		txt_Num = (GTextField)GetChildAt(11);
		txt_MaxNum = (GTextField)GetChildAt(13);
		list_RoomSetting = (GList)GetChildAt(17);
		image_isBlack = (GImage)GetChildAt(18);
		Cut_in = GetTransitionAt(0);
		ClickScale = GetTransitionAt(1);
	}
}
