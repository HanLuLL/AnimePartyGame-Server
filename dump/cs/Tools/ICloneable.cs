namespace Tools;

public interface ICloneable<out T>
{
	T Clone();
}
