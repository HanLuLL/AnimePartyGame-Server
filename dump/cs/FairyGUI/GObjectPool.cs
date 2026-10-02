using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

public class GObjectPool
{
	public delegate void InitCallbackDelegate(GObject obj);

	public InitCallbackDelegate initCallback;

	private Dictionary<string, Queue<GObject>> _pool;

	private Transform _manager;

	public int count => _pool.Count;

	public GObjectPool(Transform manager)
	{
		_manager = manager;
		_pool = new Dictionary<string, Queue<GObject>>();
	}

	public void Clear()
	{
		foreach (KeyValuePair<string, Queue<GObject>> item in _pool)
		{
			foreach (GObject item2 in item.Value)
			{
				item2.Dispose();
			}
		}
		_pool.Clear();
	}

	public GObject GetObject(string url)
	{
		url = UIPackage.NormalizeURL(url);
		if (url == null)
		{
			return null;
		}
		if (_pool.TryGetValue(url, out var value) && value.Count > 0)
		{
			return value.Dequeue();
		}
		GObject gObject = UIPackage.CreateObjectFromURL(url);
		if (gObject != null && initCallback != null)
		{
			initCallback(gObject);
		}
		return gObject;
	}

	public void ReturnObject(GObject obj)
	{
		if (!obj.displayObject.isDisposed)
		{
			string resourceURL = obj.resourceURL;
			if (!_pool.TryGetValue(resourceURL, out var value))
			{
				value = new Queue<GObject>();
				_pool.Add(resourceURL, value);
			}
			if (_manager != null)
			{
				obj.displayObject.cachedTransform.SetParent(_manager, worldPositionStays: false);
			}
			value.Enqueue(obj);
		}
	}
}
