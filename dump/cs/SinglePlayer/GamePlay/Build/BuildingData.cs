using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Collections;
using party.model;

namespace SinglePlayer.GamePlay.Build;

public class BuildingData : IEnumerable<BuildingBase>, IEnumerable
{
	private readonly Dictionary<int, BuildingBase> _buildings = new Dictionary<int, BuildingBase>();

	private readonly Dictionary<int, BuildingBase> _buildingFoundationMapping = new Dictionary<int, BuildingBase>();

	private readonly Dictionary<int, BuildingBase> _buildingCardMapping = new Dictionary<int, BuildingBase>();

	public int RoundUpgradeCount { get; set; }

	public void Dispose()
	{
		foreach (KeyValuePair<int, BuildingBase> building in _buildings)
		{
			building.Value.Dispose();
		}
		_buildings.Clear();
		_buildingFoundationMapping.Clear();
		_buildingCardMapping.Clear();
	}

	public void Add(BuildingBase building, int buildingFoundationId)
	{
		_buildings.Add(building.Id, building);
		_buildingFoundationMapping.Add(buildingFoundationId, building);
		_buildingCardMapping.Add(building.Card.UID, building);
	}

	public void Remove(BuildingBase building)
	{
		_buildings.Remove(building.Id);
		_buildingFoundationMapping.Remove(building.BuildingFoundationId);
		_buildingCardMapping.Remove(building.Card.UID);
	}

	public void AddToFoundationMapping(int buildingFoundationId, BuildingBase building)
	{
		_buildingFoundationMapping.Add(buildingFoundationId, building);
	}

	public void RemoveFromFoundationMapping(int buildingFoundationId)
	{
		_buildingFoundationMapping.Remove(buildingFoundationId);
	}

	public bool TryGetByBuildingId(int buildingId, out BuildingBase building)
	{
		return _buildings.TryGetValue(buildingId, out building);
	}

	public bool TryGetByFoundationId(int foundationId, out BuildingBase building)
	{
		return _buildingFoundationMapping.TryGetValue(foundationId, out building);
	}

	public bool TryGetByCardUid(int cardUid, out BuildingBase building)
	{
		return _buildingCardMapping.TryGetValue(cardUid, out building);
	}

	public SingleBuildingData GetUploadData()
	{
		RepeatedField<SingleBuilding> repeatedField = new RepeatedField<SingleBuilding>();
		foreach (BuildingBase value in _buildings.Values)
		{
			repeatedField.Add(value.GetUploadData());
		}
		return new SingleBuildingData
		{
			Buildings = { (IEnumerable<SingleBuilding>)repeatedField },
			BuildingUpgradeCount = RoundUpgradeCount
		};
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public IEnumerator<BuildingBase> GetEnumerator()
	{
		foreach (BuildingBase value in _buildings.Values)
		{
			yield return value;
		}
	}
}
