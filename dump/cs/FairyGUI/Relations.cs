using System.Collections.Generic;
using FairyGUI.Utils;

namespace FairyGUI;

public class Relations
{
	private GObject _owner;

	private List<RelationItem> _items;

	public GObject handling;

	public bool isEmpty => _items.Count == 0;

	public Relations(GObject owner)
	{
		_owner = owner;
		_items = new List<RelationItem>();
	}

	public void Add(GObject target, RelationType relationType)
	{
		Add(target, relationType, usePercent: false);
	}

	public void Add(GObject target, RelationType relationType, bool usePercent)
	{
		int count = _items.Count;
		for (int i = 0; i < count; i++)
		{
			RelationItem relationItem = _items[i];
			if (relationItem.target == target)
			{
				relationItem.Add(relationType, usePercent);
				return;
			}
		}
		RelationItem relationItem2 = new RelationItem(_owner);
		relationItem2.target = target;
		relationItem2.Add(relationType, usePercent);
		_items.Add(relationItem2);
	}

	public void Remove(GObject target, RelationType relationType)
	{
		int num = _items.Count;
		int num2 = 0;
		while (num2 < num)
		{
			RelationItem relationItem = _items[num2];
			if (relationItem.target == target)
			{
				relationItem.Remove(relationType);
				if (relationItem.isEmpty)
				{
					relationItem.Dispose();
					_items.RemoveAt(num2);
					num--;
					continue;
				}
				num2++;
			}
			num2++;
		}
	}

	public bool Contains(GObject target)
	{
		int count = _items.Count;
		for (int i = 0; i < count; i++)
		{
			if (_items[i].target == target)
			{
				return true;
			}
		}
		return false;
	}

	public void ClearFor(GObject target)
	{
		int num = _items.Count;
		int num2 = 0;
		while (num2 < num)
		{
			RelationItem relationItem = _items[num2];
			if (relationItem.target == target)
			{
				relationItem.Dispose();
				_items.RemoveAt(num2);
				num--;
			}
			else
			{
				num2++;
			}
		}
	}

	public void ClearAll()
	{
		int count = _items.Count;
		for (int i = 0; i < count; i++)
		{
			_items[i].Dispose();
		}
		_items.Clear();
	}

	public void CopyFrom(Relations source)
	{
		ClearAll();
		foreach (RelationItem item in source._items)
		{
			RelationItem relationItem = new RelationItem(_owner);
			relationItem.CopyFrom(item);
			_items.Add(relationItem);
		}
	}

	public void Dispose()
	{
		ClearAll();
		handling = null;
	}

	public void OnOwnerSizeChanged(float dWidth, float dHeight, bool applyPivot)
	{
		int count = _items.Count;
		if (count != 0)
		{
			for (int i = 0; i < count; i++)
			{
				_items[i].ApplyOnSelfSizeChanged(dWidth, dHeight, applyPivot);
			}
		}
	}

	public void Setup(ByteBuffer buffer, bool parentToChild)
	{
		int num = buffer.ReadByte();
		for (int i = 0; i < num; i++)
		{
			int num2 = buffer.ReadShort();
			GObject target = ((num2 == -1) ? _owner.parent : ((!parentToChild) ? _owner.parent.GetChildAt(num2) : ((GComponent)_owner).GetChildAt(num2)));
			RelationItem relationItem = new RelationItem(_owner);
			relationItem.target = target;
			_items.Add(relationItem);
			int num3 = buffer.ReadByte();
			for (int j = 0; j < num3; j++)
			{
				RelationType relationType = (RelationType)buffer.ReadByte();
				bool usePercent = buffer.ReadBool();
				relationItem.InternalAdd(relationType, usePercent);
			}
		}
	}
}
