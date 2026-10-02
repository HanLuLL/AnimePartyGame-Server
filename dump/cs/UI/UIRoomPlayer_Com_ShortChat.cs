using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomPlayer_Com_ShortChat : GComponent
{
	public GList list_Chat;

	public const string URL = "ui://m6sn3r22zi0cbs";

	public static UIRoomPlayer_Com_ShortChat CreateInstance()
	{
		return (UIRoomPlayer_Com_ShortChat)UIPackage.CreateObject("Common_External", "RoomPlayer_Com_ShortChat");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_Chat = (GList)GetChildAt(1);
	}
}
