using Cysharp.Threading.Tasks;

namespace SinglePlayer.GamePlay.Action;

public interface IPlayerActionState
{
	UniTask OnEnter();

	UniTask OnExit();
}
