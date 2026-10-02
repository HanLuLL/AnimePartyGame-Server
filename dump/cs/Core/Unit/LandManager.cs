using System.Collections.Generic;
using GameLogic;
using Tools;
using UnityEngine;

namespace Core.Unit;

public class LandManager : SimpleSingletonProvider<LandManager>
{
	public CoroutineManager.CoroutineState _coroutineState;

	public Dictionary<int, UnitLand> NodeDict;

	public MapGimmickManager MapGimmickManager;

	private readonly List<int> scopeLands = new List<int>();

	public void InitLand()
	{
		NodeDict = new Dictionary<int, UnitLand>();
		UnitLand[] array = Object.FindObjectsOfType<UnitLand>();
		foreach (UnitLand unitLand in array)
		{
			int id = unitLand.Id;
			NodeDict.TryAdd(id, unitLand);
		}
		MapGimmickManager = Object.FindObjectOfType<MapGimmickManager>();
	}

	public UnitLand GetLandById(int _Id)
	{
		return NodeDict.GetValueOrDefault(_Id);
	}

	public List<int> GetNeighborLandIds(int _Id)
	{
		return GetLandById(_Id).AdjacencyLandIds;
	}

	public UnitLand GetBornLand(int _Id)
	{
		UnitLand landById = GetLandById(_Id);
		if (landById == null || landById.LandType != LandType.Born)
		{
			Debug.LogError($"Id_{_Id}在当前地图中并不是出生点");
		}
		return landById;
	}

	public UnitLand GetFirstBornLand()
	{
		foreach (KeyValuePair<int, UnitLand> item in NodeDict)
		{
			if (item.Value.LandType == LandType.Born && item.Value.PlayerSerialNumber == 0)
			{
				return item.Value;
			}
		}
		return null;
	}

	public void Test()
	{
		Debug.Log(CheckDistance(6, 45, 10, 0));
		Debug.Log(CheckDistance(6, 33, 10, 0));
		Debug.Log(CheckDistance(6, 12, 10, 0));
	}

	public List<int> GetLandsByScope(int length)
	{
		scopeLands.Clear();
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (selfPlayerData.CharacterInst == null)
		{
			return scopeLands;
		}
		int id = selfPlayerData.CharacterInst.standLand.Id;
		scopeLands.Add(id);
		List<int> adjacencyLandIds = GetLandById(id).AdjacencyLandIds;
		FindLandsByScope(adjacencyLandIds, length);
		return scopeLands;
	}

	private void FindLandsByScope(List<int> neighborLandIds, int length)
	{
		length--;
		if (length < 0)
		{
			return;
		}
		for (int i = 0; i < neighborLandIds.Count; i++)
		{
			int num = neighborLandIds[i];
			if (!scopeLands.Contains(num))
			{
				scopeLands.Add(num);
			}
			List<int> adjacencyLandIds = GetLandById(num).AdjacencyLandIds;
			FindLandsByScope(adjacencyLandIds, length);
		}
	}

	public bool CheckDistance(int startLandId, int targetLandId, int length, int nodeCount)
	{
		nodeCount++;
		if (nodeCount > length)
		{
			return false;
		}
		List<int> neighborLandIds = GetNeighborLandIds(startLandId);
		for (int i = 0; i < neighborLandIds.Count; i++)
		{
			if (neighborLandIds[i] == targetLandId)
			{
				return true;
			}
		}
		for (int j = 0; j < neighborLandIds.Count; j++)
		{
			if (CheckDistance(neighborLandIds[j], targetLandId, length, nodeCount))
			{
				return true;
			}
		}
		return false;
	}

	public void Dispose()
	{
		NodeDict?.Clear();
	}
}
