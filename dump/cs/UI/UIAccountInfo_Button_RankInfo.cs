using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAccountInfo_Button_RankInfo : GButton
{
	public Controller GameMode;

	public Controller PVEResult;

	public Controller GiveUp;

	public UIAccountInfo_Com_Rank com_Rank;

	public GTextField txt_PlayerNick;

	public GTextField txt_HeroNick;

	public Transition Cut_in;

	public const string URL = "ui://iepldke7zhztv";

	public static UIAccountInfo_Button_RankInfo CreateInstance()
	{
		return (UIAccountInfo_Button_RankInfo)UIPackage.CreateObject("AccountInfo", "AccountInfo_Button_RankInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		GameMode = GetControllerAt(0);
		PVEResult = GetControllerAt(1);
		GiveUp = GetControllerAt(3);
		com_Rank = (UIAccountInfo_Com_Rank)GetChildAt(1);
		txt_PlayerNick = (GTextField)GetChildAt(2);
		txt_HeroNick = (GTextField)GetChildAt(3);
		Cut_in = GetTransitionAt(0);
	}
}
