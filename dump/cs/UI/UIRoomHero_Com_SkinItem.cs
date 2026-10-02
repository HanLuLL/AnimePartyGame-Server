using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomHero_Com_SkinItem : GComponent
{
	public GLoader loader_Character;

	public const string URL = "ui://l82hrmsqec7w1u";

	public static UIRoomHero_Com_SkinItem CreateInstance()
	{
		return (UIRoomHero_Com_SkinItem)UIPackage.CreateObject("RoomHero", "RoomHero_Com_SkinItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Character = (GLoader)GetChildAt(1);
	}
}
