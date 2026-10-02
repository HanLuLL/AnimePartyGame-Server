using Tools;

namespace GameLogic;

public class BagSignal
{
	public readonly Signal bagMapChanged = new Signal();

	public readonly Signal<int> onItemAdded = new Signal<int>();

	public readonly Signal<int, int> onItemUpdated = new Signal<int, int>();

	public readonly Signal<int> onItemRemoved = new Signal<int>();

	public readonly Signal<int, int> onItemCountChanged = new Signal<int, int>();
}
