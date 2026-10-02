using System;
using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class UIMatch_Com_MapItem : GComponent
{
	public int MapId;

	public bool IsExtreme;

	public MapInfoConfigure Info;

	private MapModeType _mapMode;

	private float autoScrollTime;

	private bool startScrollStatus;

	public Controller type;

	public UIMatch_Button_PVEMapItem btn_PVE;

	public UIMatch_Button_PVPMapItem btn_PVP;

	public Transition CutIn;

	public const string URL = "ui://qxwapsemr26s1d";

	public void RefreshMapItem((int, bool) mapInfo, MapModeType mapMode)
	{
		(MapId, IsExtreme) = mapInfo;
		type.selectedIndex = ((mapMode != MapModeType.Pve && mapMode != MapModeType.MutatorPve) ? 1 : 0);
		_mapMode = mapMode;
		if (type.selectedIndex == 1)
		{
			RefreshMapModeInfo(mapMode);
		}
		if (MapId == 0)
		{
			Info = null;
			btn_PVE.itemType.selectedIndex = 1;
			btn_PVE.signalType.selectedIndex = 0;
			btn_PVP.itemType.selectedIndex = 1;
			RefreshRandomMapInfo();
		}
		else
		{
			btn_PVE.itemType.selectedIndex = 0;
			btn_PVP.itemType.selectedIndex = 0;
			Info = MapId.GetMapDataConfigure();
			if (type.selectedIndex == 0)
			{
				string text = "";
				if (IsExtreme)
				{
					if (StaticConfigure.ChoosingTimeLimit.DifficultyDict.TryGetValue(4, out var value))
					{
						text = value.DescriptionID.GetLocal(UIStringType.ChoosingTimeLimit) + "·";
					}
					if (!IsExtremeMapVail())
					{
						btn_PVE.txt_Explain.visible = true;
						btn_PVE.txt_Explain.text = 1020006.GetLocal(UIStringType.GUI);
					}
					else
					{
						btn_PVE.txt_Explain.visible = false;
					}
					btn_PVE.signalType.selectedIndex = 1;
				}
				else
				{
					btn_PVE.signalType.selectedIndex = Info.LabelType;
				}
				btn_PVE.txt_MapTitle.text = text + Info.MapName.GetLocal(UIStringType.Map);
				btn_PVE.txt_SignalChange.text = 1020021.GetLocal(UIStringType.GUI);
			}
			else
			{
				btn_PVP.com_Banner.loader_Map.url = Info.MapSceneImage;
				btn_PVP.txt_MapTitle.text = Info.MapName.GetLocal(UIStringType.Map);
			}
		}
		if (btn_PVE.com_MapTag is UICom_MapTag uICom_MapTag)
		{
			uICom_MapTag.tag.selectedIndex = Info?.MapMark ?? 0;
		}
		RefreshMapStatusByMapMode(mapMode);
	}

	private void RefreshMapModeInfo(MapModeType mapMode)
	{
		GameModeInfoConfigure gameModeInfoConfigure = ((int)mapMode).GetGameModeInfoConfigure();
		if (gameModeInfoConfigure != null)
		{
			btn_PVP.txt_Mode.text = gameModeInfoConfigure.NameID.GetLocal(UIStringType.GameMode);
		}
		else
		{
			btn_PVP.txt_Mode.text = "";
		}
	}

	private void RefreshRandomMapInfo()
	{
		int num = SimpleSingletonProvider<GameLogicManager>.inst.match.matchData.GetTodayRandomMatchInfo()?.Maps.Count ?? 0;
		string text = string.Format(1020005.GetLocal(UIStringType.GUI), num);
		if (type.selectedIndex == 0)
		{
			btn_PVE.txt_MapTitle.text = text;
		}
		else
		{
			btn_PVP.txt_MapTitle.text = text;
		}
	}

	public void RefreshSelectStatus(bool status)
	{
		btn_PVP.status.selectedIndex = (status ? 1 : 0);
		btn_PVE.status.selectedIndex = (status ? 1 : 0);
	}

	public void SetEvent(Action action)
	{
		if (type.selectedIndex == 0)
		{
			btn_PVE.onClick.Set((EventCallback0)delegate
			{
				if (!IsExtremeMapVail())
				{
					SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(11025);
				}
				else if (!MapIsUnLock(_mapMode))
				{
					SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1100202);
				}
				else
				{
					btn_PVE.onClick.Retain();
					action?.Invoke();
					btn_PVE.onClick.Release();
				}
			});
		}
		else
		{
			btn_PVP.onClick.Set((EventCallback0)delegate
			{
				btn_PVP.onClick.Retain();
				action?.Invoke();
				btn_PVP.onClick.Release();
			});
		}
	}

	private bool IsExtremeMapVail()
	{
		if (Info == null || !IsExtreme)
		{
			return true;
		}
		return SimpleSingletonProvider<GameLogicManager>.inst.match.IsVailDifficulty(4);
	}

	private void RefreshMapStatusByMapMode(MapModeType mapMode)
	{
		btn_PVE.grayed = false;
		btn_PVE.enabled = true;
		if (mapMode != MapModeType.MutatorPve)
		{
			return;
		}
		if (MapId != 0)
		{
			if (!SimpleSingletonProvider<GameLogicManager>.inst.heroCard.sportsMeetData.MapIsUnlock(MapId))
			{
				btn_PVE.grayed = true;
			}
		}
		else if (!SimpleSingletonProvider<GameLogicManager>.inst.heroCard.sportsMeetData.MapIsUnlockAll())
		{
			btn_PVE.grayed = true;
		}
	}

	private bool MapIsUnLock(MapModeType mapMode)
	{
		if (mapMode == MapModeType.MutatorPve)
		{
			if (MapId == 0)
			{
				return SimpleSingletonProvider<GameLogicManager>.inst.heroCard.sportsMeetData.MapIsUnlockAll();
			}
			return SimpleSingletonProvider<GameLogicManager>.inst.heroCard.sportsMeetData.MapIsUnlock(MapId);
		}
		return true;
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (base.visible && type.selectedIndex == 0 && startScrollStatus && btn_PVE.list_Monster.numItems > 1)
		{
			if (autoScrollTime >= 2f)
			{
				btn_PVE.list_Monster.scrollPane.ScrollRight(1f, ani: true);
				autoScrollTime = 0f;
			}
			autoScrollTime += Time.deltaTime;
		}
	}

	public void RefreshMonsterHead(int difficulty)
	{
		if (type.selectedIndex != 0)
		{
			startScrollStatus = false;
			return;
		}
		List<string> monsterHeadImages = new List<string>();
		if (Info != null)
		{
			UpdateMonsterHeadImages(monsterHeadImages, Info, difficulty);
		}
		btn_PVE.list_Monster.SetVirtualAndLoop();
		btn_PVE.list_Monster.itemRenderer = delegate(int index, GObject item)
		{
			if (item is UIMatch_Com_MapMonsterItem uIMatch_Com_MapMonsterItem)
			{
				uIMatch_Com_MapMonsterItem.loader_Boss.url = monsterHeadImages[index];
			}
		};
		btn_PVE.list_Monster.numItems = monsterHeadImages.Count;
		if (monsterHeadImages.Count > 1)
		{
			startScrollStatus = true;
			btn_PVE.list_Monster.scrollPane.onScroll.Call();
		}
		else
		{
			startScrollStatus = false;
		}
	}

	private void UpdateMonsterHeadImages(List<string> monsterHeadImages, MapInfoConfigure mapInfo, int difficulty)
	{
		foreach (int preloadCharacterId in mapInfo.PreloadCharacterIds)
		{
			MonsterInfoConfigure monsterCharacterConfigure = CharacterHandle.GetMonsterCharacterConfigure(preloadCharacterId);
			if (monsterCharacterConfigure != null && monsterCharacterConfigure.MonsterType == MonsterType.Boss && !monsterHeadImages.Contains(monsterCharacterConfigure.CharacterMap))
			{
				monsterHeadImages.Add(monsterCharacterConfigure.CharacterMap);
			}
		}
		RepeatedField<int> difficultyIds = mapInfo.DifficultyIds;
		if (difficultyIds == null || difficultyIds.Count <= 0)
		{
			return;
		}
		foreach (int item in difficultyIds)
		{
			RepeatedField<MapGameDifficultyConfigureItem> mapGameDifficultyItems = item.GetMapGameDifficultyItems();
			for (int i = 0; i < mapGameDifficultyItems.Count; i++)
			{
				if (mapGameDifficultyItems[i].Index != difficulty)
				{
					continue;
				}
				foreach (int preloadCharacterId2 in mapGameDifficultyItems[i].PreloadCharacterIds)
				{
					MonsterInfoConfigure monsterCharacterConfigure2 = CharacterHandle.GetMonsterCharacterConfigure(preloadCharacterId2);
					if (monsterCharacterConfigure2 != null && monsterCharacterConfigure2.MonsterType == MonsterType.Boss && !monsterHeadImages.Contains(monsterCharacterConfigure2.CharacterMap))
					{
						monsterHeadImages.Add(monsterCharacterConfigure2.CharacterMap);
					}
				}
				break;
			}
		}
	}

	public static UIMatch_Com_MapItem CreateInstance()
	{
		return (UIMatch_Com_MapItem)UIPackage.CreateObject("Match", "Match_Com_MapItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		btn_PVE = (UIMatch_Button_PVEMapItem)GetChildAt(0);
		btn_PVP = (UIMatch_Button_PVPMapItem)GetChildAt(1);
		CutIn = GetTransitionAt(0);
	}
}
