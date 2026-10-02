namespace Tools;

public class CustomAsyncResultT<T> : CustomAsyncResult
{
	public T Param { get; private set; }

	public CustomAsyncResultT(T param)
	{
		Param = param;
	}
}
