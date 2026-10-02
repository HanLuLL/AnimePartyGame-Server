using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIMatchSuccessWindow : GComponent
{
	public GLoader loader_Bg;

	public UIMatchSuccess_Com_MapBanner com_Map;

	public GTextField txt_Theme;

	public GTextField txt_ModeName;

	public GTextField txt_MapName;

	public GComponent com_Player_1;

	public GComponent com_Player_2;

	public GComponent com_Player_3;

	public GComponent com_Player_4;

	public GButton btn_StartGame;

	public GTextField txt_Time;

	public Transition loop;

	public Transition Cut_in;

	public Transition loop2;

	public const string URL = "ui://aepd0gr2jo0y10";

	public static UIMatchSuccessWindow CreateInstance()
	{
		BindAll();
		return (UIMatchSuccessWindow)UIPackage.CreateObject("MatchSuccess", "MatchSuccessWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://aepd0gr2jo0y10", typeof(UIMatchSuccessWindow));
		UIObjectFactory.SetPackageItemExtension("ui://aepd0gr2jxlu16", typeof(UIHeiSeTiaoTiao));
		UIObjectFactory.SetPackageItemExtension("ui://aepd0gr2jxlu18", typeof(UIBG2));
		UIObjectFactory.SetPackageItemExtension("ui://aepd0gr2r26s2", typeof(UIMatchSuccess_Com_MapBanner));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Bg = (GLoader)GetChildAt(12);
		com_Map = (UIMatchSuccess_Com_MapBanner)GetChildAt(14);
		txt_Theme = (GTextField)GetChildAt(15);
		txt_ModeName = (GTextField)GetChildAt(16);
		txt_MapName = (GTextField)GetChildAt(17);
		com_Player_1 = (GComponent)GetChildAt(18);
		com_Player_2 = (GComponent)GetChildAt(19);
		com_Player_3 = (GComponent)GetChildAt(20);
		com_Player_4 = (GComponent)GetChildAt(21);
		btn_StartGame = (GButton)GetChildAt(22);
		txt_Time = (GTextField)GetChildAt(23);
		loop = GetTransitionAt(0);
		Cut_in = GetTransitionAt(1);
		loop2 = GetTransitionAt(2);
	}
}
