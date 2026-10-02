using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHeroPanel : GComponent
{
	public Controller MaterialType;

	public Controller tab;

	public Controller standingPaintType;

	public GLoader loader_Skin_Image;

	public GGraph loader_Skin_Video;

	public GGraph loader_Effect;

	public GButton btn_Preview;

	public GTextField txt_Nick;

	public GTextField txt_Name;

	public UIHero_Com_Progress group_Progress;

	public UIHero_bar_FavorValue slider_Favor;

	public GButton btn_Return;

	public GButton btn_prePage;

	public GButton btn_nextPage;

	public GButton btn_GiftTab;

	public GGraph com_MaskGiftTab;

	public UIHero_Com_Selector com_HeroList;

	public GTextField txt_CharacterProgress;

	public GButton btn_Detail;

	public UIHero_Com_ComboBox com_HeroSort;

	public UIHero_Btn_Sort Btn_sort;

	public GGroup group_Selector;

	public UIHero_Com_File com_File;

	public UIHero_Com_Gift com_Gift;

	public UIHero_Com_Skin com_Skin;

	public UIHero_Com_Emoji com_Emoji;

	public GGroup group_Detail;

	public GGroup group_Character;

	public UIHero_Button_BreakThrough btn_BreakThroughShow;

	public GGraph com_MaskHero;

	public Transition Cutin;

	public Transition HeartLvUP;

	public Transition Heart;

	public Transition SwitchCutin;

	public const string URL = "ui://7qkd4lqxg1lk0";

	public static UIHeroPanel CreateInstance()
	{
		BindAll();
		return (UIHeroPanel)UIPackage.CreateObject("Hero", "HeroPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxat01q33", typeof(UIHero_Com_Selector));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxat01q34", typeof(UIHero_Com_SkinSelector));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxbfwlq3i", typeof(UIHero_Com_Level));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxbfwlq3o", typeof(UIHero_Progress_PVEExpNew));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxbfwlq3q", typeof(UIHero_Com_FileBreakThroughDesc));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxbfwlq3t", typeof(UIHero_Com_TalentItem));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxbfwlq3v", typeof(UIHero_Com_FileArchive));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxbfwlq3w", typeof(UIHero_Com_InfoArchiveContentDesc));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxg1lk0", typeof(UIHeroPanel));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxg1lk18", typeof(UIHero_Com_BreakThrough));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxg1lk19", typeof(UIHero_Com_Skin));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxg1lk1a", typeof(UIHero_Com_Expression));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxg1lk1c", typeof(UIHero_Com_Progress));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxg1lk7", typeof(UIHero_bar_FavorValue));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxg1lkb", typeof(UIHero_Button_Hero));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxg1lkm", typeof(UIHero_Com_File));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxg1lkn", typeof(UIHero_Com_Gift));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxg1lkq", typeof(UIHero_Com_Relation));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxg1lkv", typeof(UIHero_Button_GiftItem));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxjhclq4c", typeof(UIHero_com_show));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxjhclq4i", typeof(UIHero_Com_ComboBox));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxjhclq4l", typeof(UIHero_Com_ComboBox_popup));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxjhclq4n", typeof(UIHero_Btn_Sort));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxkp9fq36", typeof(UIHero_Com_FileSkill));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxkp9fq37", typeof(UIHero_Com_FileUpgrade));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxkp9fq38", typeof(UIHero_Button_FileSelect));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxkp9fq39", typeof(UIHero_Com_FileBreakThrough));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxkqgjq2f", typeof(UIHero_Com_Skill));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxl6o6q1s", typeof(UIHero_Button_GiftWay));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxot0wq29", typeof(UIHero_Com_PveLvDesc));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxot0wq2b", typeof(UIHero_Button_UpgradeOperate));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxot0wq2d", typeof(UIHero_Com_PveDrop));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxq93cq2s", typeof(UIHero_Com_Emoji));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxq93cq2t", typeof(UIHero_Button_SkinItem));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxq93cq2u", typeof(UIHero_Com_SkinImage));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxq93cq2w", typeof(UIHero_Com_HeroItem));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxx2xxq1x", typeof(UIHero_Button_SwitchMode));
		UIObjectFactory.SetPackageItemExtension("ui://7qkd4lqxyhn0q1l", typeof(UIHero_Button_BreakThrough));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		MaterialType = GetControllerAt(0);
		tab = GetControllerAt(1);
		standingPaintType = GetControllerAt(2);
		loader_Skin_Image = (GLoader)GetChildAt(2);
		loader_Skin_Video = (GGraph)GetChildAt(3);
		loader_Effect = (GGraph)GetChildAt(4);
		btn_Preview = (GButton)GetChildAt(5);
		txt_Nick = (GTextField)GetChildAt(7);
		txt_Name = (GTextField)GetChildAt(8);
		group_Progress = (UIHero_Com_Progress)GetChildAt(9);
		slider_Favor = (UIHero_bar_FavorValue)GetChildAt(10);
		btn_Return = (GButton)GetChildAt(12);
		btn_prePage = (GButton)GetChildAt(13);
		btn_nextPage = (GButton)GetChildAt(14);
		btn_GiftTab = (GButton)GetChildAt(19);
		com_MaskGiftTab = (GGraph)GetChildAt(20);
		com_HeroList = (UIHero_Com_Selector)GetChildAt(23);
		txt_CharacterProgress = (GTextField)GetChildAt(24);
		btn_Detail = (GButton)GetChildAt(25);
		com_HeroSort = (UIHero_Com_ComboBox)GetChildAt(26);
		Btn_sort = (UIHero_Btn_Sort)GetChildAt(27);
		group_Selector = (GGroup)GetChildAt(28);
		com_File = (UIHero_Com_File)GetChildAt(29);
		com_Gift = (UIHero_Com_Gift)GetChildAt(30);
		com_Skin = (UIHero_Com_Skin)GetChildAt(31);
		com_Emoji = (UIHero_Com_Emoji)GetChildAt(32);
		group_Detail = (GGroup)GetChildAt(33);
		group_Character = (GGroup)GetChildAt(34);
		btn_BreakThroughShow = (UIHero_Button_BreakThrough)GetChildAt(35);
		com_MaskHero = (GGraph)GetChildAt(36);
		Cutin = GetTransitionAt(0);
		HeartLvUP = GetTransitionAt(1);
		Heart = GetTransitionAt(2);
		SwitchCutin = GetTransitionAt(3);
	}
}
