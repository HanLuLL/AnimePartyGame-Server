using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using SinglePlayer.GamePlay.Card;
using SinglePlayer.GamePlay.Map;
using SinglePlayer.Tools;
using Tools;
using UnityEngine;
using party.model;

namespace SinglePlayer.GamePlay.Build;

public abstract class BuildingBase
{
	private List<int> _usedCards = new List<int>();

	private Int32Encryptor defalutEncryptor = new Int32Encryptor(0);

	private int[] diceSubTriggerCountArray;

	private readonly Dictionary<OperateType, Int32Encryptor> _operateBonus = new Dictionary<OperateType, Int32Encryptor>
	{
		[OperateType.None] = new Int32Encryptor(0),
		[OperateType.ThrowDiceGold] = new Int32Encryptor(0),
		[OperateType.PassGold] = new Int32Encryptor(0),
		[OperateType.StayGold] = new Int32Encryptor(0)
	};

	public int Id { get; private set; }

	public SinglePlayer.GamePlay.Card.Card Card { get; private set; }

	public int Level => Card.Level.Value;

	public int BuildingFoundationId { get; private set; }

	public int ThrowDiceTriggerCount { get; private set; }

	public int PassTriggerCount { get; private set; }

	public int StayTriggerCount { get; private set; }

	public Signal BuildingTriggerPassEffect { get; private set; } = new Signal();

	public Signal BuildingTriggerStayEffect { get; private set; } = new Signal();

	public void Initialize(int id, SinglePlayer.GamePlay.Card.Card card, int buildingFoundationId, SingleBuilding serverData = null)
	{
		Id = id;
		Card = card;
		BuildingFoundationId = buildingFoundationId;
		InitializeFromServerData(serverData);
		Game.GetModel<GlobalSignal>().GenerateDicePoint.AddListener(CheckDicePointEffect);
		Game.GetModel<GameData>().Round.AddListener(OnRoundChange);
		int count = Card.GetConfigureItem().TriggerPoint.Count;
		diceSubTriggerCountArray = new int[count];
		OnCreate();
	}

	private void InitializeFromServerData(SingleBuilding serverData)
	{
		if (serverData == null)
		{
			return;
		}
		Id = serverData.BuildingId;
		BuildingFoundationId = serverData.BuildingFoundationId;
		ThrowDiceTriggerCount = serverData.ThrowDiceTriggerCount;
		PassTriggerCount = serverData.PassTriggerCount;
		StayTriggerCount = serverData.StayTriggerCount;
		foreach (var (key, originalNumber) in serverData.OperateBonus)
		{
			_operateBonus[(OperateType)key].EncryptSet(originalNumber);
		}
	}

	public void Dispose()
	{
		Game.GetModel<GlobalSignal>().GenerateDicePoint.RemoveListener(CheckDicePointEffect);
		Game.GetModel<GameData>().Round.RemoveListener(OnRoundChange);
		OnDestroy();
		Id = 0;
		BuildingFoundationId = 0;
		_usedCards.Clear();
		_usedCards = null;
	}

	public void TriggerThrowDiceEffect()
	{
		SinglePlayerCardConfigureItem configureItem = GetConfigureItem();
		if (configureItem != null && (configureItem.TriggerTimes <= 0 || ThrowDiceTriggerCount < configureItem.TriggerTimes))
		{
			int diceTriggerCount = GetDiceTriggerCount();
			OnDice(diceTriggerCount);
		}
	}

	public void TriggerForcedThrowDiceEffect(int count = 1)
	{
		SinglePlayerCardConfigureItem configureItem = GetConfigureItem();
		if (configureItem != null && (configureItem.TriggerTimes <= 0 || ThrowDiceTriggerCount < configureItem.TriggerTimes))
		{
			Array.Fill(diceSubTriggerCountArray, 0);
			if (diceSubTriggerCountArray.Length != 0)
			{
				diceSubTriggerCountArray[0] = count;
			}
			OnDice(count);
		}
	}

	protected virtual void OnCreate()
	{
	}

	protected virtual void OnDestroy()
	{
	}

	public void Enter()
	{
		SinglePlayerCardConfigureItem configureItem = GetConfigureItem();
		if (configureItem != null && (configureItem.WalkTimes <= 0 || PassTriggerCount < configureItem.WalkTimes))
		{
			bool trigger = false;
			OnEnter(ref trigger);
			if (trigger)
			{
				PassTriggerCount++;
				BuildingTriggerPassEffect.Dispatch();
			}
		}
	}

	public void Stay()
	{
		SinglePlayerCardConfigureItem configureItem = GetConfigureItem();
		if (configureItem != null && (configureItem.StopTimes <= 0 || StayTriggerCount < configureItem.StopTimes))
		{
			bool trigger = false;
			OnStay(ref trigger);
			if (trigger)
			{
				StayTriggerCount++;
				BuildingTriggerStayEffect.Dispatch();
				Game.GetModel<GlobalSignal>().BuildingTriggerStayEffect.Dispatch(Card.CardConfigure.Id);
			}
		}
	}

	protected virtual void OnDice(int count)
	{
		ThrowDiceTriggerCount++;
	}

	protected virtual void OnEnter(ref bool trigger)
	{
	}

	protected virtual void OnStay(ref bool trigger)
	{
	}

	public SinglePlayerCardConfigureItem GetConfigureItem()
	{
		return Card.GetConfigureItem();
	}

	public void UserCard(SinglePlayer.GamePlay.Card.Card card)
	{
		int num = card.ConvertToExp();
		if (Card.CardConfigure.Id == card.CardConfigure.Id)
		{
			SinglePlayerParamConfigure safeByIndex = StaticConfigure.SinglePlayer.Params.GetSafeByIndex(0);
			if (safeByIndex != null)
			{
				num += safeByIndex.SameCardExp;
			}
		}
		AddExp(num);
		_usedCards.Add(card.UID);
		Game.GetModel<GlobalSignal>().CardUsed.Dispatch(card.UID, t2: true);
	}

	public void AddExp(int exp)
	{
		Card.Exp.Value += exp;
	}

	public void AddExp(int exp, BuildingBase sourceBuilding)
	{
		if (Game.GetController<BuildingController>().TryGetBuildingView(Id, out var buildingView))
		{
			Game.GetController<BuildingController>().AddWaitAddExpBuildingCount();
			sourceBuilding.FireBullet(buildingView.GetPosition(), delegate
			{
				Game.GetController<BuildingController>().CutWaitAddExpBuildingCount();
				AddExp(exp);
			});
		}
		else
		{
			AddExp(exp);
		}
	}

	private void FireBullet(Vector3 targetPosition, System.Action complete)
	{
		if (Game.GetController<BuildingController>().TryGetBuildingView(Id, out var buildingView))
		{
			buildingView.FireBullet(targetPosition, complete).Forget();
		}
		else
		{
			complete();
		}
	}

	public void SetBuildingFoundationId(int buildingFoundationId)
	{
		BuildingFoundationId = buildingFoundationId;
	}

	public void AddExecuteQueue()
	{
		Game.GetSystem<BoardManager>().buildingManager.AddExecuteQueue(this);
	}

	public void AddForcedExecuteDiceQueue(int count = 1)
	{
		Game.GetSystem<BoardManager>().buildingManager.AddForcedExecuteQueue(this, count);
	}

	public void AddForceExecuteStayQueue()
	{
		Game.GetSystem<BoardManager>().buildingManager.AddForceExecuteStayQueue(this);
	}

	public List<BuildingBase> GetNeighborBuildings()
	{
		List<BuildingBase> list = new List<BuildingBase>();
		MapData mapData = Game.GetModel<GameData>().MapData;
		Land landByBuildingFoundationId = mapData.GetLandByBuildingFoundationId(BuildingFoundationId);
		if (landByBuildingFoundationId == null)
		{
			return list;
		}
		BuildingManager buildingManager = Game.GetSystem<BoardManager>().buildingManager;
		foreach (int neighborLandId in landByBuildingFoundationId.NeighborLandIds)
		{
			Land landById = mapData.GetLandById(neighborLandId);
			if (landById == null)
			{
				continue;
			}
			BuildingBase building2;
			if (landById.IsSpecialLand() || landById.BuildingFoundationId == 0)
			{
				foreach (int neighborLandId2 in landById.NeighborLandIds)
				{
					if (neighborLandId2 == landByBuildingFoundationId.Id)
					{
						continue;
					}
					Land landById2 = mapData.GetLandById(neighborLandId2);
					if (!(landById2 == null))
					{
						BuildingBase building = landById2.GetBuilding();
						if (building != null)
						{
							list.Add(building);
						}
					}
				}
			}
			else if (buildingManager.TryGetBuildingByFoundationId(landById.BuildingFoundationId, out building2))
			{
				list.Add(building2);
			}
		}
		return list;
	}

	public bool HasDiceEffect()
	{
		return GetConfigureItem().TriggerPoint.Count > 0;
	}

	private void CheckDicePointEffect(List<int> values)
	{
		if (CheckDicePoint(values))
		{
			AddExecuteQueue();
		}
	}

	protected virtual bool CheckDicePoint(List<int> values)
	{
		SinglePlayerCardConfigureItem configureItem = GetConfigureItem();
		if (configureItem != null)
		{
			if (!configureItem.TriggerPoint.Contains(-1))
			{
				return configureItem.TriggerPoint.ContainsAny(values);
			}
			return true;
		}
		return false;
	}

	protected bool CheckDicePoint(int values)
	{
		SinglePlayerCardConfigureItem configureItem = GetConfigureItem();
		if (configureItem != null)
		{
			if (!configureItem.TriggerPoint.Contains(-1))
			{
				return configureItem.TriggerPoint.Contains(values);
			}
			return true;
		}
		return false;
	}

	private void OnRoundChange(int round)
	{
		ThrowDiceTriggerCount = 0;
		PassTriggerCount = 0;
		StayTriggerCount = 0;
	}

	public int GetGoldMultiValue(int index, ParameterType operateType, int effectMultiplier = 1)
	{
		Int32Encryptor value = new Int32Encryptor();
		switch (operateType)
		{
		case ParameterType.ThrowDice:
			_operateBonus.TryGetValue(OperateType.ThrowDiceGold, out value);
			break;
		case ParameterType.Pass:
			_operateBonus.TryGetValue(OperateType.PassGold, out value);
			break;
		case ParameterType.Stay:
			_operateBonus.TryGetValue(OperateType.StayGold, out value);
			break;
		default:
			throw new ArgumentOutOfRangeException("operateType", operateType, null);
		}
		return (GetEffectParam(operateType)?.GetSafeByIndex(index) ?? 0) * effectMultiplier + value.DecryptGet();
	}

	public int HasAttributeValue(OperateType operateType)
	{
		return _operateBonus.GetValueOrDefault(operateType, defalutEncryptor).DecryptGet();
	}

	public RepeatedField<int> GetEffectParam(ParameterType operateType)
	{
		SinglePlayerCardConfigureItem configureItem = GetConfigureItem();
		if (configureItem == null)
		{
			return null;
		}
		return operateType switch
		{
			ParameterType.ThrowDice => configureItem.TriggerParam, 
			ParameterType.Pass => configureItem.WalkParam, 
			ParameterType.Stay => configureItem.StopParam, 
			ParameterType.Other => configureItem.OtherParam, 
			_ => null, 
		};
	}

	public int GetConfigParam(ParameterType operateType, int index)
	{
		return GetEffectParam(operateType)?.GetSafeByIndex(index) ?? 0;
	}

	public void ChangeOperateBonus(OperateType operateType, int changeValue)
	{
		if (_operateBonus.ContainsKey(operateType))
		{
			Int32Encryptor int32Encryptor = _operateBonus[operateType];
			int32Encryptor.EncryptSet(int32Encryptor.DecryptGet() + changeValue);
			Game.GetModel<GlobalSignal>().BuildingOperateBonus.Dispatch(Id, (int)operateType, changeValue);
		}
		else
		{
			Debug.LogError($"修改 {operateType} 的加成是无效的");
		}
	}

	public int GetDiceTriggerCount()
	{
		Array.Fill(diceSubTriggerCountArray, 0);
		RepeatedField<int> triggerPoint = GetConfigureItem().TriggerPoint;
		if (triggerPoint == null || triggerPoint.Count <= 0)
		{
			return 0;
		}
		if (triggerPoint.GetSafeByIndex(0) == -1)
		{
			return 1;
		}
		int num = 0;
		foreach (int dicePoint in Game.GetSystem<BoardManager>().gameManager.DicePoints)
		{
			int num2 = triggerPoint.IndexOf(dicePoint);
			if (num2 >= 0)
			{
				diceSubTriggerCountArray[num2]++;
				num++;
			}
		}
		return num;
	}

	protected int GetDiceSubTriggerCount(int index)
	{
		return diceSubTriggerCountArray.GetSafeByIndex(index);
	}

	public SingleBuilding GetUploadData()
	{
		SingleBuilding singleBuilding = new SingleBuilding
		{
			BuildingId = Id,
			BuildingFoundationId = BuildingFoundationId,
			CardUID = Card.UID,
			ThrowDiceTriggerCount = ThrowDiceTriggerCount,
			PassTriggerCount = PassTriggerCount,
			StayTriggerCount = StayTriggerCount
		};
		foreach (var (key, int32Encryptor2) in _operateBonus)
		{
			singleBuilding.OperateBonus.Add((int)key, int32Encryptor2.DecryptGet());
		}
		return singleBuilding;
	}
}
