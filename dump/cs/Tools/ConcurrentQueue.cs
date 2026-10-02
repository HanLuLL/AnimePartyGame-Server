using System.Collections.Generic;

namespace Tools;

public class ConcurrentQueue<T>
{
	private readonly object syncLock = new object();

	private Queue<T> queue;

	public int Count
	{
		get
		{
			lock (syncLock)
			{
				return queue.Count;
			}
		}
	}

	public ConcurrentQueue()
	{
		queue = new Queue<T>();
	}

	public ConcurrentQueue(int capacity)
	{
		queue = new Queue<T>(capacity);
	}

	public T Peek()
	{
		lock (syncLock)
		{
			return queue.Peek();
		}
	}

	public void Enqueue(T obj)
	{
		lock (syncLock)
		{
			queue.Enqueue(obj);
		}
	}

	public T Dequeue()
	{
		lock (syncLock)
		{
			return queue.Dequeue();
		}
	}

	public void Clear()
	{
		lock (syncLock)
		{
			queue.Clear();
		}
	}

	public T[] CopyToArray()
	{
		lock (syncLock)
		{
			if (queue.Count == 0)
			{
				return new T[0];
			}
			T[] array = new T[queue.Count];
			queue.CopyTo(array, 0);
			return array;
		}
	}

	public static ConcurrentQueue<T> InitFromArray(IEnumerable<T> initValues)
	{
		ConcurrentQueue<T> concurrentQueue = new ConcurrentQueue<T>();
		if (initValues == null)
		{
			return concurrentQueue;
		}
		foreach (T initValue in initValues)
		{
			concurrentQueue.Enqueue(initValue);
		}
		return concurrentQueue;
	}
}
