using System;
using System.Collections.Generic;
using Core.Net;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class UICom_RoomSetting : GComponent
{
	private int _MapModeType;

	private ChoosingTimeLimitroomsettingConfigure RoomSettingConfig;

	private UIRoomSetting_Com_SetItem _setItem_Map;

	private UIRoomSetting_Com_SetItem_ThinkTime _setItem_ThinkTime;

	private UIRoomSetting_Com_SetItem_Upgrade _setItem_Upgrade;

	private UIRoomSetting_Com_SetItem_GameSpeed _setItem_GameSpeed;

	private UIRoomSetting_Com_SetItem _setItem_Difficulty;

	private UIRoomSetting_Com_SetItem _setItem_SetStory;

	private UIRoomSetting_Com_SetItem _setItem_RoomLabel;

	private UIRoomSetting_ComboBox_popup _roomSetting_ComboBox_popup;

	private UIRoomSetting_Com_DifficultyExplain _difficultyDesc;

	private readonly List<MapInfoConfigure> Maps = new List<MapInfoConfigure>();

	private readonly List<string> MapTitles = new List<string>();

	private readonly List<UpgradeDataConfigure> UpgradeConfig = new List<UpgradeDataConfigure>();

	private readonly List<string> UpgradeTitles = new List<string>();

	private readonly List<string> ThinkTimes = new List<string>();

	private readonly List<string> GameSpeeds = new List<string>();

	private readonly List<string> SetStoryTypes = new List<string>
	{
		26.GetLocal(UIStringType.ChoosingTimeLimit),
		27.GetLocal(UIStringType.ChoosingTimeLimit)
	};

	private readonly List<string> RoomLabelTitles = new List<string>();

	private readonly List<int> DifficultyIndex = new List<int>();

	private readonly List<string> DifficultyTitle = new List<string>();

	public Controller showPSWBtn;

	public GTextField txt_RoomId;

	public GTextInput txtField_PSW;

	public GButton btn_SurePSW;

	public GList list_Setting;

	public const string URL = "ui://m6sn3r22esjw8q";

	public void InitComponents()
	{
		_roomSetting_ComboBox_popup = UIRoomSetting_ComboBox_popup.CreateInstance();
		_difficultyDesc = UIRoomSetting_Com_DifficultyExplain.CreateInstance();
	}

	public void AddEvent()
	{
		txtField_PSW.onChanged.Add(ChangePSWStatus);
		btn_SurePSW.onClick.Add(ChangePSW);
	}

	public void RemoveEvent()
	{
		txtField_PSW.onChanged.Remove(ChangePSWStatus);
		btn_SurePSW.onClick.Remove(ChangePSW);
	}

	public void Close()
	{
		GRoot.inst.HidePopup(_roomSetting_ComboBox_popup);
		GRoot.inst.HidePopup(_difficultyDesc);
	}

	public override void Dispose()
	{
		GRoot.inst.HidePopup(_roomSetting_ComboBox_popup);
		_roomSetting_ComboBox_popup?.Dispose();
		_roomSetting_ComboBox_popup = null;
		GRoot.inst.HidePopup(_difficultyDesc);
		_difficultyDesc?.Dispose();
		_difficultyDesc = null;
		base.Dispose();
	}

	private void ResetExpandTag(UIRoomSetting_Com_SetItem setItem)
	{
		if (setItem != null)
		{
			setItem.btn_Explain.visible = false;
			setItem.com_MapTag.visible = false;
		}
	}

	public void Create_Refresh(MapModeType mapMode, int MapId)
	{
		RefreshData((int)mapMode);
		txt_RoomId.text = "";
		txtField_PSW.text = "";
		list_Setting.numItems = 0;
		if (RoomSettingConfig.HasMap)
		{
			_setItem_Map = list_Setting.AddItemFromPool("ui://m6sn3r22n2xaby") as UIRoomSetting_Com_SetItem;
			int mapIndexByMapId = GetMapIndexByMapId(MapId);
			RefreshSetItem_Map(mapIndexByMapId);
		}
		if (RoomSettingConfig.HasChoosingTime)
		{
			_setItem_ThinkTime = list_Setting.AddItemFromPool("ui://m6sn3r22n2xac0") as UIRoomSetting_Com_SetItem_ThinkTime;
			int defaultThinkTimeIndex = GetDefaultThinkTimeIndex();
			RefreshSetItem_ThinkTime(defaultThinkTimeIndex);
		}
		if (RoomSettingConfig.HasUpgrade)
		{
			_setItem_Upgrade = list_Setting.AddItemFromPool("ui://m6sn3r22n2xabz") as UIRoomSetting_Com_SetItem_Upgrade;
			int defaultConditionIndex = GetDefaultConditionIndex();
			RefreshSetItem_Upgrade(defaultConditionIndex);
		}
		else
		{
			_setItem_Upgrade = null;
		}
		if (RoomSettingConfig.HasGameSpeed)
		{
			_setItem_GameSpeed = list_Setting.AddItemFromPool("ui://m6sn3r22n2xac1") as UIRoomSetting_Com_SetItem_GameSpeed;
			int defaultGameSpeedIndex = GetDefaultGameSpeedIndex();
			RefreshSetItem_GameSpeed(defaultGameSpeedIndex);
		}
		if (RoomSettingConfig.HasDifficulty)
		{
			_setItem_Difficulty = list_Setting.AddItemFromPool("ui://m6sn3r22n2xaby") as UIRoomSetting_Com_SetItem;
			RefreshDifficulty(0);
		}
		else
		{
			_setItem_Difficulty = null;
		}
		if (RoomSettingConfig.HasLabel.Count > 0)
		{
			_setItem_RoomLabel = list_Setting.AddItemFromPool("ui://m6sn3r22n2xaby") as UIRoomSetting_Com_SetItem;
			RefreshSetItem_RoomLabel(0);
		}
		else
		{
			_setItem_RoomLabel = null;
		}
		int currentMap = GetCurrentMap();
		if (BattleConfig.StoryMapIds.Contains(currentMap))
		{
			_setItem_SetStory = list_Setting.AddItemFromPool("ui://m6sn3r22n2xaby") as UIRoomSetting_Com_SetItem;
			RefreshSetItem_SetStory(0);
		}
		else
		{
			_setItem_SetStory = null;
		}
	}

	private void RefreshSetItem_Map(int index)
	{
		ResetExpandTag(_setItem_Map);
		_setItem_Map.data = index;
		_setItem_Map.txt_Content.text = MapTitles[index];
		_setItem_Map.txt_Type.text = 101.GetLocal(UIStringType.ChoosingTimeLimit);
		_setItem_Map.btn_Click.onClick.Set((EventCallback0)delegate
		{
			ShowRoomSetting_ComboBox_popup(_setItem_Map.btn_Click, 5, MapTitles, MapInfoChange);
		});
		bool flag = BattleConfig.IsPractice(_MapModeType);
		_setItem_Map.image_Arrow.visible = !flag;
		_setItem_Map.txt_Content.text = MapTitles[index];
		RefreshMapTag();
	}

	private void RefreshSetItem_ThinkTime(int thinkTimeIndex)
	{
		_setItem_ThinkTime.data = thinkTimeIndex;
		_setItem_ThinkTime.txt_Content.SetVar("think", ThinkTimes[thinkTimeIndex]).FlushVars();
		_setItem_ThinkTime.txt_Type.text = 102.GetLocal(UIStringType.ChoosingTimeLimit);
		_setItem_ThinkTime.btn_Click.onClick.Set((EventCallback0)delegate
		{
			ShowRoomSetting_ComboBox_popup(_setItem_ThinkTime.btn_Click, 2, ThinkTimes, ThinkTimeChange);
		});
		bool flag = BattleConfig.IsPractice(_MapModeType);
		_setItem_ThinkTime.image_Arrow.visible = !flag;
	}

	private void RefreshSetItem_Upgrade(int conditionIndex)
	{
		_setItem_Upgrade.data = conditionIndex;
		_setItem_Upgrade.txt_Content.text = UpgradeTitles[conditionIndex];
		_setItem_Upgrade.txt_Type.text = 103.GetLocal(UIStringType.ChoosingTimeLimit);
		_setItem_Upgrade.btn_Click.onClick.Set((EventCallback0)delegate
		{
			ShowRoomSetting_ComboBox_popup(_setItem_Upgrade.btn_Click, 1, UpgradeTitles, UpgradeChange);
		});
		bool flag = BattleConfig.IsPractice(_MapModeType);
		_setItem_Upgrade.image_Arrow.visible = !flag;
	}

	private void RefreshSetItem_GameSpeed(int gameSpeedIndex)
	{
		_setItem_GameSpeed.data = gameSpeedIndex;
		_setItem_GameSpeed.txt_Content.text = GameSpeeds[gameSpeedIndex];
		_setItem_GameSpeed.txt_Type.text = 105.GetLocal(UIStringType.ChoosingTimeLimit);
		_setItem_GameSpeed.btn_Click.onClick.Set((EventCallback0)delegate
		{
			ShowRoomSetting_ComboBox_popup(_setItem_GameSpeed.btn_Click, 3, GameSpeeds, GameSpeedChange);
		});
		bool flag = BattleConfig.IsPractice(_MapModeType);
		_setItem_GameSpeed.image_Arrow.visible = flag;
	}

	private void RefreshSetItem_SetStory(int skipIndex)
	{
		ResetExpandTag(_setItem_SetStory);
		_setItem_SetStory.data = skipIndex;
		_setItem_SetStory.txt_Content.text = SetStoryTypes[skipIndex];
		_setItem_SetStory.txt_Type.text = 106.GetLocal(UIStringType.ChoosingTimeLimit);
		_setItem_SetStory.btn_Click.onClick.Set((EventCallback0)delegate
		{
			ShowRoomSetting_ComboBox_popup(_setItem_SetStory.btn_Click, 0, SetStoryTypes, GameSetStoryChange);
		});
		bool flag = BattleConfig.IsPractice(_MapModeType);
		_setItem_SetStory.image_Arrow.visible = !flag;
	}

	private void RefreshSetItem_RoomLabel(int labelIndex)
	{
		ResetExpandTag(_setItem_RoomLabel);
		_setItem_RoomLabel.data = labelIndex;
		_setItem_RoomLabel.txt_Content.text = RoomLabelTitles[labelIndex];
		_setItem_RoomLabel.txt_Type.text = 107.GetLocal(UIStringType.ChoosingTimeLimit);
		_setItem_RoomLabel.btn_Click.onClick.Set((EventCallback0)delegate
		{
			ShowRoomSetting_ComboBox_popup(_setItem_RoomLabel.btn_Click, 0, RoomLabelTitles, RoomLabelChange);
		});
	}

	public void Wait_Refresh()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		RefreshData(curRoomInfo.MapType);
		txt_RoomId.text = 108.GetLocal(UIStringType.ChoosingTimeLimit) + curRoomInfo.Id;
		txtField_PSW.inputTextField.text = curRoomInfo.Pwd;
		list_Setting.numItems = 0;
		if (RoomSettingConfig.HasMap)
		{
			_setItem_Map = list_Setting.AddItemFromPool("ui://m6sn3r22n2xaby") as UIRoomSetting_Com_SetItem;
			int curRoomMapIndex = GetCurRoomMapIndex();
			RefreshSetItem_Map(curRoomMapIndex);
		}
		if (RoomSettingConfig.HasChoosingTime)
		{
			_setItem_ThinkTime = list_Setting.AddItemFromPool("ui://m6sn3r22n2xac0") as UIRoomSetting_Com_SetItem_ThinkTime;
			int curThinkTimeIndex = GetCurThinkTimeIndex();
			RefreshSetItem_ThinkTime(curThinkTimeIndex);
		}
		if (RoomSettingConfig.HasUpgrade)
		{
			_setItem_Upgrade = list_Setting.AddItemFromPool("ui://m6sn3r22n2xabz") as UIRoomSetting_Com_SetItem_Upgrade;
			int curConditionIndex = GetCurConditionIndex();
			RefreshSetItem_Upgrade(curConditionIndex);
		}
		else
		{
			_setItem_Upgrade = null;
		}
		if (RoomSettingConfig.HasGameSpeed)
		{
			_setItem_GameSpeed = list_Setting.AddItemFromPool("ui://m6sn3r22n2xac1") as UIRoomSetting_Com_SetItem_GameSpeed;
			int curGameSpeedIndex = GetCurGameSpeedIndex();
			RefreshSetItem_GameSpeed(curGameSpeedIndex);
		}
		if (RoomSettingConfig.HasDifficulty)
		{
			_setItem_Difficulty = list_Setting.AddItemFromPool("ui://m6sn3r22n2xaby") as UIRoomSetting_Com_SetItem;
			RefreshDifficulty(GetCurRoomDifficultyIndex());
		}
		else
		{
			_setItem_Difficulty = null;
		}
		if (RoomSettingConfig.HasLabel.Count > 0)
		{
			_setItem_RoomLabel = list_Setting.AddItemFromPool("ui://m6sn3r22n2xaby") as UIRoomSetting_Com_SetItem;
			RefreshSetItem_RoomLabel(GetCurRoomLabelIndex());
		}
		else
		{
			_setItem_RoomLabel = null;
		}
		int currentMap = GetCurrentMap();
		if (BattleConfig.StoryMapIds.Contains(currentMap))
		{
			_setItem_SetStory = list_Setting.AddItemFromPool("ui://m6sn3r22n2xaby") as UIRoomSetting_Com_SetItem;
			RefreshSetItem_SetStory(GetCurStorySetIndex());
		}
		else
		{
			_setItem_SetStory = null;
		}
	}

	private void RefreshData(int mapModeType)
	{
		_MapModeType = mapModeType;
		txtField_PSW.inputTextField.text = "";
		if (!StaticConfigure.ChoosingTimeLimit.RoomsettingDict.TryGetValue(mapModeType, out RoomSettingConfig))
		{
			Debug.LogError($"MapModeType={mapModeType}, Unable to retrieve roomSetting from ChoosingTimeLimit.RoomSettingDict");
			return;
		}
		RefreshMapInfo(mapModeType);
		RefreshCondition(mapModeType);
		RefreshThinkTime();
		RefreshGameSpeed();
		RefreshRoomLabel();
	}

	public void Particles_Refresh(int mapModeType)
	{
		txtField_PSW.inputTextField.text = "";
	}

	private void RefreshMapInfo(int mapModeType)
	{
		Maps.Clear();
		MapTitles.Clear();
		RepeatedField<int> mapID = mapModeType.GetGameModeInfoConfigure().MapID;
		RepeatedField<MapInfoConfigure> mapInfoConfigs = StaticConfigure.Map.GetMapInfoConfigs();
		for (int i = 0; i < mapID.Count; i++)
		{
			for (int j = 0; j < mapInfoConfigs.Count; j++)
			{
				if (mapInfoConfigs[j].Id == mapID[i])
				{
					Maps.Add(mapInfoConfigs[j]);
					MapTitles.Add(mapInfoConfigs[j].MapName.GetLocal(UIStringType.Map));
					break;
				}
			}
		}
	}

	private void MapInfoChange(UIRoomSetting_Button_Info btn_Info, int index)
	{
		if (MapIsLock(index))
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1100202);
			return;
		}
		if (_setItem_Map != null)
		{
			_setItem_Map.data = index;
			_setItem_Map.txt_Content.text = MapTitles[index];
			RefreshMapTag();
		}
		RefreshDifficulty(0);
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is RoomWaitPanel)
		{
			ModifyRoomSetting();
			return;
		}
		ChangeStoryComponent();
		SimpleSingletonProvider<GameLogicManager>.inst.roomList.signal.roomChange.Dispatch();
	}

	private bool MapIsLock(int index)
	{
		if (BattleConfig.IsMutatorPve(_MapModeType))
		{
			MapInfoConfigure mapInfoConfigure = Maps[index];
			return !SimpleSingletonProvider<GameLogicManager>.inst.heroCard.sportsMeetData.MapIsUnlock(mapInfoConfigure.Id);
		}
		return false;
	}

	private void RefreshCondition(int mapModeType)
	{
		UpgradeConfig.Clear();
		UpgradeTitles.Clear();
		RepeatedField<int> upgrade = mapModeType.GetGameModeInfoConfigure().Upgrade;
		for (int i = 0; i < upgrade.Count; i++)
		{
			if (StaticConfigure.Upgrade.DataDict.TryGetValue(upgrade[i], out var value))
			{
				RepeatedField<UpgradeDataConfigureItem> upgradeDataConfigureItems = value.UpgradeDataConfigureItems;
				string text = "";
				for (int j = 0; j < upgradeDataConfigureItems.Count; j++)
				{
					text = text + ((text != "") ? "/" : "") + upgradeDataConfigureItems[j].Gold;
				}
				UpgradeTitles.Add(text);
				UpgradeConfig.Add(value);
			}
		}
	}

	private void UpgradeChange(UIRoomSetting_Button_Info btn_Info, int index)
	{
		if (_setItem_Upgrade != null)
		{
			_setItem_Upgrade.data = index;
			_setItem_Upgrade.txt_Content.text = UpgradeTitles[index];
		}
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is RoomWaitPanel)
		{
			ModifyRoomSetting();
		}
	}

	private void RefreshThinkTime()
	{
		ThinkTimes.Clear();
		for (int i = 0; i < StaticConfigure.ChoosingTimeLimit.Infos.Count; i++)
		{
			ThinkTimes.Add(StaticConfigure.ChoosingTimeLimit.Infos[i].DescriptionID.GetLocal(UIStringType.ChoosingTimeLimit));
		}
	}

	private void ThinkTimeChange(UIRoomSetting_Button_Info btn_Info, int index)
	{
		if (_setItem_ThinkTime != null)
		{
			_setItem_ThinkTime.data = index;
			_setItem_ThinkTime.txt_Content.SetVar("think", ThinkTimes[index]).FlushVars();
		}
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is RoomWaitPanel)
		{
			ModifyRoomSetting();
		}
	}

	private void RefreshGameSpeed()
	{
		GameSpeeds.Clear();
		for (int i = 0; i < StaticConfigure.ChoosingTimeLimit.Gamespeeds.Count; i++)
		{
			GameSpeeds.Add(StaticConfigure.ChoosingTimeLimit.Gamespeeds[i].DescriptionID.GetLocal(UIStringType.ChoosingTimeLimit));
		}
	}

	private void GameSpeedChange(UIRoomSetting_Button_Info btn_Info, int index)
	{
		if (_setItem_GameSpeed != null)
		{
			_setItem_GameSpeed.data = index;
			_setItem_GameSpeed.txt_Content.text = GameSpeeds[index];
		}
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is RoomWaitPanel)
		{
			ModifyRoomSetting();
		}
	}

	private void GameSetStoryChange(UIRoomSetting_Button_Info btn_Info, int index)
	{
		if (_setItem_SetStory != null)
		{
			_setItem_SetStory.data = index;
			_setItem_SetStory.txt_Content.text = SetStoryTypes[index];
		}
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is RoomWaitPanel)
		{
			ModifyRoomSetting();
		}
	}

	private void ChangeStoryComponent()
	{
		int currentMap = GetCurrentMap();
		if (BattleConfig.StoryMapIds.Contains(currentMap))
		{
			if (_setItem_SetStory == null)
			{
				_setItem_SetStory = list_Setting.AddItemFromPool("ui://m6sn3r22n2xaby") as UIRoomSetting_Com_SetItem;
				RefreshSetItem_SetStory(0);
			}
		}
		else
		{
			if (_setItem_SetStory != null)
			{
				list_Setting.RemoveChildToPool(_setItem_SetStory);
			}
			_setItem_SetStory = null;
		}
	}

	private void RefreshRoomLabel()
	{
		RoomLabelTitles.Clear();
		foreach (int item in RoomSettingConfig.HasLabel)
		{
			string local = item.GetLocal(UIStringType.ChoosingTimeLimit);
			RoomLabelTitles.Add(local);
		}
	}

	private void RoomLabelChange(UIRoomSetting_Button_Info btn_Info, int index)
	{
		if (_setItem_RoomLabel != null)
		{
			_setItem_RoomLabel.data = index;
			_setItem_RoomLabel.txt_Content.text = RoomLabelTitles[index];
		}
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is RoomWaitPanel)
		{
			ModifyRoomSetting();
		}
	}

	private void ShowRoomSetting_ComboBox_popup(GGraph _item, int type, List<string> titles, Action<UIRoomSetting_Button_Info, int> OnChange)
	{
		if (!TouchableStatus())
		{
			return;
		}
		_roomSetting_ComboBox_popup.list.itemRenderer = delegate(int index, GObject item)
		{
			UIRoomSetting_Button_Info _infoCom = item as UIRoomSetting_Button_Info;
			if (_infoCom != null)
			{
				_infoCom.type.selectedIndex = type;
				_infoCom.data = index;
				_infoCom.grayed = false;
				_infoCom.isLock.selectedIndex = 0;
				if (type == 0)
				{
					_infoCom.txt_Content_Map.text = titles[index];
				}
				else if (type == 1)
				{
					_infoCom.txt_Content_Condition.text = titles[index];
				}
				else if (type == 2)
				{
					_infoCom.txt_Content_Time.SetVar("think", titles[index]).FlushVars();
				}
				else if (type == 3)
				{
					_infoCom.txt_Content_Multiple.text = titles[index];
				}
				else if (type == 4)
				{
					_infoCom.txt_Content_Difficulty.text = GetDifficultyIconText(index) + titles[index];
				}
				if (type == 5)
				{
					MapInfoConfigure mapInfoConfigure = Maps[index];
					_infoCom.com_mapTag.tag.selectedIndex = mapInfoConfigure.MapMark;
					_infoCom.txt_Content_Map.text = titles[index];
					_infoCom.isLock.selectedIndex = (MapIsLock(index) ? 1 : 0);
				}
				_infoCom.onClick.Set((EventCallback0)delegate
				{
					GRoot.inst.HidePopup(_roomSetting_ComboBox_popup);
					OnChange?.Invoke(_infoCom, index);
				});
			}
		};
		_roomSetting_ComboBox_popup.list.numItems = titles.Count;
		_roomSetting_ComboBox_popup.list.ResizeToFit();
		_roomSetting_ComboBox_popup.sortingOrder = 999;
		GRoot.inst.ShowPopup(_roomSetting_ComboBox_popup, _item);
	}

	private int GetCurRoomMapIndex()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo == null)
		{
			return 0;
		}
		return GetMapIndexByMapId(curRoomInfo.MapId);
	}

	private int GetMapIndexByMapId(int MapId)
	{
		if (MapId == 0)
		{
			return 0;
		}
		for (int i = 0; i < Maps.Count; i++)
		{
			if (Maps[i].Id == MapId)
			{
				return i;
			}
		}
		return 0;
	}

	public int GetCurrentMap()
	{
		if (!(_setItem_Map?.data is int index))
		{
			return Maps[0].Id;
		}
		return Maps[index].Id;
	}

	private int GetDefaultThinkTimeIndex()
	{
		for (int i = 0; i < StaticConfigure.ChoosingTimeLimit.Infos.Count; i++)
		{
			if (StaticConfigure.ChoosingTimeLimit.Infos[i].IsDefault)
			{
				return i;
			}
		}
		return 0;
	}

	private int GetCurThinkTimeIndex()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo == null)
		{
			return 0;
		}
		for (int i = 0; i < StaticConfigure.ChoosingTimeLimit.Infos.Count; i++)
		{
			if (StaticConfigure.ChoosingTimeLimit.Infos[i].ChoosingTimeType == (ChoosingTimeType)curRoomInfo.TimePlan)
			{
				return i;
			}
		}
		return 0;
	}

	public int GetCurrentThinkTime()
	{
		if (!(_setItem_ThinkTime?.data is int index))
		{
			return (int)StaticConfigure.ChoosingTimeLimit.Infos[0].ChoosingTimeType;
		}
		return (int)StaticConfigure.ChoosingTimeLimit.Infos[index].ChoosingTimeType;
	}

	private int GetDefaultConditionIndex()
	{
		for (int i = 0; i < UpgradeConfig.Count; i++)
		{
			if (UpgradeConfig[i].IsDefault)
			{
				return i;
			}
		}
		return 0;
	}

	private int GetCurConditionIndex()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo == null)
		{
			return 0;
		}
		for (int i = 0; i < UpgradeConfig.Count; i++)
		{
			if (UpgradeConfig[i].Id == curRoomInfo.UpgradePlan)
			{
				return i;
			}
		}
		return 0;
	}

	public int GetCurrentCondition()
	{
		int mapModeType = _MapModeType;
		if (mapModeType == 4 || mapModeType == 12)
		{
			int currentDifficultyIndex = GetCurrentDifficultyIndex();
			GameModeDifficultyDataConfigureItem pVEDifficultyConfig = GetPVEDifficultyConfig(currentDifficultyIndex);
			if (pVEDifficultyConfig != null)
			{
				return pVEDifficultyConfig.UpgradeId;
			}
		}
		if (!(_setItem_Upgrade?.data is int index))
		{
			return UpgradeConfig[0].Id;
		}
		return UpgradeConfig[index].Id;
	}

	private GameModeDifficultyDataConfigureItem GetPVEDifficultyConfig(int difficulty)
	{
		if (StaticConfigure.GameMode.DifficultyDataDict.TryGetValue(4, out var value))
		{
			GameModeDifficultyDataConfigureItem safeByIndex = value.GameModeDifficultyDataConfigureItems.GetSafeByIndex(difficulty);
			if (safeByIndex != null)
			{
				return safeByIndex;
			}
		}
		return null;
	}

	private int GetDefaultGameSpeedIndex()
	{
		for (int i = 0; i < StaticConfigure.ChoosingTimeLimit.Gamespeeds.Count; i++)
		{
			if (StaticConfigure.ChoosingTimeLimit.Gamespeeds[i].IsDefault)
			{
				return i;
			}
		}
		return 0;
	}

	private int GetCurGameSpeedIndex()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo == null)
		{
			return 0;
		}
		for (int i = 0; i < StaticConfigure.ChoosingTimeLimit.Gamespeeds.Count; i++)
		{
			if (StaticConfigure.ChoosingTimeLimit.Gamespeeds[i].GameSpeedType == (GameSpeedType)curRoomInfo.speedType)
			{
				return i;
			}
		}
		return 0;
	}

	public int GetCurrentGameSpeed()
	{
		if (!(_setItem_GameSpeed?.data is int index))
		{
			return 2;
		}
		return (int)StaticConfigure.ChoosingTimeLimit.Gamespeeds[index].GameSpeedType;
	}

	private void ChangePSW(EventContext context)
	{
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is RoomWaitPanel && TouchableStatus())
		{
			btn_SurePSW.onClick.Retain();
			showPSWBtn.selectedIndex = 0;
			ModifyRoomSetting(delegate
			{
				btn_SurePSW.onClick.Release();
				txtField_PSW.onChanged.Release();
			});
		}
	}

	private void ChangePSWStatus(EventContext context)
	{
		if (!(SimpleSingletonProvider<UIManager>.inst.currentPanel is RoomWaitPanel))
		{
			return;
		}
		if (!TouchableStatus())
		{
			RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
			if (curRoomInfo == null)
			{
				txtField_PSW.text = "";
			}
			else
			{
				txtField_PSW.text = curRoomInfo.Pwd;
			}
		}
		else
		{
			txtField_PSW.onChanged.Retain();
			showPSWBtn.selectedIndex = 1;
		}
	}

	private int GetCurRoomDifficultyIndex()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		RepeatedField<int> repeatedField = curRoomInfo?.MapId.GetMapDataConfigure().DifficultyIds;
		if (repeatedField == null)
		{
			return 0;
		}
		RepeatedField<MapGameDifficultyConfigureItem> mapGameDifficultyItems = repeatedField[0].GetMapGameDifficultyItems();
		if (mapGameDifficultyItems == null)
		{
			return 0;
		}
		for (int i = 0; i < mapGameDifficultyItems.Count; i++)
		{
			if (mapGameDifficultyItems[i].Index == curRoomInfo.Difficulty)
			{
				return i;
			}
		}
		return 0;
	}

	public int GetCurrentDifficultyIndex()
	{
		if (!(_setItem_Map?.data is int index) || Maps[index].DifficultyIds.Count == 0)
		{
			return 0;
		}
		RepeatedField<MapGameDifficultyConfigureItem> mapGameDifficultyItems = Maps[index].DifficultyIds[0].GetMapGameDifficultyItems();
		if (mapGameDifficultyItems == null)
		{
			return 0;
		}
		if (!(_setItem_Difficulty?.data is int index2))
		{
			return 0;
		}
		return mapGameDifficultyItems[index2].Index;
	}

	private void UpdateDifficultyContent(MapInfoConfigure mapInfo)
	{
		RepeatedField<MapGameDifficultyConfigureItem> mapGameDifficultyItems = mapInfo.DifficultyIds[0].GetMapGameDifficultyItems();
		RepeatedField<MapMapLevelConfigureItem> mapLevelConfigureItems = mapInfo.Id.GetMapLevelConfigureItems();
		DifficultyIndex.Clear();
		DifficultyTitle.Clear();
		for (int i = 0; i < mapGameDifficultyItems.Count; i++)
		{
			if (mapLevelConfigureItems == null || mapLevelConfigureItems.Count <= i)
			{
				Debug.LogError("mapLevels‘s count is not equal to itemsInfo.count");
				break;
			}
			if (TimeHelper.ValidityTime(mapLevelConfigureItems[i].BeginTime, mapLevelConfigureItems[i].EndTime))
			{
				ChoosingTimeLimitdifficultyConfigure choosingTimeLimitDifficultyConfigure = mapGameDifficultyItems[i].Index.GetChoosingTimeLimitDifficultyConfigure();
				DifficultyTitle.Add(choosingTimeLimitDifficultyConfigure.DescriptionID.GetLocal(UIStringType.ChoosingTimeLimit));
				DifficultyIndex.Add(mapGameDifficultyItems[i].Index);
			}
		}
	}

	private void RefreshDifficulty(int index)
	{
		if (_setItem_Difficulty == null || !(_setItem_Map?.data is int index2))
		{
			return;
		}
		UpdateDifficultyContent(Maps[index2]);
		if (DifficultyIndex.Count <= index || DifficultyTitle.Count <= index)
		{
			index = 0;
		}
		RefreshDifficultyExplain();
		_setItem_Difficulty.data = DifficultyIndex[index];
		_setItem_Difficulty.txt_Content.text = GetDifficultyIconText(index) + DifficultyTitle[index];
		_setItem_Difficulty.txt_Type.text = 104.GetLocal(UIStringType.ChoosingTimeLimit);
		_setItem_Difficulty.btn_Click.onClick.Set((EventCallback0)delegate
		{
			ShowRoomSetting_ComboBox_popup(_setItem_Difficulty.btn_Click, 4, DifficultyTitle, DifficultyChange);
			Player playerInfo = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo();
			if (playerInfo != null && playerInfo.Level < StaticGlobalData.ROOM_PVELOCK_LEVEL)
			{
				int unLockDifficulty = playerInfo.UnLockDifficulty;
				List<GObject> children = _roomSetting_ComboBox_popup.list._children;
				for (int i = 0; i < children.Count && children[i] is UIRoomSetting_Button_Info uIRoomSetting_Button_Info; i++)
				{
					uIRoomSetting_Button_Info.grayed = DifficultyIndex[i] >= StaticGlobalData.ROOM_PVELOCK_DIFFICULTY && DifficultyIndex[i] > unLockDifficulty;
				}
			}
		});
		_setItem_Difficulty.image_Arrow.visible = !BattleConfig.IsPractice(_MapModeType);
	}

	private void DifficultyChange(UIRoomSetting_Button_Info btn_Info, int index)
	{
		if (btn_Info.grayed)
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(11025);
			return;
		}
		if (_setItem_Difficulty != null)
		{
			_setItem_Difficulty.data = DifficultyIndex[index];
			_setItem_Difficulty.txt_Content.text = GetDifficultyIconText(index) + DifficultyTitle[index];
		}
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is RoomWaitPanel)
		{
			ModifyRoomSetting();
		}
		else
		{
			SimpleSingletonProvider<GameLogicManager>.inst.roomList.signal.roomChange.Dispatch();
		}
	}

	private string GetDifficultyIconText(int index)
	{
		string safeByIndex = CommonUIManager.DifficultyIcons.GetSafeByIndex(index);
		return "<img src='" + safeByIndex + "' width='40' height='40'/>";
	}

	private void RefreshDifficultyExplain()
	{
		if (_setItem_Difficulty == null)
		{
			return;
		}
		_setItem_Difficulty.btn_Explain.visible = true;
		_setItem_Difficulty.btn_Explain.onClick.Set((EventCallback0)delegate
		{
			if (_setItem_Difficulty != null && _setItem_Difficulty.data is int difficulty)
			{
				GameModeDifficultyDataConfigureItem pVEDifficultyConfig = GetPVEDifficultyConfig(difficulty);
				if (pVEDifficultyConfig != null)
				{
					_setItem_Difficulty.btn_Explain.onClick.Retain();
					_difficultyDesc.txt_Desc.text = pVEDifficultyConfig.DifficultyDescId.GetLocal(UIStringType.GameMode);
					GRoot.inst.ShowPopup(_difficultyDesc, _setItem_Difficulty.btn_Explain, PopupDirection.Down);
					_difficultyDesc.SetXY(_difficultyDesc.x + _setItem_Difficulty.btn_Explain.width, _difficultyDesc.y - _setItem_Difficulty.btn_Explain.height);
					_setItem_Difficulty.btn_Explain.onClick.Release();
				}
			}
		});
	}

	private void RefreshMapTag()
	{
		if (_setItem_Map == null || !(_setItem_Map?.data is int index))
		{
			return;
		}
		_setItem_Map.com_MapTag.visible = true;
		_setItem_Map.com_MapTag.tag.selectedIndex = Maps[index].MapMark;
		_setItem_Map.com_MapTag.onClick.Set((EventCallback0)delegate
		{
			if (_setItem_Map.com_MapTag.tag.selectedIndex != 0)
			{
				_setItem_Map.com_MapTag.onClick.Retain();
				int num = _setItem_Map.com_MapTag.tag.selectedIndex switch
				{
					1 => 1110001, 
					2 => 1110002, 
					3 => 1110003, 
					_ => 0, 
				};
				if (num != 0)
				{
					_difficultyDesc.txt_Desc.text = num.GetLocal(UIStringType.GUI);
					GRoot.inst.ShowPopup(_difficultyDesc, _setItem_Map.btn_Explain, PopupDirection.Down);
					_difficultyDesc.SetXY(_difficultyDesc.x + _setItem_Map.btn_Explain.width, _difficultyDesc.y - _setItem_Map.btn_Explain.height);
					_setItem_Map.com_MapTag.onClick.Release();
				}
			}
		});
	}

	private int GetCurStorySetIndex()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo == null || curRoomInfo.info == null)
		{
			return 0;
		}
		if (!curRoomInfo.info.SkipStory)
		{
			return 0;
		}
		return 1;
	}

	public bool GetSkipStoryStatus()
	{
		if (!(_setItem_SetStory?.data is int num))
		{
			return false;
		}
		return num == 1;
	}

	private int GetCurRoomLabelIndex()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo == null || curRoomInfo.info == null)
		{
			return 0;
		}
		return curRoomInfo.info.RoomLabel;
	}

	public int GetRoomLabelIndex()
	{
		object obj = _setItem_RoomLabel?.data;
		if (obj is int)
		{
			return (int)obj;
		}
		return 0;
	}

	private void ModifyRoomSetting(System.Action _action = null)
	{
		string pwd = txtField_PSW.inputTextField.text;
		int currentMap = GetCurrentMap();
		int currentThinkTime = GetCurrentThinkTime();
		int currentCondition = GetCurrentCondition();
		int currentGameSpeed = GetCurrentGameSpeed();
		int currentDifficultyIndex = GetCurrentDifficultyIndex();
		bool skipStoryStatus = GetSkipStoryStatus();
		int roomLabelIndex = GetRoomLabelIndex();
		SimpleSingletonProvider<GameLogicManager>.inst.room.RequestChangeRoomC2S(pwd, currentMap, currentThinkTime, currentCondition, currentGameSpeed, currentDifficultyIndex, skipStoryStatus, roomLabelIndex).OnFinished.AddOnce(delegate(RPCAsyncResult result)
		{
			if (result.errId != 0)
			{
				Wait_Refresh();
			}
			_action?.Invoke();
		});
	}

	private bool IsMaster()
	{
		if (!(SimpleSingletonProvider<UIManager>.inst.currentPanel is RoomWaitPanel))
		{
			return true;
		}
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo == null || curRoomInfo.IsMatchRoom)
		{
			return true;
		}
		return SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(curRoomInfo.MasterId);
	}

	private bool TouchableStatus()
	{
		if (!BattleConfig.IsPractice(_MapModeType))
		{
			return IsMaster();
		}
		return false;
	}

	public static UICom_RoomSetting CreateInstance()
	{
		return (UICom_RoomSetting)UIPackage.CreateObject("Common_External", "Com_RoomSetting");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showPSWBtn = GetControllerAt(0);
		txt_RoomId = (GTextField)GetChildAt(0);
		txtField_PSW = (GTextInput)GetChildAt(3);
		btn_SurePSW = (GButton)GetChildAt(4);
		list_Setting = (GList)GetChildAt(6);
	}
}
