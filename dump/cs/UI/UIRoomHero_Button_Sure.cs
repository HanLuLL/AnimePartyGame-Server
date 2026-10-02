using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomHero_Button_Sure : GButton
{
	public Transition Loop;

	public const string URL = "ui://l82hrmsqqzg8w";

	public static UIRoomHero_Button_Sure CreateInstance()
	{
		return (UIRoomHero_Button_Sure)UIPackage.CreateObject("RoomHero", "RoomHero_Button_Sure");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Loop = GetTransitionAt(0);
	}
}
