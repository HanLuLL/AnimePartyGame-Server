using System.Collections.Generic;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;

namespace Core;

public class RoadLineManager : SimpleSingletonProvider<RoadLineManager>
{
	private Character _RunningCharacter;

	private readonly List<RoadLineData> roadLines = new List<RoadLineData>();

	private readonly List<RoadLineData> _TempRoadLines = new List<RoadLineData>();

	private readonly List<StopEffectData> stopEffects = new List<StopEffectData>();

	private readonly List<StopEffectData> _TempStopEffects = new List<StopEffectData>();

	private readonly List<UIBattleInfo_Com_UpgradeTips> UpgradeTips = new List<UIBattleInfo_Com_UpgradeTips>();

	private readonly List<int> UpgradeLandIds = new List<int>();

	private readonly List<RoadLineData> _SearchPossiblePaths = new List<RoadLineData>();

	private readonly List<StopEffectData> _SearchTerminals = new List<StopEffectData>();

	public async UniTask<bool> GeneratePath(Character character, int movePoint, bool ForceDir = false)
	{
		_RunningCharacter = character;
		GetPossiblePaths(character.standLand.Id, movePoint, ForceDir ? (-1) : character.fromLandId);
		_TempRoadLines.Clear();
		List<RoadLineData> vailPaths = GetVailRoadLines(_SearchPossiblePaths);
		for (int i = 0; i < vailPaths.Count; i++)
		{
			RoadLineData data = vailPaths[i];
			if (roadLines.Contains(data))
			{
				int num = roadLines.IndexOf(data);
				if (num >= 0 && num < roadLines.Count)
				{
					data = roadLines[num];
					roadLines.RemoveAt(num);
				}
			}
			else
			{
				Color color = GameConfig.roadColor[character.player.Slot];
				if (!(await data.InstantiateRoad(color)))
				{
					return false;
				}
			}
			_TempRoadLines.Add(data);
		}
		_TempStopEffects.Clear();
		for (int i = 0; i < _SearchTerminals.Count; i++)
		{
			StopEffectData data2 = _SearchTerminals[i];
			if (stopEffects.Contains(data2))
			{
				int num2 = stopEffects.IndexOf(data2);
				if (num2 >= 0 && num2 < stopEffects.Count)
				{
					data2 = stopEffects[num2];
					stopEffects.RemoveAt(num2);
				}
			}
			else
			{
				await data2.CreateEffect(character.player.Slot, movePoint);
			}
			_TempStopEffects.Add(data2);
		}
		DestroyRoad();
		roadLines.AddRange(_TempRoadLines);
		stopEffects.AddRange(_TempStopEffects);
		for (int i = 0; i < stopEffects.Count; i++)
		{
			await stopEffects[i].TryShowTerminalPath();
		}
		if (UpgradeTipLicense())
		{
			BattleInfoPanel battleInfo = SimpleSingletonProvider<GameLogicManager>.inst.battle.battleInfo;
			if (battleInfo != null)
			{
				for (int j = 0; j < UpgradeLandIds.Count; j++)
				{
					UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(UpgradeLandIds[j]);
					UIBattleInfo_Com_UpgradeTips item = battleInfo.RegisterUpgradeTips(landById.transform);
					UpgradeTips.Add(item);
				}
			}
		}
		ShowSuggestArrow();
		return true;
	}

	public void DestroyRoad()
	{
		for (int i = 0; i < roadLines.Count; i++)
		{
			roadLines[i].DestroyRoadLine();
		}
		for (int j = 0; j < stopEffects.Count; j++)
		{
			stopEffects[j].DestroyEffect();
		}
		BattleInfoPanel battleInfo = SimpleSingletonProvider<GameLogicManager>.inst.battle.battleInfo;
		if (battleInfo != null)
		{
			for (int k = 0; k < UpgradeTips.Count; k++)
			{
				if (UpgradeTips[k] != null)
				{
					battleInfo.RemoveUpgradeTips(UpgradeTips[k]);
				}
			}
		}
		stopEffects.Clear();
		roadLines.Clear();
		UpgradeTips.Clear();
	}

	public void CloseAllDirections()
	{
		for (int i = 0; i < stopEffects.Count; i++)
		{
			stopEffects[i].CloseAllDirections();
		}
		SimpleSingletonProvider<MoveArrowManager>.inst.CloseArrowEffect();
	}

	public void Dispose()
	{
		DestroyRoad();
		OnDestroyInstance();
	}

	private void GetPossiblePaths(int curLandId, int step, int fromLand)
	{
		UpgradeLandIds.Clear();
		_SearchPossiblePaths.Clear();
		_SearchTerminals.Clear();
		if (step != 0)
		{
			List<int> list = SimpleSingletonProvider<LandManager>.inst.GetLandById(curLandId).CanSelectedLandId(fromLand);
			for (int i = 0; i < list.Count; i++)
			{
				List<int> currentPath = new List<int> { curLandId };
				FindCurPath(currentPath, list[i], curLandId, step, null);
			}
		}
	}

	private void FindCurPath(List<int> currentPath, int curLandId, int preLandId, int step, RoadLineData prePathData)
	{
		step--;
		currentPath.Add(curLandId);
		TryUpdateUpgradeLandId(curLandId, step);
		if (step == 0)
		{
			StopEffectData item = new StopEffectData(curLandId);
			if (!_SearchTerminals.Contains(item))
			{
				_SearchTerminals.Add(item);
			}
			TrySavePaths(currentPath, prePathData, step);
			return;
		}
		List<int> list = SimpleSingletonProvider<LandManager>.inst.GetLandById(curLandId).CanSelectedLandId(preLandId);
		if (list.Count == 1)
		{
			FindCurPath(currentPath, list[0], curLandId, step, prePathData);
			return;
		}
		RoadLineData prePathData2 = TrySavePaths(currentPath, prePathData, step);
		for (int i = 0; i < list.Count; i++)
		{
			List<int> currentPath2 = new List<int> { curLandId };
			FindCurPath(currentPath2, list[i], curLandId, step, prePathData2);
		}
	}

	private RoadLineData TrySavePaths(List<int> currentPath, RoadLineData prePathData, int step)
	{
		RoadLineData roadLineData = new RoadLineData(currentPath, prePathData, step);
		_SearchPossiblePaths.Add(roadLineData);
		return roadLineData;
	}

	private List<RoadLineData> GetVailRoadLines(List<RoadLineData> roads)
	{
		List<RoadLineData> list = new List<RoadLineData>();
		for (int i = 0; i < roads.Count; i++)
		{
			if (!list.Contains(roads[i]))
			{
				list.Add(roads[i]);
			}
		}
		return list;
	}

	public List<RoadLineData> GetTargetPathsByTerminal(int terminal)
	{
		List<RoadLineData> list = new List<RoadLineData>();
		for (int i = 0; i < _SearchPossiblePaths.Count; i++)
		{
			RoadLineData roadLineData = _SearchPossiblePaths[i];
			if (roadLineData.Step == 0 && roadLineData.Tail == terminal && !list.Contains(roadLineData))
			{
				list.Add(roadLineData);
			}
		}
		List<RoadLineData> list2 = new List<RoadLineData>();
		for (int j = 0; j < list.Count; j++)
		{
			List<int> list3 = new List<int>();
			list3.AddRange(list[j].Path);
			for (RoadLineData preLines = list[j].PreLines; preLines != null; preLines = preLines.PreLines)
			{
				list3.RemoveAt(0);
				list3.InsertRange(0, preLines.Path);
			}
			RoadLineData item = new RoadLineData(list3, null, 0);
			list2.Add(item);
		}
		return list2;
	}

	private void TryUpdateUpgradeLandId(int curLandId, int step)
	{
		RoomPlayer roomPlayer = _RunningCharacter?.player;
		if (roomPlayer != null && roomPlayer.characterType == CharacterType.Hero && SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(roomPlayer.Id))
		{
			int slot = roomPlayer.Slot;
			UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(curLandId);
			if (((landById.LandType == LandType.Born && (step == 0 || landById.GetNodeInfo().playerSerialNumber == slot)) || landById.LandType == LandType.FillingStation) && !UpgradeLandIds.Contains(curLandId))
			{
				UpgradeLandIds.Add(curLandId);
			}
		}
	}

	private bool UpgradeTipLicense()
	{
		Character runningCharacter = _RunningCharacter;
		if ((object)runningCharacter != null && runningCharacter.player.Level > 6)
		{
			return false;
		}
		return UpgradeStatus();
	}

	private bool UpgradeStatus()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo == null)
		{
			return false;
		}
		if (_RunningCharacter == null)
		{
			return false;
		}
		BattleProperty property = _RunningCharacter.player.Property;
		if (curRoomInfo.MapType == 7)
		{
			return false;
		}
		if (property.level.Value < 3)
		{
			return StaticConfigure.Upgrade.DataDict[curRoomInfo.UpgradePlan].UpgradeDataConfigureItems[property.level.Value].Gold <= property.gold.Value;
		}
		return false;
	}

	private void ShowSuggestArrow()
	{
		if (!GameSettings.RoadSuggest || !UpgradeStatus() || !SimpleSingletonProvider<MoveArrowManager>.inst.IsSelectDir() || UpgradeLandIds.Count == 0)
		{
			return;
		}
		HashSet<RoadLineData> hashSet = new HashSet<RoadLineData>();
		foreach (int upgradeLandId in UpgradeLandIds)
		{
			UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(upgradeLandId);
			bool flag = landById.LandType == LandType.Born && landById.GetNodeInfo().playerSerialNumber != _RunningCharacter.player.Slot;
			foreach (RoadLineData searchPossiblePath in _SearchPossiblePaths)
			{
				bool flag2 = false;
				if ((!flag) ? (searchPossiblePath.Path != null && searchPossiblePath.Path.Contains(upgradeLandId)) : (searchPossiblePath.Step == 0 && searchPossiblePath.Tail == landById.Id))
				{
					RoadLineData item = FindRootPath(searchPossiblePath);
					hashSet.Add(item);
				}
			}
		}
		bool flag3 = false;
		int value = _RunningCharacter.player.Property.level.Value;
		foreach (RoadLineData item2 in hashSet)
		{
			List<int> path = item2.Path;
			if (path != null && path.Count > 1)
			{
				flag3 = true;
				SimpleSingletonProvider<MoveArrowManager>.inst.ShowSuggestArrow(path[0], path[1], value);
			}
		}
		if (flag3 && SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_RunningCharacter.player.Id))
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowTopTip(1010007.GetLocal(UIStringType.GUI)).Forget();
		}
	}

	private RoadLineData FindRootPath(RoadLineData pathData)
	{
		while (pathData.PreLines != null)
		{
			pathData = pathData.PreLines;
		}
		return pathData;
	}
}
