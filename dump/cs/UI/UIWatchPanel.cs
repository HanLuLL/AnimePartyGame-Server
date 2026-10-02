using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIWatchPanel : GComponent
{
	public Controller tab;

	public Controller showResult;

	public GButton btn_Return;

	public GButton btn_SerachPlay;

	public GTextInput txtField_Search;

	public UIWatch_Com_Player com_player_4;

	public UIWatch_Com_Player com_player_3;

	public UIWatch_Com_Player com_player_2;

	public UIWatch_Com_Player com_player_1;

	public GTextField txt_WatchCount;

	public GTextField txt_StartTime;

	public GButton btn_Watch;

	public const string URL = "ui://sv6gwhbej6om0";

	public static UIWatchPanel CreateInstance()
	{
		BindAll();
		return (UIWatchPanel)UIPackage.CreateObject("Watch", "WatchPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://sv6gwhbej6om0", typeof(UIWatchPanel));
		UIObjectFactory.SetPackageItemExtension("ui://sv6gwhbej6om1", typeof(UIWatch_Com_Player));
		UIObjectFactory.SetPackageItemExtension("ui://sv6gwhbej6om4", typeof(UIWatch_Button_DetailInfo));
		UIObjectFactory.SetPackageItemExtension("ui://sv6gwhbekqgjn", typeof(UIWatch_Com_Skill));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tab = GetControllerAt(0);
		showResult = GetControllerAt(1);
		btn_Return = (GButton)GetChildAt(0);
		btn_SerachPlay = (GButton)GetChildAt(1);
		txtField_Search = (GTextInput)GetChildAt(3);
		com_player_4 = (UIWatch_Com_Player)GetChildAt(4);
		com_player_3 = (UIWatch_Com_Player)GetChildAt(5);
		com_player_2 = (UIWatch_Com_Player)GetChildAt(6);
		com_player_1 = (UIWatch_Com_Player)GetChildAt(7);
		txt_WatchCount = (GTextField)GetChildAt(9);
		txt_StartTime = (GTextField)GetChildAt(10);
		btn_Watch = (GButton)GetChildAt(12);
	}
}
