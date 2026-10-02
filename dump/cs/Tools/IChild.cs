namespace Tools;

public interface IChild<in V>
{
	T child<T>() where T : V;
}
