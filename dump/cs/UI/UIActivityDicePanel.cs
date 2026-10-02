using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityDicePanel : GComponent
{
	public GGraph Bg;

	public UIActivityDice_Btn_Throw Button_Throw;

	public GTextField Txt_CricleNum;

	public GGroup Group_ThorwDice;

	public UIActivityDice_Grid Grid_1;

	public UIActivityDice_Grid Grid_2;

	public UIActivityDice_Grid Grid_3;

	public UIActivityDice_Grid Grid_4;

	public UIActivityDice_Grid Grid_5;

	public UIActivityDice_Grid Grid_6;

	public UIActivityDice_Grid Grid_24;

	public UIActivityDice_Grid Grid_23;

	public UIActivityDice_Grid Grid_22;

	public UIActivityDice_Grid Grid_21;

	public UIActivityDice_Grid Grid_17;

	public UIActivityDice_Grid Grid_18;

	public UIActivityDice_Grid Grid_19;

	public UIActivityDice_Grid Grid_20;

	public UIActivityDice_Grid Grid_7;

	public UIActivityDice_Grid Grid_16;

	public UIActivityDice_Grid Grid_15;

	public UIActivityDice_Grid Grid_14;

	public UIActivityDice_Grid Grid_13;

	public UIActivityDice_Grid Grid_8;

	public UIActivityDice_Grid Grid_11;

	public UIActivityDice_Grid Grid_10;

	public UIActivityDice_Grid Grid_12;

	public UIActivityDice_Grid Grid_9;

	public GComponent Player;

	public GGroup Group_Chessboard;

	public GList List_Mission;

	public GTextField Txt_Title;

	public GTextField Txt_Data;

	public GGroup Group_Mission;

	public GProgressBar Bar_Progress;

	public GList LIst_Reward;

	public GTextField Txt_Progress;

	public UIActivityDice_Button_Receive Btn_Receive;

	public GGroup Group_Reward;

	public GLoader loader_dice;

	public GGraph loader_video;

	public Transition Cut_in;

	public Transition PlayerLoop;

	public Transition PlayerCutin;

	public Transition PointChange;

	public Transition DiceFade;

	public const string URL = "ui://lypih982eo8h0";

	public static UIActivityDicePanel CreateInstance()
	{
		BindAll();
		return (UIActivityDicePanel)UIPackage.CreateObject("ActivityDice", "ActivityDicePanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://lypih982eo8h0", typeof(UIActivityDicePanel));
		UIObjectFactory.SetPackageItemExtension("ui://lypih982eo8h3", typeof(UIActivityDice_Grid));
		UIObjectFactory.SetPackageItemExtension("ui://lypih982eo8h4", typeof(UIActivityDice_Btn_Throw));
		UIObjectFactory.SetPackageItemExtension("ui://lypih982hdqo20", typeof(UIActivityDice_Button_Complete));
		UIObjectFactory.SetPackageItemExtension("ui://lypih982lbjz12", typeof(UIActivityDice_Com_DiceMission));
		UIObjectFactory.SetPackageItemExtension("ui://lypih982lbjz15", typeof(UIActivityDice_Button_TaskStatus));
		UIObjectFactory.SetPackageItemExtension("ui://lypih982lbjz1b", typeof(UIActivityDice_Button_GoWay));
		UIObjectFactory.SetPackageItemExtension("ui://lypih982lbjz1n", typeof(UIActivityDice_Button_Progress));
		UIObjectFactory.SetPackageItemExtension("ui://lypih982lbjzy", typeof(UIActivityDice_Bg_Mission));
		UIObjectFactory.SetPackageItemExtension("ui://lypih982rae11t", typeof(UIActivityDice_Button_Receive));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Bg = (GGraph)GetChildAt(0);
		Button_Throw = (UIActivityDice_Btn_Throw)GetChildAt(4);
		Txt_CricleNum = (GTextField)GetChildAt(5);
		Group_ThorwDice = (GGroup)GetChildAt(6);
		Grid_1 = (UIActivityDice_Grid)GetChildAt(8);
		Grid_2 = (UIActivityDice_Grid)GetChildAt(9);
		Grid_3 = (UIActivityDice_Grid)GetChildAt(10);
		Grid_4 = (UIActivityDice_Grid)GetChildAt(11);
		Grid_5 = (UIActivityDice_Grid)GetChildAt(12);
		Grid_6 = (UIActivityDice_Grid)GetChildAt(13);
		Grid_24 = (UIActivityDice_Grid)GetChildAt(14);
		Grid_23 = (UIActivityDice_Grid)GetChildAt(15);
		Grid_22 = (UIActivityDice_Grid)GetChildAt(16);
		Grid_21 = (UIActivityDice_Grid)GetChildAt(17);
		Grid_17 = (UIActivityDice_Grid)GetChildAt(18);
		Grid_18 = (UIActivityDice_Grid)GetChildAt(19);
		Grid_19 = (UIActivityDice_Grid)GetChildAt(20);
		Grid_20 = (UIActivityDice_Grid)GetChildAt(21);
		Grid_7 = (UIActivityDice_Grid)GetChildAt(22);
		Grid_16 = (UIActivityDice_Grid)GetChildAt(23);
		Grid_15 = (UIActivityDice_Grid)GetChildAt(24);
		Grid_14 = (UIActivityDice_Grid)GetChildAt(25);
		Grid_13 = (UIActivityDice_Grid)GetChildAt(26);
		Grid_8 = (UIActivityDice_Grid)GetChildAt(27);
		Grid_11 = (UIActivityDice_Grid)GetChildAt(28);
		Grid_10 = (UIActivityDice_Grid)GetChildAt(29);
		Grid_12 = (UIActivityDice_Grid)GetChildAt(30);
		Grid_9 = (UIActivityDice_Grid)GetChildAt(31);
		Player = (GComponent)GetChildAt(32);
		Group_Chessboard = (GGroup)GetChildAt(33);
		List_Mission = (GList)GetChildAt(35);
		Txt_Title = (GTextField)GetChildAt(36);
		Txt_Data = (GTextField)GetChildAt(38);
		Group_Mission = (GGroup)GetChildAt(39);
		Bar_Progress = (GProgressBar)GetChildAt(41);
		LIst_Reward = (GList)GetChildAt(42);
		Txt_Progress = (GTextField)GetChildAt(44);
		Btn_Receive = (UIActivityDice_Button_Receive)GetChildAt(45);
		Group_Reward = (GGroup)GetChildAt(46);
		loader_dice = (GLoader)GetChildAt(47);
		loader_video = (GGraph)GetChildAt(48);
		Cut_in = GetTransitionAt(0);
		PlayerLoop = GetTransitionAt(1);
		PlayerCutin = GetTransitionAt(2);
		PointChange = GetTransitionAt(3);
		DiceFade = GetTransitionAt(4);
	}
}
