using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Camera;
using Core.Net;
using Core.Scene;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class SummonLogic
{
	private readonly Dictionary<long, List<Summon>> _bombSummonDict = new Dictionary<long, List<Summon>>();

	public readonly Dictionary<long, LandBuffData> _landSummonDict = new Dictionary<long, LandBuffData>();

	private Dictionary<int, List<LandBuffData>> _clusterSummonDict = new Dictionary<int, List<LandBuffData>>();

	private List<LandBuffData> _summonList = new List<LandBuffData>();

	public bool TryGetLandBuff(long buffUid, out LandBuffData buffData)
	{
		return _landSummonDict.TryGetValue(buffUid, out buffData);
	}

	public List<LandBuffData> GetSummonById(int summonId)
	{
		return _landSummonDict.Values.Where((LandBuffData x) => x.buffData.Source.S == buff_source.Types.source.Summon && x.buffData.Source.Id == summonId).ToList();
	}

	public async UniTask UpdateSummonData(Room Room, bool Show)
	{
		if (_landSummonDict.Count > 0)
		{
			foreach (KeyValuePair<long, LandBuffData> item in _landSummonDict)
			{
				item.Value.CloseSummon();
			}
		}
		_landSummonDict.Clear();
		foreach (UnitLand value2 in SimpleSingletonProvider<LandManager>.inst.NodeDict.Values)
		{
			if (Room.LandBuffs.TryGetValue(value2.Id, out var value))
			{
				UpdateLandBuffs(value2, value);
			}
		}
		await CreateLandBuffSummon(Show);
	}

	private void UpdateLandBuffs(UnitLand _Land, BuffArray _buffs)
	{
		List<LandBuffData> landBuffsByLandId = GetLandBuffsByLandId(_Land.Id);
		List<LandBuffData> list = new List<LandBuffData>();
		foreach (LandBuffData item in landBuffsByLandId)
		{
			if (!_buffs.Buffs.ContainsKey(item.UniqueId))
			{
				list.Add(item);
			}
		}
		if (list.Count > 0)
		{
			foreach (LandBuffData item2 in list)
			{
				if (_landSummonDict.TryGetValue(item2.UniqueId, out var value))
				{
					value.CloseSummon();
					_landSummonDict.Remove(item2.UniqueId);
				}
			}
		}
		foreach (KeyValuePair<long, Buff> buff in _buffs.Buffs)
		{
			_landSummonDict.TryAdd(buff.Key, new LandBuffData(buff.Key, buff.Value, _Land.Id));
		}
	}

	private List<LandBuffData> GetLandBuffsByLandId(int landId)
	{
		List<LandBuffData> list = new List<LandBuffData>();
		foreach (KeyValuePair<long, LandBuffData> item in _landSummonDict)
		{
			if (item.Value.LandId == landId)
			{
				list.Add(item.Value);
			}
		}
		return list;
	}

	private async UniTask CreateLandBuffSummon(bool show)
	{
		if (!(await CreateThundercloudBuffs(show)))
		{
			return;
		}
		_clusterSummonDict.Clear();
		_summonList.Clear();
		foreach (KeyValuePair<long, LandBuffData> item in _landSummonDict)
		{
			if (item.Value.FinishSummon || item.Value.buffData.Source.S != buff_source.Types.source.Summon)
			{
				continue;
			}
			int id = item.Value.buffData.Source.Id;
			SummonInfoConfigure summonDataConfigure = id.GetSummonDataConfigure();
			if (summonDataConfigure == null)
			{
				continue;
			}
			if (summonDataConfigure.IsPerformGroup)
			{
				if (!_clusterSummonDict.ContainsKey(id))
				{
					_clusterSummonDict.Add(id, new List<LandBuffData>());
				}
				_clusterSummonDict[id].Add(item.Value);
			}
			else
			{
				_summonList.Add(item.Value);
			}
		}
		foreach (int key in _clusterSummonDict.Keys)
		{
			SummonInfoConfigure summonDataConfigure2 = key.GetSummonDataConfigure();
			if (summonDataConfigure2 != null)
			{
				SetCameraPos(_clusterSummonDict[key][0].LandId, show && summonDataConfigure2.IsFollowSummoning);
				UniTask[] tasks = _clusterSummonDict[key].Select((LandBuffData b) => b.CreateSummon()).ToArray();
				if (await SimpleSingletonProvider<DelaySignalManager>.inst.WhenAll(tasks))
				{
					return;
				}
			}
		}
		foreach (LandBuffData summon in _summonList)
		{
			SummonInfoConfigure summonDataConfigure3 = summon.buffData.Source.Id.GetSummonDataConfigure();
			if (summonDataConfigure3 != null)
			{
				SetCameraPos(summon.LandId, show && summonDataConfigure3.IsFollowSummoning);
				await summon.CreateSummon();
			}
		}
	}

	private async UniTask<bool> CreateThundercloudBuffs(bool Show)
	{
		List<LandBuffData> _thunderCloudBuffs = new List<LandBuffData>(_landSummonDict.Count);
		foreach (KeyValuePair<long, LandBuffData> item in _landSummonDict)
		{
			if (!item.Value.FinishSummon && item.Value.buffData.Source.S == buff_source.Types.source.Summon)
			{
				int id = item.Value.buffData.Source.Id;
				if (id == 2003 || id == 2401)
				{
					_thunderCloudBuffs.Add(item.Value);
				}
			}
		}
		if (_thunderCloudBuffs.Count > 0)
		{
			int num = StaticConfigure.Event.InfoDict[30016].Params[1];
			if (num * 2 != _thunderCloudBuffs.Count)
			{
				Debug.LogError($"创建雷云召唤物，当前拿到召唤物id为2003|2401的buff数量为{_thunderCloudBuffs.Count}, 显然与配置数量{num}的两倍不符合，存在问题");
				return false;
			}
			_thunderCloudBuffs.Sort((LandBuffData x, LandBuffData y) => x.IsNeighbor(y.LandId));
			int index = num / 2;
			int pos_2 = num * 3 / 2;
			UniTask[] showTask = new UniTask[num];
			SetCameraPos(_thunderCloudBuffs[index].LandId, Show);
			for (int num2 = 0; num2 < num; num2++)
			{
				showTask[num2] = _thunderCloudBuffs[num2].CreateSummon();
			}
			if (await SimpleSingletonProvider<DelaySignalManager>.inst.WhenAll(showTask))
			{
				return false;
			}
			SetCameraPos(_thunderCloudBuffs[pos_2].LandId, Show);
			for (int num3 = 0; num3 < num; num3++)
			{
				showTask[num3] = _thunderCloudBuffs[num3 + num].CreateSummon();
			}
			if (await SimpleSingletonProvider<DelaySignalManager>.inst.WhenAll(showTask))
			{
				return false;
			}
		}
		return true;
	}

	private void SetCameraPos(int _landId, bool Show)
	{
		if (Show)
		{
			UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(_landId);
			SimpleSingletonProvider<CameraManager>.inst.EnableShowCamera(landById.transform.position);
		}
	}

	public async UniTask UpdateBombs(long playerId, RepeatedField<Bomb> attrBombs)
	{
		List<Summon> playerBombs = GetBombs(playerId);
		if (playerBombs != null && playerBombs.Count >= attrBombs.Count)
		{
			int num = playerBombs.Count - attrBombs.Count;
			RemoveBombs(playerId, num);
		}
		if (playerBombs != null && playerBombs.Count >= attrBombs.Count)
		{
			return;
		}
		int i;
		if (playerBombs == null)
		{
			for (i = 0; i < attrBombs.Count; i++)
			{
				await AddBomb(playerId, i);
			}
			return;
		}
		i = attrBombs.Count - playerBombs.Count;
		for (int j = 0; j < i; j++)
		{
			await AddBomb(playerId, j + playerBombs.Count);
		}
	}

	private async UniTask AddBomb(long playerId, int index)
	{
		if (!_bombSummonDict.TryGetValue(playerId, out var value))
		{
			value = new List<Summon>();
			_bombSummonDict.Add(playerId, value);
		}
		Summon summon = new Summon(2006);
		value.Add(summon);
		await summon.SetSummonObj(index, playerId);
	}

	private List<Summon> GetBombs(long playerId)
	{
		return _bombSummonDict.GetValueOrDefault(playerId);
	}

	private void RemoveBombs(long playerId, int num)
	{
		if (_bombSummonDict.TryGetValue(playerId, out var value))
		{
			for (int i = 0; i < num; i++)
			{
				List<Summon> list = value;
				Summon summon = list[list.Count - 1];
				summon.CloseSummon();
				value.Remove(summon);
			}
		}
	}

	public async UniTask ActiveBomb(long playerId)
	{
		if (_bombSummonDict.TryGetValue(playerId, out var value))
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(playerId, value[0].trapConfig.PerformDebuff, "手捧雷爆炸");
		}
	}

	public void HideBomb(long playerId)
	{
		if (_bombSummonDict.TryGetValue(playerId, out var value))
		{
			value[0].CloseSummon();
		}
	}

	public void Dispose()
	{
		_bombSummonDict.Clear();
		_landSummonDict.Clear();
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.LandBuffsS2C.OnLandBuffsS2CServerCallBackAsync = OnLandBuffsS2CServerCallBack;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.LandBuffsS2C.OnLandBuffsS2CServerCallBackAsync = null;
	}

	private async UniTask OnLandBuffsS2CServerCallBack(LandBuffsS2C model, int errId, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle || SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType != RoomStateType.RUNNING || errId != 0)
		{
			return;
		}
		Dictionary<int, UnitLand> nodeDict = SimpleSingletonProvider<LandManager>.inst.NodeDict;
		foreach (LandBuffsS2C.Types.LandBuffsWrap buff in model.Buffs)
		{
			if (nodeDict.TryGetValue(buff.NodeId, out var value))
			{
				UpdateLandBuffs(value, buff.BuffArr);
			}
		}
		await CreateLandBuffSummon(show: true);
	}
}
