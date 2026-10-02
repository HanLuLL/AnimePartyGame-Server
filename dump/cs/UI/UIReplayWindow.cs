using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIReplayWindow : GComponent
{
	public Controller status;

	public Controller showTurn;

	public GImage image_HP;

	public GImage image_Monster;

	public GImage image_Gold;

	public GImage image_Lv;

	public GGraph zhezhao;

	public UIReplay_Button_Control btn_Control;

	public UIReplay_Button_Turn btn_NextTurn;

	public UIReplay_Button_Turn btn_PreTurn;

	public UIReplay_Button_Round btn_NextRound;

	public UIReplay_Button_Round btn_PreRound;

	public GButton btn_Speed;

	public UIReplay_Button_OpenTurn btn_OpenTurn;

	public UIReplay_Button_Switch btn_Close;

	public UIReplay_Button_Switch btn_Open;

	public GList list_Nodes;

	public Transition Cut_in;

	public Transition Cut_out;

	public Transition List_Cut_in;

	public Transition List_Cut_out;

	public const string URL = "ui://dw3tmgbem0hx0";

	public static UIReplayWindow CreateInstance()
	{
		BindAll();
		return (UIReplayWindow)UIPackage.CreateObject("Replay", "ReplayWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://dw3tmgbem0hx0", typeof(UIReplayWindow));
		UIObjectFactory.SetPackageItemExtension("ui://dw3tmgbem0hx1", typeof(UIReplay_Button_Control));
		UIObjectFactory.SetPackageItemExtension("ui://dw3tmgbem0hx2", typeof(UIReplay_Button_Turn));
		UIObjectFactory.SetPackageItemExtension("ui://dw3tmgbem0hx3", typeof(UIReplay_Button_Round));
		UIObjectFactory.SetPackageItemExtension("ui://dw3tmgbem0hx5", typeof(UIReplay_Button_OpenTurn));
		UIObjectFactory.SetPackageItemExtension("ui://dw3tmgbem0hx7", typeof(UIReplay_Button_RoundItem));
		UIObjectFactory.SetPackageItemExtension("ui://dw3tmgbem0hx8", typeof(UIReplay_Button_TurnItem));
		UIObjectFactory.SetPackageItemExtension("ui://dw3tmgbepf0p14", typeof(UIReplay_Button_Grip));
		UIObjectFactory.SetPackageItemExtension("ui://dw3tmgbepf0p15", typeof(UIReplay_Button_Switch));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		showTurn = GetControllerAt(1);
		image_HP = (GImage)GetChildAt(0);
		image_Monster = (GImage)GetChildAt(1);
		image_Gold = (GImage)GetChildAt(2);
		image_Lv = (GImage)GetChildAt(3);
		zhezhao = (GGraph)GetChildAt(4);
		btn_Control = (UIReplay_Button_Control)GetChildAt(14);
		btn_NextTurn = (UIReplay_Button_Turn)GetChildAt(15);
		btn_PreTurn = (UIReplay_Button_Turn)GetChildAt(16);
		btn_NextRound = (UIReplay_Button_Round)GetChildAt(17);
		btn_PreRound = (UIReplay_Button_Round)GetChildAt(18);
		btn_Speed = (GButton)GetChildAt(19);
		btn_OpenTurn = (UIReplay_Button_OpenTurn)GetChildAt(20);
		btn_Close = (UIReplay_Button_Switch)GetChildAt(22);
		btn_Open = (UIReplay_Button_Switch)GetChildAt(23);
		list_Nodes = (GList)GetChildAt(25);
		Cut_in = GetTransitionAt(0);
		Cut_out = GetTransitionAt(1);
		List_Cut_in = GetTransitionAt(2);
		List_Cut_out = GetTransitionAt(3);
	}
}
