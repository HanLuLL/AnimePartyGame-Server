using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHomePanel : GComponent
{
	public Controller Hide;

	public Controller BGset;

	public GLoader loader_bg;

	public UIHome_Com_BGSet Com_BGSet;

	public UIHome_Button_PlayerInfo btn_PlayerGuide;

	public UIHome_Com_Banner com_Banner;

	public UIHome_Com_Activity com_Activity;

	public UIHome_Com_ExpandActivity com_expand;

	public UIHome_Com_BattlePass com_BattlePass;

	public UIHome_Com_ActivityBanner com_ActivityBanner;

	public GButton btn_DouYinReward;

	public GButton btn_DouYinGroupChat;

	public GButton btn_DouYinShareReward;

	public GButton btn_DouYinSubscribeReward;

	public GButton Btn_Hide;

	public GButton Btn_BGSet;

	public UIHome_Com_TipBGSet Com_Tip;

	public GGraph Icon;

	public UIHome_Button_CardBook btn_Dictionary;

	public UIHome_Button_Mall btn_Mall;

	public UIHome_Button_Gacha btn_Gacha;

	public UIHome_Button_Activity btn_Activity;

	public UIHome_Button_GameStart btn_GameOnline;

	public Transition Cut_in;

	public const string URL = "ui://u7xbdcguq9rv0";

	public static UIHomePanel CreateInstance()
	{
		BindAll();
		return (UIHomePanel)UIPackage.CreateObject("Home", "HomePanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://u7xbdcgu11biq2e", typeof(UIHome_Button_Gacha));
		UIObjectFactory.SetPackageItemExtension("ui://u7xbdcguejpwq5l", typeof(UIHome_Btn_BattlePass));
		UIObjectFactory.SetPackageItemExtension("ui://u7xbdcguenysq3l", typeof(UIHome_Button_ActivityBannerItem));
		UIObjectFactory.SetPackageItemExtension("ui://u7xbdcguezynq2l", typeof(UIHome_Button_Activity));
		UIObjectFactory.SetPackageItemExtension("ui://u7xbdcguheywq3n", typeof(UIHome_Com_ActivityBanner));
		UIObjectFactory.SetPackageItemExtension("ui://u7xbdcgujz24q2q", typeof(UIHome_Com_BattlePass));
		UIObjectFactory.SetPackageItemExtension("ui://u7xbdcgujz24q2u", typeof(UIHome_Com_BattlePassReward));
		UIObjectFactory.SetPackageItemExtension("ui://u7xbdcgukrvoq4f", typeof(UIHome_Com_TipBGSet));
		UIObjectFactory.SetPackageItemExtension("ui://u7xbdcgukrvoq4g", typeof(UIHero_Button_Set));
		UIObjectFactory.SetPackageItemExtension("ui://u7xbdcgumyupq5m", typeof(UIHome_Com_ExpandActivity));
		UIObjectFactory.SetPackageItemExtension("ui://u7xbdcgumyupq5o", typeof(UIHome_Button_Questionnaire));
		UIObjectFactory.SetPackageItemExtension("ui://u7xbdcgune5m1y", typeof(UIHome_Button_SpecialActivity));
		UIObjectFactory.SetPackageItemExtension("ui://u7xbdcgupgrvq4h", typeof(UIHome_Com_BGSet));
		UIObjectFactory.SetPackageItemExtension("ui://u7xbdcguq9rv0", typeof(UIHomePanel));
		UIObjectFactory.SetPackageItemExtension("ui://u7xbdcgursc721", typeof(UIHome_Com_Banner));
		UIObjectFactory.SetPackageItemExtension("ui://u7xbdcgursc722", typeof(UIHome_Button_Banner));
		UIObjectFactory.SetPackageItemExtension("ui://u7xbdcgusjg4a", typeof(UIHome_Button_GameStart));
		UIObjectFactory.SetPackageItemExtension("ui://u7xbdcgusjg4b", typeof(UIHome_Button_PlayerInfo));
		UIObjectFactory.SetPackageItemExtension("ui://u7xbdcgusjg4c", typeof(UIHome_Button_CardBook));
		UIObjectFactory.SetPackageItemExtension("ui://u7xbdcgusjg4d", typeof(UIHome_Button_Mall));
		UIObjectFactory.SetPackageItemExtension("ui://u7xbdcguwi2kq48", typeof(UIHome_Com_Activity));
		UIObjectFactory.SetPackageItemExtension("ui://u7xbdcguy9qnq5j", typeof(UIHome_Button_ComeBack));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Hide = GetControllerAt(0);
		BGset = GetControllerAt(1);
		loader_bg = (GLoader)GetChildAt(0);
		Com_BGSet = (UIHome_Com_BGSet)GetChildAt(1);
		btn_PlayerGuide = (UIHome_Button_PlayerInfo)GetChildAt(2);
		com_Banner = (UIHome_Com_Banner)GetChildAt(3);
		com_Activity = (UIHome_Com_Activity)GetChildAt(4);
		com_expand = (UIHome_Com_ExpandActivity)GetChildAt(5);
		com_BattlePass = (UIHome_Com_BattlePass)GetChildAt(6);
		com_ActivityBanner = (UIHome_Com_ActivityBanner)GetChildAt(7);
		btn_DouYinReward = (GButton)GetChildAt(8);
		btn_DouYinGroupChat = (GButton)GetChildAt(9);
		btn_DouYinShareReward = (GButton)GetChildAt(10);
		btn_DouYinSubscribeReward = (GButton)GetChildAt(11);
		Btn_Hide = (GButton)GetChildAt(12);
		Btn_BGSet = (GButton)GetChildAt(13);
		Com_Tip = (UIHome_Com_TipBGSet)GetChildAt(14);
		Icon = (GGraph)GetChildAt(15);
		btn_Dictionary = (UIHome_Button_CardBook)GetChildAt(16);
		btn_Mall = (UIHome_Button_Mall)GetChildAt(17);
		btn_Gacha = (UIHome_Button_Gacha)GetChildAt(18);
		btn_Activity = (UIHome_Button_Activity)GetChildAt(19);
		btn_GameOnline = (UIHome_Button_GameStart)GetChildAt(20);
		Cut_in = GetTransitionAt(0);
	}
}
