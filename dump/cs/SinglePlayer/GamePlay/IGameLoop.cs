using Cysharp.Threading.Tasks;

namespace SinglePlayer.GamePlay;

public interface IGameLoop
{
	UniTask<bool> Execute();

	void Dispose();
}
