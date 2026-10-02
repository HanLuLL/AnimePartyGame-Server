using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomHero_Com_BubbleTip : GComponent
{
	public GTextField txt_content;

	public const string URL = "ui://l82hrmsqdx5u2n";

	public static UIRoomHero_Com_BubbleTip CreateInstance()
	{
		return (UIRoomHero_Com_BubbleTip)UIPackage.CreateObject("RoomHero", "RoomHero_Com_BubbleTip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_content = (GTextField)GetChildAt(1);
	}
}
