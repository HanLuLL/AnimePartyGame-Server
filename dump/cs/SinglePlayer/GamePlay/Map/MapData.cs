using System;
using System.Collections.Generic;
using System.Linq;
using FairyGUI;
using Google.Protobuf.Collections;
using UnityEngine;
using party.model;

namespace SinglePlayer.GamePlay.Map;

public class MapData
{
	private List<Land> _mapLands;

	private List<BuildingFoundation> _buildingFoundations;

	private Dictionary<int, Land> _foundationLandMapping;

	private List<MapMission> _mapMissionData;

	private Dictionary<int, List<int>> _graph = new Dictionary<int, List<int>>();

	private Queue<int> _queue = new Queue<int>();

	private Dictionary<int, int> _distance = new Dictionary<int, int>();

	private HashSet<int> _visited = new HashSet<int>();

	public List<BuildingFoundation> BuildingFoundations => _buildingFoundations;

	public List<MapMission> MapMissionData => _mapMissionData;

	public int MaxProgress { get; private set; }

	public int MissionCount { get; private set; }

	public void Initialize(SingleMapData mapData)
	{
		InitializeLand();
		InitializeBuildingFoundationData(mapData?.Wastefoundation);
		InitializeMapMission(mapData);
		InitializeMapGraph();
	}

	public void ReInitMapMission()
	{
		InitializeMapMission(null);
	}

	public void Dispose()
	{
		foreach (BuildingFoundation buildingFoundation in _buildingFoundations)
		{
			buildingFoundation.Dispose();
		}
	}

	private void InitializeLand()
	{
		_mapLands = UnityEngine.Object.FindObjectsOfType<Land>().ToList();
		Dictionary<int, Type> dictionary = new Dictionary<int, Type>();
		foreach (Land mapLand in _mapLands)
		{
			if (!dictionary.TryGetValue((int)mapLand.LandType, out var value))
			{
				value = Type.GetType($"SinglePlayer.GamePlay.Map.DiceLandComponent_{mapLand.LandType}");
				dictionary.Add((int)mapLand.LandType, value);
			}
			mapLand.Initialize(value);
		}
	}

	public Land GetFillingStationLand()
	{
		Land land = _mapLands.Find((Land x) => x.LandType == SinglePlayerLandType.Start);
		if (land == null)
		{
			Debug.LogError("无法获取当前地图配置的加油站地图格");
			return _mapLands[0];
		}
		return land;
	}

	public Land GetLandById(int landId)
	{
		return _mapLands.Find((Land x) => x.Id == landId);
	}

	public SinglePlayerLandType GetLandTypeById(int landId)
	{
		Land landById = GetLandById(landId);
		if (landById == null)
		{
			return SinglePlayerLandType.None;
		}
		return landById.LandType;
	}

	public List<int> GetNextLandIds(int standLandId, int preLandId)
	{
		return GetLandById(standLandId).GetNextLandIds(preLandId);
	}

	public Transform GetElementByRay(string LayerName)
	{
		if (Stage.isTouchOnUI && GRoot.inst.touchTarget.touchable)
		{
			return null;
		}
		Vector2 touchPosition = Stage.inst.touchPosition;
		touchPosition.y = (float)Screen.height - touchPosition.y;
		if (Camera.main != null)
		{
			Ray ray = Camera.main.ScreenPointToRay(touchPosition);
			LayerMask layerMask = LayerMask.GetMask(LayerName);
			RaycastHit val = default(RaycastHit);
			if (Physics.Raycast(ray, ref val, 500f, (int)layerMask))
			{
				return ((RaycastHit)(ref val)).transform;
			}
		}
		return null;
	}

	public BuildingFoundation GetBuildingFoundationById(int foundationId)
	{
		return _buildingFoundations.Find((BuildingFoundation x) => x.Id == foundationId);
	}

	public BuildingFoundation GetSpecialBuildingFoundation()
	{
		foreach (BuildingFoundation buildingFoundation in _buildingFoundations)
		{
			if (buildingFoundation.Special && !buildingFoundation.HasBuilding())
			{
				return buildingFoundation;
			}
		}
		return null;
	}

	public Land GetLandByBuildingFoundationId(int foundationId)
	{
		if (!_foundationLandMapping.TryGetValue(foundationId, out var value))
		{
			Debug.LogError("无法通过地基ID获取对应地图格，地基ID:" + foundationId);
			return null;
		}
		return value;
	}

	public int GetWastelandCount()
	{
		int num = 0;
		foreach (BuildingFoundation buildingFoundation in BuildingFoundations)
		{
			if (buildingFoundation.Wasteland)
			{
				num++;
			}
		}
		return num;
	}

	private void InitializeMapMission(SingleMapData mapData)
	{
		int levelId = Game.GetModel<GameData>().LevelId;
		if (!StaticConfigure.SinglePlayer.LevelProgressDict.TryGetValue(levelId, out var value))
		{
			Debug.LogError($"无法通过关卡ID:{levelId}, 获取对应的关卡任务");
		}
		else
		{
			if (value == null)
			{
				return;
			}
			RepeatedField<int> repeatedField = mapData?.FinishMissionIndex;
			int num = mapData?.CurrentMissionProgress ?? 0;
			RepeatedField<SinglePlayerLevelProgressConfigureItem> singlePlayerLevelProgressConfigureItems = value.SinglePlayerLevelProgressConfigureItems;
			_mapMissionData = new List<MapMission>(singlePlayerLevelProgressConfigureItems.Count);
			foreach (SinglePlayerLevelProgressConfigureItem item in singlePlayerLevelProgressConfigureItems)
			{
				MapMission classInstance = ReflectionHelper.GetClassInstance<MapMission>($"SinglePlayer.GamePlay.Map.MapMission_{value.Id}");
				if (classInstance == null)
				{
					return;
				}
				bool flag = repeatedField?.Contains(item.MissionIndex) ?? false;
				classInstance.Initialize(levelId, item, flag);
				if (!flag && num > 0)
				{
					classInstance.InitializeProgress(num);
					num = 0;
				}
				_mapMissionData.Add(classInstance);
			}
			RepeatedField<SinglePlayerLevelProgressConfigureItem> singlePlayerLevelProgressConfigureItems2 = value.SinglePlayerLevelProgressConfigureItems;
			MaxProgress = singlePlayerLevelProgressConfigureItems2[singlePlayerLevelProgressConfigureItems2.Count - 1].ProgressValue;
			MissionCount = value.SinglePlayerLevelProgressConfigureItems.Count;
		}
	}

	private void InitializeMapGraph()
	{
		_graph.Clear();
		foreach (Land mapLand in _mapLands)
		{
			foreach (int neighborLandId in mapLand.NeighborLandIds)
			{
				AddEdge(mapLand.Id, neighborLandId);
			}
		}
	}

	private void AddEdge(int u, int v)
	{
		if (!_graph.ContainsKey(u))
		{
			_graph[u] = new List<int>();
		}
		if (!_graph.ContainsKey(v))
		{
			_graph[v] = new List<int>();
		}
		_graph[u].Add(v);
		_graph[v].Add(u);
	}

	private void ClearGraph()
	{
		_queue.Clear();
		_distance.Clear();
		_visited.Clear();
	}

	public int Distance(int start, int end)
	{
		if (!_graph.ContainsKey(start) || !_graph.ContainsKey(end))
		{
			return -1;
		}
		_queue.Enqueue(start);
		_distance[start] = 0;
		_visited.Add(start);
		while (_queue.Count > 0)
		{
			int num = _queue.Dequeue();
			if (num == end)
			{
				int result = _distance[num];
				ClearGraph();
				return result;
			}
			foreach (int item in _graph[num])
			{
				if (_visited.Add(item))
				{
					_distance[item] = _distance[num] + 1;
					_queue.Enqueue(item);
				}
			}
		}
		ClearGraph();
		return -1;
	}

	private void InitializeBuildingFoundationData(RepeatedField<int> wasteFoundations)
	{
		_buildingFoundations = UnityEngine.Object.FindObjectsOfType<BuildingFoundation>().ToList();
		_buildingFoundations.Sort((BuildingFoundation a, BuildingFoundation b) => a.Id.CompareTo(b.Id));
		_foundationLandMapping = new Dictionary<int, Land>();
		foreach (Land mapLand in _mapLands)
		{
			BuildingFoundation buildingFoundationById = GetBuildingFoundationById(mapLand.BuildingFoundationId);
			if (!(buildingFoundationById == null))
			{
				if (wasteFoundations != null && !wasteFoundations.Contains(buildingFoundationById.Id))
				{
					buildingFoundationById.FinishDevelopLand();
				}
				_foundationLandMapping.Add(mapLand.BuildingFoundationId, mapLand);
			}
		}
		foreach (BuildingFoundation buildingFoundation in _buildingFoundations)
		{
			buildingFoundation.Init();
		}
	}

	public SingleMapData GetUploadData()
	{
		SingleMapData singleMapData = new SingleMapData();
		List<int> values = (from x in _mapMissionData
			where x.Status == MapMissionStatus.Success
			select x.Id).ToList();
		singleMapData.FinishMissionIndex.AddRange(values);
		List<int> values2 = (from x in _buildingFoundations
			where x.Wasteland
			select x.Id).ToList();
		singleMapData.Wastefoundation.AddRange(values2);
		MapMission currentMissionData = Game.GetSystem<BoardManager>().missionManager.GetCurrentMissionData();
		if (currentMissionData != null)
		{
			singleMapData.CurrentMissionProgress = currentMissionData.MissionProgress;
		}
		else
		{
			List<MapMission> mapMissionData = Game.GetModel<GameData>().MapData.MapMissionData;
			singleMapData.CurrentMissionProgress = mapMissionData[mapMissionData.Count - 1].Deadline;
		}
		return singleMapData;
	}
}
