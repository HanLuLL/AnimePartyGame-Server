using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomWait_Com_SpeedDown : GComponent
{
	public Transition display;

	public const string URL = "ui://tskjvvjlukpb18";

	public static UIRoomWait_Com_SpeedDown CreateInstance()
	{
		return (UIRoomWait_Com_SpeedDown)UIPackage.CreateObject("RoomWait", "RoomWait_Com_SpeedDown");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		display = GetTransitionAt(0);
	}
}
