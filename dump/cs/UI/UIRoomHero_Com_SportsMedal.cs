using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomHero_Com_SportsMedal : GButton
{
	public GImage rank_1;

	public GImage rank_2;

	public GImage rank_3;

	public GTextField txt_VictoryCount;

	public GLoader medalRoot;

	public const string URL = "ui://l82hrmsqdx5u2l";

	public static UIRoomHero_Com_SportsMedal CreateInstance()
	{
		return (UIRoomHero_Com_SportsMedal)UIPackage.CreateObject("RoomHero", "RoomHero_Com_SportsMedal");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		rank_1 = (GImage)GetChildAt(1);
		rank_2 = (GImage)GetChildAt(2);
		rank_3 = (GImage)GetChildAt(3);
		txt_VictoryCount = (GTextField)GetChildAt(4);
		medalRoot = (GLoader)GetChildAt(5);
	}
}
