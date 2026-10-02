using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISettingWindow : GComponent
{
	public Controller tab;

	public Controller country;

	public Controller TransferCode;

	public GComponent gb;

	public UISetting_Button_UID btn_UID;

	public GGroup bg;

	public UISetting_Com_Screen com_Screen;

	public UISetting_Com_Volume com_Volume;

	public UISetting_Com_User com_User;

	public UISetting_Com_LiveMode com_LiveMode;

	public UISetting_Com_Score com_Score;

	public GGroup tabList;

	public GGroup tab_2;

	public GButton btn_Return;

	public GLoader loader_bg_detail;

	public UISetting_Com_Detail com_Detail;

	public Transition SetCut_in;

	public Transition ShengYinCut_in;

	public Transition HuaMianCut_In;

	public Transition YongHuCut_in;

	public Transition GongNengCut_in;

	public Transition XinYuFenCut_in;

	public Transition RoleCut_in;

	public const string URL = "ui://iy1joavtwh311r";

	public static UISettingWindow CreateInstance()
	{
		BindAll();
		return (UISettingWindow)UIPackage.CreateObject("Setting", "SettingWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavthm5n27", typeof(UISetting_Comp_Score_Desc));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavto1801z", typeof(UISetting_Com_Score));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavto18025", typeof(UISetting_Com_Detail));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavto1n810", typeof(UISetting_Com_InputField));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavto1n816", typeof(UISetting_Com_Button));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavto1n81c", typeof(UISetting_Com_LiveMode));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavto1n81f", typeof(UISetting_Button_Operate));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavto1n82", typeof(UISetting_Com_Bg));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavto1n85", typeof(UISetting_Com_BottomBg));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavto1n87", typeof(UISetting_Com_Screen));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavto1n88", typeof(UISetting_Item_Buttom));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavto1n89", typeof(UISetting_Item_Bg));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavto1n8a", typeof(UISetting_Item_BottomBg));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavto1n8d", typeof(UISetting_Item_Title));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavto1n8f", typeof(UISetting_Com_ComboBox));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavto1n8i", typeof(UISetting_Com_ComboBox_popup));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavto1n8k", typeof(UISetting_Com_Camera));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavto1n8l", typeof(UISetting_Com_Toggle));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavto1n8o", typeof(UISetting_Com_EnergySaving));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavto1n8q", typeof(UISetting_Com_Volume));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavto1n8t", typeof(UISetting_Com_SliderBar));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavto1n8z", typeof(UISetting_Com_User));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavtodf71t", typeof(UISetting_Com_Vibrate));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavtodf71y", typeof(UISetting_Grip));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavtp5xi1s", typeof(UISetting_Button_UID));
		UIObjectFactory.SetPackageItemExtension("ui://iy1joavtwh311r", typeof(UISettingWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tab = GetControllerAt(0);
		country = GetControllerAt(1);
		TransferCode = GetControllerAt(2);
		gb = (GComponent)GetChildAt(0);
		btn_UID = (UISetting_Button_UID)GetChildAt(1);
		bg = (GGroup)GetChildAt(12);
		com_Screen = (UISetting_Com_Screen)GetChildAt(13);
		com_Volume = (UISetting_Com_Volume)GetChildAt(14);
		com_User = (UISetting_Com_User)GetChildAt(15);
		com_LiveMode = (UISetting_Com_LiveMode)GetChildAt(16);
		com_Score = (UISetting_Com_Score)GetChildAt(17);
		tabList = (GGroup)GetChildAt(25);
		tab_2 = (GGroup)GetChildAt(26);
		btn_Return = (GButton)GetChildAt(28);
		loader_bg_detail = (GLoader)GetChildAt(29);
		com_Detail = (UISetting_Com_Detail)GetChildAt(30);
		SetCut_in = GetTransitionAt(0);
		ShengYinCut_in = GetTransitionAt(1);
		HuaMianCut_In = GetTransitionAt(2);
		YongHuCut_in = GetTransitionAt(3);
		GongNengCut_in = GetTransitionAt(4);
		XinYuFenCut_in = GetTransitionAt(5);
		RoleCut_in = GetTransitionAt(6);
	}
}
