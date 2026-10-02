using System;
using System.Collections.Generic;
using Core.Net;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class UICom_CreateRoom : GComponent
{
	private MapModeType _MapModeType;

	private Action _CloseAction;

	private List<MapEventInfoConfigure> _MapEventInfoConfigures;

	private MapInfoConfigure _CurMapInfo;

	private List<int> _MonsterConfigIds;

	private int _MonsterIndex;

	public Controller InfoType;

	public UICom_RoomSetting com_RoomSetting;

	public GButton btn_ReturnList;

	public GButton btn_CreateRoom;

	public UICom_MapInfo com_MapInfo;

	public GComponent com_File;

	public GButton btn_Next;

	public GButton btn_Previous;

	public UIButton_MapInfoSelect btn_MapInfo;

	public UIButton_MapInfoSelect btn_MonsterInfo;

	public const string URL = "ui://m6sn3r22px78j9h";

	public void CreateRoom_InitComponents()
	{
		com_RoomSetting.InitComponents();
		com_MapInfo.list_MapEvent.itemRenderer = RenderMapEvent;
	}

	public void CreateRoom_Refresh(MapModeType mapModeType, int MapId, Action closeAction = null)
	{
		_MapModeType = mapModeType;
		_CloseAction = closeAction;
		com_RoomSetting.Create_Refresh(mapModeType, MapId);
		RefreshMapInfo();
		ReleaseButton();
	}

	public void CreateRoom_AddEvent()
	{
		btn_ReturnList.onClick.Add(OpenRoomList);
		btn_CreateRoom.onClick.Add(RequestCreateRoom);
		com_RoomSetting.AddEvent();
		btn_MapInfo.onClick.Add(RefreshMapEventInfo);
		btn_MonsterInfo.onClick.Add(RefreshMapMonsterFile);
		btn_Next.onClick.Add(ShowNextMonster);
		btn_Previous.onClick.Add(ShowPreviousMonster);
	}

	public void CreateRoom_RemoveEvent()
	{
		btn_ReturnList.onClick.Remove(OpenRoomList);
		btn_CreateRoom.onClick.Remove(RequestCreateRoom);
		com_RoomSetting.RemoveEvent();
		btn_MapInfo.onClick.Remove(RefreshMapEventInfo);
		btn_MonsterInfo.onClick.Remove(RefreshMapMonsterFile);
		btn_Next.onClick.Remove(ShowNextMonster);
		btn_Previous.onClick.Remove(ShowPreviousMonster);
	}

	public void CreateRoom_AddListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.roomList.signal.roomChange.AddListener(RefreshMapInfo);
	}

	public void CreateRoom_RemoveListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.roomList.signal.roomChange.RemoveListener(RefreshMapInfo);
	}

	public void CreateRoom_Close()
	{
		com_RoomSetting.Close();
	}

	public override void Dispose()
	{
		com_RoomSetting.Dispose();
		base.Dispose();
	}

	private void OpenRoomList()
	{
		btn_ReturnList.onClick.Retain();
		_CloseAction?.Invoke();
		btn_ReturnList.onClick.Release();
	}

	public void RequestCreateRoom()
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.StartGameLicense() || !SimpleSingletonProvider<GameLogicManager>.inst.match.CheckOperateForMatch())
		{
			return;
		}
		btn_CreateRoom.onClick.Retain();
		int mapId = com_RoomSetting.GetCurrentMap();
		int currentThinkTime = com_RoomSetting.GetCurrentThinkTime();
		int currentCondition = com_RoomSetting.GetCurrentCondition();
		int currentGameSpeed = com_RoomSetting.GetCurrentGameSpeed();
		int currentDifficultyIndex = com_RoomSetting.GetCurrentDifficultyIndex();
		bool skipStoryStatus = com_RoomSetting.GetSkipStoryStatus();
		int roomLabelIndex = com_RoomSetting.GetRoomLabelIndex();
		ulong steamLobbyId = 0uL;
		SimpleSingletonProvider<GameLogicManager>.inst.room.RequestCreateC2S(SimpleSingletonProvider<GameLogicManager>.inst.account.GetName(), com_RoomSetting.txtField_PSW.inputTextField.text, mapId, 0, currentCondition, currentThinkTime, steamLobbyId, currentGameSpeed, (int)_MapModeType, currentDifficultyIndex, skipStoryStatus, roomLabelIndex).OnFinished.AddOnce(delegate(RPCAsyncResult result)
		{
			if (result.errId != 0 && _MapModeType != MapModeType.None)
			{
				com_RoomSetting.Create_Refresh(_MapModeType, mapId);
			}
			ReleaseButton();
		});
	}

	private void ReleaseButton()
	{
		btn_CreateRoom.onClick.Release();
	}

	private void RefreshMapInfo()
	{
		int currentMap = com_RoomSetting.GetCurrentMap();
		if (!StaticConfigure.Map.InfoDict.TryGetValue(currentMap, out _CurMapInfo))
		{
			Debug.LogError($"Map.InfoDict中找不到地图ID{currentMap}的配置");
			return;
		}
		btn_MapInfo.title = 1068.GetLocal(UIStringType.Message);
		btn_MonsterInfo.title = 1069.GetLocal(UIStringType.Message);
		List<int> configMonsterIds = GetConfigMonsterIds();
		btn_MonsterInfo.visible = configMonsterIds != null && configMonsterIds.Count > 0;
		btn_MapInfo.onClick.Call();
	}

	private void RefreshMapEventInfo()
	{
		if (_CurMapInfo == null)
		{
			return;
		}
		InfoType.selectedIndex = 0;
		com_MapInfo.loader_Map.url = _CurMapInfo.MapImage;
		com_MapInfo.list_MapEventSelect.visible = _CurMapInfo.DifficultyIds.Count > 1;
		if (_CurMapInfo.DifficultyIds.Count == 0)
		{
			_MapEventInfoConfigures = _CurMapInfo.MapEventInfoConfigures;
		}
		else
		{
			if (_CurMapInfo.DifficultyIds.Count > 1)
			{
				com_MapInfo.list_MapEventSelect.itemRenderer = delegate(int index, GObject item)
				{
					if (item is UIButton_MapEventSelect uIButton_MapEventSelect)
					{
						uIButton_MapEventSelect.title = (index + 1).ToString();
						uIButton_MapEventSelect.data = index;
						uIButton_MapEventSelect.selected = index == 0;
					}
				};
				com_MapInfo.list_MapEventSelect.numItems = _CurMapInfo.DifficultyIds.Count;
				com_MapInfo.list_MapEventSelect.onClickItem.Set(delegate(EventContext context)
				{
					if (context.data is UIButton_MapEventSelect { data: var obj } && obj is int index)
					{
						ReadyMapEventConfigs(index);
						RefreshMapEventList();
					}
				});
			}
			ReadyMapEventConfigs(0);
		}
		RefreshMapEventList();
	}

	private void ReadyMapEventConfigs(int index)
	{
		int currentDifficultyIndex = com_RoomSetting.GetCurrentDifficultyIndex();
		RepeatedField<MapGameDifficultyConfigureItem> mapGameDifficultyItems = _CurMapInfo.DifficultyIds[index].GetMapGameDifficultyItems();
		for (int i = 0; i < mapGameDifficultyItems.Count; i++)
		{
			if (mapGameDifficultyItems[i].Index == currentDifficultyIndex)
			{
				_MapEventInfoConfigures = mapGameDifficultyItems[i].MapEventInfoConfigures;
			}
		}
	}

	private void RefreshMapEventList()
	{
		int num = Mathf.Max(_MapEventInfoConfigures?.Count ?? 3, 3);
		com_MapInfo.list_MapEvent.numItems = num;
		com_MapInfo.list_MapEvent.scrollPane.touchEffect = num > 3;
		com_MapInfo.list_MapEvent.scrollPane.percX = 0f;
	}

	private void RenderMapEvent(int index, GObject item)
	{
		UIButton_RoomMapCard btn_RoomMapEvent = item as UIButton_RoomMapCard;
		if (btn_RoomMapEvent == null)
		{
			return;
		}
		if (_MapEventInfoConfigures == null || _MapEventInfoConfigures.Count <= index)
		{
			btn_RoomMapEvent.showInfo.selectedIndex = 0;
			return;
		}
		btn_RoomMapEvent.showInfo.selectedIndex = 1;
		MapEventMapEventCardConfigure mapEventCardConfigure = _MapEventInfoConfigures[index].MapEventCardConfigure;
		string cardIndex = ((mapEventCardConfigure.CardNumb == 0) ? "" : mapEventCardConfigure.CardNumb.GetLocal(UIStringType.MapEvent));
		string cardTips = ((mapEventCardConfigure.CommentId == 0) ? "" : mapEventCardConfigure.CommentId.GetLocal(UIStringType.MapEvent));
		CommonUIManager.RendererCard(btn_RoomMapEvent.com_card as UICom_Card, mapEventCardConfigure.GetImage(), mapEventCardConfigure.NameID.GetLocal(UIStringType.MapEvent), mapEventCardConfigure.DescId.GetLocal(UIStringType.MapEvent), cardIndex, cardTips, 0, mapEventCardConfigure.CardType);
		btn_RoomMapEvent.onClick.Set((EventCallback0)delegate
		{
			if (_MapEventInfoConfigures.Count > index)
			{
				btn_RoomMapEvent.onClick.Retain();
				SimpleSingletonProvider<UIManager>.inst.displayCard.TryShowMapEvent(index, _MapEventInfoConfigures);
				btn_RoomMapEvent.onClick.Release();
			}
		});
	}

	private void RefreshMapMonsterFile()
	{
		if (_CurMapInfo == null)
		{
			return;
		}
		InfoType.selectedIndex = 1;
		_MonsterConfigIds = new List<int>();
		List<int> configMonsterIds = GetConfigMonsterIds();
		if (configMonsterIds != null && configMonsterIds.Count > 0)
		{
			foreach (int item in configMonsterIds)
			{
				if (CharacterHandle.GetCharacterType(item) == CharacterType.Monster && CharacterHandle.GetCharacterIsGalleryShow(item))
				{
					_MonsterConfigIds.Add(item);
				}
			}
		}
		_MonsterIndex = 0;
		RendererMonsterFile();
		GButton gButton = btn_Next;
		bool flag = (btn_Previous.visible = _MonsterConfigIds.Count > 1);
		gButton.visible = flag;
	}

	private void RendererMonsterFile()
	{
		if (_MonsterConfigIds != null && _MonsterIndex >= 0 && _MonsterIndex <= _MonsterConfigIds.Count - 1)
		{
			int monsterId = _MonsterConfigIds[_MonsterIndex];
			int currentDifficultyIndex = com_RoomSetting.GetCurrentDifficultyIndex();
			((UICom_CharacterFile)com_File).RenderMonster(monsterId, currentDifficultyIndex);
		}
	}

	private void ShowNextMonster(EventContext context)
	{
		if (_MonsterConfigIds != null)
		{
			btn_Next.onClick.Retain();
			_MonsterIndex = ((_MonsterIndex < _MonsterConfigIds.Count - 1) ? (_MonsterIndex + 1) : 0);
			RendererMonsterFile();
			btn_Next.onClick.Release();
		}
	}

	private void ShowPreviousMonster(EventContext context)
	{
		if (_MonsterConfigIds != null)
		{
			btn_Previous.onClick.Retain();
			_MonsterIndex = ((_MonsterIndex == 0) ? (_MonsterConfigIds.Count - 1) : (_MonsterIndex - 1));
			RendererMonsterFile();
			btn_Previous.onClick.Release();
		}
	}

	private List<int> GetConfigMonsterIds()
	{
		if (_CurMapInfo == null)
		{
			return null;
		}
		List<int> list = new List<int>();
		list.AddRange(_CurMapInfo.PreloadCharacterIds);
		RepeatedField<int> difficultyIds = _CurMapInfo.DifficultyIds;
		int currentDifficultyIndex = com_RoomSetting.GetCurrentDifficultyIndex();
		if (difficultyIds != null && difficultyIds.Count > 0)
		{
			foreach (int item in difficultyIds)
			{
				RepeatedField<MapGameDifficultyConfigureItem> mapGameDifficultyItems = item.GetMapGameDifficultyItems();
				for (int i = 0; i < mapGameDifficultyItems.Count; i++)
				{
					if (mapGameDifficultyItems[i].Index != currentDifficultyIndex)
					{
						continue;
					}
					foreach (int preloadCharacterId in mapGameDifficultyItems[i].PreloadCharacterIds)
					{
						if (!list.Contains(preloadCharacterId))
						{
							list.Add(preloadCharacterId);
						}
					}
					break;
				}
			}
		}
		return list;
	}

	public static UICom_CreateRoom CreateInstance()
	{
		return (UICom_CreateRoom)UIPackage.CreateObject("Common_External", "Com_CreateRoom");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		InfoType = GetControllerAt(0);
		com_RoomSetting = (UICom_RoomSetting)GetChildAt(1);
		btn_ReturnList = (GButton)GetChildAt(2);
		btn_CreateRoom = (GButton)GetChildAt(3);
		com_MapInfo = (UICom_MapInfo)GetChildAt(4);
		com_File = (GComponent)GetChildAt(5);
		btn_Next = (GButton)GetChildAt(6);
		btn_Previous = (GButton)GetChildAt(7);
		btn_MapInfo = (UIButton_MapInfoSelect)GetChildAt(10);
		btn_MonsterInfo = (UIButton_MapInfoSelect)GetChildAt(11);
	}
}
