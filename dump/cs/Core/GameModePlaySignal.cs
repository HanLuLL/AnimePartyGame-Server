using Tools;

namespace Core;

public class GameModePlaySignal
{
	public readonly Signal<int> score = new Signal<int>();

	public readonly Signal<int, bool> luckyStar = new Signal<int, bool>();

	public readonly Signal<int> luckyStarMission = new Signal<int>();
}
