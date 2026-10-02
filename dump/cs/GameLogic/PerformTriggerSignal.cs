using Tools;

namespace GameLogic;

public class PerformTriggerSignal
{
	public Signal<long, long, int, bool> characterFightHitSignal = new Signal<long, long, int, bool>();

	public Signal<long, int> heroMovePointsSignal = new Signal<long, int>();

	public Signal<long, int> heroStarUpSignal = new Signal<long, int>();

	public Signal<int> monsterShowSignal = new Signal<int>();

	public Signal refereeJoinBattleSignal = new Signal();
}
