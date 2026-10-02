using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINewGameLibrary_Com_MapMain : GComponent
{
	public Controller tab;

	public Controller Isroom;

	public Controller type;

	public GButton btn_Return;

	public UINewGamelibrary_Com_taball com_TabAll;

	public UINewGameLibrary_Com_Monster com_TabMonster;

	public UINewGameLibrary_Com_MapTab com_TabMap;

	public GButton btn_all;

	public GButton btn_monster;

	public GButton btn_relic;

	public GButton btn_card;

	public GButton btn_event;

	public GButton btn_land;

	public GGraph MapModel;

	public GGraph MapDray;

	public GButton next_btn;

	public GButton last_btn;

	public Transition Info_Cut_in;

	public Transition Cut_in;

	public const string URL = "ui://mc0y3plupj0z25";

	public static UINewGameLibrary_Com_MapMain CreateInstance()
	{
		return (UINewGameLibrary_Com_MapMain)UIPackage.CreateObject("NewGameLibrary", "NewGameLibrary_Com_MapMain");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tab = GetControllerAt(0);
		Isroom = GetControllerAt(1);
		type = GetControllerAt(2);
		btn_Return = (GButton)GetChildAt(0);
		com_TabAll = (UINewGamelibrary_Com_taball)GetChildAt(1);
		com_TabMonster = (UINewGameLibrary_Com_Monster)GetChildAt(2);
		com_TabMap = (UINewGameLibrary_Com_MapTab)GetChildAt(3);
		btn_all = (GButton)GetChildAt(4);
		btn_monster = (GButton)GetChildAt(5);
		btn_relic = (GButton)GetChildAt(6);
		btn_card = (GButton)GetChildAt(7);
		btn_event = (GButton)GetChildAt(8);
		btn_land = (GButton)GetChildAt(9);
		MapModel = (GGraph)GetChildAt(10);
		MapDray = (GGraph)GetChildAt(11);
		next_btn = (GButton)GetChildAt(12);
		last_btn = (GButton)GetChildAt(13);
		Info_Cut_in = GetTransitionAt(0);
		Cut_in = GetTransitionAt(1);
	}
}
