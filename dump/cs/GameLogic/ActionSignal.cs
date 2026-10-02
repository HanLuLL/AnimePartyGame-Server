using Tools;

namespace GameLogic;

public class ActionSignal
{
	public readonly Signal<long> notifyAllPlayer = new Signal<long>();

	public readonly Signal<long> dealThrowDice = new Signal<long>();

	public readonly Signal<long> dealEffectCard = new Signal<long>();

	public readonly Signal<long, int> dealQuickCard = new Signal<long, int>();
}
