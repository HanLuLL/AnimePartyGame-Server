using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomPlayer_Com_MasterMenu : GComponent
{
	public GButton btn_KickPlayer;

	public GButton btn_SetMaster;

	public GButton btn_AccountInfo;

	public const string URL = "ui://m6sn3r22zi0cbv";

	public static UIRoomPlayer_Com_MasterMenu CreateInstance()
	{
		return (UIRoomPlayer_Com_MasterMenu)UIPackage.CreateObject("Common_External", "RoomPlayer_Com_MasterMenu");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_KickPlayer = (GButton)GetChildAt(0);
		btn_SetMaster = (GButton)GetChildAt(1);
		btn_AccountInfo = (GButton)GetChildAt(2);
	}
}
