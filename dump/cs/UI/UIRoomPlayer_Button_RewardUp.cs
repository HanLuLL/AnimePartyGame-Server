using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomPlayer_Button_RewardUp : GButton
{
	public Controller state;

	public const string URL = "ui://xuaw6o8jrct9q3d";

	public static UIRoomPlayer_Button_RewardUp CreateInstance()
	{
		return (UIRoomPlayer_Button_RewardUp)UIPackage.CreateObject("Common", "RoomPlayer_Button_RewardUp");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(1);
	}
}
