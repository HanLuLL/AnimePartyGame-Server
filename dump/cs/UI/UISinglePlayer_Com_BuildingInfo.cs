using FairyGUI;
using FairyGUI.Utils;
using SinglePlayer;
using SinglePlayer.GamePlay;
using SinglePlayer.GamePlay.Build;
using SinglePlayer.GamePlay.Card;
using SinglePlayer.Tools;
using Tools;

namespace UI;

public class UISinglePlayer_Com_BuildingInfo : GComponent
{
	private Card _card;

	public Controller type;

	public GList list_Tags;

	public GRichTextField txt_Desc;

	public UISinglePlayer_Com_CardItemSlot com_CardInfo;

	public UISinglePlayer_Com_CardName com_Name;

	public GButton btn_Sell;

	public GTextField txt_Price;

	public GButton btn_Buy;

	public Transition Cut_in;

	public const string URL = "ui://mi9vm3w0fo1bq3z";

	public void Refresh(Card card)
	{
		Cut_in.Play();
		_card = card;
		com_Name.txt_Name.text = card.CardConfigure.NameID.GetLocal(UIStringType.SinglePlayer);
		com_Name.type.selectedIndex = GetNameColor();
		com_CardInfo.Refresh(card, CardItemSlotType.None);
		com_CardInfo.type.selectedIndex = 1;
		list_Tags.itemRenderer = RendererTags;
		list_Tags.numItems = card.CardConfigure.CardTag.Count;
		btn_Sell.visible = card.HasPurchase;
		RefreshBuyBtn(card);
		int num;
		if (card.HasPurchase)
		{
			num = Game.GetSystem<BoardManager>().cardManager.GetRecycleCardPrice(card.UID);
			type.selectedIndex = 0;
		}
		else
		{
			num = card.BuyPrice;
			type.selectedIndex = 1;
		}
		txt_Price.SetVar("price", num.ToString()).FlushVars();
		btn_Sell.onClick.Set((EventCallback0)delegate
		{
			btn_Sell.onClick.Retain();
			Game.GetSystem<BoardManager>().cardManager.RecycleCard(card.UID);
			GRoot.inst.HidePopup(this);
			btn_Sell.onClick.Release();
		});
		btn_Buy.onClick.Set((EventCallback0)delegate
		{
			btn_Buy.onClick.Retain();
			Game.GetSystem<BoardManager>().cardManager.OnRequestPurchaseCardPutInBag(card.UID);
			GRoot.inst.HidePopup(this);
			btn_Buy.onClick.Release();
		});
	}

	public void RefreshDesc(Card card)
	{
		txt_Desc.text = ParseSinglePlayerText.ParseAstralCardDesc(card.CardConfigure.Id, card.Level.Value);
	}

	public void RefreshDesc(BuildingBase building)
	{
		txt_Desc.text = ParseSinglePlayerText.ParseAstralBuildingDesc(building);
	}

	private void RendererTags(int index, GObject gObject)
	{
		if (gObject is UISinglePlayer_Com_CardTag2 uISinglePlayer_Com_CardTag && _card != null)
		{
			SinglePlayerTagType safeByIndex = _card.CardConfigure.CardTag.GetSafeByIndex(index);
			if (safeByIndex >= SinglePlayerTagType.None && (int)safeByIndex < uISinglePlayer_Com_CardTag.type.pageCount)
			{
				uISinglePlayer_Com_CardTag.type.selectedIndex = (int)safeByIndex;
			}
		}
	}

	private void RefreshBuyBtn(Card card)
	{
		if (!Game.GetSystem<BoardManager>().cardManager.IsBagFull() && Game.GetSystem<BoardManager>().CheckGold(card.BuyPrice))
		{
			btn_Buy.visible = true;
		}
		else
		{
			btn_Buy.visible = false;
		}
	}

	private int GetNameColor()
	{
		return (CardRarity)_card.CardConfigure.Rarity switch
		{
			CardRarity.GREEN => 1, 
			CardRarity.BLUE => 2, 
			CardRarity.PURPLE => 3, 
			CardRarity.GOLDEN => 4, 
			CardRarity.RED => 4, 
			_ => 0, 
		};
	}

	public static UISinglePlayer_Com_BuildingInfo CreateInstance()
	{
		return (UISinglePlayer_Com_BuildingInfo)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_Com_BuildingInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		list_Tags = (GList)GetChildAt(1);
		txt_Desc = (GRichTextField)GetChildAt(2);
		com_CardInfo = (UISinglePlayer_Com_CardItemSlot)GetChildAt(3);
		com_Name = (UISinglePlayer_Com_CardName)GetChildAt(4);
		btn_Sell = (GButton)GetChildAt(5);
		txt_Price = (GTextField)GetChildAt(7);
		btn_Buy = (GButton)GetChildAt(9);
		Cut_in = GetTransitionAt(0);
	}
}
