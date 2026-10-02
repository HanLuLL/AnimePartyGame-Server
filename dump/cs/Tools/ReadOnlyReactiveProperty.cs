namespace Tools;

public class ReadOnlyReactiveProperty<T> : ReactiveProperty<T>
{
	public new T Value => _value;

	public ReadOnlyReactiveProperty(T initialValue)
		: base(initialValue)
	{
	}

	public void SetValue(T value)
	{
		if (!EqualityComparer.Equals(_value, value))
		{
			SetValueAndForceDispath(value);
		}
	}
}
