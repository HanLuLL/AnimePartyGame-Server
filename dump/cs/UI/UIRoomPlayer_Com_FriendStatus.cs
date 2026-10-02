using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomPlayer_Com_FriendStatus : GComponent
{
	public Controller type;

	public const string URL = "ui://m6sn3r22zi0cbp";

	public static UIRoomPlayer_Com_FriendStatus CreateInstance()
	{
		return (UIRoomPlayer_Com_FriendStatus)UIPackage.CreateObject("Common_External", "RoomPlayer_Com_FriendStatus");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
	}
}
