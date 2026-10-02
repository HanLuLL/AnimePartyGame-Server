using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRoomHeroPanel : GComponent
{
	public Controller step;

	public GGraph loader_ReadyAnime;

	public GTextField txt_Explain;

	public UIRoomHero_Com_Player com_player_4;

	public UIRoomHero_Com_Player com_player_3;

	public UIRoomHero_Com_Player com_player_2;

	public UIRoomHero_Com_Player com_player_1;

	public GProgressBar progress_OperationTime;

	public GList list_RendererSelectRole;

	public GList List_SelectSkin;

	public UIRoomHero_Button_Sure btn_SureHero;

	public UIRoomHero_Button_Sure btn_SureSkin;

	public GButton btn_Up;

	public GButton btn_Down;

	public GButton btn_terms;

	public Transition StartLoad;

	public Transition CutIn;

	public Transition CTCut_in_;

	public Transition CTCut_out;

	public const string URL = "ui://l82hrmsqqzg812";

	public static UIRoomHeroPanel CreateInstance()
	{
		BindAll();
		return (UIRoomHeroPanel)UIPackage.CreateObject("RoomHero", "RoomHeroPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://l82hrmsqdx5u2l", typeof(UIRoomHero_Com_SportsMedal));
		UIObjectFactory.SetPackageItemExtension("ui://l82hrmsqdx5u2n", typeof(UIRoomHero_Com_BubbleTip));
		UIObjectFactory.SetPackageItemExtension("ui://l82hrmsqdx5u31", typeof(UIRoomHero_ListItem_Medal));
		UIObjectFactory.SetPackageItemExtension("ui://l82hrmsqdx5u32", typeof(UIRoomHero_Com_MedalTip));
		UIObjectFactory.SetPackageItemExtension("ui://l82hrmsqec7w1u", typeof(UIRoomHero_Com_SkinItem));
		UIObjectFactory.SetPackageItemExtension("ui://l82hrmsqec7w1v", typeof(UIRoomHero_Button_SelectSkin));
		UIObjectFactory.SetPackageItemExtension("ui://l82hrmsqhzcu2g", typeof(UIRoomHero_Com_ApplyChangeSlot));
		UIObjectFactory.SetPackageItemExtension("ui://l82hrmsqhzcu2h", typeof(UIRoomHero_Com_ReceiveChangeSlot));
		UIObjectFactory.SetPackageItemExtension("ui://l82hrmsqhzcu2i", typeof(UIRoomHero_Com_ChangeSlot));
		UIObjectFactory.SetPackageItemExtension("ui://l82hrmsqiorl1a", typeof(UIRoomHero_Com_SelectHero));
		UIObjectFactory.SetPackageItemExtension("ui://l82hrmsqkqgj1h", typeof(UIRoomHero_Com_Skill));
		UIObjectFactory.SetPackageItemExtension("ui://l82hrmsqm49i1c", typeof(UIRoomHero_Button_SelectHero_2));
		UIObjectFactory.SetPackageItemExtension("ui://l82hrmsqqzg812", typeof(UIRoomHeroPanel));
		UIObjectFactory.SetPackageItemExtension("ui://l82hrmsqqzg8g", typeof(UIRoomHero_Com_Player));
		UIObjectFactory.SetPackageItemExtension("ui://l82hrmsqqzg8l", typeof(UIRoomHero_Button_DetailInfo));
		UIObjectFactory.SetPackageItemExtension("ui://l82hrmsqqzg8t", typeof(UIRoomHero_CharacterDi));
		UIObjectFactory.SetPackageItemExtension("ui://l82hrmsqqzg8w", typeof(UIRoomHero_Button_Sure));
		UIObjectFactory.SetPackageItemExtension("ui://l82hrmsqtcj01k", typeof(UIRoomHero_Com_CharacterName));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		step = GetControllerAt(0);
		loader_ReadyAnime = (GGraph)GetChildAt(1);
		txt_Explain = (GTextField)GetChildAt(2);
		com_player_4 = (UIRoomHero_Com_Player)GetChildAt(3);
		com_player_3 = (UIRoomHero_Com_Player)GetChildAt(4);
		com_player_2 = (UIRoomHero_Com_Player)GetChildAt(5);
		com_player_1 = (UIRoomHero_Com_Player)GetChildAt(6);
		progress_OperationTime = (GProgressBar)GetChildAt(8);
		list_RendererSelectRole = (GList)GetChildAt(9);
		List_SelectSkin = (GList)GetChildAt(10);
		btn_SureHero = (UIRoomHero_Button_Sure)GetChildAt(11);
		btn_SureSkin = (UIRoomHero_Button_Sure)GetChildAt(12);
		btn_Up = (GButton)GetChildAt(13);
		btn_Down = (GButton)GetChildAt(14);
		btn_terms = (GButton)GetChildAt(15);
		StartLoad = GetTransitionAt(0);
		CutIn = GetTransitionAt(1);
		CTCut_in_ = GetTransitionAt(2);
		CTCut_out = GetTransitionAt(3);
	}
}
