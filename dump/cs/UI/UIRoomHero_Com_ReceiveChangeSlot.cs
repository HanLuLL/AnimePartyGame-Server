using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomHero_Com_ReceiveChangeSlot : GComponent
{
	public GTextField txt_ApplySlot;

	public GButton btn_Reject;

	public GButton btn_Accept;

	public GTextField txt_Receive;

	public const string URL = "ui://l82hrmsqhzcu2h";

	public static UIRoomHero_Com_ReceiveChangeSlot CreateInstance()
	{
		return (UIRoomHero_Com_ReceiveChangeSlot)UIPackage.CreateObject("RoomHero", "RoomHero_Com_ReceiveChangeSlot");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_ApplySlot = (GTextField)GetChildAt(1);
		btn_Reject = (GButton)GetChildAt(2);
		btn_Accept = (GButton)GetChildAt(3);
		txt_Receive = (GTextField)GetChildAt(4);
	}
}
