using Cysharp.Threading.Tasks;

namespace Core.Tutorial;

public abstract class BaseTutorialLand
{
	public abstract UniTask Pass(int landId, long playerId);

	public abstract UniTask Stay(int landId, long playerId);
}
