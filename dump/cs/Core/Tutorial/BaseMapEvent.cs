using Cysharp.Threading.Tasks;

namespace Core.Tutorial;

public abstract class BaseMapEvent
{
	public abstract UniTask TryActiveEvent();
}
