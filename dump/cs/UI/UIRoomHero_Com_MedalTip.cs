using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomHero_Com_MedalTip : GComponent
{
	public GList list_medalTip;

	public const string URL = "ui://l82hrmsqdx5u32";

	public static UIRoomHero_Com_MedalTip CreateInstance()
	{
		return (UIRoomHero_Com_MedalTip)UIPackage.CreateObject("RoomHero", "RoomHero_Com_MedalTip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_medalTip = (GList)GetChildAt(1);
	}
}
