using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class UINewGameLibrary_Map : GComponent
{
	private float autoScrollTime;

	private bool startScrollStatus;

	private List<MapEventInfoConfigure> _MapEventInfoConfigures = new List<MapEventInfoConfigure>();

	private readonly List<int> _monsterConfigIds = new List<int>();

	private readonly List<string> _monsterItemsConfigIds = new List<string>();

	private const int NoMonsterAndRelic = 3;

	private const int NoRelic = 2;

	private const int NoMonster = 1;

	private const int All = 0;

	private readonly List<int> _mapConfigIds = new List<int>();

	private int _CurMapIndex;

	private readonly List<GameModeGalleryMapConfigure> gameModeInfos = new List<GameModeGalleryMapConfigure>();

	private int curType;

	private MapInfoConfigure _curMapInfo;

	private NewGameLibraryWindow _CurWin;

	private bool _isDragging;

	private Vector2 _lastTouchPos;

	private const float RotateSpeed = 0.5f;

	public Controller State;

	public GList list_Maps;

	public GButton btn_Return;

	public GList list_GameMode;

	public UINewGameLibrary_Com_MapMain Com_main;

	public Transition Cut_in;

	public const string URL = "ui://mc0y3plupj0z1q";

	public void RefreshMapMainInfo(MapInfoConfigure curMapInfo, NewGameLibraryWindow win = null)
	{
		if (curMapInfo == null)
		{
			return;
		}
		if (_CurWin == null)
		{
			_CurWin = win;
		}
		if (_CurWin == null)
		{
			return;
		}
		_MapEventInfoConfigures.Clear();
		GetMapToMonster(curMapInfo);
		if (StaticConfigure.Map.SceneDict.TryGetValue(curMapInfo.Mids[0], out var value))
		{
			SimpleSingletonProvider<RenderTextureManager>.inst.ShowTargetModelRenderTexture(Com_main.MapModel, value.MiniMapName, LayerMask.GetMask("MiniMap"), Vector3.zero, new Vector3(0f, -0.85f, 5.2f), Quaternion.Euler(45f, 180f, 0f), Vector3.one * 2f, update: true).Forget();
		}
		if (curMapInfo.DifficultyIds.Count == 0)
		{
			_MapEventInfoConfigures = curMapInfo.MapEventInfoConfigures;
		}
		else
		{
			foreach (int difficultyId in curMapInfo.DifficultyIds)
			{
				foreach (MapGameDifficultyConfigureItem mapGameDifficultyItem in difficultyId.GetMapGameDifficultyItems())
				{
					foreach (MapEventInfoConfigure mapEventInfoConfigure in mapGameDifficultyItem.MapEventInfoConfigures)
					{
						if (!_MapEventInfoConfigures.Contains(mapEventInfoConfigure))
						{
							_MapEventInfoConfigures.Add(mapEventInfoConfigure);
						}
					}
				}
			}
		}
		_MapEventInfoConfigures.Sort((MapEventInfoConfigure MapEventX, MapEventInfoConfigure MapEventY) => MapEventX.Id.CompareTo(MapEventY.Id));
		RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
		if (roomController != null && roomController.IsBattle())
		{
			curType = (roomController.localRoom.IsPVE() ? 1 : 2);
		}
		bool flag = _monsterConfigIds.Count <= 0;
		bool flag2 = curType == 2;
		if (curType == 3)
		{
			int curMapType = GetCurMapType(curMapInfo.Id);
			if (BattleConfig.IsAsymmetricalBattle(curMapType))
			{
				flag = true;
				flag2 = true;
			}
			else if (BattleConfig.IsLuckyStarBattle(curMapType))
			{
				flag2 = true;
			}
		}
		if (flag)
		{
			Com_main.type.selectedIndex = ((!flag2) ? 1 : 3);
		}
		else if (flag2)
		{
			Com_main.type.selectedIndex = 2;
		}
		else
		{
			Com_main.type.selectedIndex = 0;
		}
		Com_main.com_TabAll.list_Items.numItems = _monsterItemsConfigIds.Count;
		startScrollStatus = true;
		Com_main.com_TabAll.com_Banner.list_Eventcard.numItems = _MapEventInfoConfigures.Count;
		Com_main.com_TabAll.text_title.text = curMapInfo.MapName.GetLocal(UIStringType.Map);
		Com_main.com_TabAll.com_Desc.text = curMapInfo.MapGallery.GetLocal(UIStringType.Map);
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (startScrollStatus && Com_main.com_TabAll.com_Banner.list_Eventcard.numItems > 1)
		{
			if (autoScrollTime >= 2f)
			{
				Com_main.com_TabAll.com_Banner.list_Eventcard.scrollPane.ScrollRight(1f, ani: true);
				autoScrollTime = 0f;
			}
			autoScrollTime += Time.deltaTime;
		}
	}

	private void StartAutoScroll(EventContext context)
	{
		Com_main.com_TabAll.com_Banner.list_Eventcard.onTouchEnd.Retain();
		autoScrollTime = 0f;
		startScrollStatus = true;
		Com_main.com_TabAll.com_Banner.list_Eventcard.onTouchEnd.Release();
	}

	private void StopAutoScroll(EventContext context)
	{
		Com_main.com_TabAll.com_Banner.list_Eventcard.onTouchBegin.Retain();
		autoScrollTime = 0f;
		startScrollStatus = false;
		Com_main.com_TabAll.com_Banner.list_Eventcard.onTouchBegin.Release();
	}

	private void GetMapToMonster(MapInfoConfigure curMapInfo)
	{
		_curMapInfo = curMapInfo;
		_monsterConfigIds.Clear();
		_monsterItemsConfigIds.Clear();
		List<int> configMonsterIds = GetConfigMonsterIds();
		RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
		if (roomController != null && roomController.IsBattle() && roomController.localRoom != null && roomController.localRoom.State == Room.Types.State.Running && roomController.localRoom.IsTerms)
		{
			foreach (int roomTerm in roomController.localRoom.RoomTerms)
			{
				MutatorInfoConfigure mutatorInfoConfigure = roomTerm.GetMutatorInfoConfigure();
				if (mutatorInfoConfigure == null || mutatorInfoConfigure.PreloadCharacterIds.Count == 0)
				{
					continue;
				}
				foreach (int preloadCharacterId in mutatorInfoConfigure.PreloadCharacterIds)
				{
					if (!configMonsterIds.Contains(preloadCharacterId))
					{
						configMonsterIds.Add(preloadCharacterId);
					}
				}
			}
		}
		if (configMonsterIds != null && configMonsterIds.Count > 0)
		{
			foreach (int item in configMonsterIds)
			{
				MonsterInfoConfigure monsterCharacterConfigure = CharacterHandle.GetMonsterCharacterConfigure(item);
				if (monsterCharacterConfigure != null && monsterCharacterConfigure.HeroType == CharacterType.Monster && monsterCharacterConfigure.IsGalleryShow)
				{
					_monsterConfigIds.Add(monsterCharacterConfigure.Id);
				}
			}
		}
		_monsterConfigIds.Sort(Comparison);
		foreach (int monsterConfigId in _monsterConfigIds)
		{
			MonsterInfoConfigure monsterCharacterConfigure2 = CharacterHandle.GetMonsterCharacterConfigure(monsterConfigId);
			if (monsterCharacterConfigure2 != null && !_monsterItemsConfigIds.Contains(monsterCharacterConfigure2.CharacterMap))
			{
				_monsterItemsConfigIds.Add(monsterCharacterConfigure2.CharacterMap);
			}
		}
	}

	private int Comparison(int MonsterIdX, int MonsterIdY)
	{
		if (!StaticConfigure.Monster.InfoDict.TryGetValue(MonsterIdX, out var value))
		{
			return 0;
		}
		if (!StaticConfigure.Monster.InfoDict.TryGetValue(MonsterIdY, out var value2))
		{
			return 0;
		}
		return value.OrderWeight.CompareTo(value2.OrderWeight);
	}

	private List<int> GetConfigMonsterIds()
	{
		if (_curMapInfo == null)
		{
			return null;
		}
		List<int> list = new List<int>();
		list.AddRange(_curMapInfo.PreloadCharacterIds);
		RepeatedField<int> difficultyIds = _curMapInfo.DifficultyIds;
		if (difficultyIds != null && difficultyIds.Count > 0)
		{
			foreach (int item in difficultyIds)
			{
				foreach (MapGameDifficultyConfigureItem mapGameDifficultyItem in item.GetMapGameDifficultyItems())
				{
					foreach (int preloadCharacterId in mapGameDifficultyItem.PreloadCharacterIds)
					{
						if (!list.Contains(preloadCharacterId))
						{
							list.Add(preloadCharacterId);
						}
					}
				}
			}
		}
		return list;
	}

	private void RefreshLabelItem(int index, GObject item)
	{
		if (item is UINewGameLibrary_Button_Card { com_card: UICom_Card com_card } && index >= 0 && index <= _MapEventInfoConfigures.Count - 1)
		{
			MapEventMapEventCardConfigure mapEventCardConfigure = _MapEventInfoConfigures[index].MapEventCardConfigure;
			string cardIndex = ((mapEventCardConfigure.CardNumb == 0) ? "" : mapEventCardConfigure.CardNumb.GetLocal(UIStringType.MapEvent));
			string cardTips = ((mapEventCardConfigure.CommentId == 0) ? "" : mapEventCardConfigure.CommentId.GetLocal(UIStringType.MapEvent));
			string text = ((mapEventCardConfigure.NameID == 0) ? "" : mapEventCardConfigure.NameID.GetLocal(UIStringType.MapEvent));
			string content = ((mapEventCardConfigure.DescId == 0) ? "" : mapEventCardConfigure.DescId.GetLocal(UIStringType.MapEvent));
			CommonUIManager.RendererCard(com_card, mapEventCardConfigure.GetImage(), text, content, cardIndex, cardTips, 0, mapEventCardConfigure.CardType);
		}
	}

	private void OnAllMonsterRenderer(int index, GObject item)
	{
		if (item is UINewGameLibrary_Com_NewMonsterItem uINewGameLibrary_Com_NewMonsterItem && index >= 0 && index <= _monsterConfigIds.Count - 1)
		{
			string url = _monsterItemsConfigIds[index];
			uINewGameLibrary_Com_NewMonsterItem.loader_Profile.url = url;
		}
	}

	private void TipsShow()
	{
		Com_main.com_TabAll.loader_Detail.onClick.Retain();
		if (_curMapInfo != null)
		{
			int curMapType = GetCurMapType(_curMapInfo.Id);
			SimpleSingletonProvider<GameLogicManager>.inst.map.ShowMapDetail((MapModeType)curMapType, _curMapInfo);
		}
		else
		{
			Debug.Log("当前 _curMapInfo = null");
		}
		Com_main.com_TabAll.loader_Detail.onClick.Release();
	}

	private void OnMapRenderer(int index, GObject gObj)
	{
		UINewGameLibrary_Button_MapItem uiItem = gObj as UINewGameLibrary_Button_MapItem;
		if (uiItem == null || index < 0 || index > StaticConfigure.Map.Infos.Count - 1)
		{
			return;
		}
		int mapId = _mapConfigIds[index];
		MapInfoConfigure _CurMapInfo = OnMapIdGetMapData(mapId);
		if (_CurMapInfo != null)
		{
			uiItem.loader_Icon.url = _CurMapInfo.MapImage;
			uiItem.txt_tltle.text = _CurMapInfo.MapName.GetLocal(UIStringType.Map);
			uiItem.onClick.Set((EventCallback0)delegate
			{
				uiItem.onClick.Retain();
				State.selectedIndex = 1;
				_CurMapIndex = index;
				Com_main.Cut_in.Play();
				RefreshMapMainInfo(_CurMapInfo);
				uiItem.onClick.Release();
			});
		}
	}

	private MapInfoConfigure OnMapIdGetMapData(int MapId)
	{
		if (!StaticConfigure.Map.InfoDict.TryGetValue(MapId, out var value))
		{
			Debug.LogError($"Map.InfoDict中找不到地图ID{MapId}的配置");
			return null;
		}
		return value;
	}

	private void ReadyMapId(int Type, List<int> mapIds)
	{
		if (StaticConfigure.GameMode.GalleryMapDict.TryGetValue(Type, out var value))
		{
			foreach (int item in value.MapID)
			{
				MapInfoConfigure mapDataConfigure = item.GetMapDataConfigure();
				if (mapDataConfigure != null && TimeHelper.ValidityTime(mapDataConfigure.BeginTimeMap, mapDataConfigure.EndTimeMap))
				{
					mapIds.Add(item);
				}
			}
			return;
		}
		Debug.LogError($"GameMode.GalleryMapDict中找不到地图模式{Type}的配置");
	}

	private void SwitchGameMode()
	{
		if (list_GameMode.selectedIndex != -1)
		{
			list_GameMode.onClick.Retain();
			GameModeGalleryMapConfigure gameModeGalleryMapConfigure = gameModeInfos[list_GameMode.selectedIndex];
			if (gameModeGalleryMapConfigure.GalleryMapType == (GalleryMapType)curType)
			{
				list_GameMode.onClick.Release();
				return;
			}
			curType = (int)gameModeGalleryMapConfigure.GalleryMapType;
			_mapConfigIds.Clear();
			ReadyMapId(curType, _mapConfigIds);
			list_Maps.numItems = _mapConfigIds.Count;
			list_GameMode.onClick.Release();
		}
	}

	private void RendererGameMode(int index, GObject item)
	{
		if (item is UINewGameLibrary_Button_GameMode uINewGameLibrary_Button_GameMode)
		{
			GameModeGalleryMapConfigure gameModeGalleryMapConfigure = gameModeInfos[index];
			uINewGameLibrary_Button_GameMode.txt_Title.text = gameModeGalleryMapConfigure.GallerynameID.GetLocal(UIStringType.GameMode);
			uINewGameLibrary_Button_GameMode.txt_Explain.text = gameModeGalleryMapConfigure.GalleryDesID.GetLocal(UIStringType.GameMode);
			uINewGameLibrary_Button_GameMode.GameMode.selectedIndex = ((gameModeGalleryMapConfigure.GalleryMapType == GalleryMapType.GalleryMapPve) ? 1 : ((gameModeGalleryMapConfigure.GalleryMapType == GalleryMapType.GalleryMapSpecial) ? 2 : 0));
			uINewGameLibrary_Button_GameMode.selected = GalleryMapType.GalleryMapPve == gameModeGalleryMapConfigure.GalleryMapType;
		}
	}

	public void RefreshGameMode()
	{
		gameModeInfos.Clear();
		gameModeInfos.AddRange(StaticConfigure.GameMode.GalleryMaps);
		list_GameMode.numItems = gameModeInfos.Count;
	}

	public void RefreshMapInfo(NewGameLibraryWindow win)
	{
		if (win != null)
		{
			_CurWin = win;
			_mapConfigIds.Clear();
			ReadyMapId(1, _mapConfigIds);
			curType = 1;
			list_Maps.scrollPane.percY = 0f;
			list_Maps.numItems = _mapConfigIds.Count;
		}
	}

	private int GetCurMapType(int mapId)
	{
		foreach (GameModeInfoConfigure info in StaticConfigure.GameMode.Infos)
		{
			if (info.IsShow && info.MapID.Contains(mapId))
			{
				return (int)info.MapModeType;
			}
		}
		return -1;
	}

	public void InitComponent()
	{
		Com_main.com_TabAll.com_Banner.list_Eventcard.SetVirtualAndLoop();
		Com_main.com_TabAll.com_Banner.list_Eventcard.scrollPane.decelerationRate = 0.05f;
		Com_main.com_TabAll.com_Banner.list_Eventcard.itemRenderer = RefreshLabelItem;
		list_Maps.SetVirtual();
		list_Maps.itemRenderer = OnMapRenderer;
		Com_main.com_TabAll.list_Items.SetVirtual();
		Com_main.com_TabAll.list_Items.itemRenderer = OnAllMonsterRenderer;
		list_GameMode.itemRenderer = RendererGameMode;
	}

	public void AddEvent()
	{
		Com_main.com_TabAll.com_Banner.list_Eventcard.onTouchBegin.Add(StopAutoScroll);
		Com_main.com_TabAll.com_Banner.list_Eventcard.onTouchEnd.Add(StartAutoScroll);
		list_GameMode.onClickItem.Add(SwitchGameMode);
		Com_main.btn_Return.onClick.Add(ReturnMap);
		Com_main.btn_monster.onClick.Add(ChangeMonster);
		Com_main.btn_card.onClick.Add(ChangeOtherPage);
		Com_main.btn_land.onClick.Add(ChangeOtherPage);
		Com_main.btn_event.onClick.Add(ChangeOtherPage);
		Com_main.btn_relic.onClick.Add(ChangeOtherPage);
		Com_main.MapDray.onTouchBegin.Add(BeginSpin);
		Com_main.MapDray.onTouchMove.Add(MoveSpin);
		Com_main.MapDray.onTouchEnd.Add(EndSpin);
		Com_main.last_btn.onClick.Add(OnChangeLastMap);
		Com_main.next_btn.onClick.Add(OnChangeNextMap);
		Com_main.com_TabAll.loader_Detail.onClick.Add(TipsShow);
	}

	public void RemoveEvent()
	{
		Com_main.com_TabAll.com_Banner.list_Eventcard.onTouchBegin.Remove(StopAutoScroll);
		Com_main.com_TabAll.com_Banner.list_Eventcard.onTouchEnd.Remove(StartAutoScroll);
		list_GameMode.onClickItem.Remove(SwitchGameMode);
		Com_main.btn_Return.onClick.Remove(ReturnMap);
		Com_main.btn_monster.onClick.Remove(ChangeMonster);
		Com_main.btn_card.onClick.Remove(ChangeOtherPage);
		Com_main.btn_land.onClick.Remove(ChangeOtherPage);
		Com_main.btn_event.onClick.Remove(ChangeOtherPage);
		Com_main.btn_relic.onClick.Remove(ChangeOtherPage);
		Com_main.MapDray.onTouchBegin.Remove(BeginSpin);
		Com_main.MapDray.onTouchMove.Remove(MoveSpin);
		Com_main.MapDray.onTouchEnd.Remove(EndSpin);
		Com_main.last_btn.onClick.Remove(OnChangeLastMap);
		Com_main.next_btn.onClick.Remove(OnChangeNextMap);
		Com_main.com_TabAll.loader_Detail.onClick.Remove(TipsShow);
	}

	public void Close()
	{
		startScrollStatus = false;
		SimpleSingletonProvider<RenderTextureManager>.inst.Dispose(Com_main.MapModel, releaseHandle: true);
		SimpleSingletonProvider<RenderTextureManager>.inst.ClearCache();
	}

	public void ReturnMap()
	{
		Com_main.btn_Return.onClick.Retain();
		RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
		if (roomController == null)
		{
			State.selectedIndex = 0;
			Com_main.tab.selectedIndex = 0;
		}
		else if (roomController.IsBattle())
		{
			SimpleSingletonProvider<UIManager>.inst.HideNewGameLibrary();
		}
		else
		{
			State.selectedIndex = 0;
			Com_main.tab.selectedIndex = 0;
		}
		Com_main.btn_Return.onClick.Release();
	}

	private void BeginSpin(EventContext context)
	{
		Com_main.MapDray.onTouchBegin.Retain();
		_isDragging = true;
		InputEvent inputEvent = context.inputEvent;
		_lastTouchPos = Com_main.MapModel.GlobalToLocal(new Vector2(inputEvent.x, inputEvent.y));
		context.CaptureTouch();
		Com_main.MapDray.onTouchBegin.Release();
	}

	private void MoveSpin(EventContext context)
	{
		if (_isDragging && !(Com_main.MapModel.RenderModelRoot == null))
		{
			Com_main.MapDray.onTouchMove.Retain();
			InputEvent inputEvent = context.inputEvent;
			Vector2 vector = Com_main.MapModel.GlobalToLocal(new Vector2(inputEvent.x, inputEvent.y));
			Vector2 vector2 = vector - _lastTouchPos;
			if (Mathf.Abs(vector2.x) > 0f)
			{
				Com_main.MapModel.RotateChildrenModel((0f - vector2.x) * 0.5f);
			}
			_lastTouchPos = vector;
			Com_main.MapDray.onTouchMove.Release();
		}
	}

	private void EndSpin(EventContext context)
	{
		Com_main.MapDray.onTouchEnd.Retain();
		_isDragging = false;
		Com_main.MapDray.onTouchEnd.Release();
	}

	private void ChangeOtherPage()
	{
		Com_main.btn_event.onClick.Retain();
		Com_main.btn_land.onClick.Retain();
		Com_main.btn_card.onClick.Retain();
		Com_main.btn_relic.onClick.Retain();
		if (_curMapInfo != null)
		{
			int curMapType = GetCurMapType(_curMapInfo.Id);
			if (curMapType != -1)
			{
				Com_main.Info_Cut_in.Play();
				Com_main.com_TabMap.RefreshOtherPage(_curMapInfo, Com_main.tab.selectedIndex, curMapType, _CurWin);
			}
		}
		Com_main.btn_card.onClick.Release();
		Com_main.btn_land.onClick.Release();
		Com_main.btn_event.onClick.Release();
		Com_main.btn_relic.onClick.Release();
	}

	private void ChangeMonster()
	{
		Com_main.btn_monster.onClick.Retain();
		if (_monsterConfigIds.Count == 0)
		{
			Com_main.tab.selectedIndex = 0;
			Com_main.btn_monster.onClick.Release();
		}
		else
		{
			Com_main.Info_Cut_in.Play();
			Com_main.com_TabMonster.RefreshMonster(_monsterConfigIds, curType);
			Com_main.btn_monster.onClick.Release();
		}
	}

	private void OnChangeLastMap()
	{
		if (_mapConfigIds.Count <= 1)
		{
			return;
		}
		Com_main.last_btn.onClick.Retain();
		_CurMapIndex--;
		if (_CurMapIndex < 0)
		{
			_CurMapIndex = _mapConfigIds.Count - 1;
		}
		int mapId = _mapConfigIds[_CurMapIndex];
		MapInfoConfigure mapInfoConfigure = OnMapIdGetMapData(mapId);
		if (mapInfoConfigure == null)
		{
			Com_main.last_btn.onClick.Release();
			return;
		}
		Com_main.Info_Cut_in.Play();
		RefreshMapMainInfo(mapInfoConfigure);
		if (Com_main.tab.selectedIndex != 0)
		{
			if (Com_main.tab.selectedIndex == 1 && _monsterConfigIds.Count > 0)
			{
				Com_main.com_TabMonster.RefreshMonster(_monsterConfigIds, curType);
			}
			else if (Com_main.tab.selectedIndex == 1 && _monsterConfigIds.Count == 0)
			{
				Com_main.tab.selectedIndex = 0;
			}
			else
			{
				int curMapType = GetCurMapType(_curMapInfo.Id);
				if (curMapType == -1)
				{
					Com_main.last_btn.onClick.Release();
					return;
				}
				Com_main.com_TabMap.RefreshOtherPage(_curMapInfo, Com_main.tab.selectedIndex, curMapType, _CurWin);
			}
		}
		Com_main.last_btn.onClick.Release();
	}

	private void OnChangeNextMap()
	{
		if (_mapConfigIds.Count <= 1)
		{
			return;
		}
		Com_main.next_btn.onClick.Retain();
		_CurMapIndex++;
		if (_CurMapIndex >= _mapConfigIds.Count)
		{
			_CurMapIndex = 0;
		}
		int mapId = _mapConfigIds[_CurMapIndex];
		MapInfoConfigure mapInfoConfigure = OnMapIdGetMapData(mapId);
		if (mapInfoConfigure == null)
		{
			Com_main.next_btn.onClick.Release();
			return;
		}
		Com_main.Info_Cut_in.Play();
		RefreshMapMainInfo(mapInfoConfigure);
		if (Com_main.tab.selectedIndex != 0)
		{
			if (Com_main.tab.selectedIndex == 1 && _monsterConfigIds.Count > 0)
			{
				Com_main.com_TabMonster.RefreshMonster(_monsterConfigIds, curType);
			}
			else if (Com_main.tab.selectedIndex == 1 && _monsterConfigIds.Count == 0)
			{
				Com_main.tab.selectedIndex = 0;
			}
			else
			{
				int curMapType = GetCurMapType(_curMapInfo.Id);
				if (curMapType == -1)
				{
					Com_main.next_btn.onClick.Release();
					return;
				}
				Com_main.com_TabMap.RefreshOtherPage(_curMapInfo, Com_main.tab.selectedIndex, curMapType, _CurWin);
			}
		}
		Com_main.next_btn.onClick.Release();
	}

	public static UINewGameLibrary_Map CreateInstance()
	{
		return (UINewGameLibrary_Map)UIPackage.CreateObject("NewGameLibrary", "NewGameLibrary_Map");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		State = GetControllerAt(0);
		list_Maps = (GList)GetChildAt(0);
		btn_Return = (GButton)GetChildAt(1);
		list_GameMode = (GList)GetChildAt(2);
		Com_main = (UINewGameLibrary_Com_MapMain)GetChildAt(3);
		Cut_in = GetTransitionAt(0);
	}
}
