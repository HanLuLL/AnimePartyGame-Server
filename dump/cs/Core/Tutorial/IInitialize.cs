using Cysharp.Threading.Tasks;

namespace Core.Tutorial;

public interface IInitialize
{
	UniTask Initialize();
}
public interface IInitialize<in T>
{
	UniTask Initialize(T t);
}
