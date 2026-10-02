using System.Collections.Generic;

namespace GameLogic;

public class BagContainer
{
	private readonly Dictionary<int, BagItem> _bagMap;

	private readonly Dictionary<ItemType, List<int>> _itemTypeList;

	public int capacity => _bagMap.Count;

	public BagContainer()
	{
		_bagMap = new Dictionary<int, BagItem>();
		_itemTypeList = new Dictionary<ItemType, List<int>>(EnumComparerRef.itemTypeComparer);
	}

	public void Add(BagItem item)
	{
		_bagMap.Add(item.config.Id, item);
		if (!_itemTypeList.ContainsKey(item.config.ItemType))
		{
			_itemTypeList[item.config.ItemType] = new List<int>();
		}
		_itemTypeList[item.config.ItemType].Add(item.config.Id);
	}

	public void Remove(int id)
	{
		if (_bagMap.TryGetValue(id, out var value))
		{
			_itemTypeList[value.config.ItemType].Remove(value.config.Id);
		}
		_bagMap.Remove(id);
	}

	public void UpdateItemCount(int id, int newCount)
	{
		if (_bagMap.TryGetValue(id, out var value))
		{
			value.count.Value = newCount;
		}
	}

	public bool ExistItem(int id)
	{
		return _bagMap.ContainsKey(id);
	}

	public BagItem GetItem(int id)
	{
		if (!_bagMap.TryGetValue(id, out var value))
		{
			return null;
		}
		return value;
	}

	public T GetItem<T>(int id) where T : BagItem
	{
		if (!_bagMap.TryGetValue(id, out var value))
		{
			return null;
		}
		return value as T;
	}

	public BagItem TryGetAItemByConfigId(int itemID)
	{
		foreach (KeyValuePair<int, BagItem> item in _bagMap)
		{
			if (item.Value.config.Id == itemID)
			{
				return item.Value;
			}
		}
		return null;
	}

	public T TryGetAItemByConfigId<T>(int itemID) where T : BagItem
	{
		foreach (KeyValuePair<int, BagItem> item in _bagMap)
		{
			if (item.Value.config.Id == itemID)
			{
				return item.Value as T;
			}
		}
		return null;
	}

	public List<int> GetIDs(ItemType type)
	{
		if (!_itemTypeList.TryGetValue(type, out var value))
		{
			return null;
		}
		return value;
	}

	public int GetItemCount(int itemID)
	{
		int num = 0;
		foreach (KeyValuePair<int, BagItem> item in _bagMap)
		{
			if (item.Value.config.Id == itemID)
			{
				num += item.Value.count.Value;
			}
		}
		return num;
	}

	public void Clear()
	{
		_bagMap.Clear();
		_itemTypeList.Clear();
	}
}
