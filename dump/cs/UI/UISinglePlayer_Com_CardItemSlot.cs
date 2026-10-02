using System;
using Core;
using FairyGUI;
using FairyGUI.Utils;
using SinglePlayer;
using SinglePlayer.GamePlay;
using SinglePlayer.GamePlay.Action;
using SinglePlayer.GamePlay.Card;
using SinglePlayer.GamePlay.Map;
using Tools;

namespace UI;

public class UISinglePlayer_Com_CardItemSlot : GButton
{
	public Card CardInfo;

	public Controller status;

	public Controller type;

	public Controller language;

	public Controller purchase;

	public GList list_Tags;

	public UISinglePlayer_Com_CardQuality com_Quality;

	public GLoader loader_Icon;

	public GTextField txt_SoldOutCN;

	public GTextField txt_SoldOutEN;

	public GTextField txt_Price;

	public Transition Cut_in;

	public const string URL = "ui://mi9vm3w0oai4q52";

	public void Refresh(Card card, CardItemSlotType slotType)
	{
		RefreshInfo(card, slotType);
		base.onClick.Set((EventCallback0)delegate
		{
			if (CardInfo != null && SimpleSingletonProvider<UIManager>.inst.currentPanel is SinglePlayerPanel singlePlayerPanel)
			{
				singlePlayerPanel.ShowCardInfo(this);
			}
		});
		base.draggable = true;
		base.onDragStart.Set(delegate(EventContext context)
		{
			context.PreventDefault();
			if (context.inputEvent.button == 0 && CardInfo != null)
			{
				if (!CardInfo.HasPurchase && !Game.GetSystem<BoardManager>().CheckGold(CardInfo.BuyPrice))
				{
					SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(10013);
				}
				else if (Game.GetSystem<PlayerActionFSM>().CurrentState == PlayerActionType.Idle)
				{
					base.onTouchBegin.Retain();
					status.selectedIndex = 0;
					loader_Icon.visible = false;
					Game.GetController<BuildingController>().StartDraggingCard(CardInfo);
					base.onTouchBegin.Release();
				}
			}
		});
	}

	public void RefreshInfo(Card card, CardItemSlotType slotType)
	{
		CardInfo = card;
		status.selectedIndex = ((CardInfo != null) ? 1 : 0);
		com_Quality.quality.selectedIndex = 0;
		loader_Icon.visible = true;
		loader_Icon.grayed = false;
		txt_SoldOutCN.visible = false;
		txt_SoldOutEN.visible = false;
		purchase.selectedIndex = 0;
		switch (slotType)
		{
		case CardItemSlotType.Bag:
			type.selectedIndex = 1;
			break;
		case CardItemSlotType.Shop:
			Cut_in.Play();
			type.selectedIndex = 0;
			break;
		}
		if (CardInfo != null)
		{
			loader_Icon.url = CardInfo.CardConfigure.Icon;
			com_Quality.quality.selectedIndex = GetQualityIndex();
			list_Tags.itemRenderer = RendererTags;
			list_Tags.numItems = CardInfo.CardConfigure.CardTag.Count;
			if (!CardInfo.HasPurchase)
			{
				txt_Price.text = CardInfo.BuyPrice.ToString();
				if (!Game.GetSystem<BoardManager>().CheckGold(card.BuyPrice))
				{
					purchase.selectedIndex = 1;
				}
			}
			else
			{
				txt_Price.text = Game.GetSystem<BoardManager>().cardManager.GetRecycleCardPrice(card.UID).ToString();
			}
		}
		else
		{
			if (GameSettings.languageType == LanguageType.SimplifiedChinese)
			{
				txt_SoldOutCN.visible = true;
				txt_SoldOutEN.visible = false;
			}
			else
			{
				txt_SoldOutCN.visible = false;
				txt_SoldOutEN.visible = true;
			}
			if (slotType != CardItemSlotType.Shop)
			{
				loader_Icon.visible = false;
			}
			else
			{
				loader_Icon.grayed = true;
			}
		}
	}

	private void RendererTags(int index, GObject gObject)
	{
		if (gObject is UISinglePlayer_Com_CardTag uISinglePlayer_Com_CardTag)
		{
			uISinglePlayer_Com_CardTag.type.selectedIndex = GetTagType(index);
		}
	}

	private int GetTagType(int index)
	{
		if (CardInfo == null)
		{
			return 0;
		}
		return (int)CardInfo.CardConfigure.CardTag.GetSafeByIndex(index);
	}

	private int GetQualityIndex()
	{
		if (CardInfo == null)
		{
			return 0;
		}
		return (CardRarity)CardInfo.CardConfigure.Rarity switch
		{
			CardRarity.GREEN => 1, 
			CardRarity.BLUE => 2, 
			CardRarity.PURPLE => 3, 
			CardRarity.GOLDEN => 4, 
			CardRarity.RED => 5, 
			_ => throw new ArgumentOutOfRangeException(), 
		};
	}

	public void Clear()
	{
		CardInfo = null;
		status.selectedIndex = 0;
		list_Tags.numItems = 0;
		loader_Icon.visible = false;
	}

	public static UISinglePlayer_Com_CardItemSlot CreateInstance()
	{
		return (UISinglePlayer_Com_CardItemSlot)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_Com_CardItemSlot");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		type = GetControllerAt(1);
		language = GetControllerAt(2);
		purchase = GetControllerAt(3);
		list_Tags = (GList)GetChildAt(1);
		com_Quality = (UISinglePlayer_Com_CardQuality)GetChildAt(3);
		loader_Icon = (GLoader)GetChildAt(5);
		txt_SoldOutCN = (GTextField)GetChildAt(6);
		txt_SoldOutEN = (GTextField)GetChildAt(7);
		txt_Price = (GTextField)GetChildAt(9);
		Cut_in = GetTransitionAt(0);
	}
}
