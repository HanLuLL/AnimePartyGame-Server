using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomHero_ListItem_Medal : GComponent
{
	public Controller type;

	public Controller haveMedal;

	public GTextField title;

	public const string URL = "ui://l82hrmsqdx5u31";

	public static UIRoomHero_ListItem_Medal CreateInstance()
	{
		return (UIRoomHero_ListItem_Medal)UIPackage.CreateObject("RoomHero", "RoomHero_ListItem_Medal");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		haveMedal = GetControllerAt(1);
		title = (GTextField)GetChildAt(1);
	}
}
