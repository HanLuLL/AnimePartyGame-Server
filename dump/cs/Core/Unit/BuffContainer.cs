using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.model;

namespace Core.Unit;

public class BuffContainer
{
	private Character owner;

	public Dictionary<long, Buff> _buffDict;

	public BuffContainer(MapField<long, Buff> Buffs)
	{
		_buffDict = new Dictionary<long, Buff>();
		foreach (KeyValuePair<long, Buff> Buff in Buffs)
		{
			_buffDict.TryAdd(Buff.Key, Buff.Value);
		}
	}

	public async void InitBuffEffect(Character character)
	{
		owner = character;
		foreach (KeyValuePair<long, Buff> item in _buffDict)
		{
			await SimpleSingletonProvider<BuffEffectManager>.inst.SyncBuffEffect(item.Value, owner);
		}
	}

	public async UniTask UpdateBuff(MapField<long, Buff> Buffs)
	{
		foreach (KeyValuePair<long, Buff> Buff in Buffs)
		{
			if (!_buffDict.ContainsKey(Buff.Key))
			{
				await SimpleSingletonProvider<BuffEffectManager>.inst.SyncBuffEffect(Buff.Value, owner);
			}
		}
		foreach (KeyValuePair<long, Buff> item in _buffDict)
		{
			if (!Buffs.ContainsKey(item.Value.UniqueId))
			{
				SimpleSingletonProvider<BuffEffectManager>.inst.DestroyBuffEffect(item.Value, owner);
			}
		}
		_buffDict.Clear();
		foreach (KeyValuePair<long, Buff> Buff2 in Buffs)
		{
			_buffDict.TryAdd(Buff2.Key, Buff2.Value);
		}
	}

	public async UniTask InsertBuff(Buff Buff)
	{
		if (!_buffDict.TryAdd(Buff.UniqueId, Buff))
		{
			_buffDict[Buff.UniqueId] = Buff;
		}
		else
		{
			await SimpleSingletonProvider<BuffEffectManager>.inst.SyncBuffEffect(Buff, owner);
		}
	}

	public void DeleteBuff(Buff Buff)
	{
		_buffDict.Remove(Buff.UniqueId);
		SimpleSingletonProvider<BuffEffectManager>.inst.DestroyBuffEffect(Buff, owner);
	}

	public async UniTask ChangeBuff(Buff Buff)
	{
		if (_buffDict.ContainsKey(Buff.UniqueId))
		{
			_buffDict[Buff.UniqueId] = Buff;
			await SimpleSingletonProvider<BuffEffectManager>.inst.UpdateBuffEffect(Buff, owner);
		}
	}

	public Buff GetBuffById(long unique_id)
	{
		if (!_buffDict.TryGetValue(unique_id, out var value))
		{
			return null;
		}
		return value;
	}

	public (List<Buff>, List<PropertyData<int>>) GetShowBuffs(BattlePlayerData playerData, bool isRegister = false)
	{
		List<Buff> list = new List<Buff>();
		foreach (KeyValuePair<long, Buff> item in _buffDict)
		{
			BuffInfoConfigure buffConfigure = item.Value.BuffId.GetBuffConfigure();
			if (buffConfigure != null && buffConfigure.IsShow && (item.Value.Progress != 0 || buffConfigure.IsShowIconProgressZero))
			{
				list.Add(item.Value);
			}
		}
		List<PropertyData<int>> list2 = new List<PropertyData<int>>();
		if (playerData.Property != null)
		{
			foreach (PropertyData<int> propertyBuffData in playerData.Property.propertyBuffDataList)
			{
				if (isRegister || propertyBuffData.property.Value > 0)
				{
					list2.Add(propertyBuffData);
				}
			}
		}
		return (list, list2);
	}

	public bool Contain(int configBuffId)
	{
		foreach (KeyValuePair<long, Buff> item in _buffDict)
		{
			if (item.Value.BuffId == configBuffId)
			{
				return true;
			}
		}
		return false;
	}

	public bool Contain(RepeatedField<int> configBuffIds)
	{
		foreach (KeyValuePair<long, Buff> item in _buffDict)
		{
			if (configBuffIds.Contains(item.Value.BuffId))
			{
				return true;
			}
		}
		return false;
	}

	public Buff GetBuff(int buffConfigId)
	{
		foreach (var (_, buff2) in _buffDict)
		{
			if (buff2.BuffId == buffConfigId)
			{
				return buff2;
			}
		}
		return null;
	}
}
