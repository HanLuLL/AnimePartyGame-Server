using System.Collections;
using System.Collections.Generic;
using SinglePlayer.Tools;

namespace SinglePlayer.GamePlay.BuffSystem;

public sealed class BuffContainer : IEnumerable<BuffBase>, IEnumerable
{
	private readonly Dictionary<uint, BuffBase> _buffDict = new Dictionary<uint, BuffBase>();

	private readonly List<BuffBase> _addList = new List<BuffBase>();

	private readonly List<BuffBase> _removeList = new List<BuffBase>();

	private bool _lock;

	private bool _needUpdate;

	public BuffContainer()
	{
		Game.GetSystem<MonoBehaviourManager>().Update0.AddListener(Tick);
	}

	private void Tick()
	{
		if (!_needUpdate)
		{
			return;
		}
		_lock = true;
		if (_removeList.Count > 0)
		{
			for (int i = 0; i < _removeList.Count; i++)
			{
				BuffBase buffBase = _removeList[i];
				_buffDict.Remove(buffBase.Id);
			}
			_removeList.Clear();
		}
		if (_addList.Count > 0)
		{
			for (int j = 0; j < _addList.Count; j++)
			{
				BuffBase buffBase2 = _addList[j];
				_buffDict.Add(buffBase2.Id, buffBase2);
			}
			_addList.Clear();
		}
		_lock = false;
		_needUpdate = false;
	}

	public void Clear()
	{
		Game.GetSystem<MonoBehaviourManager>().Update0.RemoveListener(Tick);
		_buffDict.Clear();
		_removeList.Clear();
		_addList.Clear();
		_lock = false;
		_needUpdate = false;
	}

	private void LockContainer()
	{
		_lock = true;
	}

	private void ReleaseContainer()
	{
		_lock = false;
		if (_removeList.Count > 0)
		{
			for (int i = 0; i < _removeList.Count; i++)
			{
				BuffBase buffBase = _removeList[i];
				_buffDict.Remove(buffBase.Id);
			}
			_removeList.Clear();
		}
		if (_addList.Count > 0)
		{
			for (int j = 0; j < _addList.Count; j++)
			{
				BuffBase buffBase2 = _addList[j];
				_buffDict.Add(buffBase2.Id, buffBase2);
			}
			_addList.Clear();
		}
		_removeList.Clear();
		_addList.Clear();
	}

	public void Add(BuffBase buff)
	{
		if (buff != null)
		{
			if (_lock)
			{
				_addList.Add(buff);
				return;
			}
			_buffDict.Add(buff.Id, buff);
			_needUpdate = true;
		}
	}

	public void Remove(BuffBase buff)
	{
		if (buff != null)
		{
			if (_lock)
			{
				_removeList.Add(buff);
				return;
			}
			_buffDict.Remove(buff.Id);
			_needUpdate = true;
		}
	}

	public bool TryGetValue(uint id, out BuffBase buff)
	{
		return _buffDict.TryGetValue(id, out buff);
	}

	public bool Contains(uint id)
	{
		return _buffDict.ContainsKey(id);
	}

	public IEnumerator<BuffBase> GetEnumerator()
	{
		LockContainer();
		foreach (KeyValuePair<uint, BuffBase> item in _buffDict)
		{
			yield return item.Value;
		}
		ReleaseContainer();
	}

	public IEnumerable<BuffBase> GetUnLockedEnumerator()
	{
		foreach (KeyValuePair<uint, BuffBase> item in _buffDict)
		{
			yield return item.Value;
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
