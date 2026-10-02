using Cysharp.Threading.Tasks;

namespace Core.Tutorial;

public interface IGameLoop
{
	UniTask<bool> Execute();

	void Dispose();
}
