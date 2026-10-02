using System.Collections.Generic;

namespace Tools;

public class ConcurrentHashSet<T>
{
	private readonly object syncLock = new object();

	private HashSet<T> _hashSet = new HashSet<T>();

	public int Count
	{
		get
		{
			lock (syncLock)
			{
				return _hashSet.Count;
			}
		}
	}

	public bool Add(T item)
	{
		lock (syncLock)
		{
			return _hashSet.Add(item);
		}
	}

	public void Clear()
	{
		lock (syncLock)
		{
			_hashSet.Clear();
		}
	}

	public bool Contains(T item)
	{
		lock (syncLock)
		{
			return _hashSet.Contains(item);
		}
	}

	public bool Remove(T item)
	{
		lock (syncLock)
		{
			return _hashSet.Remove(item);
		}
	}
}
