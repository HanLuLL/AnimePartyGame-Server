namespace Tools;

public class MergeSignal<T> where T : struct
{
	private readonly Signal s = new Signal();

	private readonly ReactiveProperty<T>[] _rps;

	public MergeSignal(ReactiveProperty<T>[] rps)
	{
		_rps = rps;
		for (int i = 0; i < _rps.Length; i++)
		{
			_rps[i].AddListener(OnDispatch);
		}
	}

	public void RemoveListener()
	{
		ReactiveProperty<T>[] rps = _rps;
		for (int i = 0; i < rps.Length; i++)
		{
			rps[i].RemoveListener(OnDispatch);
		}
	}

	private void OnDispatch(T t)
	{
		s.Dispatch();
	}
}
