using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomHero_CharacterDi : GComponent
{
	public Controller p;

	public Controller battlePass;

	public GMovieClip aMovie_Selected;

	public Transition aMoive;

	public const string URL = "ui://l82hrmsqqzg8t";

	public static UIRoomHero_CharacterDi CreateInstance()
	{
		return (UIRoomHero_CharacterDi)UIPackage.CreateObject("RoomHero", "RoomHero_CharacterDi");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		p = GetControllerAt(0);
		battlePass = GetControllerAt(1);
		aMovie_Selected = (GMovieClip)GetChildAt(0);
		aMoive = GetTransitionAt(0);
	}
}
