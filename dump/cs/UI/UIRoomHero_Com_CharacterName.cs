using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomHero_Com_CharacterName : GComponent
{
	public GTextField txt_Title;

	public const string URL = "ui://l82hrmsqtcj01k";

	public static UIRoomHero_Com_CharacterName CreateInstance()
	{
		return (UIRoomHero_Com_CharacterName)UIPackage.CreateObject("RoomHero", "RoomHero_Com_CharacterName");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Title = (GTextField)GetChildAt(0);
	}
}
