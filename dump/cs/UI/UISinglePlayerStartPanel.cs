using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayerStartPanel : GComponent
{
	public Controller state;

	public Controller type;

	public UISinglePlayer_Button_LevelTitle btn_title;

	public UISinglePlayer_Button_Level btn_level_1;

	public UISinglePlayer_Button_Level btn_level_2;

	public UISinglePlayer_Button_Level btn_level_3;

	public UISinglePlayer_Button_LevelTitle btn_lock;

	public UISinglePlayer_Button_Pack btn_pack;

	public GButton btn_exit;

	public UISinglePlayer_Com_SelectTag com_selectTag;

	public UISinglePlayer_Button_Confirm btn_Continue;

	public UISinglePlayer_Button_Confirm btn_Start;

	public UISinglePlayer_Button_Confirm btn_Reset;

	public UISinglePlayer_Button_Confirm btn_book;

	public UISinglePlayer_Button_Confirm btn_FriendRankList;

	public UISinglePlayer_Button_Confirm btn_Back;

	public GGroup BtnGroups;

	public UISinglePlayer_Com_Book com_book;

	public Transition Cut_in;

	public const string URL = "ui://xsairahjpmrnq8k";

	public static UISinglePlayerStartPanel CreateInstance()
	{
		BindAll();
		return (UISinglePlayerStartPanel)UIPackage.CreateObject("SinglePlayerStart", "SinglePlayerStartPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://xsairahjhh8oq96", typeof(UISinglePlayer_Com_Book));
		UIObjectFactory.SetPackageItemExtension("ui://xsairahjhh8oq9v", typeof(UISinglePlayer_Button_LevelTitle));
		UIObjectFactory.SetPackageItemExtension("ui://xsairahjhh8oq9w", typeof(UISinglePlayer_Button_Level));
		UIObjectFactory.SetPackageItemExtension("ui://xsairahjhh8oq9y", typeof(UISinglePlayer_Button_BookBack));
		UIObjectFactory.SetPackageItemExtension("ui://xsairahjhh8oq9z", typeof(UISinglePlayer_Button_Type));
		UIObjectFactory.SetPackageItemExtension("ui://xsairahjhh8oqa0", typeof(UISinglePlayer_Button_Item));
		UIObjectFactory.SetPackageItemExtension("ui://xsairahjhh8oqa6", typeof(UISinglePlayer_Button_BookTag));
		UIObjectFactory.SetPackageItemExtension("ui://xsairahjhh8oqa7", typeof(UISinglePlayer_Button_Arrow));
		UIObjectFactory.SetPackageItemExtension("ui://xsairahjhh8oqa8", typeof(UISinglePlayer_Button_Dropdown));
		UIObjectFactory.SetPackageItemExtension("ui://xsairahjhh8oqa9", typeof(UISinglePlayer_Button_ItemGrade));
		UIObjectFactory.SetPackageItemExtension("ui://xsairahjpmrnq8k", typeof(UISinglePlayerStartPanel));
		UIObjectFactory.SetPackageItemExtension("ui://xsairahjpmrnq8n", typeof(UISinglePlayer_Button_Confirm));
		UIObjectFactory.SetPackageItemExtension("ui://xsairahjti1tq8o", typeof(UISinglePlayer_Button_Tag));
		UIObjectFactory.SetPackageItemExtension("ui://xsairahjti1tq8p", typeof(UISinglePlayer_Com_SelectTag));
		UIObjectFactory.SetPackageItemExtension("ui://xsairahjti1tq94", typeof(UISinglePlayer_Button_General));
		UIObjectFactory.SetPackageItemExtension("ui://xsairahjti1tq95", typeof(UISinglePlayer_Button_Pack));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(0);
		type = GetControllerAt(1);
		btn_title = (UISinglePlayer_Button_LevelTitle)GetChildAt(2);
		btn_level_1 = (UISinglePlayer_Button_Level)GetChildAt(3);
		btn_level_2 = (UISinglePlayer_Button_Level)GetChildAt(4);
		btn_level_3 = (UISinglePlayer_Button_Level)GetChildAt(5);
		btn_lock = (UISinglePlayer_Button_LevelTitle)GetChildAt(6);
		btn_pack = (UISinglePlayer_Button_Pack)GetChildAt(7);
		btn_exit = (GButton)GetChildAt(9);
		com_selectTag = (UISinglePlayer_Com_SelectTag)GetChildAt(10);
		btn_Continue = (UISinglePlayer_Button_Confirm)GetChildAt(11);
		btn_Start = (UISinglePlayer_Button_Confirm)GetChildAt(12);
		btn_Reset = (UISinglePlayer_Button_Confirm)GetChildAt(13);
		btn_book = (UISinglePlayer_Button_Confirm)GetChildAt(14);
		btn_FriendRankList = (UISinglePlayer_Button_Confirm)GetChildAt(15);
		btn_Back = (UISinglePlayer_Button_Confirm)GetChildAt(16);
		BtnGroups = (GGroup)GetChildAt(17);
		com_book = (UISinglePlayer_Com_Book)GetChildAt(18);
		Cut_in = GetTransitionAt(0);
	}
}
