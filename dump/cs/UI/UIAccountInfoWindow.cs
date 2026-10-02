using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAccountInfoWindow : GComponent
{
	public Controller showHero;

	public Controller showAchieve;

	public GGraph mohu;

	public UIAccountInfo_Com_Main com_Main;

	public UIAccountInfo_Com_Hero com_Hero;

	public UIAccountInfo_Com_Achieve com_Achieve;

	public GList list_Operate;

	public Transition Cut_in;

	public Transition t1;

	public const string URL = "ui://iepldke7m2t80";

	public static UIAccountInfoWindow CreateInstance()
	{
		BindAll();
		return (UIAccountInfoWindow)UIPackage.CreateObject("AccountInfo", "AccountInfoWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://iepldke77h6s24", typeof(UIAccount_Button_Operator));
		UIObjectFactory.SetPackageItemExtension("ui://iepldke7f3tl19", typeof(UIAccountInfo_Com_AchieveLoader));
		UIObjectFactory.SetPackageItemExtension("ui://iepldke7f3tl1n", typeof(UIAccountInfo_Button_Grip));
		UIObjectFactory.SetPackageItemExtension("ui://iepldke7f3tl1y", typeof(UIAccountInfo_Com_Skin));
		UIObjectFactory.SetPackageItemExtension("ui://iepldke7hni02", typeof(UIAccountInfo_Com_Hero));
		UIObjectFactory.SetPackageItemExtension("ui://iepldke7hni04", typeof(UIAccountInfo_Button_Hero));
		UIObjectFactory.SetPackageItemExtension("ui://iepldke7hni07", typeof(UIAccountInfo_Loader_Hero));
		UIObjectFactory.SetPackageItemExtension("ui://iepldke7it6m2x", typeof(UIAccountInfo_Button_Replay));
		UIObjectFactory.SetPackageItemExtension("ui://iepldke7it6m32", typeof(UIAccountInfo_Button_ReplaySaving));
		UIObjectFactory.SetPackageItemExtension("ui://iepldke7m2t80", typeof(UIAccountInfoWindow));
		UIObjectFactory.SetPackageItemExtension("ui://iepldke7p5xi2j", typeof(UIAccountInfo_Button_UID));
		UIObjectFactory.SetPackageItemExtension("ui://iepldke7x20l20", typeof(UIAccountInfo_Com_Main));
		UIObjectFactory.SetPackageItemExtension("ui://iepldke7zhzta", typeof(UIAccountInfo_Button_ShowAchieve));
		UIObjectFactory.SetPackageItemExtension("ui://iepldke7zhztb", typeof(UIAccountInfo_Com_Achieve));
		UIObjectFactory.SetPackageItemExtension("ui://iepldke7zhztc", typeof(UIAccountInfo_Button_SelectAchieve));
		UIObjectFactory.SetPackageItemExtension("ui://iepldke7zhztg", typeof(UIAccountInfo_Button_Toggle));
		UIObjectFactory.SetPackageItemExtension("ui://iepldke7zhzth", typeof(UIAccount_Button_FightData));
		UIObjectFactory.SetPackageItemExtension("ui://iepldke7zhzti", typeof(UIAccountInfo_Com_FightInfo));
		UIObjectFactory.SetPackageItemExtension("ui://iepldke7zhztj", typeof(UIAccountInfo_Com_Rank));
		UIObjectFactory.SetPackageItemExtension("ui://iepldke7zhztk", typeof(UIAccountInfoo_Com_PlayerLevel));
		UIObjectFactory.SetPackageItemExtension("ui://iepldke7zhztv", typeof(UIAccountInfo_Button_RankInfo));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showHero = GetControllerAt(0);
		showAchieve = GetControllerAt(1);
		mohu = (GGraph)GetChildAt(0);
		com_Main = (UIAccountInfo_Com_Main)GetChildAt(1);
		com_Hero = (UIAccountInfo_Com_Hero)GetChildAt(2);
		com_Achieve = (UIAccountInfo_Com_Achieve)GetChildAt(3);
		list_Operate = (GList)GetChildAt(4);
		Cut_in = GetTransitionAt(0);
		t1 = GetTransitionAt(1);
	}
}
