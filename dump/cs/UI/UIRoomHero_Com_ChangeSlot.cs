using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomHero_Com_ChangeSlot : GComponent
{
	public GButton btn_ChangeSlot;

	public UIRoomHero_Com_ApplyChangeSlot com_ApplyChangeSlot;

	public UIRoomHero_Com_ReceiveChangeSlot com_Receive1;

	public UIRoomHero_Com_ReceiveChangeSlot com_Receive2;

	public UIRoomHero_Com_ReceiveChangeSlot com_Receive3;

	public const string URL = "ui://l82hrmsqhzcu2i";

	public static UIRoomHero_Com_ChangeSlot CreateInstance()
	{
		return (UIRoomHero_Com_ChangeSlot)UIPackage.CreateObject("RoomHero", "RoomHero_Com_ChangeSlot");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_ChangeSlot = (GButton)GetChildAt(0);
		com_ApplyChangeSlot = (UIRoomHero_Com_ApplyChangeSlot)GetChildAt(1);
		com_Receive1 = (UIRoomHero_Com_ReceiveChangeSlot)GetChildAt(2);
		com_Receive2 = (UIRoomHero_Com_ReceiveChangeSlot)GetChildAt(3);
		com_Receive3 = (UIRoomHero_Com_ReceiveChangeSlot)GetChildAt(4);
	}
}
