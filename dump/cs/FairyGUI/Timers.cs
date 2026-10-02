using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

public class Timers
{
	public static int repeat;

	public static float time;

	public static bool catchCallbackExceptions;

	private Dictionary<TimerCallback, Anymous_T> _items;

	private Dictionary<TimerCallback, Anymous_T> _toAdd;

	private List<Anymous_T> _toRemove;

	private List<Anymous_T> _pool;

	private TimersEngine _engine;

	private GameObject gameObject;

	private static Timers _inst;

	public static Timers inst
	{
		get
		{
			if (_inst == null)
			{
				_inst = new Timers();
			}
			return _inst;
		}
	}

	public Timers()
	{
		_inst = this;
		gameObject = new GameObject("[FairyGUI.Timers]");
		gameObject.hideFlags = HideFlags.HideInHierarchy;
		gameObject.SetActive(value: true);
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		_engine = gameObject.AddComponent<TimersEngine>();
		_items = new Dictionary<TimerCallback, Anymous_T>();
		_toAdd = new Dictionary<TimerCallback, Anymous_T>();
		_toRemove = new List<Anymous_T>();
		_pool = new List<Anymous_T>(100);
	}

	public void Add(float interval, int repeat, TimerCallback callback)
	{
		Add(interval, repeat, callback, null);
	}

	public void Add(float interval, int repeat, TimerCallback callback, object callbackParam)
	{
		if (callback == null)
		{
			Debug.LogWarning("timer callback is null, " + interval + "," + repeat);
			return;
		}
		if (_items.TryGetValue(callback, out var value))
		{
			value.set(interval, repeat, callback, callbackParam);
			value.elapsed = 0f;
			value.deleted = false;
			return;
		}
		if (_toAdd.TryGetValue(callback, out value))
		{
			value.set(interval, repeat, callback, callbackParam);
			return;
		}
		value = GetFromPool();
		value.interval = interval;
		value.repeat = repeat;
		value.callback = callback;
		value.param = callbackParam;
		_toAdd[callback] = value;
	}

	public void CallLater(TimerCallback callback)
	{
		Add(0.001f, 1, callback);
	}

	public void CallLater(TimerCallback callback, object callbackParam)
	{
		Add(0.001f, 1, callback, callbackParam);
	}

	public void AddUpdate(TimerCallback callback)
	{
		Add(0.001f, 0, callback);
	}

	public void AddUpdate(TimerCallback callback, object callbackParam)
	{
		Add(0.001f, 0, callback, callbackParam);
	}

	public void StartCoroutine(IEnumerator routine)
	{
		_engine.StartCoroutine(routine);
	}

	public bool Exists(TimerCallback callback)
	{
		if (_toAdd.ContainsKey(callback))
		{
			return true;
		}
		if (_items.TryGetValue(callback, out var value))
		{
			return !value.deleted;
		}
		return false;
	}

	public void Remove(TimerCallback callback)
	{
		if (_toAdd.TryGetValue(callback, out var value))
		{
			_toAdd.Remove(callback);
			ReturnToPool(value);
		}
		if (_items.TryGetValue(callback, out value))
		{
			value.deleted = true;
		}
	}

	private Anymous_T GetFromPool()
	{
		int count = _pool.Count;
		Anymous_T anymous_T;
		if (count > 0)
		{
			anymous_T = _pool[count - 1];
			_pool.RemoveAt(count - 1);
			anymous_T.deleted = false;
			anymous_T.elapsed = 0f;
		}
		else
		{
			anymous_T = new Anymous_T();
		}
		return anymous_T;
	}

	private void ReturnToPool(Anymous_T t)
	{
		t.callback = null;
		_pool.Add(t);
	}

	public void Update()
	{
		float unscaledDeltaTime = Time.unscaledDeltaTime;
		if (_items.Count > 0)
		{
			Dictionary<TimerCallback, Anymous_T>.Enumerator enumerator = _items.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Anymous_T value = enumerator.Current.Value;
				if (value.deleted)
				{
					_toRemove.Add(value);
					continue;
				}
				value.elapsed += unscaledDeltaTime;
				if (value.elapsed < value.interval)
				{
					continue;
				}
				value.elapsed -= value.interval;
				if (value.elapsed < 0f || value.elapsed > 0.03f)
				{
					value.elapsed = 0f;
				}
				if (value.repeat > 0)
				{
					value.repeat--;
					if (value.repeat == 0)
					{
						value.deleted = true;
						_toRemove.Add(value);
					}
				}
				repeat = value.repeat;
				if (value.callback == null)
				{
					continue;
				}
				if (catchCallbackExceptions)
				{
					try
					{
						value.callback(value.param);
					}
					catch (Exception ex)
					{
						value.deleted = true;
						Debug.LogWarning("FairyGUI: timer(internal=" + value.interval + ", repeat=" + value.repeat + ") callback error > " + ex.Message);
					}
				}
				else
				{
					value.callback(value.param);
				}
			}
			enumerator.Dispose();
		}
		int count = _toRemove.Count;
		if (count > 0)
		{
			for (int i = 0; i < count; i++)
			{
				Anymous_T anymous_T = _toRemove[i];
				if (anymous_T.deleted && anymous_T.callback != null)
				{
					_items.Remove(anymous_T.callback);
					ReturnToPool(anymous_T);
				}
			}
			_toRemove.Clear();
		}
		if (_toAdd.Count > 0)
		{
			Dictionary<TimerCallback, Anymous_T>.Enumerator enumerator = _toAdd.GetEnumerator();
			while (enumerator.MoveNext())
			{
				_items.Add(enumerator.Current.Key, enumerator.Current.Value);
			}
			enumerator.Dispose();
			_toAdd.Clear();
		}
	}
}
