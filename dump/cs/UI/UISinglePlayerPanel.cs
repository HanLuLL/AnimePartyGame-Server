using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayerPanel : GComponent
{
	public Controller showShop;

	public Controller developLand;

	public Controller RemainingDice;

	public GComponent com_AttrParent;

	public GTextField txt_Gold;

	public UISinglePlayer_Slider_Progress slider_Progress;

	public GRichTextField txt_RemainingRounds;

	public GButton btn_guide;

	public GButton btn_Back;

	public UISinglePlayer_Button_UnLockLand btn_UnLockLand;

	public GTextField txt_DevelopTip;

	public UISinglePlayer_Com_CardsProb Com_CardsProb;

	public GList list_Shop;

	public UISinglePlayer_Button_Refresh btn_RefreshStore;

	public GTextField txt_StoreGold;

	public GList list_Buff;

	public GList list_Bag;

	public UISinglePlayer_Button_Move btn_Move;

	public UISinglePlayer_Com_RewardShow com_RewardShow;

	public Transition Cut_in;

	public Transition Tips_1;

	public const string URL = "ui://mi9vm3w0oazx0";

	public static UISinglePlayerPanel CreateInstance()
	{
		BindAll();
		return (UISinglePlayerPanel)UIPackage.CreateObject("SinglePlayer", "SinglePlayerPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0ap9hc", typeof(UISinglePlayer_Button_DragArea));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0as5tq46", typeof(UISinglePlayer_Com_AttrTip));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0as5tq48", typeof(UISinglePlayer_Com_UnitAttrInfo));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0dajbq8c", typeof(UISinglePlayer_Com_LevelLine));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0dajbq8d", typeof(UISinglePlayer_Com_BuildingLevel));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0fo1bq3z", typeof(UISinglePlayer_Com_BuildingInfo));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0fx3oq71", typeof(UISinglePlayer_Com_SettleMission));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0fx3oq72", typeof(UISinglePlayer_SettleMission_Button_Card));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0jsh0q8j", typeof(UISinglePlayer_SettleMission_Item));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0kducq5g", typeof(UISinglePlayer_Com_CardTag));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0kducq5m", typeof(UISinglePlayer_Com_CardName));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0kgpqq6x", typeof(UISinglePlayer_Com_BuildingLevelSlider));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0kvexq49", typeof(UISinglePlayer_Com_DiceInfo));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0kvexq4a", typeof(UISinglePlayer_Com_Point));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0oai4q52", typeof(UISinglePlayer_Com_CardItemSlot));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0oazx0", typeof(UISinglePlayerPanel));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0oazx1", typeof(UISinglePlayer_Button_Move));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0s244q3t", typeof(UISinglePlayer_Com_CardQuality));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0tvj1q7e", typeof(UISinglePlayer_Slider_Progress));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0tvj1q7w", typeof(UISinglePlayer_Com_CardsProb));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0tvj1q7z", typeof(UISinglePlayer_Com_CardTag2));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0tvj1q80", typeof(UISinglePlayer_Com_RelicInfo));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0tvj1q81", typeof(UISinglePlayer_SettleMission_Button_RelicBuffItem));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0wcumq61", typeof(UISinglePlayer_Button_Refresh));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0wcumq62", typeof(UISinglePlayer_Button_UnLockLand));
		UIObjectFactory.SetPackageItemExtension("ui://mi9vm3w0wcumq6u", typeof(UISinglePlayer_Com_RewardShow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showShop = GetControllerAt(0);
		developLand = GetControllerAt(1);
		RemainingDice = GetControllerAt(2);
		com_AttrParent = (GComponent)GetChildAt(0);
		txt_Gold = (GTextField)GetChildAt(1);
		slider_Progress = (UISinglePlayer_Slider_Progress)GetChildAt(3);
		txt_RemainingRounds = (GRichTextField)GetChildAt(4);
		btn_guide = (GButton)GetChildAt(5);
		btn_Back = (GButton)GetChildAt(7);
		btn_UnLockLand = (UISinglePlayer_Button_UnLockLand)GetChildAt(9);
		txt_DevelopTip = (GTextField)GetChildAt(11);
		Com_CardsProb = (UISinglePlayer_Com_CardsProb)GetChildAt(13);
		list_Shop = (GList)GetChildAt(14);
		btn_RefreshStore = (UISinglePlayer_Button_Refresh)GetChildAt(15);
		txt_StoreGold = (GTextField)GetChildAt(16);
		list_Buff = (GList)GetChildAt(21);
		list_Bag = (GList)GetChildAt(24);
		btn_Move = (UISinglePlayer_Button_Move)GetChildAt(27);
		com_RewardShow = (UISinglePlayer_Com_RewardShow)GetChildAt(29);
		Cut_in = GetTransitionAt(0);
		Tips_1 = GetTransitionAt(1);
	}
}
