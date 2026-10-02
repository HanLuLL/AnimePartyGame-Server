using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomListPanel : GComponent
{
	public Controller tab;

	public GButton btn_Return;

	public GList list_Room;

	public GButton btn_JoinTargetRoom;

	public GTextInput txtField_Search;

	public GButton btn_Refresh;

	public UIRoomList_Button_QuickJoin com_QuickJoin;

	public GButton btn_OpenCreateRoom;

	public GButton btn_Watch;

	public GButton btn_ExplainMode;

	public GList list_GameMode;

	public GButton btn_prePage;

	public GButton btn_nextPage;

	public GComponent com_CreateRoom;

	public Transition Cut_in;

	public const string URL = "ui://5rtgb50eqb3s8";

	public static UIRoomListPanel CreateInstance()
	{
		BindAll();
		return (UIRoomListPanel)UIPackage.CreateObject("RoomList", "RoomListPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://5rtgb50eglhuj90", typeof(UIRoomList_Button_GameMode));
		UIObjectFactory.SetPackageItemExtension("ui://5rtgb50eiorlj94", typeof(UIRoomList_Com_ThinkTime));
		UIObjectFactory.SetPackageItemExtension("ui://5rtgb50eiorlj95", typeof(UIRoomList_Com_Speed));
		UIObjectFactory.SetPackageItemExtension("ui://5rtgb50eiorlj97", typeof(UIRoomList_Com_Difficulty));
		UIObjectFactory.SetPackageItemExtension("ui://5rtgb50em2t88a", typeof(UIRoomList_PlayerLabel_Loader));
		UIObjectFactory.SetPackageItemExtension("ui://5rtgb50em49ij99", typeof(UIRoomList_Button_QuickJoin));
		UIObjectFactory.SetPackageItemExtension("ui://5rtgb50eq6fh7u", typeof(UIRoomList_Item_Button));
		UIObjectFactory.SetPackageItemExtension("ui://5rtgb50eqb3s8", typeof(UIRoomListPanel));
		UIObjectFactory.SetPackageItemExtension("ui://5rtgb50esxcpj9i", typeof(UIRoomList_Com_GoldCount));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tab = GetControllerAt(0);
		btn_Return = (GButton)GetChildAt(0);
		list_Room = (GList)GetChildAt(1);
		btn_JoinTargetRoom = (GButton)GetChildAt(2);
		txtField_Search = (GTextInput)GetChildAt(4);
		btn_Refresh = (GButton)GetChildAt(6);
		com_QuickJoin = (UIRoomList_Button_QuickJoin)GetChildAt(7);
		btn_OpenCreateRoom = (GButton)GetChildAt(8);
		btn_Watch = (GButton)GetChildAt(9);
		btn_ExplainMode = (GButton)GetChildAt(11);
		list_GameMode = (GList)GetChildAt(12);
		btn_prePage = (GButton)GetChildAt(13);
		btn_nextPage = (GButton)GetChildAt(14);
		com_CreateRoom = (GComponent)GetChildAt(16);
		Cut_in = GetTransitionAt(0);
	}
}
