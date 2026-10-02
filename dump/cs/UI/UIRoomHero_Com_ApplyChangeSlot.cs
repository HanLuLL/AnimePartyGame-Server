using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomHero_Com_ApplyChangeSlot : GComponent
{
	public GTextField txt_TargetSlot;

	public GTextField txt_Apply;

	public GButton btn_Cancel;

	public const string URL = "ui://l82hrmsqhzcu2g";

	public static UIRoomHero_Com_ApplyChangeSlot CreateInstance()
	{
		return (UIRoomHero_Com_ApplyChangeSlot)UIPackage.CreateObject("RoomHero", "RoomHero_Com_ApplyChangeSlot");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_TargetSlot = (GTextField)GetChildAt(1);
		txt_Apply = (GTextField)GetChildAt(2);
		btn_Cancel = (GButton)GetChildAt(3);
	}
}
