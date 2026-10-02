using System;
using Tools;

namespace Core;

public class PropertyData<T>
{
	public ReactiveProperty<T> property;

	public int buffId;

	public Action<T> UpdateAction;

	public PropertyData(T initialValue, int buffConfigId)
	{
		property = new ReactiveProperty<T>(initialValue);
		buffId = buffConfigId;
	}

	public void UpdateProperty(T newValue)
	{
		property.Value = newValue;
		UpdateAction?.Invoke(newValue);
	}

	public void ClearProperty()
	{
		UpdateAction = null;
	}
}
