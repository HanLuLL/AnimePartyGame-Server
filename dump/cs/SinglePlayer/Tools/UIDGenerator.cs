namespace SinglePlayer.Tools;

public static class UIDGenerator
{
	private static int _currentUID;

	private static readonly object _lock = new object();

	public static void SetCurrentUID(int uid)
	{
		_currentUID = uid;
	}

	public static int NextUID()
	{
		lock (_lock)
		{
			_currentUID++;
			return _currentUID;
		}
	}

	public static void Reset()
	{
		lock (_lock)
		{
			_currentUID = 0;
		}
	}
}
