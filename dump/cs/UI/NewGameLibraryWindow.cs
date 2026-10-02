using System.Collections.Generic;
using Core.Scene;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class NewGameLibraryWindow : BaseWindow
{
	public readonly List<CardInfoConfigure> CardInfos = new List<CardInfoConfigure>();

	public readonly List<LandInfoConfigure> LandInfos = new List<LandInfoConfigure>();

	public readonly List<RelicInfoConfigure> RelicInfos = new List<RelicInfoConfigure>();

	public readonly List<EventInfoConfigure> EventInfos = new List<EventInfoConfigure>();

	private const int MainTabIndex = 0;

	private const int CardTabIndex = 1;

	private const int LandTabIndex = 2;

	private const int MapTabIndex = 3;

	private const int RelicTabIndex = 4;

	private const int EventTabIndex = 5;

	private bool isBlockHandCardBtnEventOnce;

	private UINewGameLibrary_Button_RelicItem CurSelectRelicItem;

	public NewGameLibraryWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UINewGameLibraryWindow.CreateInstance();
		base.OnInit();
	}

	public async UniTask TryShowAsync()
	{
		if (!base.isShowing)
		{
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (!(base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow))
		{
			return;
		}
		AddEvent();
		uINewGameLibraryWindow.Cut_in.Play();
		uINewGameLibraryWindow.com_Cords.list_Cards.SetVirtual();
		uINewGameLibraryWindow.com_Cords.list_Cards.itemRenderer = OnCardRenderer;
		uINewGameLibraryWindow.com_Lands.list_Lands.SetVirtual();
		uINewGameLibraryWindow.com_Lands.list_Lands.itemRenderer = OnLandRenderer;
		uINewGameLibraryWindow.com_Relic.list_Relics.SetVirtual();
		uINewGameLibraryWindow.com_Relic.list_Relics.itemRenderer = OnRelicRenderer;
		uINewGameLibraryWindow.com_Map.InitComponent();
		uINewGameLibraryWindow.com_Map.Com_main.com_TabMap.InitComponent();
		SetText();
		SimpleSingletonProvider<CriMovieManager>.inst.Play(50.GetVideoKey(), uINewGameLibraryWindow.loader_BG).Forget();
		uINewGameLibraryWindow.loader_Line.visible = true;
		RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
		if (roomController != null && roomController.IsBattle())
		{
			if (!StaticConfigure.Map.InfoDict.TryGetValue(roomController.localRoom.MapId, out var value))
			{
				Debug.LogError($"Map.InfoDict中找不到地图ID{roomController.localRoom.MapId}的配置");
				return;
			}
			uINewGameLibraryWindow.label.selectedIndex = 3;
			uINewGameLibraryWindow.com_Map.State.selectedIndex = 1;
			uINewGameLibraryWindow.com_Map.Com_main.Isroom.selectedIndex = 1;
			uINewGameLibraryWindow.com_Map.Com_main.Cut_in.Play();
			uINewGameLibraryWindow.com_Map.RefreshMapMainInfo(value, this);
		}
		else
		{
			uINewGameLibraryWindow.label.selectedIndex = 0;
			uINewGameLibraryWindow.Btn_Code.redPoint.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.altArtCardLogic.HasAnyNewAltArtCard() ? 1 : 0);
		}
	}

	private void AddEvent()
	{
		if (base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow)
		{
			uINewGameLibraryWindow.com_Cords.list_Cards.onClickItem.Add(OnClickHandCardItem);
			uINewGameLibraryWindow.btn_Return.onClick.Add(CloseWin);
			uINewGameLibraryWindow.com_Cords.btn_Return.onClick.Add(ReturnMain);
			uINewGameLibraryWindow.com_Relic.btn_Return.onClick.Add(ReturnMain);
			uINewGameLibraryWindow.com_Lands.btn_Return.onClick.Add(ReturnMain);
			uINewGameLibraryWindow.com_Map.btn_Return.onClick.Add(ReturnMain);
			uINewGameLibraryWindow.Btn_Code.onClick.Add(ChangeLabelCode);
			uINewGameLibraryWindow.Btn_Event.onClick.Add(ChangeLabelEvent);
			uINewGameLibraryWindow.Btn_Land.onClick.Add(ChangeLabelLand);
			uINewGameLibraryWindow.Btn_Relic.onClick.Add(ChangeLabelRelic);
			uINewGameLibraryWindow.Btn_Map.onClick.Add(ChangeLabelMap);
			uINewGameLibraryWindow.com_Cords.Btn_Del.onClick.Add(OnDelCardsText);
			uINewGameLibraryWindow.com_Lands.Btn_Del.onClick.Add(OnDelCardsText);
			uINewGameLibraryWindow.com_Relic.Btn_Del.onClick.Add(OnDelCardsText);
			uINewGameLibraryWindow.com_Relic.Search_Input.onChanged.Add(OnSearchRelic);
			uINewGameLibraryWindow.com_Cords.Search_Input.onChanged.Add(OnSearchCards);
			uINewGameLibraryWindow.com_Lands.Search_Input.onChanged.Add(OnSearchLands);
			uINewGameLibraryWindow.com_Cords.DropDown_Filter.onChanged.Add(OnFilterStateChanged);
			uINewGameLibraryWindow.com_Map.AddEvent();
			uINewGameLibraryWindow.com_Map.Com_main.com_TabMap.AddEvent();
			SimpleSingletonProvider<GameLogicManager>.inst.altArtCardLogic.OnCardChanged.AddListener(OnCardChanged);
			SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.curPlayerOperate.AddListener(CloseWin);
		}
	}

	private void RemoveEvent()
	{
		if (base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow)
		{
			uINewGameLibraryWindow.com_Cords.list_Cards.onClickItem.Remove(OnClickHandCardItem);
			uINewGameLibraryWindow.btn_Return.onClick.Remove(CloseWin);
			uINewGameLibraryWindow.com_Cords.btn_Return.onClick.Remove(ReturnMain);
			uINewGameLibraryWindow.com_Relic.btn_Return.onClick.Remove(ReturnMain);
			uINewGameLibraryWindow.com_Lands.btn_Return.onClick.Remove(ReturnMain);
			uINewGameLibraryWindow.com_Map.btn_Return.onClick.Remove(ReturnMain);
			uINewGameLibraryWindow.Btn_Code.onClick.Remove(ChangeLabelCode);
			uINewGameLibraryWindow.Btn_Event.onClick.Remove(ChangeLabelEvent);
			uINewGameLibraryWindow.Btn_Land.onClick.Remove(ChangeLabelLand);
			uINewGameLibraryWindow.Btn_Relic.onClick.Remove(ChangeLabelRelic);
			uINewGameLibraryWindow.Btn_Map.onClick.Remove(ChangeLabelMap);
			uINewGameLibraryWindow.com_Cords.Btn_Del.onClick.Remove(OnDelCardsText);
			uINewGameLibraryWindow.com_Lands.Btn_Del.onClick.Remove(OnDelCardsText);
			uINewGameLibraryWindow.com_Relic.Btn_Del.onClick.Remove(OnDelCardsText);
			uINewGameLibraryWindow.com_Relic.Search_Input.onChanged.Remove(OnSearchRelic);
			uINewGameLibraryWindow.com_Cords.Search_Input.onChanged.Remove(OnSearchCards);
			uINewGameLibraryWindow.com_Lands.Search_Input.onChanged.Remove(OnSearchLands);
			uINewGameLibraryWindow.com_Cords.DropDown_Filter.onChanged.Remove(OnFilterStateChanged);
			uINewGameLibraryWindow.com_Map.RemoveEvent();
			uINewGameLibraryWindow.com_Map.Com_main.com_TabMap.RemoveEvent();
			uINewGameLibraryWindow.com_Map.Close();
			SimpleSingletonProvider<GameLogicManager>.inst.altArtCardLogic.OnCardChanged.RemoveListener(OnCardChanged);
			SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.curPlayerOperate.RemoveListener(CloseWin);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow)
		{
			RemoveEvent();
			SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(uINewGameLibraryWindow.loader_BG);
			if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Home)
			{
				SimpleSingletonProvider<CriMovieManager>.inst.ClearOne(50.GetVideoKey());
			}
		}
	}

	public void CloseWin()
	{
		if (base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow)
		{
			uINewGameLibraryWindow.btn_Return.onClick.Retain();
			SimpleSingletonProvider<UIManager>.inst.HideNewGameLibrary();
			uINewGameLibraryWindow.btn_Return.onClick.Release();
		}
	}

	private void ReturnMain()
	{
		if (base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow)
		{
			uINewGameLibraryWindow.com_Cords.btn_Return.onClick.Retain();
			uINewGameLibraryWindow.com_Relic.btn_Return.onClick.Retain();
			uINewGameLibraryWindow.com_Lands.btn_Return.onClick.Retain();
			uINewGameLibraryWindow.com_Map.btn_Return.onClick.Retain();
			uINewGameLibraryWindow.label.selectedIndex = 0;
			uINewGameLibraryWindow.Btn_Code.redPoint.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.altArtCardLogic.HasAnyNewAltArtCard() ? 1 : 0);
			uINewGameLibraryWindow.com_Cords.Search_Input.text = "";
			uINewGameLibraryWindow.com_Relic.Search_Input.text = "";
			uINewGameLibraryWindow.com_Lands.Search_Input.text = "";
			uINewGameLibraryWindow.com_Cords.btn_Return.onClick.Release();
			uINewGameLibraryWindow.com_Relic.btn_Return.onClick.Release();
			uINewGameLibraryWindow.com_Lands.btn_Return.onClick.Release();
			uINewGameLibraryWindow.com_Map.btn_Return.onClick.Release();
		}
	}

	private void ChangeLabelCode()
	{
		if (base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow)
		{
			uINewGameLibraryWindow.com_Cords.DropDown_Filter.selectedIndex = 0;
			uINewGameLibraryWindow.com_Cords.showFilter.selectedIndex = 1;
			UpdateFilterTitle();
			uINewGameLibraryWindow.Btn_Code.onClick.Retain();
			RefreshCardInfo();
			uINewGameLibraryWindow.Btn_Code.onClick.Release();
		}
	}

	private void ChangeLabelEvent()
	{
		if (base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow)
		{
			uINewGameLibraryWindow.com_Cords.showFilter.selectedIndex = 0;
			uINewGameLibraryWindow.Btn_Event.onClick.Retain();
			RefreshEventInfo();
			uINewGameLibraryWindow.Btn_Event.onClick.Release();
		}
	}

	private void ChangeLabelLand()
	{
		if (base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow)
		{
			uINewGameLibraryWindow.Btn_Land.onClick.Retain();
			RefreshLandInfo();
			uINewGameLibraryWindow.Btn_Land.onClick.Release();
		}
	}

	private void ChangeLabelRelic()
	{
		if (base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow)
		{
			uINewGameLibraryWindow.Btn_Relic.onClick.Retain();
			RefreshRelicInfo();
			uINewGameLibraryWindow.Btn_Relic.onClick.Release();
		}
	}

	private void ChangeLabelMap()
	{
		if (base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow)
		{
			uINewGameLibraryWindow.Btn_Map.onClick.Retain();
			uINewGameLibraryWindow.com_Map.State.selectedIndex = 0;
			uINewGameLibraryWindow.com_Map.Com_main.tab.selectedIndex = 0;
			uINewGameLibraryWindow.com_Map.RefreshMapInfo(this);
			uINewGameLibraryWindow.com_Map.RefreshGameMode();
			uINewGameLibraryWindow.Btn_Map.onClick.Release();
		}
	}

	private void OnSearchRelic()
	{
		if (base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow)
		{
			uINewGameLibraryWindow.com_Relic.Btn_Del.onClick.Retain();
			uINewGameLibraryWindow.com_Relic.Btn_Del.Type.selectedIndex = 1;
			string inputText = uINewGameLibraryWindow.com_Relic.Search_Input.text;
			SearchRelics(inputText);
			uINewGameLibraryWindow.com_Relic.Btn_Del.onClick.Release();
		}
	}

	private void OnSearchLands()
	{
		if (base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow)
		{
			uINewGameLibraryWindow.com_Lands.Btn_Del.onClick.Retain();
			uINewGameLibraryWindow.com_Lands.Btn_Del.Type.selectedIndex = 1;
			string inputText = uINewGameLibraryWindow.com_Lands.Search_Input.text;
			SearchLands(inputText);
			uINewGameLibraryWindow.com_Lands.Btn_Del.onClick.Release();
		}
	}

	private void OnDelCardsText()
	{
		if (base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow)
		{
			uINewGameLibraryWindow.com_Cords.Btn_Del.onClick.Retain();
			uINewGameLibraryWindow.com_Lands.Btn_Del.onClick.Retain();
			uINewGameLibraryWindow.com_Relic.Btn_Del.onClick.Retain();
			if (uINewGameLibraryWindow.label.selectedIndex == 1)
			{
				string text = (uINewGameLibraryWindow.com_Cords.Search_Input.text = "");
				string inputText = text;
				SearchCards(inputText, GetCardFilterFlag());
			}
			else if (uINewGameLibraryWindow.label.selectedIndex == 5)
			{
				string text = (uINewGameLibraryWindow.com_Cords.Search_Input.text = "");
				string inputText2 = text;
				SearchEvents(inputText2);
			}
			else if (uINewGameLibraryWindow.label.selectedIndex == 2)
			{
				string text = (uINewGameLibraryWindow.com_Lands.Search_Input.text = "");
				string inputText3 = text;
				SearchLands(inputText3);
			}
			else if (uINewGameLibraryWindow.label.selectedIndex == 4)
			{
				string text = (uINewGameLibraryWindow.com_Relic.Search_Input.text = "");
				string inputText4 = text;
				SearchRelics(inputText4);
			}
			uINewGameLibraryWindow.com_Cords.Btn_Del.onClick.Release();
			uINewGameLibraryWindow.com_Lands.Btn_Del.onClick.Release();
			uINewGameLibraryWindow.com_Relic.Btn_Del.onClick.Release();
		}
	}

	private void OnSearchCards()
	{
		if (base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow)
		{
			uINewGameLibraryWindow.com_Cords.Btn_Del.onClick.Retain();
			string inputText = uINewGameLibraryWindow.com_Cords.Search_Input.text;
			if (uINewGameLibraryWindow.label.selectedIndex == 1)
			{
				SearchCards(inputText, GetCardFilterFlag());
			}
			else if (uINewGameLibraryWindow.label.selectedIndex == 5)
			{
				SearchEvents(inputText);
			}
			uINewGameLibraryWindow.com_Cords.Btn_Del.onClick.Release();
		}
	}

	private void UpdateFilterTitle()
	{
		if (base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow)
		{
			int selectedIndex = uINewGameLibraryWindow.com_Cords.DropDown_Filter.selectedIndex;
			if (selectedIndex >= 0 && selectedIndex < uINewGameLibraryWindow.com_Cords.DropDown_Filter.items.Length)
			{
				string title = uINewGameLibraryWindow.com_Cords.DropDown_Filter.items[selectedIndex];
				uINewGameLibraryWindow.com_Cords.DropDown_Filter.title = title;
			}
		}
	}

	private CardFilterFlag GetCardFilterFlag()
	{
		if (!(base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow))
		{
			return CardFilterFlag.None;
		}
		int selectedIndex = uINewGameLibraryWindow.com_Cords.DropDown_Filter.selectedIndex;
		if (selectedIndex > 0)
		{
			return (CardFilterFlag)(1 << selectedIndex - 1);
		}
		return CardFilterFlag.None;
	}

	private void OnFilterStateChanged()
	{
		if (base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow)
		{
			CardFilterFlag cardFilterFlag = GetCardFilterFlag();
			string inputText = uINewGameLibraryWindow.com_Cords.Search_Input.text;
			SearchCards(inputText, cardFilterFlag);
			UpdateFilterTitle();
		}
	}

	private void SearchCards(string inputText, CardFilterFlag flag)
	{
		if (!(base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow))
		{
			return;
		}
		FilterCards(flag, inputText);
		if (string.IsNullOrEmpty(inputText))
		{
			uINewGameLibraryWindow.com_Cords.Search.selectedIndex = 0;
			uINewGameLibraryWindow.com_Cords.Search_Input.color = Color.gray;
			uINewGameLibraryWindow.com_Cords.Btn_Del.Type.selectedIndex = 0;
		}
		else
		{
			uINewGameLibraryWindow.com_Cords.Search_Input.color = Color.black;
			if (CardInfos.Count == 0)
			{
				uINewGameLibraryWindow.com_Cords.Search.selectedIndex = 1;
			}
			else
			{
				uINewGameLibraryWindow.com_Cords.Search.selectedIndex = 0;
			}
			uINewGameLibraryWindow.com_Cords.Btn_Del.Type.selectedIndex = 1;
		}
		uINewGameLibraryWindow.com_Cords.list_Cards.scrollPane.percY = 0f;
		uINewGameLibraryWindow.com_Cords.list_Cards.numItems = CardInfos.Count;
		RefreshTransition(uINewGameLibraryWindow.com_Cords.list_Cards._children);
	}

	public void FilterCards(CardFilterFlag flag, string inputText)
	{
		CardInfos.Clear();
		bool flag2 = !string.IsNullOrEmpty(inputText);
		foreach (CardInfoConfigure info in StaticConfigure.Card.Infos)
		{
			bool flag3 = info.IsGalleryShow;
			if (flag3 && flag2)
			{
				string obj = ((info.NameID == 0) ? "" : info.NameID.GetLocal(UIStringType.Card));
				string text = ((info.DescId == 0) ? "" : info.DescId.GetLocal(UIStringType.Card));
				flag3 = obj.Contains(inputText) || text.Contains(inputText);
			}
			if (flag3 && flag.HasFlag(CardFilterFlag.HasAltCard))
			{
				flag3 = SimpleSingletonProvider<GameLogicManager>.inst.altArtCardLogic.HasAltCard(info.Id);
			}
			if (flag3 && flag.HasFlag(CardFilterFlag.HasUnlocked))
			{
				flag3 = SimpleSingletonProvider<GameLogicManager>.inst.altArtCardLogic.HasUnlockedAltCard(info.Id);
			}
			if (flag3 && flag.HasFlag(CardFilterFlag.CharterCard))
			{
				flag3 = info.IsDedicated;
			}
			if (flag3)
			{
				CardInfos.Add(info);
			}
		}
	}

	private void SearchEvents(string inputText)
	{
		if (!(base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow))
		{
			return;
		}
		EventInfos.Clear();
		if (string.IsNullOrEmpty(inputText))
		{
			EventInfos.AddRange(StaticConfigure.Event.Infos);
			uINewGameLibraryWindow.com_Cords.Search_Input.color = Color.gray;
			uINewGameLibraryWindow.com_Cords.Search.selectedIndex = 0;
			uINewGameLibraryWindow.com_Cords.Btn_Del.Type.selectedIndex = 0;
		}
		else
		{
			uINewGameLibraryWindow.com_Cords.Search_Input.color = Color.black;
			foreach (EventInfoConfigure info in StaticConfigure.Event.Infos)
			{
				string obj = ((info.NameID == 0) ? "" : info.NameID.GetLocal(UIStringType.Event));
				string text = ((info.DescId == 0) ? "" : info.DescId.GetLocal(UIStringType.Event));
				if (obj.Contains(inputText) || text.Contains(inputText))
				{
					EventInfos.Add(info);
				}
			}
			if (EventInfos.Count == 0)
			{
				uINewGameLibraryWindow.com_Cords.Search.selectedIndex = 1;
			}
			else
			{
				uINewGameLibraryWindow.com_Cords.Search.selectedIndex = 0;
			}
			uINewGameLibraryWindow.com_Cords.Btn_Del.Type.selectedIndex = 1;
		}
		uINewGameLibraryWindow.com_Cords.list_Cards.scrollPane.percY = 0f;
		uINewGameLibraryWindow.com_Cords.list_Cards.numItems = EventInfos.Count;
		RefreshTransition(uINewGameLibraryWindow.com_Cords.list_Cards._children);
	}

	private void SearchLands(string inputText)
	{
		if (!(base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow))
		{
			return;
		}
		LandInfos.Clear();
		if (string.IsNullOrEmpty(inputText))
		{
			foreach (LandInfoConfigure info in StaticConfigure.Land.Infos)
			{
				if (info.IsGalleryShow)
				{
					LandInfos.Add(info);
				}
			}
			uINewGameLibraryWindow.com_Lands.Search_Input.color = Color.gray;
			uINewGameLibraryWindow.com_Lands.Search.selectedIndex = 0;
			uINewGameLibraryWindow.com_Lands.Btn_Del.Type.selectedIndex = 0;
		}
		else
		{
			uINewGameLibraryWindow.com_Lands.Search_Input.color = Color.black;
			foreach (LandInfoConfigure info2 in StaticConfigure.Land.Infos)
			{
				if (info2.IsGalleryShow)
				{
					string local = info2.NameID.GetLocal(UIStringType.Land);
					string local2 = info2.DescriptionID.GetLocal(UIStringType.Land);
					if (local.Contains(inputText) || local2.Contains(inputText))
					{
						LandInfos.Add(info2);
					}
				}
			}
			if (LandInfos.Count == 0)
			{
				uINewGameLibraryWindow.com_Lands.Search.selectedIndex = 1;
			}
			else
			{
				uINewGameLibraryWindow.com_Lands.Search.selectedIndex = 0;
			}
			uINewGameLibraryWindow.com_Lands.Btn_Del.Type.selectedIndex = 1;
		}
		uINewGameLibraryWindow.com_Lands.list_Lands.scrollPane.percY = 0f;
		uINewGameLibraryWindow.com_Lands.list_Lands.numItems = LandInfos.Count;
		RefreshTransition(uINewGameLibraryWindow.com_Lands.list_Lands._children);
	}

	private void SearchRelics(string inputText)
	{
		if (!(base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow))
		{
			return;
		}
		RelicInfos.Clear();
		if (string.IsNullOrEmpty(inputText))
		{
			RelicInfos.AddRange(StaticConfigure.Relic.Infos);
			uINewGameLibraryWindow.com_Relic.Search.selectedIndex = 0;
			uINewGameLibraryWindow.com_Relic.Btn_Del.Type.selectedIndex = 0;
			uINewGameLibraryWindow.com_Relic.Search_Input.color = Color.gray;
		}
		else
		{
			uINewGameLibraryWindow.com_Relic.Search_Input.color = Color.black;
			foreach (RelicInfoConfigure info in StaticConfigure.Relic.Infos)
			{
				string local = info.NameID.GetLocal(UIStringType.Relic);
				string local2 = info.DescID.GetLocal(UIStringType.Relic);
				if (local.Contains(inputText) || local2.Contains(inputText))
				{
					RelicInfos.Add(info);
				}
			}
			if (RelicInfos.Count == 0)
			{
				uINewGameLibraryWindow.com_Relic.Search.selectedIndex = 1;
			}
			else
			{
				uINewGameLibraryWindow.com_Relic.Search.selectedIndex = 0;
			}
			uINewGameLibraryWindow.com_Relic.Btn_Del.Type.selectedIndex = 1;
		}
		uINewGameLibraryWindow.com_Relic.list_Relics.scrollPane.percY = 0f;
		uINewGameLibraryWindow.com_Relic.list_Relics.numItems = RelicInfos.Count;
		RefreshTransition(uINewGameLibraryWindow.com_Relic.list_Relics._children);
	}

	private void OnCardChanged(int cardId)
	{
		if (!(base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow) || uINewGameLibraryWindow.label.selectedIndex != 1)
		{
			return;
		}
		int num = CardInfos.FindIndex((CardInfoConfigure x) => x.Id == cardId);
		if (num >= 0)
		{
			int num2 = uINewGameLibraryWindow.com_Cords.list_Cards.ItemIndexToChildIndex(num);
			if (num2 >= 0)
			{
				GObject childAt = uINewGameLibraryWindow.com_Cords.list_Cards.GetChildAt(num2);
				OnCardRenderer(num, childAt);
			}
		}
	}

	private void RefreshCardInfo()
	{
		if (base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow)
		{
			CardFilterFlag cardFilterFlag = GetCardFilterFlag();
			string inputText = uINewGameLibraryWindow.com_Cords.Search_Input.text;
			SearchCards(inputText, cardFilterFlag);
		}
	}

	private void OnCardRenderer(int index, GObject gObj)
	{
		if (base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow)
		{
			if (uINewGameLibraryWindow.label.selectedIndex == 1)
			{
				RendererHandCard(index, gObj);
			}
			else if (uINewGameLibraryWindow.label.selectedIndex == 5)
			{
				RendererEventCard(index, gObj);
			}
			gObj.visible = true;
			gObj.data = index;
		}
	}

	public void RendererHandCard(int index, GObject item)
	{
		if (!(item is UINewGameLibrary_Button_Card { com_card: var com_card }))
		{
			return;
		}
		UICom_Card card = com_card as UICom_Card;
		if (card == null || index < 0 || index > CardInfos.Count - 1)
		{
			return;
		}
		CardInfoConfigure cardInfoConfigure = CardInfos[index];
		string cardIndex = ((cardInfoConfigure.CardNumb == 0) ? "" : cardInfoConfigure.CardNumb.GetLocal(UIStringType.Card));
		string cardTips = ((cardInfoConfigure.CommentId == 0) ? "" : cardInfoConfigure.CommentId.GetLocal(UIStringType.Card));
		string text = ((cardInfoConfigure.NameID == 0) ? "" : cardInfoConfigure.NameID.GetLocal(UIStringType.Card));
		string content = ((cardInfoConfigure.DescId == 0) ? "" : cardInfoConfigure.DescId.GetLocal(UIStringType.Card));
		CardView mainPlayerCardView = cardInfoConfigure.GetMainPlayerCardView();
		CommonUIManager.RendererCard(card, mainPlayerCardView, text, content, cardIndex, cardTips, cardInfoConfigure.Cost, cardInfoConfigure.CardType, cardInfoConfigure.CardTargetType, _showFront: true, showRedPoint: true);
		if (mainPlayerCardView.CardType == 2)
		{
			card.HideDescState.Play();
			card.RegisterLongPressEvent(delegate
			{
				isBlockHandCardBtnEventOnce = false;
			}, delegate
			{
				card.Cut__In.Stop();
				card.Cut_out.Play();
			}, delegate
			{
				isBlockHandCardBtnEventOnce = true;
				card.Cut_out.Stop();
				card.Cut__In.Play();
			}, 0f, 0.3f);
		}
		else
		{
			card.Cut__In.Stop();
			card.Cut_out.Stop();
			card.ShowDescState.Play();
			card.UnRegisterLongPressEvent();
		}
	}

	private void RefreshEventInfo()
	{
		if (base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow)
		{
			EventInfos.Clear();
			EventInfos.AddRange(StaticConfigure.Event.Infos);
			uINewGameLibraryWindow.com_Cords.list_Cards.scrollPane.percY = 0f;
			uINewGameLibraryWindow.com_Cords.list_Cards.numItems = EventInfos.Count;
			RefreshTransition(uINewGameLibraryWindow.com_Cords.list_Cards._children);
		}
	}

	public void RendererEventCard(int index, GObject item)
	{
		if (item is UINewGameLibrary_Button_Card { com_card: UICom_Card com_card } && index >= 0 && index <= EventInfos.Count - 1)
		{
			EventInfoConfigure eventInfoConfigure = EventInfos[index];
			string cardIndex = ((eventInfoConfigure.CardNumb == 0) ? "" : eventInfoConfigure.CardNumb.GetLocal(UIStringType.Event));
			string cardTips = ((eventInfoConfigure.CommentId == 0) ? "" : eventInfoConfigure.CommentId.GetLocal(UIStringType.Event));
			string text = ((eventInfoConfigure.NameID == 0) ? "" : eventInfoConfigure.NameID.GetLocal(UIStringType.Event));
			string content = ((eventInfoConfigure.DescId == 0) ? "" : eventInfoConfigure.DescId.GetLocal(UIStringType.Event));
			CommonUIManager.RendererCard(com_card, eventInfoConfigure.GetImage(), text, content, cardIndex, cardTips, 0, eventInfoConfigure.CardType);
			com_card.ShowDescState.Play();
			com_card.UnRegisterLongPressEvent();
		}
	}

	private void RefreshLandInfo()
	{
		if (!(base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow))
		{
			return;
		}
		LandInfos.Clear();
		foreach (LandInfoConfigure info in StaticConfigure.Land.Infos)
		{
			if (info.IsGalleryShow)
			{
				LandInfos.Add(info);
			}
		}
		uINewGameLibraryWindow.com_Lands.list_Lands.scrollPane.percY = 0f;
		uINewGameLibraryWindow.com_Lands.list_Lands.numItems = LandInfos.Count;
		RefreshTransition(uINewGameLibraryWindow.com_Lands.list_Lands._children);
	}

	public void OnLandRenderer(int index, GObject gObj)
	{
		if (gObj is UICom_LandCard uICom_LandCard && index >= 0 && index <= StaticConfigure.Land.Infos.Count - 1)
		{
			LandInfoConfigure landInfoConfigure = LandInfos[index];
			uICom_LandCard.loader_FrontCard.url = landInfoConfigure.LandIcon;
			uICom_LandCard.txt_Name.text = landInfoConfigure.NameID.GetLocal(UIStringType.Land);
			uICom_LandCard.txt_Content.text = landInfoConfigure.DescriptionID.GetLocal(UIStringType.Land);
		}
	}

	private void RefreshRelicInfo()
	{
		if (base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow)
		{
			RelicInfos.Clear();
			RelicInfos.AddRange(StaticConfigure.Relic.Infos);
			RelicInfos.Sort((RelicInfoConfigure relicX, RelicInfoConfigure relicY) => (relicX.OrderWeight != relicY.OrderWeight) ? relicX.OrderWeight.CompareTo(relicY.OrderWeight) : relicX.Id.CompareTo(relicY.Id));
			uINewGameLibraryWindow.com_Relic.list_Relics.scrollPane.percY = 0f;
			uINewGameLibraryWindow.com_Relic.list_Relics.numItems = RelicInfos.Count;
		}
	}

	public void OnRelicRenderer(int index, GObject item)
	{
		GComponent gComponent = base.contentPane;
		UINewGameLibraryWindow win = gComponent as UINewGameLibraryWindow;
		if (win == null)
		{
			return;
		}
		UINewGameLibrary_Button_RelicItem relicItem = item as UINewGameLibrary_Button_RelicItem;
		if (relicItem == null || index < 0 || index >= RelicInfos.Count)
		{
			return;
		}
		relicItem.cutIn.Play();
		RelicInfoConfigure relicConfig = RelicInfos[index];
		relicItem.txt_tltle.text = relicConfig.NameID.GetLocal(UIStringType.Relic);
		relicItem.txt_Desc.text = relicConfig.DescID.GetLocal(UIStringType.Relic);
		((UICom_RelicQuality_Large)relicItem.loader_Frame).qualityType.selectedIndex = (int)relicConfig.RelicQualityType;
		relicItem.loader_Icon.url = relicConfig.Icon;
		relicItem.status.selectedIndex = 0;
		relicItem.onClick.Set((EventCallback0)delegate
		{
			relicItem.onClick.Retain();
			win.com_Relic.list_Relics.ScrollToView(index, ani: true, setFirst: true);
			if (CurSelectRelicItem != null && CurSelectRelicItem != relicItem)
			{
				CurSelectRelicItem.status.selectedIndex = 0;
			}
			CurSelectRelicItem = relicItem;
			CurSelectRelicItem.status.selectedIndex = 1;
			RefreshKeywords(relicConfig.KeyWordType, relicItem);
			relicItem.onClick.Release();
		});
	}

	private void RefreshKeywords(RelicKeyWordType KeyWordType, UINewGameLibrary_Button_RelicItem relicItem)
	{
		if (StaticConfigure.Relic.KeywordsDict.TryGetValue((int)KeyWordType, out var value))
		{
			((UICom_RelicKeyword)relicItem.com_Keyword).Refresh(value);
		}
		else
		{
			relicItem.com_Keyword.visible = false;
		}
	}

	private void OnClickHandCardItem(EventContext context)
	{
		if (!(base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow) || !(context.data is UINewGameLibrary_Button_Card uINewGameLibrary_Button_Card))
		{
			return;
		}
		if (isBlockHandCardBtnEventOnce)
		{
			isBlockHandCardBtnEventOnce = false;
			return;
		}
		int index = (int)uINewGameLibrary_Button_Card.data;
		if (uINewGameLibraryWindow.label.selectedIndex == 1)
		{
			SimpleSingletonProvider<UIManager>.inst.displayCard.TryShowHandCard(CardInfos, index);
		}
		else if (uINewGameLibraryWindow.label.selectedIndex == 5)
		{
			SimpleSingletonProvider<UIManager>.inst.displayCard.TryShowEventCard(EventInfos, index);
		}
	}

	public void RefreshTransition(List<GObject> ListCard)
	{
		for (int i = 0; i < ListCard.Count; i++)
		{
			GObject gObject = ListCard[i];
			UINewGameLibrary_Button_Card cardItem1 = gObject as UINewGameLibrary_Button_Card;
			if (cardItem1 != null)
			{
				cardItem1.visible = false;
				cardItem1.Cut_in.Play(1, 0.01f * (float)i, delegate
				{
					cardItem1.visible = true;
				}, null);
				continue;
			}
			gObject = ListCard[i];
			UINewGameLibrary_Button_RelicItem cardItem2 = gObject as UINewGameLibrary_Button_RelicItem;
			if (cardItem2 != null)
			{
				cardItem2.visible = false;
				cardItem2.cutIn.Play(1, 0.01f * (float)i, delegate
				{
					cardItem2.visible = true;
				}, null);
				continue;
			}
			gObject = ListCard[i];
			UICom_LandCard cardItem3 = gObject as UICom_LandCard;
			if (cardItem3 != null)
			{
				cardItem3.visible = false;
				cardItem3.Cut_in.Play(1, 0.01f * (float)i, delegate
				{
					cardItem3.visible = true;
				}, null);
			}
		}
	}

	private void SetText()
	{
		if (base.contentPane is UINewGameLibraryWindow uINewGameLibraryWindow)
		{
			uINewGameLibraryWindow.com_Lands.Search_Input.promptText = 1112.GetLocal(UIStringType.Message);
			uINewGameLibraryWindow.com_Relic.Search_Input.promptText = 1112.GetLocal(UIStringType.Message);
			uINewGameLibraryWindow.com_Cords.Search_Input.promptText = 1112.GetLocal(UIStringType.Message);
			uINewGameLibraryWindow.com_Lands.Text_NoSearch.text = 1113.GetLocal(UIStringType.Message);
			uINewGameLibraryWindow.com_Relic.Text_NoSearch.text = 1113.GetLocal(UIStringType.Message);
			uINewGameLibraryWindow.com_Cords.Text_NoSearch.text = 1113.GetLocal(UIStringType.Message);
			uINewGameLibraryWindow.Btn_Map.Text_des.text = 1107.GetLocal(UIStringType.Message);
			uINewGameLibraryWindow.Btn_Code.Text_des.text = 1108.GetLocal(UIStringType.Message);
			uINewGameLibraryWindow.Btn_Event.Text_des.text = 1111.GetLocal(UIStringType.Message);
			uINewGameLibraryWindow.Btn_Land.Text_des.text = 1110.GetLocal(UIStringType.Message);
			uINewGameLibraryWindow.Btn_Relic.Text_des.text = 1109.GetLocal(UIStringType.Message);
		}
	}
}
