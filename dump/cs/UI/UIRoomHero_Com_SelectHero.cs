using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomHero_Com_SelectHero : GComponent
{
	public Controller p;

	public Controller stateChange;

	public Controller Sure;

	public GGraph zhezhao;

	public GLoader load_Character;

	public GLoader load_SelectedCharacter;

	public UIRoomHero_Com_CharacterName com_CharacterName;

	public UIRoomHero_CharacterDi com_selectedaMovie;

	public Transition xuanting;

	public Transition queding;

	public Transition xuanze;

	public Transition OverSfx;

	public Transition DownSfx;

	public const string URL = "ui://l82hrmsqiorl1a";

	public static UIRoomHero_Com_SelectHero CreateInstance()
	{
		return (UIRoomHero_Com_SelectHero)UIPackage.CreateObject("RoomHero", "RoomHero_Com_SelectHero");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		p = GetControllerAt(0);
		stateChange = GetControllerAt(1);
		Sure = GetControllerAt(2);
		zhezhao = (GGraph)GetChildAt(0);
		load_Character = (GLoader)GetChildAt(3);
		load_SelectedCharacter = (GLoader)GetChildAt(4);
		com_CharacterName = (UIRoomHero_Com_CharacterName)GetChildAt(7);
		com_selectedaMovie = (UIRoomHero_CharacterDi)GetChildAt(14);
		xuanting = GetTransitionAt(0);
		queding = GetTransitionAt(1);
		xuanze = GetTransitionAt(2);
		OverSfx = GetTransitionAt(3);
		DownSfx = GetTransitionAt(4);
	}
}
