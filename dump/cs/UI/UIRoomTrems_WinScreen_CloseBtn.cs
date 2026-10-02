using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomTrems_WinScreen_CloseBtn : GButton
{
	public Transition Cut_in;

	public const string URL = "ui://tzpop51dejjwr";

	public static UIRoomTrems_WinScreen_CloseBtn CreateInstance()
	{
		return (UIRoomTrems_WinScreen_CloseBtn)UIPackage.CreateObject("RoomTerms", "RoomTrems_WinScreen_CloseBtn");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Cut_in = GetTransitionAt(0);
	}
}
