using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomTerms_ScrollBar_Grip : GButton
{
	public GGraph grip;

	public const string URL = "ui://tzpop51dejjwj";

	public static UIRoomTerms_ScrollBar_Grip CreateInstance()
	{
		return (UIRoomTerms_ScrollBar_Grip)UIPackage.CreateObject("RoomTerms", "RoomTerms_ScrollBar_Grip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		grip = (GGraph)GetChildAt(0);
	}
}
