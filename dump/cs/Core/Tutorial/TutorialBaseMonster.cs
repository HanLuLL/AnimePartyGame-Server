using Cysharp.Threading.Tasks;

namespace Core.Tutorial;

public abstract class TutorialBaseMonster
{
	public abstract UniTask StartEncounterEvent(long playerId, long target);

	public virtual bool ActionStatus()
	{
		return true;
	}
}
