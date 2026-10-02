using System.Collections.Generic;
using Core;
using Core.Unit;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UI;

public class UINewGameLibrary_Com_MapTab : GComponent
{
	public class GameLibraryMapData
	{
		public List<LandNode> nodes = new List<LandNode>();
	}

	private MapInfoConfigure _curMapInfo;

	private const int RelicTab = 0;

	private const int CardTab = 1;

	private const int LandTab = 2;

	private const int EventTab = 3;

	private const int Exclusive = 0;

	private const int NoExclusive = 1;

	private int _CurMapType;

	private int _CurIndex;

	private NewGameLibraryWindow _curWin;

	public Controller tab;

	public Controller Iscom;

	public Controller HaveExclusive;

	public GButton Btn_Exclusive;

	public GButton Btn_Common;

	public GList list_Cards;

	public GList list_Relics;

	public GList list_Lands;

	public const string URL = "ui://mc0y3plupj0z2m";

	public void InitComponent()
	{
		list_Cards.SetVirtual();
		list_Cards.itemRenderer = OnCardRenderer;
		list_Relics.SetVirtual();
		list_Lands.SetVirtual();
	}

	public void AddEvent()
	{
		Btn_Common.onClick.Add(RefreshPage);
		list_Cards.onClickItem.Add(OnClickHandCardItem);
		Btn_Exclusive.onClick.Add(RefreshPage);
	}

	public void RemoveEvent()
	{
		Btn_Common.onClick.Remove(RefreshPage);
		Btn_Exclusive.onClick.Remove(RefreshPage);
		list_Cards.onClickItem.Remove(OnClickHandCardItem);
	}

	private void RefreshPage()
	{
		Btn_Exclusive.onClick.Retain();
		Btn_Common.onClick.Retain();
		OnCurIndexToRefresh();
		Btn_Common.onClick.Release();
		Btn_Exclusive.onClick.Release();
	}

	public void RefreshOtherPage(MapInfoConfigure curMapInfo, int index, int curMapType, NewGameLibraryWindow curWin)
	{
		if (curWin == null)
		{
			return;
		}
		_curWin = curWin;
		list_Relics.itemRenderer = _curWin.OnRelicRenderer;
		list_Lands.itemRenderer = _curWin.OnLandRenderer;
		_curMapInfo = curMapInfo;
		_CurMapType = curMapType;
		if (_CurMapType == 0)
		{
			RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
			if (roomController != null && roomController.IsBattle())
			{
				_CurMapType = ((!roomController.localRoom.IsPractice()) ? roomController.localRoom.MapType : ((!roomController.localRoom.IsPVE()) ? 1 : 4));
			}
		}
		_CurIndex = index;
		Iscom.selectedIndex = 0;
		OnCurIndexToRefresh();
	}

	private void OnCurIndexToRefresh()
	{
		if (_CurIndex == 2)
		{
			if (_curMapInfo.MapSpecialRelic.Count > 0)
			{
				HaveExclusive.selectedIndex = 0;
			}
			else
			{
				HaveExclusive.selectedIndex = 1;
				Iscom.selectedIndex = 1;
			}
			RefreshRelicInfo();
			tab.selectedIndex = 0;
		}
		else if (_CurIndex == 3)
		{
			if (_curMapInfo.MapSpecialCard.Count > 0)
			{
				HaveExclusive.selectedIndex = 0;
			}
			else
			{
				HaveExclusive.selectedIndex = 1;
				Iscom.selectedIndex = 1;
			}
			RefreshCardInfo();
			tab.selectedIndex = 1;
		}
		else if (_CurIndex == 4)
		{
			if (_curMapInfo.SpLand != LandType.None)
			{
				HaveExclusive.selectedIndex = 0;
			}
			else
			{
				HaveExclusive.selectedIndex = 1;
				Iscom.selectedIndex = 1;
			}
			RefreshLandInfo();
			tab.selectedIndex = 2;
		}
		else if (_CurIndex == 5)
		{
			if (_curMapInfo.MapSpecialEvent.Count > 0)
			{
				HaveExclusive.selectedIndex = 0;
			}
			else
			{
				HaveExclusive.selectedIndex = 1;
				Iscom.selectedIndex = 1;
			}
			RefreshEventInfo();
			tab.selectedIndex = 3;
		}
	}

	private void RefreshEventInfo()
	{
		_curWin.EventInfos.Clear();
		int key = _curMapInfo.Id * 1000 + _CurMapType;
		if (StaticConfigure.Map.MapPoolDict.TryGetValue(key, out var value))
		{
			if (StaticConfigure.Event.PeriodDict.TryGetValue(value.EventPool, out var value2))
			{
				RepeatedField<EventPeriodConfigureItem> eventPeriodConfigureItems = value2.EventPeriodConfigureItems;
				if (Iscom.selectedIndex == 0)
				{
					foreach (int item in _curMapInfo.MapSpecialEvent)
					{
						if (StaticConfigure.Event.InfoDict.TryGetValue(item, out var value3))
						{
							_curWin.EventInfos.Add(value3);
						}
					}
				}
				else
				{
					foreach (EventPeriodConfigureItem item2 in eventPeriodConfigureItems)
					{
						if ((_curMapInfo.MapSpecialEvent.Count == 0 || !_curMapInfo.MapSpecialEvent.Contains(item2.EventId)) && StaticConfigure.Event.InfoDict.TryGetValue(item2.EventId, out var value4))
						{
							_curWin.EventInfos.Add(value4);
						}
					}
				}
			}
			else
			{
				Debug.LogError($"通过地图Id:{_curMapInfo.Id} 无法在Card.EffectPoolDict中获取数据");
			}
		}
		list_Cards.scrollPane.percY = 0f;
		list_Cards.numItems = _curWin.EventInfos.Count;
		_curWin.RefreshTransition(list_Cards._children);
	}

	private void RefreshRelicInfo()
	{
		_curWin.RelicInfos.Clear();
		if (Iscom.selectedIndex == 0)
		{
			foreach (int item in _curMapInfo.MapSpecialRelic)
			{
				if (StaticConfigure.Relic.InfoDict.TryGetValue(item, out var value))
				{
					_curWin.RelicInfos.Add(value);
				}
			}
		}
		else
		{
			foreach (RelicInfoConfigure info in StaticConfigure.Relic.Infos)
			{
				if (info.MapLimit.Count == 0)
				{
					_curWin.RelicInfos.Add(info);
				}
			}
		}
		_curWin.RelicInfos.Sort((RelicInfoConfigure relicX, RelicInfoConfigure relicY) => (relicX.OrderWeight != relicY.OrderWeight) ? relicX.OrderWeight.CompareTo(relicY.OrderWeight) : relicX.Id.CompareTo(relicY.Id));
		list_Relics.scrollPane.percY = 0f;
		list_Relics.numItems = _curWin.RelicInfos.Count;
	}

	private async void RefreshLandInfo()
	{
		_curWin.LandInfos.Clear();
		if (Iscom.selectedIndex == 0)
		{
			if (StaticConfigure.Land.InfoDict.TryGetValue((int)_curMapInfo.SpLand, out var value))
			{
				_curWin.LandInfos.Add(value);
			}
		}
		else
		{
			RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
			if (roomController != null && roomController.IsBattle())
			{
				foreach (KeyValuePair<int, UnitLand> item in SimpleSingletonProvider<LandManager>.inst.NodeDict)
				{
					if (StaticConfigure.Land.InfoDict.TryGetValue((int)item.Value.LandType, out var value2) && value2.IsGalleryShow && value2.LandType != _curMapInfo?.SpLand && !_curWin.LandInfos.Contains(value2))
					{
						_curWin.LandInfos.Add(value2);
					}
				}
			}
			else
			{
				string sceneName = "";
				int key = ((_curMapInfo.Id != 82012) ? _curMapInfo.Mids[0] : _curMapInfo.Mids[2]);
				if (StaticConfigure.Map.SceneDict.TryGetValue(key, out var value3))
				{
					sceneName = value3.AssetName.Split("_")[1];
				}
				else
				{
					Debug.LogError($"Map.SceneDict中找不到地图ID{_curMapInfo.Mids[0]}的配置");
				}
				GameLibraryMapData gameLibraryMapData = await LoadSceneData(sceneName);
				if (gameLibraryMapData == null || gameLibraryMapData.nodes.Count == 0)
				{
					return;
				}
				foreach (LandNode node in gameLibraryMapData.nodes)
				{
					if (StaticConfigure.Land.InfoDict.TryGetValue((int)node.landType, out var value4) && value4.IsGalleryShow && value4.LandType != _curMapInfo?.SpLand && !value4.IsSPLand && !_curWin.LandInfos.Contains(value4))
					{
						_curWin.LandInfos.Add(value4);
					}
				}
			}
		}
		_curWin.LandInfos.Sort((LandInfoConfigure LandX, LandInfoConfigure LandY) => LandX.LandType.CompareTo(LandY.LandType));
		list_Lands.scrollPane.percY = 0f;
		list_Lands.numItems = _curWin.LandInfos.Count;
	}

	private void RefreshCardInfo()
	{
		_curWin.CardInfos.Clear();
		if (Iscom.selectedIndex == 0)
		{
			foreach (int item in _curMapInfo.MapSpecialCard)
			{
				if (StaticConfigure.Card.InfoDict.TryGetValue(item, out var value))
				{
					_curWin.CardInfos.Add(value);
				}
			}
		}
		else
		{
			int key = _curMapInfo.Id * 1000 + _CurMapType;
			if (StaticConfigure.Map.MapPoolDict.TryGetValue(key, out var value2))
			{
				if (StaticConfigure.Card.BattlePoolDict.TryGetValue(value2.BattleCardPool, out var value3))
				{
					RepeatedField<CardBattlePoolConfigureItem> cardBattlePoolConfigureItems = value3.CardBattlePoolConfigureItems;
					for (int i = 0; i < cardBattlePoolConfigureItems.Count; i++)
					{
						CardInfoConfigure cardConfigure = cardBattlePoolConfigureItems[i].CardId.GetCardConfigure();
						if (cardConfigure.IsGalleryShow)
						{
							_curWin.CardInfos.Add(cardConfigure);
						}
					}
				}
				if (StaticConfigure.Card.EffectPoolDict.TryGetValue(value2.EffectCardPool, out var value4))
				{
					RepeatedField<CardEffectPoolConfigureItem> cardEffectPoolConfigureItems = value4.CardEffectPoolConfigureItems;
					for (int j = 0; j < cardEffectPoolConfigureItems.Count; j++)
					{
						CardInfoConfigure cardConfigure2 = cardEffectPoolConfigureItems[j].CardId.GetCardConfigure();
						if (cardConfigure2.IsGalleryShow)
						{
							_curWin.CardInfos.Add(cardConfigure2);
						}
					}
				}
			}
		}
		list_Cards.scrollPane.percY = 0f;
		list_Cards.numItems = _curWin.CardInfos.Count;
		_curWin.RefreshTransition(list_Cards._children);
	}

	private void OnCardRenderer(int index, GObject gObj)
	{
		if (_CurIndex == 3)
		{
			_curWin.RendererHandCard(index, gObj);
		}
		else if (_CurIndex == 5)
		{
			_curWin.RendererEventCard(index, gObj);
		}
		gObj.visible = true;
		gObj.data = index;
	}

	private void OnClickHandCardItem(EventContext context)
	{
		if (_curWin != null && context.data is UINewGameLibrary_Button_Card uINewGameLibrary_Button_Card)
		{
			int index = (int)uINewGameLibrary_Button_Card.data;
			if (tab.selectedIndex == 1)
			{
				SimpleSingletonProvider<UIManager>.inst.displayCard.TryShowHandCard(_curWin.CardInfos, index);
			}
			else if (tab.selectedIndex == 3)
			{
				SimpleSingletonProvider<UIManager>.inst.displayCard.TryShowEventCard(_curWin.EventInfos, index);
			}
		}
	}

	private async UniTask<GameLibraryMapData> LoadSceneData(string sceneName)
	{
		if (string.IsNullOrEmpty(sceneName))
		{
			return null;
		}
		AsyncOperationHandle<TextAsset> handle = await AddressableHelper.LoadAssetAsync<TextAsset>(sceneName);
		if (handle.IsDone)
		{
			GameLibraryMapData result = JsonUtility.FromJson<GameLibraryMapData>(handle.Result.text);
			Addressables.Release(handle);
			return result;
		}
		Addressables.Release(handle);
		return null;
	}

	public static UINewGameLibrary_Com_MapTab CreateInstance()
	{
		return (UINewGameLibrary_Com_MapTab)UIPackage.CreateObject("NewGameLibrary", "NewGameLibrary_Com_MapTab");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tab = GetControllerAt(0);
		Iscom = GetControllerAt(1);
		HaveExclusive = GetControllerAt(2);
		Btn_Exclusive = (GButton)GetChildAt(0);
		Btn_Common = (GButton)GetChildAt(1);
		list_Cards = (GList)GetChildAt(2);
		list_Relics = (GList)GetChildAt(3);
		list_Lands = (GList)GetChildAt(4);
	}
}
