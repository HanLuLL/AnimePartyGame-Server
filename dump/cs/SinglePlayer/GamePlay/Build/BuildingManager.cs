using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic;
using SinglePlayer.GamePlay.Card;
using SinglePlayer.GamePlay.Map;
using SinglePlayer.Tools;
using Tools;
using UnityEngine;
using party.model;

namespace SinglePlayer.GamePlay.Build;

public class BuildingManager
{
	private readonly PriorityQueue<BuildingExecutionItem> executionQueue = new PriorityQueue<BuildingExecutionItem>(new BuildingCompare());

	private BuildingData BuildingData { get; set; }

	private GlobalSignal GlobalSignal { get; set; }

	public void Initialize()
	{
		BuildingData = Game.GetModel<GameData>().BuildingData;
		GlobalSignal = Game.GetModel<GlobalSignal>();
		GlobalSignal.MoveStop.AddListener(OnMoveStop);
		GlobalSignal.PassLand.AddListener(OnPassLand);
		GlobalSignal.RoundStartBefore.AddListener(OnRoundStartBefore);
		GlobalSignal.CardUpgrade.AddListener(OnBuildingUpgrade);
	}

	public void Dispose()
	{
		GlobalSignal.MoveStop.RemoveListener(OnMoveStop);
		GlobalSignal.PassLand.RemoveListener(OnPassLand);
		GlobalSignal.RoundStartBefore.RemoveListener(OnRoundStartBefore);
		GlobalSignal.CardUpgrade.RemoveListener(OnBuildingUpgrade);
	}

	private void OnHeroUpgrade(int star)
	{
		SinglePlayerUpgradeConfigureItem heroUpgradeInfo = Game.GetModel<GameData>().GetHeroUpgradeInfo(star);
		if (heroUpgradeInfo == null)
		{
			Debug.LogError($"无法获取等级为:{star}的配置！");
		}
		else if (heroUpgradeInfo.UpgradeCard != 0)
		{
			SinglePlayer.GamePlay.Card.Card card = Game.GetSystem<BoardManager>().cardManager.GenerateCard(heroUpgradeInfo.UpgradeCard);
			BuildingFoundation specialBuildingFoundation = Game.GetModel<GameData>().MapData.GetSpecialBuildingFoundation();
			if (specialBuildingFoundation == null)
			{
				Debug.LogError("#建筑物模块# 地图上不存在可用的特殊地基");
				return;
			}
			AddBuilding(specialBuildingFoundation.Id, card.UID);
			Game.GetModel<GlobalSignal>().GetCardPerformance.Dispatch(card.UID);
		}
	}

	public void AddBuilding(int buildingFoundationId, int cardUid, SingleBuilding serverData = null)
	{
		if (buildingFoundationId == 0)
		{
			Debug.LogError("#建筑物模块# 添加建筑失败，地基ID为零");
			Game.GetModel<GlobalSignal>().CardUsed.Dispatch(cardUid, t2: false);
			return;
		}
		if (cardUid == 0)
		{
			Debug.LogError("#建筑物模块# 添加建筑失败，Card UID为零");
			Game.GetModel<GlobalSignal>().CardUsed.Dispatch(cardUid, t2: false);
			return;
		}
		SinglePlayer.GamePlay.Card.Card cardByUID = Game.GetSystem<BoardManager>().cardManager.GetCardByUID(cardUid);
		if (cardByUID == null)
		{
			Debug.LogError($"#建筑物模块# 添加建筑失败，未找到Card实例，Card UID :{cardUid}");
			Game.GetModel<GlobalSignal>().CardUsed.Dispatch(cardUid, t2: false);
			return;
		}
		if (BuildingData.TryGetByCardUid(cardUid, out var _))
		{
			Debug.LogError($"#建筑物模块# 添加建筑失败，Card UID :{cardUid}已经创建过建筑了");
			Game.GetModel<GlobalSignal>().CardUsed.Dispatch(cardUid, t2: false);
			return;
		}
		if (serverData == null && cardByUID.Level.Value == SinglePlayer.GamePlay.Card.Card.MaxLevel())
		{
			Game.GetModel<GlobalSignal>().CardUsed.Dispatch(cardUid, t2: false);
			return;
		}
		if (TryGetBuildingByFoundationId(buildingFoundationId, out var building2))
		{
			if (building2.Card.CardConfigure.Id != cardByUID.CardConfigure.Id)
			{
				Game.GetModel<GlobalSignal>().CardUsed.Dispatch(cardUid, t2: false);
				return;
			}
			if (building2.Card.Level.Value == SinglePlayer.GamePlay.Card.Card.MaxLevel())
			{
				Game.GetModel<GlobalSignal>().CardUsed.Dispatch(cardUid, t2: false);
				return;
			}
			building2.UserCard(cardByUID);
			Game.GetModel<GlobalSignal>().BuildingMoveUpgrade.Dispatch();
			return;
		}
		string text = string.Format("{0}{1}", "SinglePlayer.GamePlay.Build.Building_", cardByUID.CardConfigure.Id);
		Type type = Type.GetType(text);
		if (type == null)
		{
			Debug.LogError("#建筑物模块# 添加建筑失败，未找到类型:" + text);
			return;
		}
		if (!(Activator.CreateInstance(type) is BuildingBase buildingBase))
		{
			Debug.LogError("#建筑物模块# 添加建筑失败，反射创建实例失败:" + text);
			return;
		}
		buildingBase.Initialize(UIDGenerator.NextUID(), cardByUID, buildingFoundationId, serverData);
		BuildingData.Add(buildingBase, buildingFoundationId);
		Game.GetModel<GlobalSignal>().BuildingCreate.Dispatch(buildingFoundationId, buildingBase.Id, cardByUID.UID);
		Game.GetModel<GlobalSignal>().CardUsed.Dispatch(cardUid, t2: true);
	}

	public void HandleMoveBuildingToTarget(int sourceBuildingFoundationId, int targetBuildingFoundationId)
	{
		if (targetBuildingFoundationId == 0)
		{
			return;
		}
		BuildingBase building2;
		BuildingBase building3;
		BuildingBase building4;
		if (sourceBuildingFoundationId == targetBuildingFoundationId)
		{
			if (TryGetBuildingByFoundationId(sourceBuildingFoundationId, out var building))
			{
				Game.GetModel<GlobalSignal>().BuildingMoveFailure.Dispatch(sourceBuildingFoundationId, building.Id, building.Card.UID);
			}
		}
		else if (!BuildingData.TryGetByFoundationId(sourceBuildingFoundationId, out building2))
		{
			Debug.LogError($"#建筑物模块# 移动建筑失败，地基{sourceBuildingFoundationId}不存在建筑");
		}
		else if (building2.Card.Level.Value == SinglePlayer.GamePlay.Card.Card.MaxLevel())
		{
			Game.GetModel<GlobalSignal>().BuildingMoveFailure.Dispatch(sourceBuildingFoundationId, building2.Id, building2.Card.UID);
		}
		else if (BuildingData.TryGetByFoundationId(targetBuildingFoundationId, out building3))
		{
			if (building2.Card.CardConfigure.Id != building3.Card.CardConfigure.Id)
			{
				Game.GetModel<GlobalSignal>().BuildingMoveFailure.Dispatch(sourceBuildingFoundationId, building2.Id, building2.Card.UID);
				return;
			}
			if (building3.Card.Level.Value == SinglePlayer.GamePlay.Card.Card.MaxLevel())
			{
				Game.GetModel<GlobalSignal>().BuildingMoveFailure.Dispatch(sourceBuildingFoundationId, building2.Id, building2.Card.UID);
				return;
			}
			building3.UserCard(building2.Card);
			RemoveBuilding(building2.Card.UID);
			Game.GetModel<GlobalSignal>().BuildingMoveUpgrade.Dispatch();
		}
		else if (!BuildingData.TryGetByFoundationId(sourceBuildingFoundationId, out building4))
		{
			Debug.LogError($"#建筑物模块# 移动建筑失败，地基{sourceBuildingFoundationId}不存在建筑");
		}
		else
		{
			BuildingData.RemoveFromFoundationMapping(sourceBuildingFoundationId);
			building4.SetBuildingFoundationId(targetBuildingFoundationId);
			BuildingData.AddToFoundationMapping(building4.BuildingFoundationId, building4);
			Game.GetModel<GlobalSignal>().BuildingMoveSuccess.Dispatch(sourceBuildingFoundationId, building4.Id, targetBuildingFoundationId);
		}
	}

	public void MoveBuildingFailure(int sourceBuildingFoundationId)
	{
		if (BuildingData.TryGetByFoundationId(sourceBuildingFoundationId, out var building))
		{
			Game.GetModel<GlobalSignal>().BuildingMoveFailure.Dispatch(sourceBuildingFoundationId, building.Id, building.Card.UID);
		}
	}

	public bool RemoveBuilding(int cardUid)
	{
		if (cardUid == 0)
		{
			Debug.LogError("#建筑物模块# 移除建筑失败，Card UID为零");
			return false;
		}
		if (!BuildingData.TryGetByCardUid(cardUid, out var building))
		{
			Debug.LogError($"#建筑物模块# 移除建筑失败，未找到Card UID为{cardUid}的建筑");
			return false;
		}
		Game.GetModel<GlobalSignal>().BuildingRemove.Dispatch(building.BuildingFoundationId, building.Card.UID, building.Id);
		BuildingData.Remove(building);
		building.Dispose();
		return true;
	}

	public bool TryGetBuildingByFoundationId(int buildingFoundationId, out BuildingBase building)
	{
		return BuildingData.TryGetByFoundationId(buildingFoundationId, out building);
	}

	public bool TryGetBuildingById(int buildingId, out BuildingBase building)
	{
		return BuildingData.TryGetByBuildingId(buildingId, out building);
	}

	public bool TryGetBuildingByCardUid(int cardUid, out BuildingBase building)
	{
		return BuildingData.TryGetByCardUid(cardUid, out building);
	}

	public void AddExecuteQueue(BuildingBase buildingBase)
	{
		if (buildingBase != null)
		{
			executionQueue.Enqueue(new BuildingExecutionItem(buildingBase, BuildingExecutionType.NormalDice));
		}
	}

	public void AddForcedExecuteQueue(BuildingBase buildingBase, int count = 1)
	{
		if (buildingBase != null && count > 0)
		{
			executionQueue.Enqueue(new BuildingExecutionItem(buildingBase, BuildingExecutionType.ForcedDice, count));
		}
	}

	public void AddForceExecuteStayQueue(BuildingBase buildingBase)
	{
		if (buildingBase != null)
		{
			executionQueue.Enqueue(new BuildingExecutionItem(buildingBase, BuildingExecutionType.ForcedStay));
		}
	}

	public List<BuildingBase> GetBuildingsByTags(SinglePlayerTagType tag)
	{
		List<BuildingBase> list = new List<BuildingBase>();
		foreach (BuildingBase buildingDatum in BuildingData)
		{
			if (buildingDatum.Card.CardConfigure.CardTag.Contains(tag))
			{
				list.Add(buildingDatum);
			}
		}
		return list;
	}

	public List<BuildingBase> GetBuildingsByConfigId(int configId)
	{
		List<BuildingBase> list = new List<BuildingBase>();
		foreach (BuildingBase buildingDatum in BuildingData)
		{
			if (buildingDatum.Card.CardConfigure.Id == configId)
			{
				list.Add(buildingDatum);
			}
		}
		return list;
	}

	public List<BuildingBase> GetBuildingsByConfigId(int[] configIds)
	{
		List<BuildingBase> list = new List<BuildingBase>();
		foreach (BuildingBase buildingDatum in BuildingData)
		{
			if (configIds.Contains(buildingDatum.Card.CardConfigure.Id))
			{
				list.Add(buildingDatum);
			}
		}
		return list;
	}

	public List<BuildingBase> GetRandomBuildings(int count, BuildingBase exclude = null)
	{
		List<BuildingBase> list = new List<BuildingBase>();
		if (count <= 0)
		{
			return list;
		}
		List<BuildingBase> list2 = new List<BuildingBase>(BuildingData);
		if (exclude != null)
		{
			list2.Remove(exclude);
		}
		int num = Mathf.Min(count, list2.Count);
		for (int i = 0; i < num; i++)
		{
			int index = UnityEngine.Random.Range(0, list2.Count);
			list.Add(list2[index]);
			list2.RemoveAt(index);
		}
		return list;
	}

	public int GetBuildingCountByTags(SinglePlayerTagType tag)
	{
		return BuildingData.Count((BuildingBase kv) => kv.Card.CardConfigure.CardTag.Contains(tag));
	}

	public int GetHeroNearBuildingCount(SinglePlayerTagType tag)
	{
		GameData model = Game.GetModel<GameData>();
		int standLandId = model.heroProperty.StandLandId;
		Land landById = model.MapData.GetLandById(standLandId);
		if (landById == null)
		{
			return 0;
		}
		int num = 0;
		if (TryGetBuildingByFoundationId(landById.BuildingFoundationId, out var building) && building.Card.CardConfigure.CardTag.Contains(tag))
		{
			num++;
		}
		foreach (int neighborLandId in landById.NeighborLandIds)
		{
			Land landById2 = model.MapData.GetLandById(neighborLandId);
			if (landById2 == null)
			{
				return 0;
			}
			if (TryGetBuildingByFoundationId(landById2.BuildingFoundationId, out building) && building.Card.CardConfigure.CardTag.Contains(tag))
			{
				num++;
			}
		}
		return num;
	}

	public bool CanUsedCardInBuildingFoundation(int buildingFoundationId, SinglePlayer.GamePlay.Card.Card card)
	{
		if (card.Level.Value == SinglePlayer.GamePlay.Card.Card.MaxLevel())
		{
			return false;
		}
		if (TryGetBuildingByFoundationId(buildingFoundationId, out var building))
		{
			if (building.Card.CardConfigure.Id != card.CardConfigure.Id)
			{
				return false;
			}
			if (building.Card.Level.Value == SinglePlayer.GamePlay.Card.Card.MaxLevel())
			{
				return false;
			}
		}
		return true;
	}

	public void DoEffect()
	{
		while (!executionQueue.IsEmpty)
		{
			BuildingExecutionItem buildingExecutionItem = executionQueue.Dequeue();
			if (buildingExecutionItem?.Building != null)
			{
				switch (buildingExecutionItem.ExecutionType)
				{
				case BuildingExecutionType.ForcedDice:
					buildingExecutionItem.Building.TriggerForcedThrowDiceEffect(buildingExecutionItem.TriggerCount);
					break;
				case BuildingExecutionType.NormalDice:
					buildingExecutionItem.Building.TriggerThrowDiceEffect();
					break;
				case BuildingExecutionType.ForcedStay:
					buildingExecutionItem.Building.Stay();
					break;
				}
			}
		}
	}

	private void OnPassLand(int landId, int buildingFoundationId)
	{
		if (TryGetBuildingByFoundationId(buildingFoundationId, out var building))
		{
			building.Enter();
		}
	}

	private void OnMoveStop(Land land)
	{
		if (TryGetBuildingByFoundationId(land.BuildingFoundationId, out var building))
		{
			building.Stay();
		}
	}

	private int Comparison(BuildingBase x, BuildingBase y)
	{
		int num = x.Card.CardConfigure.TriggerPriority.CompareTo(y.Card.CardConfigure.TriggerPriority);
		if (num == 0)
		{
			GameData model = Game.GetModel<GameData>();
			Land landByBuildingFoundationId = model.MapData.GetLandByBuildingFoundationId(x.BuildingFoundationId);
			BuildingFoundation buildingFoundationById = model.MapData.GetBuildingFoundationById(y.BuildingFoundationId);
			if (landByBuildingFoundationId == null || buildingFoundationById == null)
			{
				return 0;
			}
			int num2 = Game.GetModel<GameData>().MapData.Distance(model.heroProperty.StandLandId, landByBuildingFoundationId.Id);
			int value = Game.GetModel<GameData>().MapData.Distance(model.heroProperty.StandLandId, buildingFoundationById.Id);
			return num2.CompareTo(value);
		}
		return num;
	}

	public void TrySellingBuilding(int cardUid)
	{
		if (TryGetBuildingByCardUid(cardUid, out var building))
		{
			Game.GetModel<GlobalSignal>().SellingBuilding.Dispatch(building.Card.CardConfigure.Id);
			RemoveBuilding(cardUid);
		}
	}

	private void OnRoundStartBefore()
	{
		BuildingData.RoundUpgradeCount = 0;
	}

	private void OnBuildingUpgrade(int value)
	{
		BuildingData.RoundUpgradeCount++;
	}

	public void BuildBattleFieldGameObject()
	{
		SingleGameData singleGameData = SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer?.ServerGameData;
		if (singleGameData?.BuildingData?.Buildings == null)
		{
			return;
		}
		foreach (SingleBuilding building in singleGameData.BuildingData.Buildings)
		{
			AddBuilding(building.BuildingFoundationId, building.CardUID, building);
		}
	}
}
