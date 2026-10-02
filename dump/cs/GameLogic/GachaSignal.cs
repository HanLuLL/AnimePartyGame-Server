using Tools;

namespace GameLogic;

public class GachaSignal
{
	public readonly Signal<bool> finishGacha = new Signal<bool>();

	public readonly Signal<int> gachaProgress = new Signal<int>();
}
