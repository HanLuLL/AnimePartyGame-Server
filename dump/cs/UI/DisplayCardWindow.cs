using System;
using System.Collections.Generic;
using Core.Scene;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class DisplayCardWindow : BaseWindow
{
	private int _CurIndex;

	private int _TotalDisplayCount;

	private Action<int> _SwitchCardAction;

	private List<(int, bool)> currentAltCardInfos = new List<(int, bool)>();

	private List<MapEventInfoConfigure> _MapEventInfoConfigures;

	private List<CardInfoConfigure> _HandCardInfos;

	private List<EventInfoConfigure> _EventCardInfos;

	public DisplayCardWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIDisplayCardWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShowAsync()
	{
		Show();
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIDisplayCardWindow uIDisplayCardWindow)
		{
			uIDisplayCardWindow.com_relicKeyword.visible = false;
			uIDisplayCardWindow.btn_Bg.onClick.Add(CloseMapEventInfo);
			uIDisplayCardWindow.btn_Next.onClick.Add(ShowNextMapEvent);
			uIDisplayCardWindow.btn_Previous.onClick.Add(ShowPreviousMapEvent);
			uIDisplayCardWindow.Btn_Next_AltArt.onClick.Add(ShowNextMapEvent);
			uIDisplayCardWindow.Btn_Previous_AltArt.onClick.Add(ShowPreviousMapEvent);
			uIDisplayCardWindow.btn_back.onClick.Add(CloseMapEventInfo);
			uIDisplayCardWindow.list_altCard.itemRenderer = RenderAltCard;
			uIDisplayCardWindow.list_altCard.opaque = false;
			if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle)
			{
				uIDisplayCardWindow.btn_way.visible = false;
			}
			else
			{
				uIDisplayCardWindow.btn_way.visible = true;
			}
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIDisplayCardWindow uIDisplayCardWindow)
		{
			uIDisplayCardWindow.btn_Bg.onClick.Remove(CloseMapEventInfo);
			uIDisplayCardWindow.btn_Next.onClick.Remove(ShowNextMapEvent);
			uIDisplayCardWindow.btn_Previous.onClick.Remove(ShowPreviousMapEvent);
			uIDisplayCardWindow.Btn_Next_AltArt.onClick.Remove(ShowNextMapEvent);
			uIDisplayCardWindow.Btn_Previous_AltArt.onClick.Remove(ShowPreviousMapEvent);
			uIDisplayCardWindow.btn_back.onClick.Remove(CloseMapEventInfo);
			uIDisplayCardWindow.list_altCard.itemRenderer = null;
			uIDisplayCardWindow.state.selectedIndex = 0;
		}
	}

	private void CloseMapEventInfo()
	{
		if (base.contentPane is UIDisplayCardWindow uIDisplayCardWindow)
		{
			uIDisplayCardWindow.btn_Bg.onClick.Retain();
			Hide();
			uIDisplayCardWindow.btn_Bg.onClick.Release();
		}
	}

	private void ShowNextMapEvent()
	{
		if (base.contentPane is UIDisplayCardWindow uIDisplayCardWindow)
		{
			int num = (_CurIndex + 1) % _TotalDisplayCount;
			if (_CurIndex != num)
			{
				uIDisplayCardWindow.btn_Next.onClick.Retain();
				_SwitchCardAction(num);
				uIDisplayCardWindow.btn_Next.onClick.Release();
			}
		}
	}

	private void ShowPreviousMapEvent()
	{
		if (base.contentPane is UIDisplayCardWindow uIDisplayCardWindow)
		{
			int num = ((_CurIndex == 0) ? (_TotalDisplayCount - 1) : (_CurIndex - 1));
			if (_CurIndex != num)
			{
				uIDisplayCardWindow.btn_Previous.onClick.Retain();
				_SwitchCardAction(num);
				uIDisplayCardWindow.btn_Previous.onClick.Release();
			}
		}
	}

	public async UniTask TryShowMapEvent(int index, List<MapEventInfoConfigure> mapEventInfoConfigures)
	{
		await TryShowAsync();
		if (base.contentPane is UIDisplayCardWindow uIDisplayCardWindow)
		{
			uIDisplayCardWindow.state.selectedIndex = 0;
			_MapEventInfoConfigures = mapEventInfoConfigures;
			_TotalDisplayCount = _MapEventInfoConfigures.Count;
			_SwitchCardAction = RenderMapEventCard;
			GButton btn_Previous = uIDisplayCardWindow.btn_Previous;
			bool flag = (uIDisplayCardWindow.btn_Next.visible = _MapEventInfoConfigures.Count > 1);
			btn_Previous.visible = flag;
			RenderMapEventCard(index);
		}
	}

	private void RenderMapEventCard(int index)
	{
		if (base.contentPane is UIDisplayCardWindow uIDisplayCardWindow)
		{
			_CurIndex = index;
			MapEventMapEventCardConfigure mapEventCardConfigure = _MapEventInfoConfigures[index].MapEventCardConfigure;
			string cardIndex = ((mapEventCardConfigure.CardNumb == 0) ? "" : mapEventCardConfigure.CardNumb.GetLocal(UIStringType.MapEvent));
			string cardTips = ((mapEventCardConfigure.CommentId == 0) ? "" : mapEventCardConfigure.CommentId.GetLocal(UIStringType.MapEvent));
			CommonUIManager.RendererCard(uIDisplayCardWindow.com_Card as UICom_Card, mapEventCardConfigure.GetImage(), mapEventCardConfigure.NameID.GetLocal(UIStringType.MapEvent), mapEventCardConfigure.DescId.GetLocal(UIStringType.MapEvent), cardIndex, cardTips, 0, mapEventCardConfigure.CardType);
		}
	}

	public async UniTask TryShowHandCard(List<CardInfoConfigure> handCardInfos, int index)
	{
		await TryShowAsync();
		if (base.contentPane is UIDisplayCardWindow uIDisplayCardWindow)
		{
			uIDisplayCardWindow.state.selectedIndex = 1;
			uIDisplayCardWindow.YiHua_Cut_in.Play();
			_HandCardInfos = handCardInfos;
			_TotalDisplayCount = _HandCardInfos.Count;
			_SwitchCardAction = RendererHandCard;
			GButton btn_Previous = uIDisplayCardWindow.btn_Previous;
			bool flag = (uIDisplayCardWindow.btn_Next.visible = _HandCardInfos.Count > 1);
			btn_Previous.visible = flag;
			RendererHandCard(index);
		}
	}

	private void SetCardClickEvent(CardView cardView, UICom_Card card)
	{
		if (cardView.CardType == 2)
		{
			card.Cut__In.Play();
			int clickClount = 0;
			card.onClick.Add((EventCallback0)delegate
			{
				if (clickClount % 2 == 0)
				{
					card.Cut__In.Stop();
					card.Cut_out.Play();
				}
				else
				{
					card.Cut__In.Play();
					card.Cut_out.Stop();
				}
				clickClount++;
			});
		}
		else
		{
			card.ShowDescState.Play();
			card.onClick.Clear();
		}
	}

	private void RendererHandCard(int index)
	{
		if (base.contentPane is UIDisplayCardWindow uIDisplayCardWindow && index >= 0 && index <= _HandCardInfos.Count - 1)
		{
			_CurIndex = index;
			CardInfoConfigure cardInfoConfigure = _HandCardInfos[index];
			string cardIndex = ((cardInfoConfigure.CardNumb == 0) ? "" : cardInfoConfigure.CardNumb.GetLocal(UIStringType.Card));
			string cardTips = ((cardInfoConfigure.CommentId == 0) ? "" : cardInfoConfigure.CommentId.GetLocal(UIStringType.Card));
			string text = ((cardInfoConfigure.NameID == 0) ? "" : cardInfoConfigure.NameID.GetLocal(UIStringType.Card));
			string content = ((cardInfoConfigure.DescId == 0) ? "" : cardInfoConfigure.DescId.GetLocal(UIStringType.Card));
			currentAltCardInfos = SimpleSingletonProvider<GameLogicManager>.inst.altArtCardLogic.GetAltArtCardInfos(cardInfoConfigure.Id);
			uIDisplayCardWindow.list_altCard.numItems = currentAltCardInfos.Count;
			CardView mainPlayerCardView = cardInfoConfigure.GetMainPlayerCardView();
			UICom_Card uICom_Card = uIDisplayCardWindow.AltArtCard as UICom_Card;
			CommonUIManager.RendererCard(uICom_Card, mainPlayerCardView, text, content, cardIndex, cardTips, cardInfoConfigure.Cost, cardInfoConfigure.CardType, cardInfoConfigure.CardTargetType);
			SimpleSingletonProvider<GameLogicManager>.inst.altArtCardLogic.TryGetAltArtCardId(cardInfoConfigure.Id, out var altArtCardId);
			RefreshBtnWay(cardInfoConfigure, altArtCardId);
			SetCardClickEvent(mainPlayerCardView, uICom_Card);
		}
	}

	private void RefreshBtnWay(CardInfoConfigure cardInfo, int altArtCardId)
	{
		if (base.contentPane is UIDisplayCardWindow { btn_way: var btn_way })
		{
			btn_way.txt_Title.color = Color.white;
			ItemInfoConfigure value;
			if (altArtCardId == cardInfo.Id)
			{
				btn_way.Refresh(70001);
			}
			else if (StaticConfigure.Item.InfoDict.TryGetValue(altArtCardId, out value))
			{
				int way = ((value.WayList.Count > 0) ? value.WayList[0] : 0);
				btn_way.Refresh(way);
			}
		}
	}

	private void RenderAltCard(int index, GObject obj)
	{
		if (base.contentPane is UIDisplayCardWindow && index >= 0 && index <= currentAltCardInfos.Count - 1)
		{
			(int, bool) tuple = currentAltCardInfos[index];
			int item = tuple.Item1;
			bool item2 = tuple.Item2;
			CardInfoConfigure cardInfoConfigure = _HandCardInfos[_CurIndex];
			SimpleSingletonProvider<GameLogicManager>.inst.altArtCardLogic.TryGetAltArtCardId(cardInfoConfigure.Id, out var altArtCardId);
			UIDisplayCard_Button_AltArt uIDisplayCard_Button_AltArt = obj as UIDisplayCard_Button_AltArt;
			uIDisplayCard_Button_AltArt.isSelected.selectedIndex = ((item == altArtCardId) ? 1 : 0);
			uIDisplayCard_Button_AltArt.loader_icon.grayed = !item2;
			ItemInfoConfigure value;
			if (item == cardInfoConfigure.Id)
			{
				uIDisplayCard_Button_AltArt.loader_icon.url = cardInfoConfigure.GetCardView().Key;
				uIDisplayCard_Button_AltArt.title.text = cardInfoConfigure.NameID.GetLocal(UIStringType.Card);
			}
			else if (StaticConfigure.Item.InfoDict.TryGetValue(item, out value))
			{
				uIDisplayCard_Button_AltArt.loader_icon.url = value.ShowIcon;
				uIDisplayCard_Button_AltArt.title.text = value.NameID.GetLocal(UIStringType.Item);
			}
			bool num = SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle;
			bool flag = SimpleSingletonProvider<GameLogicManager>.inst.altArtCardLogic.IsNewAltArtCard(cardInfoConfigure.Id, item);
			bool flag2 = !num && flag;
			uIDisplayCard_Button_AltArt.redPoint.selectedIndex = (flag2 ? 1 : 0);
			uIDisplayCard_Button_AltArt.onClick.Clear();
			uIDisplayCard_Button_AltArt.onClick.Add((EventCallback0)delegate
			{
				OnAltCardButtonClick(index);
			});
		}
	}

	private void OnAltCardButtonClick(int index)
	{
		if (base.contentPane is UIDisplayCardWindow uIDisplayCardWindow && index >= 0 && index <= currentAltCardInfos.Count - 1)
		{
			bool flag = SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle;
			(int, bool) tuple = currentAltCardInfos[index];
			int item = tuple.Item1;
			bool item2 = tuple.Item2;
			CardInfoConfigure cardInfoConfigure = _HandCardInfos[_CurIndex];
			SimpleSingletonProvider<GameLogicManager>.inst.altArtCardLogic.TryGetAltArtCardId(cardInfoConfigure.Id, out var altArtCardId);
			UIDisplayCard_Button_AltArt uIDisplayCard_Button_AltArt = uIDisplayCardWindow.list_altCard.GetChildAt(index) as UIDisplayCard_Button_AltArt;
			if (item2 && !flag)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.altArtCardLogic.RemoveNewAltCardTip(cardInfoConfigure.Id, item);
				uIDisplayCard_Button_AltArt.redPoint.selectedIndex = 0;
			}
			if (uIDisplayCard_Button_AltArt.isSelected.selectedIndex != 1 && item2 && item != altArtCardId && !flag)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.altArtCardLogic.SetCardAltArtC2S(cardInfoConfigure.Id, item);
			}
			for (int i = 0; i < uIDisplayCardWindow.list_altCard.numItems; i++)
			{
				(uIDisplayCardWindow.list_altCard.GetChildAt(i) as UIDisplayCard_Button_AltArt).isSelected.selectedIndex = ((i == index) ? 1 : 0);
			}
			string cardIndex = ((cardInfoConfigure.CardNumb == 0) ? "" : cardInfoConfigure.CardNumb.GetLocal(UIStringType.Card));
			string cardTips = ((cardInfoConfigure.CommentId == 0) ? "" : cardInfoConfigure.CommentId.GetLocal(UIStringType.Card));
			string text = ((cardInfoConfigure.NameID == 0) ? "" : cardInfoConfigure.NameID.GetLocal(UIStringType.Card));
			string content = ((cardInfoConfigure.DescId == 0) ? "" : cardInfoConfigure.DescId.GetLocal(UIStringType.Card));
			CardView cardView = cardInfoConfigure.GetCardView(item);
			UICom_Card uICom_Card = uIDisplayCardWindow.AltArtCard as UICom_Card;
			CommonUIManager.RendererCard(uICom_Card, cardView, text, content, cardIndex, cardTips, cardInfoConfigure.Cost, cardInfoConfigure.CardType, cardInfoConfigure.CardTargetType);
			RefreshBtnWay(cardInfoConfigure, item);
			SetCardClickEvent(cardView, uICom_Card);
		}
	}

	public async UniTask TryShowEventCard(List<EventInfoConfigure> eventCardInfos, int index)
	{
		await TryShowAsync();
		if (base.contentPane is UIDisplayCardWindow uIDisplayCardWindow)
		{
			uIDisplayCardWindow.state.selectedIndex = 0;
			uIDisplayCardWindow.Cut_In.Play();
			_EventCardInfos = eventCardInfos;
			_TotalDisplayCount = _EventCardInfos.Count;
			_SwitchCardAction = RendererEventCard;
			GButton btn_Previous = uIDisplayCardWindow.btn_Previous;
			bool flag = (uIDisplayCardWindow.btn_Next.visible = _EventCardInfos.Count > 1);
			btn_Previous.visible = flag;
			RendererEventCard(index);
			uIDisplayCardWindow.Cut_In.Play();
		}
	}

	private void RendererEventCard(int index)
	{
		if (base.contentPane is UIDisplayCardWindow { com_Card: UICom_Card com_Card, com_relicKeyword: UICom_RelicKeyword com_relicKeyword } && index >= 0 && index <= _EventCardInfos.Count - 1)
		{
			_CurIndex = index;
			EventInfoConfigure eventInfoConfigure = _EventCardInfos[index];
			string cardIndex = ((eventInfoConfigure.CardNumb == 0) ? "" : eventInfoConfigure.CardNumb.GetLocal(UIStringType.Event));
			string cardTips = ((eventInfoConfigure.CommentId == 0) ? "" : eventInfoConfigure.CommentId.GetLocal(UIStringType.Event));
			string text = ((eventInfoConfigure.NameID == 0) ? "" : eventInfoConfigure.NameID.GetLocal(UIStringType.Event));
			string content = ((eventInfoConfigure.DescId == 0) ? "" : eventInfoConfigure.DescId.GetLocal(UIStringType.Event));
			CommonUIManager.RendererCard(com_Card, eventInfoConfigure.GetImage(), text, content, cardIndex, cardTips, 0, eventInfoConfigure.CardType);
			com_relicKeyword.visible = false;
			com_relicKeyword.RefreshInfo(CommonUIManager.TryShowDisplayCardRelicKeywordData(com_Card.txt_Content.text));
		}
	}
}
