namespace Tools;

public class SimpleSingletonProvider<T> where T : class, new()
{
	private static T _inst;

	private static readonly object _lockHelper = new object();

	public static T inst
	{
		get
		{
			if (_inst == null)
			{
				lock (_lockHelper)
				{
					if (_inst == null)
					{
						_inst = new T();
						(_inst as SimpleSingletonProvider<T>)?.InstanceInit();
					}
				}
			}
			(_inst as SimpleSingletonProvider<T>)?.OnGetInstance();
			return _inst;
		}
	}

	public static bool hasInstance => _inst != null;

	protected virtual void InstanceInit()
	{
	}

	protected virtual void OnGetInstance()
	{
	}

	protected virtual void OnDestroyInstance()
	{
		_inst = null;
	}
}
