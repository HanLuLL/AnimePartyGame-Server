using Cysharp.Threading.Tasks;

namespace Core.Tutorial;

public interface IPlayerActionState
{
	UniTask OnEnter();

	UniTask OnExit();
}
