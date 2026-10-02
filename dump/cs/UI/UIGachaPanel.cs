using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGachaPanel : GComponent
{
	public Controller showTime;

	public Controller gachaProcess;

	public Controller poolUIStatus;

	public Controller poolType;

	public GButton btn_Return;

	public GList list_PoolButton;

	public GButton btn_Excharge;

	public GLoader loader_Pool;

	public GTextField txt_PoolTitile;

	public GTextField txt_Desc;

	public GButton btn_Info;

	public GButton btn_Record;

	public UIGacha_Com_UpDesc com_UpDesc;

	public GGraph loader_UpAnimation;

	public GTextField txt_timeTip;

	public UICacha_Button_Gacha btn_GachaOne;

	public UICacha_Button_Gacha btn_GachaMulti;

	public GTextField txt_rookieGachaCount;

	public GTextField txt_rookieGachaProcess;

	public UICacha_Button_RookieReward btn_rookieReward;

	public UICacha_Button_RookieGacha btn_GachaMulti_Rookie;

	public GList list_GachaType;

	public UIGacha_Skin com_Skin;

	public UIGacha_Skin_Replica com_Skin_Replica;

	public GComponent mohu;

	public UIGacha_Com_Result com_Result;

	public Transition Cut_in;

	public Transition GachaCut_in;

	public const string URL = "ui://j90wpcmng6uw0";

	public static UIGachaPanel CreateInstance()
	{
		BindAll();
		return (UIGachaPanel)UIPackage.CreateObject("Gacha", "GachaPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://j90wpcmn11biq21", typeof(UIGacha_Com_UpDesc));
		UIObjectFactory.SetPackageItemExtension("ui://j90wpcmnabqa19", typeof(UIGacha_Com_Result));
		UIObjectFactory.SetPackageItemExtension("ui://j90wpcmnabqa1a", typeof(UIGacha_Button_ResultItem));
		UIObjectFactory.SetPackageItemExtension("ui://j90wpcmndmntqq2r", typeof(UIGacha_Button_TabSelect));
		UIObjectFactory.SetPackageItemExtension("ui://j90wpcmnf9snqq2x", typeof(UICacha_Button_RookieGacha));
		UIObjectFactory.SetPackageItemExtension("ui://j90wpcmnfs8hqq2b", typeof(UICacha_Button_Gacha2));
		UIObjectFactory.SetPackageItemExtension("ui://j90wpcmnfs8hqq2d", typeof(UIGacha_Skin));
		UIObjectFactory.SetPackageItemExtension("ui://j90wpcmnfs8hqq2g", typeof(UIProgress_Reward));
		UIObjectFactory.SetPackageItemExtension("ui://j90wpcmnfs8hqq2l", typeof(UIGacha_Button_SliderItem));
		UIObjectFactory.SetPackageItemExtension("ui://j90wpcmng6uw0", typeof(UIGachaPanel));
		UIObjectFactory.SetPackageItemExtension("ui://j90wpcmng85eqq2w", typeof(UICacha_Button_RookieReward));
		UIObjectFactory.SetPackageItemExtension("ui://j90wpcmni82zqq37", typeof(UIGacha_Button_SupportPackage));
		UIObjectFactory.SetPackageItemExtension("ui://j90wpcmni82zqq39", typeof(UIGacha_ProgressTab));
		UIObjectFactory.SetPackageItemExtension("ui://j90wpcmni82zqq3c", typeof(UIGacha_Button_Item));
		UIObjectFactory.SetPackageItemExtension("ui://j90wpcmnqzg81", typeof(UICacha_Button_Pool));
		UIObjectFactory.SetPackageItemExtension("ui://j90wpcmnvjqjqq3k", typeof(UIGacha_Skin_Replica));
		UIObjectFactory.SetPackageItemExtension("ui://j90wpcmnvjqjqq3l", typeof(UIGacha_Com_Replica));
		UIObjectFactory.SetPackageItemExtension("ui://j90wpcmnvjqjqq3m", typeof(UIGacha_Com_UpReplica));
		UIObjectFactory.SetPackageItemExtension("ui://j90wpcmnvpjm7", typeof(UICacha_Button_Gacha));
		UIObjectFactory.SetPackageItemExtension("ui://j90wpcmnw6jfqq2t", typeof(UIGacha_Com_Arrow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showTime = GetControllerAt(0);
		gachaProcess = GetControllerAt(1);
		poolUIStatus = GetControllerAt(2);
		poolType = GetControllerAt(3);
		btn_Return = (GButton)GetChildAt(0);
		list_PoolButton = (GList)GetChildAt(3);
		btn_Excharge = (GButton)GetChildAt(4);
		loader_Pool = (GLoader)GetChildAt(5);
		txt_PoolTitile = (GTextField)GetChildAt(6);
		txt_Desc = (GTextField)GetChildAt(7);
		btn_Info = (GButton)GetChildAt(8);
		btn_Record = (GButton)GetChildAt(9);
		com_UpDesc = (UIGacha_Com_UpDesc)GetChildAt(10);
		loader_UpAnimation = (GGraph)GetChildAt(11);
		txt_timeTip = (GTextField)GetChildAt(13);
		btn_GachaOne = (UICacha_Button_Gacha)GetChildAt(15);
		btn_GachaMulti = (UICacha_Button_Gacha)GetChildAt(16);
		txt_rookieGachaCount = (GTextField)GetChildAt(18);
		txt_rookieGachaProcess = (GTextField)GetChildAt(20);
		btn_rookieReward = (UICacha_Button_RookieReward)GetChildAt(21);
		btn_GachaMulti_Rookie = (UICacha_Button_RookieGacha)GetChildAt(22);
		list_GachaType = (GList)GetChildAt(24);
		com_Skin = (UIGacha_Skin)GetChildAt(25);
		com_Skin_Replica = (UIGacha_Skin_Replica)GetChildAt(26);
		mohu = (GComponent)GetChildAt(28);
		com_Result = (UIGacha_Com_Result)GetChildAt(29);
		Cut_in = GetTransitionAt(0);
		GachaCut_in = GetTransitionAt(1);
	}
}
