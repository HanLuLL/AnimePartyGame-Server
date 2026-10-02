using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGMWindow : GComponent
{
	public Controller tab;

	public Controller ShowServer;

	public UIGM_Com_SetContent com_Move;

	public UIGM_Com_SetContent com_MoveAgain;

	public UIGM_Com_SetContent com_AtkPoint;

	public UIGM_Com_SetContent com_DefPoint;

	public UIGM_Com_SetContent com_BombPoint;

	public UIGM_Button_Operate btn_ReSet;

	public UIGM_Button_SingleContent btn_ClinetAI;

	public UIGM_Button_SingleContent btn_ClinetAFK;

	public GTextField txt_IP;

	public GTextField txt_Player;

	public GTextField txt_ServerTimer;

	public GTextInput Input_Server;

	public GList list_Server;

	public UIGM_Button_Operate btn_ChangeIP;

	public GGroup group_Login;

	public GComboBox comboBox_Event;

	public UIGM_Button_Operate btn_Event;

	public GComboBox comboBox_Destiny;

	public UIGM_Button_Operate btn_Destiny;

	public GComboBox comboBox_Divination;

	public UIGM_Button_Operate btn_Divination;

	public GTextInput txtField_terms;

	public UIGM_Button_Operate btn_SetTerms;

	public GComboBox comboBox_MapDifficulty;

	public UIGM_Button_Operate btn_SetMapDifficulty;

	public UIGM_Button_Operate btn_OpenLandId;

	public UIGM_Button_Operate btn_CloseLandId;

	public UIGM_Button_Operate btn_OpenFreeCamera;

	public UIGM_Button_Operate btn_CloseFreeCamera;

	public UIGM_Button_Operate btn_UnlockSportInfo;

	public UIGM_Button_Operate btn_UnlockRoleInfo;

	public UIGM_Com_SetAttr com_HP;

	public UIGM_Com_SetAttr com_Gold;

	public UIGM_Com_SetAttr com_RelicCount;

	public UIGM_Button_Operate btn_ResetSkill;

	public GComboBox comboBox_Card;

	public UIGM_Button_Operate btn_AddCard;

	public UIGM_Button_Operate btn_RemoveCard;

	public GComboBox comboBox_Relic;

	public UIGM_Button_Operate btn_Relic;

	public UIGM_Button_Operate btn_Perform;

	public GTextInput txtField_perform;

	public UIGM_Com_RandomSkin com_Random;

	public UIGM_Button_Operate btn_GameOver;

	public UIGM_Com_SetAttr com_GameProgress;

	public UIGM_Com_SetAttr com_GameAFK;

	public UIGM_Com_SinglePlayer com_SinglePlayer;

	public UIGM_Com_Target com_Target;

	public GList list_Tab;

	public UIGM_Button_Operate btn_Together;

	public GTextInput txtField_nodeId;

	public UIGM_Button_Operate btn_Test1;

	public UIGM_Button_Operate btn_Test2;

	public UIGM_Button_Operate btn_Test3;

	public UIGM_Button_Operate btn_Test4;

	public UIGM_Button_Operate btn_Test5;

	public UIGM_Button_Operate btn_Test6;

	public UIGM_Button_Operate btn_Test7;

	public UIGM_Button_Operate btn_Test8;

	public UIGM_Button_Operate btn_Test9;

	public UIGM_Button_Operate btn_Close;

	public UIGM_Button_SingleContent btn_Tutorial;

	public UIGM_Button_SingleContent btn_Tutorial1001;

	public UIGM_Button_SingleContent btn_Tutorial1002;

	public UIGM_Button_SingleContent btn_TutorialSingle;

	public UIGM_Button_Operate btn_AllItem;

	public UIGM_Button_Operate btn_Item;

	public GTextInput txtField_item;

	public UIGM_Button_Operate btn_Quick;

	public UIGM_Button_Operate btn_Gacha;

	public GTextInput txtField_Gacha;

	public UIGM_Button_Operate btn_Exp;

	public GTextInput txtField_Exp;

	public GTextInput txtField_AltArtCard;

	public UIGM_Button_Operate btn_AltArtCard;

	public UIGM_Com_Replay com_Replay;

	public UIGM_Com_SetAttr com_Credit;

	public UIGM_Com_SetAttr com_CreditAction;

	public const string URL = "ui://725vhs9yv5as0";

	public static UIGMWindow CreateInstance()
	{
		BindAll();
		return (UIGMWindow)UIPackage.CreateObject("GM", "GMWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://725vhs9y7bvqv", typeof(UIGM_Com_RandomSkin));
		UIObjectFactory.SetPackageItemExtension("ui://725vhs9y7g9qx", typeof(UIGM_Com_Replay));
		UIObjectFactory.SetPackageItemExtension("ui://725vhs9yfcst2", typeof(UIGM_Com_SetContent));
		UIObjectFactory.SetPackageItemExtension("ui://725vhs9yl4b28", typeof(UIGM_Button_Single));
		UIObjectFactory.SetPackageItemExtension("ui://725vhs9ylagsq", typeof(UIGM_Button_Operate));
		UIObjectFactory.SetPackageItemExtension("ui://725vhs9ylmo03", typeof(UIGM_Com_SetAttr));
		UIObjectFactory.SetPackageItemExtension("ui://725vhs9ylmo05", typeof(UIComboBox1_popup));
		UIObjectFactory.SetPackageItemExtension("ui://725vhs9yo11dt", typeof(UIGM_Button_Server));
		UIObjectFactory.SetPackageItemExtension("ui://725vhs9ypmdy1", typeof(UIGM_Button_SingleContent));
		UIObjectFactory.SetPackageItemExtension("ui://725vhs9yq2uxw", typeof(UIGM_Com_Set_Attr_Buff));
		UIObjectFactory.SetPackageItemExtension("ui://725vhs9yulwvr", typeof(UIGM_Com_SetAttr_1));
		UIObjectFactory.SetPackageItemExtension("ui://725vhs9yulwvs", typeof(UIGM_Com_Target));
		UIObjectFactory.SetPackageItemExtension("ui://725vhs9yv5as0", typeof(UIGMWindow));
		UIObjectFactory.SetPackageItemExtension("ui://725vhs9yvvbqu", typeof(UIGM_Com_SinglePlayer));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tab = GetControllerAt(0);
		ShowServer = GetControllerAt(1);
		com_Move = (UIGM_Com_SetContent)GetChildAt(1);
		com_MoveAgain = (UIGM_Com_SetContent)GetChildAt(2);
		com_AtkPoint = (UIGM_Com_SetContent)GetChildAt(3);
		com_DefPoint = (UIGM_Com_SetContent)GetChildAt(4);
		com_BombPoint = (UIGM_Com_SetContent)GetChildAt(5);
		btn_ReSet = (UIGM_Button_Operate)GetChildAt(6);
		btn_ClinetAI = (UIGM_Button_SingleContent)GetChildAt(8);
		btn_ClinetAFK = (UIGM_Button_SingleContent)GetChildAt(11);
		txt_IP = (GTextField)GetChildAt(14);
		txt_Player = (GTextField)GetChildAt(15);
		txt_ServerTimer = (GTextField)GetChildAt(18);
		Input_Server = (GTextInput)GetChildAt(21);
		list_Server = (GList)GetChildAt(22);
		btn_ChangeIP = (UIGM_Button_Operate)GetChildAt(23);
		group_Login = (GGroup)GetChildAt(24);
		comboBox_Event = (GComboBox)GetChildAt(26);
		btn_Event = (UIGM_Button_Operate)GetChildAt(27);
		comboBox_Destiny = (GComboBox)GetChildAt(28);
		btn_Destiny = (UIGM_Button_Operate)GetChildAt(29);
		comboBox_Divination = (GComboBox)GetChildAt(30);
		btn_Divination = (UIGM_Button_Operate)GetChildAt(31);
		txtField_terms = (GTextInput)GetChildAt(33);
		btn_SetTerms = (UIGM_Button_Operate)GetChildAt(34);
		comboBox_MapDifficulty = (GComboBox)GetChildAt(35);
		btn_SetMapDifficulty = (UIGM_Button_Operate)GetChildAt(36);
		btn_OpenLandId = (UIGM_Button_Operate)GetChildAt(39);
		btn_CloseLandId = (UIGM_Button_Operate)GetChildAt(40);
		btn_OpenFreeCamera = (UIGM_Button_Operate)GetChildAt(43);
		btn_CloseFreeCamera = (UIGM_Button_Operate)GetChildAt(44);
		btn_UnlockSportInfo = (UIGM_Button_Operate)GetChildAt(46);
		btn_UnlockRoleInfo = (UIGM_Button_Operate)GetChildAt(47);
		com_HP = (UIGM_Com_SetAttr)GetChildAt(49);
		com_Gold = (UIGM_Com_SetAttr)GetChildAt(50);
		com_RelicCount = (UIGM_Com_SetAttr)GetChildAt(51);
		btn_ResetSkill = (UIGM_Button_Operate)GetChildAt(52);
		comboBox_Card = (GComboBox)GetChildAt(54);
		btn_AddCard = (UIGM_Button_Operate)GetChildAt(55);
		btn_RemoveCard = (UIGM_Button_Operate)GetChildAt(56);
		comboBox_Relic = (GComboBox)GetChildAt(57);
		btn_Relic = (UIGM_Button_Operate)GetChildAt(58);
		btn_Perform = (UIGM_Button_Operate)GetChildAt(60);
		txtField_perform = (GTextInput)GetChildAt(62);
		com_Random = (UIGM_Com_RandomSkin)GetChildAt(63);
		btn_GameOver = (UIGM_Button_Operate)GetChildAt(65);
		com_GameProgress = (UIGM_Com_SetAttr)GetChildAt(66);
		com_GameAFK = (UIGM_Com_SetAttr)GetChildAt(67);
		com_SinglePlayer = (UIGM_Com_SinglePlayer)GetChildAt(69);
		com_Target = (UIGM_Com_Target)GetChildAt(70);
		list_Tab = (GList)GetChildAt(71);
		btn_Together = (UIGM_Button_Operate)GetChildAt(72);
		txtField_nodeId = (GTextInput)GetChildAt(74);
		btn_Test1 = (UIGM_Button_Operate)GetChildAt(75);
		btn_Test2 = (UIGM_Button_Operate)GetChildAt(76);
		btn_Test3 = (UIGM_Button_Operate)GetChildAt(77);
		btn_Test4 = (UIGM_Button_Operate)GetChildAt(78);
		btn_Test5 = (UIGM_Button_Operate)GetChildAt(79);
		btn_Test6 = (UIGM_Button_Operate)GetChildAt(80);
		btn_Test7 = (UIGM_Button_Operate)GetChildAt(81);
		btn_Test8 = (UIGM_Button_Operate)GetChildAt(82);
		btn_Test9 = (UIGM_Button_Operate)GetChildAt(83);
		btn_Close = (UIGM_Button_Operate)GetChildAt(86);
		btn_Tutorial = (UIGM_Button_SingleContent)GetChildAt(88);
		btn_Tutorial1001 = (UIGM_Button_SingleContent)GetChildAt(91);
		btn_Tutorial1002 = (UIGM_Button_SingleContent)GetChildAt(94);
		btn_TutorialSingle = (UIGM_Button_SingleContent)GetChildAt(97);
		btn_AllItem = (UIGM_Button_Operate)GetChildAt(100);
		btn_Item = (UIGM_Button_Operate)GetChildAt(101);
		txtField_item = (GTextInput)GetChildAt(103);
		btn_Quick = (UIGM_Button_Operate)GetChildAt(104);
		btn_Gacha = (UIGM_Button_Operate)GetChildAt(105);
		txtField_Gacha = (GTextInput)GetChildAt(107);
		btn_Exp = (UIGM_Button_Operate)GetChildAt(108);
		txtField_Exp = (GTextInput)GetChildAt(110);
		txtField_AltArtCard = (GTextInput)GetChildAt(112);
		btn_AltArtCard = (UIGM_Button_Operate)GetChildAt(113);
		com_Replay = (UIGM_Com_Replay)GetChildAt(115);
		com_Credit = (UIGM_Com_SetAttr)GetChildAt(116);
		com_CreditAction = (UIGM_Com_SetAttr)GetChildAt(117);
	}
}
