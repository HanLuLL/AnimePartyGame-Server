using Tools;

namespace GameLogic;

public class CampaignSignal
{
	public readonly Signal<int, bool> unlockLevel = new Signal<int, bool>();

	public readonly Signal campaignCondition = new Signal();
}
