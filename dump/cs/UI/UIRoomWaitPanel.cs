using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomWaitPanel : GComponent
{
	public Controller roomMaster;

	public Controller speedMode;

	public Controller masterKick;

	public GButton btn_Return;

	public GButton btn_Leave;

	public GComponent com_SetRoom;

	public GComponent com_Player;

	public GButton btn_Start;

	public UIRoomWait_Button_Ready btn_Ready;

	public GProgressBar progress_KickMaster;

	public UIRoomWait_Com_SpeedUp com_Up;

	public UIRoomWait_Com_SpeedDown com_Down;

	public Transition Cut_in;

	public const string URL = "ui://tskjvvjlp4oi0";

	public static UIRoomWaitPanel CreateInstance()
	{
		BindAll();
		return (UIRoomWaitPanel)UIPackage.CreateObject("RoomWait", "RoomWaitPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://tskjvvjlayrmx", typeof(UIRoomWait_Button_Ready));
		UIObjectFactory.SetPackageItemExtension("ui://tskjvvjlp4oi0", typeof(UIRoomWaitPanel));
		UIObjectFactory.SetPackageItemExtension("ui://tskjvvjlukpb18", typeof(UIRoomWait_Com_SpeedDown));
		UIObjectFactory.SetPackageItemExtension("ui://tskjvvjlukpb1a", typeof(UIRoomWait_Com_SpeedUp));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		roomMaster = GetControllerAt(0);
		speedMode = GetControllerAt(1);
		masterKick = GetControllerAt(2);
		btn_Return = (GButton)GetChildAt(2);
		btn_Leave = (GButton)GetChildAt(3);
		com_SetRoom = (GComponent)GetChildAt(4);
		com_Player = (GComponent)GetChildAt(6);
		btn_Start = (GButton)GetChildAt(7);
		btn_Ready = (UIRoomWait_Button_Ready)GetChildAt(8);
		progress_KickMaster = (GProgressBar)GetChildAt(9);
		com_Up = (UIRoomWait_Com_SpeedUp)GetChildAt(11);
		com_Down = (UIRoomWait_Com_SpeedDown)GetChildAt(12);
		Cut_in = GetTransitionAt(0);
	}
}
