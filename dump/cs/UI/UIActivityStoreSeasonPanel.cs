using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreSeasonPanel : GComponent
{
	public Controller tab;

	public Controller showResult;

	public GLoader Loader_BG;

	public UIActivityStoreSeason_Com_Sport com_Sport;

	public UIActivityStoreSeason_Com_Task com_Task;

	public UIActivityStoreSeason_Com_Store com_Store;

	public UIActivityStoreSeason_Com_Gacha com_Gacha;

	public GList list_Tabs;

	public GGraph graph_GachaResultMask;

	public GComponent mohu;

	public GButton btn_Gacha_Confirm;

	public GList list_Gacha_Result;

	public GGroup group_GahcaResult;

	public GButton btn_Return;

	public UIActivityStoreSeason_Com_SportHeroInfo com_SportHeroInfo;

	public Transition Cut_in;

	public const string URL = "ui://begz6gfv7vwm0";

	public static UIActivityStoreSeasonPanel CreateInstance()
	{
		BindAll();
		return (UIActivityStoreSeasonPanel)UIPackage.CreateObject("ActivityStoreSeason", "ActivityStoreSeasonPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://begz6gfv7vwm0", typeof(UIActivityStoreSeasonPanel));
		UIObjectFactory.SetPackageItemExtension("ui://begz6gfv7vwm11", typeof(UIActivityStoreSeason_Com_ItemMask));
		UIObjectFactory.SetPackageItemExtension("ui://begz6gfv7vwm13", typeof(UIActivityStoreSeason_Com_Gacha));
		UIObjectFactory.SetPackageItemExtension("ui://begz6gfv7vwm19", typeof(UIActivityStoreSeason_Button_Gacha));
		UIObjectFactory.SetPackageItemExtension("ui://begz6gfv7vwm1c", typeof(UIActivityStoreSeason_Button_AddGift));
		UIObjectFactory.SetPackageItemExtension("ui://begz6gfv7vwm1f", typeof(UIActivityStoreSeason_Button_Item));
		UIObjectFactory.SetPackageItemExtension("ui://begz6gfv7vwmh", typeof(UIActivityStoreSeason_Com_Task));
		UIObjectFactory.SetPackageItemExtension("ui://begz6gfv7vwmp", typeof(UIActivityStoreSeason_Button_Toggle));
		UIObjectFactory.SetPackageItemExtension("ui://begz6gfv7vwmq", typeof(UIActivityStoreSeason_Com_Token));
		UIObjectFactory.SetPackageItemExtension("ui://begz6gfv7vwms", typeof(UIActivityStoreSeason_Com_Store));
		UIObjectFactory.SetPackageItemExtension("ui://begz6gfv7vwmt", typeof(UIActivityStoreSeason_Com_BottomBg));
		UIObjectFactory.SetPackageItemExtension("ui://begz6gfv7vwmw", typeof(UIActivityStoreSeason_Com_BottomMask));
		UIObjectFactory.SetPackageItemExtension("ui://begz6gfv7vwmz", typeof(UIActivityStoreSeason_Com_ItemBg));
		UIObjectFactory.SetPackageItemExtension("ui://begz6gfvok4t1j", typeof(UIActivityStoreSeason_Com_Label));
		UIObjectFactory.SetPackageItemExtension("ui://begz6gfvok4t1m", typeof(UIActivityStoreSeason_Button_TaskStatus));
		UIObjectFactory.SetPackageItemExtension("ui://begz6gfvok4t21", typeof(UIActivityStoreSeason_Button_Tab));
		UIObjectFactory.SetPackageItemExtension("ui://begz6gfvtaoa31", typeof(UIActivityStoreSeason_Com_Sport));
		UIObjectFactory.SetPackageItemExtension("ui://begz6gfvtaoa32", typeof(UIActivityStoreSeason_Button_Hero));
		UIObjectFactory.SetPackageItemExtension("ui://begz6gfvtaoa39", typeof(UIActivityStoreSeason_Button_Reward));
		UIObjectFactory.SetPackageItemExtension("ui://begz6gfvtaoa3a", typeof(UIActivityStoreSeason_Com_SportHeroInfo));
		UIObjectFactory.SetPackageItemExtension("ui://begz6gfvtaoa3b", typeof(UIActivityStoreSeason_Com_SportInfoItem));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tab = GetControllerAt(0);
		showResult = GetControllerAt(1);
		Loader_BG = (GLoader)GetChildAt(0);
		com_Sport = (UIActivityStoreSeason_Com_Sport)GetChildAt(1);
		com_Task = (UIActivityStoreSeason_Com_Task)GetChildAt(2);
		com_Store = (UIActivityStoreSeason_Com_Store)GetChildAt(3);
		com_Gacha = (UIActivityStoreSeason_Com_Gacha)GetChildAt(4);
		list_Tabs = (GList)GetChildAt(5);
		graph_GachaResultMask = (GGraph)GetChildAt(6);
		mohu = (GComponent)GetChildAt(7);
		btn_Gacha_Confirm = (GButton)GetChildAt(10);
		list_Gacha_Result = (GList)GetChildAt(11);
		group_GahcaResult = (GGroup)GetChildAt(13);
		btn_Return = (GButton)GetChildAt(14);
		com_SportHeroInfo = (UIActivityStoreSeason_Com_SportHeroInfo)GetChildAt(15);
		Cut_in = GetTransitionAt(0);
	}
}
