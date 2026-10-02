using System;
using System.Collections.Generic;

namespace Tools;

public class Signal
{
	protected readonly HashSet<Action> _cachedOnceListeners;

	protected readonly HashSet<Action> _cachedListeners;

	private event Action _onceListener;

	protected event Action _listener;

	public Signal(int capcity = 8)
	{
		if (capcity <= 0)
		{
			capcity = 8;
		}
		_cachedListeners = new HashSet<Action>(capcity);
		_cachedOnceListeners = new HashSet<Action>(capcity);
	}

	public void AddOnce(Action callback)
	{
		if (_cachedOnceListeners.Add(callback))
		{
			_onceListener += callback;
		}
	}

	public void AddListener(Action callback)
	{
		if (_cachedListeners.Add(callback))
		{
			_listener += callback;
		}
	}

	public void RemoveListener(Action callback)
	{
		if (_cachedListeners.Remove(callback))
		{
			_listener -= callback;
		}
	}

	private void RemoveOncedListener()
	{
		this._onceListener = null;
		_cachedOnceListeners.Clear();
	}

	private void RemoveListener()
	{
		this._listener = null;
		_cachedListeners.Clear();
	}

	public void RemoveAllListeners()
	{
		RemoveOncedListener();
		RemoveListener();
	}

	public void Dispatch()
	{
		this._onceListener?.Invoke();
		RemoveOncedListener();
		this._listener?.Invoke();
	}
}
public class Signal<T>
{
	protected readonly HashSet<Action<T>> _cachedOnceListeners;

	protected readonly HashSet<Action<T>> _cachedListeners;

	private event Action<T> _onceListener;

	protected event Action<T> _listener;

	public Signal(int capcity = 8)
	{
		if (capcity <= 0)
		{
			capcity = 8;
		}
		_cachedListeners = new HashSet<Action<T>>(capcity);
		_cachedOnceListeners = new HashSet<Action<T>>(capcity);
	}

	public void AddOnce(Action<T> callback)
	{
		if (_cachedOnceListeners.Add(callback))
		{
			_onceListener += callback;
		}
	}

	public void AddListener(Action<T> callback)
	{
		if (_cachedListeners.Add(callback))
		{
			_listener += callback;
		}
	}

	public void RemoveListener(Action<T> callback)
	{
		if (_cachedListeners.Remove(callback))
		{
			_listener -= callback;
		}
	}

	private void RemoveOncedListener()
	{
		this._onceListener = null;
		_cachedOnceListeners.Clear();
	}

	private void RemoveListener()
	{
		this._listener = null;
		_cachedListeners.Clear();
	}

	public void RemoveAllListeners()
	{
		RemoveOncedListener();
		RemoveListener();
	}

	public void Dispatch(T t)
	{
		this._onceListener?.Invoke(t);
		RemoveOncedListener();
		this._listener?.Invoke(t);
	}
}
public class Signal<T1, T2>
{
	protected readonly HashSet<Action<T1, T2>> _cachedOnceListeners;

	protected readonly HashSet<Action<T1, T2>> _cachedListeners;

	private event Action<T1, T2> _onceListener;

	protected event Action<T1, T2> _listener;

	public Signal(int capcity = 8)
	{
		if (capcity <= 0)
		{
			capcity = 8;
		}
		_cachedListeners = new HashSet<Action<T1, T2>>(capcity);
		_cachedOnceListeners = new HashSet<Action<T1, T2>>(capcity);
	}

	public void AddOnce(Action<T1, T2> callback)
	{
		if (_cachedOnceListeners.Add(callback))
		{
			_onceListener += callback;
		}
	}

	public void AddListener(Action<T1, T2> callback)
	{
		if (_cachedListeners.Add(callback))
		{
			_listener += callback;
		}
	}

	public void RemoveListener(Action<T1, T2> callback)
	{
		if (_cachedListeners.Remove(callback))
		{
			_listener -= callback;
		}
	}

	private void RemoveOncedListener()
	{
		this._onceListener = null;
		_cachedOnceListeners.Clear();
	}

	private void RemoveListener()
	{
		this._listener = null;
		_cachedListeners.Clear();
	}

	public void RemoveAllListeners()
	{
		RemoveOncedListener();
		RemoveListener();
	}

	public void Dispatch(T1 t1, T2 t2)
	{
		this._onceListener?.Invoke(t1, t2);
		RemoveOncedListener();
		this._listener?.Invoke(t1, t2);
	}
}
public class Signal<T1, T2, T3>
{
	protected readonly HashSet<Action<T1, T2, T3>> _cachedOnceListeners;

	protected readonly HashSet<Action<T1, T2, T3>> _cachedListeners;

	private event Action<T1, T2, T3> _onceListener;

	protected event Action<T1, T2, T3> _listener;

	public Signal(int capcity = 8)
	{
		if (capcity <= 0)
		{
			capcity = 8;
		}
		_cachedListeners = new HashSet<Action<T1, T2, T3>>(capcity);
		_cachedOnceListeners = new HashSet<Action<T1, T2, T3>>(capcity);
	}

	public void AddOnce(Action<T1, T2, T3> callback)
	{
		if (_cachedOnceListeners.Add(callback))
		{
			_onceListener += callback;
		}
	}

	public void AddListener(Action<T1, T2, T3> callback)
	{
		if (_cachedListeners.Add(callback))
		{
			_listener += callback;
		}
	}

	public void RemoveListener(Action<T1, T2, T3> callback)
	{
		if (_cachedListeners.Remove(callback))
		{
			_listener -= callback;
		}
	}

	private void RemoveOncedListener()
	{
		this._onceListener = null;
		_cachedOnceListeners.Clear();
	}

	private void RemoveListener()
	{
		this._listener = null;
		_cachedListeners.Clear();
	}

	public void RemoveAllListeners()
	{
		RemoveOncedListener();
		RemoveListener();
	}

	public void Dispatch(T1 t1, T2 t2, T3 t3)
	{
		this._onceListener?.Invoke(t1, t2, t3);
		RemoveOncedListener();
		this._listener?.Invoke(t1, t2, t3);
	}
}
public class Signal<T1, T2, T3, T4>
{
	protected readonly HashSet<Action<T1, T2, T3, T4>> _cachedOnceListeners;

	protected readonly HashSet<Action<T1, T2, T3, T4>> _cachedListeners;

	private event Action<T1, T2, T3, T4> _onceListener;

	protected event Action<T1, T2, T3, T4> _listener;

	public Signal(int capcity = 8)
	{
		if (capcity <= 0)
		{
			capcity = 8;
		}
		_cachedListeners = new HashSet<Action<T1, T2, T3, T4>>(capcity);
		_cachedOnceListeners = new HashSet<Action<T1, T2, T3, T4>>(capcity);
	}

	public void AddOnce(Action<T1, T2, T3, T4> callback)
	{
		if (_cachedOnceListeners.Add(callback))
		{
			_onceListener += callback;
		}
	}

	public void AddListener(Action<T1, T2, T3, T4> callback)
	{
		if (_cachedListeners.Add(callback))
		{
			_listener += callback;
		}
	}

	public void RemoveListener(Action<T1, T2, T3, T4> callback)
	{
		if (_cachedListeners.Remove(callback))
		{
			_listener -= callback;
		}
	}

	private void RemoveOncedListener()
	{
		this._onceListener = null;
		_cachedOnceListeners.Clear();
	}

	private void RemoveListener()
	{
		this._listener = null;
		_cachedListeners.Clear();
	}

	public void RemoveAllListeners()
	{
		RemoveOncedListener();
		RemoveListener();
	}

	public void Dispatch(T1 t1, T2 t2, T3 t3, T4 t4)
	{
		this._onceListener?.Invoke(t1, t2, t3, t4);
		RemoveOncedListener();
		this._listener?.Invoke(t1, t2, t3, t4);
	}
}
