using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayerSettingInBattleWindow : GComponent
{
	public GButton btn_Continue;

	public GButton btn_OpenSetting;

	public GButton btn_Reset;

	public GButton btn_LeaveRoom;

	public GButton btn_QuitGame;

	public Transition Cut_In;

	public const string URL = "ui://r680i7z8qogv0";

	public static UISinglePlayerSettingInBattleWindow CreateInstance()
	{
		BindAll();
		return (UISinglePlayerSettingInBattleWindow)UIPackage.CreateObject("SinglePlayerSettingInBattle", "SinglePlayerSettingInBattleWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://r680i7z8qogv0", typeof(UISinglePlayerSettingInBattleWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_Continue = (GButton)GetChildAt(2);
		btn_OpenSetting = (GButton)GetChildAt(3);
		btn_Reset = (GButton)GetChildAt(4);
		btn_LeaveRoom = (GButton)GetChildAt(5);
		btn_QuitGame = (GButton)GetChildAt(6);
		Cut_In = GetTransitionAt(0);
	}
}
