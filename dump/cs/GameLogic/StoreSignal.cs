using Tools;

namespace GameLogic;

public class StoreSignal
{
	public readonly Signal<int> refreshCurShelf = new Signal<int>();
}
