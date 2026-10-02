using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandGambleWindow : GComponent
{
	public Controller phase;

	public Controller showPoint;

	public Controller pointState;

	public GTextField txt_Prize;

	public UILandGamble_Button_dd btn_odd;

	public UILandGamble_Button_Even btn_even;

	public GButton btn_Dice;

	public GTextField totalPoint;

	public UILandGamble_Player player1;

	public GComponent player1_Dice;

	public UILandGamble_Player player2;

	public GComponent player2_Dice;

	public UILandGamble_Player player3;

	public GComponent player3_Dice;

	public UILandGamble_Player player4;

	public GComponent player4_Dice;

	public Transition btn_Dice_2;

	public Transition totalPoint_2;

	public const string URL = "ui://d1prfn9srum70";

	public static UILandGambleWindow CreateInstance()
	{
		BindAll();
		return (UILandGambleWindow)UIPackage.CreateObject("LandGamble", "LandGambleWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://d1prfn9srum70", typeof(UILandGambleWindow));
		UIObjectFactory.SetPackageItemExtension("ui://d1prfn9srum73", typeof(UILandGamble_Button_dd));
		UIObjectFactory.SetPackageItemExtension("ui://d1prfn9srum77", typeof(UILandGamble_Button_Even));
		UIObjectFactory.SetPackageItemExtension("ui://d1prfn9srum7b", typeof(UILandGamble_Player));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		phase = GetControllerAt(0);
		showPoint = GetControllerAt(1);
		pointState = GetControllerAt(2);
		txt_Prize = (GTextField)GetChildAt(4);
		btn_odd = (UILandGamble_Button_dd)GetChildAt(7);
		btn_even = (UILandGamble_Button_Even)GetChildAt(8);
		btn_Dice = (GButton)GetChildAt(10);
		totalPoint = (GTextField)GetChildAt(16);
		player1 = (UILandGamble_Player)GetChildAt(19);
		player1_Dice = (GComponent)GetChildAt(20);
		player2 = (UILandGamble_Player)GetChildAt(21);
		player2_Dice = (GComponent)GetChildAt(22);
		player3 = (UILandGamble_Player)GetChildAt(23);
		player3_Dice = (GComponent)GetChildAt(24);
		player4 = (UILandGamble_Player)GetChildAt(25);
		player4_Dice = (GComponent)GetChildAt(26);
		btn_Dice_2 = GetTransitionAt(0);
		totalPoint_2 = GetTransitionAt(1);
	}
}
