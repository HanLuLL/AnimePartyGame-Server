using System.Collections.Generic;
using SinglePlayer.GamePlay.Character;
using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay.BuffSystem;

public class BuffComponent
{
	private CharacterLogic _owner;

	private BuffContainer _container;

	public Signal<uint> onAddBuff;

	public Signal<uint> onRemoveBuff;

	public BuffComponent(CharacterLogic owner)
	{
		_owner = owner;
		_container = new BuffContainer();
		onAddBuff = new Signal<uint>();
		onRemoveBuff = new Signal<uint>();
	}

	public void Tick()
	{
		foreach (BuffBase item in _container)
		{
			item.Tick();
		}
	}

	public void Dispose()
	{
		_owner = null;
		_container.Clear();
		onAddBuff.RemoveAllListeners();
		onRemoveBuff.RemoveAllListeners();
	}

	public void Add(uint configureId, CharacterLogic caster, uint initialLayer = 1u)
	{
		BuffConfigure buffConfigure = null;
		buffConfigure = new BuffConfigure
		{
			Id = 1000u,
			Delay = 0u,
			Duration = 10u,
			Interval = 2u,
			TriggerType = BuffTriggerType.RoundStart,
			AddType = BuffAddType.Override,
			MaxLayer = 1u,
			Effects = new Dictionary<int, object>
			{
				[100] = 10,
				[101] = 5
			}
		};
		BuffBase buffBase = BuffFactory.Create(configureId);
		BuffBase buff;
		if (buffBase == null)
		{
			Debug.LogError($"Buff 添加失败：无法创建实例：{configureId}");
		}
		else if (_container.TryGetValue(configureId, out buff))
		{
			uint num = buff.Layer + 1;
			if (buff.Layer >= buffConfigure.MaxLayer)
			{
				num = buffConfigure.MaxLayer;
			}
			uint delta = num - buff.Layer;
			buff.RefreshLayer(num, delta);
		}
		else
		{
			buffBase.Initialize(buffConfigure, caster, _owner, initialLayer);
			_container.Add(buffBase);
			onAddBuff.Dispatch(configureId);
		}
	}

	public void Remove(uint configureId)
	{
		if (!_container.TryGetValue(configureId, out var buff))
		{
			Debug.LogError($"Buff 移除失败，不存在Buff：{configureId}");
		}
		else
		{
			Remove(buff);
		}
	}

	public void Remove(BuffBase buff)
	{
		if (buff == null)
		{
			Debug.LogError("Buff 移除失败，Buff is null");
			return;
		}
		buff.OnRemove();
		_container.Remove(buff);
		BuffFactory.Release(buff);
	}

	public bool Contains(uint configureId)
	{
		return _container.Contains(configureId);
	}
}
