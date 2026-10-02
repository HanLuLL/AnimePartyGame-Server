using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISettingInBattleWindow : GComponent
{
	public Controller showWatchCode;

	public Controller playerType;

	public GList list_lockExpression;

	public GTextField txt_Tip;

	public GButton btn_MonsterLibrary;

	public GButton btn_OpenSetting;

	public GButton btn_LeaveRoom;

	public GButton btn_QuitGame;

	public GButton btn_AudienceContinue;

	public GButton btn_AudienceOpenSetting;

	public GButton btn_AudienceLeaveRoom;

	public GButton btn_AudienceQuitGame;

	public GButton btn_Continue;

	public GButton btn_Copy;

	public GTextField txt_Code;

	public Transition CutIn;

	public const string URL = "ui://h47kn589d11o0";

	public static UISettingInBattleWindow CreateInstance()
	{
		BindAll();
		return (UISettingInBattleWindow)UIPackage.CreateObject("SettingInBattle", "SettingInBattleWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://h47kn589d11o0", typeof(UISettingInBattleWindow));
		UIObjectFactory.SetPackageItemExtension("ui://h47kn589d11o1", typeof(UISettingInBattle_Button_LockExpression));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showWatchCode = GetControllerAt(0);
		playerType = GetControllerAt(1);
		list_lockExpression = (GList)GetChildAt(2);
		txt_Tip = (GTextField)GetChildAt(3);
		btn_MonsterLibrary = (GButton)GetChildAt(5);
		btn_OpenSetting = (GButton)GetChildAt(6);
		btn_LeaveRoom = (GButton)GetChildAt(7);
		btn_QuitGame = (GButton)GetChildAt(8);
		btn_AudienceContinue = (GButton)GetChildAt(10);
		btn_AudienceOpenSetting = (GButton)GetChildAt(11);
		btn_AudienceLeaveRoom = (GButton)GetChildAt(12);
		btn_AudienceQuitGame = (GButton)GetChildAt(13);
		btn_Continue = (GButton)GetChildAt(15);
		btn_Copy = (GButton)GetChildAt(17);
		txt_Code = (GTextField)GetChildAt(19);
		CutIn = GetTransitionAt(0);
	}
}
