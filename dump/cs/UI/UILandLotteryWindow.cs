using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandLotteryWindow : GComponent
{
	public Controller lotteryResult;

	public Controller stage;

	public UILandLottery_Com_SelectLottery com_Select;

	public GGraph turntableEffect;

	public UILandLottery_Com_Turntable com_lotteryTurntable;

	public GList list_Winners;

	public GTextField txt_LotteryReward;

	public GGraph loader_VictoryEffect;

	public GGraph loader_GoldEffect;

	public GLoader loader_Player_1;

	public GTextField txt_hasLottery_1;

	public GLoader loader_Player_2;

	public GTextField txt_hasLottery_2;

	public GLoader loader_Player_3;

	public GTextField txt_hasLottery_3;

	public GLoader loader_Player_4;

	public GTextField txt_hasLottery_4;

	public Transition cutInResult;

	public const string URL = "ui://d6gnxdx1rum70";

	public static UILandLotteryWindow CreateInstance()
	{
		BindAll();
		return (UILandLotteryWindow)UIPackage.CreateObject("LandLottery", "LandLotteryWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://d6gnxdx1hmjfx", typeof(UILandLottery_Com_Number));
		UIObjectFactory.SetPackageItemExtension("ui://d6gnxdx1qzg8y", typeof(UILandLottery_Com_SelectLottery));
		UIObjectFactory.SetPackageItemExtension("ui://d6gnxdx1rum70", typeof(UILandLotteryWindow));
		UIObjectFactory.SetPackageItemExtension("ui://d6gnxdx1rum72", typeof(UILandLottery_Button_LotteryNumb));
		UIObjectFactory.SetPackageItemExtension("ui://d6gnxdx1rum7i", typeof(UILandLottery_Button_Lottery_Selection));
		UIObjectFactory.SetPackageItemExtension("ui://d6gnxdx1rum7o", typeof(UILandLottery_Com_Turntable));
		UIObjectFactory.SetPackageItemExtension("ui://d6gnxdx1rum7r", typeof(UILandLottery_Com_Cheer));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		lotteryResult = GetControllerAt(0);
		stage = GetControllerAt(1);
		com_Select = (UILandLottery_Com_SelectLottery)GetChildAt(1);
		turntableEffect = (GGraph)GetChildAt(2);
		com_lotteryTurntable = (UILandLottery_Com_Turntable)GetChildAt(13);
		list_Winners = (GList)GetChildAt(15);
		txt_LotteryReward = (GTextField)GetChildAt(21);
		loader_VictoryEffect = (GGraph)GetChildAt(23);
		loader_GoldEffect = (GGraph)GetChildAt(24);
		loader_Player_1 = (GLoader)GetChildAt(27);
		txt_hasLottery_1 = (GTextField)GetChildAt(28);
		loader_Player_2 = (GLoader)GetChildAt(30);
		txt_hasLottery_2 = (GTextField)GetChildAt(31);
		loader_Player_3 = (GLoader)GetChildAt(33);
		txt_hasLottery_3 = (GTextField)GetChildAt(34);
		loader_Player_4 = (GLoader)GetChildAt(36);
		txt_hasLottery_4 = (GTextField)GetChildAt(37);
		cutInResult = GetTransitionAt(0);
	}
}
