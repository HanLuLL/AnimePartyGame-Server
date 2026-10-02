using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomWait_Com_SpeedUp : GComponent
{
	public Transition display;

	public const string URL = "ui://tskjvvjlukpb1a";

	public static UIRoomWait_Com_SpeedUp CreateInstance()
	{
		return (UIRoomWait_Com_SpeedUp)UIPackage.CreateObject("RoomWait", "RoomWait_Com_SpeedUp");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		display = GetTransitionAt(0);
	}
}
