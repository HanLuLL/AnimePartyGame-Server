using Tools;

namespace GameLogic;

public class ActivitySignal
{
	public readonly Signal<int> switchTokenList = new Signal<int>();

	public readonly Signal<bool> PlayerLabelController = new Signal<bool>();

	public readonly Signal<int> activityStatus = new Signal<int>();
}
