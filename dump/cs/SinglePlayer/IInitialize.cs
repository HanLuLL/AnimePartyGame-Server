using Cysharp.Threading.Tasks;

namespace SinglePlayer;

public interface IInitialize
{
	UniTask Initialize();
}
public interface IInitialize<in T>
{
	UniTask Initialize(T t);
}
