using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomWait_Button_Ready : GButton
{
	public Controller status;

	public const string URL = "ui://tskjvvjlayrmx";

	public static UIRoomWait_Button_Ready CreateInstance()
	{
		return (UIRoomWait_Button_Ready)UIPackage.CreateObject("RoomWait", "RoomWait_Button_Ready");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
	}
}
