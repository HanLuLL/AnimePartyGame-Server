namespace Tools;

public class CustomAsyncResultT2<T1, T2> : CustomAsyncResult
{
	public T1 Param1 { get; private set; }

	public T2 Param2 { get; private set; }

	public CustomAsyncResultT2(T1 param1, T2 param2)
	{
		Param1 = param1;
		Param2 = param2;
	}
}
