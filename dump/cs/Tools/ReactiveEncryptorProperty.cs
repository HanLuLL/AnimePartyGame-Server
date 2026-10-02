using System;
using System.Collections.Generic;

namespace Tools;

public class ReactiveEncryptorProperty<T, V> where T : struct where V : IEncryptor<T>, new()
{
	private static readonly IEqualityComparer<T> defaultEqualityComparer = ReactivePropertyEqualityComparer.GetDefault<T>();

	private V _encryptor;

	private readonly Signal<T> _signal;

	private readonly Signal<T, T> _signalWithPreviousValue;

	protected virtual IEqualityComparer<T> EqualityComparer => defaultEqualityComparer;

	protected V encryptor
	{
		get
		{
			if (_encryptor == null)
			{
				_encryptor = new V();
			}
			return _encryptor;
		}
	}

	protected T _value
	{
		get
		{
			return encryptor.DecryptGet();
		}
		set
		{
			encryptor.EncryptSet(value);
		}
	}

	public T Value
	{
		get
		{
			return _value;
		}
		set
		{
			if (!EqualityComparer.Equals(_value, value))
			{
				DispatchWithPreviousValue(ref value);
				JustSetValue(value);
				Dispatch(ref value);
			}
		}
	}

	public ReactiveEncryptorProperty()
		: this(default(T))
	{
	}

	public ReactiveEncryptorProperty(T initialValue)
	{
		_signal = new Signal<T>();
		_signalWithPreviousValue = new Signal<T, T>();
		JustSetValue(initialValue);
	}

	public void AddOnce(Action<T> callback)
	{
		_signal.AddOnce(callback);
	}

	public void AddListener(Action<T> callback, bool excuteImmediately = true)
	{
		_signal.AddListener(callback);
		if (excuteImmediately)
		{
			callback?.Invoke(_value);
		}
	}

	public virtual void RemoveListener(Action<T> callback)
	{
		_signal.RemoveListener(callback);
	}

	public virtual void RemoveAllListeners()
	{
		_signal.RemoveAllListeners();
		_signalWithPreviousValue.RemoveAllListeners();
	}

	private void Dispatch(ref T value)
	{
		_signal.Dispatch(value);
	}

	public virtual void JustSetValue(T value)
	{
		_value = value;
	}

	public void SetValueAndForceDispath(T value)
	{
		DispatchWithPreviousValue(ref value);
		JustSetValue(value);
		Dispatch(ref value);
	}

	public void AddOnce(Action<T, T> callback)
	{
		_signalWithPreviousValue.AddOnce(callback);
	}

	public void AddListener(Action<T, T> callback, bool excuteImmediately = true)
	{
		_signalWithPreviousValue.AddListener(callback);
		if (excuteImmediately)
		{
			callback?.Invoke(_value, _value);
		}
	}

	public virtual void RemoveListener(Action<T, T> callback)
	{
		_signalWithPreviousValue.RemoveListener(callback);
	}

	private void DispatchWithPreviousValue(ref T value)
	{
		_signalWithPreviousValue.Dispatch(_value, value);
	}

	public override string ToString()
	{
		return _value.ToString();
	}
}
